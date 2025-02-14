namespace Naticord
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.naticordLogo = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorTitleLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.creditsLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.donateLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // naticordLogo
            // 
            this.naticordLogo.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.naticordLogo.Location = new System.Drawing.Point(17, 16);
            this.naticordLogo.Name = "naticordLogo";
            this.naticordLogo.Size = new System.Drawing.Size(50, 50);
            this.naticordLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.naticordLogo.TabIndex = 0;
            this.naticordLogo.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.titleLabel.Location = new System.Drawing.Point(74, 21);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(70, 21);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Naticord";
            // 
            // authorTitleLabel
            // 
            this.authorTitleLabel.AutoSize = true;
            this.authorTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorTitleLabel.ForeColor = System.Drawing.Color.Black;
            this.authorTitleLabel.Location = new System.Drawing.Point(139, 26);
            this.authorTitleLabel.Name = "authorTitleLabel";
            this.authorTitleLabel.Size = new System.Drawing.Size(77, 15);
            this.authorTitleLabel.TabIndex = 2;
            this.authorTitleLabel.Text = "by patricktbp";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.Black;
            this.versionLabel.Location = new System.Drawing.Point(75, 43);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(108, 15);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Version: <vernum>";
            // 
            // noticeLabel
            // 
            this.noticeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noticeLabel.Location = new System.Drawing.Point(14, 74);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(311, 49);
            this.noticeLabel.TabIndex = 4;
            this.noticeLabel.Text = "Naticord is a Win32 (Native) Discord client developed in C#. I couldn\'t make this" +
    " all by my self so I have to thank the community for this!\r\n";
            // 
            // creditsLabel
            // 
            this.creditsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditsLabel.Location = new System.Drawing.Point(14, 127);
            this.creditsLabel.Name = "creditsLabel";
            this.creditsLabel.Size = new System.Drawing.Size(311, 94);
            this.creditsLabel.TabIndex = 5;
            this.creditsLabel.Text = resources.GetString("creditsLabel.Text");
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(260, 243);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(65, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // githubLink
            // 
            this.githubLink.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.githubLink.AutoSize = true;
            this.githubLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.githubLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.githubLink.LinkColor = System.Drawing.Color.RoyalBlue;
            this.githubLink.Location = new System.Drawing.Point(14, 247);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(101, 15);
            this.githubLink.TabIndex = 7;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "GitHub repository";
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLink_LinkClicked);
            // 
            // donateLink
            // 
            this.donateLink.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.donateLink.AutoSize = true;
            this.donateLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.donateLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.donateLink.LinkColor = System.Drawing.Color.RoyalBlue;
            this.donateLink.Location = new System.Drawing.Point(121, 247);
            this.donateLink.Name = "donateLink";
            this.donateLink.Size = new System.Drawing.Size(48, 15);
            this.donateLink.TabIndex = 8;
            this.donateLink.TabStop = true;
            this.donateLink.Text = "Donate!";
            this.donateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.donateLink_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(337, 275);
            this.Controls.Add(this.donateLink);
            this.Controls.Add(this.githubLink);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.creditsLabel);
            this.Controls.Add(this.noticeLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.authorTitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.naticordLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox naticordLogo;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorTitleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label noticeLabel;
        private System.Windows.Forms.Label creditsLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.LinkLabel githubLink;
        private System.Windows.Forms.LinkLabel donateLink;
    }
}