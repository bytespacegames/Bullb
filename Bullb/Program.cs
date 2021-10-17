using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullb
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Mutex mutex = new Mutex(false, appGuid);
            //if (!mutex.WaitOne(0, false))
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Settings.bulbsettings");
                for (int i = 0; i < 9; i++) { tr.ReadLine(); }
                if (tr.ReadLine() == "true")
                {
                    String ln10 = tr.ReadLine();
                    if (ln10.Contains(".bullbform"))
                    {
                        Bullb.runBullbForm(ln10);
                    }
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Bullb());
        }
            private static string appGuid = "4183b785-cafe-427f-9fa6-6b536181ca06";
    }
}
