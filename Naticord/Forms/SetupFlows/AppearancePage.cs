using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms.SetupFlows
{
    public partial class AppearancePage : Form
    {
        private SetupFlowPiece _setupFlowPiece;

        private string WINDOWS_11 = "Windows 11 - 10";
        private string WINDOWS_8DOT1 = "Windows 8.1 - 7";

        public AppearancePage(SetupFlowPiece setupFlowPiece)
        {
            InitializeComponent();
            _setupFlowPiece = setupFlowPiece;

            // Set labels to be the correct font, and not Tahoma!
            icnPreviewLabel.Font = SystemFonts.MessageBoxFont;
            layoutNoticeLabel.Font = SystemFonts.MessageBoxFont;

            layoutBox.SelectedItem = Properties.Settings.Default.layoutMode;

            if (Properties.Settings.Default.layoutMode == WINDOWS_11)
            {
                skeuoRadio.Checked = false;
                modernRadio.Checked = true;
            }
        }

        private void skeuoRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (skeuoRadio.Checked == true)
            {
                Properties.Settings.Default.iconStyle = "Skeuomorphic";

                // Change all of the picture boxes to the right style
                accountIcon.Image = Properties.Resources.account_skeuo;
                settingsIcon.Image = Properties.Resources.cog_skeuo;
                githubIcon.Image = Properties.Resources.github_skeuo;
                dmIcon.Image = Properties.Resources.dm_skeuo;
                serversIcon.Image = Properties.Resources.servers_skeuo;

                // Set the SetupFlowPiece button icons to match the style
                _setupFlowPiece.backButton.ButtonIcon = Properties.Resources.arrow_back_skeuo;
                _setupFlowPiece.nextButton.ButtonIcon = Properties.Resources.arrow_forward_skeuo;

                Properties.Settings.Default.Save();
            }
        }

        private void modernRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modernRadio.Checked == true)
            {
                Properties.Settings.Default.iconStyle = "Modern";

                // Change all of the picture boxes to the right style
                accountIcon.Image = Properties.Resources.account;
                settingsIcon.Image = Properties.Resources.cog;
                githubIcon.Image = Properties.Resources.github;
                dmIcon.Image = Properties.Resources.dm;
                serversIcon.Image = Properties.Resources.servers;

                // Set the SetupFlowPiece button icons to match the style
                _setupFlowPiece.backButton.ButtonIcon = Properties.Resources.arrow_back;
                _setupFlowPiece.nextButton.ButtonIcon = Properties.Resources.arrow_forward;

                Properties.Settings.Default.Save();
            }
        }

        private void layoutBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layoutBox.SelectedItem?.ToString() == WINDOWS_11)
            {
                if (Properties.Settings.Default.layoutMode != WINDOWS_11)
                {
                    Properties.Settings.Default.layoutMode = WINDOWS_11;
                    _setupFlowPiece.buttonPanel.Width -= 16; _setupFlowPiece.buttonPanel.Left += 8;
                    Properties.Settings.Default.Save();
                }
            }
            else if (layoutBox.SelectedItem?.ToString() == WINDOWS_8DOT1)
            {
                if (Properties.Settings.Default.layoutMode != WINDOWS_8DOT1)
                {
                    Properties.Settings.Default.layoutMode = WINDOWS_8DOT1;
                    _setupFlowPiece.buttonPanel.Width += 16; _setupFlowPiece.buttonPanel.Left -= 8;
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}