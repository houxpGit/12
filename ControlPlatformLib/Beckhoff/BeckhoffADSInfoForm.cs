using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlPlatformLib.Beckhoff
{
    public partial class BeckhoffADSInfoForm : Form
    {
        BeckhoffADSInfo hardInfo;
        public BeckhoffADSInfoForm(BeckhoffADSInfo Info)
        {
            InitializeComponent();
            hardInfo = Info;
        }

       private void BeckhoffADSInfoForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
            txt_Ip.Text = hardInfo.ipAddress;
        }

        private void txt_Ip_TextChanged(object sender, EventArgs e)
        {
            hardInfo.ipAddress = txt_Ip.Text;
        }
    }
}
