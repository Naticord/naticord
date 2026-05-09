namespace Naticord.UserControls
{
    partial class GlassExtendedButton
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
            this.buttonLabel = new Naticord.UserControls.AeroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.buttonIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonIcon
            // 
            this.buttonIcon.Image = global::Naticord.Properties.Resources.light_bulb_skeuo;
            this.buttonIcon.Location = new System.Drawing.Point(0, 2);
            this.buttonIcon.Name = "buttonIcon";
            this.buttonIcon.Size = new System.Drawing.Size(16, 16);
            this.buttonIcon.TabIndex = 0;
            this.buttonIcon.TabStop = false;
            // 
            // buttonLabel
            // 
            this.buttonLabel.FixClippingBug = true;
            this.buttonLabel.Location = new System.Drawing.Point(21, 0);
            this.buttonLabel.Name = "buttonLabel";
            this.buttonLabel.Size = new System.Drawing.Size(126, 20);
            this.buttonLabel.TabIndex = 1;
            this.buttonLabel.Text = "Example button";
            this.buttonLabel.TextAlignVertical = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            // 
            // GlassExtendedButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.buttonLabel);
            this.Controls.Add(this.buttonIcon);
            this.Name = "GlassExtendedButton";
            this.Size = new System.Drawing.Size(150, 20);
            ((System.ComponentModel.ISupportInitialize)(this.buttonIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox buttonIcon;
        private AeroLabel buttonLabel;
    }
}
