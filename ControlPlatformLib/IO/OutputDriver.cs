using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    //public enum TableAxisName
    //{
    //    X,Y,Z,U,ALL
    //}
    public class OutputDriver
    {
        public string strDriverName;

        IOutputAction actionOutput;
        int iOutputNo;
        bool bIgnore = false;
        public string strRemark;
        public OutputData ioData;

        public bool bready = false;

        public OutputData outputData;


        public void Init(OutputData data)
        {
            ioData = data;
            strDriverName = data.OutputCardName;
            try
            {
                if (HardwareManage.hardwardDictionary[ioData.OutputCardName] is IOutputAction)
                {

                    actionOutput = (IOutputAction)HardwareManage.hardwardDictionary[ioData.OutputCardName];
                    iOutputNo = ioData.iOutputNo;
                    bIgnore = ioData.bignore;
                    strRemark = ioData.strRemark;
                    bready = true;

                }
            }
            catch
            {

            }
        }
        public bool SetOutBit(bool bOn)
        {
            if (ioData.bignore)
                return true;
            if (bready == false)
            {
                return false;
            }
            return actionOutput.SetOutBit(iOutputNo, bOn);
        }
        public bool GetOn()
        {
            if (ioData.bignore)
                return true;
            if (bready == false)
            {
                return true;
            }
            return actionOutput.GetOutBit(iOutputNo);
        }
        public bool GetOff()
        {
            if (ioData.bignore)
                return true;
            if (bready == false)
            {
                return true;
            }
            return !actionOutput.GetOutBit(iOutputNo);
        }
    }
}
