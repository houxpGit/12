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

    public partial class FormProjectDataSetting : Form
    {
        FormProjectDataItemSetting frmItem;

        public FormProjectDataSetting()
        {
            InitializeComponent();
        }
        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            if (textBoxGroupName.Text == "")
            {
                return;
            }
            try
            {
                ProjectDataS data = new ProjectDataS();
                DataManage.m_Doc.m_dataDictionary.Add(textBoxGroupName.Text, data);
                DataManage.m_Doc.m_dataList.Add(data);
                data.strGroupName = textBoxGroupName.Text;

                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell IoNameCell = new DataGridViewTextBoxCell();
                IoNameCell.Value = data.strGroupName;

                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = data.strRemark;

                rowAdd.Cells.Add(IoNameCell);
                rowAdd.Cells.Add(RemarkCell);
                dataGridGroup.Rows.Add(rowAdd);

            }
            catch
            {

            }
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            if (dataGridGroup.SelectedRows.Count == 1)
            {
                string strGroupName = dataGridGroup.SelectedRows[0].Cells[0].Value.ToString();
                DataManage.m_Doc.m_dataDictionary.Remove(strGroupName);
                DataManage.m_Doc.m_dataList.RemoveAt(dataGridGroup.SelectedRows[0].Index);
                dataGridGroup.Rows.Remove(dataGridGroup.SelectedRows[0]);
                frmItem.Hide();
            }
        }

        private void dataGridGroup_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string strGroupName = dataGridGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
            ProjectDataS dataDis = DataManage.m_Doc.m_dataDictionary[strGroupName];
            ProjectDataS dataList = DataManage.m_Doc.m_dataList[e.RowIndex];
            dataDis.strRemark = dataGridGroup.Rows[e.RowIndex].Cells[1].Value.ToString();
            dataList.strRemark = dataGridGroup.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void FormProjectDataSetting_Load(object sender, EventArgs e)
        {
            foreach (ProjectDataS item in DataManage.m_Doc.m_dataList)
            {
                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell IoNameCell = new DataGridViewTextBoxCell();
                IoNameCell.Value = item.strGroupName;

                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = item.strRemark;

                rowAdd.Cells.Add(IoNameCell);
                rowAdd.Cells.Add(RemarkCell);
                dataGridGroup.Rows.Add(rowAdd);
            }
            if (dataGridGroup.Rows.Count > 0)
            {
                string strGroupName = dataGridGroup.Rows[0].Cells[0].Value.ToString();
                ProjectDataS dataList = DataManage.m_Doc.m_dataList[0];
                frmItem = new FormProjectDataItemSetting(dataList);
                frmItem.TopLevel = false;
                panel1.Controls.Add(frmItem);
                frmItem.Size = panel1.Size;
                frmItem.Show();

            }
            else
            {
                frmItem = new FormProjectDataItemSetting();
                frmItem.TopLevel = false;
                panel1.Controls.Add(frmItem);
                frmItem.Size = panel1.Size;
                frmItem.Show();
                frmItem.Hide();
            }
        }

        private void dataGridGroup_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string strGroupName = dataGridGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
            ProjectDataS dataList = DataManage.m_Doc.m_dataList[e.RowIndex];
            frmItem.UpdateData(dataList);
            frmItem.Show();
        }

        private void dataGridGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                string strGroupName = dataGridGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
                ProjectDataS dataList = DataManage.m_Doc.m_dataList[e.RowIndex];
                frmItem.UpdateData(dataList);
                frmItem.Show();
            }
            catch
            {
            }

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string strTempPath = @".//TempFile/";
            string strTempName = "DataGroup.cs";
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
    }
}
