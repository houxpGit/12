using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib;

namespace WorldGeneralLib.DataLogicBarCode
{
    public class Datalogic_Scanner
    {
        System.IO.Ports.SerialPort serialPort;
        public string m_strCurrentBarCode = "";
        public bool bInitOK = false;
        HiPerfTimer timeM;

        BarcodeData barcodeData;

        //public TextBox TextBoxCurrentBarCode;
        public object lockObj = new object();
        public object lockObj2 = new object();
        public bool bCodeReady = false;
        public Action RecieveMessageAction;
        private string strRemaid = "";
        public Queue<string> m_BarcodeQueue;

        //string strFilePath = "D:\\dropBox\\";
        //string strFileStart1 = "Start1.txt";
        //string strFileStart2 = "Start2.txt";
        //string strFileDone1 = "Done1.txt";
        //string strFileDone2 = "Done2.txt";
        //string strSn1 = "";
        //string strSn2 = "";

        public Datalogic_Scanner()
        {
            m_BarcodeQueue = new Queue<string>();
           // this.TextBoxCurrentBarCode = TextBoxCurrentBarCode;
            timeM = new HiPerfTimer();
            serialPort = new System.IO.Ports.SerialPort();
            bInitOK = false;
        }
        public bool InitReader(BarcodeData barcodeData)
        {
            this.barcodeData =barcodeData;
            try
            {
                serialPort.PortName = barcodeData.PortName;
                serialPort.BaudRate = barcodeData.BaudRate;
                serialPort.DataBits = barcodeData.DataBit;
                serialPort.StopBits = barcodeData.StopBit;
                serialPort.Parity = barcodeData.Parity;
                serialPort.Open();
                bInitOK = true;
                //TextBoxCurrentBarCode.BackColor = Color.White;
                System.Threading.Thread readThread = new System.Threading.Thread(ReadThread);
                readThread.IsBackground = true;
                readThread.Start();
            }
            catch
            {
                bInitOK = false;
                //TextBoxCurrentBarCode.BackColor = Color.Red;
                return false;
            }
            return true;
        }

        public void Clear()
        {
            serialPort.ReadExisting();
        }

        public void Close()
        {
            serialPort.Close();
            //serialPort.Dispose();
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

                    if (Get2DSN(ref m_strCurrentBarCode)&& !string.IsNullOrWhiteSpace(m_strCurrentBarCode))
                    {
                        if (!m_BarcodeQueue.Contains(m_strCurrentBarCode))
                        {
                            m_BarcodeQueue.Enqueue(m_strCurrentBarCode);
                        }                        
                        //TextBoxCurrentBarCode.Text = m_strCurrentBarCode;
                        //bCodeReady = true;
                    }      
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 获取条码
        /// </summary>
        /// <param name="m_strCurrentBarCode">条码</param>
        /// <returns></returns>
        private bool Get1DSN(ref string m_strCurrentBarCode)
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
                            if (bCodeReady)
                            {
                                strRecieve = strRemaid + strRecieve + serialPort.ReadExisting();
                                if (strRecieve.IndexOf("\r") > -1)
                                {
                                    //strRecieve = strRecieve.Trim();
                                    m_strCurrentBarCode = strRecieve.Substring(0, strRecieve.IndexOf("\r"));
                                    strRecieve = strRecieve.Substring(strRecieve.IndexOf("\r") + 2);
                                    strRemaid = strRecieve;
                                    RecieveMessageAction?.Invoke();
                                    break;
                                }
                                if (timeM.TimeUp(0.2))
                                {
                                    return false;
                                }
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
        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="m_strCurrentBarCode">二维码</param>
        /// <returns></returns>
        private bool Get2DSN(ref string m_strCurrentBarCode)
        {
            string strRecieve = "";
            char[] charArray = new char[1];
            byte[] buffer = new byte[1024];
            string[] spilt = new string[] { "\r"};
            charArray[0] = ',';
            string[] rec;
            lock (lockObj)
            {
                try
                {
                    timeM.Start();
                    while (true)
                    {
                        try
                        {
                            if (bCodeReady)
                            {
                                strRecieve = serialPort.ReadTo("\r");
                                if (strRecieve!="")
                                {
                                    m_strCurrentBarCode = "";
                                    rec = strRecieve.Split(spilt, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var item in rec)
                                    {
                                        m_strCurrentBarCode += item;
                                    }
                                    //strRecieve = strRecieve.Trim();
                                    bCodeReady = false;
                                    Console.WriteLine(m_strCurrentBarCode);
                                }
                                
                                break;
                                if (timeM.TimeUp(0.2))
                                {
                                    return false;
                                }
                            }
                        }
                        catch(Exception e)
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
 

        public void CloseEd()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
        int received_count = 0;
        public string DataReceivedstr = "";
        public string DataReceivedstr_STR = "";

        object NM = new object();
      

        private string DataReceived_Date()
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
        object lock1 = new object(), lock2 = new object();
        public string senddatetotest(string str)
        {
            lock (lock1)
            {

                string date = "";
                if (serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Write(str + "\r\n");
                        Thread.Sleep(500);
                        date = DataReceived_Date();
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
    }
}
