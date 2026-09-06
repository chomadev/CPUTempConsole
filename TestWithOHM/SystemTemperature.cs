using System;
using System.Collections.Generic;

namespace TestWithOHM
{
    public class SystemTemperature
    {
        public string Name;
        public List<Tuple<string, float?>> Temperatures = new List<Tuple<string, float?>>();

        public SystemTemperature(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $@"{Name}
{String.Join("|", Temperatures)}
";
        }
    }
}
