using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.IO;

namespace Naticord
{
    public partial class DM : Form
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/";
        private WebSocketClientDM websocketClient;
        private const string htmlStart = "<!DOCTYPE html><html><head><meta http-equiv=\"X-UA-Compatible\" content=\"edge\" ><style>* {background-color: transparent; font-family: \"Segoe UI\", sans-serif; font-size: 10pt; overflow-x: hidden;} p,strong,b,i,em,mark,small,del,ins,sub,sup,h1,h2,h3,h4,h5,h6 {display: inline;} img {width: auto; height: auto; max-width: 60% !important; max-height: 60% !important;} .spoiler {background-color: black; color: black; border-radius: 5px;} .spoiler:hover {background-color: black; color: white; border-radius: 5px;} .ping {background-color: #e6e8fd; color: #5865f3; border-radius: 5px;} .rich {width: 60%; border-style: solid; border-radius: 5px; border-width: 2px; border-color: black; padding: 10px;}</style></head><body>";
        private string htmlMiddle = "";
        private const string htmlEnd = "</body></html>";
        private readonly string AccessToken;
        public long ChatID { get; }
        public long FriendID { get; }
        private readonly string userPFP;
        private string lastMessageAuthor = "";

        public DM(long chatid, long friendid, string token, string userpfp)
        {
            InitializeComponent();
            AccessToken = token;
            ChatID = chatid;
            FriendID = friendid;
            userPFP = userpfp;
            SetFriendInfo();
            LoadMessages();
            websocketClient = new WebSocketClientDM(AccessToken, this);
            // friend pfp
            SetProfilePictureShape(profilepicturefriend);
        }

