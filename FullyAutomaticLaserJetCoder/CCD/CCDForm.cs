using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder.CCD
{
    public partial class CCDForm : Form
    {
        CCDClient ccdClient;
        public CCDForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            CCDCommunication ccdCommunication = new CCDCommunication();
            ccdCommunication.IP = IPAddress.Parse(txtIP.Text);
            ccdCommunication.Port = int.Parse(txtPort.Text);
            ccdClient = new CCDClient(ccdCommunication, txtRecieve);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtRecieve.AppendText(DateTime.Now + " " + "Send:" + txtSend.Text + "\r\n");
            string rec = CCDClientManage.ccdClient.Send(txtSend.Text);
            txtRecieve.AppendText(DateTime.Now + " " + "Return:" + rec+"\r\n");
        }

        private void CCDForm_Load(object sender, EventArgs e)
        {
            CCDClientManage.ccdClient.textBox = txtRecieve;
            txtIP.Text = CCDClientManage.ccdDoc.IPAddress;
            txtPort.Text = CCDClientManage.ccdDoc.Port.ToString();
            txtName.Text = CCDClientManage.ccdDoc.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CCDClientManage.ccdDoc.Name = txtName.Text;
            CCDClientManage.ccdDoc.IPAddress = txtIP.Text;
            CCDClientManage.ccdDoc.IP = IPAddress.Parse(txtIP.Text);
            CCDClientManage.ccdDoc.Port = int.Parse(txtPort.Text);
            CCDClientManage.ccdDoc.SaveDocument();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
