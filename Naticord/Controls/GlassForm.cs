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
                    break;

                case "Mica":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;

                case "Acrylic":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;

                case "Mica (Alt)":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    break;

                case "Composition disabled":
                    DWMExtTitlebar.DwmMethods.SetWindowAttribute(Handle, DWMExtTitlebar.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 0);
                    glassMargin = new Padding(0, 0, 0, 0);

                    clientForm.buttonPanel.BackColor = System.Drawing.Color.White;
                    clientForm.settingsButton.BackColor = System.Drawing.Color.White;
                    clientForm.accountButton.BackColor = System.Drawing.Color.White;
                    clientForm.ghButton.BackColor = System.Drawing.Color.White;
                    break;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GlassMargins = glassMargin;
        }
    }
}