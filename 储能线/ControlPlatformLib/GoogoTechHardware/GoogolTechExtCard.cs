using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class GoogolTechExtCard : HardWareBase, IInputAction, IOutputAction
    {
        public short usCardNo = 0;
        public short usExtNo = 0;
        private bool[] bBitInputStatus = new bool[64];

        private bool[] bBitOutputStatus = new bool[64];
        public override bool Init(HardWareInfoBase infoHardWare)
        {
            GoogolTechExtCardInfo googoTechMCInfo = infoHardWare as GoogolTechExtCardInfo;
            Global.logger.InfoFormat("初始化固高拓展卡,卡名称{0}", googoTechMCInfo.hardwareName);
            usCardNo = (short)googoTechMCInfo.iCardNo;
            usExtNo = (short)googoTechMCInfo.iExtCardNo;
            short sRtn = 0;
            sRtn = gts.mc.GT_OpenExtMdl((short)usCardNo,googoTechMCInfo.m_strExtDllName);
            if (!HandleErrorMessage(sRtn))
            {
                Global.logger.ErrorFormat("初始化固高拓展卡{0}失败", googoTechMCInfo.hardwareName);
                bInitOK = false;
                return false;
            }
            //sRtn = gts.mc.GT_Stop(0xFF, 0);

            sRtn = gts.mc.GT_ResetExtMdl((short)usCardNo);
            sRtn = gts.mc.GT_LoadExtConfig((short)usCardNo, googoTechMCInfo.m_strConfigPath);
            //sRtn = gts.mc.GT_ClrSts((short)usCardNo, 1, 8);//清除轴报警和限位
            bInitOK = true;

            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }

        private void ScanThreadFunction()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(10);
                    
                    GetAllIOStatus();
                }
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("固高拓展卡运行出现错误：{0}", ex.Message);
                throw;
            }
        }

        private void GetAllIOStatus()
        {
            lock (lockObj)
            {
                ushort uiInput = 0;
                ushort uiOutput = 0;
                short sRtn;
                try
                {
                    sRtn = gts.mc.GT_GetExtIoValue(usCardNo,usExtNo , out uiInput);
                    sRtn = gts.mc.GT_GetExtDoValue(usCardNo, usExtNo, out uiOutput);
                }
                catch
                {
                }
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
            }
        }
        

        public bool HandleErrorMessage(short errorMessage)
        {
            if (errorMessage != 0)
            {
                return false;
            }
            return true;
        }

        public override bool Close()
        {
            try
            {
                short sRtn = gts.mc.GT_CloseExtMdl(usCardNo);
                return HandleErrorMessage(sRtn);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetInputBit(int iBit)
        {
            return bBitInputStatus[iBit];
        }

        public bool GetOutBit(int iBit)
        {
            return bBitOutputStatus[iBit];
        }

        public bool SetOutBit(int iBit, bool bOn)
        {
            ushort uiInput = bOn == true ? (ushort)0 : (ushort)1; ;
            try
            {
                short sRtn = gts.mc.GT_SetExtIoBit(usCardNo, usExtNo, (short)iBit, uiInput);
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("固高拓展卡设置输出点位出现错误：{0}", ex.Message);
                return false;
            }
        }
    }
}
