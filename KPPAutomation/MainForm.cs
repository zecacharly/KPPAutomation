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

namespace KPPAutomation {
    public partial class MainForm : Form {

        public Boolean SetAdmin = false;

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
    }
}
