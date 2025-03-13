#nullable enable
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Naticord
{
    public partial class TwoFA : Form
    {
        private readonly string ticket;

        public TwoFA(string ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
            Debug.WriteLine(ticket);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string code = authenticationBox.Text;
            string jsonData = JsonConvert.SerializeObject(new { ticket, code });

            string headers = string.Join(" ",
                "-H \"Content-Type: application/json\"",
                "-H \"User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0\"",
                "-H \"X-Super-Properties: eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJlbi1VUyIsImhhc19jbGllbnRfbW9rcyI6ZmFsc2UsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjEzNS4wKSBHZWNrby8yMDEwMDEwMSBGaXJlZm94LzEzNS4wIiwiYnJvd3Nlcl92ZXJzaW9uIjoiMTM1LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6MzY4NDY0LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==\""
            );

            string arguments = string.Format(
                "{0} -X POST {1} --data-raw \"{2}\"",
                "https://discord.com/api/v9/auth/mfa/totp",
                headers,
                jsonData.Replace("\"", "\\\"")
            );

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

            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.WriteLine("cURL Error: " + error);
            }

            dynamic? jsonResponse = JsonConvert.DeserializeObject(output);
            if (jsonResponse?.token != null)
            {
                Properties.Settings.Default.token = jsonResponse.token.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
            else
            {
                new CMessageBox("Authentication failed", "An invalid authentication code has been entered, or Discord has broke the client. If you are sure you entered the right code, please report this to the GitHub.").Show();
            }
        }
    }
}
