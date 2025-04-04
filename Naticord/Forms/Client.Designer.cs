namespace Naticord.Forms
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.ghButton = new Naticord.Controls.ExtButton();
            this.accountButton = new Naticord.Controls.ExtButton();
            this.settingsButton = new Naticord.Controls.ExtButton();
            this.profilePictureUser = new System.Windows.Forms.PictureBox();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureUser)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(776, 4);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.usernameLabel.Size = new System.Drawing.Size(199, 25);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "usernameLabel";
            this.usernameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.usernameLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Black;
            this.buttonPanel.Controls.Add(this.ghButton);
            this.buttonPanel.Controls.Add(this.accountButton);
            this.buttonPanel.Controls.Add(this.settingsButton);
            this.buttonPanel.Location = new System.Drawing.Point(8, 2);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(300, 30);
            this.buttonPanel.TabIndex = 4;
            // 
            // ghButton
            // 
            this.ghButton.BackColor = System.Drawing.Color.Black;
            this.ghButton.ButtonIcon = global::Naticord.Properties.Resources.github;
            this.ghButton.ButtonLabel = "GitHub";
            this.ghButton.Location = new System.Drawing.Point(183, 0);
            this.ghButton.Name = "ghButton";
            this.ghButton.Size = new System.Drawing.Size(85, 30);
            this.ghButton.TabIndex = 4;
            // 
            // accountButton
            // 
            this.accountButton.BackColor = System.Drawing.Color.Black;
            this.accountButton.ButtonIcon = global::Naticord.Properties.Resources.account;
            this.accountButton.ButtonLabel = "Account";
            this.accountButton.Location = new System.Drawing.Point(92, 0);
            this.accountButton.Name = "accountButton";
            this.accountButton.Size = new System.Drawing.Size(85, 30);
            this.accountButton.TabIndex = 3;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.Black;
            this.settingsButton.ButtonIcon = global::Naticord.Properties.Resources.settings;
            this.settingsButton.ButtonLabel = "Settings";
            this.settingsButton.Location = new System.Drawing.Point(1, 0);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(85, 30);
            this.settingsButton.TabIndex = 2;
            // 
            // profilePictureUser
            // 
            this.profilePictureUser.BackColor = System.Drawing.Color.Black;
            this.profilePictureUser.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.profilePictureUser.Location = new System.Drawing.Point(981, 4);
            this.profilePictureUser.Name = "profilePictureUser";
            this.profilePictureUser.Size = new System.Drawing.Size(25, 25);
            this.profilePictureUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureUser.TabIndex = 0;
            this.profilePictureUser.TabStop = false;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 591);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilePictureUser);
            this.GlassMargins = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord";
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox profilePictureUser;
        public Controls.ExtButton settingsButton;
        public WindowsFormsAero.ThemeLabel usernameLabel;
        public System.Windows.Forms.Panel buttonPanel;
        public Controls.ExtButton accountButton;
        public Controls.ExtButton ghButton;
    }
}