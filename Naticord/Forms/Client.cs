using Naticord.Classes;
using Naticord.Controls;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Client : GlassForm
    {
        private string iconStyle = Properties.Settings.Default["iconStyle"]?.ToString() ?? "Unknown";
        private string borderStyle = Properties.Settings.Default["borderStyle"]?.ToString() ?? "Unknown";

        public Client()
        {
            Debug.WriteLine("[DEBUG] Client started");
            InitializeComponent();

            // Set up the app
            DecideDefaults();
            SetUpTBB();
            this.FormClosing += (sender, e) => { Application.Exit(); };

            // Load the UI
            this.Shown += (s, e) => ReadDefaults();
            CenterToScreen();
        }

        private void ReadDefaults()
        {
            if (iconStyle == "Legacy")
            {
                // Do nothing (Already set)
            }
            else if (iconStyle == "Modern")
            {
                settingsButton.ButtonIcon = Properties.Resources.settings_modern;
                accountButton.ButtonIcon = Properties.Resources.account_modern;
                ghButton.ButtonIcon = Properties.Resources.github_modern;
            }
            if (borderStyle == "Slim")
            {
                // Do nothing (Already set)
            }
            else if (borderStyle == "Thick")
            {
                ChangeElementPos();
            }
        }

        public void ChangeElementPos()
        {
            Point defaultUsernameLocation = new Point(785, 5);
            Point defaultProfilePictureLocation = new Point(989, 4);
            Point defaultButtonPanelLocation = new Point(0, 2);

            usernameLabel.Location = defaultUsernameLocation;
            profilePictureUser.Location = defaultProfilePictureLocation;
            buttonPanel.Location = defaultButtonPanelLocation;
        }

        private void DecideDefaults()
        {
            if (Properties.Settings.Default.runDefaults)
                return;

            string osVersion = OSVersionHelper.GetWindowsVersion();
            switch (osVersion)
            {
                case "Windows 11":
                    Properties.Settings.Default.renderMode = "Mica";
                    Properties.Settings.Default.iconStyle = "Modern";
                    Properties.Settings.Default.borderStyle = "Slim";
                    break;

                case "Windows 10":
                    Properties.Settings.Default.renderMode = "Acrylic";
                    Properties.Settings.Default.iconStyle = "Modern";
                    Properties.Settings.Default.borderStyle = "Slim";
                    break;

                case "Windows 7 - 8.1":
                    Properties.Settings.Default.renderMode = "Aero";
                    Properties.Settings.Default.iconStyle = "Legacy";
                    Properties.Settings.Default.borderStyle = "Thick";
                    break;
            }

            Properties.Settings.Default.runDefaults = true;
            Properties.Settings.Default.Save();
        }

        private void SetUpTBB()
        {
            settingsButton.ButtonClick += (s, e) =>
            {
                Settings settingsForm = new Settings(this);
                settingsForm.Show();
            };
            accountButton.ButtonClick += (s, e) =>
            {
                // TODO
            };
            ghButton.ButtonClick += (s, e) =>
            {
                // TODO
            };
        }
    }
}