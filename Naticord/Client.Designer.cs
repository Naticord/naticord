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
            this.usernameLabelAndImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoBar = new System.Windows.Forms.StatusStrip();
            this.naticordVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.FSTabControl = new System.Windows.Forms.TabControl();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.serversTab = new System.Windows.Forms.TabPage();
            this.messagesPanel = new System.Windows.Forms.Panel();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.FSTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabelAndImage
            // 
            this.usernameLabelAndImage.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabelAndImage.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.usernameLabelAndImage.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.usernameLabelAndImage.Name = "usernameLabelAndImage";
            this.usernameLabelAndImage.Size = new System.Drawing.Size(0, 18);
            // 
            // infoBar
            // 
            this.infoBar.BackColor = System.Drawing.SystemColors.Control;
            this.infoBar.Location = new System.Drawing.Point(0, 580);
            this.infoBar.Name = "infoBar";
            this.infoBar.Size = new System.Drawing.Size(944, 22);
            this.infoBar.SizingGrip = false;
            this.infoBar.TabIndex = 6;
            this.infoBar.Text = "infoBar";
            // 
            // naticordVersion
            // 
            this.naticordVersion.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.naticordVersion.Name = "naticordVersion";
            this.naticordVersion.Size = new System.Drawing.Size(197, 18);
            this.naticordVersion.Text = "You are on Naticord version 1.0.0";
            // 
            // FSTabControl
            // 
            this.FSTabControl.Controls.Add(this.friendsTab);
            this.FSTabControl.Controls.Add(this.serversTab);
            this.FSTabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FSTabControl.Location = new System.Drawing.Point(13, 14);
            this.FSTabControl.Name = "FSTabControl";
            this.FSTabControl.SelectedIndex = 0;
            this.FSTabControl.Size = new System.Drawing.Size(200, 549);
            this.FSTabControl.TabIndex = 7;
            // 
            // friendsTab
            // 
            this.friendsTab.Location = new System.Drawing.Point(4, 24);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Padding = new System.Windows.Forms.Padding(3);
            this.friendsTab.Size = new System.Drawing.Size(192, 521);
            this.friendsTab.TabIndex = 0;
            this.friendsTab.Text = "Friends";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // serversTab
            // 
            this.serversTab.Location = new System.Drawing.Point(4, 22);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(192, 523);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // messagesPanel
            // 
            this.messagesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messagesPanel.Location = new System.Drawing.Point(219, 14);
            this.messagesPanel.Name = "messagesPanel";
            this.messagesPanel.Size = new System.Drawing.Size(711, 520);
            this.messagesPanel.TabIndex = 8;
            // 
            // messageTextBox
            // 
            this.messageTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageTextBox.Location = new System.Drawing.Point(219, 540);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(635, 23);
            this.messageTextBox.TabIndex = 9;
            // 
            // uploadButton
            // 
            this.uploadButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(860, 540);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(70, 23);
            this.uploadButton.TabIndex = 10;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 602);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.messagesPanel);
            this.Controls.Add(this.FSTabControl);
            this.Controls.Add(this.infoBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord - Home";
            this.Load += new System.EventHandler(this.Client_Load);
            this.FSTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel usernameLabelAndImage;
        private System.Windows.Forms.StatusStrip infoBar;
        private System.Windows.Forms.ToolStripStatusLabel naticordVersion;
        private System.Windows.Forms.TabControl FSTabControl;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        private System.Windows.Forms.Panel messagesPanel;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button uploadButton;
    }
}