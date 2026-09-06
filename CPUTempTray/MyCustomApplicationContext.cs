using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using TestWithOHM;
using System.Collections.Generic;

namespace CPUTempTray
{
    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;


        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = new System.Drawing.Icon("chomadev.ico"),
                Text = "CPU Temp - choma.dev",
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Display Status", DisplayStatus),
                    new MenuItem("Exit", Exit),
                }),
                Visible = true
            };

        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            //isRunning = false;
            Application.Exit();
        }

        void DisplayStatus(object sender, EventArgs e)
        {
            new FormInfo().Show();
        }
    }
}
