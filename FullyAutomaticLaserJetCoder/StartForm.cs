using ControlPlatformLib;
using FullyAutomaticLaserJetCoder.MainTask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib;
//using LocationCircle;
namespace FullyAutomaticLaserJetCoder
{
    public partial class StartForm : Form
    {
        public MainControl MainControl = MainControl.Instance();
        private TaskGroup m_WeldingTaskGroup = new TaskGroup();
        private FormOperator frmOperator;
        private bool startHoming;
        private bool bJetTestStart;
        private bool bMarkTestStart;
        private int homeStep=-1;
        private int jetTestStep = -1;
        private int markTestStep = -1;
        private HiPerfTimer hiPerfTimer;
        private HiPerfTimer resetTimer;
        private object lockObj = new object();
        public TaskGroup m_MarkJetTaskGroup;
      //  public ComeOut_process ComeOut = new ComeOut_process();
     //   public Weld_Process Weld=new Weld_Process();
        public MarkTask m_MarkTask;
      //  public Laser_PowerOn_And_Off Laser_PowerOn=new Laser_PowerOn_And_Off();
        public MainControl MainControls = MainControl.Instance();
   //     public MainControlLeft MainControlLe = MainControlLeft.Instance();
       // public Feed_process FeedTask=new Feed_process();
        public Method Methods=new Method();
        AutoSizeFormClass asc = new AutoSizeFormClass();
        // private OperateForm operateForm;
        public string stfPath = System.Environment.CurrentDirectory + "\\FlowDocument";

        TestDateCom testDt = new TestDateCom();

        public StartForm()
        {
            InitializeComponent();
           
        }
        //Load事件
        private void StartForm_Load(object sender, EventArgs e)
        {
            threadList.Add(ThreadHome);
               //  MainControls = new MainControl("运动流程任务", m_WeldingTaskGroup);

               //  MessageBox.Show(testDt.COMM.PortName);
               // testDt.DataReceivedstr = "2";
               hiPerfTimer = new HiPerfTimer();
            resetTimer = new HiPerfTimer();

            frmOperator = new FormOperator();
            frmOperator.TopLevel = false;
            panelOP.Controls.Add(frmOperator);
            frmOperator.Size = panelOP.Size;
            frmOperator.Show();
            FormOperator.startButtonPushed += StartClick;
            FormOperator.stopButtonPushed += StopClick;
            frmOperator.homeButtonPushed += Home;
            frmOperator.resetUp += ResetUp;
            frmOperator.resetDown += ResetDown;

           // lb_Yield.Text = Properties.Settings.Default.Yield.ToString();
           // lb_OKYield.Text = Properties.Settings.Default.OKYield.ToString();
          //  lb_NGYield.Text = Properties.Settings.Default.NGYield.ToString();


        

            m_MarkJetTaskGroup = new TaskGroup();
           // m_FeederTask = new ComeOut_process("上料托盘任务", m_MarkJetTaskGroup);
          //  m_TransferTask = new Weld_Process("移载任务", m_MarkJetTaskGroup);
           // m_MarkTask = new MarkTask("打标任务", m_MarkJetTaskGroup);
          //  m_JetTask = new Laser_PowerOn_And_Off("喷码任务", m_MarkJetTaskGroup);
            MainControls =  MainControl.Instance();
         ///   MainControlLe = MainControlLeft.Instance();
            // m_BlankingTrayTask = new Feed_process("下料托盘任务",m_MarkJetTaskGroup);

            // m_MarkJetTaskGroup.AddTaskUnit(m_FeederTask);
            //m_MarkJetTaskGroup.AddTaskUnit(m_MarkTask);
            //m_MarkJetTaskGroup.AddTaskUnit(m_TransferTask);
            // m_MarkJetTaskGroup.AddTaskUnit(m_JetTask);
            m_MarkJetTaskGroup.AddTaskUnit(MainControls);
           // m_MarkJetTaskGroup.AddTaskUnit(MainControlLe);
            // m_MarkJetTaskGroup.AddTaskUnit(m_BlankingTrayTask);

            m_MarkJetTaskGroup.StartThread();
            //cbIgnoreCCD.Checked = Properties.Settings.Default.IgnoreCCD;

            MarkCom.MarkReadWrite();

            Methods = new Method();
            //视觉定位系统.FormMain ccdForm = new 视觉定位系统.FormMain();
            //ccdForm.FormBorderStyle = FormBorderStyle.None;
            //ccdForm.TopLevel = false;
            //panelCCD.Controls.Add(ccdForm);
            //ccdForm.Size = panelCCD.Size;
            //ccdForm.Show();
            //try
            //{
            //    EXEToWinform showCCDForm = new EXEToWinform(this.panelCCD, "CCD");
            //    showCCDForm.Start(@"E:\优尔数控软件发布V6.4\ur-soft.exe");
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("激光器软件加载失败");
            //}
            //注册监听
            Weld_Log.Level_Log_CallBack += Level_Log_CallBack;
          
        }
        
