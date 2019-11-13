using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.SerialPorts
{
    public class CommonSerialPort : ISerialPort
    {
        System.IO.Ports.SerialPort serialPort;
        public string m_strCurrentBarCode = "";
        public bool bInitOK = false;
        HiPerfTimer timeM;

        SerialPortData serialPortData;

        //public TextBox TextBoxCurrentBarCode;
        public object lockObj = new object();
        public object lockObj2 = new object();
        private string strRemaid = "";

        public CommonSerialPort()
        {
            // this.TextBoxCurrentBarCode = TextBoxCurrentBarCode;
            timeM = new HiPerfTimer();
            serialPort = new System.IO.Ports.SerialPort();
            bInitOK = false;
        }

        public bool  Init(SerialPortData data)
        {
            return true;
        }

        public void GetData(ref string data)
        {
            throw new NotImplementedException();
        }

        public void GetData(ref double data)
        {
            throw new NotImplementedException();
        }

        bool ISerialPort.GetData(ref double data)
        {
            throw new NotImplementedException();
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }
    }
}
