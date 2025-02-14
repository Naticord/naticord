namespace Naticord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.usernameLabel = new System.Windows.Forms.Label();
            this.chatTabControl = new System.Windows.Forms.TabControl();
            this.frdSrvPanel = new System.Windows.Forms.Panel();
            this.friendsButton = new System.Windows.Forms.Button();
            this.serversButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.infoBar = new System.Windows.Forms.StatusStrip();
            this.onlinePeopleCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusChanger = new System.Windows.Forms.ToolStripDropDownButton();
            this.profilePictureHome = new System.Windows.Forms.PictureBox();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotDisturbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureHome)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.usernameLabel.Location = new System.Drawing.Point(56, 15);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(116, 21);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "usernameLabel";
            // 
            // chatTabControl
            // 
            this.chatTabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTabControl.Location = new System.Drawing.Point(213, 20);
            this.chatTabControl.Name = "chatTabControl";
            this.chatTabControl.SelectedIndex = 0;
            this.chatTabControl.Size = new System.Drawing.Size(654, 445);
            this.chatTabControl.TabIndex = 2;
            // 
            // frdSrvPanel
            // 
            this.frdSrvPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frdSrvPanel.Location = new System.Drawing.Point(18, 61);
            this.frdSrvPanel.Name = "frdSrvPanel";
            this.frdSrvPanel.Size = new System.Drawing.Size(183, 376);
            this.frdSrvPanel.TabIndex = 3;
            // 
            // friendsButton
            // 
            this.friendsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.friendsButton.Location = new System.Drawing.Point(18, 442);
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.Size = new System.Drawing.Size(65, 23);
            this.friendsButton.TabIndex = 0;
            this.friendsButton.Text = "Friends";
            this.friendsButton.UseVisualStyleBackColor = true;
            // 
            // serversButton
            // 
            this.serversButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serversButton.Location = new System.Drawing.Point(89, 442);
            this.serversButton.Name = "serversButton";
            this.serversButton.Size = new System.Drawing.Size(65, 23);
            this.serversButton.TabIndex = 4;
            this.serversButton.Text = "Servers";
            this.serversButton.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(57, 36);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(66, 15);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "statusLabel";
            // 
            // infoBar
            // 
            this.infoBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlinePeopleCounter,
            this.statusChanger});
            this.infoBar.Location = new System.Drawing.Point(0, 482);
            this.infoBar.Name = "infoBar";
            this.infoBar.Size = new System.Drawing.Size(884, 22);
            this.infoBar.TabIndex = 7;
            this.infoBar.Text = "infoBar";
            // 
            // onlinePeopleCounter
            // 
            this.onlinePeopleCounter.BackColor = System.Drawing.Color.Transparent;
            this.onlinePeopleCounter.Name = "onlinePeopleCounter";
            this.onlinePeopleCounter.Size = new System.Drawing.Size(142, 17);
            this.onlinePeopleCounter.Text = "<num> people are online";
            // 
            // statusChanger
            // 
            this.statusChanger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusChanger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.doNotDisturbToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.statusChanger.Image = global::Naticord.Properties.Resources.online;
            this.statusChanger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusChanger.Name = "statusChanger";
            this.statusChanger.Size = new System.Drawing.Size(29, 20);
            this.statusChanger.Text = "toolStripDropDownButton1";
            this.statusChanger.ToolTipText = "statusChanger";
            // 
            // profilePictureHome
            // 
            this.profilePictureHome.Image = global::Naticord.Properties.Resources.default_pfp_logon;
            this.profilePictureHome.Location = new System.Drawing.Point(18, 20);
            this.profilePictureHome.Name = "profilePictureHome";
            this.profilePictureHome.Size = new System.Drawing.Size(32, 32);
            this.profilePictureHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureHome.TabIndex = 5;
            this.profilePictureHome.TabStop = false;
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            // 
            // doNotDisturbToolStripMenuItem
            // 
            this.doNotDisturbToolStripMenuItem.Name = "doNotDisturbToolStripMenuItem";
            this.doNotDisturbToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.doNotDisturbToolStripMenuItem.Text = "Do not disturb";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.idleToolStripMenuItem.Text = "Idle";
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 504);
            this.Controls.Add(this.infoBar);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.profilePictureHome);
            this.Controls.Add(this.serversButton);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.frdSrvPanel);
            this.Controls.Add(this.chatTabControl);
            this.Controls.Add(this.usernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord - Home";
            this.infoBar.ResumeLayout(false);
            this.infoBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureHome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TabControl chatTabControl;
        private System.Windows.Forms.Panel frdSrvPanel;
        private System.Windows.Forms.Button friendsButton;
        private System.Windows.Forms.Button serversButton;
        private System.Windows.Forms.PictureBox profilePictureHome;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.StatusStrip infoBar;
        private System.Windows.Forms.ToolStripStatusLabel onlinePeopleCounter;
        private System.Windows.Forms.ToolStripDropDownButton statusChanger;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNotDisturbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
    }
}