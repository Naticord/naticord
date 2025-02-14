#nullable enable
using System;
using System.Net.Http;
using System.Threading.Tasks;
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
                new CMessageBox("Login Successful", $"Token: {jsonResponse.token}").Show();
            }
            else if (jsonResponse.mfa == true && jsonResponse.ticket != null)
            {
                string ticket = jsonResponse.ticket;
                TwoFA twoFAForm = new TwoFA(ticket);
                twoFAForm.Show();
            }
            else
            {
                new CMessageBox("Login Failed", "Unexpected response from server. Please check your details and file a GitHub issue if it's still not working.").Show();
            }
        }
    }
}
