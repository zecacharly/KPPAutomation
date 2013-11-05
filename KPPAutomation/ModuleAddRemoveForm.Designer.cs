namespace KPPAutomation {
    partial class ModuleAddRemoveForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleAddRemoveForm));
            this.@__ListModules = new BrightIdeasSoftware.ObjectListView();
            this.@__ModuleName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.@__ListModules)).BeginInit();
            this.SuspendLayout();
            // 
            // __ListModules
            // 
            resources.ApplyResources(this.@__ListModules, "__ListModules");
            this.@__ListModules.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.@__ListModules.AllColumns.Add(this.@__ModuleName);
            this.@__ListModules.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.@__ListModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__ModuleName});
            this.@__ListModules.FullRowSelect = true;
            this.@__ListModules.GridLines = true;
            this.@__ListModules.HideSelection = false;
            this.@__ListModules.IsSimpleDropSink = true;
            this.@__ListModules.LabelEdit = true;
            this.@__ListModules.MultiSelect = false;
            this.@__ListModules.Name = "__ListModules";
            this.@__ListModules.OverlayText.Text = resources.GetString("resource.Text");
            this.@__ListModules.ShowCommandMenuOnRightClick = true;
            this.@__ListModules.ShowGroups = false;
            this.@__ListModules.ShowItemToolTips = true;
            this.@__ListModules.ShowSortIndicators = false;
            this.@__ListModules.SortGroupItemsByPrimaryColumn = false;
            this.@__ListModules.UseCompatibleStateImageBehavior = false;
            this.@__ListModules.UseTranslucentSelection = true;
            this.@__ListModules.View = System.Windows.Forms.View.Details;
            // 
            // __ModuleName
            // 
            this.@__ModuleName.AspectName = "ModuleName";
            resources.ApplyResources(this.@__ModuleName, "__ModuleName");
            // 
            // ModuleAddRemoveForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__ListModules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ModuleAddRemoveForm";
            this.Load += new System.EventHandler(this.ModuleAddRemoveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.@__ListModules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView __ListModules;
        public BrightIdeasSoftware.OLVColumn __ModuleName;
    }
}