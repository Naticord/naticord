
namespace Naticord.Forms
{
    partial class Token
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Token));
            this.okButton = new System.Windows.Forms.Button();
            this.tokenBox = new System.Windows.Forms.TextBox();
            this.naticordLogo = new System.Windows.Forms.PictureBox();
            this.messageTitle = new System.Windows.Forms.Label();
            this.messageContent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(226, 104);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(96, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Authenticate";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tokenBox
            // 
            this.tokenBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tokenBox.Location = new System.Drawing.Point(71, 68);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(249, 23);
            this.tokenBox.TabIndex = 7;
            // 
            // naticordLogo
            // 
            this.naticordLogo.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.naticordLogo.Location = new System.Drawing.Point(13, 11);
            this.naticordLogo.Name = "naticordLogo";
            this.naticordLogo.Size = new System.Drawing.Size(48, 48);
            this.naticordLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.naticordLogo.TabIndex = 8;
            this.naticordLogo.TabStop = false;
            // 
            // messageTitle
            // 
            this.messageTitle.AutoSize = true;
            this.messageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.messageTitle.Location = new System.Drawing.Point(67, 11);
            this.messageTitle.Name = "messageTitle";
            this.messageTitle.Size = new System.Drawing.Size(91, 21);
            this.messageTitle.TabIndex = 9;
            this.messageTitle.Text = "Token login";
            // 
            // messageContent
            // 
            this.messageContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageContent.Location = new System.Drawing.Point(68, 32);
            this.messageContent.Name = "messageContent";
            this.messageContent.Size = new System.Drawing.Size(254, 32);
            this.messageContent.TabIndex = 10;
            this.messageContent.Text = "You have requested to login using the token.\r\nPlease enter your token in the box." +
    "";
            // 
            // Token
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 137);
            this.Controls.Add(this.messageContent);
            this.Controls.Add(this.messageTitle);
            this.Controls.Add(this.naticordLogo);
            this.Controls.Add(this.tokenBox);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Token";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Token login - Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox tokenBox;
        private System.Windows.Forms.PictureBox naticordLogo;
        public System.Windows.Forms.Label messageTitle;
        public System.Windows.Forms.Label messageContent;
    }
}