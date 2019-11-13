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
    public partial class FormProjectData : Form
    {
        //public TableDriver tableDriver;
        public FormProjectData()
        {
            InitializeComponent();
            //tableDriver = driver;
        }
        public void UpdateData()
        {

        }

        private void timerScan_Tick(object sender, EventArgs e)
        {

            if (Visible == false)
                return;
            string strInputName = "";
            for (int i = 0; i < dataGridViewInput.Rows.Count; i++)
            {
                strInputName = dataGridViewInput.Rows[i].Cells[0].Value.ToString();
                bool bOn = IOManage.InputDrivers.drivers[strInputName].GetOn();
                //bOn = true;
                dataGridViewInput.Rows[i].Cells[1].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
            }
            string strOutputName = "";
            for (int i = 0; i < dataGridViewOutput.Rows.Count; i++)
            {
                strOutputName = dataGridViewOutput.Rows[i].Cells[0].Value.ToString();
                bool bOn = IOManage.OutputDrivers.drivers[strOutputName].GetOn();
                dataGridViewOutput.Rows[i].Cells[1].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
            }



        }

        private void FormIODriver_Load(object sender, EventArgs e)
        {
            foreach (InputData itemTemp in IOManage.IODoc.m_InputDataList)
            {
                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell IoNameCell = new DataGridViewTextBoxCell();
                IoNameCell.Value = itemTemp.strIOName;



                DataGridViewTextBoxCell CardONFFCell = new DataGridViewTextBoxCell();
                CardONFFCell.Value = "";

                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = itemTemp.strRemark;

                rowAdd.Cells.Add(IoNameCell);
                rowAdd.Cells.Add(CardONFFCell);
                rowAdd.Cells.Add(RemarkCell);
                dataGridViewInput.Rows.Add(rowAdd);
            }
            foreach (OutputData itemTemp in IOManage.IODoc.m_OutputDataList)
            {
                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell IoNameCell = new DataGridViewTextBoxCell();
                IoNameCell.Value = itemTemp.strIOName;



                DataGridViewTextBoxCell OnFFCell = new DataGridViewTextBoxCell();

                OnFFCell.Value = ""; ;

                DataGridViewButtonCell OnCell = new DataGridViewButtonCell();
                OnCell.Value = "On";

                DataGridViewButtonCell OffCell = new DataGridViewButtonCell();
                OffCell.Value = "Off";

                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = itemTemp.strRemark;

                rowAdd.Cells.Add(IoNameCell);
                rowAdd.Cells.Add(OnFFCell);
                rowAdd.Cells.Add(OnCell);
                rowAdd.Cells.Add(OffCell);
                rowAdd.Cells.Add(RemarkCell);
                dataGridViewOutput.Rows.Add(rowAdd);
            }
            timerScan.Enabled = true;
        }

        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strName = dataGridViewOutput.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (e.ColumnIndex == 2)
            {
                IOManage.OutputDrivers.drivers[strName].SetOutBit(true);
            }
            if (e.ColumnIndex == 3)
            {
                IOManage.OutputDrivers.drivers[strName].SetOutBit(false);
            }
        }
    }
}
