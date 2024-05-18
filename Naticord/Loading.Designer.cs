namespace Naticord
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loadingProgress = new System.Windows.Forms.ProgressBar();
            this.loadingQuotes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Naticord.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(107, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 97);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // loadingProgress
            // 
            this.loadingProgress.Location = new System.Drawing.Point(74, 139);
            this.loadingProgress.Name = "loadingProgress";
            this.loadingProgress.Size = new System.Drawing.Size(166, 23);
            this.loadingProgress.TabIndex = 1;
            // 
            // loadingQuotes
            // 
            this.loadingQuotes.AutoSize = true;
            this.loadingQuotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingQuotes.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.loadingQuotes.Location = new System.Drawing.Point(138, 168);
            this.loadingQuotes.Name = "loadingQuotes";
            this.loadingQuotes.Size = new System.Drawing.Size(38, 15);
            this.loadingQuotes.TabIndex = 2;
            this.loadingQuotes.Text = "quote";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 223);
            this.Controls.Add(this.loadingQuotes);
            this.Controls.Add(this.loadingProgress);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loading";
            this.Text = "Loading";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar loadingProgress;
        private System.Windows.Forms.Label loadingQuotes;
    }
}