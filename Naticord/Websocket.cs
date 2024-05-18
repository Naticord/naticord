using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace Naticord
{
    public class WebSocketClient
    {
        private Naticord parentNaticordForm;
        private WebSocket webSocket;
        private readonly string accessToken;
        private const SslProtocols Tls12 = (SslProtocols)0x00000C00;
        private Dictionary<string, string> userCache = new Dictionary<string, string>();

        public WebSocketClient(string accessToken, Naticord parentNaticordForm)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            Console.WriteLine($"Using Access Token: {accessToken} to get WS events");

            this.accessToken = accessToken;
            this.parentNaticordForm = parentNaticordForm;
            InitializeWebSocket();
        }

        private void InitializeWebSocket()
        {
            webSocket = new WebSocket($"wss://gateway.discord.gg/?v=9&encoding=json");
            webSocket.SslConfiguration.EnabledSslProtocols = Tls12;
            webSocket.OnMessage += (sender, e) => HandleWebSocketMessage(e.Data);
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
                    webSocket.Send(Newtonsoft.Json.JsonConvert.SerializeObject(identifyPayload));
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

        private void HandleWebSocketMessage(string data)
        {
            Console.WriteLine($"WebSocket Received: {data}");

            var json = JObject.Parse(data);
            int opCode = (int)json["op"];

            switch (opCode)
            {
                case 0:
                    string eventType = (string)json["t"];
                    switch (eventType)
                    {
                        case "TYPING_START":
                            HandleTypingStartEvent(json["d"]);
                            break;
                        case "MESSAGE_CREATE":
                            HandleMessageCreateEvent(json["d"]);
                            HandleTypingStopEvent(json["d"]);
                            break;
                    }
                    break;
                default:
                    // handle other op codes when needed ig
                    break;
            }
        }

        private void HandleTypingStartEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                string userId = (string)jToken["user_id"];
                string username = GetUsernameById(userId);
                string message = $"{username} is typing...";

                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    parentNaticordForm.typingStatus.Text = message;
                }));
            }
        }

        private void HandleTypingStopEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                string message = "";

                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    parentNaticordForm.typingStatus.Text = message;
                }));
            }
        }

        private string GetUsernameById(string userId)
        {
            if (userCache.ContainsKey(userId))
            {
                return userCache[userId];
            }

            try
            {
                dynamic user = parentNaticordForm.GetApiResponse($"users/{userId}");
                string username = user.global_name ?? user.username;
                userCache[userId] = username;
                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get username for user ID {userId}: {ex.Message}");
                return "Unknown";
            }
        }

        private void HandleMessageCreateEvent(JToken jToken)
        {
            dynamic eventData = jToken;
            string channelId = eventData["channel_id"];
            string authorId = eventData["author"]["id"];
            string content = FormatPings(eventData["content"].ToString());

            string authorNickname = "";
            string authorGlobalName = "";
            string authorUsername = "";

            try
            {
                dynamic user = parentNaticordForm.GetApiResponse($"users/{authorId}");
                authorNickname = user.nickname ?? "";
                authorGlobalName = user.global_name ?? "";
                authorUsername = user.username ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve user information for user ID {authorId}: {ex.Message}");
            }

            string authorDisplayName = authorNickname != "" ? authorNickname :
                                       authorGlobalName != "" ? authorGlobalName :
                                       authorUsername != "" ? authorUsername :
                                       "Unknown";

            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    parentNaticordForm.UpdateMessageBoxWithFormatting($"{authorDisplayName}: {content}\n");
                }));
            }
        }

        private string FormatPings(string content)
        {
            return Regex.Replace(content, @"<@(\d+)>", new MatchEvaluator(MatchEvaluator));
        }

        private string MatchEvaluator(Match match)
        {
            string userId = match.Groups[1].Value;
            string username = GetUsernameById(userId);
            return $"@@@{username}@@@";
        }

        private void HandleWebSocketError(string errorMessage)
        {
            parentNaticordForm.Invoke((MethodInvoker)(() =>
            {
                Console.WriteLine($"WebSocket Error: {errorMessage}");
                InitializeWebSocket();
            }));
        }

        private void HandleWebSocketClose()
        {
            if (parentNaticordForm.IsHandleCreated)
            {
                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    Console.WriteLine("WebSocket connection closed.");
                    InitializeWebSocket();
                }));
            }
            else
            {
                Console.WriteLine("Form handle not created. WebSocket connection closed.");
                InitializeWebSocket();
            }
        }

        public void CloseWebSocket()
        {
            webSocket?.Close();
        }
    }
}
