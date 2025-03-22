// This is a very early implementation of the Websockets.
// This was made with the help of the documentation from discord.sex
// Without them, I never would've gotten the right implementation of it.

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    class Websocket
    {
        private string token;
        private string gatewayUrl;
        private Client mainClient;
        private const SslProtocols Tls12 = SslProtocols.Tls12;
        public WebSocketSharp.WebSocket WSClient { get; private set; }
        private int heartbeatInterval;

        public Websocket(Client clientForm)
        {
            mainClient = clientForm;
            token = Properties.Settings.Default.token;
            gatewayUrl = "wss://gateway.discord.gg/?v=9&encoding=json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            InitWS();
        }

        public static class UserStatusStore
        {
            private static readonly ConcurrentDictionary<string, string> _userStatuses =
                new ConcurrentDictionary<string, string>();

            public static void UpdateStatus(string userId, string status)
            {
                _userStatuses.AddOrUpdate(userId, status, (key, oldValue) => status);
            }

            public static string GetStatus(string userId)
            {
                return _userStatuses.TryGetValue(userId, out var status) ? status : "Offline";
            }

            public static void Clear()
            {
                _userStatuses.Clear();
            }
        }

        public void InitWS()
        {
            WSClient = new WebSocketSharp.WebSocket(gatewayUrl);
            WSClient.SslConfiguration.EnabledSslProtocols = Tls12;

            WSClient.OnOpen += (sender, e) => Debug.WriteLine("Connected to the gateway.");
            WSClient.OnMessage += (sender, e) => HandleMessage(e.Data);
            WSClient.OnClose += (sender, e) =>
            {
                Debug.WriteLine($"Disconnected from the gateway. Reason: {e.Reason}, Code: {e.Code}");

                if (e.Code != 1000 && e.Code != 4004)
                {
                    Debug.WriteLine("Reconnecting...");
                    InitWS();
                }
            };
            WSClient.OnError += (sender, e) => Debug.WriteLine($"Error! {e.Message}");

            WSClient.Connect();
            SendPayload();
        }

        private void HandleMessage(string data)
        {
            // Debug.WriteLine($"Received: {data}");

            try
            {
                var json = JObject.Parse(data);
                int opCode = json["op"]?.Value<int>() ?? -1;

                switch (opCode)
                {
                    case 0: // Dispatch Event
                        string eventType = json["t"]?.Value<string>() ?? "";

                        switch (eventType)
                        {
                            case "READY":
                                HandleUserStatus(json["d"]);
                                break;
                            case "MESSAGE_CREATE":
                                HandleMessageCreate(json["d"]);
                                break;
                            default:
                                // Debug.WriteLine($"Unhandled event type: {eventType}");
                                break;
                        }
                        break;

                    case 10: // Hello from the gateway (Op 10)
                        heartbeatInterval = json["d"]?["heartbeat_interval"]?.Value<int>() ?? 0;
                        Debug.WriteLine($"Received Op 10 (Hello), sending Op 1 (Heartbeat) every {heartbeatInterval}ms");
                        SendHeartbeat();
                        break;

                    case 11: // Heartbeat ACK
                        Debug.WriteLine("Heartbeat confirmed!");
                        break;
                    default:
                        Debug.WriteLine($"Unhandled opcode: {opCode}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing message: {ex.Message}");
            }
        }

        private async void HandleMessageCreate(JToken messageData)
        {
            string attachmentUrl = null;
            var attachments = messageData["attachments"] as JArray;
            if (attachments != null && attachments.Count > 0)
            {
                attachmentUrl = attachments[0]["url"]?.ToString();
            }

            string authorUser = messageData["author"]?["username"]?.Value<string>() ?? "Unknown";
            string authorDisplay = messageData["author"]?["global_name"]?.Value<string>() ?? "Unknown";
            string authorAvatar = messageData["author"]?["avatar"]?.Value<string>() ?? "Unknown";
            string authorID = messageData["author"]?["id"]?.Value<string>() ?? "Unknown";
            string wsChannelId = messageData["channel_id"]?.Value<string>() ?? "Unknown";
            string content = messageData["content"]?.Value<string>() ?? "";

            string displayName = !string.IsNullOrWhiteSpace(authorDisplay) ? authorDisplay :
                                 !string.IsNullOrWhiteSpace(authorUser) ? authorUser : "Unknown";

            Image attachment = null;
            if (attachmentUrl != null)
            {
                attachment = await mainClient.DownloadImage(attachmentUrl);
            }
            await mainClient.AddMessage(displayName, content, authorID, authorAvatar, attachment, wsChannelId);
        }

        private void HandleUserStatus(JToken messageData)
        {
            if (messageData["presences"] is JArray presencesArray)
            {
                foreach (var presence in presencesArray)
                {
                    string userId = presence?["user"]?["id"]?.Value<string>() ?? "Unknown";
                    string rawStatus = presence?["status"]?.Value<string>() ?? "offline";
                    string userStatus = MapStatus(rawStatus);

                    UserStatusStore.UpdateStatus(userId, userStatus);
                }
            }
            else
            {
                Debug.WriteLine("No presences found in the message data.");
            }
        }

        private string MapStatus(string rawStatus)
        {
            return rawStatus.ToLower() switch
            {
                "online" => "Online",
                "dnd" => "Do Not Disturb",
                "idle" => "Idle",
                "offline" => "Offline",
                _ => "No status"
            };
        }

        private void SendPayload()
        {
            if (WSClient.ReadyState == WebSocketSharp.WebSocketState.Open)
            {
                var identifyPayload = new
                {
                    op = 2,
                    d = new
                    {
                        token = token,
                        properties = new
                        {
                            os = "windows",
                            browser = "disco",
                            device = "disco"
                        }
                    }
                };

                try
                {
                    string payloadJson = JsonConvert.SerializeObject(identifyPayload);
                    WSClient.Send(payloadJson);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending identify payload: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("WebSocket connection is not open. Unable to send identify payload.");
            }
        }

        private async void SendHeartbeat()
        {
            while (WSClient.ReadyState == WebSocketSharp.WebSocketState.Open)
            {
                var heartbeatPayload = new { op = 1, d = (object)null };

                try
                {
                    string payloadJson = JsonConvert.SerializeObject(heartbeatPayload);
                    WSClient.Send(payloadJson);
                    Debug.WriteLine("Sent Op 1 (Heartbeat).");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending heartbeat: {ex.Message}");
                }   

                await Task.Delay(heartbeatInterval);
            }
        }
    }
}