/**
* 命名空间:  WorldGeneralLib.SerialPorts
* 功 能   ： N/A
* 类 名   ： Honeywell3320G
* Ver     :  ver1.0.0.0
* 变更日期:  2019-02-20 09:56:08
* 负责人  :  wuchenjie 
* 变更内容:
* Copyright (c) 2018 Sunwoda Corporation. All rights reserved.
*┌───────────────────────────────┐
*│此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露│
*│版权所有：欣旺达电气技术有限公司 　　　　　　　　　　　　　　 │
*└───────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WorldGeneralLib.SerialPorts
{
    public class Honeywell3320G : ISerialPort
    {
        System.IO.Ports.SerialPort serialPort;
        public string m_Barcode = "";
        public bool bInitOK = false;
        HiPerfTimer timeM;

        SerialPortData barcodeData;

        //public TextBox TextBoxCurrentBarCode;
        public object lockObj = new object();
        public bool bValueReady = false;
        private string strRemaid = "";
        private byte[] m_SendByte = new byte[11];

        public Queue<string> m_BarcodeQueue;
        public Honeywell3320G()
        {
            bInitOK = false;
            serialPort = new System.IO.Ports.SerialPort();
            m_BarcodeQueue = new Queue<string>();
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
                Thread readThread = new System.Threading.Thread(ReadThread);
                readThread.IsBackground = true;
                readThread.Start();
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

                    if (GetData(ref m_Barcode))
                    {
                        if (!m_BarcodeQueue.Contains(m_Barcode))
                        {
                            m_BarcodeQueue.Enqueue(m_Barcode);
                        }
                        bValueReady = true;
                    }
                }
                catch
                {

                }
            }
        }

        private bool GetData(ref string m_Barcode)
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
                                m_Barcode = strRecieve.Substring(0, strRecieve.IndexOf("\r"));
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
                        Thread.Sleep(1);
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
            byte[] buffer = new byte[17];
            charArray[0] = ',';
            lock (lockObj)
            {
                try
                {
                    serialPort.ReadTimeout = 100;
                    serialPort.ReadExisting();
                    serialPort.Write(m_SendByte, 0, m_SendByte.Length);
                    Thread.Sleep(100);
                    //string a= serialPort.ReadExisting();
                    serialPort.Read(buffer, 0, buffer.Length);
                    //byte[] recByte = Encoding.UTF8.GetBytes(rec); 
                    string sValue = Encoding.ASCII.GetString(buffer);
                    //string sValue = Convert.ToString(buffer[2], 16).PadLeft(2, '0') + Convert.ToString(buffer[3], 16).PadLeft(2, '0');
                    string[] strValue = sValue.Split(',');
                    data = Convert.ToDouble(strValue[3]);
                    //if (buffer[2] >= 225)
                    //    data = -(65536 - Convert.ToInt32(sValue, 16)) * 0.01;
                    //else
                    //    data = (Convert.ToInt32(sValue, 16)) * 0.01;

                    return true;
                }
                catch (Exception ex)
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
