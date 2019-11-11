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
namespace FullyAutomaticLaserJetCoder
{
    public partial class TestDateCom
    {
        public SerialPort COMM = new SerialPort();
        public bool flag = false;     
        public bool Open(string com,int BaudRate, string Parity1,int DataBits,string StopBits1)
        {
            lock (lock2)
            {
                try
                {
                    if (COMM.IsOpen)
                    {
                        flag = true;
                        return flag;
                    }
                    if (Parity1 == "1")
                    {
                        COMM.Parity = Parity.Even;//偶效验
                    }
                    else if (Parity1 == "2")
                    {
                        COMM.Parity = Parity.None;//偶效验
                    }
                    else if (Parity1 == "3")
                    {
                        COMM.Parity = Parity.Odd;//偶效验
                    }
                    COMM.ReadBufferSize = 1024;
                    COMM.PortName = com;
                    COMM.BaudRate = BaudRate;//波特率9600;      
                    COMM.DataBits = DataBits;//数据位8
                    if (StopBits1 == "1")
                    {
                        COMM.StopBits = StopBits.One;//停止位
                    }
                    else if (StopBits1 == "2")
                    {
                        COMM.StopBits = StopBits.Two;//停止位
                    }
                    COMM.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                    COMM.Open();
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
        public void CloseEd()
        {
            if (COMM.IsOpen)
            {
                COMM.Close();
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
                if (COMM.IsOpen && COMM.BytesToRead > 0)
                {
                    int n = COMM.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致   ；
                    byte[] buf = new byte[n];//声明一个临时字节数组存储当前来的串口数据   
                    received_count += n;//增加接收计数                         
                    COMM.Read(buf, 0, n);//读取缓冲数据 
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
        object lock1=new object(), lock2 = new object();
        public string  senddatetotest(string str)
        {
            lock (lock1)
            {
        
                string date = "";
                if (COMM.IsOpen)
                {
                    try
                    {
                        COMM.Write(str + "\r\n");
                        Thread.Sleep(10);
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

    }
}
