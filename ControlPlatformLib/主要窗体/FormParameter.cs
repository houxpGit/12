using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class FormParameter : Form
    {
        public MainForm mainForm = null;
        //FormProjectDataMonitor frmParam;
        public FormParameter()
        {
            InitializeComponent();

        }
        public void UpdateDocInfo()
        {


        }

        private void FormParameter_Load(object sender, EventArgs e)
        {
            //frmParam = new FormProjectDataMonitor();
            //frmParam.TopLevel = false;
            //panelMain.Controls.Add(frmParam);
            //frmParam.Size = panelMain.Size;
            //frmParam.Show();
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
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }





    }
}
