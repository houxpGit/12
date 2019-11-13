using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder
{
    public partial class 波形展示 : Form
    {
        public 波形展示()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double sd = 0.0;
            TableManage.TableDriver("运动平台")._GetAdc(1, out sd);
            if (sd > 2)
            {
               // curveGraph1.
            }

        }

        private void 波形展示_Load(object sender, EventArgs e)
        {

        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private Point mouse_offset;
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouse = Control.MousePosition;
                mouse.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mouse;

            }
        }
    }
}
