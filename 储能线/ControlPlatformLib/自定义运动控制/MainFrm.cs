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
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }
        private static MainFrm MainFrom;
        private static object locker = new object();
        public static MainFrm GetForm()
        {
            if (MainFrom == null)
            {
                lock (locker)
                {
                    if (MainFrom == null)
                    {
                        MainFrom = new MainFrm();
                    }
                }
            }
            return MainFrom;
        }
      
        private void MainFrm_Load(object sender, EventArgs e)
        {
            dateGridList.Add(dataGridView1);
            dateGridList.Add(dataGridView2);

            treeView1.ImageList = imageList1;
            this.Resize += new EventHandler(Main_Resize);

            X = this.Width;
            Y = this.Height;

            setTag(this);
            Main_Resize(new object(), new EventArgs());//x,y
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
        }
        private float X = 1024;
        private float Y = 788;
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void Main_Resize(object sender, EventArgs e)   //Form automatic size
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
         //   this.Text = this.Width.ToString() + " " + this.Height.ToString();
           // this.Text = "Jasper Test Station";
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        public int GetPage(string PAGE_sTR)
        {
            int PAGE = 0;
            if (PAGE_sTR =="流程1")
            {
                PAGE = 0;
            }
            if (PAGE_sTR == "流程2")
            {
                PAGE = 1;
            }
            return PAGE;
        }
        List<DataGridView> dateGridList = new List<DataGridView>();
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {

            string PageName = tabControl1.SelectedTab.Name;
            if (PageName != "")
            {
          
                string da = treeView1.SelectedNode.Text;
              //  treeView1.SelectedNode.
              dateGridList[GetPage(PageName)].Rows.Add(new object[] { da });
            }

        }
    }
}
