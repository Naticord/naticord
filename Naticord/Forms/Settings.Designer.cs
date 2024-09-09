namespace Naticord
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkUpdates = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pluginList = new System.Windows.Forms.ListBox();
            this.addPlugin = new System.Windows.Forms.Button();
            this.removePlugin = new System.Windows.Forms.Button();
            this.applyPluginsSettings = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cliColorTxt = new System.Windows.Forms.TextBox();
            this.textColorTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.shikanokobutton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // checkUpdates
            // 
            this.checkUpdates.Location = new System.Drawing.Point(160, 40);
            this.checkUpdates.Name = "checkUpdates";
            this.checkUpdates.Size = new System.Drawing.Size(75, 22);
            this.checkUpdates.TabIndex = 1;
            this.checkUpdates.Text = "Check";
            this.checkUpdates.UseVisualStyleBackColor = true;
            this.checkUpdates.Click += new System.EventHandler(this.checkUpdates_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Check for Updates";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label3.Location = new System.Drawing.Point(12, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Plugins";
            // 
            // pluginList
            // 
            this.pluginList.FormattingEnabled = true;
            this.pluginList.Location = new System.Drawing.Point(16, 186);
            this.pluginList.Name = "pluginList";
            this.pluginList.Size = new System.Drawing.Size(212, 147);
            this.pluginList.TabIndex = 4;
            // 
            // addPlugin
            // 
            this.addPlugin.Location = new System.Drawing.Point(188, 339);
            this.addPlugin.Name = "addPlugin";
            this.addPlugin.Size = new System.Drawing.Size(40, 23);
            this.addPlugin.TabIndex = 5;
            this.addPlugin.Text = "Add";
            this.addPlugin.UseVisualStyleBackColor = true;
            this.addPlugin.Click += new System.EventHandler(this.addPlugin_Click);
            // 
            // removePlugin
            // 
            this.removePlugin.Location = new System.Drawing.Point(124, 339);
            this.removePlugin.Name = "removePlugin";
            this.removePlugin.Size = new System.Drawing.Size(58, 23);
            this.removePlugin.TabIndex = 6;
            this.removePlugin.Text = "Remove";
            this.removePlugin.UseVisualStyleBackColor = true;
            // 
            // applyPluginsSettings
            // 
            this.applyPluginsSettings.Location = new System.Drawing.Point(152, 369);
            this.applyPluginsSettings.Name = "applyPluginsSettings";
            this.applyPluginsSettings.Size = new System.Drawing.Size(75, 23);
            this.applyPluginsSettings.TabIndex = 7;
            this.applyPluginsSettings.Text = "Apply";
            this.applyPluginsSettings.UseVisualStyleBackColor = true;
            this.applyPluginsSettings.Click += new System.EventHandler(this.applyPluginsSettings_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Client Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Text Color";
            // 
            // cliColorTxt
            // 
            this.cliColorTxt.Location = new System.Drawing.Point(88, 98);
            this.cliColorTxt.Name = "cliColorTxt";
            this.cliColorTxt.Size = new System.Drawing.Size(148, 22);
            this.cliColorTxt.TabIndex = 10;
            // 
            // textColorTxt
            // 
            this.textColorTxt.Location = new System.Drawing.Point(88, 129);
            this.textColorTxt.Name = "textColorTxt";
            this.textColorTxt.Size = new System.Drawing.Size(148, 22);
            this.textColorTxt.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Theming";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 450);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "surprise me";
            // 
            // shikanokobutton
            // 
            this.shikanokobutton.Location = new System.Drawing.Point(160, 446);
            this.shikanokobutton.Name = "shikanokobutton";
            this.shikanokobutton.Size = new System.Drawing.Size(75, 22);
            this.shikanokobutton.TabIndex = 13;
            this.shikanokobutton.Text = "pluh";
            this.shikanokobutton.UseVisualStyleBackColor = true;
            this.shikanokobutton.Click += new System.EventHandler(this.shikanokobutton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(160, 405);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 22);
            this.logoutButton.TabIndex = 15;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 410);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Log out of Discord";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 438);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.shikanokobutton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textColorTxt);
            this.Controls.Add(this.cliColorTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.applyPluginsSettings);
            this.Controls.Add(this.removePlugin);
            this.Controls.Add(this.addPlugin);
            this.Controls.Add(this.pluginList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkUpdates);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button checkUpdates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox pluginList;
        private System.Windows.Forms.Button addPlugin;
        private System.Windows.Forms.Button removePlugin;
        private System.Windows.Forms.Button applyPluginsSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox cliColorTxt;
        private System.Windows.Forms.TextBox textColorTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button shikanokobutton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label label8;
    }
}