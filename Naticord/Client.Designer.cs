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
            this.chatTabControl = new System.Windows.Forms.TabControl();
            this.infoBar = new System.Windows.Forms.StatusStrip();
            this.statusChanger = new System.Windows.Forms.ToolStripDropDownButton();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotDisturbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadingProgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.loadingProgLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.usernameLabelandImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.frdSrvTabCtrl = new System.Windows.Forms.TabControl();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.serversTab = new System.Windows.Forms.TabPage();
            this.exampleTab = new System.Windows.Forms.TabPage();
            this.chatTabControl.SuspendLayout();
            this.infoBar.SuspendLayout();
            this.frdSrvTabCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatTabControl
            // 
            this.chatTabControl.Controls.Add(this.exampleTab);
            this.chatTabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTabControl.Location = new System.Drawing.Point(215, 20);
            this.chatTabControl.Name = "chatTabControl";
            this.chatTabControl.SelectedIndex = 0;
            this.chatTabControl.Size = new System.Drawing.Size(654, 445);
            this.chatTabControl.TabIndex = 2;
            // 
            // infoBar
            // 
            this.infoBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usernameLabelandImage,
            this.statusChanger,
            this.loadingProgBar,
            this.loadingProgLabel});
            this.infoBar.Location = new System.Drawing.Point(0, 482);
            this.infoBar.Name = "infoBar";
            this.infoBar.Size = new System.Drawing.Size(884, 22);
            this.infoBar.TabIndex = 7;
            this.infoBar.Text = "infoBar";
            // 
            // statusChanger
            // 
            this.statusChanger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.doNotDisturbToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.statusChanger.Image = global::Naticord.Properties.Resources.online;
            this.statusChanger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusChanger.Name = "statusChanger";
            this.statusChanger.Size = new System.Drawing.Size(71, 20);
            this.statusChanger.Text = "Online";
            this.statusChanger.ToolTipText = "statusChanger";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            // 
            // doNotDisturbToolStripMenuItem
            // 
            this.doNotDisturbToolStripMenuItem.Name = "doNotDisturbToolStripMenuItem";
            this.doNotDisturbToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.doNotDisturbToolStripMenuItem.Text = "Do not disturb";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.idleToolStripMenuItem.Text = "Idle";
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            // 
            // loadingProgBar
            // 
            this.loadingProgBar.Name = "loadingProgBar";
            this.loadingProgBar.Size = new System.Drawing.Size(100, 16);
            // 
            // loadingProgLabel
            // 
            this.loadingProgLabel.BackColor = System.Drawing.Color.Transparent;
            this.loadingProgLabel.Name = "loadingProgLabel";
            this.loadingProgLabel.Size = new System.Drawing.Size(100, 17);
            this.loadingProgLabel.Text = "loadingProgLabel";
            // 
            // usernameLabelandImage
            // 
            this.usernameLabelandImage.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabelandImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabelandImage.Image = global::Naticord.Properties.Resources.default_pfp_logon;
            this.usernameLabelandImage.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.usernameLabelandImage.Name = "usernameLabelandImage";
            this.usernameLabelandImage.Size = new System.Drawing.Size(156, 17);
            this.usernameLabelandImage.Text = "usernameLabelandImage";
            this.usernameLabelandImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // frdSrvTabCtrl
            // 
            this.frdSrvTabCtrl.Controls.Add(this.friendsTab);
            this.frdSrvTabCtrl.Controls.Add(this.serversTab);
            this.frdSrvTabCtrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frdSrvTabCtrl.Location = new System.Drawing.Point(20, 20);
            this.frdSrvTabCtrl.Name = "frdSrvTabCtrl";
            this.frdSrvTabCtrl.SelectedIndex = 0;
            this.frdSrvTabCtrl.Size = new System.Drawing.Size(189, 445);
            this.frdSrvTabCtrl.TabIndex = 8;
            // 
            // friendsTab
            // 
            this.friendsTab.Location = new System.Drawing.Point(4, 24);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Padding = new System.Windows.Forms.Padding(3);
            this.friendsTab.Size = new System.Drawing.Size(181, 417);
            this.friendsTab.TabIndex = 0;
            this.friendsTab.Text = "Friends";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // serversTab
            // 
            this.serversTab.Location = new System.Drawing.Point(4, 24);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(181, 417);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // exampleTab
            // 
            this.exampleTab.Location = new System.Drawing.Point(4, 24);
            this.exampleTab.Name = "exampleTab";
            this.exampleTab.Size = new System.Drawing.Size(646, 417);
            this.exampleTab.TabIndex = 0;
            this.exampleTab.Text = "exampleTab";
            this.exampleTab.UseVisualStyleBackColor = true;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 504);
            this.Controls.Add(this.frdSrvTabCtrl);
            this.Controls.Add(this.infoBar);
            this.Controls.Add(this.chatTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord - Home";
            this.chatTabControl.ResumeLayout(false);
            this.infoBar.ResumeLayout(false);
            this.infoBar.PerformLayout();
            this.frdSrvTabCtrl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl chatTabControl;
        private System.Windows.Forms.StatusStrip infoBar;
        private System.Windows.Forms.ToolStripDropDownButton statusChanger;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNotDisturbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar loadingProgBar;
        private System.Windows.Forms.ToolStripStatusLabel loadingProgLabel;
        private System.Windows.Forms.TabControl frdSrvTabCtrl;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        private System.Windows.Forms.ToolStripStatusLabel usernameLabelandImage;
        private System.Windows.Forms.TabPage exampleTab;
    }
}