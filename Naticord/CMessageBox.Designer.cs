namespace Naticord
{
    partial class CMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMessageBox));
            this.naticordLogo = new System.Windows.Forms.PictureBox();
            this.messageTitle = new System.Windows.Forms.Label();
            this.messageContent = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
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
            this.naticordLogo.TabIndex = 0;
            this.naticordLogo.TabStop = false;
            // 
            // messageTitle
            // 
            this.messageTitle.AutoSize = true;
            this.messageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.messageTitle.Location = new System.Drawing.Point(67, 15);
            this.messageTitle.Name = "messageTitle";
            this.messageTitle.Size = new System.Drawing.Size(100, 21);
            this.messageTitle.TabIndex = 1;
            this.messageTitle.Text = "messageTitle";
            // 
            // messageContent
            // 
            this.messageContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageContent.Location = new System.Drawing.Point(68, 37);
            this.messageContent.Name = "messageContent";
            this.messageContent.Size = new System.Drawing.Size(404, 49);
            this.messageContent.TabIndex = 2;
            this.messageContent.Text = "messageContent";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(407, 89);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(65, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyButton.Location = new System.Drawing.Point(296, 89);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(105, 23);
            this.copyButton.TabIndex = 4;
            this.copyButton.Text = "Copy message";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // CMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 124);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.messageContent);
            this.Controls.Add(this.messageTitle);
            this.Controls.Add(this.naticordLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CMessageBox";
            ((System.ComponentModel.ISupportInitialize)(this.naticordLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox naticordLogo;
        public System.Windows.Forms.Label messageTitle;
        public System.Windows.Forms.Label messageContent;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button copyButton;
    }
}