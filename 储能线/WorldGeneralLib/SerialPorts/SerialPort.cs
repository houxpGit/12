/**
* 命名空间:  FullyAutomaticLaserJetCoder
* 功 能   ： N/A
* 类 名   ： TestDateCom
* Ver     :  ver1.0.0.0
* 变更日期:  2019-05-16 09:29:10
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
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
namespace WorldGeneralLib.SerialPorts
{
    public  class SerialPort_date : ISerialPort
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
        public SerialPort_date()
        {
            bInitOK = false;
            serialPort = new System.IO.Ports.SerialPort();
            m_BarcodeQueue = new Queue<string>();
        }
       // public SerialPort SerialPort_ = new SerialPort();
        public bool flag = false;     
        public bool Open(string com,int BaudRate, string Parity1,int DataBits,string StopBits1)
        {
            lock (lock2)
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        flag = true;
                        return flag;
                    }
                    if (Parity1 == "1")
                    {
                        serialPort.Parity = Parity.Even;//偶效验
                    }
                    else if (Parity1 == "2")
                    {
                        serialPort.Parity = Parity.None;//偶效验
                    }
                    else if (Parity1 == "3")
                    {
                        serialPort.Parity = Parity.Odd;//偶效验
                    }
                    serialPort.ReadBufferSize = 1024;
                    serialPort.PortName = com;
                    serialPort.BaudRate = BaudRate;//波特率9600;      
                    serialPort.DataBits = DataBits;//数据位8
                    if (StopBits1 == "1")
                    {
                        serialPort.StopBits = StopBits.One;//停止位
                    }
                    else if (StopBits1 == "2")
                    {
                        serialPort.StopBits = StopBits.Two;//停止位
                    }
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                    serialPort.Open();
                    flag = true;
                    return flag;
                }
                catch (Exception)
                {
                 //   MessageBox.Show("请确认连接端口 " + com + " 失败或已连接");
                    flag = false;
                    return flag;
                }
            }
        }
        public bool Open_1()
        {
            return true;
            //lock (lock2)
            //{
            //    try
            //    {
            //        if (SerialPort_.IsOpen)
            //        {
            //            flag = true;
            //            return flag;
            //        }
            //        if (Parity1 == "1")
            //        {
            //            SerialPort_.Parity = Parity.Even;//偶效验
            //        }
            //        else if (Parity1 == "2")
            //        {
            //            SerialPort_.Parity = Parity.None;//偶效验
            //        }
            //        else if (Parity1 == "3")
            //        {
            //            SerialPort_.Parity = Parity.Odd;//偶效验
            //        }
            //        SerialPort_.ReadBufferSize = 1024;
            //        SerialPort_.PortName = com;
            //        SerialPort_.BaudRate = BaudRate;//波特率9600;      
            //        SerialPort_.DataBits = DataBits;//数据位8
            //        if (StopBits1 == "1")
            //        {
            //            SerialPort_.StopBits = StopBits.One;//停止位
            //        }
            //        else if (StopBits1 == "2")
            //        {
            //            SerialPort_.StopBits = StopBits.Two;//停止位
            //        }
            //        SerialPort_.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            //        SerialPort_.Open();
            //        flag = true;
            //        return flag;
            //    }
            //    catch (Exception)
            //    {
            //        //   MessageBox.Show("请确认连接端口 " + com + " 失败或已连接");
            //        flag = false;
            //        return flag;
            //    }
            //}
        }
        public void CloseEd()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
         int  received_count=0;
        public  string DataReceivedstr = "";
        public string DataReceivedstr_STR = "";

        object NM = new object();
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {        
            lock (NM)
            {    
            try
            {        
                Thread.Sleep(100);
                if (serialPort.IsOpen && serialPort.BytesToRead > 0)
                {
                    int n = serialPort.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致   ；
                    byte[] buf = new byte[n];//声明一个临时字节数组存储当前来的串口数据   
                    received_count += n;//增加接收计数                         
                        serialPort.Read(buf, 0, n);//读取缓冲数据 
               //   string  eee=      COMM.ReadLine();
                    for (int i = 0; i < buf.Length; i++)
                    {
                            DataReceivedstr_STR = System.Text.Encoding.Default.GetString(buf);
                            DataReceivedstr = System.Text.Encoding.Default.GetString(buf);
                    }
                }
            }
            catch (Exception )
            {          
            }
                string dd = DataReceivedstr;
            }     
        }

        private string  DataReceived_Date()
        {
            string Received_Date = "";
            lock (NM)
            {
                try
                {
                    Thread.Sleep(100);
                    if (serialPort.IsOpen && serialPort.BytesToRead > 0)
                    {
                        int n = serialPort.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致   ；
                        byte[] buf = new byte[n];//声明一个临时字节数组存储当前来的串口数据   
                        received_count += n;//增加接收计数                         
                        serialPort.Read(buf, 0, n);//读取缓冲数据 
                                                   //   string  eee=      COMM.ReadLine();
                        for (int i = 0; i < buf.Length; i++)
                        {
                            DataReceivedstr_STR = System.Text.Encoding.Default.GetString(buf);
                            Received_Date = System.Text.Encoding.Default.GetString(buf);
                        }
                    }
                }
                catch (Exception)
                {
                }
               // string Received_Date = DataReceivedstr;
            }
            return Received_Date;
        }
        object lock1=new object(), lock2 = new object();
        public string  senddatetotest(string str)
        {
            lock (lock1)
            {
              //  Init(serialPort);
                string date = "";
                if (serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Write(str + "\r\n");
                        Thread.Sleep(500);
                        date= DataReceived_Date();
                    }
                    catch (Exception)
                    {
                        return date;
                    }
                }
                else
                {
                    return date;
                }
                return date;
            }
        }

    

        private string HToTen(string str)
        {
            string str1 = "";
            string str2 = "";
            string s = "";
            if (str.Length == 4)
            {
                str1 = str.Substring(0, 2);
                str2 = str.Substring(2, 2);
                str = str2 + str1;
                double b1 = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);
                s = (b1 / 100).ToString();
            }
            return s;
        }

        public bool Init(SerialPortData data)
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

        public void DisConnect()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void GetData(ref string data)
        {
            data = senddatetotest(data);                  
        }

        public bool GetData(ref double data)
        {
            throw new NotImplementedException();
        }
    }
}
