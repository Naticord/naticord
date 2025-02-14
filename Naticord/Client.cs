using System;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Client : Form
    {
        private ContextMenuStrip contextMenu;

        public Client()
        {
            InitializeComponent();
            TruncateLabels(usernameLabel, statusLabel);
            InitializeContextMenu();
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

        public void TruncateLabels(Label usernameLabel, Label statusLabel)
        {
            if (usernameLabel.Text.Length >= 17)
            {
                usernameLabel.Text = usernameLabel.Text.Substring(0, usernameLabel.Text.Length - 3) + "...";
            }

            if (statusLabel.Text.Length >= 23)
            {
                statusLabel.Text = statusLabel.Text.Substring(0, statusLabel.Text.Length - 3) + "...";
            }
        }
    }
}
