using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Naticord
{
    public partial class TwoFA : Form
    {
        private string ticket;

        public TwoFA(string ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
            Debug.WriteLine(ticket);
        }

        private async void okButton_Click(object sender, EventArgs e)
        {
            string code = authenticationBox.Text;

            var data = new { code = code, ticket = ticket };
            string response = await API.SendAPI(null, "auth/mfa/totp", HttpMethod.Post, data);

            dynamic? jsonResponse = JsonConvert.DeserializeObject(response);
            if (jsonResponse == null)
            {
                new CMessageBox("MFA Failed", "Invalid response from server.").Show();
                return;
            }

            if (jsonResponse.token != null)
            {
                new CMessageBox("MFA Successful", $"Token: {jsonResponse.token}").Show();
                this.Close();
            }
            else
            {
                new CMessageBox("MFA Failed", "Invalid authentication code.").Show();
            }
        }
    }
}
