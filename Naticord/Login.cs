using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Login: Form
    {
        private string email;
        private string password;

        public Login()
        {
            API api = new API();
            InitializeComponent();
            CheckIfInternetConnectionExists();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            email = emailBox.Text;
            password = passwordBox.Text;
            LoginToDiscord(email, password);
        }

        public async void LoginToDiscord(string email, string password)
        {
            var body = new
            {
                login = email,
                password = password,
                undelete = false
            };

            try
            {
                string response = await API.SendAPI(null, "auth/login", HttpMethod.Post, body);

                if (response.Contains("\"token\"")) // No 2FA
                {
                    var json = JObject.Parse(response);
                    string token = json["token"]?.ToString();

                    if (!string.IsNullOrEmpty(token))
                    {
                        Properties.Settings.Default.token = token;
                        Properties.Settings.Default.Save();
                    }

                    // Continues to the client
                    Client clientForm = new Client();
                    clientForm.Show();
                    this.Hide();
                }
                if (response.Contains("\"ticket\"")) // With 2FA
                {
                    var json = JObject.Parse(response);
                    string ticket = json["ticket"]?.ToString();

                    TwoFA twoFAForm = new TwoFA(ticket);
                    twoFAForm.Show();
                }
                else // Wrong credentials or Discord is down
                {
                    new CMessageBox("Couldn't login", "You may have entered the wrong credentials or Discord's API is down. Please check Discord's status and your details.").Show();
                }
            }
            catch (Exception ex)
            {
                new CMessageBox("An error has occured", $"An error trying to send data to the Discord API has occured. Please report this to the GitHub. {ex.Message}");
            }
        }

        private void CheckIfInternetConnectionExists()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 3000);
                    if (reply.Status == IPStatus.Success)
                    {
                        // Do nothing if a connection is active
                        return;
                    }
                }
            }
            catch
            {
                // Ignore exceptions (no network)
            }

            new CMessageBox("No internet connection was found", "We have failed to find an internet connection. The client will load, but may not function as intended.");
        }

        private void CheckTokenAutoLogin()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.token))
            {
                this.BeginInvoke(new Action(() =>
                {
                    Client clientForm = new Client();
                    clientForm.Show();
                    this.Hide();
                }));
            }
            else
            {
                // Continue execution (Nothing to be done)
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CheckTokenAutoLogin();
        }
    }
}