namespace Naticord.Forms.SetupFlows
{
    partial class AppearancePage
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
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
        private void InitializeComponent()
        {
            this.appearanceHeaderRenderer = new DirectUI.Net.Hosting.DirectUIRenderer();
            this.cutOffPanel = new System.Windows.Forms.Panel();
            this.layoutBox = new System.Windows.Forms.ComboBox();
            this.layoutNoticeLabel = new System.Windows.Forms.Label();
            this.modernRadio = new System.Windows.Forms.RadioButton();
            this.skeuoRadio = new System.Windows.Forms.RadioButton();
            this.serversIcon = new System.Windows.Forms.PictureBox();
            this.dmIcon = new System.Windows.Forms.PictureBox();
            this.githubIcon = new System.Windows.Forms.PictureBox();
            this.settingsIcon = new System.Windows.Forms.PictureBox();
            this.accountIcon = new System.Windows.Forms.PictureBox();
            this.icnPreviewLabel = new System.Windows.Forms.Label();
            this.cutOffPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serversIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dmIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // appearanceHeaderRenderer
            // 
            this.appearanceHeaderRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appearanceHeaderRenderer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.appearanceHeaderRenderer.Location = new System.Drawing.Point(0, 0);
            this.appearanceHeaderRenderer.Name = "appearanceHeaderRenderer";
            this.appearanceHeaderRenderer.ResId = "appearanceMain";
            this.appearanceHeaderRenderer.Size = new System.Drawing.Size(800, 75);
            this.appearanceHeaderRenderer.TabIndex = 0;
            this.appearanceHeaderRenderer.XmlFile = "User Interface\\SetupFlow\\AppearanceHeader.xml";
            // 
            // cutOffPanel
            // 
            this.cutOffPanel.Controls.Add(this.layoutBox);
            this.cutOffPanel.Controls.Add(this.layoutNoticeLabel);
            this.cutOffPanel.Controls.Add(this.modernRadio);
            this.cutOffPanel.Controls.Add(this.skeuoRadio);
            this.cutOffPanel.Controls.Add(this.serversIcon);
            this.cutOffPanel.Controls.Add(this.dmIcon);
            this.cutOffPanel.Controls.Add(this.githubIcon);
            this.cutOffPanel.Controls.Add(this.settingsIcon);
            this.cutOffPanel.Controls.Add(this.accountIcon);
            this.cutOffPanel.Controls.Add(this.icnPreviewLabel);
            this.cutOffPanel.Location = new System.Drawing.Point(20, 75);
            this.cutOffPanel.Name = "cutOffPanel";
            this.cutOffPanel.Size = new System.Drawing.Size(760, 334);
            this.cutOffPanel.TabIndex = 1;
            // 
            // layoutBox
            // 
            this.layoutBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.layoutBox.FormattingEnabled = true;
            this.layoutBox.Items.AddRange(new object[] {
            "Windows 8.1 - 7",
            "Windows 11 - 10"});
            this.layoutBox.Location = new System.Drawing.Point(1, 120);
            this.layoutBox.Name = "layoutBox";
            this.layoutBox.Size = new System.Drawing.Size(182, 21);
            this.layoutBox.TabIndex = 9;
            this.layoutBox.SelectedIndexChanged += new System.EventHandler(this.layoutBox_SelectedIndexChanged);
            // 
            // layoutNoticeLabel
            // 
            this.layoutNoticeLabel.AutoSize = true;
            this.layoutNoticeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.layoutNoticeLabel.Location = new System.Drawing.Point(-2, 88);
            this.layoutNoticeLabel.Name = "layoutNoticeLabel";
            this.layoutNoticeLabel.Size = new System.Drawing.Size(401, 26);
            this.layoutNoticeLabel.TabIndex = 8;
            this.layoutNoticeLabel.Text = "You might want to configure how Naticord handles the spacing between elements.\r\nT" +
    "his is based on your operating system, and not your system theme.";
            // 
            // modernRadio
            // 
            this.modernRadio.AutoSize = true;
            this.modernRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.modernRadio.Location = new System.Drawing.Point(1, 64);
            this.modernRadio.Name = "modernRadio";
            this.modernRadio.Size = new System.Drawing.Size(95, 18);
            this.modernRadio.TabIndex = 7;
            this.modernRadio.TabStop = true;
            this.modernRadio.Text = "Modern icons";
            this.modernRadio.UseVisualStyleBackColor = true;
            this.modernRadio.CheckedChanged += new System.EventHandler(this.modernRadio_CheckedChanged);
            // 
            // skeuoRadio
            // 
            this.skeuoRadio.AutoSize = true;
            this.skeuoRadio.Checked = true;
            this.skeuoRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.skeuoRadio.Location = new System.Drawing.Point(1, 41);
            this.skeuoRadio.Name = "skeuoRadio";
            this.skeuoRadio.Size = new System.Drawing.Size(127, 18);
            this.skeuoRadio.TabIndex = 6;
            this.skeuoRadio.TabStop = true;
            this.skeuoRadio.Text = "Skeuomorphic icons";
            this.skeuoRadio.UseVisualStyleBackColor = true;
            this.skeuoRadio.CheckedChanged += new System.EventHandler(this.skeuoRadio_CheckedChanged);
            // 
            // serversIcon
            // 
            this.serversIcon.Image = global::Naticord.Properties.Resources.servers_skeuo;
            this.serversIcon.Location = new System.Drawing.Point(89, 19);
            this.serversIcon.Name = "serversIcon";
            this.serversIcon.Size = new System.Drawing.Size(16, 16);
            this.serversIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.serversIcon.TabIndex = 5;
            this.serversIcon.TabStop = false;
            // 
            // dmIcon
            // 
            this.dmIcon.Image = global::Naticord.Properties.Resources.dm_skeuo;
            this.dmIcon.Location = new System.Drawing.Point(67, 19);
            this.dmIcon.Name = "dmIcon";
            this.dmIcon.Size = new System.Drawing.Size(16, 16);
            this.dmIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dmIcon.TabIndex = 4;
            this.dmIcon.TabStop = false;
            // 
            // githubIcon
            // 
            this.githubIcon.Image = global::Naticord.Properties.Resources.github_skeuo;
            this.githubIcon.Location = new System.Drawing.Point(45, 19);
            this.githubIcon.Name = "githubIcon";
            this.githubIcon.Size = new System.Drawing.Size(16, 16);
            this.githubIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.githubIcon.TabIndex = 3;
            this.githubIcon.TabStop = false;
            // 
            // settingsIcon
            // 
            this.settingsIcon.Image = global::Naticord.Properties.Resources.cog_skeuo;
            this.settingsIcon.Location = new System.Drawing.Point(23, 19);
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(16, 16);
            this.settingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.settingsIcon.TabIndex = 2;
            this.settingsIcon.TabStop = false;
            // 
            // accountIcon
            // 
            this.accountIcon.Image = global::Naticord.Properties.Resources.account_skeuo;
            this.accountIcon.Location = new System.Drawing.Point(1, 19);
            this.accountIcon.Name = "accountIcon";
            this.accountIcon.Size = new System.Drawing.Size(16, 16);
            this.accountIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.accountIcon.TabIndex = 1;
            this.accountIcon.TabStop = false;
            // 
            // icnPreviewLabel
            // 
            this.icnPreviewLabel.AutoSize = true;
            this.icnPreviewLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.icnPreviewLabel.Location = new System.Drawing.Point(-2, 1);
            this.icnPreviewLabel.Name = "icnPreviewLabel";
            this.icnPreviewLabel.Size = new System.Drawing.Size(73, 13);
            this.icnPreviewLabel.TabIndex = 0;
            this.icnPreviewLabel.Text = "Icon preview:";
            // 
            // AppearancePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cutOffPanel);
            this.Controls.Add(this.appearanceHeaderRenderer);
            this.Name = "AppearancePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppearancePage";
            this.cutOffPanel.ResumeLayout(false);
            this.cutOffPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serversIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dmIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DirectUI.Net.Hosting.DirectUIRenderer appearanceHeaderRenderer;
        private System.Windows.Forms.Panel cutOffPanel;
        private System.Windows.Forms.PictureBox accountIcon;
        private System.Windows.Forms.Label icnPreviewLabel;
        private System.Windows.Forms.PictureBox dmIcon;
        private System.Windows.Forms.PictureBox githubIcon;
        private System.Windows.Forms.PictureBox settingsIcon;
        private System.Windows.Forms.PictureBox serversIcon;
        private System.Windows.Forms.RadioButton modernRadio;
        private System.Windows.Forms.RadioButton skeuoRadio;
        private System.Windows.Forms.Label layoutNoticeLabel;
        private System.Windows.Forms.ComboBox layoutBox;
    }
}