// This has been taken from the 'aerocord' project found here:
// https://github.com/jukfiuune/aerocord
// Credit goes to them for this

using Naticord.Classes.ExtendedGlass;
using Naticord.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero;
using Naticord.Forms.SetupFlows;

namespace Naticord.UserControls
{
    public class GlassForm : AeroForm
    {
        public bool AutoColorMode = false;
        public string RenderMode = Properties.Settings.Default.renderMode;

        public Padding glassMargin = new Padding(0, 30, 0, 0);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            switch (RenderMode)
            {
                case "Aero":
                    GlassInvoke.DwmMethods.SetWindowAttribute(Handle, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 1);
                    break;

                case "Mica":
                    GlassInvoke.DwmMethods.SetWindowAttribute(Handle, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;

                case "Acrylic":
                    GlassInvoke.DwmMethods.SetWindowAttribute(Handle, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;

                case "Mica (Alt)":
                    GlassInvoke.DwmMethods.SetWindowAttribute(Handle, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    break;

                case "Composition disabled":
                    GlassInvoke.DwmMethods.SetWindowAttribute(Handle, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 0);
                    glassMargin = new Padding(0, 0, 0, 0);

                    break;
            }
        }

        public void CompSet(bool compActive, bool isSetup, SetupFlowPiece setupFlow = null, Client clientForm = null)
        {
            if (isSetup)
            {
                if (compActive)
                {
                    // Do nothing, the designer has it set to be composite anyway
                }
                else
                {
                    // Set the button panel to be the page count color
                    setupFlow.buttonPanel.BackColor = SystemColors.ButtonFace;

                    // Set the two buttons to have composition disabled
                    setupFlow.backButton.CompositionDisabled = true;
                    setupFlow.nextButton.CompositionDisabled = true;
                }
            }
            else
            {
                if (compActive)
                {
                    // Do nothing, the designer has it set to be composite anyway
                }
                else
                {
                    // Set the button panel to be the button face color
                    clientForm.buttonPanel.BackColor = SystemColors.ButtonFace;
                    // Set the profile picture box to be the button face color too
                    clientForm.avatarBox.BackColor = SystemColors.ButtonFace;

                    // Set the three buttons to have composition disabled
                    clientForm.accountButton.CompositionDisabled = true;
                    clientForm.settingsButton.CompositionDisabled = true;
                    clientForm.githubButton.CompositionDisabled = true;
                    // And this text label...
                    clientForm.usernameLabel.CompositionDisabled = true;

                    // We need to offset them so they look nice on systems with no composition
                    // This doesn't matter on composite systems because the extended glass will help ease it out
                    clientForm.accountButton.Top += 3;
                    clientForm.settingsButton.Top += 3;
                    clientForm.githubButton.Top += 3;

                    // Offset the user details too!
                    clientForm.avatarBox.Top += 3;
                    clientForm.usernameLabel.Top += 3;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GlassMargins = glassMargin;
        }
    }
}