        #region 产量统计
    

        #endregion


        #region 界面四个按钮功能
    

        private void ResetDown()
        {
            //   resetTimer.Start();
            resetTimer.Start();
            //  MainModule.FormMain.bResetPress = false;
            if (startHoming)
                return;
            if (resetTimer.TimeUp(2))
            {
                DateSave.Instance().Production.IsStop = false;
                bJetTestStart = false;
                jetTestStep = -1;
                bMarkTestStart = false;
                markTestStep = -1;
                //  Program.ccdStationA.Clear();
                Program.num = 0;
                MainModule.FormMain.bEstop = false;
                //if (IOManage.INPUT("ESTOP").On)
                //{
             
                DateSave.Instance().Production.StationMaterial = false;//工位有无料标志
                // }
                MainControls.StationMaterial = false;//工位有无料标志


                //   m_FeederTask.taskInfo.iTaskStep = 0;
                //    m_FeederTask.taskInfo.bTaskOnGoing = false;
                //m_MarkTask.taskInfo.iTaskStep = 0;
                //  m_MarkTask.taskInfo.bTaskOnGoing = false;
                //   m_TransferTask.taskInfo.iTaskStep = 0;
                //   m_TransferTask.taskInfo.bTaskOnGoing = false;
                //  m_JetTask.taskInfo.iTaskStep = 0;
                //  m_JetTask.taskInfo.bTaskOnGoing = false;
                MainControls.taskInfo.iTaskStep = 0;
                MainControls.taskInfo.bTaskOnGoing = false;

                ControlPlatformLib.Global.logger.Info("长按复位:" + DateTime.Now.ToString("yyyy/MM/dd/ HH : mm : ss"));
            }

        }

        private void ResetUp()
        {
            DateSave.Instance().Production.EStop = false;//急停标志
            MainModule.FormMain.bResetPress = false;
            if (startHoming)
                return;
           // resetTimer.Start();
            if (resetTimer.TimeUp(2))
            {
                DateSave.Instance().Production.IsStop = false;
                bJetTestStart = false;
                jetTestStep = -1;
                bMarkTestStart = false;
                markTestStep = -1;
                //  Program.ccdStationA.Clear();
                Program.num = 0;
                MainModule.FormMain.bEstop = false;
                //if (IOManage.INPUT("ESTOP").On)
                //{
                DateSave.Instance().Production.EStop = false;//急停标志
                DateSave.Instance().Production.StationMaterial = false;//工位有无料标志
                // }
                MainControls.StationMaterial = false;//工位有无料标志


                //   m_FeederTask.taskInfo.iTaskStep = 0;
                //    m_FeederTask.taskInfo.bTaskOnGoing = false;
                //m_MarkTask.taskInfo.iTaskStep = 0;
                //  m_MarkTask.taskInfo.bTaskOnGoing = false;
                //   m_TransferTask.taskInfo.iTaskStep = 0;
                //   m_TransferTask.taskInfo.bTaskOnGoing = false;
                //  m_JetTask.taskInfo.iTaskStep = 0;
                //  m_JetTask.taskInfo.bTaskOnGoing = false;
                MainControls.taskInfo.iTaskStep = 0;
                MainControls.taskInfo.bTaskOnGoing = false;

                ControlPlatformLib.Global.logger.Info("长按复位:" + DateTime.Now.ToString("yyyy/MM/dd/ HH : mm : ss"));
            }
        }
        List<Thread> threadList = new List<Thread>();
        Thread ThreadHome = null;   //测试线程
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (MainModule.FormMain.bAuto)
            {
                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "请先停止设运行");
                MessageBox.Show("请先停止设运行");
                richTextBox1.AppendText("请先停止设运行");
                return;
            }
            MainModule.FormMain.bHomeReady = false;
            if (threadList[0] == null)
            {
             //   threadList[0].Abort();

                ParameterizedThreadStart paramLoopFunc1 = new ParameterizedThreadStart(RunHome);
                threadList[0] = new Thread(paramLoopFunc1);
                threadList[0].IsBackground = true;
                threadList[0].Start(0);

            }
            else if (threadList[0] != null && threadList[0].IsAlive == false)
            {
                threadList[0].Abort();
                ParameterizedThreadStart paramLoopFunc1 = new ParameterizedThreadStart(RunHome);
                threadList[0] = new Thread(paramLoopFunc1);
                threadList[0].IsBackground = true;
                threadList[0].Start(0);
            }
            //     Homing = new Thread(RunHome);



