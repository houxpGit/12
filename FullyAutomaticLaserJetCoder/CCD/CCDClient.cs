/**
* 命名空间:  FullyAutomaticLaserJetCoder.CCD
* 功 能   ： N/A
* 类 名   ： CCDClient
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-18 13:40:27
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
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder.CCD
{
    public enum OrderType
    {
        A,
        B,
        C
    }
    public class CCDClient
    {
        private bool bConnectOk;
        private TcpClient tcpClient;
        private CCDCommunication ccdCommunication;
        private NetworkStream stream;
        public TextBox textBox;
        public string strRecieve;
        private OrderType orderType;
        private EventWaitHandle wh = new AutoResetEvent(false);
        public delegate void C1TaskDelegate();
        public event C1TaskDelegate C1TaskEvent;

        public delegate void C2TaskDelegate();
        public event C2TaskDelegate C2TaskEvent;

        public delegate void C3TaskDelegate();
        public event C3TaskDelegate C3TaskEvent;
        public CCDClient(CCDCommunication ccdCommunication)
        {
            this.ccdCommunication = ccdCommunication;
            Init();
        }

        public CCDClient(CCDCommunication ccdCommunication, TextBox textBox)
        {
            this.ccdCommunication = ccdCommunication;
            this.textBox = textBox;
            Init();
        }

        private void Init()
        {
            Thread thread = new Thread(KeepingConnect);
            thread.IsBackground = true;
            thread.Start();
        }

        private void KeepingConnect()
        {
            byte[] charRec = new byte[1024];
            while (true)
            {
                try
                {
                    if (bConnectOk == false)
                    {
                        tcpClient = new TcpClient();
                        UpdateConnectStatus(false);
                        tcpClient.Connect(ccdCommunication.IP, ccdCommunication.Port);
                        tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 3000, 10), null);
                        stream = tcpClient.GetStream();
                        UpdateConnectStatus(true);
                        bConnectOk = true;
                        while (true)
                        {
                            
                            int i = stream.Read(charRec, 0, 1024);
                            if (i == 0)
                            {
                                tcpClient.Close();
                                bConnectOk = false;
                                break;
                            }
                            else
                            {
                                strRecieve = Encoding.ASCII.GetString(charRec, 0, i);
                                wh.Set();
                                //C1
                                if (strRecieve=="C1\r\n")
                                    C1TaskEvent?.Invoke();
                                if (strRecieve == "C2\r\n")
                                    C2TaskEvent?.Invoke();
                                if (strRecieve == "C3\r\n")
                                    C3TaskEvent?.Invoke();
                                AddMessage(strRecieve);
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tcpClient?.Close();
                    bConnectOk = false;
                }
                Thread.Sleep(1);
            }
        }

        private void AddMessage(string message)
        {
            Action action = () =>
            {
                textBox?.AppendText("接收：" + message + "\r\n");
            };
            textBox?.Invoke(action);
        }

        public string Send(string message)
        {
            try
            {
                strRecieve = "";
                byte[] charRec = new byte[1024];
                //stream.Flush();
                byte[] sendBuffer = Encoding.Default.GetBytes(message);
                if (!bConnectOk || tcpClient == null)
                    return "";
                
                stream.Write(sendBuffer, 0, sendBuffer.Length);
                while (true)
                {
                    Thread.Sleep(10);
                    wh.Reset();
                    if (!wh.WaitOne(3000))
                    {
                        return "";
                    }
                    return strRecieve;
                   // return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public void Send(OrderType orderType)
        {
            try
            {
                this.orderType = orderType;
                byte[] sendBuffer = Encoding.Default.GetBytes(orderType.ToString());
                if (!bConnectOk || tcpClient == null)
                    return;
                stream.Write(sendBuffer, 0, sendBuffer.Length);
            }
            catch (Exception ex)
            {

            }
        }

        private byte[] KeepAlive(int onOff, int keepAliveTime, int keepAliveInterval)
        {
            byte[] buffer = new byte[12];
            BitConverter.GetBytes(onOff).CopyTo(buffer, 0);
            BitConverter.GetBytes(keepAliveTime).CopyTo(buffer, 4);
            BitConverter.GetBytes(keepAliveInterval).CopyTo(buffer, 8);
            return buffer;
        }

        private void UpdateConnectStatus(bool bStatus)
        {

        }
    }
}
