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
    public partial class FormIODriver : Form
    {
        //public TableDriver tableDriver;
        public FormIODriver()
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
            for (int i = 0; i < dataGridViewInput.Rows.Count; i++)
            {
                bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                //bOn = true;
                dataGridViewInput.Rows[i].Cells[0].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
            }
            for (int i = 0; i < dataGridViewOutput.Rows.Count; i++)
            {
                bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                dataGridViewOutput.Rows[i].Cells[0].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                dataGridViewOutput.Rows[i].Cells[1].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                dataGridViewOutput.Rows[i].Cells[1].Value = bOn ? "ON" : "OFF";
            }
        }
        void DrawInput()
        {
            dataGridViewInput.Columns.Clear();
            DataGridViewTextBoxColumn ioNameColumn = new DataGridViewTextBoxColumn();
            ioNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ioNameColumn.HeaderText = "输入点名称";
            ioNameColumn.DataPropertyName = "strIOName";

            DataGridViewTextBoxColumn onoffColumn = new DataGridViewTextBoxColumn();
            onoffColumn.HeaderText = "状态";

            DataGridViewTextBoxColumn cardNameColumn = new DataGridViewTextBoxColumn();
            cardNameColumn.Visible = false;
            cardNameColumn.HeaderText = "卡名称";
            cardNameColumn.DataPropertyName = "InputCardName";

            DataGridViewTextBoxColumn ioColumn = new DataGridViewTextBoxColumn();
            ioColumn.Visible = false;
            ioColumn.HeaderText = "点位";
            ioColumn.DataPropertyName = "iInputNo";

            DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            ignore.Visible = false;
            ignore.DataPropertyName = "bignore";
            ignore.HeaderText = "屏蔽";

            DataGridViewTextBoxColumn remark = new DataGridViewTextBoxColumn();
            remark.HeaderText = "备注";
            remark.DataPropertyName = "strRemark";

            dataGridViewInput.Columns.Add(ioNameColumn);
            dataGridViewInput.Columns.Add(onoffColumn);
            dataGridViewInput.Columns.Add(cardNameColumn); 
            dataGridViewInput.Columns.Add(ioColumn);
            dataGridViewInput.Columns.Add(ignore);
            dataGridViewInput.Columns.Add(remark);

            dataGridViewInput.DataSource = IOManage.IODoc.m_InputDataList;
        }

        void DrawOutput()
        {
            dataGridViewOutput.Columns.Clear();
            DataGridViewTextBoxColumn ioNameColumn = new DataGridViewTextBoxColumn();
            ioNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ioNameColumn.HeaderText = "输出点名称";
            ioNameColumn.DataPropertyName = "strIOName";

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.HeaderText = "状态";

            DataGridViewButtonColumn onoffColumn = new DataGridViewButtonColumn(); 
            onoffColumn.HeaderText = "ON/OFF";

            DataGridViewTextBoxColumn cardNameColumn = new DataGridViewTextBoxColumn();
            cardNameColumn.Visible = false;
            cardNameColumn.HeaderText = "卡名称";
            cardNameColumn.DataPropertyName = "OutputCardName";

            DataGridViewTextBoxColumn ioColumn = new DataGridViewTextBoxColumn();
            ioColumn.Visible = false;
            ioColumn.HeaderText = "点位";
            ioColumn.DataPropertyName = "iOutputNo";

            DataGridViewCheckBoxColumn ignore = new DataGridViewCheckBoxColumn();
            ignore.Visible = false;
            ignore.DataPropertyName = "bignore";
            ignore.HeaderText = "屏蔽";

            DataGridViewTextBoxColumn remark = new DataGridViewTextBoxColumn();
            remark.HeaderText = "备注";
            remark.DataPropertyName = "strRemark";

            dataGridViewOutput.Columns.Add(ioNameColumn);
            dataGridViewOutput.Columns.Add(statusColumn);
            dataGridViewOutput.Columns.Add(onoffColumn);
            //dataGridViewOutput.Columns.Add(on);
            //dataGridViewOutput.Columns.Add(off);
            dataGridViewOutput.Columns.Add(cardNameColumn);
            dataGridViewOutput.Columns.Add(ioColumn);
            dataGridViewOutput.Columns.Add(ignore);
            dataGridViewOutput.Columns.Add(remark);

            dataGridViewOutput.DataSource = IOManage.IODoc.m_OutputDataList;
        }
        private void FormIODriver_Load(object sender, EventArgs e)
        {
            DrawInput();
            DrawOutput();
            timerScan.Enabled = true;
        }

        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string strName = (dataGridViewOutput.Rows[e.RowIndex].DataBoundItem as OutputData).strIOName;
            if (e.ColumnIndex == 1)
            { 
                if (IOManage.OutputDrivers.drivers[strName].GetOn())
                {
                    IOManage.OutputDrivers.drivers[strName].SetOutBit(false);
                }
                else
                {
                    IOManage.OutputDrivers.drivers[strName].SetOutBit(true);
                }
            }
        }
    }
}
