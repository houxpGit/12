using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    /********Googoltech InputOutputCard Information/********
    *Vendor: Googoltech 固高科技
    *Type: InputOutputCard
    *Hardware Interface Type: PCI
    *Version: 1.0.1.0
    *Author：WuChenJie
    ****************************************************/
    public class GoogoTechInputOutputCard : HardWareBase, IInputAction, IOutputAction
    {

        private bool[] bBitInputStatus = new bool[128];
        private bool[] bBitOutputStatus = new bool[128];
        public ushort usCardNo = 0;
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
        public bool SetOutBit(int iBit, bool bOn)
        {
            if (iBit < 128 && iBit > -1)
            {
                lock (lockObj)
                {
                    ushort uValue = bOn ? (ushort)0 : (ushort)1;
                    LTDIO.ioc_write_outbit(usCardNo, (ushort)(iBit + 1), uValue);
                }
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
                return bBitOutputStatus[iBit];
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
