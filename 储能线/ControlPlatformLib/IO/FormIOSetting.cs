using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ControlPlatformLib
{

    public partial class FormIOSetting : Form
    {
        //BindingSource inputSource;
        public FormIOSetting()
        {
            InitializeComponent();
            //inputSource = new BindingSource();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxInputName.Text == "")
            {
                return;
            }
            try
            {
                if (IOManage.IODoc.m_InputDictionary.ContainsKey(textBoxInputName.Text.Trim()))
                {
                    MessageBox.Show("已存在同名输入点位！");
                    return;
                }
                InputData data = new InputData();
                data.strIOName = textBoxInputName.Text;
                data.InputCardName = HardwareManage.hardwardDictionary.FirstOrDefault().Key.ToString();
                data.iInputNo = 0;
                IOManage.IODoc.m_InputDictionary.Add(textBoxInputName.Text, data);
                IOManage.IODoc.m_InputDataList.Add(data);
                InputDriver driver = new InputDriver();
                driver.Init(data);
                IOManage.InputDrivers.drivers.Add(data.strIOName, driver);
                DrawInput();

            }
            catch(Exception ex)
            {

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewInput.SelectedRows.Count == 1)
            {
                InputData data = dataGridViewInput.SelectedRows[0].DataBoundItem as InputData;
                
                IOManage.IODoc.m_InputDictionary.Remove(data.strIOName);
                IOManage.IODoc.m_InputDataList.Remove(data);
                DrawInput();
            }
        }

        private void dataGridViewInput_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dataGridViewInput_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
               
            }
        }

        void DrawInput()
        {
            dataGridViewInput.Columns.Clear();
            DataGridViewTextBoxColumn ioNameColumn = new DataGridViewTextBoxColumn();
            ioNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ioNameColumn.HeaderText = "输入点名称";
            ioNameColumn.DataPropertyName = "strIOName";

            DataGridViewComboBoxColumn cardNameColumn = new DataGridViewComboBoxColumn();
            foreach (KeyValuePair<string, HardWareBase> item in HardwareManage.hardwardDictionary)
            {
                if (item.Value is IInputAction)
                {
                    cardNameColumn.Items.Add(item.Key);
                }
            }
            cardNameColumn.HeaderText = "卡名称";
            cardNameColumn.DataPropertyName = "InputCardName";

            DataGridViewComboBoxColumn ioColumn = new DataGridViewComboBoxColumn();
            for (int i = 0; i < 64; i++)
            {
                ioColumn.Items.Add(i);
            }
            //ioColumn.Sorted = true;
            ioColumn.HeaderText = "点位";
            ioColumn.DataPropertyName = "iInputNo";

            DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            ignore.DataPropertyName = "bignore";
            ignore.HeaderText = "屏蔽";

            DataGridViewTextBoxColumn remark = new DataGridViewTextBoxColumn();
            remark.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            remark.HeaderText = "备注";
            remark.DataPropertyName = "strRemark";
            
            dataGridViewInput.Columns.Add(ioNameColumn);
            dataGridViewInput.Columns.Add(cardNameColumn);
            dataGridViewInput.Columns.Add(ioColumn);
            dataGridViewInput.Columns.Add(ignore);
            dataGridViewInput.Columns.Add(remark);

            if (IOManage.IODoc.m_InputDataList!=null&& IOManage.IODoc.m_InputDataList.Count>0)
            {
                dataGridViewInput.DataSource = IOManage.IODoc.m_InputDataList;
            }
        }
        void DrawOutput()
        {
            dataGridViewOutput.Columns.Clear();
            DataGridViewTextBoxColumn ioNameColumn = new DataGridViewTextBoxColumn();
            ioNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ioNameColumn.HeaderText = "输出点名称";
            ioNameColumn.DataPropertyName = "strIOName";

            DataGridViewComboBoxColumn cardNameColumn = new DataGridViewComboBoxColumn();
            foreach (KeyValuePair<string, HardWareBase> item in HardwareManage.hardwardDictionary)
            {
                if (item.Value is IInputAction)
                {
                    cardNameColumn.Items.Add(item.Key);
                }
            }
            cardNameColumn.HeaderText = "卡名称";
            cardNameColumn.DataPropertyName = "OutputCardName";

            DataGridViewComboBoxColumn ioColumn = new DataGridViewComboBoxColumn();
            for (int i = 0; i < 64; i++)
            {
                ioColumn.Items.Add(i);
            }
            ioColumn.HeaderText = "点位";
            ioColumn.DataPropertyName = "iOutputNo";

            DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            ignore.DataPropertyName = "bignore";
            ignore.HeaderText = "屏蔽";

            DataGridViewTextBoxColumn remark = new DataGridViewTextBoxColumn();
            remark.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            remark.HeaderText = "备注";
            remark.DataPropertyName = "strRemark";

            dataGridViewOutput.Columns.Add(ioNameColumn);
            dataGridViewOutput.Columns.Add(cardNameColumn);
            dataGridViewOutput.Columns.Add(ioColumn);
            dataGridViewOutput.Columns.Add(ignore);
            dataGridViewOutput.Columns.Add(remark);

            if (IOManage.IODoc.m_OutputDataList!=null&& IOManage.IODoc.m_OutputDataList.Count>0)
            {
                dataGridViewOutput.DataSource = IOManage.IODoc.m_OutputDataList;
            }
           
        }
        private void FormIOSetting_Load(object sender, EventArgs e)
        {
            DrawInput();
            DrawOutput();
        }
        private void buttonAddOutput_Click(object sender, EventArgs e)
        {
            if (textBoxOutputName.Text == "")
            {
                return;
            }
            try
            {
                OutputData data = new OutputData();
                data.strIOName = textBoxOutputName.Text.Trim();
                data.iOutputNo = 0;
                data.OutputCardName = "";
                IOManage.IODoc.m_OutputDictionary.Add(data.strIOName, data);
                IOManage.IODoc.m_OutputDataList.Add(data);
                DrawOutput();
            }
            catch
            {

            }
        }
        private void buttonRemoveOutput_Click(object sender, EventArgs e)
        {
            if (dataGridViewOutput.SelectedRows.Count == 1)
            {
                OutputData data = dataGridViewOutput.SelectedRows[0].DataBoundItem as OutputData;
                IOManage.IODoc.m_OutputDictionary.Remove(data.strIOName);
                IOManage.IODoc.m_OutputDataList.Remove(data);
                DrawOutput();
            }
        }
        private void dataGridViewOutput_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string strTempPath = @".//TempFile/";
            string strTempName = "InputName.cs";
            if (!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            string strConcent = MakeInputText();
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);

            strTempPath = @".//TempFile/";
            strTempName = "OutputName.cs";
            if (!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            strConcent = MakeOutputText();
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
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

        private void dataGridViewInput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridViewOutput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
