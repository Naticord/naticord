using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Globalization;

namespace Naticord.Classes
{
    internal class ConfigMgr
    {
        private static readonly Random _rng = new Random();

        // Launch info
        public string LaunchSignature { get; private set; }
        public string ClientLaunchId { get; private set; }

        // System related options
        public string OperatingSystem { get; set; } = "Windows";
        public string BrowserName { get; set; } = "Firefox";
        public string DeviceName { get; set; } = string.Empty; // Discord leaves this empty for some reason?
        public string SystemLocale { get; set; } = CultureInfo.CurrentCulture.Name;
        public string OSVersion { get; set; } = "10";

        // Discord related options
        public bool HasClientMods { get; set; } = false; // Discord uses this in the XSP, don't know why they need this.
        public string DCReferrer { get; set; } = string.Empty;
        public string DCReferringDomain { get; set; } = string.Empty;
        public string DCReferringCurrent { get; set; } = "https://discord.com/";
        public string DCReferringCurrentDomain { get; set; } = "discord.com";
        public string DCReleaseChannel { get; set; } = "canary";
        public int DCClientBuild { get; set; } = 537475; // Latest build as of 1/5/26
        public string DCClientEvtSrc { get; set; } = null;
        public string DCClientState { get; set; } = "unfocused";

        // Browser related options
        public string BrowserUA { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";
        public string BrowserVer { get; set; } = "115.0";

        public string GetXSPJson()
        {
            string xspJson = GenerateXSP();
            byte[] xspBytes = Encoding.UTF8.GetBytes(xspJson);
            return Convert.ToBase64String(xspBytes);
        }

        private string GenerateXSP()
        {
            // Build the JSON required for XSP
            GenerateLaunchSignature();

            var dict = new Dictionary<string, object>
            {
                { "os", OperatingSystem },
                { "browser", BrowserName },
                { "device", DeviceName },
                { "system_locale", SystemLocale },
                { "has_client_mods", HasClientMods },
                { "browser_user_agent", BrowserUA },
                { "browser_version", BrowserVer },
                { "os_version", OSVersion },
                { "referrer", DCReferrer },
                { "referring_domain", DCReferringDomain },
                { "referrer_current", DCReferringCurrent },
                { "referring_domain_current", DCReferringCurrentDomain },
                { "release_channel", DCReleaseChannel },
                { "client_build_number", DCClientBuild },
                { "client_event_source", DCClientEvtSrc },
                { "client_launch_id", ClientLaunchId },
                { "launch_signature", LaunchSignature },
                { "client_app_state", DCClientState }
            };

            // Returns the finished XSP!
            return JsonSerializer.Serialize(dict);
        }

        // These functions below were rewritten from the source code of Discord Messenger, the exact file can be found here:
        // https://github.com/DiscordMessenger/dm/blob/master/src/core/config/DiscordClientConfig.cpp
        // Credit goes to them for this code, technically since it's based off of theirs.
        public static string FormatUUID(ulong partLeft, ulong partRight)
        {
            string buffer = partLeft.ToString("x16") + partRight.ToString("x16");
            return buffer.Substring(0, 8) + "-" +
                   buffer.Substring(8, 4) + "-" +
                   buffer.Substring(12, 4) + "-" +
                   buffer.Substring(16, 4) + "-" +
                   buffer.Substring(20, 12);
        }

        private static ulong RandU64()
        {
            byte[] bytes = new byte[8];
            _rng.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public void GenerateLaunchSignature()
        {
            ulong launchUuidPart1 = RandU64();
            ulong launchUuidPart2 = RandU64();

            launchUuidPart1 &= ~(
               (1UL << 11) |
               (1UL << 24) |
               (1UL << 38) |
               (1UL << 48) |
               (1UL << 55) |
               (1UL << 61)
           );

            launchUuidPart2 &= ~(
                (1UL << 11) |
                (1UL << 20) |
                (1UL << 27) |
                (1UL << 36) |
                (1UL << 44) |
                (1UL << 55)
            );

            LaunchSignature = FormatUUID(launchUuidPart1, launchUuidPart2);
            ClientLaunchId = FormatUUID(RandU64(), RandU64());
        }
    }
}