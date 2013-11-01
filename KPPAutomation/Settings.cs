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

namespace KPPAutomation {

    
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

       

        private KPPVision m_Vision = new KPPVision();
        //[XmlIgnore]
        [Category("Modules Definition")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public KPPVision Vision {
            get { return m_Vision; }
            set { m_Vision = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public ApplicationSettings() {
            Name = "Application Settings";
           

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
