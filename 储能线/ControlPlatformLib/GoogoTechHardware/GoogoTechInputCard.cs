using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    /*****************Googoltech InputCard Information*****************
     *Vendor: Googoltech 固高科技
     *Type: InputCard
     *Hardware Interface Type: PCI
     *Version: 1.0.1.0
     *Author：WuChenJie
     **************************************************************/
    public class GoogoTechInputCard : HardWareBase, IInputAction
    {
        private bool[] bBitInputStatus = new bool[128];
        public bool GetInputBit(int iBit)
        {
            if (iBit < 128 && iBit > -1)
            {
                return bBitInputStatus[iBit];
            }
            else
            {
                return false;
            }
        }
        override public bool Init(HardWareInfoBase infoHardWare)
        {
            bInitOK = true;
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            System.Threading.Thread.Sleep(1000);


            int iStep = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                switch (iStep)
                {
                    case 0:
                        {
                            timer.Start();
                            iStep = 10;
                        }
                        break;
                    case 10:
                        {
                            if (timer.TimeUp(1))
                            {
                                timer.Start();
                                for (int i = 0; i < 128; i++)
                                {
                                    bBitInputStatus[i] = true;
                                }
                                iStep = 20;
                            }
                        }
                        break;
                    case 20:
                        {
                            if (timer.TimeUp(1))
                            {
                                for (int i = 0; i < 128; i++)
                                {
                                    bBitInputStatus[i] = false;
                                }
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
