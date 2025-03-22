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
using static Naticord.Websocket;

namespace Naticord
{
    public partial class Client : Form
    {
        private static readonly HttpClient httpClient = new();
        private string selectedFilePath = null;
        private string currentChannelId;
        private readonly string token;
        private NotifyIcon trayIcon;

        // AppData paths for Naticord (mostly used for caching)
        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord"
        );
        private static readonly string AvatarCachePath = Path.Combine(CachePath, "Avatars");

        public Client()
        {
            token = Properties.Settings.Default.token;
            Directory.CreateDirectory(CachePath);
            Directory.CreateDirectory(AvatarCachePath);

            InitializeComponent();
            AddKeyUpHandler(messageTextBox);

            // Event handlers
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

                usernameLabelAndImage.Image = await GetCachedAvatar(userId, avatarHash, false);
                usernameLabelAndImage.Text = $"{globalName} ({username})";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Parse error: {ex.Message}");
            }
        }

        private async Task LoadFriendsList()
        {
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

                string status = UserStatusStore.GetStatus(userId);
                FSControl friendControl = new FSControl
                {
                    LText = displayName,
                    SText = status,
                    PFPPic = await GetCachedAvatar(userId, avatarHash, false)
                };

                friendControl.Click += async (sender, e) =>
                {
                    if (sender is FSControl control)
                    {
                        // Save last DM before closing Naticord
                        Properties.Settings.Default.lastdmid = channelId;
                        Properties.Settings.Default.lastdm = displayName;
                        Properties.Settings.Default.Save();

                        await FriendClicked(control, displayName, userId, channelId);
                    }
                };

                friendsPanelList.Controls.Add(friendControl);
            }
        }

        private async Task LoadServersList()
        {
            string serversList = await API.SendAPI(token, "users/@me/guilds", HttpMethod.Get, null);
            JArray servers = JArray.Parse(serversList);
            Debug.WriteLine(serversList);

            serversPanelList.Controls.Clear();
            foreach (var guild in servers)
            {
                string serverName = guild["name"]?.ToString();
                string serverId = guild["id"]?.ToString();
                string serverHash = guild["icon"]?.ToString();
                
                FSControl serverControl = new FSControl
                {
                    LText = serverName,
                    SText = "A Discord server...",
                    PFPPic = await GetCachedAvatar(serverId, serverHash, true)
                };

                serverControl.Click += async (sender, e) =>
                {
                    if (sender is FSControl control)
                    {
                        await ServerClicked(control, serverName, serverId);
                    }
                };

                serversPanelList.Controls.Add(serverControl);
            }
        }

        private async Task LoadMessages(string userId, string channelId)
        {
            messagesPanel.Controls.Clear();
            string messageStack = await API.SendAPI(token, $"channels/{channelId}/messages?limit=50", HttpMethod.Get, null);
            JArray messages = JArray.Parse(messageStack);
            messages = new JArray(messages.Reverse());

            foreach (var message in messages)
            {
                var attachments = message["attachments"] as JArray;
                string attachmentUrl = attachments?.Count > 0 ? attachments[0]["url"]?.ToString() : null;

                string authorDisplay = message["author"]?["global_name"]?.ToString();
                string authorUser = message["author"]?["username"]?.ToString();
                string authorPFP = message["author"]?["avatar"]?.ToString();
                string authorID = message["author"]?["id"]?.ToString();
                string content = message["content"]?.ToString();

                string displayName = !string.IsNullOrWhiteSpace(authorDisplay) ? authorDisplay :
                                     !string.IsNullOrWhiteSpace(authorUser) ? authorUser : "Unknown";

                Image attachment = attachmentUrl != null ? await DownloadImage(attachmentUrl) : null;

                await AddMessage(displayName, content, authorID, authorPFP, attachment, currentChannelId);
            }
        }

        private async Task SendMessage()
        {
            string message = messageTextBox.Text.Trim();
            byte[] fileData = null;
            string fileName = null;

            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                fileData = File.ReadAllBytes(selectedFilePath);
                fileName = Path.GetFileName(selectedFilePath);
                Debug.WriteLine($"Selected file: {selectedFilePath}");
            }

            if (!string.IsNullOrEmpty(message) || fileData != null)
            {
                try
                {
                    var postData = new { content = message };
                    string responseString = await API.SendAPI(token, $"channels/{currentChannelId}/messages", HttpMethod.Post, postData, fileData, fileName);
                    Debug.WriteLine(responseString);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error! {ex.Message}");
                    new CMessageBox("Something went wrong...", $"Naticord hit a bump and can't send your message. Report it on GitHub if your Wi-Fi is fine. {ex.Message}").Show();
                }
                finally
                {
                    messageTextBox.Clear();
                    selectedFilePath = null;
                    uploadFileName.Text = "No file has been selected.";
                    SetUplCanButtonVisibility(false);
                }
            }
        }

        // Helper functions
        public async Task<Image> DownloadImage(string url, string savePath = null)
        {
            try
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                using (var ms = new MemoryStream(imageBytes))
                {
                    try
                    {
                        Image img = Image.FromStream(ms);
                        if (!string.IsNullOrEmpty(savePath))
                        {
                            File.WriteAllBytes(savePath, imageBytes);
                        }
                        return img;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The downloaded data is not a valid image.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download or load image: {ex.Message} URL used: {url}");
                return null;
            }
        }

        private void RenderPlaceholderMessageBox(string placeholder)
        {
            messagesPanel.Controls.Clear();
            Label placeholderLabel = new Label
            {
                Text = placeholder,
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Regular | FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Width = messagesPanel.ClientSize.Width,
                Height = messagesPanel.ClientSize.Height,
                ForeColor = Color.DarkGray
            };

            messagesPanel.Controls.Add(placeholderLabel);
        }


        private async Task<Image> GetCachedAvatar(string Id, string avatarHash, bool isServer)
        {
            if (string.IsNullOrEmpty(avatarHash))
                return Properties.Resources.discord_profile;

            string avatarFile = Path.Combine(AvatarCachePath, $"{avatarHash}-{Id}.png");

            if (File.Exists(avatarFile))
            {
                return Image.FromFile(avatarFile);
            }
            else
            {
                string avatarUrl = GetAvatarUrl(Id, avatarHash, isServer);
                return await DownloadImage(avatarUrl, avatarFile);
            }
        }

        private string GetAvatarUrl(string userId, string avatarHash, bool isServer)
        {
            if (isServer)
            {
                return $"https://cdn.discordapp.com/icons/{userId}/{avatarHash}.png?size=128";
            }
            else
            {
                return $"https://cdn.discordapp.com/avatars/{userId}/{avatarHash}.png?size=128";
            }
        }

        private async Task FriendClicked(FSControl pickedUser, string username, string userId, string channelId)
        {
            foreach (FSControl friend in friendsPanelList.Controls)
            {
                friend.ClickedDesignChange(false);
            }

            pickedUser.ClickedDesignChange(true);
            currentChannelId = channelId;
            await LoadMessages(userId, channelId);
        }

        private async Task ServerClicked(FSControl pickedServer, string username, string serverId)
        {
            foreach (FSControl server in serversPanelList.Controls)
            {
                server.ClickedDesignChange(false);
            }

            pickedServer.ClickedDesignChange(true);
            // currentChannelId = channelId;
            // await LoadMessages(userId, channelId);
        }

        public async Task AddMessage(string displayName, string content, string authorID, string authorAvatar, Image attachment, string channelId)
        {
            if (channelId != currentChannelId) return;

            if (messagesPanel.InvokeRequired)
            {
                await Task.Run(() => messagesPanel.Invoke(new Action(async () => await AddMessageInternal(displayName, content, authorID, authorAvatar, attachment))));
            }
            else
            {
                await AddMessageInternal(displayName, content, authorID, authorAvatar, attachment);
            }
        }

        public async Task AddMessageInternal(string author, string content, string userId, string avatarHash, Image attachment)
        {
            try
            {
                Message messageControl = new Message
                {
                    authorText = author,
                    messageContentText = content,
                    PFPPicAuthor = await GetCachedAvatar(userId, avatarHash, false)
                };

                if (attachment != null)
                {
                    messageControl.attachmentImageDisplay = attachment;
                }

                messagesPanel.Controls.Add(messageControl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding message: {ex.Message}");
            }
            finally
            {
                GC.Collect();
                ScrollToBottom();
            }
        }

        private void ScrollToBottom()
        {
            messagesPanel.AutoScroll = true;
            if (messagesPanel.InvokeRequired)
            {
                messagesPanel.Invoke(new Action(ScrollToBottom));
            }
            else
            {
                messagesPanel.PerformLayout();
                messagesPanel.VerticalScroll.Value = messagesPanel.VerticalScroll.Maximum;
                messagesPanel.Invalidate();
            }
        }

        private void AddKeyUpHandler(TextBox textBox)
        {
            textBox.KeyUp += (sender, e) =>
            {
                if (e.KeyData == (Keys.V | Keys.Control))
                {
                    CheckPastedContent(sender as TextBox);
                }
            };

            textBox.KeyDown += async (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await SendMessage();
                }
            };
        }

        private void CheckPastedContent(TextBox textBox)
        {
            if (Clipboard.ContainsFileDropList())
            {
                HandleFilePaste(Clipboard.GetFileDropList()[0]);
            }
            else if (Clipboard.ContainsImage())
            {
                HandleImagePaste(Clipboard.GetImage());
            }
        }

        private void HandleFilePaste(string filePath)
        {
            selectedFilePath = filePath;
            var fileInfo = new FileInfo(filePath);
            DisplayFileInfo(fileInfo.Name, fileInfo.Length);
        }

        private void HandleImagePaste(Image image)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "image.png");
            image.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Png);

            selectedFilePath = tempFilePath;
            var fileInfo = new FileInfo(tempFilePath);
            DisplayFileInfo(fileInfo.Name, fileInfo.Length);
        }

        private void DisplayFileInfo(string fileName, long fileSize)
        {
            Debug.WriteLine($"Pasted file: {fileName}, Size: {fileSize} bytes");
            uploadFileName.Text = $"Selected: {fileName} ({fileSize / 1024} KB)";
            SetUplCanButtonVisibility(true);
        }

        private async void LoadLastDM()
        {
            string lastDM = Properties.Settings.Default.lastdm;
            string lastChannelId = Properties.Settings.Default.lastdmid;

            if (string.IsNullOrEmpty(lastDM) || string.IsNullOrEmpty(lastChannelId)) return;

            messagesPanel.Controls.Clear();
            foreach (FSControl friend in friendsPanelList.Controls)
            {
                if (friend.LText == lastDM)
                {
                    await FriendClicked(friend, lastDM, null, lastChannelId);
                    break;
                }
            }
        }

        private void LoadTrayIcon(string title = null, string content = null)
        {
            trayIcon = new NotifyIcon
            {
                Icon = this.Icon,
                Text = "Naticord",
                Visible = true
            };

            ContextMenu trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", (s, e) => Application.Exit());
            trayIcon.ContextMenu = trayMenu;

            if (!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(content))
            {
                trayIcon.BalloonTipTitle = title ?? "Naticord";
                trayIcon.BalloonTipText = content ?? "Message content";
                if (IsLegacySystem())
                {
                    trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                }
                trayIcon.ShowBalloonTip(5000);
            }
        }

        private bool IsLegacySystem()
        {
            var osVersion = Environment.OSVersion.Version;
            return Environment.OSVersion.Platform == PlatformID.Win32NT && osVersion.Major == 6 && osVersion.Minor <= 3;
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
            LoadTrayIcon(null, null);
            await CheckIfTokenIsValid();

            infoBar.BeginInvoke(new Action(() =>
            {
                infoBar.Items.AddRange(new ToolStripItem[]
                {
                    usernameLabelAndImage,
                    new ToolStripSeparator(),
                    naticordVersion,
                    new ToolStripSeparator(),
                    uploadFileName
                });
            }));

            RenderPlaceholderMessageBox("Loading the UI, give us a few seconds to load content...");
            
            await SetUserInfo();

            Websocket WSClient = new Websocket(this);
            await Task.Delay(1500); // Wait for the WS to initialize before actually doing anything

            await LoadFriendsList();
            await LoadServersList();

            RenderPlaceholderMessageBox(string.Empty);

            GC.Collect();
        }

        // Button related functions
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files|*.*",
                Title = "Select a file to upload..."
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                var fileInfo = new FileInfo(selectedFilePath);
                DisplayFileInfo(fileInfo.Name, fileInfo.Length);
            }
        }

        private void SetUplCanButtonVisibility(bool enabled)
        {
            uploadButton.Visible = !enabled;
            cancelButton.Visible = enabled;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            selectedFilePath = null;
            uploadFileName.Text = "No file has been selected.";
            SetUplCanButtonVisibility(false);
        }
    }
}