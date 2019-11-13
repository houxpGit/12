using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

namespace WorldGeneralLib.Cognex
{
    public partial class CognexForm : Form
    {
        public System.Drawing.Image Grey;
        public System.Drawing.Image Green;

        public CognexForm(Panel panel)
        {
            InitializeComponent();
            this.TopLevel = false;
            panel.Controls.Add(this);
            this.Size = panel.Size;
            this.Show();
        }

        private void CognexForm_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CognexForm));
            Green = (Image)(resources.GetObject("GreenPB.Image"));
            Grey = (Image)(resources.GetObject("GreyPB.Image"));
            ConnIndicatePB.Image = Grey;
            ipAddressControlCognex.SetAddressBytes(CognexManage.m_cognexDoc.ipAddress.GetAddressBytes());
            textBoxPort.Text = CognexManage.m_cognexDoc.nhostport.ToString();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (CognexManage.m_cognexclient.bConnectOk)
            {
                CognexManage.m_cognexclient.SendMessage(textBoxSend.Text);
            }
            else
            {
                MessageBox.Show("Error:Please first connect to Cognex server");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxCognex.Clear();
        }

        private void CognexForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CognexManage.m_cognexclient.threadConnect.Abort();
        }

        private void ipAddressControlCognex_TextChanged(object sender, EventArgs e)
        {
            CognexManage.m_cognexDoc.ipAddress = new IPAddress(ipAddressControlCognex.GetAddressBytes());
            CognexManage.m_cognexDoc.hostipaddress = CognexManage.m_cognexDoc.ipAddress.ToString();
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CognexManage.m_cognexDoc.nhostport = int.Parse(textBoxPort.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
