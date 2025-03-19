using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Naticord
{
    public partial class FSControl : UserControl
    {
        private Color HighlightColor;
        public string Username { get; set; }

        public FSControl()
        {
            InitializeComponent();
            HighlightColor = GetHighlightColor();
        }

        public void ClickedDesignChange(bool clicked)
        {
            if (clicked)
            {
                nameLabel.ForeColor = Color.White;
                statusLabel.ForeColor = Color.White;
                this.BackColor = HighlightColor;
            }
            else
            {
                nameLabel.ForeColor = Color.Black;
                statusLabel.ForeColor = Color.Gray;
                this.BackColor = Color.White;
            }
        }

        public string LText
        {
            get => nameLabel.Text;
            set
            {
                if (value.Length >= 20)
                    nameLabel.Text = value.Substring(0, 17) + "...";
                else
                    nameLabel.Text = value;
            }
        }

        public string SText
        {
            get => statusLabel.Text;
            set
            {
                if (value.Length >= 20)
                    statusLabel.Text = value.Substring(0, 17) + "...";
                else
                    statusLabel.Text = value;
            }
        }

        public Image PFPPic
        {
            get => profilePictureItem.Image;
            set => profilePictureItem.Image = value;
        }

        private Color GetHighlightColor()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors"))
                {
                    string colorString = key?.GetValue("Hilight") as string;
                    if (!string.IsNullOrEmpty(colorString))
                    {
                        int[] rgb = colorString.Split(' ').Select(int.Parse).ToArray();
                        return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    }
                }
            }
            catch { }

            return Color.FromArgb(0, 120, 215); // Default 'Windows 10' Hilight color
        }
    }
}
