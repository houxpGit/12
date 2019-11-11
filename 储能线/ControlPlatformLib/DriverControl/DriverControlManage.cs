using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    static public class DriverControlManage
    {
        public static List<IControlDriver> controls;
        public static void StartScan()
        {
            controls = new List<IControlDriver>();
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThread);
            threadScan.IsBackground = true;
            threadScan.Start();
        }
        public static void ScanThread()
        {

            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            while (true)
            {
                timer.Start();

                try
                {
                    foreach (IControlDriver controlItem in controls)
                    {
                        controlItem.GetDriverStatus();
                        controlItem.FreshDriverStatus();

                    }
                }
                catch
                {

                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
