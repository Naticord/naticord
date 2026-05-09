using Naticord.Classes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Naticord.Networking
{
    internal class API
    {
        private readonly ConfigMgr configMgr = new ConfigMgr();

        // Singleton, so we don't create multiple HttpClient clients
        private static readonly Lazy<API> _instance = new Lazy<API>(() => new API());
        public static API Instance => _instance.Value;

        // Reuse the HttpClient throughout the API
        internal readonly HttpClient InternalHttpClient;

        // Current Discord API version (v9, has been for a while!)
        private const int API_VERSION = 9;

        // Configuration (Firefox 115 ESR on Windows 10)
        public string XSuperProperties = null;
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";

        private API()
        {
            var compressionHandler = new HttpClientHandler
            {
                // Possibly add Brotli and zstd compression in the future?
                AutomaticDecompression =
                    DecompressionMethods.GZip |
                    DecompressionMethods.Deflate
            };

            ServicePointManager.DefaultConnectionLimit = 10;
            InternalHttpClient = new HttpClient(compressionHandler);

            // Set default headers through out the system
            InternalHttpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            InternalHttpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            // Required for endpoints like /users/@me/remote-auth/login - Discord rejects
            // the request (returning an error JSON without encrypted_token) when Origin is absent
            InternalHttpClient.DefaultRequestHeaders.Add("Origin", "https://discord.com");

            XSuperProperties = configMgr.GetXSPJson();
            InternalHttpClient.DefaultRequestHeaders.Add("X-Super-Properties", XSuperProperties);
        }

        public async Task<string> SendAPI(string endpoint, HttpMethod httpMethod, string token = null, object data = null, byte[] fileData = null, string fileName = null, Dictionary<string, string> headers = null)
        {
            string url = "https://discord.com/api/v" + API_VERSION + "/" + endpoint.TrimStart('/');
            using (var request = new HttpRequestMessage(httpMethod, url))
            {

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", token);
                    }
                    catch (Exception ex)
                    {
                        return $"[API/ParseError] An error occurred while sending the request: {ex.Message}\n\n$\"[API] URL used when the error occurred: {{url}}";
                    }
                }

                if (headers != null)
                {
                    foreach (var kvp in headers)
                    {
                        request.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);
                    }
                }

                if (fileData != null && !string.IsNullOrEmpty(fileName))
                {
                    var content = new MultipartFormDataContent
                    {
                        { new ByteArrayContent(fileData) { Headers = { { "Content-Type", "application/octet-stream" } } }, "file", fileName }
                    };

                    if (data != null)
                    {
                        string jsonData = JsonSerializer.Serialize(data);
                        content.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "payload_json");
                    }

                    request.Content = content;
                }
                else if ((httpMethod != HttpMethod.Get) && data != null)
                {
                    string jsonData = JsonSerializer.Serialize(data);
                    request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                try
                {
                    using (HttpResponseMessage response = await InternalHttpClient.SendAsync(request))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    return $"[API/RequestError]{ex.Message}\nURL: {url}";
                }
            }
        }
    }
}