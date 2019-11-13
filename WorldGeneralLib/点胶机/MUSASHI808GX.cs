using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace WorldGeneralLib
{
    public class MUSASHI808GX
    {
        private System.IO.Ports.SerialPort serialPort1;
        MUSASHIData m_data;
        TextBox txtBoxCurrentPressure;
        ToolStripStatusLabel toolStripStatus;
        public string strCurrentValue = "";
        public delegate void PressureChanged(object sender, bool bTimeOut,string strValue);
        public event PressureChanged PressureEvent;
        public MUSASHI808GX(TextBox txtBox, ToolStripStatusLabel toolStrip)
        {
            txtBoxCurrentPressure = txtBox;
            toolStripStatus = toolStrip;
            toolStripStatus.Click += toolStripStatus_Click;
            m_data = MUSASHIData.LoadObj(toolStrip.Text);
            Init();
        }

        void toolStripStatus_Click(object sender, EventArgs e)
        {
            FormMasashi frm = new FormMasashi(m_data);
            frm.ShowDialog();
        }
        public bool Init()
        {
            serialPort1=new System.IO.Ports.SerialPort();
            try
            {
                this.serialPort1.BaudRate = 19200;
                this.serialPort1.PortName = m_data.strPortName;
                serialPort1.Open();
                toolStripStatus.BackColor = Color.Green;
                Thread threadGetCurrent = new Thread(ThreadGetCurrentFunction);
                threadGetCurrent.IsBackground = true;
                threadGetCurrent.Start();
            }
            catch
            {
                toolStripStatus.BackColor = Color.Red;
                return false;
            }
            return true;
        }
        public void ThreadGetCurrentFunction()
        {
            string strValue="";
            while (true)
            {
                Thread.Sleep(500);
                if (GetCh0PresureValue(ref strValue))
                {
                    strCurrentValue = strValue;
                    PressureEvent(this, false, strValue);
                    UpdateCurrentData(false, strValue);
                }
                else
                {
                    PressureEvent(this, true, "999.999");
                    UpdateCurrentData(true, "999.999");
                }
                
            }
        }
        public string ShotStartStop()
        {
            string strForDisplay = "";
            DateTime dStart = DateTime.Now;
            TimeSpan t;
            char charENQ = Convert.ToChar(0x05);
            char charASK = Convert.ToChar(0x06);
            char charEOT = Convert.ToChar(0x04);
            char charSTX = Convert.ToChar(0x02);
            char charETX = Convert.ToChar(0x03);
            string strA0 = charSTX.ToString() + "02A02D" + charETX.ToString();
            string strA2 = charSTX.ToString() + "02A22B" + charETX.ToString();
            string strCAN = charSTX.ToString() + "0218186E" + charETX.ToString();
            string strSend = "";
            string strReturn = "";
            lock (serialPort1)
            {
                //发起通信
                strSend = charENQ.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接收响应
                while (strReturn.IndexOf(charASK) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发起通信没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";



                //发送指令
                strSend = charSTX.ToString() + "04DI  CF" + charETX.ToString() + charEOT.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接受相应
                while (strReturn.IndexOf(charETX) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发送指令没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";

                //发送结束信号
                if (strReturn.IndexOf(strA0) < 0)
                {
                    strSend = strCAN;
                    serialPort1.WriteLine(strSend);
                }
            }
            return strForDisplay;
        }
        public string GetRomVersion(ref string strRomVersion)
        {
            string strForDisplay = "";
            DateTime dStart = DateTime.Now;
            TimeSpan t;
            char charENQ = Convert.ToChar(0x05);
            char charASK = Convert.ToChar(0x06);
            char charEOT = Convert.ToChar(0x04);
            char charSTX = Convert.ToChar(0x02);
            char charETX = Convert.ToChar(0x03);
            string strA0 = charSTX.ToString() + "02A02D" + charETX.ToString();
            string strA2 = charSTX.ToString() + "02A22B" + charETX.ToString();
            string strCAN = charSTX.ToString() + "0218186E" + charETX.ToString();
            string strSend = "";
            string strReturn = "";

            lock (serialPort1)
            {
                //发起通信
                strSend = charENQ.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接收响应
                while (strReturn.IndexOf(charASK) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发起通信没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";



                //发送指令
                strSend = charSTX.ToString() + "05RM   9C" + charETX.ToString() + charASK.ToString() + charEOT.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接受相应
                while (strReturn.Length < 26)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发送指令没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";
                strRomVersion = strReturn.Substring(19, 6);
                //发送接收到信号
                if (strReturn.IndexOf(strA0) < 0)
                {
                    strSend = strCAN;
                    serialPort1.WriteLine(strSend);
                    return strForDisplay;
                }
            }
            return strForDisplay;
        }
        public bool GetCh0PresureValue(ref string strPresureValue)
        {
            string strForDisplay = "";
            DateTime dStart = DateTime.Now;
            TimeSpan t;
            char charENQ = Convert.ToChar(0x05);
            char charASK = Convert.ToChar(0x06);
            char charEOT = Convert.ToChar(0x04);
            char charSTX = Convert.ToChar(0x02);
            char charETX = Convert.ToChar(0x03);
            string strA0 = charSTX.ToString() + "02A02D" + charETX.ToString();
            string strA2 = charSTX.ToString() + "02A22B" + charETX.ToString();
            string strCAN = charSTX.ToString() + "0218186E" + charETX.ToString();
            string strSend = "";
            string strReturn = "";

            lock (serialPort1)
            {
                //发起通信
                strSend = charENQ.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接收响应
                while (strReturn.IndexOf(charASK) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发起通信没有回应！" + "\r\n";
                        return false;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";



                //发送指令
                GetStringCheckCode("05GC000");
                strSend = charSTX.ToString() + "05GC000" + GetStringCheckCode("05GC000") + charETX.ToString() + charASK.ToString() + charEOT.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接受相应
                int countFind = 0;
                int startFind = 0;
                strReturn = strReturn + serialPort1.ReadExisting();
                strReturn.IndexOf(charETX, startFind, countFind);
                while (strReturn.Length < 41)
                {
                    System.Threading.Thread.Sleep(100);
                    strReturn = strReturn + serialPort1.ReadExisting();
                    strReturn.IndexOf(charETX, startFind, countFind);
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发送指令没有回应！" + "\r\n";
                        return false;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";
                strPresureValue = strReturn.Substring(15, 4);
                strPresureValue = strPresureValue.Insert(3, ".");

                //发送接收到信号
                if (strReturn.IndexOf(strA0) < 0)
                {
                    strSend = strCAN;
                    serialPort1.WriteLine(strSend);
                    return false;
                }
            }
            return true;
        }
        public string GetStringCheckCode(string strInput)
        {
            string strTemp = "0";
            char charTemp = ' ';
            byte iTemp = 0;
            byte iResult = 0;
            for (int i = 0; i < strInput.Length; i++)
            {
                charTemp = strInput[i];
                iTemp = Convert.ToByte(charTemp);
                iResult = (byte)(iResult - iTemp);
            }
            strTemp = iResult.ToString("X2");
            return strTemp;
        }
        public string SetCh0PresureValue(string strPresureValue)
        {
            //验证数值有效性
            string strValueChange = "";
            int iTest = 0;
            float fTest = 0.0f;
            try
            {
                fTest = float.Parse(strPresureValue);
            }
            catch
            {
                return "Wrong Value";
            }
            
            fTest = fTest * 10;
            iTest = (int)fTest;
            strValueChange = iTest.ToString();
            strValueChange = strValueChange.PadLeft(4,'0');
            
            string strForDisplay = "";
            DateTime dStart = DateTime.Now;
            TimeSpan t;
            char charENQ = Convert.ToChar(0x05);
            char charASK = Convert.ToChar(0x06);
            char charEOT = Convert.ToChar(0x04);
            char charSTX = Convert.ToChar(0x02);
            char charETX = Convert.ToChar(0x03);
            string strA0 = charSTX.ToString() + "02A02D" + charETX.ToString();
            string strA2 = charSTX.ToString() + "02A22B" + charETX.ToString();
            string strCAN = charSTX.ToString() + "0218186E" + charETX.ToString();
            string strSend = "";
            string strReturn = "";

            lock (serialPort1)
            {
                //发起通信
                strSend = charENQ.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接收响应
                while (strReturn.IndexOf(charASK) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发起通信没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";



                //发送指令
                strSend = charSTX.ToString() + "0EPH  CH000P" + strValueChange + GetStringCheckCode("0EPH  CH000P" + strValueChange) + charETX.ToString() + charEOT.ToString();
                serialPort1.WriteLine(strSend);
                strForDisplay = strForDisplay + "Send:  " + strSend + "\r\n";

                //接受相应
                while (strReturn.IndexOf(charETX) < 0)
                {
                    strReturn = strReturn + serialPort1.ReadExisting();
                    t = DateTime.Now - dStart;
                    if (t.TotalMilliseconds > 500)
                    {
                        strForDisplay = strForDisplay + "发送指令没有回应！" + "\r\n";
                        return strForDisplay;
                    }
                }
                strForDisplay = strForDisplay + "Retu:  " + strReturn + "\r\n";

                //发送接收到信号
                if (strReturn.IndexOf(strA0) < 0)
                {
                    strSend = strCAN;
                    serialPort1.WriteLine(strSend);
                    return strForDisplay;
                }
            }
            return strForDisplay;  
        }
        private void UpdateCurrentData(bool bTimeOut, string strValue)
        {
            Action action = () =>
                {
                    try
                    {
                        if (bTimeOut)
                        {
                            toolStripStatus.BackColor = Color.Yellow;
                        }
                        else
                        {
                            toolStripStatus.BackColor = Color.Green;
                            if (txtBoxCurrentPressure.Focused==false)
                                txtBoxCurrentPressure.Text = strValue;
                        }
                    }
                    catch
                    {

                    }

                };
            try
            {
                txtBoxCurrentPressure.Invoke(action);
            }
            catch
            {
 
            }
        }
    }
}
