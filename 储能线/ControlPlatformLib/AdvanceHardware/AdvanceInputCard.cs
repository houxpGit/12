using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automation.BDaq;

namespace ControlPlatformLib
{
    public class AdvanceInputCard : HardWareBase, IInputAction
    {
        private bool[] bBitInputStatus = new bool[64];
        public int iCardNo = 0;
        private BDaqDevice m_device;
        private BDaqDio m_dio;
        private byte[] portData = new byte[8];

        public bool GetInputBit(int iBit)
        {
            if (iBit < 64 && iBit > -1)
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
            AdvanceInputInfo hardWareInfo = (AdvanceInputInfo)infoHardWare;
            iCardNo = hardWareInfo.iCardNo;
            ErrorCode ret;
            try
            {
                ret = BDaqDevice.Open(iCardNo, AccessMode.ModeWriteWithReset, out m_device);
                if (ErrorCode.Success == ret)
                {
                    ret = m_device.GetModule(0, out m_dio);
                    if (ErrorCode.Success == ret)
                    {

                        bInitOK = true;

                    }
                    else
                    {
                        bInitOK = false;
                        return false;
                    }
                }
                else
                {
                    bInitOK = false;
                    return false;
                }
            }
            catch
            {
                bInitOK = false;
                return false;
            }

            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            System.Threading.Thread.Sleep(1000);

            while (true)
            {
                System.Threading.Thread.Sleep(50);
                GetAllIOStatus();
            }
        }
        public void GetAllIOStatus()
        {
            ErrorCode ret;
            lock (lockObj)
            {
                ret = m_dio.DiRead(0, 8, portData);
            }
            string strTest;
            for (int i = 0; i < 8; i++)
            {
                strTest = Convert.ToString(portData[i], 2);
                strTest = strTest.PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    if (strTest[7 - j] == '1')
                        bBitInputStatus[i * 8 + j] = true;
                    else
                        bBitInputStatus[i * 8 + j] = false;
                }
            }
        }
    }
}
