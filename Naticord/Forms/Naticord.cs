using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;

namespace Naticord
{
    public partial class Naticord : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private const string ProfilePicturePath = "user.png";
        private string AccessToken;
        private Login signin;
        private string userPFP;
        private Dictionary<string, long> groupChatIDs = new Dictionary<string, long>();

        private List<ListViewItem> allFriends;
        private List<ListViewItem> allServers;

        private ContextMenuStrip friendsContextMenu;
        private ContextMenuStrip serversContextMenu;

        public Naticord(string token, Login signinArg)
        {
            InitializeComponent();
            signin = signinArg;
            AccessToken = token;
            SetUserInfo();
            PopulateFriendsTab();
            PopulateServersTab();
            SetProfilePictureRegion();

            Application.EnableVisualStyles();

            friendSearchBar.TextChanged += FriendsSearchBar_TextChanged;
            serverSearchBar.TextChanged += ServersSearchBar_TextChanged;

            InitializeContextMenus();
        }

        private void SetProfilePictureRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, profilepicture.Width, profilepicture.Height);
            profilepicture.Region = new Region(path);
        }

        private void InitializeContextMenus()
        {
            friendsContextMenu = new ContextMenuStrip();
            serversContextMenu = new ContextMenuStrip();

            ToolStripMenuItem copyFriendIdMenuItem = new ToolStripMenuItem("Copy ID");
            ToolStripMenuItem blockFriendMenuItem = new ToolStripMenuItem("Block");
            ToolStripMenuItem unfriendMenuItem = new ToolStripMenuItem("Unfriend");
            ToolStripMenuItem leaveGroupMenuItem = new ToolStripMenuItem("Leave Group");

            copyFriendIdMenuItem.Click += CopyFriendIdMenuItem_Click;
            blockFriendMenuItem.Click += BlockFriendMenuItem_Click;
            unfriendMenuItem.Click += UnfriendMenuItem_Click;
            leaveGroupMenuItem.Click += LeaveGroupMenuItem_Click;

            friendsContextMenu.Items.Add(copyFriendIdMenuItem);
            friendsContextMenu.Items.Add(blockFriendMenuItem);
            friendsContextMenu.Items.Add(unfriendMenuItem);
            friendsContextMenu.Items.Add(leaveGroupMenuItem);

            ToolStripMenuItem copyServerIdMenuItem = new ToolStripMenuItem("Copy ID");
            ToolStripMenuItem leaveServerMenuItem = new ToolStripMenuItem("Leave Server");

            copyServerIdMenuItem.Click += CopyServerIdMenuItem_Click;
            leaveServerMenuItem.Click += LeaveServerMenuItem_Click;

            serversContextMenu.Items.Add(copyServerIdMenuItem);
            serversContextMenu.Items.Add(leaveServerMenuItem);

            friendsList.ContextMenuStrip = friendsContextMenu;
            serversList.ContextMenuStrip = serversContextMenu;

            friendsList.MouseUp += FriendsList_MouseUp;
            serversList.MouseUp += ServersList_MouseUp;
        }

        private void FriendsList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = friendsList.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    friendsContextMenu.Show(friendsList, e.Location);
                }
            }
        }

        private void ServersList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = serversList.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    serversContextMenu.Show(serversList, e.Location);
                }
            }
        }

        private void CopyFriendIdMenuItem_Click(object sender, EventArgs e)
        {
            if (friendsList.SelectedItems.Count > 0)
            {
                string selectedFriend = friendsList.SelectedItems[0].Text;
                long chatID = GetChatID(selectedFriend);
                Clipboard.SetText(chatID.ToString());
            }
        }

        private void BlockFriendMenuItem_Click(object sender, EventArgs e)
        {
            if (friendsList.SelectedItems.Count > 0)
            {
                string selectedFriend = friendsList.SelectedItems[0].Text;
                long friendID = GetFriendID(selectedFriend);
                if (friendID >= 0)
                {
                    BlockUser(friendID);
                    MessageBox.Show($"{selectedFriend} has been blocked.", "Block User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to block this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UnfriendMenuItem_Click(object sender, EventArgs e)
        {
            if (friendsList.SelectedItems.Count > 0)
            {
                string selectedFriend = friendsList.SelectedItems[0].Text;
                long friendID = GetFriendID(selectedFriend);
                if (friendID >= 0)
                {
                    UnfriendUser(friendID);
                    MessageBox.Show($"{selectedFriend} has been unfriended.", "Unfriend User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to unfriend this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LeaveGroupMenuItem_Click(object sender, EventArgs e)
        {
            if (friendsList.SelectedItems.Count > 0)
            {
                string selectedGroup = friendsList.SelectedItems[0].Text;
                long groupID = GetGroupID(selectedGroup);
                if (groupID >= 0)
                {
                    LeaveGroup(groupID);
                    MessageBox.Show($"You have left the group: {selectedGroup}.", "Leave Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to leave this group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyServerIdMenuItem_Click(object sender, EventArgs e)
        {
            if (serversList.SelectedItems.Count > 0)
            {
                string selectedServer = serversList.SelectedItems[0].Text;
                long serverID = GetServerID(selectedServer);
                Clipboard.SetText(serverID.ToString());
            }
        }

        private void LeaveServerMenuItem_Click(object sender, EventArgs e)
        {
            if (serversList.SelectedItems.Count > 0)
            {
                string selectedServer = serversList.SelectedItems[0].Text;
                long serverID = GetServerID(selectedServer);
                if (serverID >= 0)
                {
                    LeaveServer(serverID);
                    MessageBox.Show($"You have left the server: {selectedServer}.", "Leave Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to leave this server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetUserInfo()
        {
            try
            {
                dynamic userProfile = GetApiResponse("users/@me");
                string displayname = userProfile.global_name ?? userProfile.username;
                string bio = userProfile.bio;
                usernameLabel.Text = displayname;
                descriptionLabel.Text = bio;

                userPFP = $"https://cdn.discordapp.com/avatars/{userProfile.id}/{userProfile.avatar}.png";

                if (!File.Exists(ProfilePicturePath))
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.DownloadFile(userPFP, ProfilePicturePath);
                    }
                }

                profilepicture.ImageLocation = ProfilePicturePath;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        // absolute garbo, dont touch this unless necessary (it will combust into pieces). if it compiles thats great and if it works thats even greater.
        private void PopulateFriendsTab()
        {
            try
            {
                dynamic channels = GetApiResponse("users/@me/channels");
                dynamic relationships = GetApiResponse("users/@me/relationships");
                HashSet<long> blockedUsers = new HashSet<long>();

                foreach (var relationship in relationships)
                {
                    if ((int)relationship.type == 2)
                    {
                        blockedUsers.Add((long)relationship.id);
                    }
                }

                allFriends = new List<ListViewItem>();
                HashSet<long> channelIds = new HashSet<long>();

                foreach (var channel in channels)
                {
                    long channelId = (long)channel.id;
                    if (channelIds.Contains(channelId))
                    {
                        continue;
                    }

                    string channelType = "";
                    string namesOrName = "";

                    switch ((int)channel.type)
                    {
                        case 1:
                            if (channel.recipients != null && channel.recipients.Count > 0)
                            {
                                List<string> names = new List<string>();
                                foreach (var recipient in channel.recipients)
                                {
                                    string recipientName = (string)recipient.nickname ?? (string)recipient.global_name ?? (string)recipient.username;
                                    names.Add(recipientName);
                                }
                                namesOrName = string.Join(", ", names);
                                channelType = "Direct Message";

                                if (blockedUsers.Contains((long)channel.recipients[0].id))
                                {
                                    namesOrName += " - Blocked";
                                }
                            }
                            else
                            {
                                namesOrName = "Unknown User";
                                channelType = "Direct Message";
                            }
                            break;

                        case 3:
                            if (channel.name != null)
                            {
                                namesOrName = (string)channel.name;
                                channelType = "Group Message";

                                SaveGroupChatID(namesOrName, channelId);
                            }
                            else if (channel.recipients != null && channel.recipients.Count > 0)
                            {
                                List<string> names = new List<string>();
                                foreach (var recipient in channel.recipients)
                                {
                                    string recipientName = (string)recipient.nickname ?? (string)recipient.global_name ?? (string)recipient.username;
                                    names.Add(recipientName);
                                }
                                namesOrName = string.Join(", ", names);
                                channelType = "Group Message";

                                SaveGroupChatID(namesOrName, channelId);
                            }

                            namesOrName += $" - {channel.recipients.Count} members";

                            if (!groupChatIDs.ContainsKey(namesOrName))
                            {
                                groupChatIDs[namesOrName] = channelId;
                            }
                            break;
                    }

                    ListViewItem item = new ListViewItem(namesOrName);
                    item.Tag = channelType;
                    allFriends.Add(item);
                    channelIds.Add(channelId);
                }

                friendsList.Items.Clear();
                friendsList.Items.AddRange(allFriends.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve channel list", ex);
            }
        }

        private void SaveGroupChatID(string groupName, long chatID)
        {
            groupChatIDs[groupName] = chatID;
        }

        private long GetChatID(string name)
        {
            try
            {
                dynamic channels = GetApiResponse("users/@me/channels");
                foreach (var channel in channels)
                {
                    if (channel.type == 1) // i fucked up this line so bad it once started to load every single group chat i had lmfao great coding skillz 1337
                    {
                        foreach (var recipient in channel.recipients)
                        {
                            string recipientName = recipient.global_name ?? recipient.username;
                            if (recipientName == name)
                            {
                                return (long)channel.id;
                            }
                        }
                    }
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve chat ID", ex);
                return -1;
            }
        }

        private long GetFriendID(string name)
        {
            try
            {
                dynamic friends = GetApiResponse("users/@me/relationships");
                foreach (var friend in friends)
                {
                    if (friend.type == 1)
                    {
                        string friendName = friend.user.global_name ?? friend.user.username;
                        if (friendName == name)
                        {
                            return (long)friend.id;
                        }
                    }
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve friend ID", ex);
                return -1;
            }
        }

        private long GetGroupID(string name)
        {
            if (groupChatIDs.ContainsKey(name))
            {
                return groupChatIDs[name];
            }
            else
            {
                // Handle case when ID is not found
                return -1;
            }
        }

        private long GetServerID(string name)
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                foreach (var guild in guilds)
                {
                    if (guild.name.ToString() == name) return (long)guild.id;
                }
                return -1;
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
                return -1;
            }
        }

        private void PopulateServersTab()
        {
            try
            {
                dynamic guilds = GetApiResponse("users/@me/guilds");
                allServers = new List<ListViewItem>();
                foreach (var guild in guilds)
                {
                    string guildName = guild.name.ToString();
                    allServers.Add(new ListViewItem(guildName));
                }
                serversList.Items.AddRange(allServers.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            signin.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            signin.Close();
        }

        private void friendsList_DoubleClick(object sender, EventArgs e)
        {
            if (friendsList.SelectedItems.Count > 0)
            {
                string selectedChannel = friendsList.SelectedItems[0].Text;
                string channelType = friendsList.SelectedItems[0].Tag as string; // Retrieve channel type from Tag

                if (channelType == "Direct Message")
                {
                    long chatID = GetChatID(selectedChannel);
                    if (chatID >= 0)
                    {
                        DM dm = new DM(chatID, GetFriendID(selectedChannel), AccessToken, userPFP);
                        dm.Show();
                        Console.WriteLine($"Direct Message Chat ID: {chatID}");
                    }
                    else
                    {
                        MessageBox.Show("Unable to open Direct Message chat. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (channelType == "Group Message")
                {
                    long groupID = GetGroupID(selectedChannel);
                    if (groupID >= 0)
                    {
                        Group groupChat = new Group(groupID, AccessToken, userPFP);
                        groupChat.Show();
                        Console.WriteLine($"Group Chat ID: {groupID}");
                    }
                    else
                    {
                        MessageBox.Show("Unable to open Group Message chat. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Unknown channel type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void serversList_DoubleClick(object sender, EventArgs ex)
        {
            if (serversList.SelectedItems.Count > 0)
            {
                string selectedServer = serversList.SelectedItems[0].Text;
                long serverID = GetServerID(selectedServer);
                if (serverID >= 0)
                {
                    Server server = new Server(serverID, AccessToken);
                    server.Show();
                }
                else
                {
                    MessageBox.Show("Unable to open this Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }

        private void FriendsSearchBar_TextChanged(object sender, EventArgs e)
        {
            FilterItems(friendSearchBar.Text.ToLower(), allFriends, friendsList);
        }

        private void ServersSearchBar_TextChanged(object sender, EventArgs e)
        {
            FilterItems(serverSearchBar.Text.ToLower(), allServers, serversList);
        }

        private void FilterItems(string searchText, List<ListViewItem> allItems, ListView listView)
        {
            listView.Items.Clear();
            List<ListViewItem> filteredItems = allItems.FindAll(item => item.Text.ToLower().Contains(searchText));
            listView.Items.AddRange(filteredItems.ToArray());
        }

        private void BlockUser(long userID)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                    var data = new System.Collections.Specialized.NameValueCollection();
                    data["type"] = "2";
                    webClient.UploadValues($"{DiscordApiBaseUrl}users/@me/relationships/{userID}", "PUT", data);
                    friendsList.Items.Clear();
                    PopulateFriendsTab();
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to block user", ex);
            }
        }

        private void UnfriendUser(long userID)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                    webClient.UploadValues($"{DiscordApiBaseUrl}users/@me/relationships/{userID}", "DELETE", new System.Collections.Specialized.NameValueCollection());
                    friendsList.Items.Clear();
                    PopulateFriendsTab();
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to unfriend user", ex);
            }
        }

        private void LeaveGroup(long groupID)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                    webClient.UploadValues($"{DiscordApiBaseUrl}channels/{groupID}", "DELETE", new System.Collections.Specialized.NameValueCollection());
                    friendsList.Items.Clear();
                    PopulateFriendsTab();
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to leave group", ex);
            }
        }

        private void LeaveServer(long serverID)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = AccessToken;
                    webClient.UploadValues($"{DiscordApiBaseUrl}users/@me/guilds/{serverID}", "DELETE", new System.Collections.Specialized.NameValueCollection());
                    serversList.Items.Clear();
                    PopulateServersTab();
                }
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to leave server", ex);
            }
        }
    }
}
