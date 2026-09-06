using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;

namespace TestWithOHM
{
    public class InfoRetriever
    {
        private Computer computer;
        public InfoRetriever()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
        }
        public List<SystemTemperature> GetSystemInfo()
        {
            var systemTemperatures = new List<SystemTemperature>();

            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                var sysTemp = new SystemTemperature(computer.Hardware[i].Name);
                for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                {
                    if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                        sysTemp.Temperatures.Add(new Tuple<string, float?>(computer.Hardware[i].Sensors[j].Name, computer.Hardware[i].Sensors[j].Value));
                }

                systemTemperatures.Add(sysTemp);
            }

            return systemTemperatures;
        }

        ~InfoRetriever()
        {
            computer.Close();
        }
    }
}
