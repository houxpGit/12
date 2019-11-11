using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib;

namespace ControlPlatformLib
{
    /********Leadtech InputOutputCard Information/********
    *Vendor: Leadtech 雷塞
    *Type: InputOutputCard
    *Hardware Interface Type: PCI
    *Version: 1.0.1.0
    *Author：WuChenJie
    ****************************************************/
    public class LEISAIInputOutputCard : HardWareBase, IInputAction, IOutputAction
    {
        static int nCardTotal;
        private bool[] bBitInputStatus = new bool[128];
        private bool[] bBitOutputStatus = new bool[128];
        public ushort usCardNo = 0;

        public override bool Close()
        {
            try
            {
                LTDIO.ioc_board_close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
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
            Global.logger.Info("初始化雷塞IO卡");
            LEISAIInputOutputInfo tempInfo = (LEISAIInputOutputInfo)infoHardWare;
            if (nCardTotal > 0)
            {
                if (nCardTotal >= tempInfo.iCardNo)
                {
                    bInitOK = true;
                    usCardNo = (ushort)tempInfo.iCardNo;
                }
                else
                {
                    bInitOK = false;
                    return false;

                }
            }
            else
            {
                nCardTotal = LTDIO.ioc_board_init();
                if (nCardTotal <= 0)//控制卡初始化
                {
                    Global.logger.Error("初始化雷塞IO卡失败");
                    bInitOK = false;
                    return false;
                }
                if (nCardTotal >= tempInfo.iCardNo)
                {
                    Global.logger.Info("初始化雷塞IO卡成功");
                    bInitOK = true;
                    usCardNo = (ushort)tempInfo.iCardNo;
                }
                else
                {
                    Global.logger.Error("初始化雷塞IO卡失败");
                    bInitOK = false;
                    return false;
                }
            }
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            while (true)
            {
                System.Threading.Thread.Sleep(50);
                GetAllIOStatus();
            }
        }
        public void GetAllIOStatus()
        {
            lock (lockObj)
            {
                uint uiInput = LTDIO.ioc_read_inport(usCardNo, 0);
                uint uiOutput = LTDIO.ioc_read_outport(usCardNo, 0);
                bBitInputStatus[0] = ((uiInput & 0x1) == 0) ? true : false;
                bBitInputStatus[1] = ((uiInput & 0x2) == 0) ? true : false;
                bBitInputStatus[2] = ((uiInput & 0x4) == 0) ? true : false;
                bBitInputStatus[3] = ((uiInput & 0x8) == 0) ? true : false;
                bBitInputStatus[4] = ((uiInput & 0x10) == 0) ? true : false;
                bBitInputStatus[5] = ((uiInput & 0x20) == 0) ? true : false;
                bBitInputStatus[6] = ((uiInput & 0x40) == 0) ? true : false;
                bBitInputStatus[7] = ((uiInput & 0x80) == 0) ? true : false;
                bBitInputStatus[8] = ((uiInput & 0x100) == 0) ? true : false;
                bBitInputStatus[9] = ((uiInput & 0x200) == 0) ? true : false;
                bBitInputStatus[10] = ((uiInput & 0x400) == 0) ? true : false;
                bBitInputStatus[11] = ((uiInput & 0x800) == 0) ? true : false;
                bBitInputStatus[12] = ((uiInput & 0x1000) == 0) ? true : false;
                bBitInputStatus[13] = ((uiInput & 0x2000) == 0) ? true : false;
                bBitInputStatus[14] = ((uiInput & 0x4000) == 0) ? true : false;
                bBitInputStatus[15] = ((uiInput & 0x8000) == 0) ? true : false;
                bBitInputStatus[16] = ((uiInput & 0x10000) == 0) ? true : false;
                bBitInputStatus[17] = ((uiInput & 0x20000) == 0) ? true : false;
                bBitInputStatus[18] = ((uiInput & 0x40000) == 0) ? true : false;
                bBitInputStatus[19] = ((uiInput & 0x80000) == 0) ? true : false;
                bBitInputStatus[20] = ((uiInput & 0x100000) == 0) ? true : false;
                bBitInputStatus[21] = ((uiInput & 0x200000) == 0) ? true : false;
                bBitInputStatus[22] = ((uiInput & 0x400000) == 0) ? true : false;
                bBitInputStatus[23] = ((uiInput & 0x800000) == 0) ? true : false;
                bBitInputStatus[24] = ((uiInput & 0x1000000) == 0) ? true : false;
                bBitInputStatus[25] = ((uiInput & 0x2000000) == 0) ? true : false;
                bBitInputStatus[26] = ((uiInput & 0x4000000) == 0) ? true : false;
                bBitInputStatus[27] = ((uiInput & 0x8000000) == 0) ? true : false;
                bBitInputStatus[28] = ((uiInput & 0x10000000) == 0) ? true : false;
                bBitInputStatus[29] = ((uiInput & 0x20000000) == 0) ? true : false;
                bBitInputStatus[30] = ((uiInput & 0x40000000) == 0) ? true : false;
                bBitInputStatus[31] = ((uiInput & 0x80000000) == 0) ? true : false;


                bBitOutputStatus[0] = ((uiOutput & 0x1) == 0) ? true : false;
                bBitOutputStatus[1] = ((uiOutput & 0x2) == 0) ? true : false;
                bBitOutputStatus[2] = ((uiOutput & 0x4) == 0) ? true : false;
                bBitOutputStatus[3] = ((uiOutput & 0x8) == 0) ? true : false;
                bBitOutputStatus[4] = ((uiOutput & 0x10) == 0) ? true : false;
                bBitOutputStatus[5] = ((uiOutput & 0x20) == 0) ? true : false;
                bBitOutputStatus[6] = ((uiOutput & 0x40) == 0) ? true : false;
                bBitOutputStatus[7] = ((uiOutput & 0x80) == 0) ? true : false;
                bBitOutputStatus[8] = ((uiOutput & 0x100) == 0) ? true : false;
                bBitOutputStatus[9] = ((uiOutput & 0x200) == 0) ? true : false;
                bBitOutputStatus[10] = ((uiOutput & 0x400) == 0) ? true : false;
                bBitOutputStatus[11] = ((uiOutput & 0x800) == 0) ? true : false;
                bBitOutputStatus[12] = ((uiOutput & 0x1000) == 0) ? true : false;
                bBitOutputStatus[13] = ((uiOutput & 0x2000) == 0) ? true : false;
                bBitOutputStatus[14] = ((uiOutput & 0x4000) == 0) ? true : false;
                bBitOutputStatus[15] = ((uiOutput & 0x8000) == 0) ? true : false;
                bBitOutputStatus[16] = ((uiOutput & 0x10000) == 0) ? true : false;
                bBitOutputStatus[17] = ((uiOutput & 0x20000) == 0) ? true : false;
                bBitOutputStatus[18] = ((uiOutput & 0x40000) == 0) ? true : false;
                bBitOutputStatus[19] = ((uiOutput & 0x80000) == 0) ? true : false;
                bBitOutputStatus[20] = ((uiOutput & 0x100000) == 0) ? true : false;
                bBitOutputStatus[21] = ((uiOutput & 0x200000) == 0) ? true : false;
                bBitOutputStatus[22] = ((uiOutput & 0x400000) == 0) ? true : false;
                bBitOutputStatus[23] = ((uiOutput & 0x800000) == 0) ? true : false;
                bBitOutputStatus[24] = ((uiOutput & 0x1000000) == 0) ? true : false;
                bBitOutputStatus[25] = ((uiOutput & 0x2000000) == 0) ? true : false;
                bBitOutputStatus[26] = ((uiOutput & 0x4000000) == 0) ? true : false;
                bBitOutputStatus[27] = ((uiOutput & 0x8000000) == 0) ? true : false;
                bBitOutputStatus[28] = ((uiOutput & 0x10000000) == 0) ? true : false;
                bBitOutputStatus[29] = ((uiOutput & 0x20000000) == 0) ? true : false;
                bBitOutputStatus[30] = ((uiOutput & 0x40000000) == 0) ? true : false;
                bBitOutputStatus[31] = ((uiOutput & 0x80000000) == 0) ? true : false;

                uint uiInput1 = LTDIO.ioc_read_inport(usCardNo, 1);
                uint uiOutput1 = LTDIO.ioc_read_outport(usCardNo, 1);
                bBitInputStatus[32] = ((uiInput1 & 0x1) == 0) ? true : false;
                bBitInputStatus[33] = ((uiInput1 & 0x2) == 0) ? true : false;
                bBitInputStatus[34] = ((uiInput1 & 0x4) == 0) ? true : false;
                bBitInputStatus[35] = ((uiInput1 & 0x8) == 0) ? true : false;
                bBitInputStatus[36] = ((uiInput1 & 0x10) == 0) ? true : false;
                bBitInputStatus[37] = ((uiInput1 & 0x20) == 0) ? true : false;
                bBitInputStatus[38] = ((uiInput1 & 0x40) == 0) ? true : false;
                bBitInputStatus[39] = ((uiInput1 & 0x80) == 0) ? true : false;
                bBitInputStatus[40] = ((uiInput1 & 0x100) == 0) ? true : false;
                bBitInputStatus[41] = ((uiInput1 & 0x200) == 0) ? true : false;
                bBitInputStatus[42] = ((uiInput1 & 0x400) == 0) ? true : false;
                bBitInputStatus[43] = ((uiInput1 & 0x800) == 0) ? true : false;
                bBitInputStatus[44] = ((uiInput1 & 0x1000) == 0) ? true : false;
                bBitInputStatus[45] = ((uiInput1 & 0x2000) == 0) ? true : false;
                bBitInputStatus[46] = ((uiInput1 & 0x4000) == 0) ? true : false;
                bBitInputStatus[47] = ((uiInput1 & 0x8000) == 0) ? true : false;
                bBitInputStatus[48] = ((uiInput1 & 0x10000) == 0) ? true : false;
                bBitInputStatus[49] = ((uiInput1 & 0x20000) == 0) ? true : false;
                bBitInputStatus[50] = ((uiInput1 & 0x40000) == 0) ? true : false;
                bBitInputStatus[51] = ((uiInput1 & 0x80000) == 0) ? true : false;
                bBitInputStatus[52] = ((uiInput1 & 0x100000) == 0) ? true : false;
                bBitInputStatus[53] = ((uiInput1 & 0x200000) == 0) ? true : false;
                bBitInputStatus[54] = ((uiInput1 & 0x400000) == 0) ? true : false;
                bBitInputStatus[55] = ((uiInput1 & 0x800000) == 0) ? true : false;
                bBitInputStatus[56] = ((uiInput1 & 0x1000000) == 0) ? true : false;
                bBitInputStatus[57] = ((uiInput1 & 0x2000000) == 0) ? true : false;
                bBitInputStatus[58] = ((uiInput1 & 0x4000000) == 0) ? true : false;
                bBitInputStatus[59] = ((uiInput1 & 0x8000000) == 0) ? true : false;
                bBitInputStatus[60] = ((uiInput1 & 0x10000000) == 0) ? true : false;
                bBitInputStatus[61] = ((uiInput1 & 0x20000000) == 0) ? true : false;
                bBitInputStatus[62] = ((uiInput1 & 0x40000000) == 0) ? true : false;
                bBitInputStatus[63] = ((uiInput1 & 0x80000000) == 0) ? true : false;


                bBitOutputStatus[32] = ((uiOutput1 & 0x1) == 0) ? true : false;
                bBitOutputStatus[33] = ((uiOutput1 & 0x2) == 0) ? true : false;
                bBitOutputStatus[34] = ((uiOutput1 & 0x4) == 0) ? true : false;
                bBitOutputStatus[35] = ((uiOutput1 & 0x8) == 0) ? true : false;
                bBitOutputStatus[36] = ((uiOutput1 & 0x10) == 0) ? true : false;
                bBitOutputStatus[37] = ((uiOutput1 & 0x20) == 0) ? true : false;
                bBitOutputStatus[38] = ((uiOutput1 & 0x40) == 0) ? true : false;
                bBitOutputStatus[39] = ((uiOutput1 & 0x80) == 0) ? true : false;
                bBitOutputStatus[40] = ((uiOutput1 & 0x100) == 0) ? true : false;
                bBitOutputStatus[41] = ((uiOutput1 & 0x200) == 0) ? true : false;
                bBitOutputStatus[42] = ((uiOutput1 & 0x400) == 0) ? true : false;
                bBitOutputStatus[43] = ((uiOutput1 & 0x800) == 0) ? true : false;
                bBitOutputStatus[44] = ((uiOutput1 & 0x1000) == 0) ? true : false;
                bBitOutputStatus[45] = ((uiOutput1 & 0x2000) == 0) ? true : false;
                bBitOutputStatus[46] = ((uiOutput1 & 0x4000) == 0) ? true : false;
                bBitOutputStatus[47] = ((uiOutput1 & 0x8000) == 0) ? true : false;
                bBitOutputStatus[48] = ((uiOutput1 & 0x10000) == 0) ? true : false;
                bBitOutputStatus[49] = ((uiOutput1 & 0x20000) == 0) ? true : false;
                bBitOutputStatus[50] = ((uiOutput1 & 0x40000) == 0) ? true : false;
                bBitOutputStatus[51] = ((uiOutput1 & 0x80000) == 0) ? true : false;
                bBitOutputStatus[52] = ((uiOutput1 & 0x100000) == 0) ? true : false;
                bBitOutputStatus[53] = ((uiOutput1 & 0x200000) == 0) ? true : false;
                bBitOutputStatus[54] = ((uiOutput1 & 0x400000) == 0) ? true : false;
                bBitOutputStatus[55] = ((uiOutput1 & 0x800000) == 0) ? true : false;
                bBitOutputStatus[56] = ((uiOutput1 & 0x1000000) == 0) ? true : false;
                bBitOutputStatus[57] = ((uiOutput1 & 0x2000000) == 0) ? true : false;
                bBitOutputStatus[58] = ((uiOutput1 & 0x4000000) == 0) ? true : false;
                bBitOutputStatus[59] = ((uiOutput1 & 0x8000000) == 0) ? true : false;
                bBitOutputStatus[60] = ((uiOutput1 & 0x10000000) == 0) ? true : false;
                bBitOutputStatus[61] = ((uiOutput1 & 0x20000000) == 0) ? true : false;
                bBitOutputStatus[62] = ((uiOutput1 & 0x40000000) == 0) ? true : false;
                bBitOutputStatus[63] = ((uiOutput1 & 0x80000000) == 0) ? true : false;
            }
        }
    }
}
