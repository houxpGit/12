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

        public void  DrawInput()
        {
            string PageName = tabControl1.SelectedTab.Name;
            dateGridList[GetPage(PageName)].Columns.Clear();
            DataGridViewTextBoxColumn ioNameColumn = new DataGridViewTextBoxColumn();
            ioNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ioNameColumn.HeaderText = "名称";
            ioNameColumn.DataPropertyName = "strName";

            DataGridViewTextBoxColumn Current_X = new DataGridViewTextBoxColumn();
            Current_X.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Current_X.HeaderText = "X";
            Current_X.DataPropertyName = "Current_X";

            DataGridViewTextBoxColumn Current_Y = new DataGridViewTextBoxColumn();
            Current_Y.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Current_Y.HeaderText = "Y";
            Current_Y.DataPropertyName = "Current_Y";

            DataGridViewTextBoxColumn Current_Z = new DataGridViewTextBoxColumn();
            Current_Z.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Current_Z.HeaderText = "Z";
            Current_Z.DataPropertyName = "Current_Z";

            DataGridViewTextBoxColumn Current_U = new DataGridViewTextBoxColumn();
            Current_U.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Current_U.HeaderText = "U";
            Current_U.DataPropertyName = "Current_U";

            DataGridViewTextBoxColumn 插补轴号 = new DataGridViewTextBoxColumn();
            插补轴号.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            插补轴号.HeaderText = "插补轴号";
            插补轴号.DataPropertyName = "插补轴号";

            DataGridViewButtonColumn 获取位置 = new DataGridViewButtonColumn();
            获取位置.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            获取位置.HeaderText = "获取位置";
            获取位置.DataPropertyName = "获取位置";

            DataGridViewButtonColumn AsixGo = new DataGridViewButtonColumn();
            AsixGo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            AsixGo.HeaderText = "AsixGo";
            AsixGo.DataPropertyName = "AsixGo";

            DataGridViewComboBoxColumn ioColumn = new DataGridViewComboBoxColumn();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
               ioColumn.Items.Add(IOManage.IODoc.m_InputDataList[i].strIOName);
            }

            DataGridViewCheckBoxColumn IO检测状态 = new DataGridViewCheckBoxColumn();
            IO检测状态.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            IO检测状态.HeaderText = "IO检测状态";
            IO检测状态.DataPropertyName = "IO检测状态";


            DataGridViewComboBoxColumn ioColumn1 = new DataGridViewComboBoxColumn();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                ioColumn1.Items.Add(IOManage.IODoc.m_InputDataList[i].strIOName);
            }


            DataGridViewCheckBoxColumn IO输出状态 = new DataGridViewCheckBoxColumn();
            IO输出状态.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            IO输出状态.HeaderText = "IO输出状态";
            IO输出状态.DataPropertyName = "IO输出状态";

            DataGridViewButtonColumn IO运动 = new DataGridViewButtonColumn();
            IO运动.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            IO运动.HeaderText = "IO运动";
            IO运动.DataPropertyName = "IO运动";

            dateGridList[GetPage(PageName)].Columns.Add(ioNameColumn);
            dateGridList[GetPage(PageName)].Columns.Add(Current_X);
            dateGridList[GetPage(PageName)].Columns.Add(Current_Y);
            dateGridList[GetPage(PageName)].Columns.Add(Current_Z);
            dateGridList[GetPage(PageName)].Columns.Add(Current_U);

            dateGridList[GetPage(PageName)].Columns.Add(插补轴号);
            dateGridList[GetPage(PageName)].Columns.Add(获取位置);
            dateGridList[GetPage(PageName)].Columns.Add(AsixGo);
            dateGridList[GetPage(PageName)].Columns.Add(ioColumn);

            dateGridList[GetPage(PageName)].Columns.Add(IO检测状态);
            dateGridList[GetPage(PageName)].Columns.Add(ioColumn1);
            dateGridList[GetPage(PageName)].Columns.Add(IO输出状态);
            dateGridList[GetPage(PageName)].Columns.Add(IO运动);

            if (IOManage.IODoc.m_InputDataList != null && IOManage.IODoc.m_InputDataList.Count > 0)
            {
                dateGridList[GetPage(PageName)].DataSource = IOManage.IODoc.m_InputDataList;
            }

            //DataGridViewComboBoxColumn ioColumn = new DataGridViewComboBoxColumn();
            //for (int i = 0; i < 64; i++)
            //{
            //    ioColumn.Items.Add(i);
            //}
            //ioColumn.Sorted = true;
            //ioColumn.HeaderText = "点位";
            //ioColumn.DataPropertyName = "iInputNo";

            //DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            //ignore.DataPropertyName = "bignore";
            //ignore.HeaderText = "屏蔽";

            //DataGridViewTextBoxColumn remark = new DataGridViewTextBoxColumn();
            //remark.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //remark.HeaderText = "备注";
            //remark.DataPropertyName = "strRemark";



        }
        //public List<FlowChar> m_InputDataList1;
        //public List<InputData> m_InputDataList;
     
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {

            string PageName = tabControl1.SelectedTab.Name;
            if (PageName != ""&& treeView1.SelectedNode.Parent.Nodes.Count != 0)
            {


                // DrawInput();

                string da = treeView1.SelectedNode.Text;
                //  treeView1.SelectedNode.
                dateGridList[GetPage(PageName)].Rows.Add(new object[] { da, "0.00", "0.00", "0.00", "0.00", "0.00", "Get", "Go" });
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    this.IO输入检测.Items.Add(IOManage.IODoc.m_InputDataList[i].strIOName);

                }
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    this.IO输出.Items.Add(IOManage.IODoc.m_OutputDataList[i].strIOName);

                }
                this.IO运动.Text = "Go";
               // IO检测Sta.TrueValue = true;
                IO检测Sta.FalseValue = 0;
                IO检测Sta.TrueValue = 1;
                IO输出STA.TrueValue = 1;
                IO输出STA.FalseValue = 0;
                //  dateGridList[GetPage(PageName)].Rows[1].Cells[9].Value = true;
                //  this.IO检测状态.
                // dateGridList[GetPage(PageName)].Rows[1].Cells[9].Value = true;



                //this.串口发送数据. = "T";
                //this.串口接受.HeaderText = "T";
                //this.网络发送.HeaderText = "T";
                //this.网络接受.HeaderText = "T";

                //this.IO输出.Items.Add("q1w");
                //this.IO输出.Items.Add("1qw");
                //this.IO输出.Items.Add("2qw");
                //this.IO输出.Items.Add("q3w");
                //for (int i = 0; i < 64; i++)
                //{
                //    dateGridList[GetPage(PageName)].Rows[1].Cells[9].Value = i;
                //    //dateGridList[GetPage(PageName)].ro.Items.Add(i);
                //}
            }

        }
    }
}
