namespace Naticord
{
    partial class TokenLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenLogin));
            this.tokenBox = new System.Windows.Forms.TextBox();
            this.signinButton = new System.Windows.Forms.Button();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // tokenBox
            // 
            this.tokenBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tokenBox.Location = new System.Drawing.Point(10, 138);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(187, 25);
            this.tokenBox.TabIndex = 7;
            // 
            // signinButton
            // 
            this.signinButton.Location = new System.Drawing.Point(202, 138);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(83, 25);
            this.signinButton.TabIndex = 8;
            this.signinButton.Text = "Sign in";
            this.signinButton.UseVisualStyleBackColor = true;
            this.signinButton.Click += new System.EventHandler(this.signinButton_Click);
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Transparent;
            this.profilepicture.Image = global::Naticord.Properties.Resources.icon;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.Location = new System.Drawing.Point(109, 12);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(86, 87);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter your token";
            // 
            // TokenLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 172);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.tokenBox);
            this.Controls.Add(this.profilepicture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TokenLogin";
            this.Text = "Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.TextBox tokenBox;
        private System.Windows.Forms.Button signinButton;
        private System.Windows.Forms.Label label1;
    }
}

