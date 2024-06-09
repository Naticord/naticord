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
            this.serversTab = new System.Windows.Forms.TabPage();
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
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.Location = new System.Drawing.Point(67, 10);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(183, 20);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username";
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionLabel.Location = new System.Drawing.Point(67, 36);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(183, 24);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "description";
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // friendsList
            // 
            this.friendsList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.friendsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.friendsList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.friendsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.friendsList.HideSelection = false;
            this.friendsList.LabelWrap = false;
            this.friendsList.Location = new System.Drawing.Point(5, 4);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(257, 335);
            this.friendsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.friendsList.TabIndex = 0;
            this.friendsList.UseCompatibleStateImageBehavior = false;
            this.friendsList.View = System.Windows.Forms.View.Tile;
            this.friendsList.DoubleClick += new System.EventHandler(this.friendsList_DoubleClick);
            this.friendsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // fsTabs
            // 
            this.fsTabs.Controls.Add(this.friendsTab);
            this.fsTabs.Controls.Add(this.serversTab);
            this.fsTabs.Location = new System.Drawing.Point(11, 66);
            this.fsTabs.Name = "fsTabs";
            this.fsTabs.SelectedIndex = 0;
            this.fsTabs.Size = new System.Drawing.Size(274, 372);
            this.fsTabs.TabIndex = 4;
            this.fsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // friendsTab
            // 
            this.friendsTab.Controls.Add(this.friendsList);
            this.friendsTab.Location = new System.Drawing.Point(4, 22);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Padding = new System.Windows.Forms.Padding(3);
            this.friendsTab.Size = new System.Drawing.Size(266, 346);
            this.friendsTab.TabIndex = 0;
            this.friendsTab.Text = "Friends";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // serversTab
            // 
            this.serversTab.Controls.Add(this.serversList);
            this.serversTab.Location = new System.Drawing.Point(4, 22);
            this.serversTab.Name = "serversTab";
            this.serversTab.Padding = new System.Windows.Forms.Padding(3);
            this.serversTab.Size = new System.Drawing.Size(266, 346);
            this.serversTab.TabIndex = 1;
            this.serversTab.Text = "Servers";
            this.serversTab.UseVisualStyleBackColor = true;
            // 
            // serversList
            // 
            this.serversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serversList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.serversList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.serversList.HideSelection = false;
            this.serversList.LabelWrap = false;
            this.serversList.Location = new System.Drawing.Point(4, 5);
            this.serversList.Name = "serversList";
            this.serversList.Size = new System.Drawing.Size(257, 335);
            this.serversList.TabIndex = 1;
            this.serversList.UseCompatibleStateImageBehavior = false;
            this.serversList.View = System.Windows.Forms.View.Tile;
            this.serversList.DoubleClick += new System.EventHandler(this.serversList_DoubleClick);
            this.serversList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(296, 449);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fsTabs);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.profilepicture);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = true;
            this.Name = "Naticord";
            this.Text = "Naticord";
            this.fsTabs.ResumeLayout(false);
            this.friendsTab.ResumeLayout(false);
            this.serversTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TabControl fsTabs;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.TabPage serversTab;
        private System.Windows.Forms.ListView friendsList;
        private System.Windows.Forms.ListView serversList;
        private System.Windows.Forms.Button button1;
    }
}
