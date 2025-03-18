namespace Naticord
{
    partial class Message
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.profilePictureAuthor = new System.Windows.Forms.PictureBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.messageContent = new Naticord.MDLabel();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureAuthor)).BeginInit();
            this.SuspendLayout();
            // 
            // profilePictureAuthor
            // 
            this.profilePictureAuthor.Image = global::Naticord.Properties.Resources.discord_profile;
            this.profilePictureAuthor.Location = new System.Drawing.Point(4, 3);
            this.profilePictureAuthor.Name = "profilePictureAuthor";
            this.profilePictureAuthor.Size = new System.Drawing.Size(35, 35);
            this.profilePictureAuthor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureAuthor.TabIndex = 1;
            this.profilePictureAuthor.TabStop = false;
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorLabel.Location = new System.Drawing.Point(45, 5);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(73, 15);
            this.authorLabel.TabIndex = 2;
            this.authorLabel.Text = "authorLabel";
            // 
            // messageContent
            // 
            this.messageContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageContent.Location = new System.Drawing.Point(46, 20);
            this.messageContent.MDImage = null;
            this.messageContent.Name = "messageContent";
            this.messageContent.Size = new System.Drawing.Size(95, 17);
            this.messageContent.TabIndex = 0;
            this.messageContent.Text = "messageContent";
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.profilePictureAuthor);
            this.Controls.Add(this.messageContent);
            this.Name = "Message";
            this.Size = new System.Drawing.Size(685, 40);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureAuthor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MDLabel messageContent;
        private System.Windows.Forms.PictureBox profilePictureAuthor;
        private System.Windows.Forms.Label authorLabel;
    }
}
