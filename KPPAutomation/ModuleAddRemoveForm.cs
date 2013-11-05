using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KPPAutomation {
    public partial class ModuleAddRemoveForm : Form {
        public ModuleAddRemoveForm() {
            InitializeComponent();
        }

        public ApplicationSettings AppSettings = null;

        private void ModuleAddRemoveForm_Load(object sender, EventArgs e) {

            if (AppSettings!=null) {
                __ListModules.Objects = AppSettings.Visions;
            }
        }
    }
}
