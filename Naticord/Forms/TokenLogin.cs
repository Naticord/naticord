using System;
using System.Windows.Forms;

namespace Naticord
{
    public partial class TokenLogin : Form
    {
        Login signin;

        public TokenLogin(Login signinArg)
        {
            InitializeComponent();
            signin = signinArg;
        }

        private void signinButton_Click(object sender, EventArgs e)
        {
            string accessToken = tokenBox.Text.Trim();

            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Please enter your Discord access token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.token = accessToken;
            Properties.Settings.Default.Save();

            signin.PerformLogin(accessToken, false);
            this.Hide();
        }

        private void SetIEVer()
        {
            int BrowserVer, RegVal;

            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            if (BrowserVer >= 11)
                RegVal = 11001;
            else if (BrowserVer == 10)
                RegVal = 10001;
            else if (BrowserVer == 9)
                RegVal = 9999;
            else if (BrowserVer == 8)
                RegVal = 8888;
            else
                RegVal = 7000;

            using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree))
                if (Key.GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe") == null)
                    Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, Microsoft.Win32.RegistryValueKind.DWord);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/jukfiuune/aerocord/blob/main/README.md#how-to-login");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            signin.Close();
        }
    }
}
