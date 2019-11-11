using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib.Softservo
{
    public partial class SoftservoControlerForm : Form
    {
        SoftservoControlerInfo hardInfo;
        public SoftservoControlerForm(SoftservoControlerInfo hardInfo)
        {
            InitializeComponent();
            this.hardInfo = hardInfo;
        }

        private void SoftservoControlerForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
            comboBoxCardNo.SelectedIndex = hardInfo.iCardNo;
            textBoxConfigName.Text = hardInfo.m_strConfigPath;
        }

        private void comboBoxCardNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            hardInfo.iCardNo = comboBoxCardNo.SelectedIndex;
        }

        private void textBoxConfigName_Validated(object sender, EventArgs e)
        {
            hardInfo.m_strConfigPath = textBoxConfigName.Text;
        }
    }
}
