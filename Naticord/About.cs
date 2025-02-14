using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Naticord
{
    public partial class About : Form
    {
        private string Version = "1.0 (beta 1)";
        public About()
        {
            InitializeComponent();
            SetVersion();
        }
        
        private void SetVersion()
        {
            versionLabel.Text = $"Version: {Version}";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Naticord/naticord",
                UseShellExecute = true
            });
        }

        private void donateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://kofi.com/patricktbp",
                UseShellExecute = true
            });
        }
    }
}
