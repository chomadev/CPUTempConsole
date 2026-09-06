using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;

namespace CPUTempConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //foreach (var temp in Temperature.Temperatures)
            //{
            //    Console.WriteLine(temp);
            //}
        }
        
    }

    public class Temperature
    {
        public double CurrentValue { get; set; }
        public string InstanceName { get; set; }
        public static List<Temperature> Temperatures
        {
            get
            {
                List<Temperature> result = new List<Temperature>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (ManagementObject obj in searcher.Get())
                {
                    Double temperature = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                    temperature = (temperature - 2732) / 10.0;
                    result.Add(new Temperature { CurrentValue = temperature, InstanceName = obj["InstanceName"].ToString() });
                }
                return result;
            }
        }

        public override string ToString()
        {
            return $"{InstanceName}: {CurrentValue}";
        }
    }


}
