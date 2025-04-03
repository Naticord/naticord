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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.appearanceTab = new System.Windows.Forms.TabPage();
            this.bdStyleBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bdrStyleBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.icnStyleBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.osVerBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.appearanceTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.appearanceTab);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 402);
            this.tabControl1.TabIndex = 0;
            // 
            // appearanceTab
            // 
            this.appearanceTab.Controls.Add(this.bdStyleBox);
            this.appearanceTab.Controls.Add(this.label4);
            this.appearanceTab.Controls.Add(this.bdrStyleBox);
            this.appearanceTab.Controls.Add(this.label3);
            this.appearanceTab.Controls.Add(this.icnStyleBox);
            this.appearanceTab.Controls.Add(this.label2);
            this.appearanceTab.Controls.Add(this.osVerBox);
            this.appearanceTab.Controls.Add(this.label1);
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
            this.bdStyleBox.Location = new System.Drawing.Point(91, 115);
            this.bdStyleBox.Name = "bdStyleBox";
            this.bdStyleBox.Size = new System.Drawing.Size(248, 23);
            this.bdStyleBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Backdrop";
            // 
            // bdrStyleBox
            // 
            this.bdrStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bdrStyleBox.FormattingEnabled = true;
            this.bdrStyleBox.Items.AddRange(new object[] {
            "Slim",
            "Thick"});
            this.bdrStyleBox.Location = new System.Drawing.Point(91, 81);
            this.bdrStyleBox.Name = "bdrStyleBox";
            this.bdrStyleBox.Size = new System.Drawing.Size(248, 23);
            this.bdrStyleBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Border Style";
            // 
            // icnStyleBox
            // 
            this.icnStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.icnStyleBox.FormattingEnabled = true;
            this.icnStyleBox.Items.AddRange(new object[] {
            "Modern",
            "Legacy"});
            this.icnStyleBox.Location = new System.Drawing.Point(91, 47);
            this.icnStyleBox.Name = "icnStyleBox";
            this.icnStyleBox.Size = new System.Drawing.Size(248, 23);
            this.icnStyleBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Icon Style";
            // 
            // osVerBox
            // 
            this.osVerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.osVerBox.FormattingEnabled = true;
            this.osVerBox.Items.AddRange(new object[] {
            "Windows 11",
            "Windows 10",
            "Windows 7 - 8.1"});
            this.osVerBox.Location = new System.Drawing.Point(91, 13);
            this.osVerBox.Name = "osVerBox";
            this.osVerBox.Size = new System.Drawing.Size(248, 23);
            this.osVerBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "OS Version";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 426);
            this.Controls.Add(this.tabControl1);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.appearanceTab.ResumeLayout(false);
            this.appearanceTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage appearanceTab;
        private System.Windows.Forms.ComboBox osVerBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox bdStyleBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox bdrStyleBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox icnStyleBox;
        private System.Windows.Forms.Label label2;
    }
}