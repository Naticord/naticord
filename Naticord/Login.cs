#nullable enable
using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Naticord
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            SetUpAPI();
        }

        private void CheckTokenAutoLogin()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.token))
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    Client naticordMain = new Client();
                    naticordMain.Show();
                }));
            }
            else
            {
                // Continue execution (Nothing to be done)
            }
        }


        private async void SetUpAPI()
        {
            await API.InitializeFingerprint();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string email = emailBox.Text;
            string password = passwordBox.Text;

            var data = new { login = email, password = password };
            string response = await API.SendAPI(null, "auth/login", HttpMethod.Post, data);

            dynamic? jsonResponse = JsonConvert.DeserializeObject(response);
            if (jsonResponse == null)
            {
                new CMessageBox("Login Failed", "Invalid response from server. Please check your details and file a GitHub issue if it's still not working.").Show();
                return;
            }

            if (jsonResponse.token != null)
            {
                Properties.Settings.Default.token = jsonResponse.token.ToString();
                Properties.Settings.Default.Save();

                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    Client naticordMain = new Client();
                    naticordMain.Show();
                }));
            }
            else if (jsonResponse.mfa == true && jsonResponse.ticket != null)
            {
                string ticket = jsonResponse.ticket;

                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    TwoFA twoFAForm = new TwoFA(ticket);
                    twoFAForm.Show();
                }));
            }
            else
            {
                new CMessageBox("Login Failed", "Unexpected response from server. Please check your details and file a GitHub issue if it's still not working.").Show();
            }
        }


        private void Login_Load(object sender, EventArgs e)
        {
            // Just a simple check.
            CheckTokenAutoLogin();
        }
    }
}
