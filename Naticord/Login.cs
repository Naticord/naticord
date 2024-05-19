using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

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

        private void loginButton_Click(object sender, EventArgs e)
        {
            string accessToken = accessTokenTextBox.Text.Trim();

            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Please enter your Discord access token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PerformLogin(accessToken);
        }

        private void PerformLogin(string accessToken, bool isAutomated = false)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Authorization] = accessToken;

                try
                {
                    string userProfileJson = webClient.DownloadString("https://discord.com/api/v9/users/@me");

                    if(!isAutomated) SaveToken(accessToken);

                    Naticord naticordForm = new Naticord(this, accessToken);
                    naticordForm.Show();
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Failed to login. Please enter a valid token! Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckToken()
        {
            try
            {

                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string filePath = Path.Combine(homeDirectory, TokenFileName);

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
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string filePath = Path.Combine(homeDirectory, TokenFileName);

                File.WriteAllText(filePath, "token=" + accessToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/n1d3v/naticord#how-to-login");
        }
    }
}
