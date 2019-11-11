using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.DataLogicBarCode;
using WorldGeneralLib.SerialPorts;

namespace FullyAutomaticLaserJetCoder
{
    public partial class FormCCD : Form
    {
        SerialPort sp;
        public FormCCD()
        {
            InitializeComponent();
        //    sp = new SerialPort("com2", 38400, Parity.None, 8, (StopBits)1);

            //EXEToWinform showCCDForm = new EXEToWinform(this.panelCCD, "CCD");
            //showCCDForm.Start(@"C:\Users\Administrator\Desktop\视觉定位系统.exe.lnk");
        }

        private void FormCCD_Load(object sender, EventArgs e)
        {
            //EXEToWinform showCCDForm = new EXEToWinform(this.panelCCD, "CCD");
            //Process[] pa = Process.GetProcesses();
            //Process p = Process.GetProcessesByName("视觉定位系统")[0];
            //showCCDForm.EmbedProcess(p, this.panelCCD);
            FormDatalogicBarcode barcodeForm = new FormDatalogicBarcode();
            barcodeForm.TopLevel = false;
            //gb_Scan.Controls.Add(barcodeForm);
            barcodeForm.Dock = DockStyle.Fill;
            barcodeForm.Show();


            //FormDatalogicBarcode barcodeForm = new FormDatalogicBarcode();
            //barcodeForm.TopLevel = false;
            //gb_Scan.Controls.Add(barcodeForm);
            //barcodeForm.Dock = DockStyle.Fill;
            //barcodeForm.Show();

            SerialPortSettingsForm serialForm = new SerialPortSettingsForm();
            serialForm.TopLevel = false;
            panel2.Controls.Add(serialForm);
            serialForm.Dock = DockStyle.Fill;
            serialForm.Show();
        }

        private void panelCCD_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = null;
            //MessageBox.Show(HexToStr("05"));
            //MarkCom.SendCmdToMark(txtSend.Text);
            MarkCom.SendCmdToMark(SendBytes);
            //MarkCom.SendCmdToMark("98");
            //txtRecv.Text = MarkCom.Recv;//MarkCom.bRecv.ToString();

            //txtRecv.Text = HexToStr(MarkCom.Recv);
        }
        //十六进制字符转换成字符串
        private static string HexToStr(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("utf-8");//"gb2312"
            return chs.GetString(bytes);
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

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] t = new byte[] { 5 };
            sp.DiscardInBuffer();
            sp.Write(t, 0, 1);
            AddMsg("发送5");
            Thread.Sleep(100);
            if (sp.BytesToRead > 0)
            {

                byte[] rcv = new byte[sp.BytesToRead];
                Thread.Sleep(100);
                sp.Read(rcv, 0, sp.BytesToRead);
                switch (rcv[0])
                {
                    case 0x06:
                        // byte[] send = new byte[] { 0x98, 0x00, 0x02, 0x00, 0x01, 0x9B };
                        AddMsg("收到6");
                        string msg = txtCommand.Text;

                        byte[] tSsend = Str2Byte(msg);
                        byte end = OrSum(tSsend);
                        byte[] send = new byte[tSsend.Length + 1];
                        Buffer.BlockCopy(tSsend, 0, send, 0, tSsend.Length);
                        Buffer.BlockCopy(new byte[] { end }, 0, send, tSsend.Length, 1);
                        sp.DiscardInBuffer();
                        sp.Write(send, 0, send.Length);
                        AddMsg("发送数据" + Byte2Str(send));
                        break;
                    default:
                        break;
                }
            }
            Thread.Sleep(100);
            if (sp.BytesToRead > 0)
            {

                byte[] rcv1 = new byte[sp.BytesToRead];
                Thread.Sleep(20);
                sp.Read(rcv1, 0, sp.BytesToRead);
                AddMsg("收到数据" + Byte2Str(rcv1));
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (sp.IsOpen)
            {
                sp.Close();
            }
            sp.Open();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //if (sp.IsOpen)
                sp.Close();
        }
        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private byte OrSum(byte[] data)
        {
            byte rst = 0;
            foreach (var item in data)
            {
                rst ^= item;
            }
            return rst;
        }

        private byte[] Str2Byte(string msg)
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

        private string Byte2Str(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item.ToString("x"));
                sb.Append(" ");
            }
            return sb.ToString();
        }

        private void AddMsg(string msg)
        {
            this.Invoke(new Action(() => { txtRst.AppendText(DateTime.Now.ToString() + "   " + msg + "\r\n"); }
            ));
        }
    }
}
