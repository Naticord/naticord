using Naticord.Classes;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            string osVersion = OSVersionHelper.GetWindowsVersion();
            switch (osVersion)
            {
                case "Windows 11":
                    osVerBox.SelectedItem = "Windows 11";
                    icnStyleBox.SelectedItem = "Modern";
                    bdrStyleBox.SelectedItem = "Slim";
                    bdStyleBox.SelectedItem = "Mica";
                    break;

                case "Windows 10":
                    osVerBox.SelectedItem = "Windows 10";
                    icnStyleBox.SelectedItem = "Modern";
                    bdrStyleBox.SelectedItem = "Slim";
                    bdStyleBox.SelectedItem = "Acrylic";
                    break;

                case "Windows 8.1":
                case "Windows 8":
                case "Windows 7":
                    osVerBox.SelectedItem = "Windows 7 - 8.1";
                    icnStyleBox.SelectedItem = "Legacy";
                    bdrStyleBox.SelectedItem = "Thick";
                    bdStyleBox.SelectedItem = "Aero";
                    break;
            }
        }
    }
}
