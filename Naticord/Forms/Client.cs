using Naticord.Classes;
using Naticord.Controls;
using System.Diagnostics;
using System.Drawing;

namespace Naticord.Forms
{
    public partial class Client : GlassForm
    {
        public Client()
        {
            Debug.WriteLine("[DEBUG] Client started");
            InitializeComponent();
            ChangeElementsBasedOnVersion();
            CenterToScreen();
        }

        private void ChangeElementsBasedOnVersion()
        {
            string osVersion = OSVersionHelper.GetWindowsVersion();
            Point defaultUsernameLocation = new Point(785, 5);
            Point defaultProfilePictureLocation = new Point(989, 4);
            Point defaultButtonPanelLocation = new Point(0, 2);

            switch (osVersion)
            {
                case "Windows 10":
                case "Windows 11":
                    break;

                case "Windows 7":
                case "Windows 8":
                case "Windows 8.1":
                case "Unknown":
                    usernameLabel.Location = defaultUsernameLocation;
                    profilePictureUser.Location = defaultProfilePictureLocation;
                    buttonPanel.Location = defaultButtonPanelLocation;
                    break;

                default:
                    usernameLabel.Location = defaultUsernameLocation;
                    profilePictureUser.Location = defaultProfilePictureLocation;
                    buttonPanel.Location = defaultButtonPanelLocation;
                    break;
            }
        }

        private void Client_Load(object sender, System.EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }
    }
}