using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    /*****************ADLINK InputCard Information*****************
     *Vendor: ADLINK 凌华
     *Type: InputCard
     *Hardware Interface Type: PCI
     *Version: 1.0.1.0
     *Author：WuChenJie
     **************************************************************/
    public class ADLINKInputCard : HardWareBase, IInputAction
    {
        public ushort usCardNo = 0;
        public bool GetInputBit(int iBit)
        {
            bool ret = false;
            ushort int_value = 0;
            try
            {
                lock (lockObj)
                {
                    DASK.DI_ReadLine(usCardNo, 0, (ushort)iBit, out int_value);
                    ret = int_value == 0 ? false : true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return ret;
        }

        public override bool Init(HardWareInfoBase infoHardWare)
        {
           // Global.logger.Info("初始化凌华IO卡");
            ADLINKInputInfo tempInfo = (ADLINKInputInfo)infoHardWare;
            usCardNo = (ushort)tempInfo.iCardNo;
            bool ret;
            try
            {
                ret = DASK.Register_Card(tempInfo.hardwareModel, usCardNo) == 0 ? true : false;
            }
            catch (Exception)
            {
            //    Global.logger.Info("初始化凌华IO卡失败!");
                return false;
            }
         //   Global.logger.Info("初始化凌华IO卡成功！");
            return ret;
        }

        public override bool Close()
        {
            bool ret;
            try
            {
                ret = DASK.Release_Card(usCardNo) == 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
            return ret;
        }
        
    }
}
