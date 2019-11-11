using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    public class AxisRun
    {
        public bool IsStop = false;
        public bool Stop = false;
        private static AxisRun AxisR;
        public static AxisRun Instance()
        {
            if (AxisR == null)
            {
            
                AxisR = new AxisRun();
            }
            return AxisR;
        }
        public bool Asix_z_Run_High(string PlatformName, string Position, double high, double Safe_High_MAX, double Safe_High_min, double MaxHigh, double MinHigh, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double dPosZ = TableManage.TablePosItem(PlatformName, Position).dPosZ;
            if (dPosZ > Safe_High_min && dPosZ < Safe_High_MAX)
            {

            }
            else
            {
                return false;
            }
            double dPosZ1 = dPosZ + high;
            if (high > MinHigh && high < MaxHigh)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Z,
                       dPosZ1, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisZData.dSpeed);
            }
            else
            {
                return false;
            }
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
                if (IsStop == true)
                {
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    break;
                }
            }
            return sta;
        }
       
        public bool Asix_z_Auto_High(string PlatformName, string Position, double high,double Safe_High_MAX ,double  Safe_High_min, double MaxHigh, double MinHigh, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double dPosZ = TableManage.TablePosItem(PlatformName, Position).dPosZ;
            if (high > Safe_High_min && high < Safe_High_MAX)
            {

            }
            else
            {
                return false;
            }
           // double dPosZ1 = dPosZ + high;
            if (high > Safe_High_min && high < Safe_High_MAX)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Z,
                       high, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisZData.dSpeed);   
            }
            else
            {
                return false;
            }
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
         
                if (TableManage.TableDriver(PlatformName).CurrentZ-0.1<high&& TableManage.TableDriver(PlatformName).CurrentZ + 0.1> high)
                {
                    sta = true;
                    break;
                }
                if (IsStop == true)
                {
                    sta = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    sta = true;
                    break;
                }
            }
                return sta;
        }
        public bool Asix_one_Run(string PlatformName, string Position, int Asix_str, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            if (Asix_str == 0)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.X,
                       TableManage.TablePosItem(PlatformName, Position).dPosX,
                       TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed);
            }
            else if (Asix_str == 1)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Y,
                        TableManage.TablePosItem(PlatformName, Position).dPosY,
                        TableManage.tablesDoc.m_tableDictionary[PlatformName].axisYData.dSpeed);
            }
            else if (Asix_str == 2)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Z,
                         TableManage.TablePosItem(PlatformName, Position).dPosZ,
                         TableManage.tablesDoc.m_tableDictionary[PlatformName].axisZData.dSpeed);
            }
            else if (Asix_str == 3)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.U,
                       TableManage.TablePosItem(PlatformName, Position).dPosU,
                       TableManage.tablesDoc.m_tableDictionary[PlatformName].axisUData.dSpeed);
            }

            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (Asix_str == 0)
                {
                    if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.X))
                    {
                        sta = true;
                        break;
                    }
                }
                else if (Asix_str == 1)
                {
                    if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.Y))
                    {
                        sta = true;
                        break;
                    }
                }
                else if (Asix_str == 2)
                {
                    if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.Z))
                    {
                        sta = true;
                        break;
                    }
                }
                else if (Asix_str == 3)
                {
                    if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.U))
                    {
                        sta = true;
                        break;
                    }
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
                if (IsStop == true)
                {
                    sta = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    sta = true;
                    break;
                }
            }
            return sta;
        }
        public bool Asix_Two_Run(string PlatformName, string Position, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;

            TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.X,
                      TableManage.TablePosItem(PlatformName, Position).dPosX,
                      TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed);
            TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Y,
                      TableManage.TablePosItem(PlatformName, Position).dPosY,
                      TableManage.tablesDoc.m_tableDictionary[PlatformName].axisYData.dSpeed);
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.X) && TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.Y))
                {
                    sta = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
                if (IsStop == true)
                {
                    sta = true;
                    //  sta = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    sta = true;
                    // sta = true;
                    break;
                }
            }
            return sta;
        }
        public bool Asix_Line_Run(string PlatformName, string Position, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double X = TableManage.TablePosItem(PlatformName, Position).dPosX;
            double Y = TableManage.TablePosItem(PlatformName, Position).dPosY;
            bool LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(50, 50, 250000, X, Y, 500);
            for (int i = 0; i < 3; i++)
            {
                if (LineXYZMove == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(10);
                    LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(50, 50, 250000, X, Y, 500);
                }
            }
            TableManage.TableDriver(PlatformName).StartCure(false);
            int iStep1 = 0;

            TableManage.TableDriver(PlatformName).CureMoveDone(out iStep1);
            if (iStep1 == 0)
            {
            }
            else
            {
            }
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                double CurrentX = TableManage.TableDriver(PlatformName).CurrentX;
                double CurrentY = TableManage.TableDriver(PlatformName).CurrentY;
                if ((X - 0.01 < CurrentX && X + 0.01 > CurrentX) && (Y - 0.01 < CurrentY && Y + 0.01 > CurrentY))
                {
                    sta = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
                if (IsStop == true)
                {
                    sta = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    sta = true;
                    break;
                }
            }
            return sta;

        }
        public bool Asix_Arc_Run(string PlatformName, string Position, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double X = TableManage.TablePosItem(PlatformName, Position).dPosX;
            double Y = TableManage.TablePosItem(PlatformName, Position).dPosY;
            bool ArcXYMove = TableManage.TableDriver(PlatformName).ArcMove(0.50, 0.50, 0.50, X, Y, 5, 0, (CoordinateType)0);
            for (int i = 0; i < 3; i++)
            {
                if (ArcXYMove == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(10);
                    ArcXYMove = TableManage.TableDriver(PlatformName).ArcMove(0.50, 0.50, 0.50, X, Y, 5, 0, (CoordinateType)0);
                }
            }
            bool ArcXYMove_run = TableManage.TableDriver(PlatformName).StartCure(false);
            if (ArcXYMove_run == true)
            {
                ArcXYMove_run = true;

            }
            else
            {
                ArcXYMove_run = false;
                return ArcXYMove_run;

            }
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (spantime.TotalMilliseconds > stime)
                {
                    sta = false;
                    break;
                }
                int iStep = 0;
                TableManage.TableDriver(PlatformName).CureMoveDone(out iStep);
                if (iStep == 0)
                {
                    sta = true;
                    break;
                }
                if (IsStop == true)
                {
                    sta = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    sta = true;
                    break;
                }
            }
            return sta;
        }



        //public override void Process()
        //{
        //    double X = 0.0;
        //    double Y = 0.0;
        //    try
        //    {
        //        lock (lockObj)
        //        {

        //            bool bAutoTrag = false;
        //            bool bManualTrag = false;
        //            bool bTragCondition = false;
        //            if (taskInfo.bTaskAlarm)
        //            {
        //                if (MainModule.FormMain.bResetPress)
        //                {
        //                    taskInfo.bTaskAlarm = false;
        //                    Thread.Sleep(10);
        //                    m_taskTime.Start();
        //                }
        //                return;
        //            }
        //            if (!MainModule.FormMain.bAuto)
        //                return;
        //            bTragCondition = true;
        //            bAutoTrag = MainModule.FormMain.bAuto && (!taskInfo.bTaskFinish) && (!taskInfo.bTaskOnGoing);
        //            bManualTrag = m_manualStart;
        //            switch (taskInfo.iTaskStep)
        //            {
        //                case (int)flowCharNew.任务开始://任务开始
        //                    if ((bAutoTrag | bManualTrag) && bTragCondition)
        //                    {
        //                        m_taskTime.Start();
        //                        m_taskGroup.AddRunMessage("打标任务0，任务开始。");
        //                        taskInfo.bTaskOnGoing = true;
        //                        taskInfo.iTaskStep = 9;
        //                        //taskInfo.iTaskStep = (int)flowCharNew.与线体PLC对接进料信号;
        //                    }
        //                    break;
        //                //case (int)flowCharNew.与线体PLC对接进料信号://任务开始
        //                //    while (true)
        //                //    {
        //                //        if (true)
        //                //        {
        //                //            taskInfo.iTaskStep = (int)flowCharNew.进料任务;
        //                //            break;
        //                //        }                        
        //                //    }                                                     
        //                //   break;
        //                //case (int)flowCharNew.进料任务://任务开始   //轴运动到安全位-进料，气缸夹紧
        //                //    Task tskExecute = new Task(() =>
        //                //    {
        //                //        ComeOut.Run();
        //                //        while (true)
        //                //        {
        //                //            if (ComeOut.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.扫码;
        //                //                break;
        //                //            }

        //                //        }
        //                //    });
        //                //    tskExecute.Start();
        //                //    break;                 
        //                //case (int)flowCharNew.扫码://扫码成功后显示在界面
        //                //    TestDateC.Open(SCANcom, 115200, "2", 8, "1");
        //                //    Thread.Sleep(100);
        //                //    if (TestDateC.flag == true && snflat == "")
        //                //    {
        //                //        TestDateC.DataReceivedstr = "";
        //                //        TestDateC.senddatetotest("T");
        //                //        Thread.Sleep(500);
        //                //        if (TestDateC.DataReceivedstr != "" && !TestDateC.DataReceivedstr.Contains("ERROR"))
        //                //        {
        //                //            taskInfo.iTaskStep = (int)flowCharNew.MES过站验证任务;
        //                //            snflat = "A";
        //                //            //显示到界面函数

        //                //        }
        //                //    }
        //                //    else
        //                //    {
        //                //        m_taskGroup.AddAlarmMessage("扫码失败，请检查扫码枪");                              
        //                //    }
        //                //    break;
        //                //case (int)flowCharNew.MES过站验证任务:
        //                //    string isok = mesIsOk(TestDateC.DataReceivedstr.Replace("\r\n", ""));
        //                //    if (isok == "OK")
        //                //    {
        //                //        taskInfo.iTaskStep = (int)flowCharNew.获取7串8串程序;
        //                //        //  DisplayLogOnWindow("MES验证结果", isok);
        //                //    }
        //                //    else
        //                //    {
        //                //        taskInfo.iTaskStep = (int)flowCharNew.MES过站验证任务;
        //                //        ComeOut.BIZZ("","");
        //                //    }                      
        //                //    break;
        //                //case (int)flowCharNew.获取7串8串程序:
        //                //    if (TestDateC.DataReceivedstr[4].ToString() == "A" && TestDateC.DataReceivedstr.Length > 5)
        //                //    {
        //                //        taskInfo.iTaskStep = (int)flowCharNew.运行7串程序;
        //                //    }
        //                //    else if (TestDateC.DataReceivedstr[4].ToString() == "B" && TestDateC.DataReceivedstr.Length > 5)
        //                //    {
        //                //        taskInfo.iTaskStep = (int)flowCharNew.运行8串程序;
        //                //    }
        //                //    else
        //                //    {
        //                //        taskInfo.iTaskStep = (int)flowCharNew.运行整机焊接程序;

        //                //    }
        //                //    break;
        //                //case (int)flowCharNew.运行整机焊接程序:
        //                //    Task tskExecute3 = new Task(() =>
        //                //    {
        //                //    Weld.Run();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.松料任务;
        //                //                break;
        //                //            }

        //                //        }
        //                //    });
        //                //    tskExecute3.Start();
        //                //    break;
        //                //case (int)flowCharNew.运行7串程序:
        //                //    Task tskExecute1 = new Task(() =>
        //                //    {
        //                //       Weld.Run_7();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.松料任务;
        //                //                break;
        //                //            }

        //                //        }
        //                //    });
        //                //    tskExecute1.Start();

        //                //    break;
        //                //case (int)flowCharNew.运行8串程序:
        //                //    Task tskExecute2 = new Task(() =>
        //                //    {
        //                //        Weld.Run_8();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.松料任务;
        //                //                break;
        //                //            }

        //                //        }
        //                //    });
        //                //    tskExecute2.Start();          
        //                //    break;
        //                //case (int)flowCharNew.松料任务:
        //                //    Task tskExecute4 = new Task(() =>
        //                //    {
        //                //       Weld.Run_8();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.MES过站任务;
        //                //                break;
        //                //            }

        //                //        }
        //                //    });
        //                //    tskExecute4.Start();
        //                //    break;
        //                //case (int)flowCharNew.MES过站任务:
        //                //    Task tskExecute6 = new Task(() =>
        //                //    {
        //                //       Weld.Run_8();

        //                //    });
        //                //    taskInfo.iTaskStep = (int)flowCharNew.排料任务;
        //                //    tskExecute6.Start();
        //                //    break;
        //                //case (int)flowCharNew.排料任务:
        //                //    Task tskExecute5 = new Task(() =>
        //                //    {

        //                //     Weld.Run_8();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.完成;
        //                //                break;
        //                //            }
        //                //        }
        //                //    });                  
        //                //    tskExecute5.Start();
        //                //    break;
        //                //case (int)flowCharNew.完成://对信号初始化  设备的运行状态与设备可入料
        //                //    Task tskExecute8 = new Task(() =>
        //                //    {
        //                //     Weld.Run_8();
        //                //        while (true)
        //                //        {
        //                //            if (Weld.IsFinish == true)
        //                //            {
        //                //                taskInfo.iTaskStep = (int)flowCharNew.与线体PLC对接进料信号;
        //                //                break;
        //                //            }
        //                //        }
        //                //    });
        //                //    tskExecute8.Start();
        //                //    break;







        //                //case (int)flowChar.运行整机焊接程序:
        //                //    RunCount = 3;

        //                //    break;
        //                //case (int)flowChar.工装板顶升上升:
        //                //    if (m_taskTime.TimeUp(30))
        //                //    {
        //                //        taskInfo.bTaskAlarm = true;
        //                //         m_taskGroup.AddAlarmMessage("工装板顶升上升，工装板下降下限位异常！");
        //                //        m_taskTime.Start();
        //                //        taskInfo.iTaskStep = (int)flowChar.报警;
        //                //    }
        //                //    else 
        //                //    {
        //                //        if (IOManage.INPUT("工装板下降下限位").On&& IOManage.INPUT("Y轴后模组定位下").On && IOManage.INPUT("Y轴前模组顶升下").On)
        //                //        {
        //                //            IOManage.OUTPUT("Y轴后模组定位上").SetOutBit(true);
        //                //            IOManage.OUTPUT("Y轴前模组顶升上").SetOutBit(true);
        //                //            IOManage.OUTPUT("工装板顶升上升").SetOutBit(true);
        //                //        }
        //                //    }
        //                //    break;
        //                case 9:
        //                    if (RunClass.StartRun == false)
        //                    {
        //                        RunClass.RunClass_IsFinish = false;
        //                        //  ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "复位流程.csv"
        //                        RunClass.runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接前流程.csv");
        //                        //Thread.Sleep(500);
        //                    }
        //                    taskInfo.iTaskStep = 8;
        //                    //if (RunClass.RunClass_IsFinish == true)
        //                    //{
        //                    //    taskInfo.iTaskStep = 10;
        //                    //}
        //                    break;
        //                case 8:
        //                    //if (RunClass.StartRun == false)
        //                    //{
        //                    //    RunClass.RunClass_IsFinish = false;
        //                    //    RunClass.runTask(ReadstfPath + "\\" + "7串8串" + "\\" + "ComeOut_process_Flow.csv");
        //                    //    Thread.Sleep(500);
        //                    //}

        //                    if (RunClass.RunClass_IsFinish == true)
        //                    {
        //                        if (RunClass.Run_OneCase.IsAlive == true)
        //                        {
        //                            RunClass.Run_OneCase.Abort();
        //                        }
        //                        taskInfo.iTaskStep = 10;
        //                    }
        //                    break;
        //                case 10:
        //                    X = TableManage.TablePosItem("运动平台", "焊接" + weldCount + "#点坐标").dPosX;
        //                    Y = TableManage.TablePosItem("运动平台", "焊接" + weldCount + "#点坐标").dPosY;

        //                    bool LineXYZMove = TableManage.TableDriver("运动平台").LineXYZMove(50, 50, 250000, X, Y, 500);
        //                    for (int i = 0; i < 3; i++)
        //                    {
        //                        if (LineXYZMove == true)
        //                        {
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            Thread.Sleep(100);
        //                            LineXYZMove = TableManage.TableDriver("运动平台").LineXYZMove(50, 50, 250000, X, Y, 500);
        //                        }

        //                    }
        //                    TableManage.TableDriver("运动平台").StartCure(false);
        //                    int iStep1 = 0;
        //                    TableManage.TableDriver("运动平台").CureMoveDone(out iStep1);
        //                    if (iStep1 == 0)
        //                    {

        //                        taskInfo.iTaskStep = 105;
        //                    }
        //                    else
        //                    {
        //                    }
        //                    //double sd = 0.0;
        //                    //TableManage.TableDriver("运动平台")._GetAdc(1, out sd);

        //                    break;

        //                case 105:
        //                    taskInfo.iTaskStep = 200;
        //                    break;
        //                case 200:
        //                    // Thread.Sleep(1000);

        //                    double xdfsd = TableManage.TablePosItem("运动平台", "焊接" + weldCount + "#点坐标").dPosX;
        //                    double xddb = TableManage.TablePosItem("运动平台", "焊接" + weldCount + "#点坐标").dPosY;
        //                    TableManage.TableDriver("运动平台").ArcMove(0.50, 0.50, 0.50, xdfsd, xddb, 5, 0, (CoordinateType)0);
        //                    TableManage.TableDriver("运动平台").StartCure(false);

        //                    taskInfo.iTaskStep = 201;




        //                    break;
        //                case 201:

        //                    if (weldCount > 10)
        //                    {
        //                        int iStep = 0;
        //                        TableManage.TableDriver("运动平台").CureMoveDone(out iStep);
        //                        if (iStep == 0)
        //                        {
        //                            weldCount = 1;
        //                            taskInfo.iTaskStep = 106;
        //                        }
        //                        else
        //                        {
        //                        }
        //                    }
        //                    else
        //                    {
        //                        int iStep = 0;
        //                        TableManage.TableDriver("运动平台").CureMoveDone(out iStep);
        //                        if (iStep == 0)
        //                        {
        //                            weldCount++;
        //                            taskInfo.iTaskStep = 10;
        //                        }
        //                        else
        //                        {
        //                        }
        //                    }
        //                    break;
        //                case 106:
        //                    if (RunClass.StartRun == false)
        //                    {
        //                        RunClass.RunClass_IsFinish = false;
        //                        RunClass.runTask(ReadstfPath + "\\" + DateSave.Instance().Production.ModelNo + "\\" + "焊接后流程.csv");

        //                    }
        //                    taskInfo.iTaskStep = 107;

        //                    //RunClass.RunClass_IsFinish = false;
        //                    //RunClass.runTask(ReadstfPath + "\\" + "7串8串" + "\\" + "Feed_process_Flow.csv");
        //                    //Thread.Sleep(1000);
        //                    //if (RunClass.RunClass_IsFinish == true)
        //                    //{
        //                    //    taskInfo.iTaskStep = 9;
        //                    //}

        //                    break;
        //                case 107:
        //                    if (RunClass.RunClass_IsFinish == true)
        //                    {
        //                        if (RunClass.Run_OneCase.IsAlive == true)
        //                        {
        //                            RunClass.Run_OneCase.Abort();
        //                        }

        //                        taskInfo.iTaskStep = 9;
        //                    }
        //                    break;
        //                case 110:

        //                    break;
        //                case 120:

        //                    m_taskTime.Start();
        //                    taskInfo.iTaskStep = 130;
        //                    break;
        //                case 130:

        //                    break;
        //                //   case 200:
        //                //if (IOManage.INPUT("下料气缸原位").On
        //                //        && TableManage.TableDriver("下料平台").CurrentZ < TableManage.TablePosItem("下料平台", "OK位低位").dPosZ)//Z轴在OK低位上方
        //                //{
        //                //    m_taskGroup.AddRunMessage("下料任务200，复位下料气缸完成。");
        //                //    m_taskTime.Start();
        //                //    if (IOManage.INPUT("下料负压检测").Off)
        //                //    {
        //                //        m_taskGroup.AddRunMessage("下料任务200，下料轴无料，复位真空信号，走下料等待位。");
        //                //        IOManage.OUTPUT("下料吸真空").SetOutBit(false);
        //                //        //IOManage.OUTPUT("下料破真空").SetOutBit(false);
        //                //        TableManage.TableDriver("下料平台").AbsMove(TableAxisName.Y,
        //                //            TableManage.TablePosItem("下料平台", "下料等待位").dPosY,
        //                //            TableManage.tablesDoc.m_tableDictionary["下料平台"].axisYData.dSpeed);
        //                //        m_taskTime.Start();
        //                //        taskInfo.iTaskStep = 300;
        //                //    }
        //                //    else if (IOManage.INPUT("下料负压检测").On)
        //                //    {
        //                //        //if (Properties.Settings.Default.HasNGPosition)
        //                //        //{
        //                //            m_taskGroup.AddRunMessage("下料任务200，下料轴有料，走NG位。");//开机下料轴上有料当做NG品放到NG位置
        //                //            IOManage.OUTPUT("下料吸真空").SetOutBit(true);
        //                //            //IOManage.OUTPUT("下料破真空").SetOutBit(false);
        //                //            TableManage.TableDriver("下料平台").AbsMove(TableAxisName.Y,
        //                //                TableManage.TablePosItem("下料平台", "下料NG位").dPosY,
        //                //                TableManage.tablesDoc.m_tableDictionary["下料平台"].axisYData.dSpeed);
        //                //            m_taskTime.Start();
        //                //            taskInfo.iTaskStep = 1300;
        //                //        //}
        //                //        //else
        //                //        //{
        //                //        //    taskInfo.bTaskAlarm = true;
        //                //        //    m_taskGroup.AddAlarmMessage("下料任务200，没有NG放料位，下料吸嘴上有料，请先取走！");
        //                //        //    m_taskTime.Start();
        //                //        //    taskInfo.iTaskStep = 200;
        //                //        //}
        //                //    }
        //                //}
        //                //else if(TableManage.TableDriver("下料平台").CurrentZ >= TableManage.TablePosItem("下料平台", "OK位低位").dPosZ)//Z轴在Ok低位下面
        //                //{
        //                //    //if(IOManage.INPUT("下料托盘有料感应器").On)
        //                //    //{
        //                //    //    taskInfo.bTaskAlarm = true;
        //                //    //    m_taskGroup.AddAlarmMessage("下料托盘满料，请下料！");
        //                //    //    m_taskTime.Start();
        //                //    //    taskInfo.iTaskStep = 100;
        //                //    //}
        //                //    //else
        //                //    //{


        //                //    //}
        //                //    m_taskGroup.AddRunMessage("下料任务200，满料，走下料等待位。");
        //                //    m_taskTime.Start();
        //                //    taskInfo.iTaskStep = 100;
        //                //}
        //                //  break;
        //                case 300:
        //                    //if (m_taskTime.TimeUp(30))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务300,报警，走下料位！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    if (TableManage.TableDriver("下料平台").MoveDone(TableAxisName.Y))
        //                    {
        //                        m_taskGroup.AddRunMessage("下料任务300，走到下料等待位。等待物料！");
        //                        m_taskTime.Start();
        //                        taskInfo.iTaskStep = 400;
        //                    }
        //                    break;
        //                // }

        //                case 400:
        //                    //if (IOManage.INPUT("移载托盘有料感应器").On
        //                    //    && TableManage.TableDriver("移载喷码平台").IsOnPos("下料位")
        //                    //    && IOManage.OUTPUT("移载托盘电磁阀").GetOff()
        //                    //    && Program.bNeedBlank)
        //                    ////&& IOManage.INPUT("移载托盘前后气缸原位").On
        //                    ////&& IOManage.INPUT("移载托盘左右气缸原位").On)
        //                    //{
        //                    //    Program.bNeedBlank = false;
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(true);
        //                    //    m_taskGroup.AddRunMessage("下料任务400，可以取料，下料电磁阀取料！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 500;
        //                    //}
        //                    break;
        //                case 500:
        //                    //if (m_taskTime.TimeUp(10))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务500，下料电磁阀伸出超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸到位").On)
        //                    //    {
        //                    //        //IOManage.OUTPUT("下料破真空").SetOutBit(false);
        //                    //        IOManage.OUTPUT("下料吸真空").SetOutBit(true);
        //     m_taskGroup.AddRunMessage("下料任务500，下料气缸到位，吸真空！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 600;
        //                    //    }
        //                    //}
        //                    break;
        //                case 600:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务600，下料吸真空超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料负压检测").On)
        //                    //    {
        //                    //        IOManage.OUTPUT("下料电磁阀").SetOutBit(false);
        //                    //        m_taskGroup.AddRunMessage("下料任务600，吸料到位，下料气缸缩回！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 700;
        //                    //    }
        //                    //}
        //                    break;
        //                case 700:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务700，下料电磁阀缩回超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸原位").On)
        //                    //    {
        //                    //        m_taskGroup.AddRunMessage("下料任务700，下料气缸缩回到位！");
        //                    //        m_taskTime.Start();
        //                    //        if (Program.bLaserNG)
        //                    //        {
        //                    //            //Program.bLaserNG = false;
        //                    //            m_taskGroup.AddRunMessage("下料任务700，打标NG，走NG位！");
        //                    //            TableManage.TableDriver("下料平台").AbsMove(TableAxisName.Y,
        //                    //                TableManage.TablePosItem("下料平台", "下料NG位").dPosY,
        //                    //                TableManage.tablesDoc.m_tableDictionary["下料平台"].axisYData.dSpeed);
        //                    //            taskInfo.iTaskStep = 800;
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            if (Program.bNG)//bNG)//喷码NG
        //                    //            {
        //                    //                Program.bNG = false;
        //                    //                m_taskGroup.AddRunMessage("下料任务700，NG板，走NG位！");
        //                    //                TableManage.TableDriver("下料平台").AbsMove(TableAxisName.Y,
        //                    //                    TableManage.TablePosItem("下料平台", "下料NG位").dPosY,
        //                    //                    TableManage.tablesDoc.m_tableDictionary["下料平台"].axisYData.dSpeed);
        //                    //                taskInfo.iTaskStep = 800;
        //                    //                Program.bNG = false;
        //                    //                taskInfo.bTaskAlarm = true;
        //                    //                m_taskGroup.AddAlarmMessage("下料任务700，下料吸嘴上有NG料，请先取走！");
        //                    //                m_taskTime.Start();
        //                    //                taskInfo.iTaskStep = 200;
        //                    //            }
        //                    //            else
        //                    //            {
        //                    //                m_taskGroup.AddRunMessage("下料任务700，OK板，走OK位！");
        //                    //                TableManage.TableDriver("下料平台").AbsMove(TableAxisName.Y,
        //                    //                     TableManage.TablePosItem("下料平台", "下料OK位").dPosY,
        //                    //                     TableManage.tablesDoc.m_tableDictionary["下料平台"].axisYData.dSpeed);
        //                    //                m_taskTime.Start();
        //                    //                taskInfo.iTaskStep = 750;
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //}
        //                    break;
        //                case 750:
        //                    //if (m_taskTime.TimeUp(60))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务750，走OK位超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (TableManage.TableDriver("下料平台").MoveDone(TableAxisName.Y))
        //                    //    {
        //                    //        m_taskGroup.AddRunMessage("下料任务750，下料轴走到下料OK位，等待下料！");
        //                    //        IOManage.OUTPUT("下料电磁阀").SetOutBit(true);
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 1000;
        //                    //    }
        //                    //}
        //                    break;
        //                case 800:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务800，走NG位超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (TableManage.TableDriver("下料平台").MoveDone(TableAxisName.Y)
        //                    //        && TableManage.TableDriver("下料平台").IsOnPos("下料NG位"))
        //                    //    {
        //                    //        m_taskGroup.AddRunMessage("下料任务800，下料轴走到下料NG位，等待下料！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 900;
        //                    //    }
        //                    //}
        //                    break;
        //                case 900:
        //                    //if (TableManage.TableDriver("下料平台").CurrentY >= TableManage.TablePosItem("下料平台", "NG位低位").dPosY)
        //                    //{
        //                    //    m_taskGroup.AddRunMessage("下料任务900，NG下料托盘可以下料，下料气缸伸出");
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(true);
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 1000;
        //                    //}
        //                    break;
        //                case 1000:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(false);
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务1000，下料气缸伸出超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸到位").On)
        //                    //    {
        //                    //        //IOManage.OUTPUT("下料破真空").SetOutBit(true);
        //                    //        IOManage.OUTPUT("下料吸真空").SetOutBit(false);
        //                    //        m_taskGroup.AddRunMessage("下料任务1000，下料气缸到位，下料破真空！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 1100;
        //                    //    }
        //                    //}
        //                    break;
        //                case 1100:
        //                    //if (m_taskTime.TimeUp(0.2))
        //                    //{
        //                    //    //IOManage.OUTPUT("下料破真空").SetOutBit(false);
        //                    //    IOManage.OUTPUT("下料吸真空").SetOutBit(false);
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(false);
        //                    //    m_taskGroup.AddRunMessage("下料任务1100，下料气缸缩回！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 1200;
        //                    //}
        //                    break;
        //                case 1200:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务1200，下料气缸缩回超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸原位").On)
        //                    //    {
        //                    //        Program.stratForm.AddCT(ctTimer.Duration);
        //                    //        ctTimer.Start();
        //                    //        m_taskGroup.AddRunMessage("下料任务1200，下料气缸缩回到位，走任务200！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 200;
        //                    //    }
        //                    //}
        //                    break;
        //                case 1300:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务1300，走下料OK位超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (TableManage.TableDriver("下料平台").MoveDone(TableAxisName.Y)
        //                    //        && TableManage.TableDriver("下料平台").IsOnPos("下料OK位"))
        //                    //    {
        //                    //        m_taskGroup.AddRunMessage("下料任务1300，走到下料OK位！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 1400;
        //                    //    }
        //                    //}
        //                    break;
        //                case 1400:
        //                    //if (TableManage.TableDriver("下料平台").CurrentZ >= TableManage.TablePosItem("下料平台", "OK位低位").dPosZ
        //                    //    && Program.bCanOKBlanking)
        //                    //{
        //                    //    m_taskGroup.AddRunMessage("下料任务1400，下料OK位可下料！下料气缸伸出！");
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(true);
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 1500;
        //                    //}
        //                    break;
        //                case 1500:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(false);
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务1500，走下料气缸伸出超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸到位").On)
        //                    //    {
        //                    //        //IOManage.OUTPUT("下料破真空").SetOutBit(true);
        //                    //        IOManage.OUTPUT("下料吸真空").SetOutBit(false);
        //                    //        m_taskGroup.AddRunMessage("下料任务1500，下料气缸到位，下料破真空！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 1600;
        //                    //    }
        //                    //}
        //                    break;
        //                case 1600:
        //                    //if (m_taskTime.TimeUp(0.2))
        //                    //{
        //                    //    //IOManage.OUTPUT("下料破真空").SetOutBit(false);
        //                    //    IOManage.OUTPUT("下料吸真空").SetOutBit(false);
        //                    //    IOManage.OUTPUT("下料电磁阀").SetOutBit(false);
        //                    //    m_taskGroup.AddRunMessage("下料任务1600，下料气缸缩回！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 1700;
        //                    //}
        //                    break;
        //                case 1700:
        //                    //if (m_taskTime.TimeUp(3))
        //                    //{
        //                    //    taskInfo.bTaskAlarm = true;
        //                    //    m_taskGroup.AddAlarmMessage("下料任务1700，下料气缸缩回超时！");
        //                    //    m_taskTime.Start();
        //                    //    taskInfo.iTaskStep = 9999;
        //                    //}
        //                    //else
        //                    //{
        //                    //    if (IOManage.INPUT("下料气缸原位").On)
        //                    //    {
        //                    //        Program.stratForm.AddCT(ctTimer.Duration);
        //                    //        ctTimer.Start();
        //                    //        m_taskGroup.AddRunMessage("下料任务1700，下料气缸缩回到位，走任务200！");
        //                    //        m_taskTime.Start();
        //                    //        taskInfo.iTaskStep = 200;
        //                    //    }
        //                    //}
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  m_taskGroup.AddAlarmMessage(string.Format("执行下料流程{0}时出现错误！错误信息：{1}", taskInfo.iTaskStep, ex.Message));
        //    }
        //}
    }
}
