using Naticord.Controls;
using System.Windows.Forms;

namespace Naticord.Forms
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.usernameLabel = new Naticord.Controls.AeroLabel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.ghButton = new Naticord.Controls.ExtButton();
            this.accountButton = new Naticord.Controls.ExtButton();
            this.settingsButton = new Naticord.Controls.ExtButton();
            this.profilePictureUser = new System.Windows.Forms.PictureBox();
            this.accountMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotDisturbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSeparatorAccMenu = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.attachLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dmsTabControl = new System.Windows.Forms.TabControl();
            this.dmsTab = new System.Windows.Forms.TabPage();
            this.servTab = new System.Windows.Forms.TabPage();
            this.chatPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureUser)).BeginInit();
            this.accountMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.dmsTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(776, 4);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.usernameLabel.Size = new System.Drawing.Size(199, 25);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "usernameLabel";
            this.usernameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.usernameLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Black;
            this.buttonPanel.Controls.Add(this.ghButton);
            this.buttonPanel.Controls.Add(this.accountButton);
            this.buttonPanel.Controls.Add(this.settingsButton);
            this.buttonPanel.Location = new System.Drawing.Point(8, 2);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(300, 30);
            this.buttonPanel.TabIndex = 4;
            // 
            // ghButton
            // 
            this.ghButton.BackColor = System.Drawing.Color.Black;
            this.ghButton.ButtonIcon = ((System.Drawing.Image)(resources.GetObject("ghButton.ButtonIcon")));
            this.ghButton.ButtonLabel = "GitHub";
            this.ghButton.CompositionDisabled = false;
            this.ghButton.Location = new System.Drawing.Point(183, 0);
            this.ghButton.Name = "ghButton";
            this.ghButton.Size = new System.Drawing.Size(85, 30);
            this.ghButton.TabIndex = 4;
            // 
            // accountButton
            // 
            this.accountButton.BackColor = System.Drawing.Color.Black;
            this.accountButton.ButtonIcon = ((System.Drawing.Image)(resources.GetObject("accountButton.ButtonIcon")));
            this.accountButton.ButtonLabel = "Account";
            this.accountButton.CompositionDisabled = false;
            this.accountButton.Location = new System.Drawing.Point(92, 0);
            this.accountButton.Name = "accountButton";
            this.accountButton.Size = new System.Drawing.Size(85, 30);
            this.accountButton.TabIndex = 3;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.Black;
            this.settingsButton.ButtonIcon = ((System.Drawing.Image)(resources.GetObject("settingsButton.ButtonIcon")));
            this.settingsButton.ButtonLabel = "Settings";
            this.settingsButton.CompositionDisabled = false;
            this.settingsButton.Location = new System.Drawing.Point(1, 0);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(85, 30);
            this.settingsButton.TabIndex = 2;
            // 
            // profilePictureUser
            // 
            this.profilePictureUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.profilePictureUser.BackColor = System.Drawing.Color.Black;
            this.profilePictureUser.Image = ((System.Drawing.Image)(resources.GetObject("profilePictureUser.Image")));
            this.profilePictureUser.Location = new System.Drawing.Point(981, 4);
            this.profilePictureUser.Name = "profilePictureUser";
            this.profilePictureUser.Size = new System.Drawing.Size(25, 25);
            this.profilePictureUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureUser.TabIndex = 0;
            this.profilePictureUser.TabStop = false;
            // 
            // accountMenu
            // 
            this.accountMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripMenuItem,
            this.viewProfileToolStripMenuItem,
            this.toolSeparatorAccMenu,
            this.logoutToolStripMenuItem});
            this.accountMenu.Name = "accountMenu";
            this.accountMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.accountMenu.Size = new System.Drawing.Size(137, 76);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.doNotDisturbToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.statusToolStripMenuItem.Text = "Status";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            // 
            // doNotDisturbToolStripMenuItem
            // 
            this.doNotDisturbToolStripMenuItem.Name = "doNotDisturbToolStripMenuItem";
            this.doNotDisturbToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.doNotDisturbToolStripMenuItem.Text = "Do Not Disturb";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.idleToolStripMenuItem.Text = "Idle";
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            // 
            // viewProfileToolStripMenuItem
            // 
            this.viewProfileToolStripMenuItem.Name = "viewProfileToolStripMenuItem";
            this.viewProfileToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.viewProfileToolStripMenuItem.Text = "View profile";
            // 
            // toolSeparatorAccMenu
            // 
            this.toolSeparatorAccMenu.Name = "toolSeparatorAccMenu";
            this.toolSeparatorAccMenu.Size = new System.Drawing.Size(133, 6);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attachLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 569);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1014, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 5;
            this.statusBar.Text = "StatusBar";
            // 
            // attachLabel
            // 
            this.attachLabel.BackColor = System.Drawing.Color.Transparent;
            this.attachLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachLabel.Name = "attachLabel";
            this.attachLabel.Size = new System.Drawing.Size(117, 17);
            this.attachLabel.Text = "No file was attached.";
            // 
            // dmsTabControl
            // 
            this.dmsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dmsTabControl.Controls.Add(this.dmsTab);
            this.dmsTabControl.Controls.Add(this.servTab);
            this.dmsTabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dmsTabControl.Location = new System.Drawing.Point(16, 49);
            this.dmsTabControl.Name = "dmsTabControl";
            this.dmsTabControl.SelectedIndex = 0;
            this.dmsTabControl.Size = new System.Drawing.Size(200, 503);
            this.dmsTabControl.TabIndex = 6;
            // 
            // dmsTab
            // 
            this.dmsTab.Location = new System.Drawing.Point(4, 24);
            this.dmsTab.Name = "dmsTab";
            this.dmsTab.Padding = new System.Windows.Forms.Padding(3);
            this.dmsTab.Size = new System.Drawing.Size(192, 475);
            this.dmsTab.TabIndex = 0;
            this.dmsTab.Text = "Direct Messages";
            this.dmsTab.UseVisualStyleBackColor = true;
            // 
            // servTab
            // 
            this.servTab.Location = new System.Drawing.Point(4, 24);
            this.servTab.Name = "servTab";
            this.servTab.Padding = new System.Windows.Forms.Padding(3);
            this.servTab.Size = new System.Drawing.Size(192, 475);
            this.servTab.TabIndex = 1;
            this.servTab.Text = "Servers";
            this.servTab.UseVisualStyleBackColor = true;
            // 
            // chatPanel
            // 
            this.chatPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatPanel.Location = new System.Drawing.Point(222, 49);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(776, 474);
            this.chatPanel.TabIndex = 7;
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBox.Location = new System.Drawing.Point(296, 529);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(702, 23);
            this.chatBox.TabIndex = 8;
            // 
            // uploadButton
            // 
            this.uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uploadButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(222, 529);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(68, 23);
            this.uploadButton.TabIndex = 5;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            // 
            // Client
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 591);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.dmsTabControl);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilePictureUser);
            this.GlassMargins = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chats - Naticord";
            this.Load += new System.EventHandler(this.Client_Load);
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureUser)).EndInit();
            this.accountMenu.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.dmsTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox profilePictureUser;
        public Controls.ExtButton settingsButton;
        public AeroLabel usernameLabel;
        public System.Windows.Forms.Panel buttonPanel;
        public Controls.ExtButton accountButton;
        public Controls.ExtButton ghButton;
        private System.Windows.Forms.ContextMenuStrip accountMenu;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNotDisturbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolSeparatorAccMenu;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel attachLabel;
        private System.Windows.Forms.TabControl dmsTabControl;
        private System.Windows.Forms.TabPage dmsTab;
        private System.Windows.Forms.TabPage servTab;
        private System.Windows.Forms.FlowLayoutPanel chatPanel;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Button uploadButton;
    }
}