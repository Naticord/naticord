using Naticord.Classes;
using Naticord.Networking;
using Naticord.Controls;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;

namespace Naticord.Forms
{
    public partial class Client : GlassForm
    {
        private static readonly HttpClient httpClient = new();

        private readonly string iconStyle = Properties.Settings.Default.iconStyle;
        private readonly string borderStyle = Properties.Settings.Default.borderStyle;
        private readonly string token = Properties.Settings.Default.token;
        private readonly bool isCompDisabled = !Application.RenderWithVisualStyles;

        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord"
        );
        private static Dictionary<string, Image> avatarCache = new Dictionary<string, Image>();
        private static readonly string AvatarCachePath = Path.Combine(CachePath, "Avatars");

        private API dcAPI;

        public Client()
        {
            Directory.CreateDirectory(CachePath);
            Directory.CreateDirectory(AvatarCachePath);

            dcAPI = new API();
            InitializeComponent();

            DecideSettings();
            SetUpToolbarButtons();

            chatPanel.Paint += XPPanelBDPaint;
            DrawPBBorder(profilePictureUser);

            this.FormClosing += (s, e) => Application.Exit();
            this.Shown += (s, e) => ApplySavedSettings();

            CenterToScreen();

            // Actual client
            SetUserInfo();
        }

        // Discord API related functionality
        private void SetUserInfo()
        {
            try
            {
                string userDetails = dcAPI.APISend("users/@me", HttpMethod.Get, null, token, null, null);
                JObject parsedJson = JObject.Parse(userDetails);

                string userId = parsedJson["id"]?.ToString() ?? "N/A";
                string globalName = parsedJson["global_name"]?.ToString() ?? "N/A";
                string username = parsedJson["username"]?.ToString() ?? "N/A";
                string avatarHash = parsedJson["avatar"]?.ToString();

                profilePictureUser.Image = GetCachedAvatar(userId, avatarHash, false, false);
                usernameLabel.Text = $"{globalName} ({username})";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Parse error: {ex.Message}");
            }
        }

        // Helper functions
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

        private bool TryGetMemoryCachedAvatar(string userId, out Image avatar)
        {
            return avatarCache.TryGetValue(userId, out avatar);
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
                return $"https://cdn.discordapp.com/icons/{Id}/{Hash}.png?size=64";
            }
            else if (isGC)
            {
                return $"https://cdn.discordapp.com/channel-icons/{Id}/{Hash}.png?size=64";
            }
            else
            {
                return $"https://cdn.discordapp.com/avatars/{Id}/{Hash}.png?size=64";
            }
        }

        // UI related functionality
        private void ApplySavedSettings()
        {
            if (iconStyle == "Modern")
            {
                settingsButton.ButtonIcon = Properties.Resources.settings_modern;
                accountButton.ButtonIcon = Properties.Resources.account_modern;
                ghButton.ButtonIcon = Properties.Resources.github_modern;
            }

            if (borderStyle == "Thick")
                ChangePos();
        }

        public void ChangePos()
        {
            usernameLabel.Location = new System.Drawing.Point(785, 5);
            profilePictureUser.Location = new System.Drawing.Point(989, 4);
            buttonPanel.Location = new System.Drawing.Point(0, 2);
        }

        private void DecideSettings()
        {
            if (Properties.Settings.Default.runDefaults)
                return;
            if (isCompDisabled == true)
                Properties.Settings.Default.renderMode = "Composition disabled";

            switch (OSVersionHelper.GetWindowsVersion())
            {
                case "Windows 11":
                    if (isCompDisabled == false)
                        Properties.Settings.Default.renderMode = "Mica";
                    Properties.Settings.Default.iconStyle = "Modern";
                    Properties.Settings.Default.borderStyle = "Slim";
                    break;

                case "Windows 10":
                    if (isCompDisabled == false)
                        Properties.Settings.Default.renderMode = "Acrylic";
                    Properties.Settings.Default.iconStyle = "Modern";
                    Properties.Settings.Default.borderStyle = "Slim";
                    break;

                case "Windows 7 - 8.1":
                    if (isCompDisabled == false)
                        Properties.Settings.Default.renderMode = "Aero";
                    Properties.Settings.Default.iconStyle = "Legacy";
                    Properties.Settings.Default.borderStyle = "Thick";
                    break;
            }

            Properties.Settings.Default.runDefaults = true;
            Properties.Settings.Default.Save();
            Application.Restart(); // This applies all the settings
        }

        private void SetUpToolbarButtons()
        {
            settingsButton.ButtonClick += (s, e) =>
            {
                new Settings(this).Show();
            };

            accountButton.ButtonClick += (s, e) =>
            {
                accountMenu.Show(accountButton, new System.Drawing.Point(0, accountButton.Height));
            };

            ghButton.ButtonClick += (s, e) =>
            {
                System.Diagnostics.Process.Start("https://github.com/Naticord/naticord");
            };
        }

        void DrawPBBorder(PictureBox pictureBox)
        {
            pictureBox.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(255, 35, 35, 35), 1))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawRectangle(pen, 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                }
            };
            pictureBox.Invalidate();
        }

        private void XPPanelBDPaint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.FromArgb(127, 157, 185);
            Control panel = (Control)sender;
            using (Pen pen = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }
    }
}