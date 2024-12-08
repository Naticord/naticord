using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace Naticord
{
    public class WebSocketClient
    {
        private static WebSocketClient _instance;
        public Naticord parentClientForm;
        public DM parentDMForm;
        public Group parentGroupForm;
        public Server parentServerForm;
        public WebSocket webSocket;
        private string accessToken;
        private const SslProtocols Tls12 = (SslProtocols)0x00000C00;
        private bool websocketClosed = false;

        private WebSocketClient(string accessToken, Naticord parentClientForm = null, DM parentDMForm = null, Group parentGroupForm = null, Server parentServerForm = null)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            this.accessToken = accessToken;
            this.parentClientForm = parentClientForm;
            this.parentDMForm = parentDMForm;
            this.parentGroupForm = parentGroupForm;
            this.parentServerForm = parentServerForm;
            InitializeWebSocket();
        }

        public static WebSocketClient Instance(string accessToken, Naticord parentClientForm = null, DM parentDMForm = null, Group parentGroupForm = null, Server parentServerForm = null)
        {
            if (_instance == null)
            {
                _instance = new WebSocketClient(accessToken, parentClientForm, parentDMForm, parentGroupForm, parentServerForm);
            }
            return _instance;
        }

        private void InitializeWebSocket()
        {
            webSocket = new WebSocket($"wss://gateway.discord.gg/?v=9&encoding=json");
            webSocket.SslConfiguration.EnabledSslProtocols = Tls12;
            webSocket.OnMessage += async (sender, e) => await HandleWebSocketMessage(e.Data);
            webSocket.OnError += (sender, e) => HandleWebSocketError(e.Message);
            webSocket.OnClose += (sender, e) => HandleWebSocketClose();
            webSocket.Connect();
            SendIdentifyPayload();
        }

        private void SendIdentifyPayload()
        {
            if (webSocket.ReadyState == WebSocketState.Open)
            {
                var identifyPayload = new
                {
                    op = 2,
                    d = new
                    {
                        token = accessToken,
                        properties = new
                        {
                            os = "windows",
                            browser = "chrome",
                            device = "pc"
                        }
                    }
                };

                try
                {
                    string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(identifyPayload);
                    webSocket.Send(payloadJson);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending identify payload: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("WebSocket connection is not open. Unable to send identify payload.");
            }
        }

        private async Task HandleWebSocketMessage(string data)
        {
            var json = JObject.Parse(data);
            int opCode = (int)json["op"];

            switch (opCode)
            {
                case 0:
                    string eventType = (string)json["t"];
                    switch (eventType)
                    {
                        case "READY":
                            ParseReadyEvent(data);
                            break;
                        case "USER_SETTINGS_UPDATE":
                            ParseCustomStatusText(data);
                            break;
                        case "TYPING_START":
                            HandleTypingStartEvent(json["d"]);
                            break;
                        case "MESSAGE_CREATE":
                            await HandleMessageCreateEventAsync(json["d"]);
                            HandleTypingStopEvent(json["d"]);
                            break;
                        case "PRESENCE_UPDATE":
                            Debug.WriteLine("Received status update");
                            break;
                        default:
                            Debug.WriteLine($"Unhandled event type: {eventType}");
                            break;
                    }
                    break;

                case 1:
                    Debug.WriteLine("Heartbeat event received");
                    break;

                case 10:
                    Debug.WriteLine("Hello! From Discord Gateway");
                    break;

                default:
                    Debug.WriteLine($"Unhandled OpCode: {opCode}");
                    break;
            }
        }

        private void ParseReadyEvent(string data)
        {
            try
            {
                var json = JObject.Parse(data);
                var user = json["d"]?["user"];
                string username = (string)user?["username"];
                string discriminator = (string)user?["discriminator"];
                Debug.WriteLine($"Logged in as {username}#{discriminator}");

                string userStatusText = (string)json["d"]?["user"]?["presence"]?["status"] ?? "Unknown";

                if (parentClientForm != null)
                {
                    UpdateFormLabel(parentClientForm.descriptionLabel, userStatusText);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing READY event: {ex.Message}");
            }
        }

        private void ParseCustomStatusText(string data)
        {
            try
            {
                var json = JObject.Parse(data);
                var customStatus = json["d"]?["custom_status"];
                string statusText = (string)customStatus?["text"] ?? "";

                if (parentClientForm != null)
                {
                    UpdateFormLabel(parentClientForm.descriptionLabel, statusText);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing custom status: {ex.Message}");
            }
        }

        private void HandleTypingStartEvent(JToken jToken)
        {
            if (parentDMForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentDMForm.ChatID)
                {
                    string userId = (string)jToken["user_id"];
                    string username = GetUsernameById(userId);
                    UpdateFormLabel(parentDMForm.typingStatus, $"{username} is typing...");
                }
            }

            if (parentGroupForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentGroupForm.ChatID)
                {
                    string userId = (string)jToken["user_id"];
                    string username = GetUsernameById(userId);
                }
            }

            if (parentServerForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentServerForm.ChatID)
                {
                    string userId = (string)jToken["user_id"];
                    string username = GetUsernameById(userId);
                }
            }
        }

        private void HandleTypingStopEvent(JToken jToken)
        {
            if (parentDMForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentDMForm.ChatID)
                {
                    UpdateFormLabel(parentDMForm.typingStatus, string.Empty);
                }
            }

            if (parentGroupForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentGroupForm.ChatID)
                {
                    // do nothing
                }
            }

            if (parentServerForm != null)
            {
                string channelId = (string)jToken["channel_id"];
                if (long.TryParse(channelId, out long parsedChannelId) && parsedChannelId == parentServerForm.ChatID)
                {
                    // do nothing
                }
            }
        }

        private async Task HandleMessageCreateEventAsync(JToken data)
        {
            if (parentDMForm != null)
            {
                string channelId = (string)data["channel_id"];
                if (channelId == parentDMForm.ChatID.ToString())
                {
                    string author = (string)data["author"]?["global_name"] ?? (string)data["author"]?["username"];
                    string content = (string)data["content"];
                    parentDMForm.AddMessage(author, content, "said", null, null, true, true);
                    parentDMForm.Invoke((MethodInvoker)(() => parentDMForm.ScrollToBottom()));
                }
            }

            if (parentGroupForm != null)
            {
                string channelId = (string)data["channel_id"];
                if (channelId == parentGroupForm.ChatID.ToString())
                {
                    string author = (string)data["author"]?["global_name"] ?? (string)data["author"]?["username"];
                    string content = (string)data["content"];
                    parentGroupForm.AddMessage(author, content, "said", null, null, true, true);
                    parentGroupForm.Invoke((MethodInvoker)(() => parentGroupForm.ScrollToBottom()));
                }
            }

            if (parentServerForm != null)
            {
                string channelId = (string)data["channel_id"];
                if (channelId == parentServerForm.ChatID.ToString())
                {
                    string author = (string)data["author"]?["global_name"] ?? (string)data["author"]?["username"];
                    string content = (string)data["content"];
                    parentServerForm.AddMessage(author, content, "said", null, null, true, true);
                    parentServerForm.Invoke((MethodInvoker)(() => parentServerForm.ScrollToBottom()));
                }
            }
        }

        private string GetUsernameById(string userId)
        {
            try
            {
                dynamic user = parentDMForm?.GetApiResponse($"users/{userId}") ?? parentGroupForm?.GetApiResponse($"users/{userId}") ?? parentServerForm?.GetApiResponse($"users/{userId}");
                return user?.username ?? "Unknown";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get username for user ID {userId}: {ex.Message}");
                return "Unknown";
            }
        }

        private void UpdateFormLabel(Control label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke((Action)(() => label.Text = text));
            }
            else
            {
                label.Text = text;
            }
        }

        private void HandleWebSocketError(string errorMessage)
        {
            Console.WriteLine($"WebSocket Error: {errorMessage}");
            InitializeWebSocket();
        }

        private void HandleWebSocketClose()
        {
            if (!websocketClosed)
            {
                Console.WriteLine("WebSocket connection closed. Attempting to reconnect...");
                InitializeWebSocket();
            }
        }

        public void CloseWebSocket()
        {
            websocketClosed = true;
            webSocket.Close();
        }

        public class Attachment
        {
            public string URL { get; set; }
            public string Type { get; set; }
        }

        public class Embed
        {
            public string Type { get; set; }
            public string Author { get; set; }
            public string AuthorURL { get; set; }
            public string Title { get; set; }
            public string TitleURL { get; set; }
            public string Description { get; set; }
        }
    }
}
