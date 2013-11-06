using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using KPP.Core.Debug;
using System.IO;
using System.Windows.Forms;
using VisionModule;
using System.ComponentModel;
using KPPAutomationCore;
using System.Drawing.Design;
using VisionModule.Forms;
using System.Windows.Forms.Design;
using WeifenLuo.WinFormsUI.Docking;

namespace KPPAutomation {


    public class ModuleAddRemoveSelector : UITypeEditor {
        // this is a container for strings, which can be 
        // picked-out
        //UserControl contextcontrol = StaticObjects.InputItemSelectorControl;
        private static KPPLogger log = new KPPLogger(typeof(ModuleAddRemoveSelector));
        public ModuleAddRemoveForm form = new ModuleAddRemoveForm();

        IWindowsFormsEditorService edSvc;
        // this is a string array for drop-down list
        //internal static List<CameraInfo> RemoteCameras = new List<CameraInfo>();





        public ModuleAddRemoveSelector() {
            //menu.BorderStyle = BorderStyle.None;
            // add event handler for drop-down box when item 
            // will be selected
            //  textbox1.KeyDown += new KeyEventHandler(textebox1_KeyDown);

            //contextcontrol.VisibleChanged += new EventHandler(contextcontrol_VisibleChanged);
        }




        void textebox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                edSvc.CloseDropDown();
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value) {




            ApplicationSettings settings = context.Instance as ApplicationSettings;
            

            edSvc =
               (IWindowsFormsEditorService)provider.
               GetService(typeof
               (IWindowsFormsEditorService));

            if (edSvc != null) {

                // form.SelectedProject = StaticObjects.SelectedProject;

                form.AppSettings = settings;
                form.__comboModuleTypes.Items.Clear();
                var lListOfBs = (from lAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from lType in lAssembly.GetTypes()
                                 where lType.IsSubclassOf(typeof(KPPModule))
                                 select lType).ToArray();
                form.__comboModuleTypes.Items.AddRange(lListOfBs);
                if (edSvc.ShowDialog(form) == DialogResult.OK) {

                }


                //if (textbox1.Text != "") {
                //    newresref.ResultReferenceID = textbox1.Text;
                //    newresref.ResultOutput = double.Parse(textbox1.Text);
                //    return newresref;
                //}

            }
            return value;
        }



    }

    #region Custom editor
    public class AppFileFolderSelector : UITypeEditor {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {

            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Application Settings File Location";
            //openFolderDialog.Title = "Select application configuration file";


            if (openFolderDialog.ShowDialog() == DialogResult.OK) {

                openFolderDialog.Dispose();
                //base.InitializeDialog(openFolderDialog);
                Uri fullPath = new Uri(openFolderDialog.SelectedPath, UriKind.Absolute);
                Uri relRoot = new Uri(AppDomain.CurrentDomain.BaseDirectory, UriKind.Absolute);

                String relative = relRoot.MakeRelativeUri(fullPath).ToString();
                relative = relative.Replace("%20", " ");

                //openFolderDialog.DirectoryPath = relative;
                return base.EditValue(context, provider, relative);
            }
            return base.EditValue(context, provider, value);
        }
    }

    #endregion

    public class KPPVisionModule : KPPModule {

        private static KPPLogger log = new KPPLogger(typeof(KPPVisionModule));

        [Browsable(false)]
        public override String DockFile {
            get;
            set;
        }

        private String m_FilesLocation = "";
        [EditorAttribute(typeof(AppFileFolderSelector), typeof(UITypeEditor))]        
        public String FilesLocation {
            get { return m_FilesLocation; }
            set {
                if (m_FilesLocation!=value) {
                   
                    m_FilesLocation = value;  
                }
            }
        }

        private String _AppFile = "";
        [Browsable(false)]
        public override String AppFile {
            get {
                return _AppFile;
            }
            set {

                _AppFile = value;

            }


        }

        private String m_ModuleName = "New vision module";
        [XmlAttribute, DisplayName("Module Name")]
        public override String ModuleName {
            get { return m_ModuleName; }
            set { m_ModuleName = value; }
        }

       
        [XmlIgnore]
        public override String ModuleType {
            get {
                return this.GetType().ToString();
            }
           
        }

        public override Object GetModelForm() {
            return ModuleForm;
        }

        private VisionForm m_ModuleForm = null;
        [XmlIgnore]
        [Browsable(false)]
        public VisionForm ModuleForm {
            get { return m_ModuleForm; }
            internal set { m_ModuleForm = value; }
        }


        public override void StartModule(DockPanel dockingpanel) {
            if (StartModule()) {
                ModuleForm.Show(dockingpanel);
            }

        }

        public override Boolean StartModule() {
            if (!ModuleStarted) {


                if (!Directory.Exists(FilesLocation)) {
                    Directory.CreateDirectory(FilesLocation);
                }
                String appath = AppDomain.CurrentDomain.BaseDirectory;


                

                //DockFile = Path.Combine(FilesLocation, "VisionModule" + ModelID + "DockPanel.dock");
                //AppFile = Path.Combine(FilesLocation, "VisionModule" + ModelID + ".module");

                DockFile = Path.Combine(FilesLocation, "VisionModule" + ModelID + "DockPanel.dock");
                AppFile = Path.Combine(FilesLocation, "VisionModule" + ModelID + ".module");

                Uri fullPath = new Uri(new Uri(appath), DockFile);
                DockFile = fullPath.LocalPath;// +Path.GetFileName(newpath);

                fullPath = new Uri(new Uri(appath), AppFile);
                AppFile = fullPath.LocalPath;// +Path.GetFileName(newpath);

                if (!File.Exists(AppFile)) {

                    VisionSettings.WriteConfiguration(new VisionSettings(), AppFile);
                }

                ModuleForm = new VisionForm();
                ModuleForm.ModuleName = ModuleName;
                ModuleForm.DockFile = DockFile;
                ModuleForm.Appfile = AppFile;

                ModuleStarted = true;
            }

            return ModuleStarted;
        }

        public override void ShowModule(DockPanel dockingpanel) {
            if (ModuleForm!=null) {
                if (!ModuleForm.Visible) {
                    ModuleForm.Show(dockingpanel);
                } 
            }
            
        }


        public override void StopModule() {
            if (ModuleStarted) {
                ModuleForm.Form1_FormClosing(this, new FormClosingEventArgs(CloseReason.UserClosing, false));
                ModuleStarted = false;
            }

        }


        public override string ToString() {
            return ModuleName;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlInclude(typeof(KPPVisionModule))]
    public class KPPModule {



        private int m_modelID = -1;
        [XmlAttribute, DisplayName("Module ID"), ReadOnly(false)]
        public virtual int ModelID {
            get { return m_modelID; }
            set { m_modelID = value; }
        }

        [XmlAttribute, DisplayName("Module Name")]
        public virtual String ModuleName {
            get;
            set;
        }

        private Boolean m_Enabled = false;
        [XmlAttribute]
        public virtual Boolean Enabled {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        private String m_ModuleType = "KPP Module";
        [XmlIgnore]
        public virtual String ModuleType {
            get { return m_ModuleType; }
            private set { m_ModuleType = value; }
        }


        private Boolean m_ModuleStarted = false;
        [XmlIgnore]
        public virtual Boolean ModuleStarted {
            get { return m_ModuleStarted; }
            internal set { m_ModuleStarted = value; }
        }

        public virtual void StopModule() {


        }

        public virtual Object GetModelForm() {
            return null;
        }

        public KPPModule() {
            DebugController.ActiveDebugController = new DebugController(Path.Combine(Application.StartupPath, "app.log"));




        }

        public KPPModule(String LoadProjectName)
            : this() {
        }

        public KPPModule(int LoadProjectID)
            : this() {

        }


        public virtual String DockFile {
            get;
            set;
        }



        public virtual String AppFile {
            get;
            set;
        }


        public virtual Boolean StartModule() {


            return ModuleStarted;
        }

        public virtual void ShowModule(DockPanel dockingpanel) {
        }

        public virtual void StartModule(DockPanel dockingpanel) {

        }

       

        public override string ToString() {
            return ModuleName;
        }
    }


    public sealed class ApplicationSettings {


         private List<UserDef> m_Users = new List<UserDef>();

        public List<UserDef> Users {
            get { return m_Users; }
            set { m_Users = value; }
        }

        public static UserDef SelectedUser = null;


        #region -  Serialization attributes  -

        public static Int32 S_BackupFilesToKeep = 5;
        public static String S_BackupFolderName = "backup";
        public static String S_BackupExtention = "bkp";
        public static String S_DefaulFileExtention = "xml";

        private String _filePath = null;
        private String _defaultPath = null;

        [XmlIgnore]
        public Int32 BackupFilesToKeep { get; set; }
        [XmlIgnore]
        public String BackupFolderName { get; set; }
        [XmlIgnore]
        public String BackupExtention { get; set; }

        #endregion
        private static KPPLogger log = new KPPLogger(typeof(ApplicationSettings));

        [XmlAttribute]
        public String Name { get; set; }



        private CustomCollection<KPPModule> m_Modules = new CustomCollection<KPPModule>();
        //[XmlIgnore]
        [Category("Modules Definition")]
        [TypeConverter(typeof(ExpandableObjectConverter)), EditorAttribute(typeof(ModuleAddRemoveSelector), typeof(UITypeEditor))]
        public CustomCollection<KPPModule> Modules {
            get { return m_Modules; }
            set { m_Modules = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public ApplicationSettings() {
            Name = "Application Settings";
            //Visions.Add

        }

        #region Read Operations

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static ApplicationSettings ReadConfigurationFile(string path) {
            //log.Debug(String.Format("Load Xml file://{0}", path));
            if (File.Exists(path)) {
                ApplicationSettings result = null;
                TextReader reader = null;

                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                    reader = new StreamReader(path);
                    ApplicationSettings config = serializer.Deserialize(reader) as ApplicationSettings;
                    config._filePath = path;

                    result = config;
                }
                catch (Exception exp) {
                    log.Error(exp);
                }
                finally {
                    if (reader != null) {
                        reader.Close();
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="childtype">The childtype.</param>
        /// <param name="xmlString">The XML string.</param>
        /// <returns></returns>
        public static ApplicationSettings ReadConfigurationString(string xmlString) {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                ApplicationSettings config = serializer.Deserialize(new StringReader(xmlString)) as ApplicationSettings;

                return config;
            }
            catch (Exception exp) {
                log.Error(exp);
            }
            return null;
        }

        #endregion

        #region Write Operations

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        public void WriteConfigurationFile() {
            if (_filePath != null) {
                WriteConfigurationFile(_filePath);
            }
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        public void WriteConfigurationFile(string path) {
            WriteConfiguration(this, path, BackupFolderName, BackupExtention, BackupFilesToKeep);
        }

        /// <summary>
        /// Writes the configuration string.
        /// </summary>
        /// <returns></returns>
        public String WriteConfigurationToString() {
            return WriteConfigurationToString(this);
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        public static void WriteConfiguration(ApplicationSettings config, string path) {
            WriteConfiguration(config, path, S_BackupFolderName, S_BackupExtention, S_BackupFilesToKeep);
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        public static void WriteConfiguration(ApplicationSettings config, string path, string backupFolderName, String backupExtention, Int32 backupFilesToKeep) {
            if (File.Exists(path) && backupFilesToKeep > 0) {
                //Do a file backup prior to overwrite
                try {
                    //Check if valid backup folder name
                    if (backupFolderName == null || backupFolderName.Length == 0) {
                        backupFolderName = "backup";
                    }

                    //Check Backup folder
                    String bkpFolder = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Config"), backupFolderName);
                    if (!Directory.Exists(bkpFolder)) {
                        Directory.CreateDirectory(bkpFolder);
                    }

                    //Check extention
                    String ext = backupExtention != null && backupExtention.Length > 0 ? backupExtention : Path.GetExtension(path);
                    if (!ext.StartsWith(".")) { ext = String.Format(".{0}", ext); }

                    //Delete existing backup file (This should not exist)
                    String bkpFile = Path.Combine(bkpFolder, String.Format("{0}_{1:yyyyMMddHHmmss}{2}", Path.GetFileNameWithoutExtension(path), DateTime.Now, ext));
                    if (File.Exists(bkpFile)) { File.Delete(bkpFile); }

                    //Delete excess backup files
                    String fileSearchPattern = String.Format("{0}_*{1}", Path.GetFileNameWithoutExtension(path), ext);
                    String[] bkpFilesList = Directory.GetFiles(bkpFolder, fileSearchPattern, SearchOption.TopDirectoryOnly);
                    if (bkpFilesList != null && bkpFilesList.Length > (backupFilesToKeep - 1)) {
                        bkpFilesList = bkpFilesList.OrderByDescending(f => f.ToString()).ToArray();
                        for (int i = (backupFilesToKeep - 1); i < bkpFilesList.Length; i++) {
                            File.Delete(bkpFilesList[i]);
                        }
                    }

                    //Backup current file
                    File.Copy(path, bkpFile);
                    //log.Debug(String.Format("Backup file://{0} to file://{1}", path, bkpFile));
                }
                catch (Exception exp) {
                    //log.Error(String.Format("Error copying file {0} to backup.", path), exp);
                }
            }
            try {
                XmlSerializer serializer = new XmlSerializer(config.GetType());

                //serializer.

                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, config);
                textWriter.Close();
                //log.Debug(String.Format("Write Xml file://{0}", path));
            }
            catch (Exception exp) {
                //log.Error("Error writing configuration. ", exp);
                Console.WriteLine(exp.ToString());
            }
        }

        /// <summary>
        /// Writes the configuration to string.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static String WriteConfigurationToString(ApplicationSettings config) {
            try {
                XmlSerializer serializer = new XmlSerializer(config.GetType());
                StringWriter stOut = new StringWriter();
                serializer.Serialize(stOut, config);
                return stOut.ToString();
            }
            catch (Exception exp) {
                //log.Error("Error writing configuration. ", exp);
            }
            return null;
        }

        #endregion
    }
    

}
