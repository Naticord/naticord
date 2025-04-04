using Naticord.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Settings : Form
    {
        private string iconStyle = Properties.Settings.Default.iconStyle;
        private bool isInitializing = true;
        private Client clientForm;

        public Settings(Client clientFormStg)
        {
            clientForm = clientFormStg;
            InitializeComponent();
            ReadSettings();
            SetOSVerEnabledValue();
        }

        private void ReadSettings()
        {
            isInitializing = true;

            osVerBox.SelectedItem = OSVersionHelper.GetWindowsVersion();
            var settings = Properties.Settings.Default;

            if (iconStyle == "Legacy")
            {
                appearanceIcon.Image = Properties.Resources.appearance;
                creditsIcon.Image = Properties.Resources.credits;
            }
            else if (iconStyle == "Modern")
            {
                appearanceIcon.Image = Properties.Resources.appearance_modern;
                creditsIcon.Image = Properties.Resources.credits_modern;
            }

            if (osVerBox.Items.Contains(settings.spoofedOS))
                osVerBox.SelectedItem = settings.spoofedOS;

            if (bdStyleBox.Items.Contains(settings.renderMode))
                bdStyleBox.SelectedItem = settings.renderMode;

            if (bdrStyleBox.Items.Contains(settings.borderStyle))
                bdrStyleBox.SelectedItem = settings.borderStyle;

            if (icnStyleBox.Items.Contains(settings.iconStyle))
                icnStyleBox.SelectedItem = settings.iconStyle;

            isInitializing = false;
        }

        private void icnStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            var selected = icnStyleBox.SelectedItem?.ToString();
            var settings = Properties.Settings.Default;

            if (selected == "Legacy")
            {
                clientForm.settingsButton.ButtonIcon = Properties.Resources.settings;
                clientForm.accountButton.ButtonIcon = Properties.Resources.account;
                clientForm.ghButton.ButtonIcon = Properties.Resources.github;
                appearanceIcon.Image = Properties.Resources.appearance;
                creditsIcon.Image = Properties.Resources.credits;

                settings.iconStyle = "Legacy";
            }
            else
            {
                clientForm.settingsButton.ButtonIcon = Properties.Resources.settings_modern;
                clientForm.accountButton.ButtonIcon = Properties.Resources.account_modern;
                clientForm.ghButton.ButtonIcon = Properties.Resources.github_modern;
                appearanceIcon.Image = Properties.Resources.appearance_modern;
                creditsIcon.Image = Properties.Resources.credits_modern;

                settings.iconStyle = "Modern";
            }

            settings.Save();
        }

        private void bdrStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            var selected = bdrStyleBox.SelectedItem?.ToString();
            var settings = Properties.Settings.Default;

            if (selected == "Thick")
            {
                clientForm.ChangeElementPos();
                settings.borderStyle = "Thick";
            }
            else
            {
                clientForm.usernameLabel.Location = new Point(777, 5);
                clientForm.profilePictureUser.Location = new Point(981, 4);
                clientForm.buttonPanel.Location = new Point(8, 2);

                settings.borderStyle = "Slim";
            }

            settings.Save();
        }

        private void bdStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            var selected = bdStyleBox.SelectedItem?.ToString();
            var settings = Properties.Settings.Default;

            if (selected == "Aero")
            {
                settings.renderMode = "Aero";
            }
            else if (selected == "Acrylic")
            {
                settings.renderMode = "Acrylic";
            }
            else if (selected == "Mica")
            {
                settings.renderMode = "Mica";
            }
            else if (selected == "Mica (Alt)")
            {
                settings.renderMode = "Mica (Alt)";
            }

            settings.Save();
            DialogResult result = MessageBox.Show(
                "You'll need to restart the app to apply these changes. Would you like to restart now?",
                "Restart Naticord",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;

            settings.runDefaults = false;
            settings.renderMode = "Mica";
            settings.iconStyle = "Modern";
            settings.borderStyle = "Slim";
            settings.Save();

            DialogResult result = MessageBox.Show(
                "You'll need to restart the app to apply these changes. Would you like to restart now?",
                "Restart Naticord",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void SetOSVerEnabledValue()
        {
            var selected = osVerBox.SelectedItem?.ToString().Trim();
            bool enableControls = selected == "Custom";

            icnStyleBox.Enabled = enableControls;
            bdrStyleBox.Enabled = enableControls;
            bdStyleBox.Enabled = enableControls;
            icnStyleLabel.Enabled = enableControls;
            bdrStyleLabel.Enabled = enableControls;
            bdStyleLabel.Enabled = enableControls;
        }

        private void osVerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            var selected = osVerBox.SelectedItem?.ToString().Trim();
            var settings = Properties.Settings.Default;

            switch (selected)
            {
                case "Windows 11":
                    settings.spoofedOS = "Windows 11";
                    settings.renderMode = "Mica";
                    settings.iconStyle = "Modern";
                    settings.borderStyle = "Slim";
                    break;

                case "Windows 10":
                    settings.spoofedOS = "Windows 10";
                    settings.renderMode = "Acrylic";
                    settings.iconStyle = "Modern";
                    settings.borderStyle = "Slim";
                    break;

                case "Windows 7 - 8.1":
                    settings.spoofedOS = "Windows 7 - 8.1";
                    settings.renderMode = "Aero";
                    settings.iconStyle = "Legacy";
                    settings.borderStyle = "Thick";
                    break;

                case "Custom":
                    settings.spoofedOS = "Custom";
                    break;

                default:
                    break;
            }

            settings.Save();

            var result = MessageBox.Show(
                "You'll need to restart the app to apply these changes. Would you like to restart now?",
                "Restart Naticord",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
    }
}