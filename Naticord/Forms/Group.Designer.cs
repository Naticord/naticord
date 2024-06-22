using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord
{
    partial class Group : Form
    {
        private IContainer components = null;
        private Label usernameLabel;
        private TextBox messageBox;
        private WebBrowser chatBox;

        public Group()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(9, 7);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(979, 25);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "Group Name";
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(13, 457);
            this.messageBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(975, 22);
            this.messageBox.TabIndex = 6;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Location = new System.Drawing.Point(13, 34);
            this.chatBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chatBox.MinimumSize = new System.Drawing.Size(23, 21);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScriptErrorsSuppressed = true;
            this.chatBox.Size = new System.Drawing.Size(976, 414);
            this.chatBox.TabIndex = 9;
            this.chatBox.Url = new System.Uri("", System.UriKind.Relative);
            this.chatBox.WebBrowserShortcutsEnabled = false;
            // 
            // Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1005, 496);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.usernameLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Group";
            this.ShowIcon = false;
            this.Text = "Group Name - Naticord";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
