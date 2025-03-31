namespace Naticord.Forms
{
    partial class TwoFA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwoFA));
            this.messageTitle = new System.Windows.Forms.Label();
            this.messageContent = new System.Windows.Forms.Label();
            this.authenticationBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.naticordLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // messageTitle
            // 
            this.messageTitle.AutoSize = true;
            this.messageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.messageTitle.Location = new System.Drawing.Point(67, 11);
            this.messageTitle.Name = "messageTitle";
            this.messageTitle.Size = new System.Drawing.Size(172, 21);
            this.messageTitle.TabIndex = 2;
            this.messageTitle.Text = "Authenticate to Discord";
            // 
            // messageContent
            // 
            this.messageContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageContent.Location = new System.Drawing.Point(68, 32);
            this.messageContent.Name = "messageContent";
            this.messageContent.Size = new System.Drawing.Size(254, 32);
            this.messageContent.TabIndex = 3;
            this.messageContent.Text = "Naticord has detected you have 2FA enabled. Please enter your 2FA code to proceed" +
    ".";
            // 
            // authenticationBox
            // 
            this.authenticationBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authenticationBox.Location = new System.Drawing.Point(71, 68);
            this.authenticationBox.Name = "authenticationBox";
            this.authenticationBox.Size = new System.Drawing.Size(251, 23);
            this.authenticationBox.TabIndex = 4;
            this.authenticationBox.UseSystemPasswordChar = true;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(226, 104);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(96, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Authenticate";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // naticordLogo
            // 
            this.naticordLogo.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.naticordLogo.Location = new System.Drawing.Point(13, 11);
            this.naticordLogo.Name = "naticordLogo";
            this.naticordLogo.Size = new System.Drawing.Size(48, 48);
            this.naticordLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.naticordLogo.TabIndex = 1;
            this.naticordLogo.TabStop = false;
            // 
            // TwoFA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 139);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.authenticationBox);
            this.Controls.Add(this.messageContent);
            this.Controls.Add(this.messageTitle);
            this.Controls.Add(this.naticordLogo);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TwoFA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication - Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox naticordLogo;
        public System.Windows.Forms.Label messageTitle;
        public System.Windows.Forms.Label messageContent;
        private System.Windows.Forms.TextBox authenticationBox;
        private System.Windows.Forms.Button okButton;
    }
}