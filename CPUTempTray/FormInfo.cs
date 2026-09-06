using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using TestWithOHM;

namespace CPUTempTray
{
    public partial class FormInfo : Form
    {
        private static int Seconds = 5;
        private static int CriticalTemperature = 80;
        private SoundPlayer warningSound = new SoundPlayer("warning.wav");

        public FormInfo()
        {
            InitializeComponent();
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                var infoRetriever = new InfoRetriever();
                var temps = infoRetriever.GetSystemInfo();
                BeginInvoke((MethodInvoker)delegate
                {
                    var shouldWarn = false;
                    foreach (var temp in temps)
                    {
                        var hardwareNode = tvInfo.Nodes.Find(temp.Name, false).FirstOrDefault();
                        if (hardwareNode == null)
                        {
                            hardwareNode = tvInfo.Nodes.Add(temp.Name, temp.Name);
                        }
                        foreach (var item in temp.Temperatures)
                        {
                            var sensorNode = hardwareNode.Nodes.Find(item.Item1, false).FirstOrDefault();
                            if (sensorNode == null)
                            {
                                hardwareNode.Nodes.Add(item.Item1, item.ToString());
                            }
                            else
                            {
                                sensorNode.Text = item.ToString();
                            }

                            if (item.Item2 > CriticalTemperature)
                            {
                                shouldWarn = true;
                            }
                        }
                    }
                    if (shouldWarn && cbPlaySounds.Checked)
                        warningSound.Play();
                    tvInfo.ExpandAll();
                });
                Thread.Sleep(Seconds * 1000);
            }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out var seconds))
            {
                Seconds = seconds;
            }
        }

        private void FormInfo_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out var critTemp))
            {
                CriticalTemperature = critTemp;
            }
        }
    }
}
