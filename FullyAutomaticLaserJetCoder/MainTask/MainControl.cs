
using ControlPlatformLib;
/**
* 命名空间:  FullyAutomaticLaserJetCoder.MainTask
* 功 能   ： N/A
* 类 名   ： BlankingTask
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-10 09:01:44
* 负责人  :  wuchenjie 
* 变更内容:
* Copyright (c) 2018 Sunwoda Corporation. All rights reserved.
*┌───────────────────────────────┐
*│此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露│
*│版权所有：欣旺达电气技术有限公司 　　　　　　　　　　　　　　 │
*└───────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    public partial class MainControl : TaskUnit
    {
        public string ReadstfPath = System.Environment.CurrentDirectory + "\\FlowDocument";
        private static TaskGroup m_WeldingTaskGroup = new TaskGroup();
        private static MainControl MainControl_run;
        public static MainControl Instance()
        {
            if (MainControl_run == null)
            {
                MainControl_run = new MainControl("",m_WeldingTaskGroup);
            }
            return MainControl_run;
        }
      
        mes mes = mes.Instance();
        private object lockObj = new object();
        public bool bNG = false;
        private WorldGeneralLib.HiPerfTimer ctTimer;
        public TestDateCom TestDateC = new TestDateCom();
        public readonly ClinderRun ClinderR = ClinderRun.Instance();
        public readonly AxisRun AxisR = AxisRun.Instance();
        public MainControl(string name, TaskGroup taskGroup) : base(name, taskGroup)
        {
            Socket_server.Instance().open();
            Loaddate();
            ctTimer = new WorldGeneralLib.HiPerfTimer();
        }
        public int RunCount = 1;
        public string SCANcom = "";
        public int NGcount = 0;
        public int OKcount = 0;
        string snflat = "";
        public Dictionary<string, List<string>> listEnum = new Dictionary<string, List<string>>();
        public void Loaddate()
        {
            DateSave.DateSav = DateSave.Instance().LoadObj();
            DateSave.Instance().Production.Empty_run = false;
            DateSave.Instance().Production.IsStop = false;
          //  DateSave.Instance().Production.StationMaterial = false;
        }
        public enum flowCharNew
        {
            任务开始,
            初始化任务,
            与线体PLC对接进料信号,
        
            进料任务,
            扫码,
            夹料任务,
            MES过站验证任务,
            获取7串8串程序,
            运行7串程序,
            运行8串程序,
            运行整机焊接程序,
            拍照任务,
            测高任务,
            焊接任务,
            松料任务,
            排料任务,
            MES过站任务,
            排料,


            焊接进料流程,
            焊接进料流程完成检测,
            焊接前流程,
            焊接前流程完成检测,
            焊接中流程,
            焊接中流程完成检测,
            焊接后流程,
            焊接后流程完成检测,
            焊接排料流程,
            焊接排料流程完成检测,
            完成,
        }
     
        int weldCount = 1;
        DateTime starttime;
     //   public bool StationMaterial = false;//工位有无料标志位
        public string Weld_Sta = "";
        //下料移栽
        public override void Process()
        {
            try
            {
                lock (lockObj)
               {
                    bool bAutoTrag = false;
                    bool bManualTrag = false;
                    bool bTragCondition = false;
                    if (taskInfo.bTaskAlarm)
                    {
                        if (MainModule.FormMain.bResetPress)
                        {
                            taskInfo.bTaskAlarm = false;
                            Thread.Sleep(10);
                            m_taskTime.Start();
                        }
                        return;
                    }
                    if (!MainModule.FormMain.bAuto)
                        return;
                    bTragCondition = true;
                    bAutoTrag = MainModule.FormMain.bAuto && (!taskInfo.bTaskFinish) && (!taskInfo.bTaskOnGoing);
                    bManualTrag = m_manualStart;
                    switch (taskInfo.iTaskStep)
                    {
                        case (int)flowCharNew.任务开始://任务开始
                            if ((bAutoTrag | bManualTrag) && bTragCondition)
                            {
                                m_taskTime.Start();
                                m_taskGroup.AddRunMessage("打标任务0，任务开始。");
                                taskInfo.bTaskOnGoing = true;
                                taskInfo.iTaskStep = (int)flowCharNew.焊接进料流程;
                                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + "任务开始");
                                //taskInfo.iTaskStep = (int)flowCharNew.与线体PLC对接进料信号;
                            }
                            break;                                                                          
                        case (int)flowCharNew.焊接进料流程:
                            if (RunClass.Instance().StartRun == false && DateSave.Instance().Production.StationMaterial == false)
                            {
                                Weld_Sta = "焊接进料流程";
                                //DateSave.Instance().Production.StationMaterial = true;
                                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + "焊接进料流程");
                                starttime = DateTime.Now;
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接进料流程.csv");
                                m_taskGroup.AddRunMessage("焊接流程，焊接进料流程。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接进料流程完成检测;
                            }
                            else
                            {
                                taskInfo.iTaskStep = (int)flowCharNew.焊接前流程;

                            }
                          //  taskInfo.iTaskStep = 9;
                       
                            break;
                        case (int)flowCharNew.焊接进料流程完成检测:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + "焊接进料流程完成");
                                m_taskGroup.AddRunMessage("焊接流程，焊接进料流程完成。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接前流程;
                            }
                         //   taskInfo.iTaskStep = (int)flowCharNew.焊接进料流程完成检测;
                            break;

                        case (int)flowCharNew.焊接前流程:
                            if (RunClass.Instance().StartRun == false)
                            {
                                Weld_Sta = "焊接前流程";
                                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + "焊接前流程");
                                starttime = DateTime.Now;
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接前流程.csv");
                                m_taskGroup.AddRunMessage("焊接流程，焊接前流程。");
                            }

                         //   taskInfo.iTaskStep = 9;
                            taskInfo.iTaskStep = (int)flowCharNew.焊接前流程完成检测;
                            break;
                        case (int)flowCharNew.焊接前流程完成检测:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + "焊接前流程完成");
                                m_taskGroup.AddRunMessage("焊接流程，焊接前流程完成。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接中流程;
                            }
                        //    taskInfo.iTaskStep = (int)flowCharNew.焊接前流程完成检测;
                            break;
                        case (int)flowCharNew.焊接中流程:
                            if (DateSave.Instance().Production.LeftRun == true)
                            {

                            }
                            else
                            {

                                if (RunClass.Instance().StartRun == false)
                                {
                                    Weld_Sta = "焊接流程";
                                    DateSave.Instance().Production.RightRun = true;
                                    RunClass.Instance().RunClass_IsFinish = false;
                                    RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接流程.csv");
                                    m_taskGroup.AddRunMessage("焊接流程，焊接流程。");
                                }
                                taskInfo.iTaskStep = (int)flowCharNew.焊接中流程完成检测;
                            }
                  
                            break;

                        case (int)flowCharNew.焊接中流程完成检测:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                m_taskGroup.AddRunMessage("焊接流程，焊接前流程完成。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接后流程;
                            }
                          //  taskInfo.iTaskStep = (int)flowCharNew.焊接中流程完成检测;
                            break;

                        case (int)flowCharNew.焊接后流程:
                            if (RunClass.Instance().StartRun == false)
                            {
                                Weld_Sta = "焊接后流程";
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接后流程.csv");
                                m_taskGroup.AddRunMessage("焊接流程，焊接后流程。");
                            }
                            taskInfo.iTaskStep = (int)flowCharNew.焊接后流程完成检测;

                            break;

                        case (int)flowCharNew.焊接后流程完成检测:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                m_taskGroup.AddRunMessage("焊接流程，焊接后流程完成检测。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接排料流程;
                            }
                          //  taskInfo.iTaskStep = (int)flowCharNew.焊接后流程完成检测;
                            break;

                        case (int)flowCharNew.焊接排料流程:
                            if (RunClass.Instance().StartRun == false)
                            {
                                Weld_Sta = "焊接排料流程";
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接排料流程.csv");
                                m_taskGroup.AddRunMessage("焊接流程，焊接排料流程。");
                            }
                            taskInfo.iTaskStep = (int)flowCharNew.焊接排料流程完成检测;


                            break;

                        case (int)flowCharNew.焊接排料流程完成检测:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                DateTime endtime = DateTime.Now;
                                TimeSpan spantime = endtime - starttime;
                                DateSave.Instance().Production.CTtime = spantime.TotalSeconds.ToString();
                                DateSave.Instance().Production.OK_date++;
                                DateSave.Instance().Production.Current_TIME++;
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                m_taskGroup.AddRunMessage("焊接流程，焊接排料流程完成检测。");
                                taskInfo.iTaskStep = (int)flowCharNew.焊接进料流程;
                               // DateSave.Instance().Production.StationMaterial = false;
                            }
                            //  taskInfo.iTaskStep = (int)flowCharNew.焊接排料流程完成检测;
                            break;
                        case 8:
                            if (RunClass.Instance().StartRun == false)
                            {
                                starttime = DateTime.Now;
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接前流程.csv");
                               m_taskGroup.AddRunMessage("焊接流程，焊接前流程。");
                            }

                            taskInfo.iTaskStep = 9;         
                            break;
                        case 9:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                m_taskGroup.AddRunMessage("焊接流程，焊接前流程完成。");
                                taskInfo.iTaskStep = 10;
                            }
                            break;
                        case 10:
                            if (DateSave.Instance().Production.LeftRun == true)
                            {

                            }
                            else
                            {
                            
                                if (RunClass.Instance().StartRun == false)
                                {
                                    DateSave.Instance().Production.RightRun = true;
                                    RunClass.Instance().RunClass_IsFinish = false;
                                    RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接流程.csv");
                                    m_taskGroup.AddRunMessage("焊接流程，焊接流程。");
                                }
                                taskInfo.iTaskStep = 105;
                            }
                            break;
                        case 105:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                DateSave.Instance().Production.RightRun = false;
                                taskInfo.iTaskStep = 106;
                                m_taskGroup.AddRunMessage("焊接流程，焊接流程完成。");
                            }
                            break;
                        case 106:
                            if (RunClass.Instance().StartRun == false)
                            {
                                RunClass.Instance().RunClass_IsFinish = false;
                                RunClass.Instance().runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接后流程.csv");
                                m_taskGroup.AddRunMessage("焊接流程，焊接后流程。");
                            }
                            taskInfo.iTaskStep = 107;
                            break;
                        case 107:
                            if (RunClass.Instance().RunClass_IsFinish == true)
                            {
                                DateTime endtime = DateTime.Now;
                                TimeSpan spantime = endtime - starttime;
                                DateSave.Instance().Production.CTtime = spantime.TotalSeconds.ToString();
                                DateSave.Instance().Production.OK_date++;

                                DateSave.Instance().Production.Current_TIME++;

                               
                                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                                {
                                    RunClass.Instance().Run_OneCase.Abort();
                                }
                                m_taskGroup.AddRunMessage("焊接流程，焊接后流程完成。");
                                taskInfo.iTaskStep = 8;
                            }
                            break;                
                    }
                }
            }
            catch (Exception ex)
            {
              //  m_taskGroup.AddAlarmMessage(string.Format("执行下料流程{0}时出现错误！错误信息：{1}", taskInfo.iTaskStep, ex.Message));
            }
        }
      public   MessageAlarmForm AlarmForm;
        public void BIZZ(string NAME, string ERR)
        {
            AlarmForm = new MessageAlarmForm();
            AlarmForm.FormHint = "";
            Thread Bizz = new Thread(BIZZRun);
            Bizz.IsBackground = true;
            Bizz.Start();
            AlarmForm.ShowForm(NAME, ERR);
        
          //  Bizz.Abort();
            //   GoOnRun = true;
    
        }
        public void BIZZRun()
        {
            RunClass.Instance().Meth.OutPut_One_Run("三色灯红", "true");
            RunClass.Instance().Meth.OutPut_One_Run("三色灯绿", "false");
            while (true)
            {
                RunClass.Instance().Meth.OutPut_One_Run("BIZZ", "true");
                Thread.Sleep(2000);
                RunClass.Instance().Meth.OutPut_One_Run("BIZZ", "false");
                Thread.Sleep(1000);
                //this.BeginInvoke(new EventHandler(delegate
                //{
                    if (AlarmForm.FormHint == "hint")
                {
                    DateSave.Instance().Production.Current_TIME = 0;
                    RunClass.Instance().Meth.OutPut_One_Run("BIZZ", "false");
                    RunClass.Instance().Meth.OutPut_One_Run("三色灯红", "false");
                    RunClass.Instance().Meth.OutPut_One_Run("三色灯绿", "true");
                       // AlarmForm.Hide();
                        break;
                        //  Bizz.Abort();
                    }
                //}));
              
            }

        }
        public void 调高()
        {
            // double ad = MainControls.ProductionData.Production.BaselineSimulation;//、、 获取基准模拟量
            double ad = DateSave.Instance().Production.BaselineSimulation;//、、 获取基准模拟量
            double date = 0.0;
            Thread.Sleep(100);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);//当前模拟量

            double ad12 = DateSave.Instance().Production.Z_AxialDatum;//获取Z基准坐标
            double sf = ad - date;
            if (sf > 0)
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                // double afaf = TableManage.TablePosItem("运动平台", "调高基准点坐标").c;

                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;
                double NeedCurrentZA = CurrentZA - z;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Z,
                            NeedCurrentZA, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisZData.dSpeed);
               //RunClass.Meth.Asix_one_Run("运动平台", "调高基准点坐标", 2, 60000);
            }
            else
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                // double afaf = TableManage.TablePosItem("运动平台", "调高基准点坐标").c;

                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;
                double NeedCurrentZA = CurrentZA+ z;
                TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Z,
                            NeedCurrentZA, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisZData.dSpeed);
              //RunClass.Meth.Asix_one_Run("运动平台", "调高基准点坐标", 2, 60000);

            }

        }

        public double  调高数据()
        {
            double high = 0.0;
            // double ad = MainControls.ProductionData.Production.BaselineSimulation;//、、 获取基准模拟量
            double ad = DateSave.Instance().Production.BaselineSimulation;//、、 获取基准模拟量
            double date = 0.0;
            Thread.Sleep(100);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);//当前模拟量

            double ad12 = DateSave.Instance().Production.Z_AxialDatum;//获取Z基准坐标
            double sf = ad - date;
            if (sf > 0)
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                // double afaf = TableManage.TablePosItem("运动平台", "调高基准点坐标").c;

                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;
                double NeedCurrentZA = CurrentZA - z;
                //TableManage.TableDriver("运动平台").AbsMove(TableAxisName.Z,
                //            NeedCurrentZA, TableManage.tablesDoc.m_tableDictionary["运动平台"].axisZData.dSpeed);

                if (CurrentZA + 5 < NeedCurrentZA && CurrentZA - 5 > NeedCurrentZA)
                {
                    high = NeedCurrentZA;
                }
                else
                {
                    high = 0.0;
                }
             
                //RunClass.Meth.Asix_one_Run("运动平台", "调高基准点坐标", 2, 60000);
            }
            else
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                // double afaf = TableManage.TablePosItem("运动平台", "调高基准点坐标").c;

                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;
                double NeedCurrentZA = CurrentZA + z;

                if (CurrentZA + 5 < NeedCurrentZA && CurrentZA - 5 > NeedCurrentZA)
                {
                    high = NeedCurrentZA;
                }
                else
                {
                    high = 0.0;
                }
                //RunClass.Meth.Asix_one_Run("运动平台", "调高基准点坐标", 2, 60000);
            }
            return high;
        }
        //public string GetSN(string sn)//过站验证
        //{
        //    string sta = "";
        //    //  string ISOK=LoginF.MES.GroupTest();       
        //    //  string ISOK = mes.GroupTest(sn, mes.userCode, mes.deviceCode);//过站验证
        //    string ISOK = mes.CellToolingPlate(sn);
        //    if (ISOK != "")
        //    {
        //        sta = ISOK;
        //    }
        //    else
        //    {
        //        sta = ISOK;
        //    }
        //    return sta;
        //}

        //public string mesIsOk(string sn)//过站验证
        //{
        //    string sta = "";
        //    //  string ISOK=LoginF.MES.GroupTest();       
        //    string ISOK = mes.Group(sn);//过站验证
        //    // string ISOK = mes.CellToolingPlate(sn);
        //    if (ISOK != "")
        //    {
        //        sta = ISOK;
        //    }
        //    else
        //    {
        //        sta = ISOK;
        //    }
        //    return sta;
        //}
        //任务结束信号
        CancellationTokenSource TaskCancelSource = new CancellationTokenSource();
      
        ManualResetEvent ResetEvent = new ManualResetEvent(true);
        public void sd()
        {
            Task tskExecute = new Task(() =>
            {
                //foreach (flowChar ietm in Enum.GetValues(typeof(flowChar))  )
                //{

                //}

            });
            tskExecute.Start();
           
             TaskStatus sfd=   tskExecute.Status;
        }
     

      
    }
}
