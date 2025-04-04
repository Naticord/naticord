namespace Naticord.Controls
{
    partial class ExtButton
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
            this.buttonIcon = new System.Windows.Forms.PictureBox();
            this.buttonLabel = new WindowsFormsAero.ThemeLabel();
            ((System.ComponentModel.ISupportInitialize)(this.buttonIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonIcon
            // 
            this.buttonIcon.BackColor = System.Drawing.Color.Transparent;
            this.buttonIcon.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.buttonIcon.Location = new System.Drawing.Point(0, 4);
            this.buttonIcon.Name = "buttonIcon";
            this.buttonIcon.Size = new System.Drawing.Size(22, 22);
            this.buttonIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonIcon.TabIndex = 0;
            this.buttonIcon.TabStop = false;
            // 
            // buttonLabel
            // 
            this.buttonLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLabel.GlowSize = 0;
            this.buttonLabel.Location = new System.Drawing.Point(28, 4);
            this.buttonLabel.Name = "buttonLabel";
            this.buttonLabel.Size = new System.Drawing.Size(57, 23);
            this.buttonLabel.TabIndex = 1;
            this.buttonLabel.Text = "Button";
            this.buttonLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.buttonLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // ExtButton
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonLabel);
            this.Controls.Add(this.buttonIcon);
            this.Name = "ExtButton";
            this.Size = new System.Drawing.Size(88, 30);
            ((System.ComponentModel.ISupportInitialize)(this.buttonIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox buttonIcon;
        private WindowsFormsAero.ThemeLabel buttonLabel;
    }
}
