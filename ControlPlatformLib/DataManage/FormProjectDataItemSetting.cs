using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ControlPlatformLib
{

    public partial class FormProjectDataItemSetting : Form
    {
        public ProjectDataS datas;
        public FormProjectDataItemSetting()
        {
            InitializeComponent();
        }

        public FormProjectDataItemSetting(ProjectDataS iDatas)
        {
            InitializeComponent();
            datas = iDatas;
        }



        private void FormIOSetting_Load(object sender, EventArgs e)
        {

        }




        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormProjectDataItemSetting_Load(object sender, EventArgs e)
        {
            foreach (ProjectDataType itemType in Enum.GetValues(typeof(ProjectDataType)))
            {
                comboBoxType.Items.Add(itemType);
            }
            if (comboBoxType.Items.Count > 0)
            {
                comboBoxType.SelectedIndex = 0;
            }
            if (datas == null)
            {

            }
            else
            {
                label3.Text = datas.strGroupName;
                foreach (ProjectDataBase item in datas.m_dataList)
                {
                    DataGridViewRow rowAdd = new DataGridViewRow();

                    DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                    NameCell.Value = item.strName;



                    DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                    foreach (ProjectDataType itemType in Enum.GetValues(typeof(ProjectDataType)))
                    {
                        DataTypeCell.Items.Add(itemType);
                    }

                    DataTypeCell.Value = item.dataType;


                    DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                    ValueCell.Value = item.objValue;


                    DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                    RemarkCell.Value = item.strRemark;

                    rowAdd.Cells.Add(NameCell);
                    rowAdd.Cells.Add(DataTypeCell);
                    rowAdd.Cells.Add(ValueCell);
                    rowAdd.Cells.Add(RemarkCell);
                    dataGridViewOutput.Rows.Add(rowAdd);
                }
            }
        }
        public void UpdateData(ProjectDataS iDatas)
        {
            if (iDatas == null)
            {

            }
            else
            {
                dataGridViewOutput.Rows.Clear();
                datas = iDatas;
                label3.Text = datas.strGroupName;
                foreach (ProjectDataBase item in datas.m_dataList)
                {
                    DataGridViewRow rowAdd = new DataGridViewRow();

                    DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                    NameCell.Value = item.strName;



                    DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                    foreach (ProjectDataType itemType in Enum.GetValues(typeof(ProjectDataType)))
                    {
                        DataTypeCell.Items.Add(itemType);
                    }

                    DataTypeCell.Value = item.dataType;


                    DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                    ValueCell.Value = item.objValue;


                    DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                    RemarkCell.Value = item.strRemark;

                    rowAdd.Cells.Add(NameCell);
                    rowAdd.Cells.Add(DataTypeCell);
                    rowAdd.Cells.Add(ValueCell);
                    rowAdd.Cells.Add(RemarkCell);
                    dataGridViewOutput.Rows.Add(rowAdd);
                }
            }
        }

        private void buttonItemAdd_Click(object sender, EventArgs e)
        {
            if (textBoxItemName.Text == "")
            {
                return;
            }
            try
            {
                ProjectDataBase data = new ProjectDataBase();
                datas.m_dataDictionary.Add(textBoxItemName.Text, data);
                datas.m_dataList.Add(data);
                data.strName = textBoxItemName.Text;

                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                NameCell.Value = data.strName;


                data.dataType = (ProjectDataType)(comboBoxType.SelectedIndex);
                DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                foreach (ProjectDataType itemType in Enum.GetValues(typeof(ProjectDataType)))
                {
                    DataTypeCell.Items.Add(itemType);
                }
                DataTypeCell.Value = data.dataType;

                if ((ProjectDataType)(comboBoxType.SelectedIndex) == ProjectDataType.STRING)
                {
                    data.objValue = "";
                }
                if ((ProjectDataType)(comboBoxType.SelectedIndex) == ProjectDataType.DOUBLE)
                {
                    data.objValue = 0.0;
                }
                if ((ProjectDataType)(comboBoxType.SelectedIndex) == ProjectDataType.INT)
                {
                    data.objValue = (int)0;
                }
                if ((ProjectDataType)(comboBoxType.SelectedIndex) == ProjectDataType.SHORT)
                {
                    data.objValue = (short)0;
                }
                if ((ProjectDataType)(comboBoxType.SelectedIndex) == ProjectDataType.BOOL)
                {
                    data.objValue = false;
                }

                DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                ValueCell.Value = data.objValue.ToString();


                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = data.strRemark;

                rowAdd.Cells.Add(NameCell);
                rowAdd.Cells.Add(DataTypeCell);
                rowAdd.Cells.Add(ValueCell);
                rowAdd.Cells.Add(RemarkCell);
                dataGridViewOutput.Rows.Add(rowAdd);

            }
            catch
            {

            }
        }

        private void buttonItemRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewOutput.SelectedRows.Count == 1)
            {
                string strItemName = dataGridViewOutput.SelectedRows[0].Cells[0].Value.ToString();
                datas.m_dataDictionary.Remove(strItemName);
                datas.m_dataList.RemoveAt(dataGridViewOutput.SelectedRows[0].Index);
                dataGridViewOutput.Rows.Remove(dataGridViewOutput.SelectedRows[0]);
            }
        }

        private void dataGridViewOutput_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridViewOutput_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string strItemName = dataGridViewOutput.Rows[e.RowIndex].Cells[0].Value.ToString();
                ProjectDataBase data = datas.m_dataList[e.RowIndex];

                data.objValue = dataGridViewOutput.Rows[e.RowIndex].Cells[2].Value;
                data.strRemark = dataGridViewOutput.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch
            {

            }


        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string strTempPath = @".//TempFile/";
            string strTempName = "Data" + datas.strGroupName + ".cs";
            if (!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            string strConcent = MakeTableText();
            System.IO.File.WriteAllText((strTempPath + strTempName), strConcent);
        }
        private string MakeTableText()
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
    }
}
