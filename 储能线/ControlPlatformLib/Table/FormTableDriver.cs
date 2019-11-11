using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlPlatformLib.Models;
using System.Drawing.Drawing2D;
using System.IO;
using ControlPlatformLib.CommonTools;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Data;
using WorldGeneralLib;
using System.Xml.Serialization;

namespace ControlPlatformLib
{
    public enum InterpolationType
    {
        圆弧,
        直线
    }

    public partial class FormTableDriver : Form
    {
        public TableDriver tableDriver;
        object objLock = new object();
        private List<TablePosItem> listEnd;
        private List<TablePosItem> listStart;
        private List<TablePosItem> listMid;
        private Canvas canvas;
        private Canvas preWeldingCanvas;
        private bool bCanXYMove=true;

        public FormTableDriver(TableDriver driver)
        {
            InitializeComponent();
            canvas = new Canvas();
            //canvas.PreviewMouseWheel += CanvasMouseWheel;
            //canvas.Width = this.canvasHost.Width;
            //canvas.Height = this.canvasHost.Height;
            //this.canvasHost.Child = canvas;

            //preWeldingCanvas = new Canvas();
            //preWeldingCanvas.Width = this.preWeldingHost.Width;
            //preWeldingCanvas.Height = this.preWeldingHost.Height;
            //this.preWeldingHost.Child = preWeldingCanvas;
            
            tableDriver = driver;
            listEnd = new List<TablePosItem>();
            listStart = new List<TablePosItem>();
            listMid = new List<TablePosItem>();

            Thread thread = new Thread(TestWeldingPath);
            thread.IsBackground = true;
            thread.Start();
        }

        private void FormTableDriver_Load(object sender, EventArgs e)
        {

            //foreach (KeyValuePair<string, TableDriver> item in TableManage.tableDrivers.drivers)
            //{

            //}
            //scaling = 25.4;
            scaling = 500;
            //this.dgv_Path.AutoGenerateColumns = false;

            dataGridViewAxisStatus.Rows.Add(new object[] { "报警", " ", " ", " ", " " });
            dataGridViewAxisStatus.Rows.Add(new object[] { "正极限", " ", " ", " ", " " });
            dataGridViewAxisStatus.Rows.Add(new object[] { "原点", " ", " ", " ", " " });
            dataGridViewAxisStatus.Rows.Add(new object[] { "负极限", " ", " ", " ", " " });
            dataGridViewAxisStatus.Rows.Add(new object[] { "运动状态", " ", " ", " ", " " });

            UpdateData();
            comboBoxMoveMode.SelectedIndex = 0;
            comboBoxMoveSpeed.SelectedIndex = 0;

            cmb_InterpolationType.DataSource = Enum.GetNames(typeof(InterpolationType));
            cmb_StartPos.DisplayMember = "strName";
            cmb_EndPos.DisplayMember = "strName";
            //cmb_Start.DisplayMember = "strName";
            //cmb_MidPos.DisplayMember = "strName";
            //cmb_End.DisplayMember = "strName";
            //cmb_PathSelect.DisplayMember = "Name";

            //cmb_PathType.DataSource = Enum.GetNames(typeof(InterpolationType));

            timerScan.Enabled = true;

        }

        #region UpdateData
        public void UpdateData()
        {
            labelTableName.Text = tableDriver.strDriverName;
            dataGridViewPosSetting.Rows.Clear();
            foreach (KeyValuePair<string, TablePosItem> item in tableDriver.tableData.tablePosData.tablePosItemDictionary)
            {
                dataGridViewPosSetting.Rows.Add(new object[] { item.Value.strName, item.Value.dPosX, item.Value.dPosY, item.Value.dPosZ, item.Value.dPosU, "Get", "Go" });
            }
            listEnd.Clear();
            listStart.Clear();
            listMid.Clear();
            foreach (var item in tableDriver.tableData.tablePosData.tablePosItemList)
            {
                listEnd.Add(item);
                listStart.Add(item);
                listMid.Add(item);
            }

            cmb_StartPos.DataSource = listStart;
            cmb_EndPos.DataSource = listEnd;

            //cmb_Start.DataSource = listStart;
            //cmb_MidPos.DataSource = listMid;
            //cmb_End.DataSource = listEnd;

            //cmb_WeldingStart.DataSource = listStart;
            //cmb_WeldingEnd.DataSource = listEnd;
            lb_XAlias.Text = tableDriver.tableData.axisXData.alias;
            lb_YAlias.Text = tableDriver.tableData.axisYData.alias;
            lb_ZAlias.Text = tableDriver.tableData.axisZData.alias;
            lb_UAlias.Text = tableDriver.tableData.axisUData.alias;

            //cmb_PathSelect.DataSource = PathDataManage.pathDoc.m_pathDataDriverList;
            if (tableDriver.tableData.axisXData.iUsed == 0)
            {
                buttonXCCW.Visible = false;
                buttonXCW.Visible = false;
                buttonXHome.Visible = false;
                lb_XAlias.Visible = false;
            }
            else
            {
                buttonXCCW.Visible = true;
                buttonXCW.Visible = true;
                buttonXHome.Visible = true;
                lb_XAlias.Visible = true;
            }
            if (tableDriver.tableData.axisYData.iUsed == 0)
            {
                buttonYCCW.Visible = false;
                buttonYCW.Visible = false;
                buttonYHome.Visible = false;
                lb_YAlias.Visible = false;
            }
            else
            {
                buttonYCCW.Visible = true;
                buttonYCW.Visible = true;
                buttonYHome.Visible = true;
                lb_YAlias.Visible = true;
            }
            if (tableDriver.tableData.axisZData.iUsed == 0)
            {
                buttonZCCW.Visible = false;
                buttonZCW.Visible = false;
                buttonZHome.Visible = false;
                lb_ZAlias.Visible = false;
            }
            else
            {
                buttonZCCW.Visible = true;
                buttonZCW.Visible = true;
                buttonZHome.Visible = true;
                lb_ZAlias.Visible = true;
            }
            if (tableDriver.tableData.axisUData.iUsed == 0)
            {
                buttonUCCW.Visible = false;
                buttonUCW.Visible = false;
                buttonUHome.Visible = false;
                lb_UAlias.Visible = false;
            }
            else
            {
                buttonUCCW.Visible = true;
                buttonUCW.Visible = true;
                buttonUHome.Visible = true;
                lb_UAlias.Visible = true;
            }
            dataGridViewMovePara.Rows.Clear();
            dataGridViewMovePara.Rows.Add(new object[] { "X",tableDriver.tableData.axisXData.dAcc,
                                                        tableDriver.tableData.axisXData.dDec,
                                                        tableDriver.tableData.axisXData.dSpeed,
                                                        tableDriver.tableData.axisXData.dJobLow,
                                                        tableDriver.tableData.axisXData.dJobHigh,
                                                        tableDriver.tableData.axisXData.dLimtSpd,
                                                        tableDriver.tableData.axisXData.dOrgSpd});
            dataGridViewMovePara.Rows.Add(new object[] { "Y",tableDriver.tableData.axisYData.dAcc,
                                                        tableDriver.tableData.axisYData.dDec,
                                                        tableDriver.tableData.axisYData.dSpeed,
                                                        tableDriver.tableData.axisYData.dJobLow,
                                                        tableDriver.tableData.axisYData.dJobHigh,
                                                        tableDriver.tableData.axisYData.dLimtSpd,
                                                        tableDriver.tableData.axisYData.dOrgSpd});
            dataGridViewMovePara.Rows.Add(new object[] { "Z",tableDriver.tableData.axisZData.dAcc,
                                                        tableDriver.tableData.axisZData.dDec,
                                                        tableDriver.tableData.axisZData.dSpeed,
                                                        tableDriver.tableData.axisZData.dJobLow,
                                                        tableDriver.tableData.axisZData.dJobHigh,
                                                        tableDriver.tableData.axisZData.dLimtSpd,
                                                        tableDriver.tableData.axisZData.dOrgSpd});
            dataGridViewMovePara.Rows.Add(new object[] { "U",tableDriver.tableData.axisUData.dAcc,
                                                        tableDriver.tableData.axisUData.dDec,
                                                        tableDriver.tableData.axisUData.dSpeed,
                                                        tableDriver.tableData.axisUData.dJobLow,
                                                        tableDriver.tableData.axisUData.dJobHigh,
                                                        tableDriver.tableData.axisUData.dLimtSpd,
                                                        tableDriver.tableData.axisUData.dOrgSpd});
        } 
        #endregion

