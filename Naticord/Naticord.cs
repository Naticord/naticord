using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Windows.Forms;
using System.Linq;
using WebSocketSharp;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    public partial class Naticord : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClient websocketClient;
        // set these to public for stuff
        public string AccessToken;
        public string CurrentChannelId;

        public Naticord(string accessToken)
        {
            InitializeComponent();
            AccessToken = accessToken;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            friendListBox.SelectedIndexChanged += FriendList_SelectedIndexChanged;
            serverListBox.DoubleClick += ServerListBox_DoubleClick;
            sendMessage.KeyDown += SendMessage_KeyDown;

            // set tls so the app doesnt break
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

            // initialize the websocket
            websocketClient = new WebSocketClient(accessToken, this);
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
                string username = userProfile.username;
                usernameLabel.Text = $"Welcome, {username}!";
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
                List<string> friendNames = new List<string>();
                foreach (var friend in friends)
                {
                    if (friend.type == 1 && friend.user.username != null)
                    {
                        friendNames.Add((string)friend.user.username);
                    }
                }
                friendListBox.Items.AddRange(friendNames.ToArray());
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
                    serverNames.Add(guildName);
                }
                serverListBox.Items.AddRange(serverNames.Select(name => new ListViewItem(name)).ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }

        private void FriendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (friendListBox.SelectedItem != null)
            {
                string selectedFriend = friendListBox.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedFriend))
                {
                    try
                    {
                        dynamic friendChannels = GetApiResponse("users/@me/channels");
                        foreach (var channel in friendChannels)
                        {
                            if (channel.type == 1 && channel.recipients[0].username == selectedFriend)
                            {
                                string channelId = channel.id;
                                CurrentChannelId = channelId;
                                dynamic messages = GetApiResponse($"channels/{channelId}/messages");
                                DisplayMessages(messages);
                                return;
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

        private void ServerListBox_DoubleClick(object sender, EventArgs e)
        {
            if (serverListBox.SelectedItems.Count > 0)
            {
                string selectedServer = serverListBox.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(selectedServer))
                {
                    try
                    {
                        dynamic guilds = GetApiResponse("users/@me/guilds");
                        foreach (var guild in guilds)
                        {
                            if (guild.name == selectedServer)
                            {
                                string serverId = guild.id;

                                dynamic channels = GetApiResponse($"guilds/{serverId}/channels");

                                DisplayChannels(channels);
                                return;
                            }
                        }
                        MessageBox.Show("Failed to find the selected server.");
                    }
                    catch (WebException ex)
                    {
                        ShowErrorMessage("Failed to fetch channels for server", ex);
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

                    if (CanAccessChannel(channel) && categoryGroups.ContainsKey(categoryId))
                    {
                        ListViewGroup categoryGroup = categoryGroups[categoryId];

                        ListViewItem channelItem = new ListViewItem(channelName);
                        channelItem.Group = categoryGroup;

                        serverListBox.Items.Add(channelItem);
                    }
                }
            }
        }

        private bool CanAccessChannel(dynamic channel)
        {
            if (channel.type == 5)
            {
                return false;
            }
            return true;
        }


        private void DisplayMessages(dynamic messages)
        {
            messageBox.Clear();

            foreach (var message in messages)
            {
                string author = message["author"]["username"];
                string content = message["content"];

                messageBox.AppendText($"{author}: {content}\n");
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
                    string postData = $"{{\"content\": \"{message}\"}}";

                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Authorization] = AccessToken;
                        string response = client.UploadString($"{DiscordApiBaseUrl}channels/{CurrentChannelId}/messages", "POST", postData);
                    }

                    sendMessage.Clear();
                }
                catch (WebException ex)
                {
                    ShowErrorMessage("Failed to send message", ex);
                }
            }
        }

        private dynamic GetApiResponse(string endpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                string jsonResponse = webClient.DownloadString(DiscordApiBaseUrl + endpoint);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
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
        }

        public void UpdateMessageBox(string message)
        {
            messageBox.AppendText(message);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();

            settingsForm.Show();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a work in progress! Please wait for the next release.");
        }
    }

    public class WebSocketClient
    {
        // this is broken! will be fixed soon
        private Naticord parentNaticordForm;
        private WebSocket webSocket;
        public const SslProtocols Tls12 = (SslProtocols)0x00000C00;

        public WebSocketClient(string accessToken, Naticord parentNaticordForm)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // idk why this doesnt work? it should but idk ill fix it later
            this.parentNaticordForm = parentNaticordForm;
            InitializeWebSocket(accessToken);
        }

        private void InitializeWebSocket(string accessToken)
        {
            webSocket = new WebSocket($"wss://gateway.discord.gg/?v=9&encoding=json");
            webSocket.SslConfiguration.EnabledSslProtocols = Tls12;
            webSocket.OnMessage += (sender, e) => HandleWebSocketMessage(e.Data);
            string AccessToken = accessToken;
            webSocket.OnError += (sender, e) => HandleWebSocketError(e.Message);
            webSocket.Connect();
            SendIdentifyPayload(accessToken);
        }

        private void SendIdentifyPayload(string accessToken)
        {
            if (webSocket.ReadyState == WebSocketState.Open)
            {
                var identifyPayload = new
                {
                    op = 2,
                    d = new
                    {
                        token = accessToken,
                        properties = new
                        {
                            os = "windows",
                            browser = "chrome",
                            device = "pc"
                        }
                    }
                };

                try
                {
                    webSocket.Send(Newtonsoft.Json.JsonConvert.SerializeObject(identifyPayload));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending identify payload: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("WebSocket connection is not open. Unable to send identify payload.");
            }
        }

        private void HandleWebSocketMessage(string data)
        {
            var json = JObject.Parse(data);
            int opCode = (int)json["op"];

            switch (opCode)
            {
                case 0:
                    string eventType = (string)json["t"];
                    if (eventType == "MESSAGE_CREATE")
                    {
                        HandleMessageCreateEvent(json["d"]);
                    }
                    break;
                default:
                    // if needed here ill add other op codes js incase
                    break;
            }
        }

        private void HandleMessageCreateEvent(JToken jToken)
        {
            throw new NotImplementedException();
        }

        private void HandleMessageCreateEvent(JObject eventData)
        {
            string channelId = (string)eventData["channel_id"];
            string author = (string)eventData["author"]["username"];
            string content = (string)eventData["content"];

            if (channelId == parentNaticordForm.CurrentChannelId)
            {
                parentNaticordForm.Invoke((MethodInvoker)(() =>
                {
                    parentNaticordForm.UpdateMessageBox($"{author}: {content}\n");
                }));
            }
        }

        private void HandleWebSocketError(string errorMessage)
        {
            parentNaticordForm.Invoke((MethodInvoker)(() =>
            {
                MessageBox.Show(parentNaticordForm, $"WebSocket Error: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }

        public void CloseWebSocket()
        {
            webSocket.Close();
        }
    }
}
