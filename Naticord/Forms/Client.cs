using Naticord.Classes;
using Naticord.Controls;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Client : GlassForm
    {
        private readonly string iconStyle = Properties.Settings.Default.iconStyle;
        private readonly string borderStyle = Properties.Settings.Default.borderStyle;

        public Client()
        {
            Debug.WriteLine("[DEBUG] Client started");
            InitializeComponent();

            DecideDefaults();
            SetUpToolbarButtons();

            this.FormClosing += (s, e) => Application.Exit();
            this.Shown += (s, e) => ApplySavedSettings();

            CenterToScreen();
        }

        private void ApplySavedSettings()
        {
            if (iconStyle == "Modern")
            {
                settingsButton.ButtonIcon = Properties.Resources.settings_modern;
                accountButton.ButtonIcon = Properties.Resources.account_modern;
                ghButton.ButtonIcon = Properties.Resources.github_modern;
            }

            if (borderStyle == "Thick")
                ChangeElementPos();
        }

        public void ChangeElementPos()
        {
            usernameLabel.Location = new Point(785, 5);
            profilePictureUser.Location = new Point(989, 4);
            buttonPanel.Location = new Point(0, 2);
        }

        private void DecideDefaults()
        {
            if (Properties.Settings.Default.runDefaults)
                return;

            switch (OSVersionHelper.GetWindowsVersion())
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

        private void SetUpToolbarButtons()
        {
            settingsButton.ButtonClick += (s, e) =>
            {
                new Settings(this).Show();
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