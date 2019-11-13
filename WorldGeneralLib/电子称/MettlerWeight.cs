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
    public class MettlerWeight
    {
        private System.IO.Ports.SerialPort serialPort1;
        MettlerData m_data;
        TextBox txtBoxCurrentPressure;
        ToolStripStatusLabel toolStripStatus;
        public string strCurrentValue = "";
        bool bOpen = false;
        public delegate void WeightChanged(object sender, bool bTimeOut, bool bStable,string strValue);
        public event WeightChanged WeightChangedEvent;
        public MettlerWeight(TextBox txtBox, ToolStripStatusLabel toolStrip)
        {
            txtBoxCurrentPressure = txtBox;
            toolStripStatus = toolStrip;
            toolStripStatus.Click += toolStripStatus_Click;
            m_data = MettlerData.LoadObj(toolStrip.Text);
            Init();
        }
        void toolStripStatus_Click(object sender, EventArgs e)
        {
            FormMettler frm = new FormMettler(m_data);
            frm.ShowDialog();
        }
        public bool Init()
        {
            serialPort1=new System.IO.Ports.SerialPort();
            try
            {
                this.serialPort1.BaudRate = 9600;
                this.serialPort1.PortName = m_data.strPortName;
                serialPort1.Open();
                bOpen = true;
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
            bool bStable = false;
            string strValue="";
            while (true)
            {
                Thread.Sleep(500);
                if (GetWeight(ref bStable,ref strValue))
                {
                    strCurrentValue = strValue;
                    WeightChangedEvent(this, false, bStable, strValue);
                    UpdateCurrentData(false, bStable,strValue);
                }
                else
                {
                    WeightChangedEvent(this, true, bStable, strValue);
                    UpdateCurrentData(true,bStable,"999.999");
                }
            }
        }
        private bool GetWeight(ref bool bStable,ref string strValue)
        {
            string strReturn = "";
            while (true)
            {
                try
                {
                    lock (this)
                    {
                        serialPort1.WriteLine("SI");
                        DateTime dateTimeStart = DateTime.Now;
                        TimeSpan timeSpan = new TimeSpan();
                        strReturn = serialPort1.ReadExisting();
                        while (strReturn.IndexOf("\r\n") < 0)
                        {
                            System.Threading.Thread.Sleep(0);
                            strReturn = strReturn + serialPort1.ReadExisting();
                            timeSpan = DateTime.Now - dateTimeStart;
                            if (timeSpan.TotalMilliseconds > 1000)
                                return false;
                        }
                    }
                    if (strReturn.Length > 10)
                    {
                        char[] charSplit = new char[] { ',', '\\', '\"', '\r', '\n', ':', ' ' };
                        string[] strArrange = strReturn.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
                        if (strArrange.Length > 3)
                        {
                            if (strArrange[1] == "S")
                            {
                                bStable = true;
                            }
                            else
                            {
                                bStable = false;
                            }
                            strValue = strArrange[2];
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {

                }
                return false;
            }
        }
        public string ClearWeightToZero()
        {
            string strResult;
            lock (this)
            {
                if (bOpen )
                {
                    serialPort1.WriteLine("Z");
                    System.Threading.Thread.Sleep(200);
                    strResult = serialPort1.ReadExisting();
                }
                else
                {
                    strResult = "PorError";
                }
            }
            return strResult;
        }
        private void UpdateCurrentData(bool bTimeOut,bool bStable, string strValue)
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
                            txtBoxCurrentPressure.Text = strValue;
                            if (bStable)
                            {
                                txtBoxCurrentPressure.BackColor = Color.Green;
                            }
                            else
                            {
                                txtBoxCurrentPressure.BackColor = Color.FromKnownColor(KnownColor.Control) ;
                            }
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
