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

            Websocket websocketClient = new Websocket();
            _ = websocketClient.InitWS();

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
                string response = await API.SendAPI(token, "users/@me", HttpMethod.Get, null);
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
                string userInfoJson = await API.SendAPI(token, "users/@me", HttpMethod.Get, null);
                JObject parsedJson = JObject.Parse(userInfoJson);

                string userId = parsedJson["id"]?.ToString() ?? "N/A";
                string globalName = parsedJson["global_name"]?.ToString() ?? "N/A";
                string username = parsedJson["username"]?.ToString() ?? "N/A";
                string avatarHash = parsedJson["avatar"]?.ToString();

                usernameLabelAndImage.Image = await GetCachedAvatar(userId, avatarHash);
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

            string relationshipList = await API.SendAPI(token, "users/@me/channels", HttpMethod.Get, null);
            JArray relationships = JArray.Parse(relationshipList);
            Debug.WriteLine(relationshipList);

            friendsPanelList.Controls.Clear();
            foreach (var relationship in relationships)
            {
                string type = relationship["type"]?.ToString();
                if (type != "1") continue; // Friends (Group chats are type 3, but we can ignore that *for now*)

                var recipient = relationship["recipients"]?.FirstOrDefault();

                if (recipient == null) continue;

                string globalName = recipient["global_name"]?.ToString();
                string username = recipient["username"]?.ToString();
                string avatarHash = recipient["avatar"]?.ToString();
                string channelId = relationship["id"]?.ToString();
                string userId = recipient["id"]?.ToString();

                string displayName = !string.IsNullOrWhiteSpace(globalName) ? globalName :
                                     !string.IsNullOrWhiteSpace(username) ? username : "Unknown";

                FSControl friendControl = new FSControl
                {
                    LText = displayName,
                    PFPPic = await GetCachedAvatar(userId, avatarHash)
                };

                EventHandler clickHandler = async (sender, e) =>
                {
                    if (sender is FSControl control)
                    {
                        await FriendClicked(control, displayName, userId, channelId);
                    }
                };

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
                GC.Collect();
            }
        }

        private async Task LoadServersList()
        {
            string serversList = await API.SendAPI(token, "users/@me/guilds", HttpMethod.Get, null);
            Debug.WriteLine(serversList);
        }

        private async Task LoadMessages(string userId, string channelId)
        {
            messagesPanel.Controls.Clear();
            string messageStack = await API.SendAPI(token, $"channels/{channelId}/messages?limit=20", HttpMethod.Get, null);
            JArray messages = JArray.Parse(messageStack);
            messages = new JArray(messages.Reverse());
            Debug.WriteLine(messages);

            foreach (var message in messages)
            {
                string attachmentUrl = null;

                var attachments = message["attachments"] as JArray;
                if (attachments != null && attachments.Count > 0)
                {
                    attachmentUrl = attachments[0]["url"]?.ToString();
                }

                string authorDisplay = message["author"]?["global_name"]?.ToString();
                string authorUser = message["author"]?["username"]?.ToString();
                string authorPFP = message["author"]?["avatar"]?.ToString();
                string authorID = message["author"]?["id"]?.ToString();
                string content = message["content"]?.ToString();

                string displayName = !string.IsNullOrWhiteSpace(authorDisplay) ? authorDisplay :
                                     !string.IsNullOrWhiteSpace(authorUser) ? authorUser : "Unknown";

                await AddMessage(displayName, content, authorID, authorPFP, attachmentUrl);
            }
        }

        // Helper functions
        private static readonly HttpClient httpClient = new();

        private async Task<Image> DownloadImage(string url, string? savePath = null)
        {
            try
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

                if (!string.IsNullOrEmpty(savePath))
                {
                    File.WriteAllBytes(savePath, imageBytes);
                }

                using var ms = new MemoryStream(imageBytes);
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download or load image: {ex.Message}");
                return Properties.Resources.discord_profile;
            }
        }

        private void RenderPlaceholderMessageBox()
        {
            Label placeholderLabel = new Label
            {
                Text = "Open a DM / server to get started!",
                AutoSize = false,
                Font = new Font("Segoe UI", 18, FontStyle.Regular | FontStyle.Italic),
                ForeColor = Color.DarkGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = messagesPanel.ClientSize.Width,
                Height = messagesPanel.ClientSize.Height
            };

            messagesPanel.Controls.Add(placeholderLabel);
        }

        private async Task<Image> GetCachedAvatar(string userId, string avatarHash)
        {
            if (string.IsNullOrEmpty(avatarHash))
                return Properties.Resources.discord_profile;

            string avatarFile = Path.Combine(AvatarCachePath, $"{avatarHash}-{userId}.png");

            if (File.Exists(avatarFile))
            {
                return Image.FromFile(avatarFile);
            }
            else
            {
                string avatarUrl = GetAvatarUrl(userId, avatarHash);
                return await DownloadImage(avatarUrl, avatarFile);
            }
        }

        private string GetAvatarUrl(string userId, string avatarHash)
        {
            bool isGif = avatarHash.StartsWith("a_");
            string extension = isGif ? "gif" : "png";
            return $"https://cdn.discordapp.com/avatars/{userId}/{avatarHash}.{extension}?size=128";
        }

        private async Task FriendClicked(FSControl pickedUser, string username, string userId, string channelId)
        {
            foreach (FSControl friend in friendsPanelList.Controls)
            {
                friend.ClickedDesignChange(false);
            }

            pickedUser.ClickedDesignChange(true);
            await LoadMessages(userId, channelId);
        }

        public async Task AddMessage(string author, string content, string userId, string avatarHash, string? attachmentImage)
        {
            Message messageControl = new Message
            {
                authorText = author,
                messageContentText = content,
                PFPPicAuthor = await GetCachedAvatar(userId, avatarHash)
            };

            if (!string.IsNullOrEmpty(attachmentImage))
            {
                messageControl.attachmentImageDisplay = await DownloadImage(attachmentImage, null);
            }

            messagesPanel.Controls.Add(messageControl);
            ScrollToBottom();
            GC.Collect();
        }

        private bool IsImageUrl(string url)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif"};
            string extension = Path.GetExtension(url).ToLower();
            return validExtensions.Contains(extension);
        }

        private void ScrollToBottom()
        {
            messagesPanel.AutoScroll = true;
            messagesPanel.AutoScrollPosition = new Point(0, messagesPanel.VerticalScroll.Maximum);
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
            await CheckIfTokenIsValid();

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

            await SetUserInfo();
            RenderPlaceholderMessageBox();
            await LoadFriendsList();
            await LoadServersList();
        }

        // Button handlers
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.Title = "Select a file to upload...";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Debug.WriteLine($"Selected file: {filePath}");
            }
        }
    }
}