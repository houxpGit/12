using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class ADLINKInputInfoForm : Form
    {
        ADLINKInputInfo hardInfo;
        public ADLINKInputInfoForm(ADLINKInputInfo info)
        {
            InitializeComponent();
            hardInfo = info;
        }

        private void ADLINKInputInfoForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
            comboBoxCardNo.SelectedIndex = hardInfo.iCardNo;
            cmb_CardType.SelectedIndex = hardInfo.hardwareModel - 1;
        }
    
        private void comboBoxCardNo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            hardInfo.iCardNo = comboBoxCardNo.SelectedIndex;
        }

        private void cmb_CardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hardInfo.hardwareModel = Convert.ToUInt16(cmb_CardType.SelectedIndex + 1);
        }
    }
}
