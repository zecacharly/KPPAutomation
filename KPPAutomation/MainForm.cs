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
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using VisionModule;

namespace KPPAutomation {
    public partial class MainForm : Form {

        public Boolean SetAdmin = false;
        private static KPPLogger log = new KPPLogger(typeof(MainForm));

        private String m_AppFile = "";
        public String AppFile {
            get { return m_AppFile; }
            set { m_AppFile = value; }
        }

        private ApplicationSettings m_ApplicationConfig = null;
        public ApplicationSettings ApplicationConfig {
            get { return m_ApplicationConfig; }
            set { m_ApplicationConfig = value; }
        }

        private string MainDockFile = "";
        DeserializeDockContent m_deserializeDockContent;

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

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

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


            MainDockFile= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config\\DockPanel.config");


            if (!Directory.Exists(MainDockFile)) {
                Directory.CreateDirectory(Path.GetDirectoryName(MainDockFile));
            }


            if (!File.Exists(AppFile)) {
                ApplicationConfig = new ApplicationSettings();
                ApplicationConfig.WriteConfigurationFile(AppFile);
            }
            ApplicationConfig = ApplicationSettings.ReadConfigurationFile(AppFile);
            ApplicationConfig.BackupExtention = ".bkp";
            ApplicationConfig.BackupFilesToKeep = 5;
            ApplicationConfig.BackupFolderName = "Backup";


            //if (ApplicationConfig.KPPModules.Count==0) {
            //    ApplicationConfig.KPPModules.Add(KPPAvaibleModules.Vision);

            //}



            // TOODO check modules

            if (File.Exists(MainDockFile))
                try {
                    __MainDock.LoadFromXml(MainDockFile, m_deserializeDockContent);
                }
                catch (Exception exp) {

                    __MainDock.SaveAsXml(MainDockFile);

                }
            else {
               

            }



        }

        private IDockContent GetContentFromPersistString(string persistString) {

            //TODO Check Modules

            //if (ApplicationConfig.KPPModules.Contains(KPPAvaibleModules.Vision)) {

            //}

            //if (persistString == typeof(VisionForm).ToString())
            //    return _ListInspForm;
            ////else if (persistString == typeof(InspectionOptionsForm).ToString())
            ////  return _InspectionOptions;
            //else if (persistString == typeof(ImageContainerForm).ToString())
            //    return _ImageContainer;
            //else if (persistString == typeof(ListROIForm).ToString())
            //    return _ListROIForm;
            //else if (persistString == typeof(LogForm).ToString())
            //    return _LogForm;
            //else if (persistString == typeof(ViewInspections).ToString())
            //    return _viewinspections;
            //else if (persistString == typeof(ResultsConfiguration).ToString())
            //    return _ResultsConfiguration;

            //else {

                return null;
           // }
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
