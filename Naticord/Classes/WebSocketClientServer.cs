﻿using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using System.Net;
using System.Threading.Tasks;

namespace Naticord
{
    public class WebSocketClientServer
    {
        private Server parentServerForm;
        private WebSocket webSocket;
        private string accessToken;
        private const SslProtocols Tls12 = (SslProtocols)0x00000C00;

        public WebSocketClientServer(string accessToken, Server parentServerForm)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            this.accessToken = accessToken;
            this.parentServerForm = parentServerForm;
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
            var json = JObject.Parse(data);
            int opCode = (int)json["op"];

            switch (opCode)
            {
                case 0:
                    string eventType = (string)json["t"];
                    switch (eventType)
                    {
                        case "MESSAGE_CREATE":
                            HandleMessageCreateEvent(json["d"]);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private async void HandleMessageCreateEvent(JToken data)
        {
            dynamic eventData = data;
            string channelId = eventData["channel_id"];

            if (channelId != parentServerForm.ChatID.ToString())
            {
                return;
            }

            string author = eventData["author"]["global_name"] ?? eventData["author"]["username"];
            string content = eventData["content"];
            dynamic attachmentData = eventData["attachments"];
            dynamic embedData = eventData["embeds"];

            List<Attachment> attachmentsFormed = new List<Attachment>();
            List<Embed> embedsFormed = new List<Embed>();

            if (attachmentData != null)
            {
                foreach (var attachment in attachmentData)
                {
                    attachmentsFormed.Add(new Attachment { URL = attachment.url, Type = attachment.content_type });
                }
            }

            if (embedData != null)
            {
                foreach (var embed in embedData)
                {
                    embedsFormed.Add(new Embed
                    {
                        Type = embed?.type ?? "",
                        Author = embed?.author?.name ?? "",
                        AuthorURL = embed?.author?.url ?? "",
                        Title = embed?.title ?? "",
                        TitleURL = embed?.url ?? "",
                        Description = embed?.description ?? ""
                    });
                }
            }

            if (parentServerForm.InvokeRequired)
            {
                parentServerForm.Invoke((MethodInvoker)(async () =>
                {
                    await ProcessMessageAndUpdateChat(author, content, attachmentsFormed.ToArray(), embedsFormed.ToArray(), eventData);
                }));
            }
            else
            {
                await ProcessMessageAndUpdateChat(author, content, attachmentsFormed.ToArray(), embedsFormed.ToArray(), eventData);
            }
        }

        private async Task ProcessMessageAndUpdateChat(string author, string content, Attachment[] attachments, Embed[] embeds, dynamic eventData)
        {
            switch ((int)eventData["type"].Value)
            {
                case 7:
                    await parentServerForm.AddMessage(author, "*Say hi!*", "slid in the server", attachments, embeds);
                    break;
                case 19:
                    bool found = false;
                    try
                    {
                        var messages = await parentServerForm.GetApiResponse($"channels/{parentServerForm.ChatID}/messages");

                        foreach (var message in messages)
                        {
                            if (message.id == eventData["message_reference"]["message_id"])
                            {
                                string replyAuthor = message.author.global_name ?? message.author.username;
                                await parentServerForm.AddMessage(author, content, "replied", attachments, embeds, found, true);
                                found = true;
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to retrieve messages: {ex.Message}");
                    }

                    if (!found)
                    {
                        await parentServerForm.AddMessage(author, content, "replied", attachments, embeds, false, false);
                    }
                    break;
                default:
                    await parentServerForm.AddMessage(author, content, "said", attachments, embeds);
                    break;
            }

            parentServerForm.UpdateChatBox(parentServerForm.htmlStart + parentServerForm.htmlMiddle + parentServerForm.htmlEnd);
        }

        private void HandleWebSocketError(string errorMessage)
        {
            parentServerForm.Invoke((MethodInvoker)(() =>
            {
                InitializeWebSocket();
            }));
        }

        private void HandleWebSocketClose()
        {
            if (parentServerForm != null && !parentServerForm.IsDisposed && parentServerForm.IsHandleCreated)
            {
                parentServerForm.Invoke((MethodInvoker)(() =>
                {
                    InitializeWebSocket();
                }));
            }
            else
            {
                CloseWebSocket();
            }
        }


        public void CloseWebSocket()
        {
            webSocket.Close();
            GC.Collect();
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
