using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;

namespace ControlPlatformLib
{
    public class AdvanceOutputCard : HardWareBase, IOutputAction
    {
        private bool[] bBitStatus = new bool[64];
        public int iCardNo = 0;
        private BDaqDevice m_device;
        private BDaqDio m_dio;
        private byte[] portData = new byte[8];

        public bool SetOutBit(int iBit, bool bOn)
        {
            if (bInitOK == false)
                return false;
            ErrorCode ret;
            int port = iBit / 8;
            int Chanalbit = iBit % 8;


            lock (lockObj)
            {
                //更新现在值
                ret = m_dio.DoRead(0, 8, portData);

                string strTest;
                for (int i = 0; i < 8; i++)
                {
                    strTest = Convert.ToString(portData[i], 2);
                    strTest = strTest.PadLeft(8, '0');
                    for (int j = 0; j < 8; j++)
                    {
                        if (strTest[7 - j] == '1')
                            bBitStatus[i * 8 + j] = true;
                        else
                            bBitStatus[i * 8 + j] = false;
                    }
                }
                byte portValue;
                if (bOn)
                {
                    portValue = (byte)(portData[port] | (1 << (Chanalbit)));
                }
                else
                {
                    byte ValveTest = (byte)~(1 << (Chanalbit));
                    portValue = (byte)(portData[port] & ValveTest);
                }

                //写入值 
                ret = m_dio.DoWrite(port, portValue);
            }
            return true;
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
            AdvanceOutputInfo hardWareInfo = (AdvanceOutputInfo)infoHardWare;
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
                ret = m_dio.DoRead(0, 8, portData);
            }
            string strTest;
            for (int i = 0; i < 8; i++)
            {
                strTest = Convert.ToString(portData[i], 2);
                strTest = strTest.PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    if (strTest[7 - j] == '1')
                        bBitStatus[i * 8 + j] = true;
                    else
                        bBitStatus[i * 8 + j] = false;
                }
            }
        }
    }
}
