﻿using System;
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
    public partial class GoogoTechInputOutputInfoForm : Form
    {

        GoogoTechInputOutputInfo hardInfo;
        public GoogoTechInputOutputInfoForm(GoogoTechInputOutputInfo Info)
        {
            InitializeComponent();
            hardInfo = Info;
        }

        private void DemoInputOutputInfoForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
            comboBoxCardNo.SelectedIndex = hardInfo.iCardNo;
        }

        private void comboBoxCardNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            hardInfo.iCardNo = comboBoxCardNo.SelectedIndex;
        }

    }
}
