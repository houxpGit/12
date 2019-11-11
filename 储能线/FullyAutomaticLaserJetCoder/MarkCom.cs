using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder
{
    class MarkCom
    {
        public static SerialPort MarkComPort = new SerialPort("COM2", 38400, Parity.None, 8, (StopBits)1);
        public static bool bMarkRW = false;
        public static string sMarkData = "";
        private static Thread ResistanceThread = new Thread(ReadWriteMark);
        public static string Recv = "";
        //public static byte bRecv;
        public static byte[] bytes = new byte[2] { 05, 98 };
        //初始化测阻仪
        private static bool InitScan()
        {
            try
            {
             //   MarkComPort.ReadTimeout = 2000;
                //MarkComPort.ReadBufferSize = 512;
              //  MarkComPort.WriteBufferSize = 512;
              //  MarkComPort.Open();
                return true;
            }
            catch (Exception ex)
            {
              //  System.Windows.Forms.MessageBox.Show("端口[" + MarkComPort.PortName + "]打开错误！\r\n" + ex.Message, "错误");
                return false;
            }
        }
        public static void SendCmdToMark(string sCmd)
        {
            try
            {
                byte[] t = new byte[] { 5 };
                MarkComPort.DiscardInBuffer();
                MarkComPort.Write(t, 0, 1);
                //AddMsg("发送5");
                Thread.Sleep(100);
                if (MarkComPort.BytesToRead > 0)
                {

                    byte[] rcv = new byte[MarkComPort.BytesToRead];
                    Thread.Sleep(100);
                    MarkComPort.Read(rcv, 0, MarkComPort.BytesToRead);
                    switch (rcv[0])
                    {
                        case 0x06:
                            // byte[] send = new byte[] { 0x98, 0x00, 0x02, 0x00, 0x01, 0x9B };
                            //AddMsg("收到6");
                            string msg = sCmd; //txtCommand.Text;

                            byte[] tSsend = Str2Byte(msg);
                            byte end = OrSum(tSsend);
                            byte[] send = new byte[tSsend.Length + 1];
                            Buffer.BlockCopy(tSsend, 0, send, 0, tSsend.Length);
                            Buffer.BlockCopy(new byte[] { end }, 0, send, tSsend.Length, 1);
                            MarkComPort.DiscardInBuffer();
                            MarkComPort.Write(send, 0, send.Length);
                            //AddMsg("发送数据" + Byte2Str(send));
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(100);
                if (MarkComPort.BytesToRead > 0)
                {

                    byte[] rcv1 = new byte[MarkComPort.BytesToRead];
                    Thread.Sleep(20);
                    MarkComPort.Read(rcv1, 0, MarkComPort.BytesToRead);
                    //AddMsg("收到数据" + Byte2Str(rcv1));
                    switch (rcv1[0])
                    {
                        case 0x06:
                            int tt = 0;
                            //byte[] send = new byte[] { 0x98, 0x00, 0x02, 0x00, 0x01, 0x9B };
                            //sp.Write(send, 0, send.Length);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private static byte OrSum(byte[] data)
        {
            byte rst = 0;
            foreach (var item in data)
            {
                rst ^= item;
            }
            return rst;
        }

        private static byte[] Str2Byte(string msg)
        {
            string tMsg = msg.Replace(" ", "");
            tMsg = tMsg.Replace(",", "");
            byte[] rst = new byte[tMsg.Length / 2];
            for (int i = 0; i < tMsg.Length; i = i + 2)
            {
                rst[i / 2] = Convert.ToByte(tMsg.Substring(i, 2), 16);
            }
            return rst;
        }

        private static string Byte2Str(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item.ToString("x"));
                sb.Append(" ");
            }
            return sb.ToString();
        }

        public static void SendCmdToMark(byte[] bytes)
        {
            try
            {
                
                //string sTemp = MarkComPort.ReadExisting();
                //int i = MarkComPort.Read(bRecv,0,4);
                //Recv = MarkComPort.ReadExisting();
                //bytes[0] = Convert.ToByte(sCmd);// Byte.Parse(sCmd);
                //bytes[1] = Convert.ToByte("98");

                MarkComPort.Write(bytes, 0, bytes.Length);
               
                MarkComPort.DataReceived += MarkComPort_DataReceived;
                //Recv = MarkComPort.ReadByte().ToString();
                //MarkComPort.DataReceived +
                //Thread.Sleep(1);
                Recv = MarkComPort.ReadByte().ToString();
                //MarkComPort.Write("|");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private static void MarkComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] RecvBytes = null;
            MarkComPort.Read(RecvBytes, 0, 1);
        }

        //字符串转换成十六进制字符
        public static string StrToHex(string str)
        {
            string strResult;
            byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(str);
            strResult = "";
            foreach (byte b in buffer)
            {
                strResult += b.ToString("X2");//X是16进制大写格式 
            }
            return strResult;
        }
        //执行函数
        public static void MarkReadWrite()
        {
            bMarkRW = InitScan();
            if (bMarkRW)
                ResistanceThread.Start();
        }
        private static void ReadWriteMark()
        {
            string[] std = new string[] { };
            byte[] bRead = new byte[512];
            string sRead = "";
            while (bMarkRW)
            {
                try
                {
                    if (MarkComPort.BytesToRead > 4) //&& bScan1Read)
                    {
                        Thread.Sleep(20);
                        sRead = MarkComPort.ReadTo("\r");
                        Global.logger.Info("收到喷码机返回->" + sRead.Trim());
                        sMarkData = sRead;
                        MarkComPort.ReadExisting();
                    }
                    Thread.Sleep(30);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(" Mark->No read" + "\r");
                    Global.logger.Info("ReadWrite Mark->" + ex.Message);
                }
                Thread.Sleep(5);
            }
        }
    }
}
