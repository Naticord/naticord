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
using WebSocketSharp;

namespace Naticord
{
    public partial class Client : Form
    {
        private static readonly HttpClient httpClient = new();
        private string selectedFilePath = null;
        private string currentChannelId;
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
            AddKeyUpHandler(messageTextBox);

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

                var (status, customStatus) = UserStatusStore.GetStatus(userId);

                FSControl friendControl = new FSControl
                {
                    LText = displayName,
                    SText = status,
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
                friendsPanelList.Controls.Add(friendControl);
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

                Image attachment = null;
                if (attachmentUrl != null)
                {
                    attachment = await DownloadImage(attachmentUrl);
                }

                await AddMessage(displayName, content, authorID, authorPFP, attachment, currentChannelId);
                ScrollToBottom();
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
                    var postData = new
                    {
                        content = message
                    };

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

        private void RenderPlaceholderMessageBox(string placeholder)
        {
            var existingPlaceholder = messagesPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.ForeColor == Color.DarkGray);
            if (existingPlaceholder != null)
            {
                messagesPanel.Controls.Remove(existingPlaceholder);
            }

            Label placeholderLabel = new Label
            {
                Text = placeholder,
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
            currentChannelId = channelId;
            await LoadMessages(userId, channelId);
        }

        public async Task AddMessage(string displayName, string content, string authorID, string authorAvatar, Image attachment, string channelId)
        {
            if (channelId != currentChannelId)
            {
                return;
            }
            if (messagesPanel.InvokeRequired)
            {
                await Task.Run(() =>
                {
                    messagesPanel.Invoke(new Action(async () =>
                    {
                        await AddMessageInternal(displayName, content, authorID, authorAvatar, attachment);
                    }));
                });
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
                    PFPPicAuthor = await GetCachedAvatar(userId, avatarHash)
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
            }
        }

        private void ScrollToBottom()
        {
            messagesPanel.AutoScroll = true;
            if (messagesPanel.InvokeRequired)
            {
                messagesPanel.Invoke(new Action(() =>
                {
                    ScrollToBottom();
                }));
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
            else
            {
                // Do nothing.
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
            RenderPlaceholderMessageBox("Open a DM to get started with Naticord!");
            GC.Collect();
        }

        // Button handlers
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.Title = "Select a file to upload...";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                string fileName = Path.GetFileName(selectedFilePath);
                long fileSize = new FileInfo(selectedFilePath).Length;

                uploadFileName.Text = $"Selected: {fileName} ({fileSize / 1024} KB)";
                SetUplCanButtonVisibility(true);
            }
        }

        private void SetUplCanButtonVisibility(bool enabled)
        {
            if (enabled == true)
            {
                uploadButton.Visible = false;
                cancelButton.Visible = true;
            }
            if (enabled == false)
            {
                uploadButton.Visible = true;
                cancelButton.Visible = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            selectedFilePath = null;
            uploadFileName.Text = "No file has been selected.";
            SetUplCanButtonVisibility(false);
        }
    }
}