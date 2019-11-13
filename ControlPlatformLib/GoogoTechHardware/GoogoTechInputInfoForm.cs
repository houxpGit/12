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
    public partial class GoogoTechInputInfoForm : Form
    {
        GoogoTechInputInfo hardInfo;
        public GoogoTechInputInfoForm(GoogoTechInputInfo Info)
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
