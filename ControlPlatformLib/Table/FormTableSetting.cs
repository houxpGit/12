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

    public partial class FormTableSetting : Form
    {
        FormTableItem frmTableItem;
        public FormTableSetting()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxTableName.Text == "")
            {
                return;
            }
            try
            {
                TableData table = new TableData();
                TableManage.tablesDoc.m_tableDictionary.Add(textBoxTableName.Text, table);
                TableManage.tablesDoc.m_tableDataList.Add(table);
                table.strTableName = textBoxTableName.Text;
                listViewNFTable.Items.Add(table.strTableName);
            }
            catch
            {

            }
        }

        private void FormHardSetting_Load(object sender, EventArgs e)
        {
            foreach (TableData item in TableManage.tablesDoc.m_tableDataList)
            {
                ListViewItem lvi = listViewNFTable.Items.Add(item.strTableName);
            }
            frmTableItem = new FormTableItem();
            frmTableItem.TopLevel = false;
            panel1.Controls.Add(frmTableItem);
            //frmTableItem.Size = panel1.Size;
            frmTableItem.Dock = DockStyle.Fill;
            frmTableItem.Show();
            frmTableItem.Hide();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewNFTable.SelectedItems.Count > 0)
            {
                TableManage.tablesDoc.m_tableDictionary.Remove(listViewNFTable.SelectedItems[0].Text);
                TableManage.tablesDoc.m_tableDataList.RemoveAt(listViewNFTable.SelectedItems[0].Index);
                listViewNFTable.SelectedItems[0].Remove();
                //if (panel1.Controls.Count > 0)
                //{
                //    panel1.Controls.RemoveAt(0);
                //}
                frmTableItem.Hide();
            }
        }

        private void listViewNFHardWare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNFTable.SelectedItems.Count > 0)
            {
                TableManage.LoadData();
                TableData tbData = TableManage.tablesDoc.m_tableDictionary[listViewNFTable.Items[listViewNFTable.SelectedItems[0].Index].Text];
                frmTableItem.UpdateItemData(tbData);
                frmTableItem.frmAxisSettingX.UpdateData(tbData.axisXData);
                frmTableItem.frmAxisSettingY.UpdateData(tbData.axisYData);
                frmTableItem.frmAxisSettingZ.UpdateData(tbData.axisZData);
                frmTableItem.frmAxisSettingU.UpdateData(tbData.axisUData);
                frmTableItem.Show();

            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string strTempPath = @".//TempFile/";
            string strTempName = "TableName.cs";
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

    }
}
