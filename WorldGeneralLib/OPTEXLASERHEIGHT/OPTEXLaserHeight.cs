using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace WorldGeneralLib.OPTEXLASERHEIGHT
{
    public class OPTEXLaserHeight
    {
        private SerialPort serialPort;
        public string m_sHeightValue = string.Empty;
        public double m_dHeight = 0.0;
        public Label LabelHeight;
        public object objLock = new object();
        Thread GetHeightThread;
        HiPerfTimer hptimer;

        public Form adhereForm;

        private char m_cStx = Convert.ToChar(0x02);
        private char m_cEnd = Convert.ToChar(0x03);

        public OPTEXLaserHeight()
        {
            hptimer = new HiPerfTimer();
        }

        public bool InitCD33(string strComName)
        {
            serialPort = new SerialPort();
            try
            {
                serialPort.PortName = strComName;
                serialPort.BaudRate = 19200;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.None;
                serialPort.Open();
                GetHeightThread = new Thread(GetHeight);
                GetHeightThread.IsBackground = true;
                GetHeightThread.Start();
            }
            catch
            {
                this.adhereForm.Invoke(
                    new Action(() =>
                        {
                            LabelHeight.Text = "Error";
                            LabelHeight.BackColor = Color.Red;
                        }
                    )
                );
                return false;
            }
            return true;
        }

        public void GetHeight()
        {
            while (true)
            {
                Thread.Sleep(100);
                if (ReadHeightValue(ref m_dHeight))
                {
                   this.adhereForm.Invoke(
                        new Action(() =>
                        {
                            LabelHeight.Text = m_dHeight.ToString();
                            LabelHeight.BackColor = Color.Green;
                        }
                        )
                    );            
                }
            }
        }

        public bool ReadHeightValue(ref double m_dHeight)
        {
            string strReceive = string.Empty;
            string[] strArray;
            char[] charArray = new char[1];
            string strSend = string.Empty;
            charArray[0] = ' ';
            lock (objLock)
            {
                try
                {
                    serialPort.ReadExisting();
                    strSend = m_cStx.ToString() + "MEASURE" + m_cEnd.ToString() + "\r\n";
                    serialPort.Write(strSend);
                    hptimer.Start();
                    strReceive = serialPort.ReadExisting();
                    while (true)
                    {
                        strReceive += serialPort.ReadExisting();
                        if (strReceive.IndexOf("\r\n") > -1)
                        {
                            strReceive = strReceive.Trim();
                            strArray = strReceive.Split(charArray, StringSplitOptions.RemoveEmptyEntries);
                            m_sHeightValue = strArray[1];
                            m_dHeight = double.Parse(m_sHeightValue);
                            return true;
                        }
                        if (hptimer.TimeUp(0.5))
                        {
                            this.adhereForm.Invoke(
                                new Action(() =>
                                {
                                    LabelHeight.Text = "Error";
                                    LabelHeight.BackColor = Color.Red;
                                }
                                )
                            );
                            return false;
                        }
                    }
                }
                catch
                {
                    this.adhereForm.Invoke(
                        new Action(() =>
                        {
                            LabelHeight.Text = "Error";
                            LabelHeight.BackColor = Color.Red;
                        }
                        )
                    );
                    return false;
                }
            }
        }

        public bool SendData(string data)
        {
            if (serialPort.IsOpen)
            {
                lock (objLock)
                {
                    serialPort.ReadExisting();
                    data = m_cStx.ToString() + data + m_cEnd.ToString() + "\r\n";
                    serialPort.Write(data);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
