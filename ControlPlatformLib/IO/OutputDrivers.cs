using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class OutputDrivers
    {
        public Dictionary<string, OutputDriver> drivers;
        public OutputDrivers()
        {
            drivers = new Dictionary<string, OutputDriver>();
        }
    }
}
