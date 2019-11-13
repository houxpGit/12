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

    public partial class FormProjectDataItemMonitor : Form
    {
        public ProjectDataS datas;
        public FormProjectDataItemMonitor()
        {
            InitializeComponent();
        }

        public FormProjectDataItemMonitor(ProjectDataS iDatas)
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
    }
}
