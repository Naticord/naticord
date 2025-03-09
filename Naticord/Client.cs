using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Client : Form
    {
        private ContextMenuStrip contextMenu;

        public Client()
        {
            InitializeComponent();
            InitializeContextMenu();
            this.FormClosing += Client_FormClosing;

            // WinForms image rendering is horrible, this applies anti-aliasing to it.
            usernameLabelandImage.Paint += (sender, e) =>
            {
                ToolStripLabel label = (ToolStripLabel)sender;
                Graphics g = e.Graphics;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                if (label.Image != null)
                {
                    Rectangle imgRect = new Rectangle(0, 0, label.Height, label.Height);
                    g.DrawImage(label.Image, imgRect);
                }
            };
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();

            // Settings
            ToolStripMenuItem settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Click += SettingsItem_Click;
            contextMenu.Items.Add(settingsItem);

            contextMenu.Items.Add(new ToolStripSeparator());

            // About dialog
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About Naticord");
            aboutItem.Click += AboutItem_Click;
            contextMenu.Items.Add(aboutItem);

            this.ContextMenuStrip = contextMenu;
        }

        private void SettingsItem_Click(object sender, EventArgs e)
        {
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
