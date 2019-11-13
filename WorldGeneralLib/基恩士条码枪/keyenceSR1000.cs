using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace WorldGeneralLib
{
     public enum TcpIpType
    {
        Server,
        Client
    };
     public enum SendResult
     {
         Sucess,
         DisConnect,
         Error,
         Timeout
     };
    public class keyenceSR1000
    {
        private object objLock;
       
        private System.Threading.Thread threadConnect;
        private TcpListener tcpServer;
        private TcpClient tcpClient;
        private NetworkStream stream;
        public bool bConnectOk = false;
        public bool bContinueMode = false;
        private ManualResetEvent mSendEvent;
        private string strBarCodeCurrent = "";
        public TextBox textBoxBarCode;
        private bool bCodeReady = false;
        ToolStripStatusLabel toolStripBarCode;
        private keyenceSRData keyenceData;
        public keyenceSR1000(TextBox txtBox,ToolStripStatusLabel toolStripCode)
        {
            textBoxBarCode = txtBox;
            toolStripBarCode = toolStripCode;
            objLock = new object();
            mSendEvent = new ManualResetEvent(false);
            keyenceData = keyenceSRData.LoadObj(toolStripCode.Text);
            bContinueMode = keyenceData.连续读取;
            toolStripCode.Click += toolStripCode_Click;
            Init();
        }

        void toolStripCode_Click(object sender, EventArgs e)
        {
            FormKeyenceBarCode frm = new FormKeyenceBarCode(keyenceData);
            frm.ShowDialog();
        }
        protected void Init()
        {
            if (keyenceData.TcpIpType== TcpIpType.Server)
            {
                threadConnect = new System.Threading.Thread(threadConnectServer);
                
            }
            else
            {
                threadConnect = new System.Threading.Thread(threadConnectClient);
            }
            threadConnect.IsBackground = true;
            threadConnect.Start();
        }
        private void threadConnectServer()
        {

            byte[] charRec = new byte[1024];
            string strRec;
            IPAddress ipAdd = IPAddress.Parse(keyenceData.RobotRomoteIp);
            while (true)
            {
                try
                {
                    if (bConnectOk == false)
                    {
                        tcpServer = new TcpListener(ipAdd,keyenceData.RobotRomotePort);
                        tcpServer.Start();
                        UpdateConnectStatus(false);
                        tcpClient = tcpServer.AcceptTcpClient();
                        stream = tcpClient.GetStream();
                        UpdateConnectStatus(true);
                        bConnectOk = true;
                        if (keyenceData.连续读取)
                        {
                            ContinueTriger();
                        }
                        while (true)
                        {
                            int i = stream.Read(charRec, 0, 1024); ;
                            if (i == 0)
                            {
                                bConnectOk = false;
                                if (tcpClient != null)
                                    tcpClient.Close();
                                if (tcpServer != null)
                                    tcpServer.Stop();
                                break;
                            }
                            else
                            {
                                strRec = System.Text.Encoding.ASCII.GetString(charRec, 0, i);
                                ProcessMessage(strRec);
                            }
                        }
                    }
                }
                catch
                {
                    bConnectOk = false;
                    if (tcpClient != null)
                        tcpClient.Close();
                    if (tcpServer != null)
                        tcpServer.Stop();                 
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void threadConnectClient()
        {
            byte[] charRec = new byte[1024];
            string strRec;
            while (true)
            {
                try
                {
                    if (bConnectOk == false)
                    {
                        tcpClient = new TcpClient();
                        UpdateConnectStatus(false);
                        tcpClient.Connect(keyenceData.RobotRomoteIp, keyenceData.RobotRomotePort);
                        stream = tcpClient.GetStream();
                        UpdateConnectStatus(true);
                        bConnectOk = true;
                        if (keyenceData.连续读取)
                        {
                            ContinueTriger();
                        }
                        while (true)
                        {
                            int i = stream.Read(charRec, 0, 1024); ;
                            if (i == 0)
                            {
                                tcpClient.Close();
                                bConnectOk = false;
                                break;
                            }
                            else
                            {
                                strRec = System.Text.Encoding.ASCII.GetString(charRec, 0, i);
                                ProcessMessage(strRec);
                            }
                        }
                    }
                }
                catch
                {
                    tcpClient.Close();
                    bConnectOk = false;

                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        public virtual void ProcessMessage(string strMessage)
        {
            strBarCodeCurrent = "";
            if (textBoxBarCode != null)
            {
                Action action = () =>
                    {
                        if (keyenceData.连续读取)
                        {
                            char[] charSplit = new char[] { ',', '\\', '\"', '\r', '\n', ':', ' '};
                            string[] strArrange = strMessage.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
                            if (strArrange.Length > 3)
                            {
                                for (int i = 0; i < strArrange.Length - 3;i++ )
                                    strBarCodeCurrent = strBarCodeCurrent+strArrange[i]+":";
                                strBarCodeCurrent = strBarCodeCurrent.Substring(0, strBarCodeCurrent.Length - 1);
                                textBoxBarCode.Text = strBarCodeCurrent;
                                bCodeReady = true;
                            }
                        }
                        else
                        {
                            strBarCodeCurrent = strMessage;
                            textBoxBarCode.Text = strBarCodeCurrent;
                            bCodeReady = true;
                        }
                    };
               textBoxBarCode.Invoke(action);
            }
            mSendEvent.Set();
        }
        public SendResult SendMessage(string strCommand)
        {
            
            if (bConnectOk == false)
            {
                return SendResult.DisConnect;
            }
            byte[] charRec = new byte[1024];
            try
            {
                lock(objLock)
                {
                    
                    mSendEvent.Reset();
                    Byte[] outbytes = System.Text.Encoding.ASCII.GetBytes(strCommand.ToCharArray());
                    stream.Write(outbytes, 0, outbytes.Length);
                    if (mSendEvent.WaitOne(3000))
                    {
                            
                    }
                    else
                    {
                        mSendEvent.Set();
                        return SendResult.Timeout;
                    }
                }
            }
            catch
            {
                stream.Close();
                bConnectOk = false;
                return SendResult.Error;
            }
            return SendResult.Sucess;
        }
        public SendResult PostMessage(string strCommand)
        {

            if (bConnectOk == false)
            {
                return SendResult.DisConnect;
            }
            byte[] charRec = new byte[1024];
            try
            {
                lock (objLock)
                {
                    mSendEvent.Reset();
                    Byte[] outbytes = System.Text.Encoding.ASCII.GetBytes(strCommand.ToCharArray());
                    stream.Write(outbytes, 0, outbytes.Length);
                }
            }
            catch
            {
                stream.Close();
                bConnectOk = false;
                return SendResult.Error;
            }
            return SendResult.Sucess;
        }
        public SendResult GetSignal()
        {
           
            bCodeReady = false;
            return PostMessage("LON" + "\r");
        }
        public SendResult StopSignal()
        {
            return PostMessage("LOF"+"\r");
        }
        public SendResult ContinueTriger()
        {
            bContinueMode = true;
            bCodeReady = false;
            return PostMessage("TEST1" + "\r");
        }
        public SendResult StopContinueTriger()
        {
            return PostMessage("QUIT" + "\r");
        }
        public bool GetBarCodeReady(ref string strBarCode)
        {

            if (bConnectOk == false)
            {
                return false;
            }
            if (bCodeReady)
            {
                strBarCode=strBarCodeCurrent;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ResetBarReady()
        {
            bCodeReady = false;
        }
        private void UpdateConnectStatus(bool bConnectOK)
        {
            Action action = () =>
                {
                    if (bConnectOK)
                    {
                        toolStripBarCode.BackColor = Color.Green;
                    }
                    else
                    {
                        toolStripBarCode.BackColor = Color.Yellow;
                    }
                };
            toolStripBarCode.GetCurrentParent().Invoke(action);
        }
    }
}
