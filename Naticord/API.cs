#nullable enable
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    internal class API
    {
        private static readonly HttpClient client = new HttpClient();
        private static string? fingerprint;

        static API()
        {
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0");
        }

        public static async Task InitializeFingerprint()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/v9/auth/fingerprint");
            request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject json = JsonConvert.DeserializeObject<JObject>(responseBody) ?? throw new Exception("Failed to parse fingerprint JSON.");
            fingerprint = json["fingerprint"]?.ToString() ?? throw new Exception("Failed to retrieve fingerprint.");

            Debug.WriteLine($"Fingerprint: {fingerprint}");
        }

        public static async Task<string> SendAPI(string? token, string endpoint, HttpMethod method, object? data = null)
        {
            if (fingerprint == null)
            {
                throw new Exception("Fingerprint not initialized. Call InitializeFingerprint() first.");
            }

            string url = $"https://discord.com/api/v9/{endpoint}";
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);
            }

            string jsonData = data != null ? JsonConvert.SerializeObject(data) : "{}";
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            request.Headers.Add("X-Fingerprint", fingerprint);
            request.Headers.Add("X-Super-Properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJlbi1VUyIsImhhc19jbGllbnRfbW9rcyI6ZmFsc2UsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjEzNS4wKSBHZWNrby8yMDEwMDEwMSBGaXJlZm94LzEzNS4wIiwiYnJvd3Nlcl92ZXJzaW9uIjoiMTM1LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6MzY4MzI3LCJjbGllZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");

            Debug.WriteLine($"Content Length: {request.Content.Headers.ContentLength}");

            foreach (var header in request.Headers)
            {
                Debug.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            foreach (var contentHeader in request.Content.Headers)
            {
                Debug.WriteLine($"{contentHeader.Key}: {string.Join(", ", contentHeader.Value)}");
            }

            HttpResponseMessage response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
