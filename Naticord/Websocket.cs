// This is a very early implementation of the Websockets.
// This was made with the help of the documentation from discord.sex
// Without them, I never would've gotten the right implementation of it.

using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Naticord
{
    class Websocket
    {
        private string token;
        private string gatewayUrl;
        private const SslProtocols Tls12 = SslProtocols.Tls12;
        public WebSocket WebSocket { get; private set; }

        public Websocket()
        {
            token = Properties.Settings.Default.token;
            gatewayUrl = "wss://gateway.discord.gg/?v=9&encoding=json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _ = InitWS(); // To run it without async
        }

        private async Task InitWS()
        {
            WebSocket = new WebSocket(gatewayUrl);
            WebSocket.SslConfiguration.EnabledSslProtocols = Tls12;

            // Configure our Websockets
            WebSocket.OnOpen += (sender, e) => Console.WriteLine("Connected to the gateway.");
            WebSocket.OnMessage += (sender, e) => Console.WriteLine($"Received from the gateway: {e.Data}");
            WebSocket.OnClose += (sender, e) => Console.WriteLine("Disconnected from the gateway.");
            WebSocket.OnError += (sender, e) => Console.WriteLine($"Error! {e.Message}");

            WebSocket.Connect();
            await SendPayload();
        }

        private async Task SendPayload()
        {
            // TODO
        }
    }
}