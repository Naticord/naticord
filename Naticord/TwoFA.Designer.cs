namespace Naticord
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
            this.naticordLogo = new System.Windows.Forms.PictureBox();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.noticeTitle = new System.Windows.Forms.Label();
            this.authenticationBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).BeginInit();
            this.SuspendLayout();
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
            // noticeLabel
            // 
            this.noticeLabel.AutoSize = true;
            this.noticeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noticeLabel.Location = new System.Drawing.Point(68, 31);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(337, 30);
            this.noticeLabel.TabIndex = 4;
            this.noticeLabel.Text = "Naticord has detected you have 2FA enabled, please enter your\r\nauthentication cod" +
    "e. ";
            // 
            // noticeTitle
            // 
            this.noticeTitle.AutoSize = true;
            this.noticeTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noticeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.noticeTitle.Location = new System.Drawing.Point(67, 10);
            this.noticeTitle.Name = "noticeTitle";
            this.noticeTitle.Size = new System.Drawing.Size(270, 21);
            this.noticeTitle.TabIndex = 3;
            this.noticeTitle.Text = "Please enter your authentication code";
            // 
            // authenticationBox
            // 
            this.authenticationBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authenticationBox.Location = new System.Drawing.Point(71, 68);
            this.authenticationBox.Name = "authenticationBox";
            this.authenticationBox.Size = new System.Drawing.Size(401, 23);
            this.authenticationBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(386, 98);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(86, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Authenticate";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // TwoFA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 133);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.authenticationBox);
            this.Controls.Add(this.noticeLabel);
            this.Controls.Add(this.noticeTitle);
            this.Controls.Add(this.naticordLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TwoFA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authenticate - Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox naticordLogo;
        public System.Windows.Forms.Label noticeLabel;
        public System.Windows.Forms.Label noticeTitle;
        private System.Windows.Forms.TextBox authenticationBox;
        private System.Windows.Forms.Button okButton;
    }
}