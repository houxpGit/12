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
    public partial class FormHardwareMonitor : Form
    {
        private static FormHardwareMonitor form_HardwareMonitor;
        private static object locker = new object();
        public string chooseName = "";
        public static FormHardwareMonitor GetForm()
        {
            if (form_HardwareMonitor == null)
            {
                lock (locker)
                {
                    if (form_HardwareMonitor == null)
                    {
                        form_HardwareMonitor = new FormHardwareMonitor();
                    }
                }
            }
            return form_HardwareMonitor;
        }

        public MainForm mainForm = null;
        FormTableDriver frmTableDriver;
        FormIODriver frmIoDriver;
        private FormHardwareMonitor()
        {
            InitializeComponent();
        }
        public void UpdateDocInfo()
        {

        }

        private void FormIOMonitor_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, TableDriver> item in TableManage.tableDrivers.drivers)
            {
                comboBoxTables.Items.Add(item.Value.strDriverName);
            }
            if (comboBoxTables.Items.Count > 0)
            {
                comboBoxTables.SelectedIndex = 0;
                frmTableDriver = new FormTableDriver(TableManage.tableDrivers.drivers[comboBoxTables.Items[0].ToString()]);
                frmTableDriver.TopLevel = false;
                panelTable.Controls.Add(frmTableDriver);
                //frmTableDriver.Size = panelTable.Size;
                frmTableDriver.Dock = DockStyle.Fill;
                frmTableDriver.Show();
            }
            frmIoDriver = new FormIODriver();
            frmIoDriver.TopLevel = false;
            panelIO.Controls.Add(frmIoDriver);
            //frmIoDriver.Size = panelIO.Size;
            frmIoDriver.Dock = DockStyle.Fill;
            frmIoDriver.Show();
            timer1.Start();
            //this.Resize += new EventHandler(Main_Resize);

            //X = this.Width;
            //Y = this.Height;

            //setTag(this);
            //Main_Resize(new object(), new EventArgs());//x,y
            //this.MaximizeBox = true;
            //this.WindowState = FormWindowState.Maximized;
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
        private void comboBoxTables_SelectionChangeCommitted(object sender, EventArgs e)
        {
            frmTableDriver.tableDriver = TableManage.tableDrivers.drivers[comboBoxTables.Items[comboBoxTables.SelectedIndex].ToString()];
            frmTableDriver.UpdateData();
        }

        private void FormHardwareMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            form_HardwareMonitor = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chooseName=="1")
            {
                comboBoxTables.SelectedIndex = 0;
                chooseName = "";
            } 
        }
    }
}
