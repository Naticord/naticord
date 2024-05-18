namespace Naticord
{
    partial class Naticord
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Naticord));
            this.usernameLabel = new System.Windows.Forms.Label();
            this.sendMessage = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.friendList = new System.Windows.Forms.TabPage();
            this.friendListBox = new System.Windows.Forms.ListView();
            this.serverList = new System.Windows.Forms.TabPage();
            this.serverListBox = new System.Windows.Forms.ListView();
            this.uploadButton = new System.Windows.Forms.Button();
            this.typingStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.friendList.SuspendLayout();
            this.serverList.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(14, 9);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(53, 13);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username";
            // 
            // sendMessage
            // 
            this.sendMessage.Location = new System.Drawing.Point(219, 412);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(569, 20);
            this.sendMessage.TabIndex = 2;
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.messageBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.Location = new System.Drawing.Point(199, 27);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(589, 379);
            this.messageBox.TabIndex = 6;
            this.messageBox.Text = "";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.friendList);
            this.tabControl.Controls.Add(this.serverList);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(181, 405);
            this.tabControl.TabIndex = 7;
            // 
            // friendList
            // 
            this.friendList.Controls.Add(this.friendListBox);
            this.friendList.Location = new System.Drawing.Point(4, 22);
            this.friendList.Name = "friendList";
            this.friendList.Padding = new System.Windows.Forms.Padding(3);
            this.friendList.Size = new System.Drawing.Size(173, 379);
            this.friendList.TabIndex = 0;
            this.friendList.Text = "Friends";
            this.friendList.UseVisualStyleBackColor = true;
            // 
            // friendListBox
            // 
            this.friendListBox.HideSelection = false;
            this.friendListBox.Location = new System.Drawing.Point(6, 6);
            this.friendListBox.MultiSelect = false;
            this.friendListBox.Name = "friendListBox";
            this.friendListBox.Size = new System.Drawing.Size(161, 368);
            this.friendListBox.TabIndex = 0;
            this.friendListBox.UseCompatibleStateImageBehavior = false;
            this.friendListBox.View = System.Windows.Forms.View.SmallIcon;
            this.friendListBox.SelectedIndexChanged += new System.EventHandler(this.FriendList_SelectedIndexChanged);
            // 
            // serverList
            // 
            this.serverList.Controls.Add(this.serverListBox);
            this.serverList.Location = new System.Drawing.Point(4, 22);
            this.serverList.Name = "serverList";
            this.serverList.Padding = new System.Windows.Forms.Padding(3);
            this.serverList.Size = new System.Drawing.Size(173, 379);
            this.serverList.TabIndex = 1;
            this.serverList.Text = "Servers";
            this.serverList.UseVisualStyleBackColor = true;
            // 
            // serverListBox
            // 
            this.serverListBox.GridLines = true;
            this.serverListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.serverListBox.HideSelection = false;
            this.serverListBox.Location = new System.Drawing.Point(6, 6);
            this.serverListBox.MultiSelect = false;
            this.serverListBox.Name = "serverListBox";
            this.serverListBox.Size = new System.Drawing.Size(161, 367);
            this.serverListBox.TabIndex = 1;
            this.serverListBox.UseCompatibleStateImageBehavior = false;
            this.serverListBox.View = System.Windows.Forms.View.SmallIcon;
            this.serverListBox.DoubleClick += new System.EventHandler(this.ServerListBox_DoubleClick);
            // 
            // uploadButton
            // 
            this.uploadButton.Image = global::Naticord.Properties.Resources.upload;
            this.uploadButton.Location = new System.Drawing.Point(199, 412);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(20, 20);
            this.uploadButton.TabIndex = 9;
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // typingStatus
            // 
            this.typingStatus.AutoSize = true;
            this.typingStatus.Location = new System.Drawing.Point(196, 434);
            this.typingStatus.Name = "typingStatus";
            this.typingStatus.Size = new System.Drawing.Size(0, 13);
            this.typingStatus.TabIndex = 10;
            // 
            // Naticord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.typingStatus);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.usernameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Naticord";
            this.Text = "Naticord";
            this.Load += new System.EventHandler(this.Naticord_Load);
            this.tabControl.ResumeLayout(false);
            this.friendList.ResumeLayout(false);
            this.serverList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox sendMessage;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage friendList;
        private System.Windows.Forms.ListView friendListBox;
        private System.Windows.Forms.TabPage serverList;
        private System.Windows.Forms.ListView serverListBox;
        private System.Windows.Forms.Button uploadButton;
        public System.Windows.Forms.Label typingStatus;
    }
}
