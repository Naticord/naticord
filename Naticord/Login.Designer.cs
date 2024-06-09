namespace Naticord
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.username = new System.Windows.Forms.Label();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.signinButton = new System.Windows.Forms.Button();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.discordStatusLink = new System.Windows.Forms.LinkLabel();
            this.tokenButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.username.BackColor = System.Drawing.Color.Transparent;
            this.username.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.username.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.username.Location = new System.Drawing.Point(111, 111);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(76, 24);
            this.username.TabIndex = 2;
            this.username.Text = "Sign in";
            // 
            // emailBox
            // 
            this.emailBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.emailBox.Location = new System.Drawing.Point(41, 144);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(215, 25);
            this.emailBox.TabIndex = 7;
            // 
            // signinButton
            // 
            this.signinButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.signinButton.Location = new System.Drawing.Point(104, 206);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(86, 23);
            this.signinButton.TabIndex = 8;
            this.signinButton.Text = "Sign in";
            this.signinButton.UseVisualStyleBackColor = true;
            this.signinButton.Click += new System.EventHandler(this.signinButton_Click);
            // 
            // profilepicture
            // 
            this.profilepicture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.profilepicture.BackColor = System.Drawing.Color.Transparent;
            this.profilepicture.Image = global::Naticord.Properties.Resources.icon;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.Location = new System.Drawing.Point(104, 17);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(86, 87);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            // 
            // passBox
            // 
            this.passBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passBox.Location = new System.Drawing.Point(41, 175);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(215, 25);
            this.passBox.TabIndex = 10;
            this.passBox.UseSystemPasswordChar = true;
            // 
            // githubLink
            // 
            this.githubLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.githubLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.githubLink.AutoSize = true;
            this.githubLink.BackColor = System.Drawing.Color.Transparent;
            this.githubLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.githubLink.LinkColor = System.Drawing.Color.Fuchsia;
            this.githubLink.Location = new System.Drawing.Point(12, 243);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(40, 13);
            this.githubLink.TabIndex = 11;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "GitHub";
            this.githubLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(50, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "|";
            // 
            // discordStatusLink
            // 
            this.discordStatusLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.discordStatusLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.discordStatusLink.AutoSize = true;
            this.discordStatusLink.BackColor = System.Drawing.Color.Transparent;
            this.discordStatusLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.discordStatusLink.LinkColor = System.Drawing.Color.Fuchsia;
            this.discordStatusLink.Location = new System.Drawing.Point(62, 243);
            this.discordStatusLink.Name = "discordStatusLink";
            this.discordStatusLink.Size = new System.Drawing.Size(76, 13);
            this.discordStatusLink.TabIndex = 13;
            this.discordStatusLink.TabStop = true;
            this.discordStatusLink.Text = "Discord Status";
            this.discordStatusLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.discordStatusLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.discordStatusLink_LinkClicked);
            // 
            // tokenButton
            // 
            this.tokenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tokenButton.Location = new System.Drawing.Point(235, 233);
            this.tokenButton.Name = "tokenButton";
            this.tokenButton.Size = new System.Drawing.Size(49, 23);
            this.tokenButton.TabIndex = 14;
            this.tokenButton.Text = "Token";
            this.tokenButton.UseVisualStyleBackColor = true;
            this.tokenButton.Click += new System.EventHandler(this.tokenButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(296, 267);
            this.Controls.Add(this.tokenButton);
            this.Controls.Add(this.discordStatusLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.githubLink);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.username);
            this.Controls.Add(this.profilepicture);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.Button signinButton;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.LinkLabel githubLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel discordStatusLink;
        private System.Windows.Forms.Button tokenButton;
    }
}

