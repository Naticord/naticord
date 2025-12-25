
namespace Naticord.Forms
{
    partial class UserViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserViewer));
            this.userPicture = new System.Windows.Forms.PictureBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.userExtraInfo = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.bioLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // userPicture
            // 
            this.userPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userPicture.Location = new System.Drawing.Point(16, 12);
            this.userPicture.Name = "userPicture";
            this.userPicture.Size = new System.Drawing.Size(48, 48);
            this.userPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userPicture.TabIndex = 1;
            this.userPicture.TabStop = false;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.White;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(156)))));
            this.usernameLabel.Location = new System.Drawing.Point(72, 17);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(38, 21);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "N/A";
            // 
            // userExtraInfo
            // 
            this.userExtraInfo.AutoSize = true;
            this.userExtraInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userExtraInfo.Location = new System.Drawing.Point(73, 40);
            this.userExtraInfo.Name = "userExtraInfo";
            this.userExtraInfo.Size = new System.Drawing.Size(62, 15);
            this.userExtraInfo.TabIndex = 3;
            this.userExtraInfo.Text = "N/A - N/A";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.BackColor = System.Drawing.Color.White;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(156)))));
            this.descriptionLabel.Location = new System.Drawing.Point(12, 66);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(75, 21);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "N/A\'s bio";
            // 
            // bioLabel
            // 
            this.bioLabel.AutoSize = true;
            this.bioLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bioLabel.Location = new System.Drawing.Point(13, 89);
            this.bioLabel.Name = "bioLabel";
            this.bioLabel.Size = new System.Drawing.Size(95, 15);
            this.bioLabel.TabIndex = 5;
            this.bioLabel.Text = "No bio available.";
            // 
            // UserViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 112);
            this.Controls.Add(this.bioLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.userExtraInfo);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.userPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "(USERNAME) - Naticord";
            ((System.ComponentModel.ISupportInitialize)(this.userPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox userPicture;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label userExtraInfo;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label bioLabel;
    }
}