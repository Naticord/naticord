using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Naticord : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private string AccessToken;

        public Naticord(string accessToken)
        {
            InitializeComponent();
            AccessToken = accessToken;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            friendListBox.SelectedIndexChanged += FriendList_SelectedIndexChanged;
            serverListBox.SelectedIndexChanged += ServerList_SelectedIndexChanged;
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
                serverListBox.Items.AddRange(serverNames.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }

        private void FriendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFriend = friendListBox.SelectedItem?.ToString();
            if (selectedFriend != null)
            {
                try
                {
                    dynamic friendChannels = GetApiResponse("users/@me/channels");
                    foreach (var channel in friendChannels)
                    {
                        if (channel.type == 1 && channel.recipients[0].username == selectedFriend)
                        {
                            string channelId = channel.id;
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

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedServer = serverListBox.SelectedItem?.ToString();
            if (selectedServer != null)
            {
                // wip, not finished
            }
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
    }
}
