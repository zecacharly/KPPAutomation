namespace KPPAutomation {
    partial class ConfigForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.@__btexitconf = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__visionenabled = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.@__epsonEnabled = new System.Windows.Forms.CheckBox();
            this.@__btsaveConf = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.@__btexitconf, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.@__btsaveConf, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.22727F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.77273F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 264);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // __btexitconf
            // 
            this.@__btexitconf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__btexitconf.Location = new System.Drawing.Point(205, 227);
            this.@__btexitconf.Name = "__btexitconf";
            this.@__btexitconf.Size = new System.Drawing.Size(197, 34);
            this.@__btexitconf.TabIndex = 3;
            this.@__btexitconf.Text = "Exit";
            this.@__btexitconf.UseVisualStyleBackColor = true;
            this.@__btexitconf.Click += new System.EventHandler(this.@__btexitconf_Click);
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(399, 218);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.@__visionenabled);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(391, 192);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Vision Module";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(8, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 154);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // __visionenabled
            // 
            this.@__visionenabled.AutoSize = true;
            this.@__visionenabled.Location = new System.Drawing.Point(8, 6);
            this.@__visionenabled.Name = "__visionenabled";
            this.@__visionenabled.Size = new System.Drawing.Size(65, 17);
            this.@__visionenabled.TabIndex = 0;
            this.@__visionenabled.Text = "Enabled";
            this.@__visionenabled.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.@__epsonEnabled);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(391, 192);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Epson Module";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(7, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 154);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // __epsonEnabled
            // 
            this.@__epsonEnabled.AutoSize = true;
            this.@__epsonEnabled.Location = new System.Drawing.Point(7, 8);
            this.@__epsonEnabled.Name = "__epsonEnabled";
            this.@__epsonEnabled.Size = new System.Drawing.Size(65, 17);
            this.@__epsonEnabled.TabIndex = 2;
            this.@__epsonEnabled.Text = "Enabled";
            this.@__epsonEnabled.UseVisualStyleBackColor = true;
            // 
            // __btsaveConf
            // 
            this.@__btsaveConf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__btsaveConf.Location = new System.Drawing.Point(3, 227);
            this.@__btsaveConf.Name = "__btsaveConf";
            this.@__btsaveConf.Size = new System.Drawing.Size(196, 34);
            this.@__btsaveConf.TabIndex = 2;
            this.@__btsaveConf.Text = "Save";
            this.@__btsaveConf.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 264);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConfigForm";
            this.Text = "Configurations";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button __btexitconf;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button __btsaveConf;
        public System.Windows.Forms.CheckBox __epsonEnabled;
        public System.Windows.Forms.CheckBox __visionenabled;

    }
}