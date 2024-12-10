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
            this.discordStatusLink = new System.Windows.Forms.LinkLabel();
            this.proxyLink = new System.Windows.Forms.LinkLabel();
            this.tokenLink = new System.Windows.Forms.LinkLabel();
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
            this.emailBox.TabIndex = 1;
            // 
            // signinButton
            // 
            this.signinButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.signinButton.Location = new System.Drawing.Point(104, 206);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(86, 23);
            this.signinButton.TabIndex = 3;
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
            this.passBox.TabIndex = 2;
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
            this.githubLink.Location = new System.Drawing.Point(16, 243);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(40, 13);
            this.githubLink.TabIndex = 11;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "GitHub";
            this.githubLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLink_LinkClicked);
            // 
            // discordStatusLink
            // 
            this.discordStatusLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.discordStatusLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.discordStatusLink.AutoSize = true;
            this.discordStatusLink.BackColor = System.Drawing.Color.Transparent;
            this.discordStatusLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.discordStatusLink.LinkColor = System.Drawing.Color.Fuchsia;
            this.discordStatusLink.Location = new System.Drawing.Point(66, 243);
            this.discordStatusLink.Name = "discordStatusLink";
            this.discordStatusLink.Size = new System.Drawing.Size(76, 13);
            this.discordStatusLink.TabIndex = 13;
            this.discordStatusLink.TabStop = true;
            this.discordStatusLink.Text = "Discord Status";
            this.discordStatusLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.discordStatusLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.discordStatusLink_LinkClicked);
            // 
            // proxyLink
            // 
            this.proxyLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.proxyLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.proxyLink.AutoSize = true;
            this.proxyLink.BackColor = System.Drawing.Color.Transparent;
            this.proxyLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.proxyLink.LinkColor = System.Drawing.Color.Fuchsia;
            this.proxyLink.Location = new System.Drawing.Point(155, 243);
            this.proxyLink.Name = "proxyLink";
            this.proxyLink.Size = new System.Drawing.Size(74, 13);
            this.proxyLink.TabIndex = 16;
            this.proxyLink.TabStop = true;
            this.proxyLink.Text = "Proxy Settings";
            this.proxyLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.proxyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.proxyLink_LinkClicked);
            // 
            // tokenLink
            // 
            this.tokenLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tokenLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tokenLink.AutoSize = true;
            this.tokenLink.BackColor = System.Drawing.Color.Transparent;
            this.tokenLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tokenLink.LinkColor = System.Drawing.Color.Fuchsia;
            this.tokenLink.Location = new System.Drawing.Point(242, 243);
            this.tokenLink.Name = "tokenLink";
            this.tokenLink.Size = new System.Drawing.Size(38, 13);
            this.tokenLink.TabIndex = 18;
            this.tokenLink.TabStop = true;
            this.tokenLink.Text = "Token";
            this.tokenLink.VisitedLinkColor = System.Drawing.Color.Fuchsia;
            this.tokenLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tokenLink_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(296, 267);
            this.Controls.Add(this.tokenLink);
            this.Controls.Add(this.proxyLink);
            this.Controls.Add(this.discordStatusLink);
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
        private System.Windows.Forms.LinkLabel discordStatusLink;
        private System.Windows.Forms.LinkLabel proxyLink;
        private System.Windows.Forms.LinkLabel tokenLink;
    }
}

