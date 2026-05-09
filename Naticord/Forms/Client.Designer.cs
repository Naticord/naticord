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

                foreach (var item in _friendItems)
                    item.Dispose();
                _friendItems.Clear();
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
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.usernameLabel = new Naticord.UserControls.AeroLabel();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.githubButton = new Naticord.UserControls.GlassExtendedButton();
            this.settingsButton = new Naticord.UserControls.GlassExtendedButton();
            this.accountButton = new Naticord.UserControls.GlassExtendedButton();
            this.sidebarRenderer = new DirectUI.Net.Hosting.DirectUIRenderer();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.accountContext = new Naticord.Classes.NativeContextGenerator(this.components);
            this.accountStatusChanger = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.logOutStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            this.accountContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Controls.Add(this.usernameLabel);
            this.buttonPanel.Controls.Add(this.avatarBox);
            this.buttonPanel.Controls.Add(this.githubButton);
            this.buttonPanel.Controls.Add(this.settingsButton);
            this.buttonPanel.Controls.Add(this.accountButton);
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(971, 35);
            this.buttonPanel.TabIndex = 0;
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.Location = new System.Drawing.Point(853, 2);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(88, 23);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "Loading...";
            this.usernameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.usernameLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // avatarBox
            // 
            this.avatarBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.avatarBox.BackColor = System.Drawing.Color.Black;
            this.avatarBox.Image = global::Naticord.Properties.Resources.user_silhouette_skeuo;
            this.avatarBox.Location = new System.Drawing.Point(947, 2);
            this.avatarBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(23, 23);
            this.avatarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.avatarBox.TabIndex = 3;
            this.avatarBox.TabStop = false;
            // 
            // githubButton
            // 
            this.githubButton.BackColor = System.Drawing.Color.Transparent;
            this.githubButton.ButtonIcon = global::Naticord.Properties.Resources.github_skeuo;
            this.githubButton.ButtonLabel = "GitHub";
            this.githubButton.CompositionDisabled = false;
            this.githubButton.Location = new System.Drawing.Point(168, 2);
            this.githubButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.githubButton.Name = "githubButton";
            this.githubButton.ReverseOrder = false;
            this.githubButton.Size = new System.Drawing.Size(76, 23);
            this.githubButton.TabIndex = 2;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.ButtonIcon = global::Naticord.Properties.Resources.cog_skeuo;
            this.settingsButton.ButtonLabel = "Settings";
            this.settingsButton.CompositionDisabled = false;
            this.settingsButton.Location = new System.Drawing.Point(85, 2);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.ReverseOrder = false;
            this.settingsButton.Size = new System.Drawing.Size(76, 23);
            this.settingsButton.TabIndex = 1;
            this.settingsButton.ButtonClick += new System.EventHandler(this.settingsButton_ButtonClick);
            // 
            // accountButton
            // 
            this.accountButton.BackColor = System.Drawing.Color.Transparent;
            this.accountButton.ButtonIcon = global::Naticord.Properties.Resources.account_skeuo;
            this.accountButton.ButtonLabel = "Account";
            this.accountButton.CompositionDisabled = false;
            this.accountButton.Location = new System.Drawing.Point(2, 2);
            this.accountButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.accountButton.Name = "accountButton";
            this.accountButton.ReverseOrder = false;
            this.accountButton.Size = new System.Drawing.Size(76, 23);
            this.accountButton.TabIndex = 0;
            this.accountButton.ButtonClick += new System.EventHandler(this.accountButton_ButtonClick);
            // 
            // sidebarRenderer
            // 
            this.sidebarRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sidebarRenderer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.sidebarRenderer.Location = new System.Drawing.Point(0, 35);
            this.sidebarRenderer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sidebarRenderer.Name = "sidebarRenderer";
            this.sidebarRenderer.ResId = "sidebarPieceMain";
            this.sidebarRenderer.Size = new System.Drawing.Size(250, 503);
            this.sidebarRenderer.TabIndex = 1;
            this.sidebarRenderer.XmlFile = "User Interface\\ClientFlow\\Sidebar.xml";
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.Location = new System.Drawing.Point(250, 34);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(721, 504);
            this.contentPanel.TabIndex = 2;
            // 
            // accountContext
            // 
            this.accountContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountStatusChanger,
            this.profileStripItem,
            this.toolStripSeparator1,
            this.logOutStripItem});
            this.accountContext.Name = "accountContext";
            this.accountContext.Size = new System.Drawing.Size(181, 98);
            // 
            // accountStatusChanger
            // 
            this.accountStatusChanger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.busyToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.accountStatusChanger.Name = "accountStatusChanger";
            this.accountStatusChanger.Size = new System.Drawing.Size(180, 22);
            this.accountStatusChanger.Text = "Your status";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Image = global::Naticord.Properties.Resources.online_skeuo;
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            // 
            // busyToolStripMenuItem
            // 
            this.busyToolStripMenuItem.Image = global::Naticord.Properties.Resources.busy_skeuo;
            this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
            this.busyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.busyToolStripMenuItem.Text = "Busy";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Image = global::Naticord.Properties.Resources.idle_skeuo;
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.idleToolStripMenuItem.Text = "Idle";
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Image = global::Naticord.Properties.Resources.offline_skeuo;
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            // 
            // profileStripItem
            // 
            this.profileStripItem.Name = "profileStripItem";
            this.profileStripItem.Size = new System.Drawing.Size(180, 22);
            this.profileStripItem.Text = "Your profile";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // logOutStripItem
            // 
            this.logOutStripItem.Name = "logOutStripItem";
            this.logOutStripItem.Size = new System.Drawing.Size(180, 22);
            this.logOutStripItem.Text = "Log out of Naticord";
            this.logOutStripItem.Click += new System.EventHandler(this.logOutStripItem_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(971, 538);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarRenderer);
            this.Controls.Add(this.buttonPanel);
            this.GlassMargins = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord";
            this.Load += new System.EventHandler(this.OnLoad);
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            this.accountContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel buttonPanel;
        public UserControls.GlassExtendedButton accountButton;
        public UserControls.GlassExtendedButton githubButton;
        public UserControls.GlassExtendedButton settingsButton;
        public System.Windows.Forms.PictureBox avatarBox;
        private DirectUI.Net.Hosting.DirectUIRenderer sidebarRenderer;
        public UserControls.AeroLabel usernameLabel;
        private System.Windows.Forms.Panel contentPanel;
        private Classes.NativeContextGenerator accountContext;
        private System.Windows.Forms.ToolStripMenuItem accountStatusChanger;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileStripItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem logOutStripItem;
    }
}