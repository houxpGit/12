using ControlPlatformLib;
//using FullyAutomaticLaserJetCoder.CCD;
using FullyAutomaticLaserJetCoder.MainTask;
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
using WorldGeneralLib.DataLogicBarCode;
using WorldGeneralLib.SerialPorts;

namespace FullyAutomaticLaserJetCoder
{
    public partial class SettingsForm : Form
    {
        public MainControl MainControls = MainControl.Instance();

        public SettingsForm()
        {
           
           
            InitializeComponent();

            最小模拟量.Text = DateSave.Instance().Production.IncreaseMminimumAnalog.ToString();
            最大模拟量.Text = DateSave.Instance().Production.IncreaseMaximumAnalog.ToString();
            Z轴最小坐标.Text = DateSave.Instance().Production.Z_AxisMinimumCoordinate.ToString();
            Z轴最大坐标.Text = DateSave.Instance().Production.Z_AxisMaximumCoordinate.ToString();
            关联后所对应.Text = DateSave.Instance().Production.High_Date.ToString();

            基准点模拟量.Text = DateSave.Instance().Production.BaselineSimulation.ToString();
            基准Z坐标.Text = DateSave.Instance().Production.Z_AxialDatum.ToString();
            基准X坐标.Text = DateSave.Instance().Production.X_AxialDatum.ToString();
            基准Y坐标.Text = DateSave.Instance().Production.Y_AxialDatum.ToString();

            相机位置X.Text = DateSave.Instance().Production.X_Camera_Position.ToString();
            相机位置Y.Text = DateSave.Instance().Production.Y_Camera_Position.ToString();
            激光位置X.Text = DateSave.Instance().Production.X_Laser_Position.ToString();
            激光位置Y.Text = DateSave.Instance().Production.Y_Laser_Position.ToString();

            X偏距.Text = DateSave.Instance().Production.X_Setover.ToString();
            Y偏距.Text = DateSave.Instance().Production.Y_Setover.ToString();


            Z轴安全高度最高.Text = DateSave.Instance().Production.SaveHigh_Top.ToString();
            Z轴安全高度最低.Text = DateSave.Instance().Production.SaveHigh_Low.ToString();
            调高最高.Text = DateSave.Instance().Production.AutoZ_High_Top.ToString();
            调高最低.Text = DateSave.Instance().Production.AutoZ_High_Low.ToString();
            铜嘴清理次数.Text = DateSave.Instance().Production.Clear_TIME.ToString();

            设备类型.Text = DateSave.Instance().Production.WeldOther.ToString();
        
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SerialPortSettingsForm barcodeForm = new SerialPortSettingsForm();
            barcodeForm.TopLevel = false;
            gb_Scan.Controls.Add(barcodeForm);
            barcodeForm.Dock = DockStyle.Fill;
            barcodeForm.Show();
        }
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public int frist = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {            
            double date = 0.0;   
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);
            当前模拟量.Text = date.ToString("#0.00 ");

            label16.Text = TableManage.TableDriver("运动平台").CurrentZ.ToString() ;
        }
        public void delay(int timeLong)
        {

            DateTime starttime = DateTime.Now;
            int stime = timeLong / 1000;
            while (true)
            {
                Thread.Sleep(1);
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (spantime.TotalSeconds > stime)
                {

                    break;
                }
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int Width = Screen.PrimaryScreen.WorkingArea.Width;
            int Height = Screen.PrimaryScreen.WorkingArea.Height;
            panel2.SetBounds(2, 2, Width, Height - 95);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void 获取最小模拟量_Click(object sender, EventArgs e)
        {
            double date = 0.0;
            Thread.Sleep(100);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);
            Thread.Sleep(100);
            DateSave.Instance().Production.IncreaseMminimumAnalog = date;
            最小模拟量.Text = date.ToString();
        }
        private void 获取最大模拟量_Click(object sender, EventArgs e)
        {
            double date = 0.0;
            Thread.Sleep(100);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);
            Thread.Sleep(100);
            DateSave.Instance().Production.IncreaseMaximumAnalog = date;
            最大模拟量.Text = date.ToString();
        }

