using Naticord.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class UserViewer : Form
    {
        private static readonly HttpClient httpClient = new();

        private API dcAPI;
        private string userId;
        private readonly string token = Properties.Settings.Default.token;

        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord"
        );

        private static Dictionary<string, Image> avatarCache = new Dictionary<string, Image>();
        private static readonly string AvatarCachePath = Path.Combine(CachePath, "Avatars");

        public UserViewer(string userIdProfile)
        {
            InitializeComponent();
            dcAPI = new API();
            userId = userIdProfile;

            LoadUserInfo();
        }

        private async Task LoadUserInfo()
        {
            try
            {
                string userDetails = await dcAPI.SendAPI($"users/{userId}/profile", HttpMethod.Get, token, null, null, null);
                JObject parsedUserJson = JObject.Parse(userDetails);

                JToken user = parsedUserJson["user"];
                JToken userProfile = parsedUserJson["user_profile"];

                if (user != null)
                {
                    string userIdValue = user["id"]?.ToString() ?? "N/A";
                    string globalName = user["global_name"]?.ToString() ?? "N/A";
                    string username = user["username"]?.ToString() ?? "N/A";
                    string avatarHash = user["avatar"]?.ToString();
                    string bannerColor = user["banner_color"]?.ToString();
                    string pronouns = userProfile["pronouns"]?.ToString() ?? "N/A";
                    string userBio = userProfile["bio"]?.ToString() ?? "No bio available.";

                    Image avatar = GetCachedAvatar(userId, avatarHash, false, false);
                    userPicture.Image = avatar;

                    // General window stuff
                    this.Text = $"{globalName} - Naticord";
                    descriptionLabel.Text = $"{globalName}'s bio";

                    // User information
                    usernameLabel.Text = globalName;
                    userExtraInfo.Text = $"{username} - {pronouns}";
                    bioLabel.Text = userBio;

                    // Makes the window adapt to the bio height
                    int lineHeight = 12;
                    int lineCount = userBio.Split('\n').Length;
                    this.Height += lineCount * lineHeight;
                }

                Debug.WriteLine(userDetails);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing user data: {ex.Message}");
            }
        }

        public Image GetCachedAvatar(string userId, string avatarHash, bool isServer, bool isGC)
        {
            if (string.IsNullOrEmpty(avatarHash))
                return Properties.Resources.naticord_logo_64;

            if (TryGetMemoryCachedAvatar(userId, out Image avatar))
                return avatar;

            string avatarFile = Path.Combine(AvatarCachePath, $"{avatarHash}-{userId}.png");
            Image retrievedavatar;
            if (File.Exists(avatarFile))
            {
                retrievedavatar = Image.FromFile(avatarFile);
            }
            else
            {
                string avatarUrl = GetAvatarUrl(userId, avatarHash, isServer, isGC);
                retrievedavatar = DownloadImage(avatarUrl, avatarFile);
            }

            SetMemoryCachedAvatar(userId, retrievedavatar);
            return retrievedavatar;
        }

        private string GetAvatarUrl(string Id, string Hash, bool isServer, bool isGC)
        {
            if (isServer)
            {
                return $"https://cdn.discordapp.com/icons/{Id}/{Hash}.png?size=256";
            }
            else if (isGC)
            {
                return $"https://cdn.discordapp.com/channel-icons/{Id}/{Hash}.png?size=256";
            }
            else
            {
                return $"https://cdn.discordapp.com/avatars/{Id}/{Hash}.png?size=256";
            }
        }

        public Image DownloadImage(string url, string savePath = null)
        {
            try
            {
                byte[] imageBytes = httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult();
                savePath ??= Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
                File.WriteAllBytes(savePath, imageBytes);
                using (var ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null;
            }
        }

        private bool TryGetMemoryCachedAvatar(string userId, out Image avatar)
        {
            return avatarCache.TryGetValue(userId, out avatar);
        }

        private void SetMemoryCachedAvatar(string userId, Image avatar)
        {
            if (avatarCache.Count >= 10)
            {
                avatarCache.Remove(avatarCache.Keys.First());
            }
            if (avatarCache.ContainsKey(userId))
            {
                avatarCache[userId].Dispose();
                avatarCache[userId] = avatar;
            }
            else
            {
                avatarCache[userId] = avatar;
            }
        }
    }
}
