using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Naticord
{
    public partial class Client : Form
    {
        private readonly string token;

        // AppData paths for Naticord (mostly used for caching)
        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord"
        );
        private static readonly string AvatarCachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord", "Avatars"
        );

        public Client()
        {
            // Get the client ready for later
            token = Properties.Settings.Default.token;
            Directory.CreateDirectory(CachePath);
            Directory.CreateDirectory(AvatarCachePath);

            InitializeComponent();

            // A bunch of events (mostly Paint events)
            infoBar.Paint += InfoBar_Paint;
            usernameLabelAndImage.Paint += Antialias_Paint;
            naticordVersion.Paint += Antialias_Paint;

            this.FormClosing += (sender, e) => Application.Exit();
        }

        // Discord API events
        private async Task CheckIfTokenIsValid()
        {
            try
            {
                string response = await API.SendAPI(token, "users/@me", HttpMethod.Get, null).ConfigureAwait(false);
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response);

                if (jsonResponse?.message != null && jsonResponse.message.ToString().Contains("401: Unauthorized"))
                {
                    new CMessageBox("Your token is invalid.", "Don't worry, this shouldn't mean anything harmful. Naticord will sign you out for you to re-sign in and get a new token. You will have to reopen Naticord.").Show();
                    Properties.Settings.Default.token = null;
                    Properties.Settings.Default.Save();
                    Application.Restart();
                }
                else
                {
                    Debug.WriteLine("Token is valid.");
                }
            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"Invalid response or error occurred: {ex.Message}");
            }
        }

        private async Task SetUserInfo()
        {
            try
            {
                string userInfoJson = await API.SendAPI(token, "users/@me", HttpMethod.Get, null).ConfigureAwait(false);
                JObject parsedJson = JObject.Parse(userInfoJson);

                string userId = parsedJson["id"]?.ToString() ?? "N/A";
                string globalName = parsedJson["global_name"]?.ToString() ?? "N/A";
                string username = parsedJson["username"]?.ToString() ?? "N/A";
                string avatarHash = parsedJson["avatar"]?.ToString();

                usernameLabelAndImage.Image = await GetCachedAvatar(userId, avatarHash).ConfigureAwait(false);
                usernameLabelAndImage.Text = $"{globalName} ({username})";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Parse error: {ex.Message}");
            }
        }

        private async Task LoadFriendsList()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(async () => await LoadFriendsList()));
                return;
            }

            string relationshipList = await API.SendAPI(token, "users/@me/relationships", HttpMethod.Get, null);
            JArray relationships = JArray.Parse(relationshipList);
            Debug.WriteLine(relationshipList);

            foreach (var relationship in relationships)
            {
                string type = relationship["type"]?.ToString();
                if (type == "2") continue; // Blocked users are skipped

                string globalName = relationship["user"]?["global_name"]?.ToString();
                string username = relationship["user"]?["username"]?.ToString();
                string avatarHash = relationship["user"]?["avatar"]?.ToString();
                string userId = relationship["user"]?["id"]?.ToString();
                string friendNick = relationship["nickname"].ToString();

                FSControl friendControl = new FSControl
                {
                    LText = !string.IsNullOrWhiteSpace(friendNick) ? friendNick :
                            !string.IsNullOrWhiteSpace(globalName) ? globalName :
                            username,
                    PFPPic = await GetCachedAvatar(userId, avatarHash)
                };

                EventHandler clickHandler = (sender, e) => FriendClicked(friendControl);
                friendControl.Click += clickHandler;

                PictureBox profilePic = friendControl.Controls.Find("profilePictureItem", true).FirstOrDefault() as PictureBox;
                if (profilePic != null)
                {
                    profilePic.Paint += Antialias_Paint;
                    profilePic.Click += clickHandler;
                }

                Label nameLabel = friendControl.Controls.Find("nameLabel", true).FirstOrDefault() as Label;
                if (nameLabel != null)
                {
                    nameLabel.Click += clickHandler;
                }

                friendsPanelList.Controls.Add(friendControl);
            }
        }

        private void FriendClicked(FSControl selectedFriend)
        {
            foreach (FSControl friend in friendsPanelList.Controls)
            {
                friend.ClickedDesignChange(false);
            }

            selectedFriend.ClickedDesignChange(true);

            // TODO: Actual message loading
        }

        private async Task LoadServersList()
        {
            string serversList = await API.SendAPI(token, "users/@me/guilds", HttpMethod.Get, null);
            Debug.WriteLine(serversList);
        }

        // Helper functions
        private async Task<Image> GetCachedAvatar(string userId, string avatarHash)
        {
            if (string.IsNullOrEmpty(avatarHash))
                return Properties.Resources.discord_profile;
            string avatarFile = Path.Combine(AvatarCachePath, $"{avatarHash}-{userId}.png");

            if (!File.Exists(avatarFile))
            {
                string avatarUrl = GetAvatarUrl(userId, avatarHash);
                using (HttpClient client = new HttpClient())
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(avatarUrl).ConfigureAwait(false);
                    File.WriteAllBytes(avatarFile, imageBytes);
                }
            }

            return Image.FromFile(avatarFile);
        }

        private string GetAvatarUrl(string userId, string avatarHash)
        {
            bool isGif = avatarHash.StartsWith("a_");
            string extension = isGif ? "gif" : "png";
            return $"https://cdn.discordapp.com/avatars/{userId}/{avatarHash}.{extension}?size=128";
        }

        // Anti-aliasing
        private void Antialias_Paint(object sender, PaintEventArgs e)
        {
            if (sender is ToolStripLabel label && label.Image != null)
            {
                DrawImage(e.Graphics, label.Image, label.Height);
            }
            else if (sender is PictureBox picBox && picBox.Image != null)
            {
                DrawImage(e.Graphics, picBox.Image, picBox.Height);
            }
        }

        private void DrawImage(Graphics g, Image image, int size)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            Rectangle imgRect = new Rectangle(0, 0, size, size);
            g.DrawImage(image, imgRect);
        }

        // Paint events
        private void InfoBar_Paint(object sender, PaintEventArgs e)
        {
            if (sender is ToolStrip toolStrip)
            {
                using (Pen borderPen = new Pen(ColorTranslator.FromHtml("#646464"), 1))
                {
                    e.Graphics.DrawLine(borderPen, 0, 0, toolStrip.Width, 0);
                }
            }
        }

        // Loads everything needed for the client
        private async void Client_Load(object sender, EventArgs e)
        {
            await CheckIfTokenIsValid().ConfigureAwait(false);

            // I hate the Windows Forms designer.
            infoBar.BeginInvoke(new Action(() =>
            {
                infoBar.Items.AddRange(new ToolStripItem[]
                {
                    usernameLabelAndImage,
                    new ToolStripSeparator(),
                    naticordVersion
                });
            }));

            await SetUserInfo().ConfigureAwait(false);
            await LoadFriendsList();
            await LoadServersList();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            // TODO
        }
    }
}