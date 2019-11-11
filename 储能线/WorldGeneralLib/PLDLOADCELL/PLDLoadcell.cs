using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;



namespace WorldGeneralLib.PLDLOADCELL
{
    public class PLDLoadcell
    {
        SerialPort serialPort;
        public string m_strPressureValue = string.Empty;
        public double m_dCurrentValue = 0.0;

        public Label labelPressure;
        public object lockObj = new object();
        HiPerfTimer hptimer;

        public Form adhereForm;

        public PLDLoadcell()
        {
            hptimer = new HiPerfTimer();
        }

        public bool InitPH20(string strComName)
        {
            serialPort = new SerialPort();
            try
            {
                serialPort.PortName = strComName;
                serialPort.BaudRate = 9600;
                serialPort.DataBits = 7;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.Even;
                serialPort.Open();
                labelPressure.BackColor = Color.Green;
                Thread readloadcellthread = new Thread(ReadLoadcellValue);
                readloadcellthread.IsBackground = true;
                readloadcellthread.Start();
            }
            catch
            {
                this.adhereForm.Invoke(
                    new Action(() =>
                        {
                            labelPressure.Text = "Error";
                            labelPressure.BackColor = Color.Red;
                        }
                    )
                );
                return false;
            }
            return true;
        }

        private void ReadLoadcellValue()
        {
            while (true)
            {
                Thread.Sleep(100);
                if (ReadPressue(ref m_dCurrentValue))
                {
                    this.adhereForm.Invoke(
                       new Action(() =>
                       {
                           labelPressure.Text =m_dCurrentValue.ToString();
                           labelPressure.BackColor = Color.Green;
                       }
                       )
                    );    
                }
            }
        }

        public bool ReadPressue(ref double PressureValue)
        {
            string strReceive = string.Empty;
            string[] strArray;
            char[] charArray = new char[1];
            charArray[0] = ',';
            lock (lockObj)
            {
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write("READ\r\n");
                    hptimer.Start();
                    strReceive = serialPort.ReadExisting();
                    while (true)
                    {
                        strReceive += serialPort.ReadExisting();
                        if (strReceive.IndexOf("\r\n") > -1)
                        { 
                            strReceive = strReceive.Trim();
                            strArray = strReceive.Split(charArray, StringSplitOptions.RemoveEmptyEntries);
                            m_strPressureValue = strArray[2];
                            PressureValue = double.Parse(m_strPressureValue);
                            break;
                        }
                        if (hptimer.TimeUp(0.5))
                        {
                            this.adhereForm.Invoke(
                               new Action(() =>
                               {
                                   labelPressure.Text = "Error";
                                   labelPressure.BackColor = Color.Red;
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
                           labelPressure.Text = "Error";
                           labelPressure.BackColor = Color.Red;
                       }
                       )
                   );
                    return false;
                }
            }
            return true;
        }

        public bool SendData(string data)
        {
            if (serialPort.IsOpen)
            {
                data += "\r\n";
                serialPort.Write(data);
                return true;
            }
            else
                return false;
        }

    }
}
