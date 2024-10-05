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
            this.messageBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.typingStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.profilepicturefriend = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).BeginInit();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.Location = new System.Drawing.Point(16, 368);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(723, 19);
            this.messageBox.TabIndex = 6;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Location = new System.Drawing.Point(16, 101);
            this.chatBox.MinimumSize = new System.Drawing.Size(17, 17);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScriptErrorsSuppressed = true;
            this.chatBox.Size = new System.Drawing.Size(723, 261);
            this.chatBox.TabIndex = 9;
            this.chatBox.Url = new System.Uri("", System.UriKind.Relative);
            this.chatBox.WebBrowserShortcutsEnabled = false;
            // 
            // typingStatus
            // 
            this.typingStatus.AutoSize = true;
            this.typingStatus.Location = new System.Drawing.Point(109, 384);
            this.typingStatus.Name = "typingStatus";
            this.typingStatus.Size = new System.Drawing.Size(0, 13);
            this.typingStatus.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Naticord.Properties.Resources.defaultdmbg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.profilepicturefriend);
            this.panel1.Controls.Add(this.usernameLabel);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 87);
            this.panel1.TabIndex = 11;
            // 
            // profilepicturefriend
            // 
            this.profilepicturefriend.BackColor = System.Drawing.Color.Transparent;
            this.profilepicturefriend.ErrorImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Image = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.InitialImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicturefriend.Location = new System.Drawing.Point(20, 16);
            this.profilepicturefriend.Name = "profilepicturefriend";
            this.profilepicturefriend.Size = new System.Drawing.Size(55, 55);
            this.profilepicturefriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilepicturefriend.TabIndex = 0;
            this.profilepicturefriend.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(88, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "No status...";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(86, 22);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(642, 25);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "username";
            // 
            // DM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(754, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.typingStatus);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.messageBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DM";
            this.Text = "Naticord";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicturefriend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicturefriend;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.WebBrowser chatBox;
        public Label typingStatus;
        private Panel panel1;
        private Label label2;
        private Label usernameLabel;
    }
}
