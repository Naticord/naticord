using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    public partial class Login : Form
    {
        private const string TokenFileName = "token.txt";

        public Login()
        {
            InitializeComponent();
            CheckToken();
        }

        private void emailPasswordLoginButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PerformEmailPasswordLogin(email, password);
        }

        private void PerformLogin(string accessToken, bool isAutomated = false)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = accessToken;

                    string userProfileJson = webClient.DownloadString("https://discord.com/api/v9/users/@me");

                    if (!isAutomated) SaveToken(accessToken);

                    OpenNaticordForm(accessToken);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Failed to login. Please enter a valid token! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEmailPasswordLogin(string email, string password)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var loginPayload = new
                    {
                        email = email,
                        password = password
                    };

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";

                    string response = webClient.UploadString("https://discord.com/api/v9/auth/login", Newtonsoft.Json.JsonConvert.SerializeObject(loginPayload));
                    var json = JObject.Parse(response);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        SaveToken(accessToken);
                        OpenNaticordForm(accessToken);
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

        private void Perform2FALogin(string ticket, string code)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var mfaPayload = new
                    {
                        code = code,
                        ticket = ticket,
                        login_source = (string)null,
                        gift_code_sku_id = (string)null
                    };

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";

                    string response = webClient.UploadString("https://discord.com/api/v9/auth/mfa/totp", Newtonsoft.Json.JsonConvert.SerializeObject(mfaPayload));
                    var json = JObject.Parse(response);

                    if (json["token"] != null)
                    {
                        string accessToken = json["token"].ToString();
                        SaveToken(accessToken);
                        OpenNaticordForm(accessToken);
                    }
                    else
                    {
                        MessageBox.Show("2FA failed. Please check your 2FA code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException ex)
            {
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

        private void ConfigureFormControls(Form form, Label label, TextBox inputBox, Button buttonOk)
        {
            form.Text = "2FA Required";
            form.StartPosition = FormStartPosition.CenterParent;
            form.Width = 300;
            form.Height = 150;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
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

        private void CheckToken()
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), TokenFileName);

                if (File.Exists(filePath))
                {
                    foreach (string line in File.ReadLines(filePath))
                    {
                        if (line.Contains("token="))
                        {
                            PerformLogin(line.Replace("token=", ""), true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToken(string accessToken)
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), TokenFileName);
                File.WriteAllText(filePath, "token=" + accessToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenNaticordForm(string accessToken)
        {
            Naticord naticordForm = new Naticord(this, accessToken);
            naticordForm.Show();
        }
    }
}
