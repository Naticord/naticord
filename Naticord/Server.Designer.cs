using System.Windows.Forms;

namespace Naticord
{
    partial class Server
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
            this.channelLabel = new System.Windows.Forms.Label();
            this.servernameLabel = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.WebBrowser();
            this.channelList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // channelLabel
            // 
            this.channelLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.channelLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.channelLabel.Location = new System.Drawing.Point(101, 36);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(561, 19);
            this.channelLabel.TabIndex = 5;
            // 
            // servernameLabel
            // 
            this.servernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.servernameLabel.Location = new System.Drawing.Point(12, 9);
            this.servernameLabel.Name = "servernameLabel";
            this.servernameLabel.Size = new System.Drawing.Size(183, 20);
            this.servernameLabel.TabIndex = 4;
            this.servernameLabel.Text = "servername";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(152, 283);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(510, 20);
            this.messageBox.TabIndex = 6;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(153, 36);
            this.chatBox.MinimumSize = new System.Drawing.Size(17, 17);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScriptErrorsSuppressed = true;
            this.chatBox.Size = new System.Drawing.Size(509, 242);
            this.chatBox.TabIndex = 9;
            this.chatBox.Url = new System.Uri("", System.UriKind.Relative);
            this.chatBox.WebBrowserShortcutsEnabled = false;
            // 
            // channelList
            // 
            this.channelList.HideSelection = false;
            this.channelList.Location = new System.Drawing.Point(11, 36);
            this.channelList.Name = "channelList";
            this.channelList.Size = new System.Drawing.Size(135, 267);
            this.channelList.TabIndex = 10;
            this.channelList.UseCompatibleStateImageBehavior = false;
            this.channelList.View = System.Windows.Forms.View.Tile;
            this.channelList.DoubleClick += new System.EventHandler(this.channelList_DoubleClick);
            // 
            // Server
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 313);
            this.Controls.Add(this.channelList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.servernameLabel);
            this.MaximizeBox = false;
            this.Name = "Server";
            this.ShowIcon = false;
            this.Text = "Naticord - Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label channelLabel;
        private System.Windows.Forms.Label servernameLabel;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.WebBrowser chatBox;
        private System.Windows.Forms.ListView channelList;
    }
}