using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using static Naticord.Websocket;
using System.Collections.Generic;

namespace Naticord
{
    public partial class Client : Form
    {
        private static readonly HttpClient httpClient = new();
        private readonly string token;
        private string selectedFilePath = null;
        private string currentChannelId;
        private NotifyIcon trayIcon;

        // AppData paths for Naticord (mostly used for caching)
        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Naticord"
        );
        private static Dictionary<string, Image> avatarCache = new Dictionary<string, Image>();
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

        // Stores
        public static class ChannelStore
        {
            private static readonly ConcurrentBag<string> _channelIds = new();
            public static void Add(string channelId)
            {
                if (!_channelIds.Contains(channelId))
                    _channelIds.Add(channelId);
            }
            public static bool Contains(string channelId) => _channelIds.Contains(channelId);
        }

        // Discord API events
        private async Task CheckIfTokenIsValid()
        {
            try
            {
                string response = await API.SendAPI(token, "users/@me", HttpMethod.Get, null);
            }
            catch (InvalidOperationException)
            {
                new CMessageBox("Your token is invalid.", "Don't worry, this shouldn't mean anything harmful. Naticord will sign you out for you to re-sign in and get a new token. You will have to reopen Naticord.").ShowDialog();
                Properties.Settings.Default.token = null;
                Properties.Settings.Default.Save();
                Application.Restart();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Invalid response or error occurred: {ex.Message}");
                Application.Exit();
            }
            Debug.WriteLine("Token is valid.");
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

                usernameLabelAndImage.Image = await GetCachedAvatar(userId, avatarHash, false, false);
                usernameLabelAndImage.Text = $"{globalName} ({username})";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Parse error: {ex.Message}");
            }
        }

        private async Task LoadFriendsList()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string relationshipList = await API.SendAPI(token, "users/@me/channels", HttpMethod.Get, null);
            Console.WriteLine($"LoadFriendsList - API request took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();
            JArray relationships = JArray.Parse(relationshipList);
            Debug.WriteLine(relationshipList);

            friendsPanelList.Controls.Clear();
            List<FSControl> friendcontrols = new List<FSControl>();
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

                ChannelStore.Add(channelId);
                string status = UserStatusStore.GetStatus(userId);

                FSControl friendControl = new FSControl
                {
                    LText = displayName,
                    SText = status,
                    PFPPic = await GetCachedAvatar(userId, avatarHash, false, false)
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
                friendcontrols.Add(friendControl);
            }
            ;

            // Update UI on the main thread
            friendsPanelList.Controls.Clear();
            friendsPanelList.Controls.AddRange(friendcontrols.ToArray());
            Console.WriteLine($"LoadFriendsList - processing took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Stop();
        }

        private async Task LoadGroupsList()
        {
            string groupsList = await API.SendAPI(token, "users/@me/channels", HttpMethod.Get, null);
            JArray groups = JArray.Parse(groupsList);
            Debug.WriteLine(groups);
            List<FSControl> controls = new();
            foreach (var group in groups)
            {
                string type = group["type"]?.ToString();
                if (type != "3") continue; // Group chats

                int memberCount = group["recipients"]?.Count() ?? 0;
                string gcHash = group["icon"]?.ToString();
                string gcId = group["id"]?.ToString();

                string gcName = group["name"]?.ToString();
                if (string.IsNullOrEmpty(gcName))
                {
                    var recipientNames = group["recipients"]?
                        .Select(r => r["global_name"]?.ToString() ?? r["username"]?.ToString())
                        .Where(name => !string.IsNullOrEmpty(name));

                    gcName = string.Join(", ", recipientNames);
                }

                if (!string.IsNullOrEmpty(gcId))
                {
                    ChannelStore.Add(gcId);
                }

                FSControl groupControl = new FSControl
                {
                    LText = gcName,
                    SText = $"{memberCount} members",
                    PFPPic = await GetCachedAvatar(gcId, gcHash, false, true)
                };

                groupControl.Click += async (sender, e) =>
                {
                    if (sender is FSControl control)
                    {
                        await FriendClicked(control, gcName, null, gcId);
                    }
                };

                controls.Add(groupControl);
            }
            friendsPanelList.Controls.AddRange(controls.ToArray());
        }

        private async Task LoadServersList()
        {
            string serversList = await API.SendAPI(token, "users/@me/guilds", HttpMethod.Get, null);
            JArray servers = JArray.Parse(serversList);
            Debug.WriteLine(serversList);

            serversPanelList.Controls.Clear();
            List<FSControl> controls = new();
            foreach (var guild in servers)
            {
                string serverName = guild["name"]?.ToString();
                string serverId = guild["id"]?.ToString();
                string serverHash = guild["icon"]?.ToString();
                
                FSControl serverControl = new FSControl
                {
                    LText = serverName,
                    SText = "A Discord server...",
                    PFPPic = await GetCachedAvatar(serverId, serverHash, true, false)
                };

                serverControl.Click += async (sender, e) =>
                {
                    if (sender is FSControl control)
                    {
                        await ServerClicked(control, serverName, serverId);
                    }
                };

                controls.Add(serverControl);
            }
            serversPanelList.Controls.AddRange(controls.ToArray());
        }

        private async Task LoadMessages(string userId, string channelId)
        {
            List<Message> processedmessages = new();
            Stopwatch sw = Stopwatch.StartNew();
            messagesPanel.Controls.Clear();
            string messageStack = await API.SendAPI(token, $"channels/{channelId}/messages?limit=50", HttpMethod.Get, null);
            Console.WriteLine($"Api call took {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            JArray messages = JArray.Parse(messageStack);

            for (int i = messages.Count - 1; i >= 0; i--)
            {
                JToken message = messages[i];
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
                //Console.WriteLine(content);
                processedmessages.Add(await CreateMessage(displayName, content, authorID, authorPFP, attachment));
            }
            await AddMessagesRange(processedmessages);
            ScrollToBottom();
            Console.WriteLine($"Processing messages took {sw.ElapsedMilliseconds} ms");
            sw.Stop();
        }

        private async Task SendMessage()
        {
            string message = messageTextBox.Text.Trim();
            messageTextBox.Clear();
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


        public async Task<Image> DownloadImage(string url, string savePath = null)
        {
            try
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                savePath ??= Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
                File.WriteAllBytes(savePath, imageBytes);
                using (var ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Failed to download or load image: {ex.Message} URL used: {url}");
                return null;
            }
        }

        private void SetMemoryCachedAvatar(string userId, Image avatar)
        {
            if (avatarCache.Count >= 10) // cache only last 10 avatars (5 DMs) to save ram
            {
                avatarCache.Remove(avatarCache.Keys.First());
            }
            if (avatarCache.ContainsKey(userId))
            {
                avatarCache[userId].Dispose();
                avatarCache[userId] = avatar;
            }
        }
        private bool TryGetMemoryCachedAvatar(string userId, out Image avatar) 
        {
            return avatarCache.TryGetValue(userId, out avatar);
        }

        public async Task<Image> GetCachedAvatar(string userId, string avatarHash, bool isServer, bool isGC)
        {
            if (string.IsNullOrEmpty(avatarHash))
                return Properties.Resources.discord_profile;

            if (TryGetMemoryCachedAvatar(userId, out Image avatar)) return avatar;

            string avatarFile = Path.Combine(AvatarCachePath, $"{avatarHash}-{userId}.png");
            Image retrievedavatar;
            if (File.Exists(avatarFile))
            {
                retrievedavatar = Image.FromFile(avatarFile);
            }
            else
            {
                string avatarUrl = GetAvatarUrl(userId, avatarHash, isServer, isGC);
                retrievedavatar = await DownloadImage(avatarUrl, avatarFile);
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

        private async Task FriendClicked(FSControl pickedUser, string username, string userID = null, string channelId = null)
        {

            foreach (FSControl friend in friendsPanelList.Controls)
            {
                friend.ClickedDesignChange(false);
            }
            pickedUser.ClickedDesignChange(true);
            currentChannelId = channelId;
            await LoadMessages(userID, channelId);
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
        public async Task AddMessagesRange(List<Message> messages)
        {
            AddMessagesRangeInternal(messages);
        }

        public async Task AddMessageInternal(string author, string content, string userId, string avatarHash, Image attachment)
        {
            try
            {
                Message messageControl = new Message
                {
                    authorText = author,
                    messageContentText = content,
                    PFPPicAuthor = await GetCachedAvatar(userId, avatarHash, false, false)
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
                ScrollToBottom();
            }
        }

        public async Task<Message> CreateMessage(string author, string content, string userId, string avatarHash, Image attachment)
        {
            try
            {
                Message messageControl = new Message
                {
                    authorText = author,
                    messageContentText = content,
                    PFPPicAuthor = await GetCachedAvatar(userId, avatarHash, false, false)
                };

                if (attachment != null)
                {
                    messageControl.attachmentImageDisplay = attachment;
                }

                return messageControl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding message: {ex.Message}");
                return new Message();
            }
        }

        public void AddMessagesRangeInternal(List<Message> messages)
        {
            messagesPanel.Controls.AddRange(messages.ToArray());
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

        public void InitTrayIcon(string title = null, string content = null)
        {
            if (trayIcon != null)
            {
                trayIcon.BalloonTipTitle = title ?? "Naticord";
                trayIcon.BalloonTipText = content ?? "Message content";

                if (IsLegacySystem())
                {
                    trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                }

                trayIcon.ShowBalloonTip(5000);
                return;
            }

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
            InitTrayIcon(null, null);
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

            Console.WriteLine("setuserinfo is called");
            Stopwatch stopwatch = Stopwatch.StartNew();
            await SetUserInfo();
            stopwatch.Stop();
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
            Websocket WSClient = new Websocket(this);
            while (WSClient.WSClient.ReadyState != WebSocketSharp.WebSocketState.Open) await Task.Delay(100);

            stopwatch.Restart();
            await LoadFriendsList();
            Console.WriteLine($"LoadFriendsList took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();
            await LoadGroupsList();
            Console.WriteLine($"LoadGroupsList took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();
            await LoadServersList();
            Console.WriteLine($"LoadServersList took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Stop();

            RenderPlaceholderMessageBox(string.Empty);
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