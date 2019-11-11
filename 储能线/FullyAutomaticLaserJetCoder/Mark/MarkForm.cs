using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder.Mark
{
    public partial class MarkForm : Form
    {
        public MarkForm()
        {
            InitializeComponent();
        }
        
        private void MarkForm_Load(object sender, EventArgs e)
        {
            MarkCom.MarkReadWrite();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //MarkCom.SendCmdToMark();
        }
    }
}
