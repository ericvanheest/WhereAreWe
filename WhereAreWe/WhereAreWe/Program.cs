using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace WhereAreWe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (Properties.Settings.Default.MaxUndoActions == -1)
                {
                    File.WriteAllText(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, Properties.Resources.app);
                }
            }
            catch(Exception)
            {
                if (MessageBox.Show("Could not locate the application's .config file.  Create a new file?", "Create new app.config file?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
                    return;
                File.WriteAllText(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, Properties.Resources.app);
                Application.Restart();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
