namespace Naticord.Forms.LoginFlows
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
            this.loginBanner = new System.Windows.Forms.PictureBox();
            this.loginDuiRenderer = new DirectUI.Net.Hosting.DirectUIRenderer();
            ((System.ComponentModel.ISupportInitialize)(this.loginBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // loginBanner
            // 
            this.loginBanner.Image = global::Naticord.Properties.Resources.naticord_banner;
            this.loginBanner.Location = new System.Drawing.Point(0, 0);
            this.loginBanner.Name = "loginBanner";
            this.loginBanner.Size = new System.Drawing.Size(412, 100);
            this.loginBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.loginBanner.TabIndex = 1;
            this.loginBanner.TabStop = false;
            // 
            // loginDuiRenderer
            // 
            this.loginDuiRenderer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loginDuiRenderer.Location = new System.Drawing.Point(0, 100);
            this.loginDuiRenderer.Name = "loginDuiRenderer";
            this.loginDuiRenderer.ResId = "loginMain";
            this.loginDuiRenderer.Size = new System.Drawing.Size(412, 178);
            this.loginDuiRenderer.TabIndex = 0;
            this.loginDuiRenderer.XmlFile = "User Interface\\Login.xml";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 278);
            this.Controls.Add(this.loginBanner);
            this.Controls.Add(this.loginDuiRenderer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Naticord!";
            ((System.ComponentModel.ISupportInitialize)(this.loginBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DirectUI.Net.Hosting.DirectUIRenderer loginDuiRenderer;
        private System.Windows.Forms.PictureBox loginBanner;
    }
}