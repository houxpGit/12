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
    public partial class GoogolTechExtCardInfoForm : Form
    {
        GoogolTechExtCardInfo hardInfo;
        public GoogolTechExtCardInfoForm(GoogolTechExtCardInfo Info)
        {
            InitializeComponent();
            hardInfo = Info;
        }
        

        private void comboBoxCardNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            hardInfo.iCardNo = comboBoxCardNo.SelectedIndex;
        }

        private void textBoxConfigName_Validated(object sender, EventArgs e)
        {
            hardInfo.m_strConfigPath = textBoxConfigName.Text;
        }

        private void txt_DllName_Validated(object sender, EventArgs e)
        {
            hardInfo.m_strExtDllName = txt_DllName.Text;
        }

        private void cmb_ExtCardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            hardInfo.iExtCardNo = cmb_ExtCardNumber.SelectedIndex;
        }

        private void GoogolTechExtCardInfoForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
            comboBoxCardNo.SelectedIndex = hardInfo.iCardNo;
            cmb_ExtCardNumber.SelectedIndex = hardInfo.iExtCardNo;
            textBoxConfigName.Text = hardInfo.m_strConfigPath;
            txt_DllName.Text = hardInfo.m_strExtDllName;
        }
    }
}
