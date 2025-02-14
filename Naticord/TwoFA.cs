// Using cURL makes this look ugly, but it's the only solution that works.
// This was a mess to get working in the first place and at the end of the day I'm happy enough this works.
// I don't know what Discord has made it this complicated to make 2FA work, I'm guessing to make 3rd party client developers to stop?
// I have no idea. This works and is the only one viable **for now**

using System;
using System.Diagnostics;
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

        private void okButton_Click(object sender, EventArgs e)
        {
            string code = authenticationBox.Text;
            string jsonData = $"{{\"ticket\":\"{ticket}\",\"code\":\"{code}\"}}";
            string escapedJsonData = jsonData.Replace("\"", "\\\"");

            string arguments = $"https://discord.com/api/v9/auth/mfa/totp -X POST " +
                               "-H \"Content-Type: application/json\" " +
                               "-H \"User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0\" " +
                               "-H \"X-Super-Properties: eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJlbi1VUyIsImhhc19jbGllbnRfbW9rcyI6ZmFsc2UsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjEzNS4wKSBHZWNrby8yMDEwMDEwMSBGaXJlZm94LzEzNS4wIiwiYnJvd3Nlcl92ZXJzaW9uIjoiMTM1LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6MzY4NDY0LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==\" " +
                               $"--data-raw \"{escapedJsonData}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "curl",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process { StartInfo = psi };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            dynamic? jsonResponse = JsonConvert.DeserializeObject(output);
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
