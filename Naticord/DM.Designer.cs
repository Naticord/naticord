using System.Windows.Forms;

namespace Naticord
{
    partial class DM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DM));
            this.usernameLabel = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.profilepicturefriend = new System.Windows.Forms.PictureBox();
            this.typingStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(108, 12);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(183, 20);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "username";
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(112, 361);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(630, 20);
            this.messageBox.TabIndex = 6;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Location = new System.Drawing.Point(112, 35);
            this.chatBox.MinimumSize = new System.Drawing.Size(17, 17);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScriptErrorsSuppressed = true;
            this.chatBox.Size = new System.Drawing.Size(630, 320);
            this.chatBox.TabIndex = 9;
            this.chatBox.Url = new System.Uri("", System.UriKind.Relative);
            this.chatBox.WebBrowserShortcutsEnabled = false;
            // 
            // profilepicture
            // 
            this.profilepicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.profilepicture.BackColor = System.Drawing.Color.Transparent;
            this.profilepicture.ErrorImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.InitialImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(10, 303);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(90, 90);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 8;
            this.profilepicture.TabStop = false;
            // 
            // profilepicturefriend
            // 
            this.profilepicturefriend.BackColor = System.Drawing.Color.Transparent;
            this.profilepicturefriend.ErrorImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Image = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.InitialImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Location = new System.Drawing.Point(12, 12);
            this.profilepicturefriend.Name = "profilepicturefriend";
            this.profilepicturefriend.Size = new System.Drawing.Size(90, 90);
            this.profilepicturefriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilepicturefriend.TabIndex = 0;
            this.profilepicturefriend.TabStop = false;
            // 
            // typingStatus
            // 
            this.typingStatus.AutoSize = true;
            this.typingStatus.Location = new System.Drawing.Point(109, 384);
            this.typingStatus.Name = "typingStatus";
            this.typingStatus.Size = new System.Drawing.Size(0, 13);
            this.typingStatus.TabIndex = 10;
            // 
            // DM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(754, 403);
            this.Controls.Add(this.typingStatus);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.profilepicture);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicturefriend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DM";
            this.ShowIcon = false;
            this.Text = "Naticord - Chat";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicturefriend;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.WebBrowser chatBox;
        public Label typingStatus;
    }
}