        private void SetProfilePictureShape(PictureBox pictureBox)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }

        private async void SetFriendInfo()
        {
            try
            {
                dynamic userProfile = await GetApiResponse($"users/{FriendID}/profile");
                string displayname = userProfile.user.global_name ?? userProfile.user.username;
                string bio = userProfile.user.bio;
                usernameLabel.Text = displayname;
                profilepicturefriend.ImageLocation = $"https://cdn.discordapp.com/avatars/{userProfile.user.id}/{userProfile.user.avatar}.png";
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to retrieve user profile", ex);
            }
        }

        private async void LoadMessages()
        {
            try
            {
                dynamic messages = await GetApiResponse($"channels/{ChatID}/messages");
                Console.WriteLine(messages);
                htmlMiddle = "";
                for (int i = messages.Count - 1; i >= 0; i--)
                {
                    string author = messages[i].author.global_name ?? messages[i].author.username;
                    string content = messages[i].content;
                    var attachmentsFormed = new List<WebSocketClientDM.Attachment>();
                    var embedsFormed = new List<WebSocketClientDM.Embed>();

                    if (messages[i].attachments != null)
                    {
                        foreach (var attachment in messages[i].attachments)
                        {
                            attachmentsFormed.Add(new WebSocketClientDM.Attachment { URL = attachment.url, Type = attachment.content_type });
                        }
                    }
                    if (messages[i].embeds != null)
                    {
                        foreach (var embed in messages[i].embeds)
                        {
                            embedsFormed.Add(new WebSocketClientDM.Embed { Type = embed?.type ?? "", Author = embed?.author?.name ?? "", AuthorURL = embed?.author?.url ?? "", Title = embed?.title ?? "", TitleURL = embed?.url ?? "", Description = embed?.description ?? "" });
                        }
                    }

                    switch ((int)messages[i].type.Value)
                    {
                        case 7:
                            AddMessage(author, "*Say hi!*", "slid in the server", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;

                        case 19:
                            bool found = false;
                            foreach (var message in messages)
                            {
                                if (message.id == messages[i].message_reference.message_id)
                                {
                                    string replyAuthor = message.author.global_name ?? message.author.username;
                                    AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, replyAuthor, message.content.Value);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) AddMessage(author, content, "replied", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false, " ", "Unable to load message");
                            break;

                        default:
                            AddMessage(author, content, "said", attachmentsFormed.ToArray(), embedsFormed.ToArray(), false, false);
                            break;
                    }
                }
                chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
                await Task.Delay(200);
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to retrieve messages", ex);
            }
        }

        public void AddMessage(string name, string message, string action, WebSocketClientDM.Attachment[] attachments, WebSocketClientDM.Embed[] embeds, bool reload = true, bool scroll = true, string replyname = "", string replymessage = "")
        {
            if (name == lastMessageAuthor && action == "said")
            {
                htmlMiddle += $"<br><p>{DiscordMDToHtml(message)}</p>";
            }
            else if (action == "replied")
            {
                htmlMiddle += $"<br><em style=\"color: darkgray\">┌ @{replyname}: {DiscordMDToHtml(replymessage)}</em><br><strong>{name} {action}:</strong><br><p>{DiscordMDToHtml(message)}</p>";
            }
            else
            {
                htmlMiddle += $"<br><strong>{name} {action}:</strong><br><p>{DiscordMDToHtml(message)}</p>";
            }
            lastMessageAuthor = name;

            foreach (var attachment in attachments)
            {
                chatBox.ScriptErrorsSuppressed = true;
                if (attachment.Type.Contains("image"))
                {
                    htmlMiddle += $"<br><img src=\"{attachment.URL}\"></img>";
                }
                else if (attachment.Type.Contains("video"))
                {
                    htmlMiddle += $"<br><embed src=\"{attachment.URL}\" type=\"{attachment.Type}\" width=\"60%\" height=\"60%\">";
                }
            }

            foreach (var embed in embeds)
            {
                if (embed.Type == "rich")
                {
                    htmlMiddle += $"<br><div class=\"rich\"><a style=\"color: black\" href=\"{embed.AuthorURL}\">{embed.Author}</a><br><br><a href=\"{embed.TitleURL}\">{embed.Title}</a><br><br><p>{embed.Description}</p></div>";
                }
            }

            if (reload)
            {
                chatBox.DocumentText = htmlStart + htmlMiddle + htmlEnd;
            }
            if (scroll)
            {
                Task.Delay(100).ContinueWith(t => ScrollToBottom());
            }
        }

        private string DiscordMDToHtml(string md)
        {
            var waitingToClose = new Stack<string>();
            var html = new StringBuilder();

            for (int i = 0; i < md.Length; i++)
            {
                switch (md[i])
                {
                    case '*':
                        if (md.Length > i + 1 && md[i + 1] == '*')
                        {
                            ToggleHtmlTag(html, waitingToClose, "strong", "**");
                            i++;
                        }
                        else
                        {
                            ToggleHtmlTag(html, waitingToClose, "em", "*");
                        }
                        break;

                    case '_':
                        if (md.Length > i + 1 && md[i + 1] == '_')
                        {
                            ToggleHtmlTag(html, waitingToClose, "u", "__");
                            i++;
                        }
                        else
                        {
                            ToggleHtmlTag(html, waitingToClose, "em", "_");
                        }
                        break;

                    case '~':
                        if (md.Length > i + 1 && md[i + 1] == '~')
                        {
                            ToggleHtmlTag(html, waitingToClose, "strike", "~~");
                            i++;
                        }
                        break;

                    case '`':
                        if (md.Length > i + 1 && md[i + 1] == '`')
                        {
                            html.Append("<code>");
                            waitingToClose.Push("</code>");
                            i++;
                        }
                        else
                        {
                            html.Append("<code>");
                            waitingToClose.Push("</code>");
                        }
                        break;

                    case '>':
                        html.Append("<br>&gt;");
                        break;

                    default:
                        html.Append(md[i]);
                        break;
                }
            }

            while (waitingToClose.Count > 0)
            {
                html.Append(waitingToClose.Pop());
            }

            return html.ToString();
        }

        private void ToggleHtmlTag(StringBuilder html, Stack<string> waitingToClose, string tag, string markdown)
        {
            if (waitingToClose.Count > 0 && waitingToClose.Peek() == $"</{tag}>")
            {
                html.Append(waitingToClose.Pop());
            }
            else
            {
                html.Append($"<{tag}>");
                waitingToClose.Push($"</{tag}>");
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
                            Image image = Clipboard.GetImage();
                            byte[] imageBytes = ImageToBytes(image);
                            await UploadImage(imageBytes);
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

        public async Task<dynamic> GetApiResponse(string endpoint)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(DiscordApiBaseUrl) })
            {
                client.DefaultRequestHeaders.Add("Authorization", AccessToken);
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(content);
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ScrollToBottom()
        {
            try
            {
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

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }
    }
}