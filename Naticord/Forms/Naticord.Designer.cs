namespace Naticord
{
    partial class Naticord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Naticord));
            this.usernameLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.friendsList = new System.Windows.Forms.ListView();
            this.fsTabs = new System.Windows.Forms.TabControl();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.friendSearchBar = new System.Windows.Forms.TextBox();
            this.serversTab = new System.Windows.Forms.TabPage();
            this.serverSearchBar = new System.Windows.Forms.TextBox();
            this.serversList = new System.Windows.Forms.ListView();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fsTabs.SuspendLayout();
            this.friendsTab.SuspendLayout();
            this.serversTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(64, 12);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(183, 38);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionLabel.Location = new System.Drawing.Point(66, 38);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(183, 18);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "No status...";
            // 
            // friendsList
            // 
            this.friendsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.friendsList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.friendsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.friendsList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.friendsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.friendsList.HideSelection = false;
            this.friendsList.LabelWrap = false;
            this.friendsList.Location = new System.Drawing.Point(5, 32);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(257, 332);
            this.friendsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.friendsList.TabIndex = 0;
            this.friendsList.UseCompatibleStateImageBehavior = false;
            this.friendsList.View = System.Windows.Forms.View.Tile;
            this.friendsList.DoubleClick += new System.EventHandler(this.friendsList_DoubleClick);
            // 
            // fsTabs
            // 
            this.fsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fsTabs.Controls.Add(this.friendsTab);
            this.fsTabs.Controls.Add(this.serversTab);
            this.fsTabs.Location = new System.Drawing.Point(11, 66);
            this.fsTabs.Name = "fsTabs";
            this.fsTabs.SelectedIndex = 0;
            this.fsTabs.Size = new System.Drawing.Size(274, 397);
            this.fsTabs.TabIndex = 4;
            // 
            // friendsTab
            // 
            this.friendsTab.Controls.Add(this.friendSearchBar);
            this.friendsTab.Controls.Add(this.friendsList);
            this.friendsTab.Location = new System.Drawing.Point(4, 22);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Padding = new System.Windows.Forms.Padding(3);
            this.friendsTab.Size = new System.Drawing.Size(266, 371);
            this.friendsTab.TabIndex = 0;
            this.friendsTab.Text = "Direct Messages";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // friendSearchBar
            // 
            this.friendSearchBar.Location = new System.Drawing.Point(7, 6);
            this.friendSearchBar.Name = "friendSearchBar";
            this.friendSearchBar.Size = new System.Drawing.Size(253, 20);
            this.friendSearchBar.TabIndex = 1;
            // 
            // serversTab
            // 
            this.serversTab.Controls.Add(this.serverSearchBar);
            this.serversTab.Controls.Add(this.serversList);
            this.serversTab.Location = new System.Drawing.Point(4, 22);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(266, 371);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // serverSearchBar
            // 
            this.serverSearchBar.Location = new System.Drawing.Point(7, 6);
            this.serverSearchBar.Name = "serverSearchBar";
            this.serverSearchBar.Size = new System.Drawing.Size(253, 20);
            this.serverSearchBar.TabIndex = 2;
            // 
            // serversList
            // 
            this.serversList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serversList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.serversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serversList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.serversList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.serversList.HideSelection = false;
            this.serversList.LabelWrap = false;
            this.serversList.Location = new System.Drawing.Point(4, 32);
            this.serversList.Name = "serversList";
            this.serversList.Size = new System.Drawing.Size(257, 333);
            this.serversList.TabIndex = 1;
            this.serversList.UseCompatibleStateImageBehavior = false;
            this.serversList.View = System.Windows.Forms.View.Tile;
            this.serversList.DoubleClick += new System.EventHandler(this.serversList_DoubleClick);
            // 
            // profilepicture
            // 
            this.profilepicture.BackColor = System.Drawing.Color.Transparent;
            this.profilepicture.ErrorImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.Image = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.ImageLocation = "";
            this.profilepicture.InitialImage = global::Naticord.Properties.Resources.defaultpfp;
            this.profilepicture.Location = new System.Drawing.Point(10, 10);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(50, 50);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilepicture.TabIndex = 0;
            this.profilepicture.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(261, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Naticord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(296, 468);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fsTabs);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicture);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Naticord";
            this.Text = "Naticord";
            this.fsTabs.ResumeLayout(false);
            this.friendsTab.ResumeLayout(false);
            this.friendsTab.PerformLayout();
            this.serversTab.ResumeLayout(false);
            this.serversTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.Label usernameLabel;
        public System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TabControl fsTabs;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        private System.Windows.Forms.ListView friendsList;
        private System.Windows.Forms.ListView serversList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox friendSearchBar;
        private System.Windows.Forms.TextBox serverSearchBar;
    }
}
