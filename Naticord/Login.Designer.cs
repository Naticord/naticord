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
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.welcomeHeader = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.proxyLabel = new System.Windows.Forms.LinkLabel();
            this.welcomeBanner = new System.Windows.Forms.PictureBox();
            this.orLabel = new System.Windows.Forms.Label();
            this.tokenLabel = new System.Windows.Forms.Label();
            this.tokenBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.welcomeBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new System.Drawing.Point(27, 187);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(301, 15);
            this.welcomeLabel.TabIndex = 1;
            this.welcomeLabel.Text = "To get started, sign in with your Discord account below.";
            // 
            // welcomeHeader
            // 
            this.welcomeHeader.AutoSize = true;
            this.welcomeHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.welcomeHeader.Location = new System.Drawing.Point(26, 164);
            this.welcomeHeader.Name = "welcomeHeader";
            this.welcomeHeader.Size = new System.Drawing.Size(162, 21);
            this.welcomeHeader.TabIndex = 2;
            this.welcomeHeader.Text = "Welcome to Naticord!";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLabel.Location = new System.Drawing.Point(43, 219);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(41, 15);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "E-mail";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(27, 251);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(57, 15);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password";
            // 
            // emailBox
            // 
            this.emailBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailBox.Location = new System.Drawing.Point(90, 216);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(377, 23);
            this.emailBox.TabIndex = 5;
            // 
            // passwordBox
            // 
            this.passwordBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBox.Location = new System.Drawing.Point(90, 248);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(377, 23);
            this.passwordBox.TabIndex = 6;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            this.loginButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.Location = new System.Drawing.Point(416, 326);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(72, 23);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 9;
            // 
            // proxyLabel
            // 
            this.proxyLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.proxyLabel.AutoSize = true;
            this.proxyLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proxyLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.proxyLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.proxyLabel.Location = new System.Drawing.Point(12, 330);
            this.proxyLabel.Name = "proxyLabel";
            this.proxyLabel.Size = new System.Drawing.Size(81, 15);
            this.proxyLabel.TabIndex = 10;
            this.proxyLabel.TabStop = true;
            this.proxyLabel.Text = "Proxy Settings";
            this.proxyLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            // 
            // welcomeBanner
            // 
            this.welcomeBanner.Image = global::Naticord.Properties.Resources.banner;
            this.welcomeBanner.Location = new System.Drawing.Point(0, 0);
            this.welcomeBanner.Name = "welcomeBanner";
            this.welcomeBanner.Size = new System.Drawing.Size(500, 150);
            this.welcomeBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.welcomeBanner.TabIndex = 0;
            this.welcomeBanner.TabStop = false;
            // 
            // orLabel
            // 
            this.orLabel.AutoSize = true;
            this.orLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orLabel.Location = new System.Drawing.Point(87, 274);
            this.orLabel.Name = "orLabel";
            this.orLabel.Size = new System.Drawing.Size(81, 15);
            this.orLabel.TabIndex = 1;
            this.orLabel.Text = "or login with...";
            // 
            // tokenLabel
            // 
            this.tokenLabel.AutoSize = true;
            this.tokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tokenLabel.Location = new System.Drawing.Point(43, 296);
            this.tokenLabel.Name = "tokenLabel";
            this.tokenLabel.Size = new System.Drawing.Size(40, 15);
            this.tokenLabel.TabIndex = 4;
            this.tokenLabel.Text = "Token";
            // 
            // tokenBox
            // 
            this.tokenBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tokenBox.Location = new System.Drawing.Point(90, 293);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(377, 23);
            this.tokenBox.TabIndex = 6;
            this.tokenBox.UseSystemPasswordChar = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 359);
            this.Controls.Add(this.proxyLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.tokenBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.tokenLabel);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.welcomeHeader);
            this.Controls.Add(this.orLabel);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.welcomeBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Naticord";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.welcomeBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox welcomeBanner;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Label welcomeHeader;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel proxyLabel;
        private System.Windows.Forms.Label orLabel;
        private System.Windows.Forms.Label tokenLabel;
        private System.Windows.Forms.TextBox tokenBox;
    }
}

