// This has been taken from the 'aerocord' project found here:
// https://github.com/jukfiuune/aerocord
// Credit goes to them for this

using Naticord.Classes;
using Naticord.Forms;
using System;
using System.Windows.Forms;
using WindowsFormsAero;

namespace Naticord.Controls
{
    public class GlassForm : AeroForm
    {
        public bool AutoColorMode = false;
        public string RenderMode = Properties.Settings.Default.renderMode;
        public Padding glassMargin = new Padding(0, 35, 0, 0);
        private Client clientForm => this as Client;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            switch (RenderMode)
            {
                case "Aero":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 1);
                    CompSet(false);
                    break;

                case "Mica":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    CompSet(false);
                    break;

                case "Acrylic":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    CompSet(false);
                    break;

                case "Mica (Alt)":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    CompSet(false);
                    break;

                case "Composition disabled":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 0);
                    glassMargin = new Padding(0, 0, 0, 0);
                    CompSet(true);
                    break;
            }
        }

        private void CompSet(bool setComp)
        {
            if (setComp == true) 
            {
                clientForm.buttonPanel.BackColor = System.Drawing.Color.White;
                clientForm.settingsButton.BackColor = System.Drawing.Color.White;
                clientForm.accountButton.BackColor = System.Drawing.Color.White;
                clientForm.ghButton.BackColor = System.Drawing.Color.White;
            }
            else
            {
                clientForm.buttonPanel.BackColor = System.Drawing.Color.Black;
                clientForm.settingsButton.BackColor = System.Drawing.Color.Black;
                clientForm.accountButton.BackColor = System.Drawing.Color.Black;
                clientForm.ghButton.BackColor = System.Drawing.Color.Black;
            }

            clientForm.usernameLabel.CompositionDisabled = setComp;
            clientForm.settingsButton.CompositionDisabled = setComp;
            clientForm.accountButton.CompositionDisabled = setComp;
            clientForm.ghButton.CompositionDisabled = setComp;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GlassMargins = glassMargin;
        }
    }
}