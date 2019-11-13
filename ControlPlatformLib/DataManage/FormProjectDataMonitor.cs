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

    public partial class FormProjectDataMonitor : Form
    {
        FormProjectDataItemMonitor frmItem;

        public FormProjectDataMonitor()
        {
            InitializeComponent();
        }
        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {

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
                frmItem = new FormProjectDataItemMonitor(dataList);
                frmItem.TopLevel = false;
                panel1.Controls.Add(frmItem);
                frmItem.Size = panel1.Size;
                frmItem.Show();

            }
            else
            {
                frmItem = new FormProjectDataItemMonitor();
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

            string strGroupName = dataGridGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
            ProjectDataS dataList = DataManage.m_Doc.m_dataList[e.RowIndex];
            frmItem.UpdateData(dataList);
            frmItem.Show();

        }
    }
}
