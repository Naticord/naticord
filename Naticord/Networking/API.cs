using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Naticord.Networking
{
    internal class API
    {
        // Re-used client (Less memory usage)
        private static readonly HttpClient client = new HttpClient();

        // Configuration (Firefox 137 on Windows 10)
        private static readonly string XSuperProperties = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJlbi1VUyIsImhhc19jbGllbnRfbW9kcyI6ZmFsc2UsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjEzNy4wKSBHZWNrby8yMDEwMDEwMSBGaXJlZm94LzEzNy4wIiwiYnJvd3Nlcl92ZXJzaW9uIjoiMTM3LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6Mzg2NDMyLCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==";
        private static readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:137.0) Gecko/20100101 Firefox/137.0";

        static API()
        {
            // Forcefully use TLS 1.2 (Adds back Windows 7 support)
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }

        public string APISend(string endpoint, HttpMethod httpMethod, object data = null, string token = null, byte[] fileData = null, string fileName = null)
        {
            string url = $"https://discord.com/api/v9/{endpoint}";
            var request = new HttpRequestMessage(httpMethod, url);

            if (httpMethod == HttpMethod.Get && data != null)
            {
                Debug.WriteLine("[DEBUG] GET requests shouldn't have a body.");
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);
            }

            if (fileData != null && !string.IsNullOrEmpty(fileName))
            {
                var content = new MultipartFormDataContent
                {
                    { new ByteArrayContent(fileData) { Headers = { { "Content-Type", "application/octet-stream" } } }, "file", fileName }
                };

                if (data != null)
                {
                    string jsonData = JsonConvert.SerializeObject(data);
                    content.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "payload_json");
                }

                request.Content = content;
            }
            else if ((httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put) && data != null)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            request.Headers.Add("User-Agent", UserAgent);
            request.Headers.Add("X-Super-Properties", XSuperProperties);

            try
            {
                HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Debug.WriteLine($"[DEBUG] Request failed: {response.StatusCode} - {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG] An error occurred while sending the request: {ex.Message}");
                Debug.WriteLine($"[DEBUG] URL used: {url}");
            }

            return string.Empty;
        }
    }
}