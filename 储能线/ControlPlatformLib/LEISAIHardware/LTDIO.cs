using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace ControlPlatformLib
{
    public static class LTDIO
    {
        [DllImport("IOC0640.dll", EntryPoint = "ioc_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_board_close();

        //---------------------   板卡初始和配置函数  ----------------------
        [DllImport("IOC0640.dll", EntryPoint = "ioc_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_board_init();

        //------------------------通用IO-----------------------
        [DllImport("IOC0640.dll", EntryPoint = "ioc_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_read_inbit(UInt16 CardNo, UInt16 bitno);

        [DllImport("IOC0640.dll", EntryPoint = "ioc_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 ioc_read_inport(UInt16 CardNo, UInt16 portno);

        [DllImport("IOC0640.dll", EntryPoint = "ioc_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_read_outbit(UInt16 CardNo, UInt16 bitno);

        [DllImport("IOC0640.dll", EntryPoint = "ioc_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 ioc_read_outport(UInt16 CardNo, UInt16 portno);

        [DllImport("IOC0640.dll", EntryPoint = "ioc_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_write_outbit(UInt16 CardNo, UInt16 bitno, UInt16 on_off);

        [DllImport("IOC0640.dll", EntryPoint = "ioc_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short ioc_write_outport(UInt16 CardNo, UInt32 IoMask, UInt32 outport_val);
    }
}
