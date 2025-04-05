using Naticord.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Settings : Form
    {
        private readonly Client clientForm;
        private bool isInitializing = true;

        public Settings(Client clientFormStg)
        {
            clientForm = clientFormStg;
            InitializeComponent();
            ReadSettings();
            SetOSVerEnabledValue();

            appearanceIcon.Paint += (s, e) => ApplyAntiAliasing(s as Control, e);
            appIcon.Paint += (s, e) => ApplyAntiAliasing(s as Control, e);
            creditsIcon.Paint += (s, e) => ApplyAntiAliasing(s as Control, e);
        }

        private void ReadSettings()
        {
            isInitializing = true;

            var settings = Properties.Settings.Default;

            osVerBox.SelectedItem = OSVersionHelper.GetWindowsVersion();

            switch (settings.iconStyle)
            {
                case "Legacy":
                    appearanceIcon.Image = Properties.Resources.appearance;
                    creditsIcon.Image = Properties.Resources.credits;
                    break;
                case "Modern":
                    appearanceIcon.Image = Properties.Resources.appearance_modern;
                    creditsIcon.Image = Properties.Resources.credits_modern;
                    break;
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
            }
            else
            {
                clientForm.settingsButton.ButtonIcon = Properties.Resources.settings_modern;
                clientForm.accountButton.ButtonIcon = Properties.Resources.account_modern;
                clientForm.ghButton.ButtonIcon = Properties.Resources.github_modern;
                appearanceIcon.Image = Properties.Resources.appearance_modern;
                creditsIcon.Image = Properties.Resources.credits_modern;
            }

            settings.iconStyle = selected;
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
            }
            else
            {
                clientForm.usernameLabel.Location = new Point(777, 5);
                clientForm.profilePictureUser.Location = new Point(981, 4);
                clientForm.buttonPanel.Location = new Point(8, 2);
            }

            settings.borderStyle = selected;
            settings.Save();
        }

        private void bdStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            var selected = bdStyleBox.SelectedItem?.ToString();
            var settings = Properties.Settings.Default;

            settings.renderMode = selected;
            settings.Save();

            PromptRestart();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;

            settings.runDefaults = false;
            settings.spoofedOS = null;
            settings.renderMode = "Mica";
            settings.iconStyle = "Modern";
            settings.borderStyle = "Slim";
            settings.Save();

            PromptRestart();
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
            }

            settings.Save();
            PromptRestart();
        }

        private void SetOSVerEnabledValue()
        {
            bool enable = osVerBox.SelectedItem?.ToString().Trim() == "Custom";

            icnStyleBox.Enabled = enable;
            bdrStyleBox.Enabled = enable;
            bdStyleBox.Enabled = enable;
            icnStyleLabel.Enabled = enable;
            bdrStyleLabel.Enabled = enable;
            bdStyleLabel.Enabled = enable;
        }

        private void PromptRestart()
        {
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

        public static void ApplyAntiAliasing(Control control, PaintEventArgs e)
        {
            if (control is PictureBox pictureBox && pictureBox.Image != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                e.Graphics.DrawImage(pictureBox.Image, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            }
        }
    }
}