using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace WorldGeneralLib.CognexAsync
{
    public partial class CognexAsyncForm : Form
    {
        public System.Drawing.Image Grey;
        public System.Drawing.Image Green;

        private bool isLive = false;
        private TcpClient client;
        private NetworkStream ns;
        private ManualResetEvent allDone = new ManualResetEvent(false);

        private delegate void SetListBoxCallBack(string str);
        private SetListBoxCallBack setlistboxcallback;

        private delegate void SetRichTextBoxReceiveCallBack(string str);
        private SetRichTextBoxReceiveCallBack setTextBoxReceiveCallBack;

        object objsendlock = new object();

        public bool handeyecalresult = true;
        public bool checkboardcalresult = true;

        #region comment
        /*
         * 0: pickup destination position for left soft stop at the left channel    [x,z]
         * 1: pickup destination position for right soft stop at the left channel   [x,z] 
         * 2: recheck left nozzle after pick up at left channel                     [x,y,z]                        
         * 3: recheck right nozzle after pick up at left channel                    [x,y,z]             
         * 4: soft stop allignment guidance at left channel                         [x,y]
         * 
         * 10:pickup destination position for left soft stop at the right channel   [x,z]
         * 11:pickup destination position for right soft stop at the right channel  [x,z] 
         * 12:recheck left nozzle after pick up at right channel                    [x,y,z]
         * 13:recheck right nozzle after pick up at right channel                   [x,y,z]
         * 14:soft stop allignment guidance at right channel                        [x,y]
         */
        #endregion
        public bool[] alignmentresult = new bool[20];
        public double[] mxdstposition = new double[20];
        public double[] mydstposition = new double[20];
        public double[] mzdstposition = new double[20];

        #region comment
        /*
         * 0 recheck gap after press but have vaccum
         * 1 recheck gap when release vaccum
         */
        #endregion
        public bool[] recheckresult = new bool[2];
        public double[] recheckvalue1 = new double[2];
        public double[] recheckvalue2 = new double[2];
        
        public string sendcalcommand = string.Empty;
        public bool cmdreceived = false;

        public string sendproductioncommand = string.Empty;
        public string sendrecheckcommand = string.Empty;

        public bool bConnectOk = false;
        
        
        public Thread threadConnect;
        public Thread ScanConnectStatusThread;

        public CognexAsyncForm(Panel panel)
        {
            InitializeComponent();
            setlistboxcallback = new SetListBoxCallBack(SetListBox);
            setTextBoxReceiveCallBack = new SetRichTextBoxReceiveCallBack(SetTextBoxReceive);
            this.TopLevel = false;
            panel.Controls.Add(this);
            this.Size = panel.Size;
            this.Show();
            for (int i = 0; i < 20; i++)
            {
                alignmentresult[i] = false;
            }
            ScanConnectStatusThread = new Thread(ScanConnectStatus);
            ScanConnectStatusThread.IsBackground = true;
            ScanConnectStatusThread.Start();
        }

        public void ScanConnectStatus()
        {
            while (true)
            {
                if (bConnectOk)
                {
                    this.Invoke(
                        new Action(() =>
                        {
                            ConnIndicatePB.Image = Green;
                        }
                        )
                    );
                }
                else
                {
                    this.Invoke(
                        new Action(() =>
                        {
                            ConnIndicatePB.Image = Grey;
                        }
                        )
                    );
                }
                Thread.Sleep(10);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {    
            StartConnect();
        }

        public void StartConnect()
        {
            if (bConnectOk)
            {
                MessageBox.Show("已经连接");
                return;
            }
            threadConnect = new Thread(ConnectToCognex);
            threadConnect.IsBackground = true;
            threadConnect.Start();
        }

        public void ConnectToCognex()
        {
            IPAddress ipaddress = new IPAddress(ipAddressControlCognex.GetAddressBytes());
            int nport = int.Parse(textBoxPort.Text);
            Connect(ipaddress, nport);
        }

        public void Connect(IPAddress ipaddress, int nport)
        {
            client = new TcpClient(AddressFamily.InterNetwork);
            allDone.Reset();
            try
            {
                client.Connect(ipaddress,nport);
                client.Client.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 300, 10), null); 
                this.Invoke(
                    new Action(() =>
                    {
                        listBoxStatus.Invoke(setlistboxcallback, string.Format("IP Address:{0}", client.Client.LocalEndPoint));
                        listBoxStatus.Invoke(setlistboxcallback, string.Format("Success Connect to {0}", client.Client.RemoteEndPoint));
                    }
                  )
                );
                bConnectOk = true;
            }
            catch (Exception ex)
            {
                this.Invoke(
                    new Action(() =>
                    {
                        listBoxStatus.Invoke(setlistboxcallback, ex.ToString());
                    }
                 )
                );
                bConnectOk = false;
                if (client != null)
                {
                    client.Close();
                }
                return;
            }
            ns = client.GetStream();
            DataRead dataRead = new DataRead(ns, client.ReceiveBufferSize);
            ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);
            allDone.WaitOne();
        }

        private void ReadCallBack(IAsyncResult iar)
        {
            try
            {
                DataRead dataRead = (DataRead)iar.AsyncState;
                int recv = dataRead.ns.EndRead(iar);
                if (recv < 1)
                {
                    if (client != null)
                    {
                        client.Close();
                    }
                    bConnectOk = false;
                }
                else
                {
                    textBoxRecv.Invoke(setTextBoxReceiveCallBack, Encoding.UTF8.GetString(dataRead.msg, 0, recv));
                    if (isLive == false)
                    {
                        dataRead = new DataRead(ns, client.ReceiveBufferSize);
                        ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);
                    }
                    bConnectOk = true;
                }
            }
            catch (Exception e)
            {
                bConnectOk = false;
                this.Invoke(
                   new Action(() =>
                   {
                       listBoxStatus.Invoke(setlistboxcallback, e.Message);
                   }
                )
               );
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

        private bool SendData(string str)
        {
            bool result = false;
            if (str == null || str.Length < 0)
                return result;
            try
            {
                byte[] bytesdata = Encoding.Default.GetBytes(str);
                if (client == null || !client.Connected || !bConnectOk)
                    return false;
                ns.BeginWrite(bytesdata, 0, bytesdata.Length, new AsyncCallback(SendCallBack), ns);
                ns.Flush();
                this.Invoke(new Action(() =>
                    { listBoxStatus.Items.Add(str); })
                );
                return true;
            }
            catch(Exception e)
            {
                this.Invoke(new Action(() =>
                  { listBoxStatus.Items.Add(e.Message); })
                );
                return false;
            }
        }

        private void SendCallBack(IAsyncResult iar)
        {
            try
            {
                ns.EndWrite(iar);
                cmdreceived = false;
            }
            catch (Exception e)
            {
                listBoxStatus.Invoke(setlistboxcallback, e.Message);
            }
        }

        private void SetListBox(string str)
        {
            this.Invoke(new Action(() =>
                {
                    listBoxStatus.Items.Add(str);
                    listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                    listBoxStatus.ClearSelected();
                })
            );
        }

        private void SetTextBoxReceive(string str)
        {
            textBoxRecv.AppendText(str);
            ParseRecevieResult(str);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendData(textBoxSend.Text);
            textBoxSend.Clear();
        }

        private void CognexAsyncForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isLive = true;
            allDone.Set();
            if (ScanConnectStatusThread.IsAlive)
                ScanConnectStatusThread.Abort();
        }

        private void CognexAsyncForm_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CognexAsyncForm));
            Green = (System.Drawing.Image)(resources.GetObject("GreenPB.Image"));
            Grey = (System.Drawing.Image)(resources.GetObject("GreyPB.Image"));
            ConnIndicatePB.Image = Grey;
            ipAddressControlCognex.SetAddressBytes(CognexAsyncManage.m_cognexAsyDoc.ipAddress.GetAddressBytes());
            textBoxPort.Text = CognexAsyncManage.m_cognexAsyDoc.nhostport.ToString();
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CognexAsyncManage.m_cognexAsyDoc.nhostport = int.Parse(textBoxPort.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ipAddressControlCognex_TextChanged(object sender, EventArgs e)
        {
            CognexAsyncManage.m_cognexAsyDoc.ipAddress = new IPAddress(ipAddressControlCognex.GetAddressBytes());
            CognexAsyncManage.m_cognexAsyDoc.hostipaddress = CognexAsyncManage.m_cognexAsyDoc.ipAddress.ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxRecv.Clear();
        }

        #region comment
        /*
        * 0: left camera1 position1                  1
        * 1: left camera1 position2                  2
        * 2: left camera2 checkboard                 3
        * 3: left camera2 9 hand eye calibration     4
        * 4: left camera3                            5
        * 5: right camera1 position1                 6
        * 6: right camera1 position2                 7
        * 7: right camera2 checkboard                8
        * 8: right camera2 9 hand eye calibration    9
        * 9: right camera3                           10
        * 10: camera4 recheck                        11
       */
        #endregion
        public bool bP100CmdRcved  = false;
        public bool bP100CmdResult = false;
        public bool bP100CmdRepeat = false;
        public double m_dP100XPos = 0.0;
        public double m_dP100ZPos = 0.0;


        public bool bP110CmdRcved = false;
        public bool bP110CmdResult = false;
        public bool bP110CmdRepeat = false;
        public double m_dP110XPos = 0.0;
        public double m_dP110ZPos = 0.0;

        public bool bP120CmdRcved = false;
        public bool bP120CmdResult = false;
        public bool bP120CmdRepeat = false;
        public double m_dP120XPos = 0.0;
        public double m_dP120YPos = 0.0;
        public double m_dP120ZPos = 0.0;


        public bool bP130CmdRcved = false;
        public bool bP130CmdResult = false;
        public bool bP130CmdRepeat = false;
        public double m_dP130XPos = 0.0;
        public double m_dP130YPos = 0.0;
        public double m_dP130ZPos = 0.0;

        public bool bP140CmdRcved = false;
        public bool bP140CmdResult = false;
        public bool bP140CmdRepeat = false;
        public double m_dP140XPos = 0.0;
        public double m_dP140YPos = 0.0;

        public bool bP200CmdRcved = false;
        public bool bP200CmdResult = false;
        public bool bP200CmdRepeat = false;
        public double m_dP200XPos = 0.0;
        public double m_dP200ZPos = 0.0;


        public bool bP210CmdRcved = false;
        public bool bP210CmdResult = false;
        public bool bP210CmdRepeat = false;
        public double m_dP210XPos = 0.0;
        public double m_dP210ZPos = 0.0;

        public bool bP220CmdRcved = false;
        public bool bP220CmdResult = false;
        public bool bP220CmdRepeat = false;
        public double m_dP220XPos = 0.0;
        public double m_dP220YPos = 0.0;
        public double m_dP220ZPos = 0.0;


        public bool bP230CmdRcved = false;
        public bool bP230CmdResult = false;
        public bool bP230CmdRepeat = false;
        public double m_dP230XPos = 0.0;
        public double m_dP230YPos = 0.0;
        public double m_dP230ZPos = 0.0;

        public bool bP240CmdRcved = false;
        public bool bP240CmdResult = false;
        public bool bP240CmdRepeat = false;
        public double m_dP240XPos = 0.0;
        public double m_dP240YPos = 0.0;

        public bool bP300CmdRcved = false;
        public bool bP300CmdResult = false;
        public double m_dP300DLVal = 0.0;
        public double m_dP300URVal = 0.0;

        public bool bP400CmdRcved = false;
        public bool bP400CmdResult = false;
        public bool bP400CmdRepeat = false;
        public double m_dP400DLVal = 0.0;
        public double m_dP400URVal = 0.0;


        public void ParseRecevieResult(string strrcv)
        {
            string strcommand = string.Empty;
            char[] charsplit = new char[1];
            charsplit[0] = ',';
            
            if (strrcv.IndexOf("\r\n") > -1)
            {
                try
                {
                    strcommand = strrcv.Substring(0, strrcv.IndexOf("\r\n"));
                    string[] strparse = strcommand.Split(charsplit, StringSplitOptions.RemoveEmptyEntries);
                    #region parse calibration command
                    #region start hand eye calibrate
                    if (strparse[0].Equals("SC") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("SC") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region end hand eye calibrate
                    if (strparse[0].Equals("EC") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("EC") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region 9-point hand eye calibrate
                    #region left channel
                    #region C1
                    if (strparse[0].Equals("C1") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C1") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C2
                    if (strparse[0].Equals("C2") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C2") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C6
                    if (strparse[0].Equals("C6") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C6") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C4
                    if (strparse[0].Equals("C4") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C4") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C5
                    if (strparse[0].Equals("C5") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C5") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #endregion

                    #region right channel
                    #region C7
                    if (strparse[0].Equals("C7") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C7") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C8
                    if (strparse[0].Equals("C8") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C8") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C12
                    if (strparse[0].Equals("C12") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C12") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C10
                    if (strparse[0].Equals("C10") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C10") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #region C11
                    if (strparse[0].Equals("C11") && strparse[1].Equals("0"))
                    {
                        handeyecalresult = false;
                    }
                    else if (strparse[0].Equals("C11") && strparse[1].Equals("1"))
                    {
                        handeyecalresult = true;
                    }
                    #endregion
                    #endregion
                    #endregion

                    #region checkboard calibrate
                    #region T10
                    if (strparse[0].Equals("T10") && strparse[1].Equals("0"))
                    {
                        checkboardcalresult = false;
                    }
                    else if (strparse[0].Equals("T10") && strparse[1].Equals("1"))
                    {
                        checkboardcalresult = true;
                    }
                    #endregion
                    #region T20
                    if (strparse[0].Equals("T20") && strparse[1].Equals("0"))
                    {
                        checkboardcalresult = false;
                    }
                    else if (strparse[0].Equals("T20") && strparse[1].Equals("1"))
                    {
                        checkboardcalresult = true;
                    }
                    #endregion
                    #region T30
                    if (strparse[0].Equals("T30") && strparse[1].Equals("0"))
                    {
                        checkboardcalresult = false;
                    }
                    else if (strparse[0].Equals("T30") && strparse[1].Equals("1"))
                    {
                        checkboardcalresult = true;
                    }
                    #endregion
                    #endregion
                    #endregion
                    #region parse production command
                    #region comment
                    /*
                     * 0: pickup destination position for left soft stop at the left channel    [x,z]
                     * 1: pickup destination position for right soft stop at the left channel   [x,z] 
                     * 2: recheck after left nozzle pick up at left channel                     [x,y]
                     * 3: recheck after right nozzle pick up at left channel                    [x,y]
                     * 4: soft stop allignment guidance at left channel                         [x,y]
                     * 
                     * 10:pickup destination position for left soft stop at the right channel   [x,z]
                     * 11:pickup destination position for right soft stop at the right channel  [x,z] 
                     * 12:recheck after left nozzle pick up at right channel                    [x,y]
                     * 13:recheck after right nozzle pick up at right channel                   [x,y]
                     * 14:soft stop allignment guidance at right channel                        [x,y]
                     */
                    #endregion
                    #region left channel
                    #region P100
                    if (strparse[0].Equals("P100") && strparse[1].Equals("0"))
                    {
                        if (!bP100CmdRepeat)
                        {
                            bP100CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(100, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn1curPos, 0, CognexAsyncManage.m_dzleftn1curPos);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            DialogResult imageoperationresult = form.ShowDialog();
                            if (imageoperationresult == DialogResult.Ignore)
                            {
                                bP100CmdRcved = true;
                                bP100CmdResult = true;
                                bP100CmdRepeat = false;
                                if (!string.IsNullOrEmpty(strparse[2]))
                                {
                                    m_dP100XPos = double.Parse(strparse[2]);
                                }
                                if (!string.IsNullOrEmpty(strparse[3]))
                                {
                                    m_dP100ZPos = double.Parse(strparse[3]);
                                }
                            }
                            else if (imageoperationresult == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(100, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn1curPos, 0, CognexAsyncManage.m_dzleftn1curPos);
                            }
                            else if (imageoperationresult == DialogResult.No)
                            {
                                bP100CmdRcved = true;
                                bP100CmdResult = false;
                                bP100CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P100") && strparse[1].Equals("1"))
                    {
                        bP100CmdRcved = true;
                        bP100CmdResult = true;
                        bP100CmdRepeat = false;
                        m_dP100XPos = double.Parse(strparse[2]);
                        m_dP100ZPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #region P110
                    if (strparse[0].Equals("P110") && strparse[1].Equals("0"))
                    {
                        if (!bP110CmdRepeat)
                        {
                            bP110CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(110, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn2curPos, 0, CognexAsyncManage.m_dzleftn2curPos);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP110CmdRcved = true;
                                bP110CmdResult = true;
                                bP110CmdRepeat = false;
                                m_dP110XPos = double.Parse(strparse[2]);
                                m_dP110ZPos = double.Parse(strparse[3]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(110, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn2curPos, 0, CognexAsyncManage.m_dzleftn2curPos);
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP110CmdRcved = true;
                                bP110CmdResult = false;
                                bP110CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P110") && strparse[1].Equals("1"))
                    {
                        bP110CmdRcved = true;
                        bP110CmdResult = true;
                        bP110CmdRepeat = false;
                        m_dP110XPos = double.Parse(strparse[2]);
                        m_dP110ZPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #region P120
                    if (strparse[0].Equals("P120") && strparse[1].Equals("0"))
                    {
                        if (!bP120CmdRepeat)
                        {
                            bP120CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(120, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn1curPos, CognexAsyncManage.m_dyleftn1curPos, 0);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP120CmdRcved = true;
                                bP120CmdResult = true;
                                bP120CmdRepeat = false;
                                m_dP120XPos = double.Parse(strparse[2]);
                                m_dP120YPos = double.Parse(strparse[3]);
                                m_dP120ZPos = double.Parse(strparse[4]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(120, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn1curPos, CognexAsyncManage.m_dyleftn1curPos, 0);
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP120CmdRcved = true;
                                bP120CmdResult = false;
                                bP120CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P120") && strparse[1].Equals("1"))
                    {
                        bP120CmdRcved = true;
                        bP120CmdResult = true;
                        bP120CmdRepeat = false;
                        m_dP120XPos = double.Parse(strparse[2]);
                        m_dP120YPos = double.Parse(strparse[3]);
                        m_dP120ZPos = double.Parse(strparse[4]);
                    }
                    #endregion
                    #region P130
                    if (strparse[0].Equals("P130") && strparse[1].Equals("0"))
                    {
                        if (!bP130CmdRepeat)
                        {
                            bP130CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(130, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn2curPos, CognexAsyncManage.m_dyleftn2curPos, 0);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP130CmdRcved = true;
                                bP130CmdResult = true;
                                bP130CmdRepeat = false;
                                m_dP130XPos = double.Parse(strparse[2]);
                                m_dP130YPos = double.Parse(strparse[3]);
                                m_dP130ZPos = double.Parse(strparse[4]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(130, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxleftn2curPos, CognexAsyncManage.m_dyleftn2curPos, 0);
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP130CmdRcved = true;
                                bP130CmdResult = false;
                                bP130CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P130") && strparse[1].Equals("1"))
                    {
                        bP130CmdRcved = true;
                        bP130CmdResult = true;
                        bP130CmdRepeat = false;
                        m_dP130XPos = double.Parse(strparse[2]);
                        m_dP130YPos = double.Parse(strparse[3]);
                        m_dP130ZPos = double.Parse(strparse[4]);
                    }
                    #endregion
                    #region P140
                    if (strparse[0].Equals("P140") && strparse[1].Equals("0"))
                    {
                        if (!bP140CmdRepeat)
                        {
                            bP140CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(140, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP140CmdRcved = true;
                                bP140CmdResult = true;
                                bP140CmdRepeat = false;
                                m_dP140XPos = double.Parse(strparse[2]);
                                m_dP140YPos = double.Parse(strparse[3]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(140, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP140CmdRcved = true;
                                bP140CmdResult = false;
                                bP140CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P140") && strparse[1].Equals("1"))
                    {
                        bP140CmdRcved = true;
                        bP140CmdResult = true;
                        bP140CmdRepeat = false;
                        m_dP140XPos = double.Parse(strparse[2]);
                        m_dP140YPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #endregion
                    #region right channel
                    #region P200
                    if (strparse[0].Equals("P200") && strparse[1].Equals("0"))
                    {
                        if (!bP200CmdRepeat)
                        {
                            bP200CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(200, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn1curPos, 0, CognexAsyncManage.m_dzrightn1curPos);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            DialogResult imageoperationresult = form.ShowDialog();
                            if (imageoperationresult == DialogResult.Ignore)
                            {
                                bP200CmdRcved = true;
                                bP200CmdResult = true;
                                bP200CmdRepeat = false;
                                if (!string.IsNullOrEmpty(strparse[2]))
                                {
                                    m_dP200XPos = double.Parse(strparse[2]);
                                }
                                if (!string.IsNullOrEmpty(strparse[3]))
                                {
                                    m_dP200ZPos = double.Parse(strparse[3]);
                                }
                            }
                            else if (imageoperationresult == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(200, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn1curPos, 0, CognexAsyncManage.m_dzrightn1curPos);
                            }
                            else if (imageoperationresult == DialogResult.No)
                            {
                                bP200CmdRcved = true;
                                bP200CmdResult = false;
                                bP200CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P200") && strparse[1].Equals("1"))
                    {
                        bP200CmdRcved = true;
                        bP200CmdResult = true;
                        bP200CmdRepeat = false;
                        m_dP200XPos = double.Parse(strparse[2]);
                        m_dP200ZPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #region P210
                    if (strparse[0].Equals("P210") && strparse[1].Equals("0"))
                    {
                        if (!bP210CmdRepeat)
                        {
                            bP210CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(210, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn2curPos, 0, CognexAsyncManage.m_dzrightn2curPos);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            DialogResult imageoperationresult = form.ShowDialog();
                            if (imageoperationresult == DialogResult.Ignore)
                            {
                                bP210CmdRcved = true;
                                bP210CmdResult = true;
                                bP210CmdRepeat = false;
                                if (!string.IsNullOrEmpty(strparse[2]))
                                {
                                    m_dP210XPos = double.Parse(strparse[2]);
                                }
                                if (!string.IsNullOrEmpty(strparse[3]))
                                {
                                    m_dP210ZPos = double.Parse(strparse[3]);
                                }
                            }
                            else if (imageoperationresult == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(210, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn2curPos, 0, CognexAsyncManage.m_dzrightn2curPos);
                            }
                            else if (imageoperationresult == DialogResult.No)
                            {
                                bP210CmdRcved = true;
                                bP210CmdResult = false;
                                bP210CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P210") && strparse[1].Equals("1"))
                    {
                        bP210CmdRcved = true;
                        bP210CmdResult = true;
                        bP210CmdRepeat = false;
                        m_dP210XPos = double.Parse(strparse[2]);
                        m_dP210ZPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #region P220
                    if (strparse[0].Equals("P220") && strparse[1].Equals("0"))
                    {
                        if (!bP220CmdRepeat)
                        {
                            bP220CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(220, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn1curPos, CognexAsyncManage.m_dyrightn1curPos, 0);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP220CmdRcved = true;
                                bP220CmdResult = true;
                                bP220CmdRepeat = false;
                                m_dP220XPos = double.Parse(strparse[2]);
                                m_dP220YPos = double.Parse(strparse[3]);
                                m_dP220ZPos = double.Parse(strparse[4]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(220, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn1curPos, CognexAsyncManage.m_dyrightn1curPos, 0);
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP220CmdRcved = true;
                                bP220CmdResult = false;
                                bP220CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P220") && strparse[1].Equals("1"))
                    {
                        bP220CmdRcved = true;
                        bP220CmdResult = true;
                        bP220CmdRepeat = false;
                        m_dP220XPos = double.Parse(strparse[2]);
                        m_dP220YPos = double.Parse(strparse[3]);
                        m_dP220ZPos = double.Parse(strparse[4]);
                    }
                    #endregion
                    #region P230
                    if (strparse[0].Equals("P230") && strparse[1].Equals("0"))
                    {
                        if (!bP230CmdRepeat)
                        {
                            bP230CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(230, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn2curPos, CognexAsyncManage.m_dyrightn2curPos, 0);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP230CmdRcved = true;
                                bP230CmdResult = true;
                                bP230CmdRepeat = false;
                                m_dP230XPos = double.Parse(strparse[2]);
                                m_dP230YPos = double.Parse(strparse[3]);
                                m_dP230ZPos = double.Parse(strparse[4]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(230, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dxrightn2curPos, CognexAsyncManage.m_dyrightn2curPos, 0);
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP230CmdRcved = true;
                                bP230CmdResult = false;
                                bP230CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P230") && strparse[1].Equals("1"))
                    {
                        bP230CmdRcved = true;
                        bP230CmdResult = true;
                        bP230CmdRepeat = false;
                        m_dP230XPos = double.Parse(strparse[2]);
                        m_dP230YPos = double.Parse(strparse[3]);
                        m_dP230ZPos = double.Parse(strparse[4]);
                    }
                    #endregion
                    #region P240
                    if (strparse[0].Equals("P240") && strparse[1].Equals("0"))
                    {
                        if (!bP240CmdRepeat)
                        {
                            bP240CmdRepeat = true;
                            Thread.Sleep(10);
                            SendProductionCmd(240, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP240CmdRcved = true;
                                bP240CmdResult = true;
                                bP240CmdRepeat = false;
                                m_dP240XPos = double.Parse(strparse[2]);
                                m_dP240YPos = double.Parse(strparse[3]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(240, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP240CmdRcved = true;
                                bP240CmdResult = false;
                                bP240CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P240") && strparse[1].Equals("1"))
                    {
                        bP240CmdRcved = true;
                        bP240CmdResult = true;
                        bP240CmdRepeat = false;
                        m_dP240XPos = double.Parse(strparse[2]);
                        m_dP240YPos = double.Parse(strparse[3]);
                    }
                    #endregion
                    #endregion
                    #endregion
                    #region recheck
                    #region P300
                    if (strparse[0].Equals("P300") && strparse[1].Equals("0"))
                    {
                        bP300CmdRcved = true;
                        bP300CmdResult = false;
                    }
                    else if (strparse[0].Equals("P300") && strparse[1].Equals("1"))
                    {
                        bP300CmdRcved = true;
                        bP300CmdResult = true;
                        m_dP300DLVal = double.Parse(strparse[2]);
                        m_dP300URVal = double.Parse(strparse[3]);
                    }
                    #endregion
                    #region P400
                    if (strparse[0].Equals("P400") && strparse[1].Equals("0"))
                    {
                        if (!bP400CmdRepeat)
                        {
                            bP400CmdRepeat = true;
                            Thread.Sleep(10);
                            //SendProductionCmd(400, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                            SendRecheckCmd(400, CognexAsyncManage.rightsn, CognexAsyncManage.colornumber, CognexAsyncManage.m_dp400dlvalmax, CognexAsyncManage.m_dp400dlvalmin, CognexAsyncManage.m_dp400urvalmax, CognexAsyncManage.m_dp400urvalmin);
                        }
                        else
                        {
                            ImageConfirmForm form = new ImageConfirmForm();
                            if (form.ShowDialog() == DialogResult.Ignore)
                            {
                                bP400CmdRcved = true;
                                bP400CmdResult = true;
                                bP400CmdRepeat = false;
                                m_dP400DLVal = double.Parse(strparse[2]);
                                m_dP400URVal = double.Parse(strparse[3]);
                            }
                            else if (form.ShowDialog() == DialogResult.Retry)
                            {
                                Thread.Sleep(10);
                                SendProductionCmd(400, CognexAsyncManage.leftsn, CognexAsyncManage.colornumber, 0.0, 0.0, 0);//??
                            }
                            else if (form.ShowDialog() == DialogResult.No)
                            {
                                bP400CmdRcved = true;
                                bP400CmdResult = false;
                                bP400CmdRepeat = false;
                            }
                        }
                    }
                    else if (strparse[0].Equals("P400") && strparse[1].Equals("1"))
                    {
                        bP400CmdRcved = true;
                        bP400CmdResult = true;
                        m_dP400DLVal = double.Parse(strparse[2]);
                        m_dP400URVal = double.Parse(strparse[3]);
                    }
                    #endregion
                    #endregion
                    cmdreceived = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region comment
        /*
         * 0: left camera1 position1                         1
         * 1: left camera1 position2                         2
         * 2: left camera2 checkboard                        3
         * 3: left camera2 9 hand eye calibration nozzle1    4    
         * 5  left camera2 9 hand eye calibration nozzle2    5
         * 4: left camera3                                   6
       
         * 5: right camera1 position1                        7
         * 6: right camera1 position2                        8
         * 7: right camera2 checkboard                       9
         * 8: right camera2 9 hand eye calibration1          10
         * 9: right camera2 9 hand eye calibration2          11
         * 9: right camera3                                  12
         * 
         * 10: camera4 recheck                               13
        */

        /*HAND EYE CALIBRATE
         * channel
         * 0:left
         * 1:right
         * 
         * calindex
         * 0: cam1 nozzle1
         * 1: cam1 nozzle1
         * 
         * istep  only for calibrate type 1
         * 0:start calibration
         * 1:p0
         * 2:p1
         * 3:p2
         * 4:p3
         * 5:p4
         * 6:p5
         * 7:p6
         * 8:p7
         * 9:p8
         * 10:end calibration
         *         
         */
        #endregion
        public void SendCalibrateHE(int channel,int calindex,int istep,double mx = 0.0f,double my = 0.0f,double mz =0.0f)
        {
            switch (istep)
            {
                case 0:
                    #region start calibration
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "SC,1,9";
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "SC,2,9";
                        }
                    }
                    else if(channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "SC,7,9";
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "SC,8,9";
                        }
                    }
                    break;
                    #endregion
                case 1:
                    #region calibrate 1 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1,"+mx.ToString()+","+mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7,"+mx.ToString()+","+mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 2:
                    #region calibrate 2 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 3:
                    #region calibrate 3 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 4:
                    #region calibrate 4 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 5:
                    #region calibrate 5 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 6:
                    #region calibrate 6 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 7:
                    #region calibrate 7 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 8:
                    #region calibrate 8 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 9:
                    #region calibrate 9 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C1," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C2," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0)    // camera 1 position1
                        {
                            sendcalcommand = "C7," + mx.ToString() + "," + mz.ToString();
                        }
                        else if (calindex == 1) //camera 1 position2
                        {
                            sendcalcommand = "C8," + mx.ToString() + "," + mz.ToString();
                        }
                    }
                    break;
                    #endregion
                case 10:
                    #region end calibrate
                    if (channel == 0)   //left channel
                    {
                        sendcalcommand = "EC";
                    }
                    else if (channel == 1) //right channel
                    {
                        sendcalcommand = "EC";
                    }
                    break;
                    #endregion
                default:
                    MessageBox.Show("The Calibrate Command is wrong,please check");
                    return;
            }
            sendcalcommand  += "\r\n";
            SendData(sendcalcommand);      
        }

        #region comment
        /*HAND EYE CALIBRATE
        * channel
        * 0:left
        * 1:right
        * 
        * calindex
        * 0: cam2&cam3 nozzle1
        * 1: cam2&cam3 nozzle2
        * 
        * istep  only for calibrate type 1
        * 0:start calibration
        * 1:p0
        * 2:p1
        * 3:p2
        * 4:p3
        * 5:p4
        * 6:p5
        * 7:p6
        * 8:p7
        * 9:p8
        * 10:p9
        * 11:end calibration
        *         
        */
        #endregion
        public void SendCalibrate2CAMHE(int channel, int calindex, int istep, double mx = 0.0f, double my = 0.0f, double mz = 0.0f)
        {
            switch (istep)
            {
                case 0:
                    #region start calibration
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 2 & camera 3  position1
                        {
                            sendcalcommand = "SC,4,1,6,9";
                        }
                        else if (calindex == 1) //camera 2 & camera 3  position2
                        {
                            sendcalcommand = "SC,5,1,6,9";
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 2 & camera3 position1 
                        {
                            sendcalcommand = "SC,10,1,12,9";
                        }
                        else if (calindex == 1) //camera 2 & camera3 position2 
                        {
                            sendcalcommand = "SC,11,1,12,9";
                        }
                    }
                    break;
                    #endregion
                case 1:
                    #region calibrate 0 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 2  position1
                        {
                            sendcalcommand = "C4," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 2 position2
                        {
                            sendcalcommand = "C5," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 2  position1
                        {
                            sendcalcommand = "C10," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 2 position2
                        {
                            sendcalcommand = "C11," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 2:
                    #region calibrate 1 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 3:
                    #region calibrate 2 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 4:
                    #region calibrate 3 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 5:
                    #region calibrate 4 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 6:
                    #region calibrate 5 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 7:
                    #region calibrate 6 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 8:
                    #region calibrate 7 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 9:
                    #region calibrate 8 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 10:
                    #region calibrate 9 point
                    if (channel == 0)   //left channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1)//camera 3 position2
                        {
                            sendcalcommand = "C6," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    else if (channel == 1) //right channel
                    {
                        if (calindex == 0) //camera 3 position1
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                        else if (calindex == 1) //camera 3 position2
                        {
                            sendcalcommand = "C12," + mx.ToString() + "," + my.ToString();
                        }
                    }
                    break;
                    #endregion
                case 11:
                    #region end calibrate
                    if (channel == 0)   //left channel
                    {
                         sendcalcommand = "EC";
                    }
                    else if (channel == 1) //right channel
                    {
                         sendcalcommand = "EC";
                    }
                    break;
                    #endregion
                default:
                    MessageBox.Show("The Calibrate Command is wrong,please check");
                    return;
            }
            sendcalcommand += "\r\n";
            SendData(sendcalcommand);
        }

        #region comment
        /*CHECK BOARD CALIBRATE
         * channel
         * 0:cam2 left
         * 1:cam2 right
         * 2:recheck cam
         
         * istep  only for calibrate type 1
         * 0:start calibration
         * 1:check board calibration
         * 2:end calibration      
        */
        #endregion
        public void SendCalibrateCB(int channel, int istep)
        {
            switch (istep)
            {
                case 0:
                    #region checkboard start
                    if (channel == 0)   //left cam2 channel
                    {
                       sendcalcommand = "T10,-2";
                    }
                    else if(channel == 1) //right cam2 channel
                    {
                       sendcalcommand = "T20,-2";
                    }
                    else if (channel == 2) //recheck cam
                    {
                        sendcalcommand = "T30,-2";
                    }
                    break;
                    #endregion
                case 1:
                    #region checkboard calibrate
                    if (channel == 0)   //left cam2 channel
                    {
                        sendcalcommand = "T10,1";
                    }
                    else if (channel == 1) //right cam2 channel
                    {
                        sendcalcommand = "T20,1";
                    }
                     else if (channel == 2) //recheck cam
                    {
                        sendcalcommand = "T30,1";
                    }
                    break;
                    #endregion
                case 2:
                    #region checkboard finish
                    if (channel == 0)   //left cam2 channel
                    {
                        sendcalcommand = "T10,-1";
                    }
                    else if (channel == 1) //right cam2 channel
                    {
                        sendcalcommand = "T20,-1";
                    }
                     else if (channel == 2) //recheck cam
                    {
                        sendcalcommand = "T30,-1";
                    }
                    break;
                    #endregion
                default:
                    MessageBox.Show("The Calibrate Command is wrong,please check");
                    return;
            }
            sendcalcommand += "\r\n";
            SendData(sendcalcommand);
        }

        #region comment
        /*
         *cmdindex
         *100   pickup guidance for left soft stop at the left channel
         *110   pickup guidance for right soft stop at the left channel
         *120   recheck left nozzle after pick up at left channel
         *130   recheck right nozzle after pick up at left channel
         *140   soft stop allignment guidance at left channel
         *
         *
         *200   pickup guidance for left soft stop at the right channel 
         *210   pickup guidance for right soft stop at the right channel 
         *220   recheck left nozzle after pick up at right channel 
         *230   recheck right nozzle after pick up at right channel
         *240   soft stop allignment guidance at right channel 
         *
         */
        #endregion
        public void SendProductionCmd(int cmdindex,string sn,int colornumber,double mdcx, double mdcy, double mdcz)
        {
            switch (cmdindex)
            {
                case 100://left nozzle pickup at left channel
                    sendproductioncommand = "P100," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcz.ToString();
                    break;
                case 110://right nozzle pickup at left channel
                    sendproductioncommand = "P110," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcz.ToString();
                    break;
                case 120://recheck after left nozzle pickup at left channel
                    sendproductioncommand = "P120," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
                case 130://recheck after right nozzle pickup at left channel
                    sendproductioncommand = "P130," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
                case 140://soft stop alignment guidance at left channel
                    sendproductioncommand = "P140," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
                case 200://left nozzle pickup at right channel
                    sendproductioncommand = "P200," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcz.ToString();
                    break;
                case 210://right nozzle pickup at right channel
                    sendproductioncommand = "P210," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcz.ToString();
                    break;
                case 220://recheck after left nozzle pickup at right channel
                    sendproductioncommand = "P220," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
                case 230://recheck after right nozzle pickup at right channel
                    sendproductioncommand = "P230," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
                case 240://soft stop alignment guidance at right channel
                    sendproductioncommand = "P240," + sn + "," + colornumber.ToString() + "," + mdcx.ToString() + "," + mdcy.ToString();
                    break;
            }
            sendproductioncommand += "\r\n";
            SendData(sendproductioncommand);
        }

        #region comment
        /*
         *300   recheck gap after press but have vaccum 
         *400   recheck gap when release vaccum 
         */
        #endregion
        public void SendRecheckCmd(int cmdindex, string sn, int colornumber, double dlmax, double dlmin, double urmax, double urmin)
        {
            switch (cmdindex)
            {
                case 300:
                    sendrecheckcommand = "P300," + sn + "," + colornumber.ToString() + "," + dlmax.ToString() + "," + dlmin.ToString() + "," + urmax.ToString() + "," + urmin.ToString();
                    break;
                case 400:
                    sendrecheckcommand = "P400," + sn + "," + colornumber.ToString() + "," + dlmax.ToString() + "," + dlmin.ToString() + "," + urmax.ToString() + "," + urmin.ToString();
                    break;
                default:
                    break;
            }
            sendrecheckcommand += "\r\n";
            SendData(sendrecheckcommand);
        }

    }
}
