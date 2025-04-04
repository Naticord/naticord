namespace Naticord.Forms
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.settingsTB = new System.Windows.Forms.TabControl();
            this.appearanceTab = new System.Windows.Forms.TabPage();
            this.bdStyleBox = new System.Windows.Forms.ComboBox();
            this.bdStyleLabel = new System.Windows.Forms.Label();
            this.bdrStyleBox = new System.Windows.Forms.ComboBox();
            this.bdrStyleLabel = new System.Windows.Forms.Label();
            this.icnStyleBox = new System.Windows.Forms.ComboBox();
            this.icnStyleLabel = new System.Windows.Forms.Label();
            this.osVerBox = new System.Windows.Forms.ComboBox();
            this.osVerLabel = new System.Windows.Forms.Label();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.ghLink = new System.Windows.Forms.LinkLabel();
            this.aboutNaticordInfo = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.quoteLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.appLabel = new System.Windows.Forms.Label();
            this.creditsTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.appearanceQuote = new System.Windows.Forms.Label();
            this.appearanceLabel = new System.Windows.Forms.Label();
            this.creditsQuote = new System.Windows.Forms.Label();
            this.creditsLabel = new System.Windows.Forms.Label();
            this.appearanceIcon = new System.Windows.Forms.PictureBox();
            this.catBox = new System.Windows.Forms.PictureBox();
            this.appIcon = new System.Windows.Forms.PictureBox();
            this.creditsIcon = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.settingsTB.SuspendLayout();
            this.appearanceTab.SuspendLayout();
            this.aboutTab.SuspendLayout();
            this.creditsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appearanceIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.catBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.creditsIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsTB
            // 
            this.settingsTB.Controls.Add(this.appearanceTab);
            this.settingsTB.Controls.Add(this.aboutTab);
            this.settingsTB.Controls.Add(this.creditsTab);
            this.settingsTB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsTB.Location = new System.Drawing.Point(12, 12);
            this.settingsTB.Name = "settingsTB";
            this.settingsTB.SelectedIndex = 0;
            this.settingsTB.Size = new System.Drawing.Size(360, 402);
            this.settingsTB.TabIndex = 0;
            // 
            // appearanceTab
            // 
            this.appearanceTab.Controls.Add(this.clearButton);
            this.appearanceTab.Controls.Add(this.appearanceQuote);
            this.appearanceTab.Controls.Add(this.appearanceLabel);
            this.appearanceTab.Controls.Add(this.appearanceIcon);
            this.appearanceTab.Controls.Add(this.bdStyleBox);
            this.appearanceTab.Controls.Add(this.bdStyleLabel);
            this.appearanceTab.Controls.Add(this.bdrStyleBox);
            this.appearanceTab.Controls.Add(this.bdrStyleLabel);
            this.appearanceTab.Controls.Add(this.icnStyleBox);
            this.appearanceTab.Controls.Add(this.icnStyleLabel);
            this.appearanceTab.Controls.Add(this.osVerBox);
            this.appearanceTab.Controls.Add(this.osVerLabel);
            this.appearanceTab.Location = new System.Drawing.Point(4, 24);
            this.appearanceTab.Name = "appearanceTab";
            this.appearanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.appearanceTab.Size = new System.Drawing.Size(352, 374);
            this.appearanceTab.TabIndex = 0;
            this.appearanceTab.Text = "Appearance";
            this.appearanceTab.UseVisualStyleBackColor = true;
            // 
            // bdStyleBox
            // 
            this.bdStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bdStyleBox.FormattingEnabled = true;
            this.bdStyleBox.Items.AddRange(new object[] {
            "Aero",
            "Acrylic",
            "Mica",
            "Mica (Alt)"});
            this.bdStyleBox.Location = new System.Drawing.Point(91, 172);
            this.bdStyleBox.Name = "bdStyleBox";
            this.bdStyleBox.Size = new System.Drawing.Size(248, 23);
            this.bdStyleBox.TabIndex = 7;
            this.bdStyleBox.SelectedIndexChanged += new System.EventHandler(this.bdStyleBox_SelectedIndexChanged);
            // 
            // bdStyleLabel
            // 
            this.bdStyleLabel.AutoSize = true;
            this.bdStyleLabel.Location = new System.Drawing.Point(28, 175);
            this.bdStyleLabel.Name = "bdStyleLabel";
            this.bdStyleLabel.Size = new System.Drawing.Size(57, 15);
            this.bdStyleLabel.TabIndex = 6;
            this.bdStyleLabel.Text = "Backdrop";
            // 
            // bdrStyleBox
            // 
            this.bdrStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bdrStyleBox.FormattingEnabled = true;
            this.bdrStyleBox.Items.AddRange(new object[] {
            "Slim",
            "Thick"});
            this.bdrStyleBox.Location = new System.Drawing.Point(91, 138);
            this.bdrStyleBox.Name = "bdrStyleBox";
            this.bdrStyleBox.Size = new System.Drawing.Size(248, 23);
            this.bdrStyleBox.TabIndex = 5;
            this.bdrStyleBox.SelectedIndexChanged += new System.EventHandler(this.bdrStyleBox_SelectedIndexChanged);
            // 
            // bdrStyleLabel
            // 
            this.bdrStyleLabel.AutoSize = true;
            this.bdrStyleLabel.Location = new System.Drawing.Point(15, 141);
            this.bdrStyleLabel.Name = "bdrStyleLabel";
            this.bdrStyleLabel.Size = new System.Drawing.Size(70, 15);
            this.bdrStyleLabel.TabIndex = 4;
            this.bdrStyleLabel.Text = "Border Style";
            // 
            // icnStyleBox
            // 
            this.icnStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.icnStyleBox.FormattingEnabled = true;
            this.icnStyleBox.Items.AddRange(new object[] {
            "Modern",
            "Legacy"});
            this.icnStyleBox.Location = new System.Drawing.Point(91, 104);
            this.icnStyleBox.Name = "icnStyleBox";
            this.icnStyleBox.Size = new System.Drawing.Size(248, 23);
            this.icnStyleBox.TabIndex = 3;
            this.icnStyleBox.SelectedIndexChanged += new System.EventHandler(this.icnStyleBox_SelectedIndexChanged);
            // 
            // icnStyleLabel
            // 
            this.icnStyleLabel.AutoSize = true;
            this.icnStyleLabel.Location = new System.Drawing.Point(27, 107);
            this.icnStyleLabel.Name = "icnStyleLabel";
            this.icnStyleLabel.Size = new System.Drawing.Size(58, 15);
            this.icnStyleLabel.TabIndex = 2;
            this.icnStyleLabel.Text = "Icon Style";
            // 
            // osVerBox
            // 
            this.osVerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.osVerBox.FormattingEnabled = true;
            this.osVerBox.Items.AddRange(new object[] {
            "Windows 11",
            "Windows 10",
            "Windows 7 - 8.1"});
            this.osVerBox.Location = new System.Drawing.Point(91, 70);
            this.osVerBox.Name = "osVerBox";
            this.osVerBox.Size = new System.Drawing.Size(248, 23);
            this.osVerBox.TabIndex = 1;
            // 
            // osVerLabel
            // 
            this.osVerLabel.AutoSize = true;
            this.osVerLabel.Location = new System.Drawing.Point(22, 73);
            this.osVerLabel.Name = "osVerLabel";
            this.osVerLabel.Size = new System.Drawing.Size(63, 15);
            this.osVerLabel.TabIndex = 0;
            this.osVerLabel.Text = "OS Version";
            // 
            // aboutTab
            // 
            this.aboutTab.Controls.Add(this.catBox);
            this.aboutTab.Controls.Add(this.ghLink);
            this.aboutTab.Controls.Add(this.aboutNaticordInfo);
            this.aboutTab.Controls.Add(this.versionLabel);
            this.aboutTab.Controls.Add(this.quoteLabel);
            this.aboutTab.Controls.Add(this.authorLabel);
            this.aboutTab.Controls.Add(this.appLabel);
            this.aboutTab.Controls.Add(this.appIcon);
            this.aboutTab.Location = new System.Drawing.Point(4, 24);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTab.Size = new System.Drawing.Size(352, 374);
            this.aboutTab.TabIndex = 1;
            this.aboutTab.Text = "About";
            this.aboutTab.UseVisualStyleBackColor = true;
            // 
            // ghLink
            // 
            this.ghLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.ghLink.AutoSize = true;
            this.ghLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ghLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.ghLink.Location = new System.Drawing.Point(15, 345);
            this.ghLink.Name = "ghLink";
            this.ghLink.Size = new System.Drawing.Size(90, 15);
            this.ghLink.TabIndex = 6;
            this.ghLink.TabStop = true;
            this.ghLink.Text = "View on GitHub";
            this.ghLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            // 
            // aboutNaticordInfo
            // 
            this.aboutNaticordInfo.Location = new System.Drawing.Point(12, 66);
            this.aboutNaticordInfo.Name = "aboutNaticordInfo";
            this.aboutNaticordInfo.Size = new System.Drawing.Size(328, 139);
            this.aboutNaticordInfo.TabIndex = 5;
            this.aboutNaticordInfo.Text = resources.GetString("aboutNaticordInfo.Text");
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(241, 345);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(96, 15);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Version 1.0.0 (B2)";
            // 
            // quoteLabel
            // 
            this.quoteLabel.AutoSize = true;
            this.quoteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quoteLabel.Location = new System.Drawing.Point(71, 39);
            this.quoteLabel.Name = "quoteLabel";
            this.quoteLabel.Size = new System.Drawing.Size(154, 15);
            this.quoteLabel.TabIndex = 3;
            this.quoteLabel.Text = "A native Discord expierence.";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(135, 21);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(77, 15);
            this.authorLabel.TabIndex = 2;
            this.authorLabel.Text = "by patricktbp";
            // 
            // appLabel
            // 
            this.appLabel.AutoSize = true;
            this.appLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.appLabel.Location = new System.Drawing.Point(70, 16);
            this.appLabel.Name = "appLabel";
            this.appLabel.Size = new System.Drawing.Size(70, 21);
            this.appLabel.TabIndex = 1;
            this.appLabel.Text = "Naticord";
            // 
            // creditsTab
            // 
            this.creditsTab.Controls.Add(this.creditsQuote);
            this.creditsTab.Controls.Add(this.creditsLabel);
            this.creditsTab.Controls.Add(this.creditsIcon);
            this.creditsTab.Controls.Add(this.label1);
            this.creditsTab.Location = new System.Drawing.Point(4, 24);
            this.creditsTab.Name = "creditsTab";
            this.creditsTab.Padding = new System.Windows.Forms.Padding(3);
            this.creditsTab.Size = new System.Drawing.Size(352, 374);
            this.creditsTab.TabIndex = 2;
            this.creditsTab.Text = "Credits";
            this.creditsTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 195);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // appearanceQuote
            // 
            this.appearanceQuote.AutoSize = true;
            this.appearanceQuote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appearanceQuote.Location = new System.Drawing.Point(71, 39);
            this.appearanceQuote.Name = "appearanceQuote";
            this.appearanceQuote.Size = new System.Drawing.Size(196, 15);
            this.appearanceQuote.TabIndex = 11;
            this.appearanceQuote.Text = "Change the appearance of Naticord";
            // 
            // appearanceLabel
            // 
            this.appearanceLabel.AutoSize = true;
            this.appearanceLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appearanceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.appearanceLabel.Location = new System.Drawing.Point(70, 16);
            this.appearanceLabel.Name = "appearanceLabel";
            this.appearanceLabel.Size = new System.Drawing.Size(92, 21);
            this.appearanceLabel.TabIndex = 9;
            this.appearanceLabel.Text = "Appearance";
            // 
            // creditsQuote
            // 
            this.creditsQuote.AutoSize = true;
            this.creditsQuote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditsQuote.Location = new System.Drawing.Point(71, 39);
            this.creditsQuote.Name = "creditsQuote";
            this.creditsQuote.Size = new System.Drawing.Size(209, 15);
            this.creditsQuote.TabIndex = 14;
            this.creditsQuote.Text = "People / Projects that helped Naticord!";
            // 
            // creditsLabel
            // 
            this.creditsLabel.AutoSize = true;
            this.creditsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(158)))));
            this.creditsLabel.Location = new System.Drawing.Point(70, 16);
            this.creditsLabel.Name = "creditsLabel";
            this.creditsLabel.Size = new System.Drawing.Size(59, 21);
            this.creditsLabel.TabIndex = 13;
            this.creditsLabel.Text = "Credits";
            // 
            // appearanceIcon
            // 
            this.appearanceIcon.Image = global::Naticord.Properties.Resources.appearance;
            this.appearanceIcon.Location = new System.Drawing.Point(15, 11);
            this.appearanceIcon.Name = "appearanceIcon";
            this.appearanceIcon.Size = new System.Drawing.Size(48, 48);
            this.appearanceIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.appearanceIcon.TabIndex = 8;
            this.appearanceIcon.TabStop = false;
            // 
            // catBox
            // 
            this.catBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.catBox.Image = global::Naticord.Properties.Resources.cat;
            this.catBox.Location = new System.Drawing.Point(18, 213);
            this.catBox.Name = "catBox";
            this.catBox.Size = new System.Drawing.Size(319, 119);
            this.catBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.catBox.TabIndex = 7;
            this.catBox.TabStop = false;
            // 
            // appIcon
            // 
            this.appIcon.Image = global::Naticord.Properties.Resources.naticord_logo_64;
            this.appIcon.Location = new System.Drawing.Point(15, 11);
            this.appIcon.Name = "appIcon";
            this.appIcon.Size = new System.Drawing.Size(48, 48);
            this.appIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.appIcon.TabIndex = 0;
            this.appIcon.TabStop = false;
            // 
            // creditsIcon
            // 
            this.creditsIcon.Image = global::Naticord.Properties.Resources.credits;
            this.creditsIcon.Location = new System.Drawing.Point(15, 11);
            this.creditsIcon.Name = "creditsIcon";
            this.creditsIcon.Size = new System.Drawing.Size(48, 48);
            this.creditsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.creditsIcon.TabIndex = 12;
            this.creditsIcon.TabStop = false;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(245, 201);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(94, 23);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Reset settings";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 426);
            this.Controls.Add(this.settingsTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings - Naticord";
            this.settingsTB.ResumeLayout(false);
            this.appearanceTab.ResumeLayout(false);
            this.appearanceTab.PerformLayout();
            this.aboutTab.ResumeLayout(false);
            this.aboutTab.PerformLayout();
            this.creditsTab.ResumeLayout(false);
            this.creditsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appearanceIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.catBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.creditsIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTB;
        private System.Windows.Forms.TabPage appearanceTab;
        private System.Windows.Forms.ComboBox osVerBox;
        private System.Windows.Forms.Label osVerLabel;
        private System.Windows.Forms.ComboBox bdStyleBox;
        private System.Windows.Forms.Label bdStyleLabel;
        private System.Windows.Forms.ComboBox bdrStyleBox;
        private System.Windows.Forms.Label bdrStyleLabel;
        private System.Windows.Forms.ComboBox icnStyleBox;
        private System.Windows.Forms.Label icnStyleLabel;
        private System.Windows.Forms.TabPage aboutTab;
        private System.Windows.Forms.Label appLabel;
        private System.Windows.Forms.PictureBox appIcon;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label quoteLabel;
        private System.Windows.Forms.Label aboutNaticordInfo;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel ghLink;
        private System.Windows.Forms.PictureBox catBox;
        private System.Windows.Forms.TabPage creditsTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label appearanceQuote;
        private System.Windows.Forms.Label appearanceLabel;
        private System.Windows.Forms.PictureBox appearanceIcon;
        private System.Windows.Forms.Label creditsQuote;
        private System.Windows.Forms.Label creditsLabel;
        private System.Windows.Forms.PictureBox creditsIcon;
        private System.Windows.Forms.Button clearButton;
    }
}