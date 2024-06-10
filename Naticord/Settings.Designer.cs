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
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Check for Updates";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Plugins (barely working)";
            // 
            // pluginList
            // 
            this.pluginList.FormattingEnabled = true;
            this.pluginList.Location = new System.Drawing.Point(20, 85);
            this.pluginList.Name = "pluginList";
            this.pluginList.Size = new System.Drawing.Size(212, 147);
            this.pluginList.TabIndex = 4;
            // 
            // addPlugin
            // 
            this.addPlugin.Location = new System.Drawing.Point(192, 238);
            this.addPlugin.Name = "addPlugin";
            this.addPlugin.Size = new System.Drawing.Size(40, 23);
            this.addPlugin.TabIndex = 5;
            this.addPlugin.Text = "Add";
            this.addPlugin.UseVisualStyleBackColor = true;
            this.addPlugin.Click += new System.EventHandler(this.addPlugin_Click);
            // 
            // removePlugin
            // 
            this.removePlugin.Location = new System.Drawing.Point(128, 238);
            this.removePlugin.Name = "removePlugin";
            this.removePlugin.Size = new System.Drawing.Size(58, 23);
            this.removePlugin.TabIndex = 6;
            this.removePlugin.Text = "Remove";
            this.removePlugin.UseVisualStyleBackColor = true;
            // 
            // applyPluginsSettings
            // 
            this.applyPluginsSettings.Location = new System.Drawing.Point(156, 268);
            this.applyPluginsSettings.Name = "applyPluginsSettings";
            this.applyPluginsSettings.Size = new System.Drawing.Size(75, 23);
            this.applyPluginsSettings.TabIndex = 7;
            this.applyPluginsSettings.Text = "Apply";
            this.applyPluginsSettings.UseVisualStyleBackColor = true;
            this.applyPluginsSettings.Click += new System.EventHandler(this.applyPluginsSettings_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 307);
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
    }
}