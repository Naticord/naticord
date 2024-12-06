using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    public partial class Login : Form
    {
        private const string TokenFileName = "token.txt";
        private string accessToken = Properties.Settings.Default.token;
        private string proxyAddress = Properties.Settings.Default.proxy;

        public Login()
        {
            InitializeComponent();

            const string keyName = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            using (var key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                key.SetValue("Naticord.exe", 11001, RegistryValueKind.DWord);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Environment.OSVersion.Version.Major <= 6 && Environment.OSVersion.Version.Minor <= 0)
            {
                // Do nothing for older OS versions
            }
            else
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            }

            CheckToken();
        }

        private void signinButton_Click(object sender, EventArgs e)
        {
            string email = emailBox.Text.Trim();
            string password = passBox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PerformLoginWithPass(email, password);
        }

        private void PerformLoginWithPass(string email, string password)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    if (!string.IsNullOrEmpty(proxyAddress))
                    {
                        webClient.Proxy = new WebProxy(proxyAddress);
                    }

                    var loginPayload = new
                    {
                        gift_code_sku_id = (string)null,
                        login = email,
                        login_source = (string)null, 
                        password = password,
                        undelete = false
                    };

                    Debug.WriteLine("Login Payload: " + Newtonsoft.Json.JsonConvert.SerializeObject(loginPayload));
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string response = webClient.UploadString("https://discord.com/api/v9/auth/login", Newtonsoft.Json.JsonConvert.SerializeObject(loginPayload));
                    var json = JObject.Parse(response);

                    Debug.WriteLine("Login Response: " + response);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        PerformLogin(accessToken, false);
                    }
                    else if (json["mfa"] != null && (bool)json["mfa"])
                    {
                        string ticket = json["ticket"].ToString();
                        string code = PromptFor2FACode();
                        Perform2FALogin(ticket, code);
                    }
                    else
                    {
                        MessageBox.Show("Failed to login. Please enter a valid token!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Failed to login. Please check your email and password! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PerformLogin(string accessToken, bool isAutomated)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            using (var webClient = new WebClient())
            {
                if (!string.IsNullOrEmpty(proxyAddress))
                {
                    webClient.Proxy = new WebProxy(proxyAddress);
                }

                webClient.Headers[HttpRequestHeader.Authorization] = accessToken;

                try
                {
                    Debug.WriteLine("Requesting user profile with token: " + accessToken);
                    string userProfileJson = webClient.DownloadString("https://discord.com/api/v9/users/@me");
                    Debug.WriteLine("User Profile Response: " + userProfileJson);

                    if (!isAutomated) SaveToken(accessToken);

                    Naticord mainForm = new Naticord(accessToken, this);
                    mainForm.Show();
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Failed to login. Please enter a valid token! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Perform2FALogin(string ticket, string code)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    if (!string.IsNullOrEmpty(proxyAddress))
                    {
                        webClient.Proxy = new WebProxy(proxyAddress);
                    }

                    var mfaPayload = new
                    {
                        code = code,
                        gift_code_sku_id = (string)null,
                        login_source = (string)null,
                        ticket = ticket
                    };

                    Debug.WriteLine("2FA Payload: " + Newtonsoft.Json.JsonConvert.SerializeObject(mfaPayload));
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string jsonResponse = webClient.UploadString("https://discord.com/api/v9/auth/mfa/totp", Newtonsoft.Json.JsonConvert.SerializeObject(mfaPayload));
                    var json = JObject.Parse(jsonResponse);

                    Debug.WriteLine("2FA Response: " + jsonResponse);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        PerformLogin(accessToken, false);
                    }
                    else
                    {
                        Debug.WriteLine("2FA failed. Response: " + json["message"]?.ToString());
                        Debug.WriteLine("Full Response JSON: " + jsonResponse);

                        MessageBox.Show("2FA failed. Please check your 2FA code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string responseBody = reader.ReadToEnd();
                        Debug.WriteLine("WebException Response Body: " + responseBody);
                    }
                }

                Debug.WriteLine("2FA failed. Exception Message: " + ex.Message);
                MessageBox.Show("2FA failed. Please check your 2FA code! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PromptFor2FACode()
        {
            using (var form = new Form())
            using (var label = new Label() { Text = "Enter your 2FA code:" })
            using (var inputBox = new TextBox())
            using (var buttonOk = new Button() { Text = "OK", DialogResult = DialogResult.OK })
            {
                ConfigureFormControls(form, label, inputBox, buttonOk);

                return form.ShowDialog() == DialogResult.OK ? inputBox.Text.Trim() : null;
            }
        }

        private void SaveToken(string accessToken)
        {
            try
            {
                Properties.Settings.Default.token = accessToken;
                Properties.Settings.Default.Save();

                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string filePath = Path.Combine(homeDirectory, TokenFileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckToken()
        {
            try
            {
                string accessToken = Properties.Settings.Default.token;

                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string filePath = Path.Combine(homeDirectory, TokenFileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (!string.IsNullOrEmpty(accessToken))
                {
                    PerformLogin(accessToken, true);
                }
                else
                {
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to check token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureFormControls(Form form, Label label, TextBox inputBox, Button buttonOk)
        {
            form.Text = "2FA Required";
            form.StartPosition = FormStartPosition.CenterParent;
            form.Width = 300;
            form.Height = 150;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            label.SetBounds(10, 10, 280, 20);
            inputBox.SetBounds(10, 40, 260, 20);
            buttonOk.SetBounds(110, 70, 75, 25);

            form.Controls.Add(label);
            form.Controls.Add(inputBox);
            form.Controls.Add(buttonOk);
            inputBox.Select();
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/n1d3v/naticord");
        }

        private void discordStatusLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discordstatus.com");
        }

        private void tokenLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TokenLogin token = new TokenLogin(this);
            token.Show();
        }

        private void proxyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            using (var form = new Form())
            using (var label = new Label() { Text = "Enter Proxy Address:" })
            using (var inputBox = new TextBox())
            using (var buttonOk = new Button() { Text = "OK", DialogResult = DialogResult.OK })
            {
                inputBox.Text = proxyAddress;
                ConfigureFormControls(form, label, inputBox, buttonOk);
                form.Text = "Proxy";
                form.FormBorderStyle = FormBorderStyle.Sizable;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.proxy = inputBox.Text.Trim();
                    Properties.Settings.Default.Save();
                    proxyAddress = Properties.Settings.Default.proxy;
                }
            }
        }
    }
}
