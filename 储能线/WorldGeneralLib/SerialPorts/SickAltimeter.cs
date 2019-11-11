using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WorldGeneralLib.DataLogicBarCode;

namespace WorldGeneralLib.SerialPorts
{
    public class SickAltimeter : ISerialPort
    {
        System.IO.Ports.SerialPort serialPort;
        public string m_hight = "";
        public bool bInitOK = false;
        HiPerfTimer timeM;

        SerialPortData barcodeData;

        //public TextBox TextBoxCurrentBarCode;
        public object lockObj = new object();
        public bool bValueReady = false;
        private string strRemaid = "";
        private byte[] m_SendByte = new byte[6];
        public SickAltimeter()
        {
            bInitOK = false;
            serialPort = new System.IO.Ports.SerialPort();

            m_SendByte[0] = 0x02;
            m_SendByte[1] = 0x43;
            m_SendByte[2] = 0xB0;
            m_SendByte[3] = 0x01;
            m_SendByte[4] = 0x03;
            m_SendByte[5] = 0xF2;
        }

        public bool  Init(SerialPortData data)
        {
            this.barcodeData = data;
            try
            {
                serialPort.PortName = barcodeData.PortName;
                serialPort.BaudRate = barcodeData.BaudRate;
                serialPort.DataBits = barcodeData.DataBit;
                serialPort.StopBits = barcodeData.StopBit;
                serialPort.Parity = barcodeData.Parity;
                serialPort.Open();
                bInitOK = true;
                //System.Threading.Thread readThread = new System.Threading.Thread(ReadThread);
                //readThread.IsBackground = true;
                //readThread.Start();
            }
            catch
            {
                bInitOK = false;
            }
            return bInitOK;
        }

        private void ReadThread()
        {
            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(1);
                    if (bInitOK == false)
                        continue;

                    if (GetData(ref m_hight))
                    {
                        bValueReady = true;
                    }
                }
                catch
                {

                }
            }
        }

        private bool GetData(ref string m_hight)
        {
            string strRecieve = "";
            char[] charArray = new char[1];
            charArray[0] = ',';
            lock (lockObj)
            {
                try
                {
                    timeM.Start();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRemaid + strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                m_hight = strRecieve.Substring(0, strRecieve.IndexOf("\r"));
                                strRecieve = strRecieve.Substring(strRecieve.IndexOf("\r") + 2);
                                strRemaid = strRecieve;
                                break;
                            }
                            if (timeM.TimeUp(1))
                            {
                                return false;
                            }
                        }
                        catch
                        {
                            return false;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool GetCurrentData(ref double m_hight)
        {
            char[] charArray = new char[1];
            charArray[0] = ',';
            lock (lockObj)
            {
                try
                {
                    try
                    {
                        serialPort.ReadExisting();
                        serialPort.Write(m_SendByte, 0, m_SendByte.Length);
                        Thread.Sleep(10);
                        string rec = serialPort.ReadExisting();
                        byte[] recByte = Encoding.Default.GetBytes(rec); ;
                        string sValue = Convert.ToString(recByte[2], 16).PadLeft(2, '0') + Convert.ToString(recByte[3], 16).PadLeft(2, '0');
                        m_hight = (Convert.ToInt32(sValue, 16)) * 0.01;
                    }
                    catch
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        void ISerialPort.GetData(ref string data)
        {
            
        }

        public bool GetData(ref double data)
        {
            char[] charArray = new char[1];
            byte[] buffer = new byte[6];
            charArray[0] = ',';
            lock (lockObj)
            {
                try
                {
                    serialPort.ReadTimeout = 100;
                    serialPort.ReadExisting();
                    serialPort.Write(m_SendByte, 0, m_SendByte.Length);
                    Thread.Sleep(10);
                    //string a= serialPort.ReadExisting();
                    serialPort.Read(buffer, 0, buffer.Length);
                    //byte[] recByte = Encoding.UTF8.GetBytes(rec); 
                    string sValue = Convert.ToString(buffer[2], 16).PadLeft(2, '0') + Convert.ToString(buffer[3], 16).PadLeft(2, '0');
                    if (buffer[2] >= 225)
                        data = -(65536 - Convert.ToInt32(sValue, 16)) * 0.01;
                    else
                        data = (Convert.ToInt32(sValue, 16)) * 0.01;

                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public void DisConnect()
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }
}
