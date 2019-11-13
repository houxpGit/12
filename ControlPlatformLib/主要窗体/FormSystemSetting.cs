using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ControlPlatformLib
{
    public partial class FormSystemSetting : Form
    {
        public MainForm mainForm = null;

        private static FormSystemSetting form_SystemSetting;
        private static object locker = new object();

        public static FormSystemSetting GetForm()
        {
            if (form_SystemSetting == null)
            {
                lock (locker)
                {
                    if (form_SystemSetting == null)
                    {
                        form_SystemSetting = new FormSystemSetting();
                    }
                }
            }
            return form_SystemSetting;
        }

        private FormSystemSetting()
        {
            InitializeComponent();
        }
        public void UpdateDocInfo()
        {

        }

        private void FormSystemSetting_Load(object sender, EventArgs e)
        {
            HardwareManage.frmHardwareSetting = new FormHardSetting();
            HardwareManage.frmHardwareSetting.TopLevel = false;
            panelMain.Controls.Add(HardwareManage.frmHardwareSetting);
            //HardwareManage.frmHardwareSetting.Size = panelMain.Size;
            HardwareManage.frmHardwareSetting.Dock = DockStyle.Fill;
            HardwareManage.frmHardwareSetting.Show();

            TableManage.frmTableSetting = new FormTableSetting();
            TableManage.frmTableSetting.TopLevel = false;
            panelMain.Controls.Add(TableManage.frmTableSetting);
            //TableManage.frmTableSetting.Size = panelMain.Size;
            TableManage.frmTableSetting.Dock = DockStyle.Fill;
            TableManage.frmTableSetting.Hide();

            IOManage.frmIoSetting = new FormIOSetting();
            IOManage.frmIoSetting.TopLevel = false;
            panelMain.Controls.Add(IOManage.frmIoSetting);
            IOManage.frmIoSetting.Dock = DockStyle.Fill;
            //IOManage.frmIoSetting.Size = panelMain.Size;
            IOManage.frmIoSetting.Hide();

            DataManage.frmProjectData = new FormProjectDataSetting();
            DataManage.frmProjectData.TopLevel = false;
            panelMain.Controls.Add(DataManage.frmProjectData);
            DataManage.frmProjectData.Dock = DockStyle.Fill;
            //DataManage.frmProjectData.Size = panelMain.Size;
            DataManage.frmProjectData.Hide();


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
        private void buttonHardware_Click(object sender, EventArgs e)
        {
            HardwareManage.frmHardwareSetting.Show();
            TableManage.frmTableSetting.Hide();
            IOManage.frmIoSetting.Hide();
            DataManage.frmProjectData.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HardwareManage.frmHardwareSetting.Hide();
            TableManage.frmTableSetting.Show();
            IOManage.frmIoSetting.Hide();
            DataManage.frmProjectData.Hide();
        }

        private void buttonIO_Click(object sender, EventArgs e)
        {
            HardwareManage.frmHardwareSetting.Hide();
            TableManage.frmTableSetting.Hide();
            IOManage.frmIoSetting.Show();
            DataManage.frmProjectData.Hide();
        }

        private void buttonParam_Click(object sender, EventArgs e)
        {
            HardwareManage.frmHardwareSetting.Hide();
            TableManage.frmTableSetting.Hide();
            IOManage.frmIoSetting.Hide();
            DataManage.frmProjectData.Show();
        }

        private void buttonExportAll_Click(object sender, EventArgs e)
        {
            //文件夹创建
            string strTempPath = @".//TempFile/";
            string strTempName = "TableName.cs";
            if (!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            //TableName
            string strConcent = MakeTableText();
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
            //TablePosition
            foreach (TableData item in TableManage.tablesDoc.m_tableDataList)
            {
                strConcent = MakeTablePosText(item);
                strTempName = "Postion" + item.strTableName + ".cs";
                System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
            }
            //Input
            strConcent = MakeInputText();
            strTempName = "InputName.cs";
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
            //output
            strConcent = MakeOutputText();
            strTempName = "OutputName.cs";
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
            //DataGroup
            strConcent = MakeDataGroupText();
            strTempName = "DataGroup.cs";
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);

            //DataItem
            foreach (ProjectDataS item in DataManage.m_Doc.m_dataList)
            {
                strConcent = MakeDataItemText(item);
                strTempName = "Data" + item.strGroupName + ".cs";
                System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
            }
        }
        private string MakeTableText()
        {
            string strReturn = "";
            if (TableManage.tablesDoc.m_tableDataList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class TableName\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (TableData item in TableManage.tablesDoc.m_tableDataList)
            {
                strItemName = item.strTableName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }
        private string MakeTablePosText(TableData tbData)
        {
            string strReturn = "";
            if (tbData.tablePosData.tablePosItemList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class " + "Postion" + tbData.strTableName + "\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (TablePosItem item in tbData.tablePosData.tablePosItemList)
            {
                strItemName = item.strName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }
        private string MakeInputText()
        {
            string strReturn = "";
            if (IOManage.IODoc.m_InputDataList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class InputName\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (InputData item in IOManage.IODoc.m_InputDataList)
            {
                strItemName = item.strIOName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }
        private string MakeOutputText()
        {
            string strReturn = "";
            if (IOManage.IODoc.m_OutputDataList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class OutputName\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (OutputData item in IOManage.IODoc.m_OutputDataList)
            {
                strItemName = item.strIOName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }
        private string MakeDataGroupText()
        {
            string strReturn = "";
            if (DataManage.m_Doc.m_dataList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class DataGroup\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (ProjectDataS item in DataManage.m_Doc.m_dataList)
            {
                strItemName = item.strGroupName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }
        private string MakeDataItemText(ProjectDataS datas)
        {
            string strReturn = "";
            if (datas.m_dataList.Count < 1)
                return strReturn;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace ControlPlatformLib\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class " + "Data" + datas.strGroupName + "\r\n";
            strTempHeader += "   {\r\n";
            strReturn = strTempHeader;
            string strItemHeader = "      public static string ";// WEIDONGYUAN = "weidongyuan";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = " ;\r\n";
            foreach (ProjectDataBase item in datas.m_dataList)
            {
                strItemName = item.strName;
                strValue = "\"" + strItemName + "\"";
                strReturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "   }\r\n";
            strTempEnd += "}\r\n";
            strReturn += strTempEnd;
            return strReturn;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            HardwareManage.hardDoc.SaveDoc();
            TableManage.tablesDoc.SaveDoc();
            IOManage.IODoc.SaveDoc();
            DataManage.m_Doc.SaveDoc();
        }

        private void FormSystemSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            form_SystemSetting = null;
        }
    }
}
