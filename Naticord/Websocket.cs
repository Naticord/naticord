using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text;
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
        private Dictionary<string, HashSet<string>> typingUsersByChannel = new Dictionary<string, HashSet<string>>();
        private const int MaxMessageSizeBytes = 4 * 1024 * 1024; // 4MB

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
            webSocket.OnMessage += (sender, e) => HandleWebSocketMessage(e);
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
                            browser = "firefox",
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

        private void HandleWebSocketMessage(MessageEventArgs e)
        {
            int messageSize = Encoding.UTF8.GetByteCount(e.Data);
            if (messageSize > MaxMessageSizeBytes)
            {
                Console.WriteLine($"Message exceeded max size of {MaxMessageSizeBytes} bytes and was discarded.");
                return;
            }

            Console.WriteLine($"WebSocket Received: {e.Data}");

            var json = JObject.Parse(e.Data);
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
                    // handle other op codes when needed
                    break;
            }
        }

        private void HandleTypingStartEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            string userId = (string)jToken["user_id"];

            if (!typingUsersByChannel.ContainsKey(channelId))
            {
                typingUsersByChannel[channelId] = new HashSet<string>();
            }

            typingUsersByChannel[channelId].Add(userId);
            UpdateTypingStatus(channelId);
        }

        private void HandleTypingStopEvent(JToken jToken)
        {
            string channelId = (string)jToken["channel_id"];
            string userId = (string)jToken["user_id"];

            if (typingUsersByChannel.ContainsKey(channelId))
            {
                typingUsersByChannel[channelId].Remove(userId);
                if (typingUsersByChannel[channelId].Count == 0)
                {
                    typingUsersByChannel.Remove(channelId);
                }
            }

            UpdateTypingStatus(channelId);
        }

        private void UpdateTypingStatus(string channelId)
        {
            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                string message = "";

                if (typingUsersByChannel.ContainsKey(channelId) && typingUsersByChannel[channelId].Count > 0)
                {
                    List<string> usernames = new List<string>();
                    foreach (string userId in typingUsersByChannel[channelId])
                    {
                        usernames.Add(GetUsernameById(userId));
                    }

                    if (usernames.Count <= 3)
                    {
                        message = string.Join(", ", usernames);
                        if (usernames.Count > 1)
                        {
                            int lastCommaIndex = message.LastIndexOf(',');
                            if (lastCommaIndex != -1)
                            {
                                message = message.Remove(lastCommaIndex, 1).Insert(lastCommaIndex, " and");
                            }
                        }
                        message += " are typing...";
                    }
                    else
                    {
                        message = "Multiple people are typing...";
                    }
                }

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

            string authorDisplayName = GetAuthorDisplayName(authorId);

            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    parentNaticordForm.UpdateMessageBoxWithFormatting($"{authorDisplayName}: {content}\n");
                }));
            }
        }

        private string GetAuthorDisplayName(string authorId)
        {
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

            return authorNickname != "" ? authorNickname :
                   authorGlobalName != "" ? authorGlobalName :
                   authorUsername != "" ? authorUsername :
                   "Unknown";
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
