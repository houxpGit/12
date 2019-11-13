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
    public partial class FormTableItem : Form
    {
        TableData tbData;
        public FormAxisSetting frmAxisSettingX;
        public FormAxisSetting frmAxisSettingY;
        public FormAxisSetting frmAxisSettingZ;
        public FormAxisSetting frmAxisSettingU;
        public FormTableItem(TableData data)
        {
            InitializeComponent();
            tbData = data;
        }
        public FormTableItem()
        {
            InitializeComponent();

        }
        private void FormTableItem_Load(object sender, EventArgs e)
        {
            //label1.Text = tbData.strTableName;
            //frmAxisSettingX = new FormAxisSetting("X",tbData.axisXData);
            frmAxisSettingX = new FormAxisSetting("X");
            frmAxisSettingX.TopLevel = false;
            panel1.Controls.Add(frmAxisSettingX);
            //frmAxisSettingX.Size = panel1.Size;
            frmAxisSettingX.Dock = DockStyle.Fill;
            frmAxisSettingX.Show();

            //frmAxisSettingY = new FormAxisSetting("Y", tbData.axisYData);
            frmAxisSettingY = new FormAxisSetting("Y");
            frmAxisSettingY.TopLevel = false;
            panel1.Controls.Add(frmAxisSettingY);
            //frmAxisSettingY.Size = panel1.Size;
            frmAxisSettingY.Dock = DockStyle.Fill;
            frmAxisSettingY.Show();
            frmAxisSettingY.Hide();

            //frmAxisSettingZ = new FormAxisSetting("Z", tbData.axisZData);
            frmAxisSettingZ = new FormAxisSetting("Z");
            frmAxisSettingZ.TopLevel = false;
            panel1.Controls.Add(frmAxisSettingZ);
            //frmAxisSettingZ.Size = panel1.Size;
            frmAxisSettingZ.Dock = DockStyle.Fill;
            frmAxisSettingZ.Show();
            frmAxisSettingZ.Hide();

            //frmAxisSettingU = new FormAxisSetting("U", tbData.axisUData);
            frmAxisSettingU = new FormAxisSetting("U");
            frmAxisSettingU.TopLevel = false;
            panel1.Controls.Add(frmAxisSettingU);
            //frmAxisSettingU.Size = panel1.Size;
            frmAxisSettingU.Dock = DockStyle.Fill;
            frmAxisSettingU.Show();
            frmAxisSettingU.Hide();
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            frmAxisSettingX.Show();
            frmAxisSettingY.Hide();
            frmAxisSettingZ.Hide();
            frmAxisSettingU.Hide();
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            frmAxisSettingX.Hide();
            frmAxisSettingY.Show();
            frmAxisSettingZ.Hide();
            frmAxisSettingU.Hide();
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            frmAxisSettingX.Hide();
            frmAxisSettingY.Hide();
            frmAxisSettingZ.Show();
            frmAxisSettingU.Hide();
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            frmAxisSettingX.Hide();
            frmAxisSettingY.Hide();
            frmAxisSettingZ.Hide();
            frmAxisSettingU.Show();
        }
        public void UpdateItemData(TableData data)
        {
            tbData = data;
            label1.Text = tbData.strTableName;
            dataGridViewPosSetting.Rows.Clear();
            foreach (TablePosItem item in tbData.tablePosData.tablePosItemList)
            {
                dataGridViewPosSetting.Rows.Add(new object[] { item.strName, item.dPosX, item.dPosY, item.dPosZ, item.dPosU, item.bActionX, item.bActionY, item.bActionZ, item.bActionU, item.bRel });
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxPosName.Text == "")
            {
                return;
            }
            try
            {
                TablePosItem item = new TablePosItem();
                tbData.tablePosData.tablePosItemDictionary.Add(textBoxPosName.Text, item);
                tbData.tablePosData.tablePosItemList.Add(item);
                item.strName = textBoxPosName.Text;
                dataGridViewPosSetting.Rows.Add(new object[] { item.strName, item.dPosX, item.dPosY, item.dPosZ, item.dPosY, item.bActionX, item.bActionY, item.bActionZ, item.bActionU, item.bRel });
            }
            catch
            {

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewPosSetting.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridViewPosSetting.SelectedRows)
                {
                    tbData.tablePosData.tablePosItemDictionary.Remove(item.Cells[0].Value.ToString());
                    tbData.tablePosData.tablePosItemList.RemoveAt(item.Index);
                    dataGridViewPosSetting.Rows.Remove(item);
                }
                //string strName = dataGridViewPosSetting.SelectedRows[0].Cells[0].Value.ToString();
                //tbData.tablePosData.tablePosItemDictionary.Remove(strName);
                //tbData.tablePosData.tablePosItemList.RemoveAt(dataGridViewPosSetting.SelectedRows[0].Index);
                //dataGridViewPosSetting.Rows.Remove(dataGridViewPosSetting.SelectedRows[0]);
            }
        }

        private void dataGridViewPosSetting_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strName = dataGridViewPosSetting.Rows[e.RowIndex].Cells[0].Value.ToString();
                TablePosItem itemDis = tbData.tablePosData.tablePosItemDictionary[strName];
                TablePosItem itemList = tbData.tablePosData.tablePosItemList[e.RowIndex];
                if (e.ColumnIndex == 1)
                {
                    double dValue = double.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.dPosX = dValue;
                    itemList.dPosX = dValue;
                }
                if (e.ColumnIndex == 2)
                {
                    double dValue = double.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.dPosY = dValue;
                    itemList.dPosY = dValue;
                }
                if (e.ColumnIndex == 3)
                {
                    double dValue = double.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.dPosZ = dValue;
                    itemList.dPosZ = dValue;
                }
                if (e.ColumnIndex == 4)
                {
                    double dValue = double.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.dPosU = dValue;
                    itemList.dPosU = dValue;
                }
                if (e.ColumnIndex == 5)
                {
                    bool bValue = bool.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.bActionX = bValue;
                    itemList.bActionX = bValue;
                }
                if (e.ColumnIndex == 6)
                {
                    bool bValue = bool.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.bActionY = bValue;
                    itemList.bActionY = bValue;
                }
                if (e.ColumnIndex == 7)
                {
                    bool bValue = bool.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.bActionZ = bValue;
                    itemList.bActionZ = bValue;
                }
                if (e.ColumnIndex == 8)
                {
                    bool bValue = bool.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.bActionU = bValue;
                    itemList.bActionU = bValue;
                }
                if (e.ColumnIndex == 9)
                {
                    bool bValue = bool.Parse(dataGridViewPosSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    itemDis.bRel = bValue;
                    itemList.bRel = bValue;
                }
            }
            catch
            {

            }

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string strTempPath = @".//TempFile/";
            string strTempName = "Postion" + tbData.strTableName + ".cs";
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
    }
}
