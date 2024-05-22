using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Naticord
{
    public partial class Naticord : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClient websocketClient;
        private Login parentLogin;
        private string accessToken;
        private string currentChannelId;
        private Dictionary<string, string> userCache = new Dictionary<string, string>();

        public static string DiscordApiBaseUrl1 => DiscordApiBaseUrl;

        public WebSocketClient WebsocketClient { get => websocketClient; set => websocketClient = value; }
        public string AccessToken { get { return accessToken; } set => accessToken = value; }
        public string CurrentChannelId { get => currentChannelId; set => currentChannelId = value; }

        public Naticord(Login parentLogin, string accessToken)
        {
            InitializeComponent();
            this.parentLogin = parentLogin;
            AccessToken = accessToken;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            friendListBox.SelectedIndexChanged += FriendList_SelectedIndexChanged;
            serverListBox.DoubleClick += ServerListBox_DoubleClick;
            sendMessage.KeyDown += SendMessage_KeyDown;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

            websocketClient = new WebSocketClient(accessToken, this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            parentLogin.Hide();
        }

        private void Naticord_Load(object sender, EventArgs e)
        {
            PopulateUserProfile();
            PopulateTabs();
        }

        private void PopulateUserProfile()
        {
            try
            {
                dynamic userProfile = GetApiResponse("users/@me");
                string username = userProfile.global_name ?? userProfile.username;

                if (username.Length > 10)
                {
                    username = username.Substring(0, 10) + "...";
                }

                usernameLabel.Text = $"{username}";

                string avatarUrl = GetAvatarUrl(userProfile);
                if (!string.IsNullOrEmpty(avatarUrl))
                {
                    Image profileImage = DownloadImage(avatarUrl);
                    if (profileImage != null)
                    {
                        profilePictureBox.Image = profileImage;
                    }
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }


        private void PopulateTabs()
        {
            PopulateFriendsTab();
            PopulateServersTab();
        }

        private void PopulateFriendsTab()
        {
            try
            {
                dynamic friends = GetApiResponse("users/@me/relationships");
                foreach (var friend in friends)
                {
                    string username;
                    if (friend.nickname != null) { username = friend.nickname; } else if (friend.user.global_name != null) { username = friend.user.global_name; } else { username = friend.user.username; }
                    var friendItem = new ListViewItem(username)
                    {
                        Tag = (string)friend.user.id
                    };
                    friendListBox.Items.Add(friendItem);
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve friend list", ex);
            }
        }

        private void PopulateServersTab()
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                List<string> serverNames = new List<string>();
                foreach (var guild in guilds)
                {
                    string guildName = guild.name.ToString();
                    var serverItem = new ListViewItem(guildName)
                    {
                        Tag = (string)guild.id
                    };
                    serverListBox.Items.Add(serverItem);
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }

        private void FriendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (friendListBox.SelectedItems.Count > 0)
            {
                var selectedFriend = friendListBox.SelectedItems[0];
                string friendId = (string)selectedFriend.Tag;
                if (!string.IsNullOrEmpty(friendId))
                {
                    try
                    {
                        dynamic friendChannels = GetApiResponse("users/@me/channels");
                        foreach (dynamic channel in friendChannels)
                        {
                            if (channel.type == 1 && channel.recipients.Count > 0)
                            {
                                dynamic recipient = channel.recipients[0];

                                if (friendId == recipient.id.ToString())
                                {
                                    FetchMessages(channel);
                                    return;
                                }
                            }
                        }
                        MessageBox.Show("No messages found for this friend.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (WebException ex)
                    {
                        ShowErrorMessage("Failed to retrieve messages", ex);
                    }
                }
            }
        }

        private void FetchMessages(dynamic channel)
        {
            string channelId = channel.id;
            CurrentChannelId = channelId;
            dynamic messages = GetApiResponse($"channels/{channelId}/messages");
            DisplayMessages(messages);
        }

        private void ServerListBox_DoubleClick(object sender, EventArgs e)
        {
            if (serverListBox.SelectedItems.Count > 0)
            {
                var selectedItem = serverListBox.SelectedItems[0];
                string id = (string)selectedItem.Tag;
                if (selectedItem.Group != null)
                {
                    string channelId = id;
                    try
                    {
                        dynamic messages = GetApiResponse($"channels/{channelId}/messages");
                        DisplayMessages(messages);
                        CurrentChannelId = channelId;
                    }
                    catch (WebException ex)
                    {
                        ShowErrorMessage("Failed to retrieve messages for the selected channel", ex);
                    }
                }
                else
                {
                    string serverId = id;
                    try
                    {
                        dynamic channels = GetApiResponse($"guilds/{serverId}/channels");
                        DisplayChannels(channels);
                    }
                    catch (WebException ex)
                    {
                        ShowErrorMessage("Failed to fetch channels for the selected server", ex);
                    }
                }
            }
        }

        private void DisplayChannels(dynamic channels)
        {
            serverListBox.Items.Clear();
            serverListBox.Groups.Clear();

            Dictionary<string, ListViewGroup> categoryGroups = new Dictionary<string, ListViewGroup>();

            foreach (var channel in channels)
            {
                if (channel.type == 4)
                {
                    string categoryId = channel.id;
                    string categoryName = channel.name;

                    ListViewGroup categoryGroup = new ListViewGroup(categoryName);
                    categoryGroups[categoryId] = categoryGroup;

                    serverListBox.Groups.Add(categoryGroup);
                }
            }

            foreach (var channel in channels)
            {
                if (channel.type != 4)
                {
                    string channelId = channel.id;
                    string channelName = channel.name;
                    string categoryId = channel.parent_id;

                    if (categoryId != null && categoryGroups.ContainsKey(categoryId))
                    {
                        ListViewGroup categoryGroup = categoryGroups[categoryId];

                        ListViewItem channelItem = new ListViewItem("# " + channelName)
                        {
                            Group = categoryGroup,
                            Tag = channelId
                        };

                        serverListBox.Items.Add(channelItem);
                    }
                    else
                    {
                        ListViewItem channelItem = new ListViewItem("# " + channelName)
                        {
                            Tag = channelId
                        };

                        serverListBox.Items.Add(channelItem);
                    }
                }
            }
        }

        private void DisplayMessages(dynamic messages)
        {
            messageBox.Clear();

            messages = ((JArray)messages).Reverse();

            foreach (var message in messages)
            {
                string author = GetAuthorDisplayName(message["author"]);
                string content = FormatPings(message["content"].ToString());

                AppendTextWithFormatting($"{author}: {content}\n", messageBox);
            }
        }

        private string FormatPings(string content)
        {
            return Regex.Replace(content, @"<@(\d+)>", new MatchEvaluator(MatchEvaluator));
        }

        private string MatchEvaluator(Match match)
        {
            string userId = match.Groups[1].Value;
            string username = GetUsernameById(userId);
            return $"@{username}";
        }

        private void AppendTextWithFormatting(string text, RichTextBox box)
        {
            var matches = Regex.Matches(text, @"@[^ ]+");

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                box.AppendText(text.Substring(lastIndex, match.Index - lastIndex));
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = Color.Blue;
                box.AppendText(match.Value);
                box.SelectionColor = box.ForeColor;

                lastIndex = match.Index + match.Length;
            }
            box.AppendText(text.Substring(lastIndex));
        }

        private string GetUsernameById(string userId)
        {
            if (userCache.ContainsKey(userId))
            {
                return userCache[userId];
            }

            try
            {
                dynamic userProfile = GetApiResponse($"users/{userId}");
                string displayName = GetAuthorDisplayName(userProfile);
                userCache[userId] = displayName;
                return displayName;
            }
            catch (WebException)
            {
                return "UnknownUser";
            }
        }

        private void DetectUrlsInRichTextBox(RichTextBox messageBox)
        {
            messageBox.LinkClicked += MessageBox_LinkClicked;
            messageBox.SelectionColor = messageBox.ForeColor;
            messageBox.DetectUrls = true;
        }

        private void MessageBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
            ((RichTextBox)sender).LinkClicked -= MessageBox_LinkClicked;
        }

        private string GetAuthorDisplayName(dynamic author)
        {
            string nickname = author["nickname"];
            string globalName = author["global_name"];
            string username = author["username"];

            if (!string.IsNullOrEmpty(nickname))
            {
                return nickname;
            }
            else if (!string.IsNullOrEmpty(globalName))
            {
                return globalName;
            }
            else
            {
                return username;
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == friendList)
            {
                friendListBox.Items.Clear();
                PopulateFriendsTab();
            }
            else if (tabControl.SelectedTab == serverList)
            {
                serverListBox.Items.Clear();
                PopulateServersTab();
            }
        }

        private void SendMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string message = sendMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(CurrentChannelId))
            {
                try
                {
                    var postData = new
                    {
                        content = message
                    };

                    string jsonPostData = JsonConvert.SerializeObject(postData);

                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Authorization] = AccessToken;

                        byte[] byteArray = Encoding.UTF8.GetBytes(jsonPostData);
                        byte[] responseArray = client.UploadData($"{DiscordApiBaseUrl}channels/{CurrentChannelId}/messages", "POST", byteArray);

                        string response = Encoding.UTF8.GetString(responseArray);
                    }

                    sendMessage.Clear();
                }
                catch (WebException ex)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string responseText = reader.ReadToEnd();
                        ShowErrorMessage($"Failed to send message. Response: {responseText}", ex);
                    }
                }
            }
        }

        public dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;

                string endpointUrl = $"{DiscordApiBaseUrl}{endpoint}?limit=20";

                string jsonResponse = webClient.DownloadString(endpointUrl);
                return JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            websocketClient.CloseWebSocket();
            parentLogin.Close();
        }

        public void UpdateMessageBoxWithFormatting(string message)
        {
            string[] parts = message.Split(new string[] { "@@" }, StringSplitOptions.None);
            messageBox.SelectionStart = messageBox.TextLength;
            messageBox.SelectionLength = 0;

            foreach (var part in parts)
            {
                if (part.StartsWith("@"))
                {
                    messageBox.SelectionColor = Color.Blue;
                    messageBox.AppendText(part);
                    messageBox.SelectionColor = messageBox.ForeColor;
                }
                else
                {
                    messageBox.AppendText(part);
                    messageBox.ScrollToCaret();
                }
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "All files (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                UploadFile(selectedFile);
            }
        }

        private void UploadFile(string filePath)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Authorization] = AccessToken;
                    byte[] response = client.UploadFile($"{DiscordApiBaseUrl}channels/{CurrentChannelId}/messages", "POST", filePath);
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to upload file", ex);
            }
        }

        private string GetAvatarUrl(dynamic userProfile)
        {
            string userId = userProfile.id;
            string avatarHash = userProfile.avatar;
            if (!string.IsNullOrEmpty(avatarHash))
            {
                return $"https://cdn.discordapp.com/avatars/{userId}/{avatarHash}.png";
            }
            return string.Empty;
        }

        private Image DownloadImage(string imageUrl)
        {
            using (WebClient client = new WebClient())
            {
                byte[] imageBytes = client.DownloadData(imageUrl);
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(stream);
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This isn't finished yet. Please wait for the official 0.1.1 stable release.");
        }
    }
}
