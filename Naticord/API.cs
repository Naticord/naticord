// Let me just say, Discord has made it harder for me to make this client.
// This API is much better than the old one, but then again Discord has made it somewhat hard to keep up.
// Thank you Discord, very cool.

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naticord
{
    internal class API
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> SendAPI(string? token, string endpoint, HttpMethod method, object? data = null)
        {
            string url = $"https://discord.com/api/v9/{endpoint}";
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", token);
            }

            // Simulate a real Discord Web client (Firefox 135 on Windows 10 User-Agent)
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0");

            if (data != null)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
