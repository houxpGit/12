using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class DemoOutputCard : HardWareBase, IOutputAction
    {
        private bool[] bBitStatus = new bool[128];
        public bool SetOutBit(int iBit, bool bOn)
        {
            if (iBit < 128 && iBit > -1)
            {
                bBitStatus[iBit] = bOn;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetOutBit(int iBit)
        {
            if (iBit < 128 && iBit > -1)
            {
                return bBitStatus[iBit];
            }
            else
            {
                return false;
            }

        }
        override public bool Init(HardWareInfoBase infoHardWare)
        {
            bInitOK = true;
            //System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            //threadScan.IsBackground = true;
            //threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            Random rdm = new Random();
            int iBit = rdm.Next(0, 127);
            int iStep = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(10);
                switch (iStep)
                {
                    case 0:
                        {
                            timer.Start();
                            iBit = rdm.Next(0, 127);
                            iStep = 10;
                        }
                        break;
                    case 10:
                        {
                            if (timer.TimeUp(1))
                            {
                                timer.Start();
                                bBitStatus[iBit] = true;
                                iStep = 20;
                            }
                        }
                        break;
                    case 20:
                        {
                            if (timer.TimeUp(1))
                            {
                                bBitStatus[iBit] = false;
                                iStep = 0;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
