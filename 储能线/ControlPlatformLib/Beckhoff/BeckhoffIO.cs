using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ControlPlatformLib.Beckhoff
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class BeckhoffIO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
        public bool[] EL1889=new bool[65];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
        public bool[] EL2889 = new bool[65];
    }
}
