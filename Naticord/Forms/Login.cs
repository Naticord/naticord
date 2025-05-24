using Naticord.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Naticord.Forms
{
    public partial class Login : Form
    {
        private API dcAPI;
        private bool isClassicMode = !Application.RenderWithVisualStyles || !VisualStyleInformation.IsEnabledByUser;
        private bool showPass = false;
        private string token;
        private string passwordText;
        private string emailText;

        public Login()
        {
            token = Properties.Settings.Default.token;
            dcAPI = new API();
            InitializeComponent();
            this.AcceptButton = loginButton;
            if (isClassicMode)
            {
                // Do nothing if Classic is enabled, make it's look ugly.
            }
            else
            {
                loginButton.BackColor = Color.Transparent;
            }

            this.Shown += async (s, e) =>
            {
                await Task.Run(() => CheckIfLoggedIn());
            };
        }

        private void naticordBanner_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made with <3 by patricktbp!", "Easter egg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            emailText = emailBox.Text;
            passwordText = passwordBox.Text;

            if (string.IsNullOrWhiteSpace(emailText) && string.IsNullOrWhiteSpace(passwordText))
            {
                Debug.WriteLine("[DEBUG] User entered nothing in the text boxes.");
                MessageBox.Show("Please enter your email and password to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                await SendLogin(emailText, passwordText);
            }
        }

        private async Task SendLogin(string email, string password)
        {
            var loginBody = new
            {
                login = email,
                password = password,
                undelete = false
            };

            Debug.WriteLine("[DEBUG] SendLogin called");
            string loginResponse = await dcAPI.SendAPI("auth/login", HttpMethod.Post, token, loginBody);

            if (loginResponse.Contains("\"token\""))
            {
                var json = JObject.Parse(loginResponse);
                string token = json["token"]?.ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    Properties.Settings.Default.token = token;
                    Properties.Settings.Default.Save();
                }

                Debug.WriteLine("[DEBUG] Token value written to setting, continuing to client. (No authentication)");
                Client clientForm = new Client();
                clientForm.Show();
                this.Hide();
            }
            else if (loginResponse.Contains("\"ticket\"")) // With 2FA
            {
                Debug.WriteLine("[DEBUG] Ticket detected, 2FA required. Passing off to 2FA dialog.");
                var json = JObject.Parse(loginResponse);
                string ticket = json["ticket"]?.ToString();

                TwoFA twoFAForm = new TwoFA(ticket, this);
                twoFAForm.Show();
            }
            else
            {
                Debug.WriteLine("[DEBUG] Failed to login.");
                MessageBox.Show("We've hit a ditch and we couldn't login for you. Try re-entering your email and password. If that doesn't work, check if Discord is down right now.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tokenLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Token tokenForm = new Token(this);
            tokenForm.Show();
        }

        private void CheckIfLoggedIn()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.token))
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(CheckIfLoggedIn));
                    return;
                }

                Client clientForm = new Client();
                clientForm.Show();
                this.Hide();
            }
            else
            {
                // Do nothing
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (showPass == false)
            {
                ShowPassword();
                showPass = true;
            }
            else
            {
                HidePassword();
                showPass = false;
            }
        }

        private void ShowPassword()
        {
            passwordBox.UseSystemPasswordChar = false;
            showButton.Text = "Hide";
        }

        private void HidePassword()
        {
            passwordBox.UseSystemPasswordChar = true;
            showButton.Text = "Show";
        }
    }
}