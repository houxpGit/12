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
using System.Threading;
using System.Threading.Tasks;
namespace FullyAutomaticLaserJetCoder
{
    public partial class 波形展示 : Form
    {
       public  List<float> date = new List<float>();
        public 波形展示()
        {
            InitializeComponent();
        }
        float time =0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            double sd = 0.0;
            TableManage.TableDriver("运动平台")._GetAdc(2, out sd);
            if (sd > 0.6)
            {
           
               Thread.Sleep(10);
               time = time + (float)0.2;
               curveGraph1.ShowCurve((float)sd, (float)sd);
               date.Add((float)sd);
               DateSave.Instance().Production.WeldDate.Add((float)sd);
                // curveGraph1.
            }
            if (sd < 0.6&& sd >0.1)
            {       
                curveGraph1.ShowCurve(0, 0);
              
            }
            else
            {
                curveGraph1.ShowCurve(0, 0);
                DateSave.Instance().Production.WeldDate.Clear();
                date.Clear();
                time = 0;
                curveGraph1.Clean();
            }
        }

        private void 波形展示_Load(object sender, EventArgs e)
        {
           功率.Text =  DateSave.Instance().Production.WeldPower.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i=0;i<70;i++)
            {
                curveGraph1.ShowCurve(i*10,i*10);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            curveGraph1.Clean();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (float i = 0; i < 70; i++)
            {
                curveGraph1.ShowCurve(i * 10, i * (float) 0.50);
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            curveGraph1.Clean();
        }

        private void 设置最大功率_Click(object sender, EventArgs e)
        {
            int power =int .Parse(功率.Text) ;
            DateSave.Instance().Production.WeldPower = power;
            DateSave.Instance().SaveDoc();
        }
    }
}
