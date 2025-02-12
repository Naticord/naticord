using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Naticord
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string token = tokenBox.Text;

            if (string.IsNullOrWhiteSpace(token))
            {
                CMessageBox errorMessageBox = new CMessageBox("Authentication error", "Please enter your Discord token in the text box.");
                errorMessageBox.ShowDialog();
                return;
            }

            try
            {
                string response = await API.SendAPI(token, "users/@me", HttpMethod.Get);
                JObject jsonResponse = JObject.Parse(response);

                if (jsonResponse.ContainsKey("id"))
                {
                    string userId = jsonResponse["id"].ToString();
                    Debug.WriteLine(userId);

                    CMessageBox successMessageBox = new CMessageBox("Authentication successful", $"User ID: {userId}");
                    successMessageBox.ShowDialog();
                }
                else
                {
                    CMessageBox errorMessageBox = new CMessageBox("Authentication error", "Login failed. Please check if your token is correct.");
                    errorMessageBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CMessageBox errorMessageBox = new CMessageBox("Error", $"Error: {ex.Message}");
                errorMessageBox.ShowDialog();
            }
        }

        private void tokenLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Naticord/naticord/wiki/How-can-I-get-my-token%3F",
                UseShellExecute = true
            });
        }
    }
}
