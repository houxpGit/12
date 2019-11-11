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
    public partial class FrmSplashScreen : Form, ISplashForm
    {
        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        public void SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbStatusInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