        private void dataGridViewPosSetting_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strName = dataGridViewPosSetting.Rows[e.RowIndex].Cells[0].Value.ToString();
                TablePosItem itemDis = tableDriver.tableData.tablePosData.tablePosItemDictionary[strName];
                TablePosItem itemList = tableDriver.tableData.tablePosData.tablePosItemList[e.RowIndex];
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

                int count = 0;
                foreach (TablePosItem item in tableDriver.tableData.tablePosData.tablePosItemList)
                {

                    if (item.strName == itemDis.strName)
                    {
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosX = item.dPosX;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosY = item.dPosY;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosZ = item.dPosZ;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosU = item.dPosU;
                        //item.dPosY
                        //     item.dPosZ
                        //item.dPosU
                    }
                    count++;
                }
                cmb_StartPos.DataSource = tableDriver.tableData.tablePosData.tablePosItemList;
                cmb_EndPos.DataSource = tableDriver.tableData.tablePosData.tablePosItemList;
            }
            catch
            {

            }
        }

        private void dataGridViewMovePara_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string strName = tableDriver.strDriverName;
            string strAxisName = dataGridViewMovePara.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (strAxisName == "X")
            {
                if (e.ColumnIndex == 1)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dAcc = dValue;
                }
                if (e.ColumnIndex == 2)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dDec = dValue;
                }
                if (e.ColumnIndex == 3)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dSpeed = dValue;
                }
                if (e.ColumnIndex == 4)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dJobLow = dValue;
                }
                if (e.ColumnIndex == 5)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dJobHigh = dValue;
                }
                if (e.ColumnIndex == 6)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dLimtSpd = dValue;
                }
                if (e.ColumnIndex == 7)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisXData.dOrgSpd = dValue;
                }
            }
            if (strAxisName == "Y")
            {
                if (e.ColumnIndex == 1)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dAcc = dValue;
                }
                if (e.ColumnIndex == 2)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dDec = dValue;
                }
                if (e.ColumnIndex == 3)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dSpeed = dValue;
                }
                if (e.ColumnIndex == 4)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dJobLow = dValue;
                }
                if (e.ColumnIndex == 5)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dJobHigh = dValue;
                }
                if (e.ColumnIndex == 6)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dLimtSpd = dValue;
                }
                if (e.ColumnIndex == 7)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisYData.dOrgSpd = dValue;
                }
            }
            if (strAxisName == "Z")
            {
                if (e.ColumnIndex == 1)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dAcc = dValue;
                }
                if (e.ColumnIndex == 2)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dDec = dValue;
                }
                if (e.ColumnIndex == 3)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dSpeed = dValue;
                }
                if (e.ColumnIndex == 4)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dJobLow = dValue;
                }
                if (e.ColumnIndex == 5)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dJobHigh = dValue;
                }
                if (e.ColumnIndex == 6)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dLimtSpd = dValue;
                }
                if (e.ColumnIndex == 7)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisZData.dOrgSpd = dValue;
                }
            }
            if (strAxisName == "U")
            {
                if (e.ColumnIndex == 1)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dAcc = dValue;
                }
                if (e.ColumnIndex == 2)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dDec = dValue;
                }
                if (e.ColumnIndex == 3)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dSpeed = dValue;
                }
                if (e.ColumnIndex == 4)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dJobLow = dValue;
                }
                if (e.ColumnIndex == 5)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dJobHigh = dValue;
                }
                if (e.ColumnIndex == 6)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dLimtSpd = dValue;
                }
                if (e.ColumnIndex == 7)
                {
                    double dValue = double.Parse(dataGridViewMovePara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tableDriver.tableData.axisUData.dOrgSpd = dValue;
                }
            }
        }
        int s = 0;
        private void timerScan_Tick(object sender, EventArgs e)
        {

           
   



            if (this.Visible == false)
            {
                return;
            }
            double dPosX = 0, dPosY = 0, dPosZ = 0, dPosU = 0;
            bool bAlarmX = false, bAlarmY = false, bAlarmZ = false, bAlarmU = false;
            bool bCWLX = false, bCWLY = false, bCWLZ = false, bCWLU = false;
            bool bOrgX = false, bOrgY = false, bOrgZ = false, bOrgU = false;
            bool bCCWLX = false, bCCWLY = false, bCCWLZ = false, bCCWLU = false;
            bool bMovX = false, bMovY = false, bMovZ = false, bMovU = false;
            tableDriver.GetTableStatus(ref dPosX, ref bAlarmX, ref bCWLX, ref bOrgX, ref bCCWLX, ref bMovX,
                                        ref dPosY, ref bAlarmY, ref bCWLY, ref bOrgY, ref bCCWLY, ref bMovY,
                                        ref dPosZ, ref bAlarmZ, ref bCWLZ, ref bOrgZ, ref bCCWLZ, ref bMovZ,
                                        ref dPosU, ref bAlarmU, ref bCWLU, ref bOrgU, ref bCCWLU, ref bMovU);
            labelCurrentPosX.Text = dPosX.ToString("0.000");
            labelCurrentPosY.Text = dPosY.ToString("0.000");
            labelCurrentPosZ.Text = dPosZ.ToString("0.000");
            labelCurrentPosU.Text = dPosU.ToString("0.000");

            //if (tableDriver.tableData.tablePosData.tablePosItemDictionary.ContainsKey("Z轴安全位"))
            //{
            //    if (dPosZ < tableDriver.tableData.tablePosData.tablePosItemDictionary["Z轴安全位"].dPosZ)
            //    {
            //        bCanXYMove = true;
            //    }
            //    else
            //    {
            //        bCanXYMove = false;
            //    }
            //}
            //if (cureSatrt)
            //{
            //    pointList.Add(new PointF() { X = (float)dPosX, Y = (float)dPosY });
            //    if (dPosX == 100 & dPosY == 100)
            //    {
            //        cureSatrt = false;
            //    }

            //    if (pointList.Count == 3)
            //    {
            //        Graphics g = pb_Route.CreateGraphics();
            //        PointF[] arr = pointList.ToArray();

            //        PointF[] bezierPoint = Bezier.draw_bezier_curves(arr, pointList.Count, 0.01f);// 二阶贝塞尔曲线  
            //        foreach (PointF item in bezierPoint)
            //        {
            //            PointF p = new PointF();
            //            p.X = item.X + (float)(pb_Route.Width / 2.0);
            //            p.Y = item.Y + (float)(pb_Route.Width / 2.0);
            //            // 绘制曲线点  
            //            // 下面是C#绘制到Panel画板控件上的代码  
            //            g.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Green), new RectangleF(p, new SizeF(2, 2)));
            //        }

            //        pointList.Clear();
            //    }
            //}
            dataGridViewAxisStatus.Rows[0].Cells[1].Style.BackColor = bAlarmX ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[1].Cells[1].Style.BackColor = bCWLX ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[2].Cells[1].Style.BackColor = bOrgX ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[3].Cells[1].Style.BackColor = bCCWLX ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[4].Cells[1].Style.BackColor = bMovX ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);

            dataGridViewAxisStatus.Rows[0].Cells[2].Style.BackColor = bAlarmY ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[1].Cells[2].Style.BackColor = bCWLY ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[2].Cells[2].Style.BackColor = bOrgY ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[3].Cells[2].Style.BackColor = bCCWLY ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[4].Cells[2].Style.BackColor = bMovY ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);

            dataGridViewAxisStatus.Rows[0].Cells[3].Style.BackColor = bAlarmZ ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[1].Cells[3].Style.BackColor = bCWLZ ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[2].Cells[3].Style.BackColor = bOrgZ ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[3].Cells[3].Style.BackColor = bCCWLZ ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[4].Cells[3].Style.BackColor = bMovZ ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);

            dataGridViewAxisStatus.Rows[0].Cells[4].Style.BackColor = bAlarmU ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[1].Cells[4].Style.BackColor = bCWLU ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[2].Cells[4].Style.BackColor = bOrgU ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[3].Cells[4].Style.BackColor = bCCWLU ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            dataGridViewAxisStatus.Rows[4].Cells[4].Style.BackColor = bMovU ? System.Drawing.Color.Green : System.Drawing.Color.FromKnownColor(KnownColor.Control);
            if (s == 0)
            {
                int Width = Screen.PrimaryScreen.WorkingArea.Width;
                int Height = Screen.PrimaryScreen.WorkingArea.Height;
                int wih = MainModule.FormMain.Width;
                int aq1e = panel1.Width;
                int aqe = panel1.Height;

                panel1.SetBounds(panel1.Top, panel1.Left, Width, panel1.Height);
                s = 1;

                tab_control.SetBounds(panel1.Top, panel1.Left, Width- btn_PreWelding.Width- tab_control.Left, panel1.Height-20);

            }

        }
       
        private void buttonYCW_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    return;
            //}
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            string strButtonText = btn.Text;
            double dPos = double.Parse(comboBoxMoveDis.Text);
            #region X
            if (strButtonText == "X+" && bCanXYMove)
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.X, true, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.X, true, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.X, dPos, false);
                }
            }
            else if (strButtonText == "X+" && !bCanXYMove)
            {
                MessageBox.Show("请将Z轴移动到安全位置！");
            }
            if (strButtonText == "X-" && bCanXYMove)
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.X, false, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.X, false, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.X, -dPos, false);
                }
            }
            else if (strButtonText == "X-" && !bCanXYMove)
            {
                MessageBox.Show("请将Z轴移动到安全位置！");
            }
            #endregion
            #region Y
            if (strButtonText == "Y+" && bCanXYMove)
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.Y, true, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.Y, true, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.Y, dPos, false);
                }
            }
            else if (strButtonText == "Y+" && !bCanXYMove)
            {
                MessageBox.Show("请将Z轴移动到安全位置！");
            }
            if (strButtonText == "Y-" && bCanXYMove)
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.Y, false, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.Y, false, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.Y, -dPos, false);
                }
            }
            else if (strButtonText == "Y-" && !bCanXYMove)
            {
                MessageBox.Show("请将Z轴移动到安全位置！");
            }
            #endregion
            #region Z
            if (strButtonText == "Z+")
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.Z, true, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.Z, true, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.Z, dPos, false);
                }
            }
            if (strButtonText == "Z-")
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.Z, false, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.Z, false, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.Z, -dPos, false);
                }
            }
            #endregion
            #region U
            if (strButtonText == "U+")
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.U, true, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.U, true, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.U, dPos, false);
                }
            }
            if (strButtonText == "U-")
            {
                if (comboBoxMoveMode.SelectedIndex == 0)
                {
                    if (comboBoxMoveSpeed.SelectedIndex == 0)
                    {
                        tableDriver.JogMove(TableAxisName.U, false, false);
                    }
                    else
                    {
                        tableDriver.JogMove(TableAxisName.U, false, true);
                    }
                }
                else
                {
                    tableDriver.RelMove(TableAxisName.U, -dPos, false);
                }
            }
            #endregion
        }

        private void buttonYCW_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (!MainModule.FormMain.bHomeReady)
            //{
            //    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
            //    return;
            //}


            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            if (comboBoxMoveMode.SelectedIndex == 0)
            {
                //tableDriver.Stop(TableAxisName.ALL);
                tableDriver.JogStop(TableAxisName.ALL);
            }
        }

        private void buttonXHome_Click(object sender, EventArgs e)
        {
            //if (!bCanXYMove)
            //{
            //    MessageBox.Show("请将Z轴移动到安全位置！");
            //    return;
            //}
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            tableDriver.Home(TableAxisName.X);
        }

        private void buttonYHome_Click(object sender, EventArgs e)
        {
            //if (!bCanXYMove)
            //{
            //    MessageBox.Show("请将Z轴移动到安全位置！");
            //    return;
            //}
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            tableDriver.Home(TableAxisName.Y);
        }

        private void buttonZHome_Click(object sender, EventArgs e)
        {
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            tableDriver.Home(TableAxisName.Z);
        }

        private void buttonUHome_Click(object sender, EventArgs e)
        {
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            tableDriver.Home(TableAxisName.U);
        }

        private void dataGridViewPosSetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            try
            {
                string strName = dataGridViewPosSetting.Rows[e.RowIndex].Cells[0].Value.ToString();
                TablePosItem itemDis = tableDriver.tableData.tablePosData.tablePosItemDictionary[strName];
                TablePosItem itemList = tableDriver.tableData.tablePosData.tablePosItemList[e.RowIndex];
                double dPosX = double.Parse(labelCurrentPosX.Text.ToString());
                double dPosY = double.Parse(labelCurrentPosY.Text.ToString());
                double dPosZ = double.Parse(labelCurrentPosZ.Text.ToString());
                double dPosU = double.Parse(labelCurrentPosU.Text.ToString());
                if (e.ColumnIndex == 5)
                {
                    if (itemDis.bActionX)
                    {
                        dataGridViewPosSetting.Rows[e.RowIndex].Cells[1].Value = dPosX;
                        itemDis.dPosX = dPosX;
                        itemList.dPosX = dPosX;
                    }
                    if (itemDis.bActionY)
                    {
                        dataGridViewPosSetting.Rows[e.RowIndex].Cells[2].Value = dPosY;
                        itemDis.dPosY = dPosY;
                        itemList.dPosY = dPosY;
                    }
                    if (itemDis.bActionZ)
                    {
                        dataGridViewPosSetting.Rows[e.RowIndex].Cells[3].Value = dPosZ;
                        itemDis.dPosZ = dPosZ;
                        itemList.dPosZ = dPosZ;
                    }
                    if (itemDis.bActionU)
                    {
                        dataGridViewPosSetting.Rows[e.RowIndex].Cells[4].Value = dPosU;
                        itemDis.dPosU = dPosU;
                        itemList.dPosU = dPosU;
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    //if (IOManage.INPUT("手动/自动").On)
                    //{
                    //    MessageBox.Show("当前处于自动模式！");
                    //    return;
                    //}
                    tableDriver.StartPosMoveLowSpeed(strName, bCanXYMove);
                }
            }
            catch
            {

            }
        }

        private void FormTableDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Environment.Exit(System.Environment.ExitCode);
            //this.Dispose();
            //this.Close();
        }

        private void FormTableDriver_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerScan.Enabled = false;
        }

        private void btn_StartInterpolation_Click(object sender, EventArgs e)
        {
            //var posStart = tableDriver.tableData.tablePosData.tablePosItemList.Find(p=>p.strName==cmb_StartPos.Text);
            ////var posEnd = tableDriver.tableData.tablePosData.tablePosItemList.Find(p => p.strName == cmb_EndPos.Text);
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            var posStart = cmb_StartPos.SelectedItem as TablePosItem;
            var posEnd = cmb_EndPos.SelectedItem as TablePosItem;
            tableDriver.LineXYZMove(100, 100, 100, posEnd.dPosX, posEnd.dPosY, posEnd.dPosZ);
        }

        List<PointF> pointList = new List<PointF>();
        bool cureSatrt = false;
        private void btn_StartCircle_Click(object sender, EventArgs e)
        {
            cureSatrt = true;
           // ableDriver.ArcMove(0, 1, 2, 100, 100, 100, 100, 0, -100, 0);
        }

        private PathDataDriver pathDataDriver = new PathDataDriver();

        private void btn_AddPath_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_PathName.Text))
            //{
            //    MessageBox.Show("请输入路径名称！");
            //    return;
            //}
            //if (PathDataManage.pathDoc.m_pathDataDriverDictionary.ContainsKey(txt_PathName.Text))
            //{
            //    MessageBox.Show("已存在同名路径！");
            //    return;
            //}
            //PathDataDriver pathDriver = new PathDataDriver();
            //pathDriver.Name = txt_PathName.Text;
            //PathDataManage.pathDoc.m_pathDataDriverList.Add(pathDriver);
            //PathDataManage.pathDoc.m_pathDataDriverDictionary.Add(pathDriver.Name, pathDriver);
            //cmb_PathSelect.DataSource = null;
            //cmb_PathSelect.DataSource = PathDataManage.pathDoc.m_pathDataDriverList;
            //cmb_PathSelect.DisplayMember = "Name";
        }

        private void btn_AddRoute_Click(object sender, EventArgs e)
        {
            InterpolationType type;
            //if (cmb_Start.SelectedIndex < 0)
            //{
            //    MessageBox.Show("请选择起始位置！");
            //    return;
            //}
            //if (cmb_End.SelectedIndex < 0)
            //{
            //    MessageBox.Show("请选择结束位置！");
            //    return;
            //}
            //if (cmb_PathType.SelectedIndex < 0)
            //{
            //    MessageBox.Show("请选择插补类型！");
            //    return;
            //}
            //else
            //{
            //    type = (InterpolationType)Enum.Parse(typeof(InterpolationType), cmb_PathType.Text);
            //    if (type == InterpolationType.圆弧)
            //    {
            //        //if (string.IsNullOrEmpty(txt_Radium.Text))
            //        //{
            //        //    MessageBox.Show("请输入半径！");
            //        //    return;
            //        //}
            //        if (cmb_Dir.SelectedIndex < 0)
            //        {
            //            MessageBox.Show("请选择方向！");
            //            return;
            //        }
            //    }
            //}

            //ControlPlatformLib.Models.PathData path = new ControlPlatformLib.Models.PathData();
            //path.PathType = type;
            //path.PosStart = cmb_Start.SelectedItem as TablePosItem;
            //path.PosEnd = cmb_End.SelectedItem as TablePosItem;
            //path.PosMid = cmb_MidPos.SelectedItem as TablePosItem;
            ////path.R = Convert.ToDouble(txt_Radium.Text);
            //path.CircleDir = (short)cmb_Dir.SelectedIndex;
            //pathDataDriver.PathDataList.Add(path);
            ////var pathbinding = pathDataDriver.PathDataList.Select(x => new { 起始 = x.PosStart.dPosX + "," + x.PosStart.dPosY, 结束 = x.PosEnd.dPosX + "," + x.PosEnd.dPosY, 类型 = x.PathType, 半径 = x.R, 方向 = x.CircleDir == 0 ? "正方向" : "反方向" });
            //if (pathDataDriver.PathDataList != null && pathDataDriver.PathDataList.Count > 0)
            //{
            //    dgv_Path.DataSource = null;
            //    dgv_Path.DataSource = pathDataDriver.PathDataList;
            //}
        }

        private void cmb_PathSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmb_PathSelect.SelectedIndex < 0)
            //{
            //    return;
            //}
            //pathDataDriver = cmb_PathSelect.SelectedItem as PathDataDriver;
            ////var pathbinding = pathDataDriver.PathDataList.Select(x => new { 起始 = x.PosStart.dPosX + "," + x.PosStart.dPosY, 结束 = x.PosEnd.dPosX + "," + x.PosEnd.dPosY,类型=x.PathType,半径=x.R,方向=x.CircleDir==0?"正方向":"反方向"});
            //if (pathDataDriver.PathDataList != null && pathDataDriver.PathDataList.Count > 0)
            //{
            //    dgv_Path.DataSource = null;
            //    dgv_Path.DataSource = pathDataDriver.PathDataList;
            //}
            //else
            //{
            //    dgv_Path.DataSource = null;
            //}

        }

        private void dgv_Path_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if ((dgv_Path.Rows[e.RowIndex].DataBoundItem != null) &&
            //    (dgv_Path.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            //{
            //    string[] names = dgv_Path.Columns[e.ColumnIndex].DataPropertyName.Split('.');
            //    object obj = dgv_Path.Rows[e.RowIndex].DataBoundItem;
            //    for (int i = 0; i < names.Count(); ++i)
            //    {
            //        try
            //        {
            //            var result = obj.GetType().GetProperty(names[i]).GetValue(obj, null);
            //            obj = result;
            //            e.Value = result.ToString();
            //        }
            //        catch (Exception)
            //        {
            //            return;
            //            throw;
            //        }
            //    }
            //}
        }

        private void cmb_Start_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmb_Start.SelectedIndex < 0)
            //{
            //    return;
            //}
        }

        private void btn_DeleteRoute_Click(object sender, EventArgs e)
        {
            //if (dgv_Path.SelectedCells.Count <= 0)
            //    return;
            //ControlPlatformLib.Models.PathData path = dgv_Path.SelectedCells[0].OwningRow.DataBoundItem as ControlPlatformLib.Models.PathData;
            //pathDataDriver.PathDataList.Remove(path);
            //dgv_Path.DataSource = null;
            //dgv_Path.DataSource = pathDataDriver.PathDataList;
        }

        #region 生成路径
        private void btn_CreatePath_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = CreatePath();
                sb.AppendLine("M2");

                if (!Directory.Exists(@"C:/Users/Administrator/Desktop/Parameter/"))
                {
                    Directory.CreateDirectory(@"C:/Users/Administrator/Desktop/Parameter/");
                }
                FileStream fsWriter = new FileStream(@"C:/Users/Administrator/Desktop/Parameter/Path" + ".nc", FileMode.Create, FileAccess.Write, FileShare.Read);
                byte[] buffer = System.Text.Encoding.Default.GetBytes(sb.ToString());
                fsWriter.Write(buffer, 0, buffer.Length);
                fsWriter.Close();
                //FtpUpLoadFiles.UploadFile("/Path/", "C:/Users/Administrator/Desktop/Parameter/Path.nc", "path.nc");
                //FtpUpLoadFiles.FTPCONSTR = "ftp://192.168.1.11/";
                FtpUpLoadFiles.Onload("C:/Users/Administrator/Desktop/Parameter/Path.nc");
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("路径生成错误，错误信息：{0}", ex.Message);
            }
        }

        private StringBuilder CreatePath()
        {
            return null;
        }

        /// <summary>
        /// 设置画布中心点
        /// </summary>
        /// <param name="point">画布中心点</param>
        private void SetCanvasOrigin(System.Windows.Point point)
        {
            var transformGroup = new TransformGroup();
            var scaleUpTransform = new ScaleTransform();
            var translateTransform = new TranslateTransform();
            var scaleDownTransform = new ScaleTransform();
            var relativeSource = new RelativeSource { AncestorType = typeof(Canvas) };
            if (point.X > 0)
            {
                var xScale = canvas.ActualWidth / point.X;
                scaleUpTransform.ScaleX = xScale;
                var xBinding = new System.Windows.Data.Binding("ActualWidth");
                xBinding.RelativeSource = relativeSource;
                BindingOperations.SetBinding(translateTransform, TranslateTransform.XProperty, xBinding);
                scaleDownTransform.ScaleX = 1 / xScale;
            }
            if (point.Y > 0)
            {
                var yScale = canvas.ActualHeight / point.Y;
                scaleUpTransform.ScaleY = yScale;
                var yBinding = new System.Windows.Data.Binding("ActualHeight");
                yBinding.RelativeSource = relativeSource;
                BindingOperations.SetBinding(translateTransform, TranslateTransform.YProperty, yBinding);
                scaleDownTransform.ScaleY = 1 / yScale;
            }
            transformGroup.Children.Add(scaleUpTransform);
            transformGroup.Children.Add(translateTransform);
            transformGroup.Children.Add(scaleDownTransform);
            this.canvas.RenderTransform = transformGroup;
        }

        private void DrawArc(Graphics g)
        {
            GraphicsPath path = new GraphicsPath();

        }

        private void btn_TestPath_Click(object sender, EventArgs e)
        {
            tableDriver.StartCure(false);
        }

        /// <summary>  
        /// 绘制线段  
        /// </summary>  
        protected void DrawingLine(System.Windows.Point startPt, System.Windows.Point endPt, Canvas canvas)
        {
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            PathGeometry pathGeometry = new PathGeometry();
            startPt = MillimetersToPixels(startPt);
            endPt = MillimetersToPixels(endPt);

            LineSegment line = new LineSegment(new System.Windows.Point(endPt.X, endPt.Y), true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new System.Windows.Point(startPt.X, startPt.Y);
            figure.Segments.Add(line);
            pathGeometry.Figures.Add(figure);
            path.Data = pathGeometry;
            path.Stroke = System.Windows.Media.Brushes.Orange;
            canvas.Children.Add(path);
        }

        /// <summary>  
        /// 绘制线段  
        /// </summary>  
        protected void DrawingArc(System.Windows.Point startPt, System.Windows.Point endPt, Canvas canvas, double r, SweepDirection circleDir)
        {

            startPt = MillimetersToPixels(startPt);
            endPt = MillimetersToPixels(endPt);
            //startPt = XYTransf(startPt,xypoint);
            //endPt = XYTransf(endPt,xypoint);

            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            PathGeometry pathGeometry = new PathGeometry();
            ArcSegment arc = new ArcSegment(new System.Windows.Point(endPt.X, endPt.Y),
                new System.Windows.Size(MillimetersToPixels((float)r),
                MillimetersToPixels((float)r)),
                0,
                false,
                circleDir,
                true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new System.Windows.Point(startPt.X, startPt.Y);
            figure.Segments.Add(arc);
            pathGeometry.Figures.Add(figure);
            path.Data = pathGeometry;
            path.Stroke = System.Windows.Media.Brushes.Orange;
            canvas.Children.Add(path);
        }

        /// <summary>
        /// 分辨率转毫米
        /// </summary>
        private double scaling;
        private double MillimetersToPixels(double length)
        {
            return length * 96 / scaling;
        }

        private System.Windows.Point MillimetersToPixels(System.Windows.Point point)
        {
            //return length * 96 / 25.4;
            return new System.Windows.Point(point.X * 96 / scaling, point.Y * 96 / scaling);
        }

        public System.Windows.Point XYTransf(System.Windows.Point point, System.Windows.Point xypoint)
        {
            point.X = MillimetersToPixels(point.X) + xypoint.X;
            point.Y = xypoint.Y - MillimetersToPixels(point.Y);

            return point;//显示屏幕坐标系的位置  
        }

        public void CanvasMouseWheel(object canva, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                scaling = scaling + e.Delta * 0.01;
                var canvas = canva as Canvas;
                CreatePath();
            }
            else
            {
                scaling = scaling - Math.Abs(e.Delta) * 0.01;
                var canvas = canva as Canvas;
                CreatePath();
            }
        }

        /// <summary>
        /// 生成预焊位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreatePreWelding_Click(object sender, EventArgs e)
        {
            int lineNumber = 10;
            StringBuilder sb = new StringBuilder();
            List<TablePosItem> listData = TableManage.TableDriver("Beckhoff").tableData.tablePosData.tablePosItemList.Where(p=>p.strName.Contains("预焊位")).ToList();

            preWeldingCanvas.Children.Clear();
            System.Windows.Point point = new System.Windows.Point(preWeldingCanvas.ActualWidth, preWeldingCanvas.ActualHeight);
            System.Windows.Point xypoint = new System.Windows.Point(point.X / 2, point.Y / 2);//新坐标原点
            SetCanvasOrigin(xypoint);
            sb.AppendLine("N" + lineNumber + " G01 X" + listData[0].dPosX + " Y" + listData[0].dPosY + " F" + tableDriver.tableData.axisXData.dSpeed * 60);

            //extendedGraphics.DrawRoundRectangle(pen, (float)pathDataDriver.PathDataList[0].PosStart.dPosX, (float)pathDataDriver.PathDataList[0].PosStart.dPosY, 100, 50, (int)pathDataDriver.PathDataList[0].R);

            lineNumber += 10;
            foreach (var item in listData)
            {
                sb.AppendLine("N" + lineNumber + " G01 X" + item.dPosX + " Y" + item.dPosY + " F" + tableDriver.tableData.axisXData.dSpeed * 60);
                lineNumber += 10;
                DrawingLine(new System.Windows.Point(item.dPosX, item.dPosY), new System.Windows.Point(item.dPosX, item.dPosY), preWeldingCanvas);
                Global.logger.InfoFormat("插入直线插补，位置{0}", item.strName);
            }

            sb.AppendLine("M2");

            if (!Directory.Exists(@"C:/Users/Administrator/Desktop/Parameter/"))
            {
                Directory.CreateDirectory(@"C:/Users/Administrator/Desktop/Parameter/");
            }
            FileStream fsWriter = new FileStream(@"C:/Users/Administrator/Desktop/Parameter/PreWeldingPath" + ".nc", FileMode.Create, FileAccess.Write, FileShare.Read);
            byte[] buffer = System.Text.Encoding.Default.GetBytes(sb.ToString());
            fsWriter.Write(buffer, 0, buffer.Length);
            fsWriter.Close();

            //FtpUpLoadFiles.FTPCONSTR = "ftp://192.168.1.11/";
            //FtpUpLoadFiles.UploadFile("C:/Users/Administrator/Desktop/Parameter/PreWeldingPath.nc", "prePath.nc");

            FtpUpLoadFiles.Onload("C:/Users/Administrator/Desktop/Parameter/PreWeldingPath.nc");
        }
        #endregion

        private void btn_TestWeldingPath_Click(object sender, EventArgs e)
        {
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
            //if (cmb_WeldingStart.SelectedIndex<0)
            //{
            //    MessageBox.Show("请选择焊接起始位置！");
            //    return;
            //}
            //if (cmb_WeldingEnd.SelectedIndex < 0)
            //{
            //    MessageBox.Show("请选择焊接起始位置！");
            //    return;
            //}
            //if (!Double.TryParse(txt_N2OutDelay.Text, out dN2OutDelay))
            //{
            //    MessageBox.Show("请输入数字！");
            //    return;
            //}
            //if (!Double.TryParse(txt_N2OutDelay.Text, out dN2CloseDelay))
            //{
            //    MessageBox.Show("请输入数字！");
            //    return;
            //}
            //if (!bTestWeldingPathBusy)
            //{
            //    Global.bTestWeldingPath = true;
            //    nTest = 0;
            //}
        }

        int nTest = 0;
        bool bTestWeldingPathBusy;
        HiPerfTimer testWeldingTimer=new HiPerfTimer();
        double dN2OutDelay, dN2CloseDelay;
        void TestWeldingPath()
        {
            //while (true)
            //{
            //    Thread.Sleep(1);
            //    bool status = !MainModule.FormMain.bAuto && !Global.weldingStarted && MainModule.FormMain.bHomeReady;
            //    if (Global.bTestWeldingPath && status)
            //    {
                   
            //    }
            //}
        }

        void CheckMode()
        {
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    MessageBox.Show("当前处于自动模式！");
            //    return;
            //}
        }

     

        private void btn_TestGCode_Click(object sender, EventArgs e)
        {
            //if (IOManage.INPUT("手动/自动").On)
            //{
            //    return;
            //}
            IOManage.OUTPUT("压装压板气缸").SetOutBit(true);
            tableDriver.StartCure(true);
        }

        private void txt_Radium_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Dir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void cmb_PathType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_ModifyPath_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_TestPreWelding_Click(object sender, EventArgs e)
        {

        }
        ControlPlatformLib.Models.PathData selectPathData;

        private void 保存D_Click(object sender, EventArgs e)
        {


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "d:\\";
            //saveFileDialog1.Filter = "ext files (*.txt)|*.txt|All files(*.*)|*>**";
            saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                string localFilePath = saveFileDialog1.FileName.ToString(); //获得文件路径
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                string filePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));//获取文件路径，不带文件名

                if (localFilePath != "")
                {
                    TableManage.tablesDoc.SaveDoc();
                    //pointdatacom.Items.Add(fileNameExt);
                    TableManage.tablesDoc.SaveDocOtherForder(localFilePath);
                }
            }


            //TableManage.tablesDoc.SaveDoc();
            //if (XMLPath.Text != "")
            //{
            //    pointdatacom.Items.Add(XMLPath.Text + ".xml");
            //    TableManage.tablesDoc.SaveDocOtherForder(XMLPath.Text);
            //}
        }
        FormHardwareMonitor FormHardware = FormHardwareMonitor.GetForm();
        private void 加载_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://";
            //  openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";

            openFileDialog.Filter = "xml文件(*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                //  LoadObjTablesDocs(fName);
                //TableManage. LoadData();
                //   LoadObjTablesDocs(fName);

                string stfPath = System.Environment.CurrentDirectory + "\\Parameter\\TablesDoc.xml";
                if (File.Exists(stfPath))
                {
                    File.Delete(stfPath);
                }
                if (!File.Exists(stfPath))
                {
                    File.Copy(fName, stfPath);

                }

                UpDate_LoadObjTablesDocs(stfPath);
                //  FormHardware.chooseName = "1";
                //UpDate_LoadObjTablesDocs(fName);
                FormHardware.chooseName = "1";
                // TableManage.LoadData_Other(fName);
                //  UpdateData();
                //  TableManage.tablesDoc.SaveDoc();
            }
            // TableManage.LoadData();
        }
        public  void LoadObjTablesDocs(string path)
        {
            TablesDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            FileStream fsReader = null;
            try
            {

                string stfPath = System.Environment.CurrentDirectory + "\\Parameter\\TablesDoc.xml";
                // frmTableDriver.tableDriver = TableManage.tableDrivers.drivers[comboBoxTables.Items[comboBoxTables.SelectedIndex].ToString()];
                // frmTableDriver.LoadObjTablesDocs(stfPath);
                //  frmTableDriver.UpdateData();



                int listDate = 0;
                dataGridViewPosSetting.Rows.Clear();
                // MessageBox.Show("1212");
                //  fsReader = File.OpenRead(@".//PointDate/" + pointdatacom.Text);
                fsReader = File.OpenRead(path);
                pDoc = (TablesDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_tableDictionary = pDoc.m_tableDataList.ToDictionary(p => p.strTableName);
                foreach (TableData item in pDoc.m_tableDataList)
                {
                    string name = item.strTableName;
                    tableDriver = TableManage.tableDrivers.drivers[name];
                    //strTableName_ShowUi = name;
                    // item.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);//tablePosItemDictionary
                    // tableDriver.tableData.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);//tablePosItemDictionary
                    //  tableDriver.tableData.tablePosData.tablePosItemDictionary[name] = item.tablePosData.tablePosItemList[listDate];//tablePosItemDictionary
                    // tableDriver.tableData.tablePosData.tablePosItemList = item.tablePosData.tablePosItemList;
                    //item.tablePosData.tablePosItemDictionary[name] = tableDriver.tableData.tablePosData.tablePosItemList ;
                    tableDriver.tableData.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);
                   // UpdateData();
                }
                string Names = TableManage.tableDrivers.drivers.FirstOrDefault().Key;
                tableDriver = TableManage.tableDrivers.drivers[Names];
                UpdateData();
                if (File.Exists(stfPath))
                {
                    File.Delete(stfPath);
                }
                if (!File.Exists(stfPath))
                {
                    File.Copy(path, stfPath);

                }
                Thread.Sleep(100);
                //frmTableItem.UpdateItemData(tbData);
            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new TablesDoc();
                //   MessageBox.Show("sdsd");
            }
        }
        public void UpDate_LoadObjTablesDocs(string path)
        {
            TablesDoc pDoc;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            FileStream fsReader = null;
            try
            {

                string stfPath = System.Environment.CurrentDirectory + "\\Parameter\\TablesDoc.xml";
                // frmTableDriver.tableDriver = TableManage.tableDrivers.drivers[comboBoxTables.Items[comboBoxTables.SelectedIndex].ToString()];
                // frmTableDriver.LoadObjTablesDocs(stfPath);
                //  frmTableDriver.UpdateData();



                int listDate = 0;
                dataGridViewPosSetting.Rows.Clear();
                // MessageBox.Show("1212");
                //  fsReader = File.OpenRead(@".//PointDate/" + pointdatacom.Text);
                fsReader = File.OpenRead(path);
                pDoc = (TablesDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_tableDictionary = pDoc.m_tableDataList.ToDictionary(p => p.strTableName);
                foreach (TableData item in pDoc.m_tableDataList)
                {
                    string name = item.strTableName;
                    tableDriver = TableManage.tableDrivers.drivers[name];
                    //strTableName_ShowUi = name;
                    // item.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);//tablePosItemDictionary
                    // tableDriver.tableData.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);//tablePosItemDictionary
                    //  tableDriver.tableData.tablePosData.tablePosItemDictionary[name] = item.tablePosData.tablePosItemList[listDate];//tablePosItemDictionary

                    //item.tablePosData.tablePosItemDictionary[name] = tableDriver.tableData.tablePosData.tablePosItemList ;
                    tableDriver.tableData.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);
                    tableDriver.tableData.tablePosData.tablePosItemList = item.tablePosData.tablePosItemList;
                    // UpdateData();
                }
                string Names = TableManage.tableDrivers.drivers.FirstOrDefault().Key;
                tableDriver = TableManage.tableDrivers.drivers[Names];
                UpdateData();
                //if (File.Exists(stfPath))
                //{
                //    File.Delete(stfPath);
                //}
                //if (!File.Exists(stfPath))
                //{
                //    File.Copy(path, stfPath);

                //}
                Thread.Sleep(100);
                //frmTableItem.UpdateItemData(tbData);
            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                //   pDoc = new TablesDoc();
                //   MessageBox.Show("sdsd");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
         //   int zx = int.Parse(textBox1.Text);  
            tab_control.SetBounds(panel1.Top, panel1.Left, Width - btn_PreWelding.Width - tab_control.Left, panel1.Height);
        }

        private void 数据更新_Click(object sender, EventArgs e)
        {
            string stfPath = System.Environment.CurrentDirectory + "\\Parameter\\TablesDoc.xml";

            UpDate_LoadObjTablesDocs(stfPath);
            FormHardware.chooseName = "1";
            //TableManage.LoadData();
        }

        private void dataGridViewPosSetting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strName = dataGridViewPosSetting.Rows[e.RowIndex].Cells[0].Value.ToString();
                TablePosItem itemDis = tableDriver.tableData.tablePosData.tablePosItemDictionary[strName];
                TablePosItem itemList = tableDriver.tableData.tablePosData.tablePosItemList[e.RowIndex];
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

                int count = 0;
                foreach (TablePosItem item in tableDriver.tableData.tablePosData.tablePosItemList)
                {
                
                    if (item.strName == itemDis.strName)
                    {
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosX = item.dPosX;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosY = item.dPosY;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosZ = item.dPosZ;
                        tableDriver.tableData.tablePosData.tablePosItemList[count].dPosU = item.dPosU;
                        //item.dPosY
                        //     item.dPosZ
                        //item.dPosU
                    }
                    count++;
                }           
                cmb_StartPos.DataSource = tableDriver.tableData.tablePosData.tablePosItemList;
                cmb_EndPos.DataSource = tableDriver.tableData.tablePosData.tablePosItemList;
            }
            catch
            {

            }
       //   TableManage.tablesDoc.SaveDoc();

        }

        private void dgv_Path_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
              
            }
            
        }

        
    }
}