        private void 获取最小Z轴坐标_Click(object sender, EventArgs e)
        {
            double date = TableManage.TableDriver("运动平台").CurrentZ;
            Thread.Sleep(100);
            DateSave.Instance().Production.Z_AxisMinimumCoordinate = date;
            Z轴最小坐标.Text = date.ToString();
        }
        private void 获取最大Z轴坐标_Click(object sender, EventArgs e)
        {
            double date = TableManage.TableDriver("运动平台").CurrentZ;
            Thread.Sleep(100);
            DateSave.Instance().Production.Z_AxisMaximumCoordinate = date;
            Z轴最大坐标.Text = date.ToString();
        }
        private void 获取关联数据_Click(object sender, EventArgs e)
        {
            double date1 = Math.Abs(DateSave.Instance().Production.IncreaseMaximumAnalog - DateSave.Instance().Production.IncreaseMminimumAnalog) ;
            double date2 = Math.Abs(DateSave.Instance().Production.Z_AxisMaximumCoordinate - DateSave.Instance().Production.Z_AxisMinimumCoordinate);

            DateSave.Instance().Production.High_Date = Math.Abs(date1 / date2) ;
            关联后所对应.Text = DateSave.Instance().Production.High_Date.ToString();
        }
        private void 获取基准模拟量_Click(object sender, EventArgs e)
        {
            double date = 0.0;
            Thread.Sleep(100);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);
            Thread.Sleep(100);
            DateSave.Instance().Production.BaselineSimulation = date;
            基准点模拟量.Text = date.ToString();
        }
        private void 获取基准坐标_Click(object sender, EventArgs e)
        {
            double dateX = TableManage.TableDriver("运动平台").CurrentX;
            Thread.Sleep(100);
            double dateY = TableManage.TableDriver("运动平台").CurrentY;
            Thread.Sleep(100);
            double dateZ = TableManage.TableDriver("运动平台").CurrentZ;
            Thread.Sleep(100);
            try
            {

                DateSave.Instance().Production.Z_AxialDatum = TableManage.TablePosItem("运动平台", "调高基准点坐标").dPosZ;
                DateSave.Instance().Production.X_AxialDatum = TableManage.TablePosItem("运动平台", "调高基准点坐标").dPosX;
                DateSave.Instance().Production.Y_AxialDatum = TableManage.TablePosItem("运动平台", "调高基准点坐标").dPosY;
                基准Z坐标.Text = DateSave.Instance().Production.Z_AxialDatum.ToString();
                基准X坐标.Text = DateSave.Instance().Production.X_AxialDatum.ToString();
                基准Y坐标.Text = DateSave.Instance().Production.Y_AxialDatum.ToString();
            }
            catch
            {
                MessageBox.Show("获取失败，请查看数据");
            }
        }
        private void 调高_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }

            MainControls.调高();
        }

        private void 到基准点_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            try
            {
                RunClass.Instance().Meth.Asix_one_Run("运动平台", "调高基准点坐标", 2, 60000);
                RunClass.Instance().Meth.Asix_Two_Run("运动平台", "调高基准点坐标", 60000);
            
            }
            catch
            {
                MessageBox.Show("调高基准点坐标失败，请查看数据");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateSave.Instance().Production.IncreaseMaximumAnalog = Convert.ToDouble(最大模拟量.Text);
                DateSave.Instance().Production.IncreaseMminimumAnalog = Convert.ToDouble(最小模拟量.Text);
                DateSave.Instance().Production.Z_AxisMaximumCoordinate = Convert.ToDouble(Z轴最大坐标.Text);
                DateSave.Instance().Production.Z_AxisMinimumCoordinate = Convert.ToDouble(Z轴最小坐标.Text);
                DateSave.Instance().Production.High_Date = Convert.ToDouble(关联后所对应.Text);

                DateSave.Instance().Production.BaselineSimulation = Convert.ToDouble(基准点模拟量.Text);
                DateSave.Instance().Production.Z_AxialDatum = Convert.ToDouble(基准Z坐标.Text);
                DateSave.Instance().Production.X_AxialDatum = Convert.ToDouble(基准X坐标.Text);
                DateSave.Instance().Production.Y_AxialDatum = Convert.ToDouble(基准Y坐标.Text);

                DateSave.Instance().Production.X_Setover = Convert.ToDouble(X偏距.Text);
                DateSave.Instance().Production.Y_Setover = Convert.ToDouble(Y偏距.Text);

                DateSave.Instance().Production.SaveHigh_Top = Convert.ToInt16(Z轴安全高度最高.Text);
                DateSave.Instance().Production.SaveHigh_Low = Convert.ToInt16(Z轴安全高度最低.Text);
                DateSave.Instance().Production.AutoZ_High_Top = Convert.ToInt16(调高最高.Text);
                DateSave.Instance().Production.AutoZ_High_Low = Convert.ToInt16(调高最低.Text);
                DateSave.Instance().Production.Clear_TIME = Convert.ToInt16(铜嘴清理次数.Text);
                DateSave.Instance().SaveDoc();
            }
            catch
            {
                MessageBox.Show("保存失败，请检查数据");
            }
        }

        private void 相机位置_Click(object sender, EventArgs e)
        {
            double CurrentX = TableManage.TableDriver("运动平台").CurrentX;
            double CurrentY = TableManage.TableDriver("运动平台").CurrentY;
            DateSave.Instance().Production.X_Camera_Position = TableManage.TablePosItem("运动平台", "相机位置").dPosX;
            DateSave.Instance().Production.Y_Camera_Position = TableManage.TablePosItem("运动平台", "相机位置").dPosY;
            相机位置X.Text = DateSave.Instance().Production.X_Camera_Position.ToString();
            相机位置Y.Text = DateSave.Instance().Production.Y_Camera_Position.ToString();
        }

        private void 激光位置_Click(object sender, EventArgs e)
        {
            double CurrentX = TableManage.TableDriver("运动平台").CurrentX;
            double CurrentY = TableManage.TableDriver("运动平台").CurrentY;
            DateSave.Instance().Production.X_Laser_Position = TableManage.TablePosItem("运动平台", "激光位置").dPosX;
            DateSave.Instance().Production.Y_Laser_Position = TableManage.TablePosItem("运动平台", "激光位置").dPosY;
            激光位置X.Text = DateSave.Instance().Production.X_Laser_Position.ToString();
            激光位置Y.Text = DateSave.Instance().Production.Y_Laser_Position.ToString();
        }

        private void 到相机位置_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "相机位置", 60000);
        }

        private void 到激光位置_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "激光位置", 60000);
        }

        private void 计算偏距_Click(object sender, EventArgs e)
        {
            DateSave.Instance().Production.X_Setover = DateSave.Instance().Production. X_Laser_Position - DateSave.Instance().Production.X_Camera_Position;
            X偏距.Text = DateSave.Instance().Production.X_Setover.ToString();

            DateSave.Instance().Production.Y_Setover = DateSave.Instance().Production. Y_Laser_Position - DateSave.Instance().Production.Y_Camera_Position;
            Y偏距.Text = DateSave.Instance().Production.Y_Setover.ToString();

        }

        private void 验证_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "调高1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "调高1#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            Thread.Sleep(100);
            double HIGH = 0.0;
            if (RunClass.Instance().调高数据() > 0)
            {
                HIGH = RunClass.Instance().调高数据();

            }
            else
            {
                MessageBox.Show("调高失败");
                return;

            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().AxisR.Asix_z_Auto_High("运动平台", "拍照1#点坐标", HIGH, DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);


          //  RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照1#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X=  TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosX;
          double Y = TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();

            string err = "";
            bool df = false;
            int needGetR = 2;
            int NeedCheckR = 2;
            Program.form.VisionLocation(NeedCheckR, needGetR,ref resultCirclr);


         //   Program.form.TestVision(ref resultCirclr, df, needGetR, NeedCheckR, out err);
            double awd = resultCirclr[0].CenterPoint.X;
            double awd1 = resultCirclr[0].CenterPoint.Y;
            double awd112 = resultCirclr[0].Radius;

          //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
          //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

            double RunX = X + DateSave.Instance().Production.X_Setover - awd;
            double RunY = Y + DateSave.Instance().Production.Y_Setover -awd1;
            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                         RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                        RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
        }

        private void 去焦点_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照1#点坐标", 2, 60000);
        }
        Thread Weld = null;
        private void 焊接_Click(object sender, EventArgs e)
        {
            Weld = new Thread(weld_Finish);
            Weld.IsBackground = true;
            Weld.Start();       
        }
        public void weld_Finish()
        {
            WeldFinishSta = "";
            IOManage.OUTPUT("脱机文件0触发").SetOutBit(true);
            Thread.Sleep(300);
            IOManage.OUTPUT("开始焊接机").SetOutBit(true);
            Task Task = WeldFinish();
            while (true)
            {
                if (WeldFinishSta == "WeldFinish")
                {
                    break;
                }
            }         
            IOManage.OUTPUT("脱机文件0触发").SetOutBit(false);
            IOManage.OUTPUT("开始焊接机").SetOutBit(false);
            if (Weld!=null)
            {
                Weld.Abort();
            }
        }
        string WeldFinishSta = "";
        public async Task WeldFinish()
        {
            WeldFinishSta = "";
            //  Task Tast
            await Task.Run(() =>
            {
                while (true)
                {
                    if (IOManage.INPUT("文档状态").On)
                    {
                        break;
                    }
                }
                while (true)
                {
                    if (IOManage.INPUT("文档状态").Off)
                    {
                        Thread.Sleep(100);
                        WeldFinishSta = "WeldFinish";
                        break;
                    }
                }
                return;
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照3#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照3#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接3#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接3#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int needGetR = 2;
            int NeedCheckR = 1;
            Program.form.TestVision(ref resultCirclr, df, needGetR, NeedCheckR, out err);
            double awd = resultCirclr[0].CenterPoint.X;
            double awd1 = resultCirclr[0].CenterPoint.Y;
            double awd112 = resultCirclr[0].Radius;

            double RunX = X - DateSave.Instance().Production.X_Setover + awd;
            double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                         RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                        RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照5#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照5#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接5#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接5#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int needGetR = 2;
            int NeedCheckR = 1;
            Program.form.TestVision(ref resultCirclr, df, needGetR, NeedCheckR, out err);
            double awd = resultCirclr[0].CenterPoint.X;
            double awd1 = resultCirclr[0].CenterPoint.Y;
            double awd112 = resultCirclr[0].Radius;

            double RunX = X - DateSave.Instance().Production.X_Setover + awd;
            double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                         RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                        RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (空跑.Checked==true)
            {
                DateSave.Instance().Production.Empty_run = true;

            }
            else { DateSave.Instance().Production.Empty_run = false; }

        }

        private void 焊接1_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照1#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照1#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int needGetR = 1;
            int NeedCheckR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        //    Program.form.TestVision(ref resultCirclr, df, needGetR, NeedCheckR, out err);
         
        }

        private void 焊接2_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照2#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照2#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照2#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照2#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            int NeedCheckR = 1;
            int needGetR = 2;  
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover -awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接3_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照3#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照3#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照3#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照3#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接4_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照4#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照4#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照4#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照4#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接5_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照5#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照5#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照5#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照5#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接6_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照6#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照6#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照6#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照6#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover -awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接7_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照7#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照7#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接7#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接7#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接8_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照8#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照8#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接8#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接8#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double CurrentX = TableManage.TableDriver("运动平台").CurrentX;
            double CurrentY = TableManage.TableDriver("运动平台").CurrentY;
           // double X = TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosX;
          //  double Y = TableManage.TablePosItem("运动平台", "拍照1#点坐标").dPosY;
            double RunX = CurrentX+DateSave.Instance().Production.X_Setover;
            double RunY = CurrentY +DateSave.Instance().Production.Y_Setover;
            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                         RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

            TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                        RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string XX = textBox1.Text;
            string YY = textBox2.Text;
            string SendDate = "Offset;" + XX + ";" + YY + ";" + "0;";
            Socket_server.Instance().sendDataToMac(SendDate);
        }

        private void 焊接9_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照9#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照9#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接9#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接9#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接10_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照10#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照10#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接10#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接10#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接11_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照11#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照11#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接11#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接11#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover -awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接12_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照12#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照12#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接12#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接12#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接13_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照13#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照13#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接13#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接13#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接14_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照14#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照14#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接14#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接14#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover -awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接15_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照15#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照15#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "焊接15#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "焊接15#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 1;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover - awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 焊接16_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "拍照16#点坐标", 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "拍照1#点坐标", 60000);
            RunClass.Instance().Meth.Asix_one_Run("运动平台", "拍照16#点坐标", 2, 60000);
            // MainControls.RunClass.Meth.Asix_Two_Run("运动平台", "相机位置", 60000);
            double X = TableManage.TablePosItem("运动平台", "拍照16#点坐标").dPosX;
            double Y = TableManage.TablePosItem("运动平台", "拍照16#点坐标").dPosY;
            Thread.Sleep(100);

            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int NeedCheckR = 1;
            int needGetR = 2;
            bool CamerFinishi = Program.form.VisionLocation(NeedCheckR, needGetR, ref resultCirclr);
            if (CamerFinishi == true)
            {
                double awd = resultCirclr[0].CenterPoint.X;
                double awd1 = resultCirclr[0].CenterPoint.Y;
                double awd112 = resultCirclr[0].Radius;

                //double RunX=  X -DateSave.Instance().Production.X_Setover+ awd;
                //double RunY = Y - DateSave.Instance().Production.Y_Setover + awd1;

                double RunX = X + DateSave.Instance().Production.X_Setover - awd;
                double RunY = Y + DateSave.Instance().Production.Y_Setover -awd1;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.X,
                             RunX, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisXData.dSpeed);

                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Y,
                            RunY, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisYData.dSpeed);
            }
            else
            {
                MessageBox.Show("拍照失败");
            }
        }

        private void 设备类型设置_Click(object sender, EventArgs e)
        {
            if (设备类型.Text!="")
            {
                int s = int.Parse(设备类型.Text);
                DateSave.Instance().Production.WeldOther = s;
            }
          


        }
        //Thread sss;
        //private void button6_Click(object sender, EventArgs e)
        //{
        //     sss = new Thread(dsds);
        //    sss.IsBackground = true;
        //    sss.Start();
        //}

        //public void dsds()
        //{
        //    int coun = 1;
        //    while (true)
        //    {
        //        try
        //        {
        //            if (coun>16)
        //            {
        //                coun = 1;
        //            }
        //            RunClass.Instance().Meth.Asix_Line_Run("运动平台", "焊接" + coun + "#点坐标", 60000);
        //            Thread.Sleep(1000);
        //            coun++;
        //        }
        //        catch
        //        {
        //            coun = 1;
        //        }


        //    }
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    sss.Abort();
        //}


        //private void button2_Click(object sender, EventArgs e)
        //{
        //    MainControls.RunClass.Meth.Asix_Line_Run("运动平台", "拍照1#点坐标", 60000);
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    MainControls.RunClass.Meth.Asix_Line_Run("运动平台", "拍照2#点坐标", 60000);
        //}
    }
}
