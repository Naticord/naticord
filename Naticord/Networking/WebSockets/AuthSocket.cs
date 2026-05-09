using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Naticord.Networking.WebSockets
{
    internal class AuthSocket : IDisposable
    {
        #region Variables used by AuthSocket
        // The required URL variables for this to actually work
        private const string gatewayUrl = "wss://remote-auth-gateway.discord.gg/?v=2";
        private const string loginEndpoint = "users/@me/remote-auth/login";

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // The client required for the WebSockets to function
        private ClientWebSocket _ws;
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        // Cancellation token for the socket
        private CancellationTokenSource _cts;

        private readonly SynchronizationContext _syncContext;
        private bool _disposed;
        private int _heartbeatInterval;

        private readonly AsymmetricCipherKeyPair _keyPair;
        private readonly byte[] _spkiBytes;

        // All actions for the socket
        public event Action<string> qrCodeReady;
        public event Action<string> tokenReceived;
        #endregion

        public AuthSocket()
        {
            _syncContext = SynchronizationContext.Current;
            var gen = new RsaKeyPairGenerator();

            // Generate a keypair for later...
            gen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            _keyPair = gen.GenerateKeyPair();

            _spkiBytes = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(_keyPair.Public).GetDerEncoded();
        }

        #region Start and stop socket functions
        public async Task StartSocket()
        {
            _cts = new CancellationTokenSource();
            _ws = new ClientWebSocket();

            // Required so Discord doesn't kick us out for being an unknown source
            _ws.Options.SetRequestHeader("Origin", "https://discord.com");

            // Connect to the gateway!
            await _ws.ConnectAsync(new Uri(gatewayUrl), CancellationToken.None);
            _ = Task.Run(ReceiveLoop);
        }

        public void StopSocket() => _cts?.Cancel();
        #endregion

        #region WebSocket loops
        private async Task ReceiveLoop()
        {
            var buffer = new byte[16384];
            var sb = new StringBuilder();

            while (_ws.State == WebSocketState.Open && !_cts.IsCancellationRequested)
            {
                sb.Clear();
                WebSocketReceiveResult result;

                do
                {
                    result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    if (result.MessageType == WebSocketMessageType.Close) { return; }

                    sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                }
                while (!result.EndOfMessage);
                await HandleMessage(sb.ToString());
            }
        }

        private async Task HeartbeatLoop()
        {
            await Task.Delay(_heartbeatInterval);
            while (!_cts.IsCancellationRequested && _ws.State == WebSocketState.Open)
            {
                await SendMessage(JsonSerializer.Serialize(new Dictionary<string, object> { { "op", "heartbeat" } }, jsonOptions));
                await Task.Delay(_heartbeatInterval, _cts.Token);
            }
        }
        #endregion

        #region WS message functions (Sending stuff to Discord)
        private async Task SendInit()
        {
            await SendMessage(JsonSerializer.Serialize(new Dictionary<string, object>
            {
                { "op", "init" },
                { "encoded_public_key", Convert.ToBase64String(_spkiBytes) }
            }, jsonOptions));
        }

        private async Task SendNonceProof(JsonElement root)
        {
            string nonce = DecryptToUrlSafeBase64(root.GetProperty("encrypted_nonce").GetString());
            await SendMessage(JsonSerializer.Serialize(new Dictionary<string, object>
            {
                { "op", "nonce_proof" },
                { "nonce", nonce }
            }, jsonOptions));
        }

        private void HandlePendingRemoteInit(JsonElement root)
        {
            string fingerprint = root.GetProperty("fingerprint").GetString();
            using (var sha256 = SHA256.Create())
            {
                string expected = Convert.ToBase64String(sha256.ComputeHash(_spkiBytes)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
                if (fingerprint != expected)
                {
                    StopSocket();
                    return;
                }
            }

            Fire(qrCodeReady, "https://discord.com/ra/" + fingerprint);
        }

        private void HandlePendingTicket(JsonElement root)
        {
            string userPayload = DecryptToUtf8(root.GetProperty("encrypted_user_payload").GetString());

            string[] parts = userPayload.Split(':');
            string username = parts.Length >= 4 ? parts[3] : "Unknown";
        }

        private async Task HandlePendingLogin(JsonElement root)
        {
            string ticket = root.GetProperty("ticket").GetString();
            string response = await API.Instance.SendAPI(loginEndpoint, HttpMethod.Post, data: new { ticket });

            using (var doc = JsonDocument.Parse(response))
            {
                var rootEl = doc.RootElement;
                if (rootEl.TryGetProperty("captcha_key", out _) || rootEl.TryGetProperty("captcha_sitekey", out _))
                {
                    // You are now stuck in a CAPTCHA loop, good luck!
                    Fire<string>(_ => MessageBox.Show("Discord is requesting for a CAPTCHA to be completed, Naticord does not support these as of now. Please try again later.", "Naticord", MessageBoxButtons.OK, MessageBoxIcon.Error), null);
                    return;
                }
                if (!rootEl.TryGetProperty("encrypted_token", out var tokenEl) || tokenEl.ValueKind != JsonValueKind.String) { return; }

                Fire(tokenReceived, DecryptToUtf8(tokenEl.GetString()));
            }
        }
        #endregion

        #region WS message functions (The actual backend of them)
        private async Task HandleMessage(string data)
        {
            using (var doc = JsonDocument.Parse(data))
            {
                var root = doc.RootElement;
                string op = root.GetProperty("op").GetString() ?? "";

                switch (op)
                {
                    case "hello":
                        // Start the heartbeat loop then send the initial payload
                        _heartbeatInterval = root.GetProperty("heartbeat_interval").GetInt32();
                        Task.Run(() => HeartbeatLoop());

                        await SendInit();
                        break;
                    case "nonce_proof": await SendNonceProof(root); break;
                    case "pending_remote_init": HandlePendingRemoteInit(root); break;
                    case "pending_ticket": HandlePendingTicket(root); break;
                    case "pending_login": await HandlePendingLogin(root); break;
                }
            }
        }

        private async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await _sendLock.WaitAsync(CancellationToken.None);

            try { await _ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None); }
            finally { _sendLock.Release(); }
        }
        #endregion

        #region Decrypt functions
        private string DecryptToUrlSafeBase64(string encBase64) { return Convert.ToBase64String(OaepDecrypt(encBase64)).TrimEnd('=').Replace('+', '-').Replace('/', '_'); }
        private string DecryptToUtf8(string encBase64) { return Encoding.UTF8.GetString(OaepDecrypt(encBase64)); }

        private byte[] OaepDecrypt(string encBase64)
        {
            var engine = new OaepEncoding(new RsaEngine(), new Org.BouncyCastle.Crypto.Digests.Sha256Digest());
            engine.Init(false, _keyPair.Private);

            byte[] enc = Convert.FromBase64String(encBase64);
            return engine.ProcessBlock(enc, 0, enc.Length);
        }
        #endregion

        private void Fire<T>(Action<T> handler, T arg)
        {
            if (handler == null) return;

            if (_syncContext != null)
                _syncContext.Post(_ => handler(arg), null);
            else
                handler(arg);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _cts?.Cancel();
            _ws?.Dispose();
            _sendLock?.Dispose();
            _cts?.Dispose();
        }
    }
}