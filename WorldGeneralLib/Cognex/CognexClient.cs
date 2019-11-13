using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Windows.Forms;

namespace WorldGeneralLib.Cognex
{
    public class CognexClient
    {
        public TcpClient tcpClient;
        public System.Threading.Thread threadConnect;
        NetworkStream networkStream;
        public Form adhereForm;
        public bool bConnectOk = false;
        object objSendLock = new object();

        string strLastRemain = string.Empty;
        string[] strCommandHeader = new string[20];
        bool[] bReceivedResult = new bool[20];
        bool[] bResult = new bool[20];
        double[] dPosX = new double[20];
        double[] dPosY = new double[20];
        double[] dAngle = new double[20];
        double[] dPosX1 = new double[20];
        double[] dPosY1 = new double[20];
        double[] dAngle1 = new double[20];

        public CognexClient()
        {
            for (int i = 0; i < strCommandHeader.Length; i++)
            {
                strCommandHeader[i] = "T" + i.ToString("00");
            }
        }

        public void StartConnect()
        {
            if (threadConnect == null)
                threadConnect = new System.Threading.Thread(FunctionConnect);
            if (threadConnect.IsAlive)
                return;
            threadConnect.IsBackground = true;
            threadConnect.Start();
        }

        public void FunctionConnect()
        {
            byte[] byteRec = new byte[1024];
            int iRecCount = 0;
            string strReturn;
            while (true)
            {
                try
                {
                    tcpClient = new TcpClient();
                    AddMessage("Connecting....");
                    tcpClient.Connect(CognexManage.m_cognexDoc.hostipaddress, CognexManage.m_cognexDoc.nhostport);
                    this.adhereForm.Invoke(
                        new Action(() =>
                            { CognexManage.m_cognexform.ConnIndicatePB.Image = CognexManage.m_cognexform.Green; }
                        )
                    );
                    AddMessage("Connect Ok");
                    networkStream = tcpClient.GetStream();
                    bConnectOk = true;

                    while (true)
                    {
                        iRecCount = networkStream.Read(byteRec, 0, byteRec.Length);
                        if (iRecCount == 0)
                        {
                            if (tcpClient != null)
                                tcpClient.Close();
                            bConnectOk = false;
                            break;
                        }
                        else
                        {
                            strReturn = Encoding.ASCII.GetString(byteRec, 0, iRecCount);
                            ProcessMessage(strReturn);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.adhereForm.Invoke(
                        new Action(() => 
                            { CognexManage.m_cognexform.ConnIndicatePB.Image = CognexManage.m_cognexform.Grey; }
                        )
                    );
                    AddMessage(ex.ToString());
                    if (tcpClient != null)
                        tcpClient.Close();
                }
            }
        }

        public void AddMessage(string strMessage)
        {
            Action action = () =>
                {
                    if (CognexManage.m_cognexform.textBoxCognex.Lines.Length > 100)
                        CognexManage.m_cognexform.textBoxCognex.Clear();
                    CognexManage.m_cognexform.textBoxCognex.AppendText(DateTime.Now.ToString("HH:mm:ss") + ":    " + strMessage + "\r\n");
                };
            this.adhereForm.Invoke(action);
        }

        public void ProcessMessage(string strMessage)
        {
            string strCommand;
            if (strMessage.IndexOf("\r\n") > -1)
            {
                strCommand = strLastRemain + strMessage.Substring(0, strMessage.IndexOf("\r\n"));
                AddMessage(strCommand);
                ProcessCommand(strCommand);
                strLastRemain = strMessage.Substring(strMessage.IndexOf("\r\n") + 2);
                if(strLastRemain.IndexOf("\r\n") > -1)
                    strLastRemain = strLastRemain.Replace("\r", string.Empty).Replace("\n", string.Empty);
            }
            else
            {
                strLastRemain = strMessage;
            }
        }

        public void ProcessCommand(string strCommand)
        {
            char[] charSplit = new char[1];
            charSplit[0] = ',';
            string[] strArrange = strCommand.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
            ProcessResult(strArrange); 
        }

        public void ProcessResult(string[] strArrange)
        {
            int i = int.Parse(strArrange[0].Substring(1));
            if (strArrange[1] == "PASS")
                bResult[i] = true;
            else
                bResult[i] = false;
            dPosX[i] = double.Parse(strArrange[2]);
            dPosY[i] = double.Parse(strArrange[3]);
            dAngle[i] = double.Parse(strArrange[4]);
            dPosX1[i] = double.Parse(strArrange[5]);
            dPosY1[i] = double.Parse(strArrange[6]);
            dAngle1[i] = double.Parse(strArrange[7]);
            bReceivedResult[i] = true;
        }

        public bool SendCommand(int i, string strSn = "")
        {
            bReceivedResult[i] = false;
            return SendMessage(strCommandHeader[i] + "," + strSn);
        }

        public bool SendMessage(string strMessage)
        {
            char[] charSplit = new char[1];
            charSplit[0] = ',';
            string[] strArrange = strMessage.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
            int i = int.Parse(strArrange[0].Substring(1));
            bReceivedResult[i] = false;

            try
            {
                strMessage = strMessage + "\r\n";
                byte[] byteSent = Encoding.ASCII.GetBytes(strMessage);
                if (tcpClient == null || !tcpClient.Connected || !bConnectOk)
                    return false;
                lock (objSendLock)
                {
                    networkStream.Write(byteSent, 0, byteSent.Length);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetImageResult(int i, ref bool bRes, ref double dx, ref double dy, ref double da, ref double dx1, ref double dy1, ref double da1)
        {
            try
            {
                if (!tcpClient.Connected)
                    return false;
                if (bReceivedResult[i])
                {
                    bRes = bResult[i];
                    dx = dPosX[i];
                    dy = dPosY[i];
                    da = dAngle[i];
                    dx1 = dPosX1[i];
                    dy1 = dPosY1[i];
                    da1 = dAngle1[i];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                bRes = false;
                dx = 0.0;
                dy = 0.0;
                da = 0.0;
                dx1 = 0.0;
                dy1 = 0.0;
                da1 = 0.0;
                return false;
            }
        }


    }
}
