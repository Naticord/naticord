using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Server : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClient websocketClient;
        public string htmlStart = "<!DOCTYPE html><html><head><meta http-equiv=\"X-UA-Compatible\" content=\"edge\" ><style>* {font-family: \"Segoe UI\", sans-serif; font-size: 10pt; overflow-x: hidden;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} img {width: auto; height: auto; max-width: 60% !important; max-height: 60% !important;} .spoiler {background-color: black; color: black; border-radius: 5px;} .spoiler:hover {background-color: black; color: white; border-radius: 5px;} .ping {background-color: #e6e8fd; color: #5865f3; border-radius: 5px;} .rich {width: 60%; border-style: solid; border-radius: 5px; border-width: 2px; border-color: black; padding: 10px;}</style></head><body>";
        public string htmlMiddle = "";
        public string htmlEnd = "</body></head></html>";
        private string AccessToken;
        public long ServerID;
        public long ChatID;
        private string lastMessageAuthor = "";
        private Image _lastUploadedImage = null;

        public Server(long serverid, String token)
        {
            InitializeComponent();
            AccessToken = token;
            ServerID = serverid;
            Thread.Sleep(750);
            PopulateFields();
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private void PopulateFields()
        {
            try
            {
                dynamic guilds = GetApiResponseSynchronus("users/@me/guilds");
                foreach (var guild in guilds)
                {
                    if ((long)guild.id == ServerID)
                    {
                        usernameLabel.Text = guild.name.ToString();
                    }
                }
                dynamic channels = GetApiResponseSynchronus($"guilds/{ServerID}/channels");
                List<ListViewGroup> categoryNames = new List<ListViewGroup>();
                List<ListViewItem> channelNames = new List<ListViewItem>();
                foreach (var category in channels)
                {
                    if (category.type == 4)
                    {
                        string categoryName = category.name.ToString();
                        ListViewGroup categoryItem = new ListViewGroup(categoryName);
                        categoryItem.Tag = (long)category.id;
                        categoryNames.Add(categoryItem);
                    }
                }
                foreach (var channel in channels)
                {
                    if (channel.type == 0)
                    {
                        string channelName = channel.name.ToString();
                        ListViewItem channelItem = new ListViewItem("# " + channelName.ToLower());
                        for (int i = 0; i < categoryNames.Count; i++)
                        {
                            if (channel.parent_id != null && (long)categoryNames[i].Tag == (long)channel.parent_id)
                            {
                                channelItem.Group = categoryNames[i];
                            }
                        }
                        channelItem.Tag = (long)channel.id;
                        channelNames.Add(channelItem);
                    }
                }
                channelList.Groups.AddRange(categoryNames.ToArray());
                channelList.Items.AddRange(channelNames.ToArray());
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve server list", ex);
            }
        }

        public async Task<string> AddMessage(string name, string message, string action, WebSocketClient.Attachment[] attachments, WebSocketClient.Embed[] embeds, bool reload = true, bool scroll = true, string replyname = "", string replymessage = "")
        {
            string result = string.Empty;

            // Check if task is canceled
            if (cts.Token.IsCancellationRequested)
                return result;

            if (name == lastMessageAuthor && action == "said")
            {
                htmlMiddle += "<br><p>" + await DiscordMDToHtml(message) + "</p>";
            }
            else if (action == "replied")
            {
                htmlMiddle += "<br><em style=\"color: darkgray\">┌ @" + replyname + ": " + await DiscordMDToHtml(replymessage) + "</em><br><strong>" + name + " " + action + ":</strong><br><p>" + await DiscordMDToHtml(message) + "</p>";
            }
            else
            {
                htmlMiddle += "<br><strong>" + name + " " + action + ":</strong><br><p>" + await DiscordMDToHtml(message) + "</p>";
            }

            lastMessageAuthor = name;

            // Check if task is canceled
            if (cts.Token.IsCancellationRequested)
                return result;

            if (attachments.Length > 0)
            {
                foreach (var attachment in attachments)
                {
                    if (cts.Token.IsCancellationRequested)
                        return result;

                    chatBox.ScriptErrorsSuppressed = true;

                    if (attachment.Type.Contains("image"))
                        htmlMiddle += "<br><img src=\"" + attachment.URL + "\"></img>";
                    if (attachment.Type.Contains("video"))
                        htmlMiddle += "<br><embed src=\"" + attachment.URL + "\" type=\"" + attachment.Type + "\" width=\"60%\" height=\"60%\">";
                }
            }

            if (embeds.Length > 0)
            {
                foreach (var embed in embeds)
                {
                    if (cts.Token.IsCancellationRequested)
                        return result;

                    if (embed.Type == "rich")
                        htmlMiddle += "<br><div class=\"rich\"><a style=\"color: black\" href=\"" + embed.AuthorURL + "\">" + embed.Author + "</a><br><br><a href=\"" + embed.TitleURL + "\">" + embed.Title + "</a><br><br><p>" + embed.Description + "</p></div>";
                }
            }

            if (reload && !chatBox.IsDisposed)
            {
                chatBox.DocumentText = (htmlStart + htmlMiddle + htmlEnd).ToString();
            }

            if (scroll && !chatBox.IsDisposed)
            {
                ScrollToBottom();
            }

            return result;
        }


        public void UpdateChatBox(string htmlContent)
        {
            if (chatBox.IsDisposed || this.IsDisposed)
            {
                return;
            }
            if (chatBox.InvokeRequired)
            {
                chatBox.Invoke((MethodInvoker)(() =>
                {
                    if (!chatBox.IsDisposed && !this.IsDisposed)
                    {
                        chatBox.DocumentText = htmlContent;
                        ScrollToBottom();
                    }
                }));
            }
            else
            {
                if (!chatBox.IsDisposed && !this.IsDisposed)
                {
                    chatBox.DocumentText = htmlContent;
                    ScrollToBottom();
                }
            }
        }


        private async Task<string> DiscordMDToHtml(string md)
        {
            List<string> waitingToClose = new List<string>();
            StringBuilder html = new StringBuilder();
            for (int i = 0; i < md.Length; i++)
            {
                switch (md[i].ToString())
                {
                    case "*":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "*")
                        {
                            if (!waitingToClose.Contains("**")) { html.Append("<strong>".ToCharArray()); waitingToClose.Add("**"); } else { html.Append("</strong>".ToCharArray()); waitingToClose.Remove("**"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("*")) { html.Append("<em>".ToCharArray()); waitingToClose.Add("*"); } else { html.Append("</em>".ToCharArray()); waitingToClose.Remove("*"); }
                        break;

                    case "_":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "_")
                        {
                            if (!waitingToClose.Contains("__")) { html.Append("<u>".ToCharArray()); waitingToClose.Add("__"); } else { html.Append("</u>".ToCharArray()); waitingToClose.Remove("__"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("_")) { html.Append("<em>".ToCharArray()); waitingToClose.Add("_"); } else { html.Append("</em>".ToCharArray()); waitingToClose.Remove("_"); }
                        break;

                    case "~":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "~") { if (!waitingToClose.Contains("~~")) { html.Append("<strike>".ToCharArray()); waitingToClose.Add("~~"); } else { html.Append("</strike>".ToCharArray()); waitingToClose.Remove("~~"); } i += 1; break; }
                        break;

                    case "#":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "#")
                        {
                            if (md.Length > i + 2 && md[i + 2].ToString() == "#")
                            {
                                if (!waitingToClose.Contains("###")) { html.Append("<h3>".ToCharArray()); waitingToClose.Add("###"); }
                                i += 2;
                                break;
                            }
                            if (!waitingToClose.Contains("##")) { html.Append("<h2>".ToCharArray()); waitingToClose.Add("##"); }
                            i += 1;
                            break;
                        }
                        if (!waitingToClose.Contains("#")) { html.Append("<h1>".ToCharArray()); waitingToClose.Add("#"); }
                        break;

                    case "|":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "|")
                        {
                            if (!waitingToClose.Contains("||")) { html.Append("<span class=\"spoiler\">".ToCharArray()); waitingToClose.Add("||"); } else { html.Append("</span>".ToCharArray()); waitingToClose.Remove("||"); }
                            i += 1;
                            break;
                        }
                        break;

                    case "<":
                        if (md.Length > i + 1 && md[i + 1].ToString() == "@")
                        {
                            StringBuilder ping = new StringBuilder();
                            if (!waitingToClose.Contains("||"))
                                ping.Append("<span class=\"ping\">");

                            ping.Append('@');
                            i += 2;

                            StringBuilder uid = new StringBuilder();
                            while (Char.IsNumber(md[i]))
                            {
                                uid.Append(md[i]);
                                i += 1;
                            }

                            if (md[i].ToString() == ">")
                            {
                                string username = await GetUsernameById(uid.ToString());
                                ping.Append(username);
                                if (!waitingToClose.Contains("||"))
                                    ping.Append("</span>");
                                html.Append(ping);
                            }

                            GC.Collect();
                            break;
                        }
                        break;

                    case "\n":
                        if (waitingToClose.Contains("###")) { html.Append("</h3>".ToCharArray()); waitingToClose.Remove("###"); }
                        if (waitingToClose.Contains("##")) { html.Append("</h2>".ToCharArray()); waitingToClose.Remove("##"); }
                        if (waitingToClose.Contains("#")) { html.Append("</h1>".ToCharArray()); waitingToClose.Remove("#"); }
                        html.Append("<br>".ToCharArray());
                        break;

                    default:
                        html.Append(md[i]);
                        break;
                }
            }
            for (int i = 0; i < waitingToClose.Count; i++)
            {
                switch (waitingToClose[i])
                {
                    case "*":
                    case "_":
                        html.Append("</em>".ToCharArray()); waitingToClose.Remove("*");
                        break;

                    case "**":
                        html.Append("</strong>".ToCharArray()); waitingToClose.Remove("**");
                        break;

                    case "__":
                        html.Append("</u>".ToCharArray()); waitingToClose.Remove("__");
                        break;

                    case "~~":
                        html.Append("</strike>".ToCharArray()); waitingToClose.Remove("~~");
                        break;

                    case "||":
                        html.Append("</span>".ToCharArray()); waitingToClose.Remove("||");
                        break;

                    case "###":
                        html.Append("</h3>".ToCharArray()); waitingToClose.Remove("###");
                        break;

                    case "##":
                        html.Append("</h2>".ToCharArray()); waitingToClose.Remove("##");
                        break;

                    case "#":
                        html.Append("</h1>".ToCharArray()); waitingToClose.Remove("#");
                        break;
                }
            }
            GC.Collect();
            return html.ToString();
        }

        private async Task<string> GetUsernameById(string userId)
        {
            try
            {
                dynamic user = await GetApiResponse($"users/{userId}");
                if (user.global_name != null) return user.global_name;
                return user.username;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get username for user ID {userId}: {ex.Message}");
                return "Unknown";
            }
        }

        public async Task<dynamic> GetApiResponse(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AccessToken);

                HttpResponseMessage response = await httpClient.GetAsync(DiscordApiBaseUrl + endpoint);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        public dynamic GetApiResponseSynchronus(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AccessToken);

                HttpResponseMessage response = httpClient.GetAsync(DiscordApiBaseUrl + endpoint).Result;
                response.EnsureSuccessStatusCode();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject(jsonResponse);
            }
        }

        private async Task LoadMessages(long channelID)
        {
            try
            {
                dynamic messages = await GetApiResponse($"channels/{channelID.ToString()}/messages");
                htmlMiddle = "";

                for (int i = messages.Count - 1; i >= 0; i--)
                {
                    string author = messages[i].author.global_name ?? messages[i].author.username;
                    string content = messages[i].content;

                    List<WebSocketClient.Attachment> attachmentsFormed = new List<WebSocketClient.Attachment>();
                    List<WebSocketClient.Embed> embedsFormed = new List<WebSocketClient.Embed>();

                    if (messages[i].attachments != null)
                    {
                        foreach (var attachment in messages[i].attachments)
                        {
                            attachmentsFormed.Add(new WebSocketClient.Attachment { URL = attachment.url, Type = attachment.content_type });
                        }
                    }

                    if (messages[i].embeds != null)
                    {
                        foreach (var embed in messages[i].embeds)
                        {
                            embedsFormed.Add(new WebSocketClient.Embed
                            {
                                Type = embed?.type ?? "",
                                Author = embed?.author?.name ?? "",
                                AuthorURL = embed?.author?.url ?? "",
                                Title = embed?.title ?? "",
                                TitleURL = embed?.url ?? "",
                                Description = embed?.description ?? ""
                            });
                        }
                    }

                    string messageResult = "";
                    switch ((int)messages[i].type.Value)
                    {
                        case 7:
                            messageResult = await AddMessage(author, "*Say hi!*", "slid in the server", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;

                        case 19:
                            bool found = false;
                            foreach (var message in messages)
                            {
                                if (message.id == messages[i].message_reference.message_id)
                                {
                                    string replyAuthor = message.author.global_name ?? message.author.username;
                                    messageResult = await AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, replyAuthor, message.content.Value);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                messageResult = await AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, " ", "Unable to load message");
                            }
                            break;

                        default:
                            messageResult = await AddMessage(author, content, "said", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;
                    }

                    htmlMiddle += messageResult;
                }

                chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
                ScrollToBottom();
            }
            catch (WebException ex)
            {
                ShowErrorMessage("Failed to retrieve messages", ex);
            }
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private async Task SendMessage()
        {
            string message = messageBox.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var postData = new
                    {
                        content = message
                    };
                    string jsonPostData = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AccessToken);
                        HttpContent content = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync($"{DiscordApiBaseUrl}channels/{ChatID}/messages", content);
                        response.EnsureSuccessStatusCode();

                        if (Clipboard.ContainsImage())
                        {
                            Image currentImage = Clipboard.GetImage();

                            if (_lastUploadedImage == null || !ImagesAreEqual(_lastUploadedImage, currentImage))
                            {
                                byte[] imageBytes = ImageToBytes(currentImage);
                                await UploadImage(imageBytes);
                                _lastUploadedImage = currentImage;
                            }
                        }
                    }

                    messageBox.Clear();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Failed to send message", ex);
                }
            }
        }

        private bool ImagesAreEqual(Image img1, Image img2)
        {
            if (img1 == null || img2 == null)
                return false;

            using (MemoryStream ms1 = new MemoryStream(), ms2 = new MemoryStream())
            {
                img1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                img2.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bytes1 = ms1.ToArray();
                byte[] bytes2 = ms2.ToArray();
                return bytes1.SequenceEqual(bytes2);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V))
            {
                if (Clipboard.ContainsImage())
                {
                    try
                    {
                        Image clipboardImage = Clipboard.GetImage();
                        byte[] imageBytes = ImageToBytes(clipboardImage);

                        Task.Run(() => UploadImage(imageBytes)).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Failed to upload image", ex);
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task UploadImage(byte[] imageBytes)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AccessToken);

                    MultipartFormDataContent formData = new MultipartFormDataContent();
                    formData.Add(new ByteArrayContent(imageBytes), "file", "image.png");

                    HttpResponseMessage response = await client.PostAsync($"{DiscordApiBaseUrl}channels/{ChatID}/messages", formData);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to upload image", ex);
            }
        }

        private byte[] ImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void channelList_DoubleClick(object sender, EventArgs ex)
        {
            if (channelList.SelectedItems[0].Text != null)
            {
                if (websocketClient != null) websocketClient.CloseWebSocket();
                string selectedChannel = channelList.SelectedItems[0].Text;
                long channelID = (long)channelList.SelectedItems[0].Tag;
                if (channelID >= 0)
                {
                    ChatID = channelID;
                    LoadMessages(channelID);
                    WebSocketClient client = WebSocketClient.Instance(AccessToken);
                }
                else MessageBox.Show("Unable to open this channel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ScrollToBottom()
        {
            try
            {
                Application.DoEvents();
                if (chatBox.Document != null && chatBox.Document.Body != null)
                {
                    chatBox.Document.OpenNew(true);
                    chatBox.Document.Write(htmlStart + htmlMiddle + htmlEnd);
                    chatBox.Document.Window.ScrollTo(0, chatBox.Document.Body.ScrollRectangle.Bottom);
                }
            }
            catch (Exception)
            {
                // who tf cares bro it works
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            ScrollToBottom();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (websocketClient != null) websocketClient.CloseWebSocket();
            GC.Collect();
            cts.Cancel();
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }
    }
}