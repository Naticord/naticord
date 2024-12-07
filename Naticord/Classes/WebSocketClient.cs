using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace Naticord
{
    public class WebSocketClient
    {
        private Naticord parentClientForm;
        private WebSocket webSocket;
        private string accessToken;
        private const SslProtocols Tls12 = (SslProtocols)0x00000C00;
        private bool tryingRandomStuffAtThisPoint = false;

        public WebSocketClient(string accessToken, Naticord parentClientForm)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            this.accessToken = accessToken;
            this.parentClientForm = parentClientForm;
            PrintToken();
            InitializeWebSocket();
        }

        private void InitializeWebSocket()
        {
            webSocket = new WebSocket($"wss://gateway.discord.gg/?v=9&encoding=json");
            webSocket.SslConfiguration.EnabledSslProtocols = Tls12;
            webSocket.OnMessage += async (sender, e) => await HandleWebSocketMessage(e.Data);
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
                    Console.WriteLine($"Sending Identify Payload: {payloadJson}");
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
            Debug.WriteLine($"Received WebSocket Message: {data}");

            try
            {
                var json = JObject.Parse(data);
                int opCode = (int)json["op"];

                Debug.WriteLine($"OpCode: {opCode}");

                switch (opCode)
                {
                    case 0:
                        string eventType = (string)json["t"];
                        Debug.WriteLine($"Event Type: {eventType}");

                        switch (eventType)
                        {
                            case "READY":
                                ParseReadyEvent(data, parentClientForm);
                                break;
                            case "USER_SETTINGS_UPDATE":
                                Debug.WriteLine("Status updated!");
                                ParseCustomStatusText(data, parentClientForm);
                                break;

                            default:
                                Debug.WriteLine($"Unhandled event type: {eventType}");
                                break;
                        }
                        break;

                    case 1:
                        Debug.WriteLine("Heartbeat event received");
                        break;

                    default:
                        Debug.WriteLine($"Unhandled OpCode: {opCode}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing message: {ex.Message}");
            }
        }

        public static void ParseReadyEvent(string data, Naticord parentClientForm)
        {
            try
            {
                // Null check for parentClientForm
                if (parentClientForm == null)
                {
                    Debug.WriteLine("parentClientForm is null. Cannot update status.");
                    return;
                }

                var json = JObject.Parse(data);

                string eventType = (string)json["t"];
                if (eventType == "READY")
                {
                    Debug.WriteLine("Received READY event data: " + data);

                    var user = json["d"]["user"];
                    if (user == null)
                    {
                        Debug.WriteLine("User data is null.");
                        return;
                    }

                    string userId = (string)user["id"];
                    string username = (string)user["username"];
                    string discriminator = (string)user["discriminator"];
                    Debug.WriteLine($"Logged in as {username}#{discriminator}");

                    var presence = json["d"]["user"]["presence"];
                    if (presence == null)
                    {
                        Debug.WriteLine("Presence data is null.");
                        return;
                    }

                    Debug.WriteLine("Presence data: " + presence);

                    string normalStatus = (string)presence["status"];
                    Debug.WriteLine($"Normal Status: {normalStatus}");

                    var customStatus = presence?["custom_status"];
                    string userStatusText = normalStatus;

                    if (customStatus != null)
                    {
                        userStatusText = (string)customStatus["text"];
                        Debug.WriteLine($"Custom Status Text: {userStatusText}");
                    }

                    if (parentClientForm.InvokeRequired)
                    {
                        parentClientForm.Invoke((Action)(() =>
                        {
                            parentClientForm.descriptionLabel.Text = userStatusText;
                        }));
                    }
                    else
                    {
                        parentClientForm.descriptionLabel.Text = userStatusText;
                    }
                }
                else
                {
                    Debug.WriteLine("Unhandled event type.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing READY event: {ex.Message}");
            }
        }

        public static void ParseCustomStatusText(string data, Naticord parentClientForm)
        {
            try
            {
                if (parentClientForm == null)
                {
                    Debug.WriteLine("parentClientForm is null. Cannot update status.");
                    return;
                }

                var json = JObject.Parse(data);
                string eventType = (string)json["t"];

                if (eventType == "USER_SETTINGS_UPDATE")
                {
                    var customStatus = json["d"]?["custom_status"];
                    if (customStatus != null)
                    {
                        string statusText = (string)customStatus["text"];
                        Debug.WriteLine($"Custom Status Text: {statusText}");

                        if (parentClientForm.InvokeRequired)
                        {
                            parentClientForm.Invoke((Action)(() =>
                            {
                                parentClientForm.descriptionLabel.Text = statusText;
                                Debug.WriteLine("Changed status on UI level!");
                            }));
                        }
                        else
                        {
                            parentClientForm.descriptionLabel.Text = statusText;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Custom status not found.");
                    }
                }
                else
                {
                    Debug.WriteLine("Unhandled event type.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing message: {ex.Message}");
            }
        }

        public void PrintToken()
        {
            Console.WriteLine($"Using Token: {accessToken}");
        }

        private void HandleWebSocketClose()
        {
            Console.WriteLine("WebSocket Closed");
            if (!tryingRandomStuffAtThisPoint)
            {
                try
                {
                    Console.WriteLine("Attempting to reconnect...");
                    InitializeWebSocket();
                    Debug.WriteLine("Started again!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during reconnect: {ex.Message}");
                }
            }
        }

        public void CloseWebSocket()
        {
            tryingRandomStuffAtThisPoint = true;
            Console.WriteLine("Closing WebSocket");
            webSocket.Close();
            GC.Collect();
        }
    }
}
