using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestWithOHM;

namespace CPUTempTray
{
    public class Program
    {
        public static List<SystemTemperature> CurrentTemp { get; private set; }
        public static bool isRunning { get; private set; } = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormInfo());
        }
    }
}
