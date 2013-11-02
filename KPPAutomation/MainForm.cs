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
using KPPAutomationCore;

namespace KPPAutomation {
    public partial class MainForm : Form {

        public Boolean SetAdmin = false;
        private static KPPLogger log = new KPPLogger(typeof(MainForm));
        private ConfigForm _ConfigForm = new ConfigForm();

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


            MainDockFile= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config\\MainDockPanel.config");


            if (!Directory.Exists(MainDockFile)) {
                Directory.CreateDirectory(Path.GetDirectoryName(MainDockFile));
            }

            AppFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config\\KPPAutomationSettings.config");

            if (!File.Exists(AppFile)) {
                ApplicationConfig = new ApplicationSettings();
                ApplicationConfig.WriteConfigurationFile(AppFile);
            }
            ApplicationConfig = ApplicationSettings.ReadConfigurationFile(AppFile);
            ApplicationConfig.BackupExtention = ".bkp";
            ApplicationConfig.BackupFilesToKeep = 5;
            ApplicationConfig.BackupFolderName = "Backup";
            if (ApplicationConfig.Users.Count == 0) {
                ApplicationConfig.Users.Add(new UserDef("auto123", Acesslevel.Admin));
                ApplicationConfig.Users.Add(new UserDef("man", Acesslevel.Man));
            }



            _ConfigForm.__btsaveConf.Click += new EventHandler(__btsaveConf_Click);
            _ConfigForm.__PropertySettings.SelectedObject = ApplicationConfig;



            foreach (KPPVision item in ApplicationConfig.Visions) {

                if (item.Enabled) {
                    item.StartModule();
                }
            }




            if (File.Exists(MainDockFile))
                try {
                    __MainDock.LoadFromXml(MainDockFile, m_deserializeDockContent);
                }
                catch (Exception exp) {

                    __MainDock.SaveAsXml(MainDockFile);

                }
            else {
               

            }
            foreach (KPPVision item in ApplicationConfig.Visions) {

                if (!item.ModuleForm.Visible) {
                    item.ModuleForm.Show(__MainDock);
                }

            }

            

            AcessManagement.OnAcesslevelChanged += new AcessManagement.AcesslevelChanged(AcessManagement_OnAcesslevelChanged);


            if (SetAdmin) {
                AcessManagement.AcessLevel = Acesslevel.Admin;
            }
            else {
                AcessManagement.AcessLevel = Acesslevel.User;
            }
        }

        void AcessManagement_OnAcesslevelChanged(Acesslevel NewLevel) {
            Boolean state = (NewLevel == Acesslevel.Admin || NewLevel == Acesslevel.Man);
            
            __btConfig.Visible = state;

            switch (NewLevel) {
                case Acesslevel.Admin:
                    __dropLogin.Text = this.GetResourceText("Admin_Mode");
                    __dropLogin.Image = new Bitmap(Properties.Resources.AcessUnlock);
                    __dropLogin.BackColor = Color.LightGreen;
                    __btlogin.Text = this.GetResourceText("Logout");
                    break;
                case Acesslevel.Man:
                    __dropLogin.Text = this.GetResourceText("Man_mode");
                    __dropLogin.Image = new Bitmap(Properties.Resources.AcessUnlock);
                    __dropLogin.BackColor = Color.LightGreen;
                    break;
                case Acesslevel.User:
                    __dropLogin.BackColor = SystemColors.Control;
                    __dropLogin.Image = new Bitmap(Properties.Resources.Acesslock);
                    __dropLogin.Text = this.GetResourceText("User_Mode");
                    __btlogin.Text = this.GetResourceText("Login");
                    break;
                default:
                    break;
            }
        }
        void __btsaveConf_Click(object sender, EventArgs e) {
            ApplicationConfig.WriteConfigurationFile();
        }

        private IDockContent GetContentFromPersistString(string persistString) {


             
            //TODO Check Modules

            //if (ApplicationConfig.Vision.Enabled) {

            //    if (persistString == ApplicationConfig.Vision.ModuleForm.GetType().ToString()) {
            //        return ApplicationConfig.Vision.ModuleForm;
            //    }
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

        //private void LoadVisionModule() {
        //    try {

        //        ApplicationConfig.Vision.StartModule(__MainDock);
        //        __MainDock.Refresh();
        //    }
        //    catch (Exception exp) {

        //        log.Error(exp);
        //    }
        //}

        private void UnLoadVisionModules() {
            foreach (KPPVision item in ApplicationConfig.Visions) {
                if (item.ModuleStarted) {
                    item.StopModule();
                }
            }

        }


        private void __btConfig_Click(object sender, EventArgs e) {
            _ConfigForm.ShowDialog();
            //if (ApplicationConfig.Vision.Enabled) {
            //    LoadVisionModule();
            //}
            //else {
            //    UnLoadVisionModule();
            //}
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                __MainDock.SaveAsXml(MainDockFile);
                UnLoadVisionModules();
            }
            catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btlogin_Click(object sender, EventArgs e) {
            if (__btlogin.Text == this.GetResourceText("Login")) {
                ApplicationSettings.SelectedUser = ApplicationConfig.Users.Find(bypass => bypass.Pass == __logpass.Text);
                if (ApplicationSettings.SelectedUser != null) {
                    AcessManagement.AcessLevel = ApplicationSettings.SelectedUser.Level;
                    __dropLogin.HideDropDown();


                }
            }
            else {

                __logpass.Text = "";

                AcessManagement.AcessLevel= Acesslevel.User;
                __dropLogin.HideDropDown();


            }
        }

    }
}
