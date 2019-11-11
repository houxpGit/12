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
    public partial class DemoInputInfoForm : Form
    {
        DemoInputInfo hardInfo;
        public DemoInputInfoForm(DemoInputInfo Info)
        {
            InitializeComponent();
            hardInfo = Info;
        }

        private void DemoInputInfoForm_Load(object sender, EventArgs e)
        {
            label1.Text = hardInfo.hardwareName;
        }
    }
}
