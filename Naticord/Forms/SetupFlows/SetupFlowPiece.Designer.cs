namespace Naticord.Forms.SetupFlows
{
    partial class SetupFlowPiece
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupFlowPiece));
            this.pageCountRenderer = new DirectUI.Net.Hosting.DirectUIRenderer();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.nextButton = new Naticord.UserControls.GlassExtendedButton();
            this.backButton = new Naticord.UserControls.GlassExtendedButton();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageCountRenderer
            // 
            this.pageCountRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageCountRenderer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pageCountRenderer.Location = new System.Drawing.Point(0, 35);
            this.pageCountRenderer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageCountRenderer.Name = "pageCountRenderer";
            this.pageCountRenderer.ResId = "pageCountPieceMain";
            this.pageCountRenderer.Size = new System.Drawing.Size(656, 40);
            this.pageCountRenderer.TabIndex = 0;
            this.pageCountRenderer.XmlFile = "User Interface\\SetupFlow\\PageCountPiece.xml";
            // 
            // pagePanel
            // 
            this.pagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagePanel.BackColor = System.Drawing.Color.White;
            this.pagePanel.Location = new System.Drawing.Point(0, 75);
            this.pagePanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(656, 497);
            this.pagePanel.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Controls.Add(this.nextButton);
            this.buttonPanel.Controls.Add(this.backButton);
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(656, 35);
            this.buttonPanel.TabIndex = 2;
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.BackColor = System.Drawing.Color.Transparent;
            this.nextButton.ButtonIcon = global::Naticord.Properties.Resources.arrow_forward_skeuo;
            this.nextButton.ButtonLabel = "Next page";
            this.nextButton.CompositionDisabled = false;
            this.nextButton.Location = new System.Drawing.Point(482, 2);
            this.nextButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.ReverseOrder = true;
            this.nextButton.Size = new System.Drawing.Size(175, 23);
            this.nextButton.TabIndex = 1;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.ButtonIcon = global::Naticord.Properties.Resources.arrow_back_skeuo;
            this.backButton.ButtonLabel = "Go back";
            this.backButton.CompositionDisabled = false;
            this.backButton.Location = new System.Drawing.Point(2, 2);
            this.backButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.backButton.Name = "backButton";
            this.backButton.ReverseOrder = false;
            this.backButton.Size = new System.Drawing.Size(175, 23);
            this.backButton.TabIndex = 0;
            // 
            // SetupFlowPiece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(656, 572);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.pageCountRenderer);
            this.Controls.Add(this.pagePanel);
            this.GlassMargins = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SetupFlowPiece";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Naticord";
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DirectUI.Net.Hosting.DirectUIRenderer pageCountRenderer;
        private System.Windows.Forms.Panel pagePanel;
        public System.Windows.Forms.Panel buttonPanel;
        public UserControls.GlassExtendedButton nextButton;
        public UserControls.GlassExtendedButton backButton;
    }
}