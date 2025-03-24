// It's been a while since I made Discord clients, but damn did they make it sort of hard to make them.
// Now you need some "X-Super-Properties" bullshit to get the client working sometimes (for example, 2FA) and it's just a pain.
// Fuck you Discord.

#nullable enable
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naticord
{
    internal class API
    {
        private static readonly HttpClient client = new HttpClient();  // This is reused to reduce an overhead when creating multiple requests!

        // Request headers to simulate a Discord Web client on Firefox 135 (Windows 10)
        private static readonly string XSuperProperties = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJlbi1VUyIsImhhc19jbGllbnRfbW9rcyI6ZmFsc2UsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjEzNS4wKSBHZWNrby8yMDEwMDEwMSBGaXJlZm94LzEzNS4wIiwiYnJvd3Nlcl92ZXJzaW9uIjoiMTM1LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6MzY4MzI3LCJjbGllZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0==";
        private static readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0";

        static API()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }


        public static async Task<string> SendAPI(
            string? token,
            string endpoint,
            HttpMethod method,
            object? data = null,
            byte[]? fileData = null,
            string? fileName = null
        )
        {
            if (method == HttpMethod.Get && data != null)
            {
                throw new InvalidOperationException("GET requests should not have a body.");
            }

            string url = $"https://discord.com/api/v9/{endpoint}";
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);
            }

            if (fileData != null && !string.IsNullOrEmpty(fileName))
            {
                var content = new MultipartFormDataContent();

                if (data != null)
                {
                    string jsonData = JsonConvert.SerializeObject(data);
                    content.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "payload_json");
                }

                var fileContent = new ByteArrayContent(fileData);
                fileContent.Headers.Add("Content-Type", "application/octet-stream");
                content.Add(fileContent, "file", fileName);

                request.Content = content;
            }
            else if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                string jsonData = data != null ? JsonConvert.SerializeObject(data) : "{}";
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            request.Headers.Add("User-Agent", UserAgent);
            request.Headers.Add("X-Super-Properties", XSuperProperties);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Request failed: {response.StatusCode} - {errorResponse}");
            }
        }
    }
}