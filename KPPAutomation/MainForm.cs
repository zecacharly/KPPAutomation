using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using KPP.Core.Debug;

namespace KPPAutomation {
    public partial class MainForm : Form {

        public Boolean SetAdmin = false;
        private static KPPLogger log = new KPPLogger(typeof(MainForm));




        public MainForm() {

            switch (Program.Language) {
                case Program.LanguageName.Unk:
                    break;
                case Program.LanguageName.PT:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-PT");

                    break;
                case Program.LanguageName.EN:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

                    break;
                default:
                    break;
            }
          
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            switch (Program.Language) {
                case Program.LanguageName.Unk:
                    break;
                case Program.LanguageName.PT:
                    portugueseToolStripMenuItem.Checked = true;
                    englishToolStripMenuItem.Checked = false;
                    break;
                case Program.LanguageName.EN:
                    portugueseToolStripMenuItem.Checked = false;
                    englishToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private Boolean restartapp(Program.LanguageName lang) {
            try {

                String cap = this.GetResourceText("MessageBox_Language_caption");
                String text = this.GetResourceText("MessageBox_Language_text");
                //String text = res_man.GetString("", Thread.CurrentThread.CurrentUICulture);
                if (MessageBox.Show(text, cap, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK) {
                    Program.Language = lang;
                    Program.Restart = true;
                    this.Close();
                }
            }
            catch (Exception exp) {

                log.Error(exp);
            }
            return false;
        }

        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Program.Language != Program.LanguageName.PT) {
                if (!restartapp(Program.LanguageName.PT)) {
                    portugueseToolStripMenuItem.Checked = false;
                    englishToolStripMenuItem.Checked = true;
                }
            }
            
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Program.Language != Program.LanguageName.EN) {
                if (!restartapp(Program.LanguageName.EN)) {
                    portugueseToolStripMenuItem.Checked = true;
                    englishToolStripMenuItem.Checked = false;
                }
            }
        }

    }
}
