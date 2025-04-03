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
            this.usernameLabel = new WindowsFormsAero.ThemeLabel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.extButton3 = new Naticord.Controls.ExtButton();
            this.extButton2 = new Naticord.Controls.ExtButton();
            this.extButton1 = new Naticord.Controls.ExtButton();
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
            this.usernameLabel.Text = "patricktbp";
            this.usernameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.usernameLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Black;
            this.buttonPanel.Controls.Add(this.extButton3);
            this.buttonPanel.Controls.Add(this.extButton2);
            this.buttonPanel.Controls.Add(this.extButton1);
            this.buttonPanel.Location = new System.Drawing.Point(7, 2);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(300, 30);
            this.buttonPanel.TabIndex = 4;
            // 
            // extButton3
            // 
            this.extButton3.BackColor = System.Drawing.Color.Black;
            this.extButton3.ButtonIcon = global::Naticord.Properties.Resources.github;
            this.extButton3.ButtonLabel = "GitHub";
            this.extButton3.Location = new System.Drawing.Point(183, 0);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(85, 30);
            this.extButton3.TabIndex = 4;
            // 
            // extButton2
            // 
            this.extButton2.BackColor = System.Drawing.Color.Black;
            this.extButton2.ButtonIcon = global::Naticord.Properties.Resources.account;
            this.extButton2.ButtonLabel = "Account";
            this.extButton2.Location = new System.Drawing.Point(92, 0);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(85, 30);
            this.extButton2.TabIndex = 3;
            // 
            // extButton1
            // 
            this.extButton1.BackColor = System.Drawing.Color.Black;
            this.extButton1.ButtonIcon = global::Naticord.Properties.Resources.settings;
            this.extButton1.ButtonLabel = "Settings";
            this.extButton1.Location = new System.Drawing.Point(1, 0);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(85, 30);
            this.extButton1.TabIndex = 2;
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
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naticord";
            this.Load += new System.EventHandler(this.Client_Load);
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilePictureUser;
        private Controls.ExtButton extButton1;
        private WindowsFormsAero.ThemeLabel usernameLabel;
        private System.Windows.Forms.Panel buttonPanel;
        private Controls.ExtButton extButton2;
        private Controls.ExtButton extButton3;
    }
}