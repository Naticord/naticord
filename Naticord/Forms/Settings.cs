using Naticord.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Settings : Form
    {
        private string iconStyle = Properties.Settings.Default.iconStyle;
        private Client clientForm;

        public Settings(Client clientFormStg)
        {
            clientForm = clientFormStg;
            InitializeComponent();
            ReadSettings();
        }

        private void ReadSettings()
        {
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

            if (bdStyleBox.Items.Contains(settings.renderMode))
                bdStyleBox.SelectedItem = settings.renderMode;

            if (bdrStyleBox.Items.Contains(settings.borderStyle))
                bdrStyleBox.SelectedItem = settings.borderStyle;

            if (icnStyleBox.Items.Contains(settings.iconStyle))
                icnStyleBox.SelectedItem = settings.iconStyle;
        }

        private void icnStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }
    }
}