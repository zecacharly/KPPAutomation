using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KPP.Core.Debug;
using System.Threading;
using System.IO;
using System.Resources;

namespace KPPAutomation {
    static class Program {

        public enum LanguageName { Unk, PT, EN }
        public static Boolean Restart = true;



        private static LanguageName _Language = LanguageName.PT;

        public static LanguageName Language {
            get { return _Language; }
            set {
                if (_Language != value) {
                    _Language = value;
                    switch (value) {
                        case LanguageName.Unk:
                            break;
                        case LanguageName.PT:
                            break;
                        case LanguageName.EN:

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static string GetResourceText(this Object from, String ResVar) {
            return GetResourceText(from, "KPPAutomation.Resources.Language.Res", ResVar);
        }

        public static string GetResourceText(this Object from, String ResLocation, String ResVar) {
            try {
                //ComponentResourceManager resources = new ComponentResourceManager(Program.);
                ResourceManager res_man = new ResourceManager(ResLocation, from.GetType().Assembly);
                return res_man.GetString(ResVar, Thread.CurrentThread.CurrentUICulture);
            }
            catch (Exception exp) {

                return "Error getting resource";
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params String[] prms) {
            bool ok;
            Mutex m = new Mutex(true, "KPP.Automation.Software.v1.0", out ok);
            DebugController.ActiveDebugController = new DebugController(Path.Combine(Application.StartupPath, "app.log"));
            //String path = Path.GetDirectoryName(Application.ExecutablePath);
           // DebugController.ActiveDebugController = new DebugController(path);
            //log = new KPPLogger(typeof(t));
            Boolean startadmin = false;
            if (prms != null && prms.Length > 0) {
                foreach (String prm in prms) {
                    switch (prm) {                      
                        case "-SetAdmin":
                            startadmin = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (ok) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                while (Restart) {
                    MainForm runningform = new MainForm();
                    try {

                        runningform.SetAdmin = startadmin;


                        Restart = false;
                        Application.Run(runningform);




                    }
                    catch (Exception exp) {
                        
                        Restart = false;


                      


                    }
                }
            }
        }
    }
}
