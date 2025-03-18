namespace Naticord
{
    partial class FSControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.profilePictureItem = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureItem)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(22, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(65, 15);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "nameLabel";
            // 
            // profilePictureItem
            // 
            this.profilePictureItem.Image = global::Naticord.Properties.Resources.discord_profile;
            this.profilePictureItem.Location = new System.Drawing.Point(3, 2);
            this.profilePictureItem.Name = "profilePictureItem";
            this.profilePictureItem.Size = new System.Drawing.Size(17, 17);
            this.profilePictureItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureItem.TabIndex = 0;
            this.profilePictureItem.TabStop = false;
            // 
            // FSControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.profilePictureItem);
            this.Name = "FSControl";
            this.Size = new System.Drawing.Size(135, 22);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox profilePictureItem;
        public System.Windows.Forms.Label nameLabel;
    }
}