            //if (HomeDoneXY == false || HomeDoneZ == false)
            //{
            //    StartHome = true;
            //}

            //if (Homing != null && Homing.IsAlive == true)
            //{
            //    return;

            //}
            //else
            //{

            //    Homing.IsBackground = true;
            //    MainModule.FormMain.bHomeReady = false;
            //    Homing.Start();
            //}

            //Task tskExecute = new Task(() =>
            //{
            //    RunHome();
            //    while (true)
            //    {
            //        if (MainModule.FormMain.bHomeReady == true)
            //        {
            //            break;
            //        }
            //    }
            //    return;
            //});
            //tskExecute.Start();
        }
        string ReadstfPath = System.Environment.CurrentDirectory + "\\FlowDocument";
        public bool StartHome;
        public bool HomeDoneZ;
        public bool HomeDoneXY;
        public void RunHome(object HubNum)
        {
            MainModule.FormMain.bHomeReady = false;
            TableManage.TableDriver("运动平台").Home(TableAxisName.Z);
            Thread.Sleep(200);
            while (true)
            {
                if (DateSave.Instance().Production.EStop==true)
                {
                    threadList[0].Abort();
                }
                if (TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Z))
                {
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[运动平台],Z轴回原完成");
                    break;
                }
            }
            if (DateSave.Instance().Production.WeldOther == 1)
            {
                TableManage.TableDriver("运动平台").Home(TableAxisName.X);
                TableManage.TableDriver("运动平台").Home(TableAxisName.Y);
                Thread.Sleep(200);
                //  TableManage.TableDriver("运动平台").Home(TableAxisName.Z);
                while (true)
                {
                    if (DateSave.Instance().Production.EStop == true)
                    {
                        threadList[0].Abort();
                    }
                    if (TableManage.TableDriver("运动平台").HomeDone(TableAxisName.X) && TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Y) /*&& TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Z)*/)
                    {
                        Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[运动平台],XY轴回原完成");
                        break;
                    }
                }
                MainModule.FormMain.bHomeReady = true;
                threadList[0].Abort();

            }
            else
            {
                TableManage.TableDriver("运动平台").Home(TableAxisName.X);
                TableManage.TableDriver("运动平台").Home(TableAxisName.Y);
               
                  TableManage.TableDriver("运动平台").Home(TableAxisName.U);
                while (true)
                {
                    if (DateSave.Instance().Production.EStop == true)
                    {
                        threadList[0].Abort();
                    }
                    if (TableManage.TableDriver("运动平台").HomeDone(TableAxisName.X) && TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Y) && TableManage.TableDriver("运动平台").HomeDone(TableAxisName.U))
                    {
                        Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[运动平台],XY轴回原完成");
                        break;
                    }
                }
                MainModule.FormMain.bHomeReady = true;
                threadList[0].Abort();
            }
        }

        private void Home()
        {
            if (MainModule.FormMain.bAuto)
            {
                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "请先停止设运行");
                MessageBox.Show("请先停止设运行");
                richTextBox1.AppendText("请先停止自动运行");
                return;
            }
            MainModule.FormMain.bHomeReady = false;
            if (threadList[0] == null)
            {
             //   threadList[0].Abort();
                ParameterizedThreadStart paramLoopFunc1 = new ParameterizedThreadStart(RunHome);
                threadList[0] = new Thread(paramLoopFunc1);
                threadList[0].IsBackground = true;
                threadList[0].Start(0);

            }
            else if (threadList[0] != null && threadList[0].IsAlive == false)
            {
                threadList[0].Abort();
                ParameterizedThreadStart paramLoopFunc1 = new ParameterizedThreadStart(RunHome);
                threadList[0] = new Thread(paramLoopFunc1);
                threadList[0].IsBackground = true;
                threadList[0].Start(0);
            }

            if (DateSave.Instance().Production.EStop == true)
            {
                DateSave.Instance().Production.EStop = false;
            }


            //else
            //{

            //    DateSave.Instance().Production.EStop = false;
            //}
            // DateSave.Instance().Production.EStop = true;
        }
        private void Level_Log_CallBack(string callBackStr)
        {
            try
            {
                richTextBox1.Invoke(new MethodInvoker(delegate
                {
                    richTextBox1.Text = richTextBox1.Text + callBackStr + "\r\n";
                    richTextBox1.Focus();
                    richTextBox1.Select(richTextBox1.TextLength, 0);
                    richTextBox1.ScrollToCaret();
                    if (richTextBox1.Lines.Count() > 1000)
                    {
                        richTextBox1.Text = "";
                    }
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show("ATSLOG  " + e.ToString());
            }
        }
        public bool isRunFinish = false;
        public int count = 0;
        private void StartClick()
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }

            if (DateSave.Instance().Production.EStop == true)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("急停标志位未复位！");
                return;
            }
            if (DateSave.Instance().Production.StationMaterial == true)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("工位有料请先排料，并清除工位记忆！");
                return;
            }           
            MainModule.FormMain.bStartPress = true;
           // RunClass.Instance().Stop = false;
         //   RunClass.Instance().AxisR.Stop = false;
          //  RunClass.Instance().ClinderR.Stop= false;
          //  RunClass.Instance().Meth.Stop = false;
            DateSave.Instance().Production.IsStop = false;
            if (!MainModule.FormMain.bAuto)
            {
                if (RunClass.Instance().Run_OneCase!=null && RunClass.Instance().Run_OneCase.IsAlive==true)
                {
                    RunClass.Instance().Run_OneCase.Abort();
                }
             
                RunClass.Instance().parse = false;
                RunClass.Instance().GoOnRun = false;//继续运行标志位
                RunClass.Instance().StartRun = false;
                MainControls. taskInfo.iTaskStep =8;
                MainModule.FormMain.bAuto = true;
                ControlPlatformLib.Global.logger.Info("自动运行开始:" + DateTime.Now.ToString("yyyy/MM/dd/ HH : mm : ss"));
            }
        }
        private void StopClick()
        {
            //ComeOut.parse = false;
            isRunFinish = false;
            count = 0;
    
            TableManage.TableDriver("运动平台").Stop(TableAxisName.X);
            TableManage.TableDriver("运动平台").Stop(TableAxisName.Y);
            TableManage.TableDriver("运动平台").Stop(TableAxisName.Z);
           // RunClass.Instance().Stop = true;
            RunClass.Instance().RunClass_IsFinish = false;
            RunClass.Instance().StartRun = false;
            DateSave.Instance().Production.IsStop = true;
            MainControls. taskInfo.iTaskStep =(int)MainControl.flowCharNew.焊接进料流程;
        }
        #endregion


        #region 功能需求
   
        private object markTestObj = new object();
    
        #endregion
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
        bool setOk = false;
        #region 防呆防撞
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            if (DateSave.Instance().Production.StationMaterial == false )
            {
                btn_LeftPosWelding.Text = "工位有料";

            }
            if ((DateSave.Instance().Production.SN != "" && LeftSnshow.Text == "" )|| (LeftSnshow.Text != DateSave.Instance().Production.SN))
            {
                LeftSnshow.Text = DateSave.Instance().Production.SN;
          
            }
            if ((DateSave.Instance().Production.OK_date != 0 && lb_LeftOK.Text == "") || (lb_LeftOK.Text != DateSave.Instance().Production.OK_date.ToString()))
            {
                lb_LeftOK.Text = DateSave.Instance().Production.OK_date.ToString();
               
            }
            if ((DateSave.Instance().Production.NG_date != 0 && lb_LeftNG.Text == "") || (lb_LeftNG.Text != DateSave.Instance().Production.NG_date.ToString()))
            {
                lb_LeftNG.Text = DateSave.Instance().Production.NG_date.ToString();
       
            }
            if ((DateSave.Instance().Production.CTtime != "" && lb_LeftCT.Text == "") || (lb_LeftCT.Text != DateSave.Instance().Production.CTtime))
            {
                
                lb_LeftCT.Text = DateSave.Instance().Production.CTtime;
            }
            if (DateSave.Instance().Production.ModelNo != "" && 当前机种号.Text == "NUM" || 当前机种号.Text != DateSave.Instance().Production.ModelNo)
            {
                当前机种号_.Text = DateSave.Instance().Production.ModelNo;
                当前机种号.Text = DateSave.Instance().Production.ModelNo;
            }
            if (Socket_server.Instance().recvDate != "")
            {
                lb_RightMarkSpeed.Text = Socket_server.Instance().recvDate;

            }

            if ((DateSave.Instance().Production.Current_TIME != 0 && 清理铜嘴次数.Text == "") || (清理铜嘴次数.Text != DateSave.Instance().Production.Current_TIME.ToString()))
            {
                清理铜嘴次数.Text = DateSave.Instance().Production.Current_TIME.ToString();

            }
            if (DateSave.Instance().Production.Clear_TIME == DateSave.Instance().Production.Current_TIME)
            {
              MainControl.BIZZ("请清理铜嘴", "请清理铜嘴");
                DateSave.Instance().Production.Current_TIME++;
            }

         
            if (frist == 0 && LoginForm.landingFinish == true)
            {
                frist++;
                asc.AddControl(panel2);
            }
            if (frist == 1)
            {
                this.Top = 0;

                this.Left = 0;

              

                frist++;
                delay(1000);
                int Width = Screen.PrimaryScreen.WorkingArea.Width;
                int Height = Screen.PrimaryScreen.WorkingArea.Height;
                int wih = MainModule.FormMain.Width;
                int aq1e = panel2.Width;
                int aqe = panel2.Height;

                panel2.SetBounds(asc.oldCtrl[0].Top, asc.oldCtrl[0].Left, Width, aqe);
                Thread.Sleep(100);
                if (asc.oldCtrl.Count > 0)
                {
                    asc.controlAutoSize(panel2);

                }        
            }
            double sd = 0.0;
            TableManage.TableDriver("运动平台")._GetAdc(1, out sd);
            if (startHoming || MainModule.FormMain.bAuto)
            {
            }
            else if (IOManage.INPUT("手轮X").On || IOManage.INPUT("手轮Y").On || IOManage.INPUT("手轮Z").On || IOManage.INPUT("手轮U").On)
            {
                if (IOManage.INPUT("手轮X").On && setOk == false)
                {
                    if (IOManage.INPUT("手轮1").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(1, 1);
                    }
                    else if (IOManage.INPUT("手轮10").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(1, 10);
                    }
                    else if (IOManage.INPUT("手轮100").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(1, 50);
                    }
                }
                if (IOManage.INPUT("手轮Y").On && setOk == false)
                {
                    if (IOManage.INPUT("手轮1").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(2, 1);
                    }
                    else if (IOManage.INPUT("手轮10").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(2, 10);
                    }
                    else if (IOManage.INPUT("手轮100").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(2, 50);
                    }
                }
                if (IOManage.INPUT("手轮Z").On && setOk == false)
                {
                    if (IOManage.INPUT("手轮1").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(3, 1);
                    }
                    else if (IOManage.INPUT("手轮10").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(3, 10);
                    }
                    else if (IOManage.INPUT("手轮100").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(3, 50);
                    }
                }

                if (IOManage.INPUT("手轮U").On && setOk == false)
                {
                    if (IOManage.INPUT("手轮1").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(4, 1);
                    }
                    else if (IOManage.INPUT("手轮10").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(4, 10);
                    }
                    else if (IOManage.INPUT("手轮100").On)
                    {
                        setOk = true;
                        TableManage.TableDriver("运动平台").StartManualPulser(4, 50);
                    }
                }
            }
            else if (IOManage.INPUT("手轮X").Off && IOManage.INPUT("手轮Y").Off && IOManage.INPUT("手轮Z").Off&& setOk==true)
            {
                setOk = false;
                TableManage.TableDriver("运动平台").StopManualPulser(0);
                TableManage.TableDriver("运动平台").StopManualPulser(1);
                TableManage.TableDriver("运动平台").StopManualPulser(2);
                TableManage.TableDriver("运动平台").StopManualPulser(3);
                TableManage.TableDriver("运动平台").StopManualPulser(4);
                TableManage.TableDriver("运动平台").StopManualPulser(5);
                TableManage.TableDriver("运动平台").StopManualPulser(6);
                TableManage.TableDriver("运动平台").StopManualPulser(7);
       
            }
            if (MainModule.FormMain.Parse == false && RunClass.Instance().parse == true)
            {
                RunClass.Instance().parse = false;
                RunClass.Instance().GoOnRun = true;
            }

            // if (IOManage.INPUT("文档状态").On)
            //{
            //    RunClass.Instance(). WeldFinishSta = "StartWeld";

            //}               
            //    if (IOManage.INPUT("文档状态").Off)
            //    {

            //    RunClass.Instance().WeldFinishSta = "WeldFinish";

            //    }


            //if (StartHome==true)
            //{
            //    MainModule.FormMain.bHomeReady = false;
            //    StartHome = false;
            //    MainModule.FormMain.bHomeReady = false;
            //    StartHome = false; HomeDoneZ = false;
            //    TableManage.TableDriver("运动平台").Home(TableAxisName.Z);


            //}

            //if (TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Z) && HomeDoneZ == false)
            //{
            //    HomeDoneZ = true;
            //    HomeDoneXY = false;
            //}

            //if (HomeDoneXY == false)
            //{
            //    HomeDoneXY = true;
            //    TableManage.TableDriver("运动平台").Home(TableAxisName.X);
            //    TableManage.TableDriver("运动平台").Home(TableAxisName.Y);
            //}
            //if (TableManage.TableDriver("运动平台").HomeDone(TableAxisName.X) && TableManage.TableDriver("运动平台").HomeDone(TableAxisName.Y)&& HomeDoneXY == false)
            //{
            //    HomeDoneXY = true;
            //    MainModule.FormMain.bHomeReady = true;
            //}
            //string en = "";
            //string sendd = "";
            //sendd = Weld_Log.Instance().Dequeue(en);
            //if (sendd != "")
            //{
            //    Weld_Log.Instance().WriteLog(LOG_LEVEL.LEVEL_3, sendd, LOG_TYPE.INFO);

            //}
            //else
            //{

            //    // Thread.Sleep(200);
            //}
            if (MainModule.FormMain.bEstop==true)
            {
                DateSave.Instance().Production.EStop = true;


            }
            //else
            //{

            //    DateSave.Instance().Production.EStop = false;
            //}
        }
        public void setStartManualPulserOperation(string IOAsix,string Speed)
        {
            if (IOAsix=="手轮X")
            {
                if (Speed == "手轮1")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(1, 1);
                }
                else if (Speed == "手轮10")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(1, 10);
                }
                else if (Speed == "手轮100")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(1, 50);
                }
            }
            if (IOAsix == "手轮Y")
            {
                if (Speed == "手轮1")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(2, 1);
                }
                else if (Speed == "手轮10")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(2, 10);
                }
                else if (Speed == "手轮100")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(2, 50);
                }
            }
            if (IOAsix == "手轮Z")
            {
                if (Speed == "手轮1")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(3, 1);
                }
                else if (Speed == "手轮10")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(3, 10);
                }
                else if (Speed == "手轮100")
                {
                    TableManage.TableDriver("运动平台").StartManualPulser(3, 50);
                }
            }
        }
        public int frist = 0;
        private void 手动夹料_Click(object sender, EventArgs e)
        {
            //Task tskExecute = new Task(() =>
            //{
            //  //  FeedTask.Run();
            //    while (true)
            //    {
            //        if (FeedTask.IsFinish == true)
            //        {
            //            isRunFinish = false;
            //            break;
            //        }

            //    }
            //    return;
            //});
            //tskExecute.Start();
        }
        手动操作窗体 form = new 手动操作窗体(); 
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            form.Show();
        }
        波形展示 form1 = new 波形展示();
        private void 波形界面_Click(object sender, EventArgs e)
        {
            //if (!MainModule.FormMain.bHomeReady)
            //{
            //    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
            //    return;
            //}
            form1.Show();
        }
        private static TaskGroup m_WeldingTask= new TaskGroup();
        private void btn_ClearLeftYield_Click(object sender, EventArgs e)
        {
            DateSave.Instance().Production.Date_Clear(0);

            DateSave.Instance().SaveDoc();
           //m_WeldingTask.AddRunMessage("打标任务0，任务开始。");
        }

        private void btn_ClearRightYield1_Click(object sender, EventArgs e)
        {
        
            //MainControls.ProductionData.Production.SN = "12233";
            //MainControls.ProductionData.Production.OK_date = 132;
            //MainControls.ProductionData.Production.NG_date = 14523;

            //Socket_server.Instance().open() ;
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {


            IOManage.OUTPUT("三色灯红").SetOutBit(false);
            IOManage.OUTPUT("三色灯黄").SetOutBit(false);
            IOManage.OUTPUT("三色灯绿").SetOutBit(false);
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            Socket_server.Instance().sendDataToMac("VERSION");
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            byte[] buf = Encoding.UTF8.GetBytes("333333" + "\r\n");
            Socket_client.Instance().SendData(buf);
            // Socket_client.Instance().openstarte();
        }
        #endregion
    }
}
