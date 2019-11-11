using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    public class RunClass
    {
        private static TaskGroup m_WeldingTaskGroup = new TaskGroup();
        private static RunClass RunC;
        //  public DateSave ProductionDatA = DateSave.Instance();
        List<Date_save> user_Date_Save =new  List<Date_save>();
        public static RunClass Instance()
        {
            if (RunC == null)
            {
                RunC = new RunClass();
            }
            return RunC;
        }
        KeyValuePair<string, string> sssf = new KeyValuePair<string, string>();
        public List<KeyValuePair<string, KeyValuePair<string, string>>> RunItem_List = new List<KeyValuePair<string, KeyValuePair<string, string>>>();
        public List<KeyValuePair<string, string>> RunItem_List1111 = new List<KeyValuePair<string, string>>();
        public string WeldPlat_Str_Name = "运动平台";
        public string stfPath = System.Environment.CurrentDirectory + "\\FlowDocument\\";
        public AxisRun AxisR = AxisRun.Instance();
        public ClinderRun ClinderR = ClinderRun.Instance();
     //   public static ComeOut_process ComeOut_pro;
        public Method Meth = Method.Instance();

        CancellationTokenSource TaskCancelSource = new CancellationTokenSource();
        CancellationToken CancelToken;
        ManualResetEvent ResetEvent = new ManualResetEvent(true);
        public int delayCheckTime = 6000;
        public RunClass()
        {
            for (int i=0; i < 30; i++)
            {
                Date_save Date_ = new Date_save();
                user_Date_Save.Add(Date_);
            }
           

            // ReadCode(stfPath);
        }

        public bool parse = false;//暂停标志位
        List<string> ListStr = new List<string>();
        public bool RunClass_IsFinish = false;
        public bool Stop = false;//急停
        public bool IsStop = false;//停止
        public bool GoOnRun = false;//继续运行标志位
        public void ReadCode(string path)// Read  code
        {

            List<string> ReadListStr = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                ReadListStr.Add(line);
            }
            for (int i = 1; i < ReadListStr.Count; i++)
            {
                string[] Code = ReadListStr[i].Split(',');
                if (Code[0].Contains("轴运动"))
                {
                    RunItem_List.Add(new KeyValuePair<string, KeyValuePair<string, string>>(Code[0], new KeyValuePair<string, string>(Code[1], Code[2])));
                    RunItem_List1111.Add(new KeyValuePair<string, string>(Code[1], ""));
                    ListStr.Add(Code[1]);

                }
                else
                {
                    RunItem_List.Add(new KeyValuePair<string, KeyValuePair<string, string>>(Code[0], new KeyValuePair<string, string>(Code[1], Code[2])));
                    RunItem_List1111.Add(new KeyValuePair<string, string>(Code[1], Code[2]));
                    ListStr.Add(Code[1]);

                }

            }

            for (int i = 0; i < RunItem_List.Count; i++)
            {
                string sda = RunItem_List[i].Key;
                sssf = RunItem_List[i].Value;
                //  sssf.Key
            }
        }
        public Thread Run_OneCase = null;
        public void runTask(string path)
        {
            RunClass_IsFinish = false;
            bool runFinish = false;
            RunItem_List.Clear();
            ReadCode(path);// Read  code
            Run_OneCase = new Thread(Run);
            Run_OneCase.IsBackground = true;
            Run_OneCase.Start();
            //Task tskExecute = new Task(() =>
            //{
            //    Run();
            //    while (true)
            //    {
            //        if (RunClass_IsFinish == true)
            //        {
            //            runFinish = true;
            //            break;
            //        }

            //    }
            //});
            //tskExecute.Start();
        }
        public bool StartRun = false;
        public void Run()
        {
            HighDate.Clear();
            CamerDate.Clear();
            StartRun = true;
            GoOnRun = false;//继续运行标志位  
            bool result = true;
            RunClass_IsFinish = false;
            for (int i = 0; i < RunItem_List.Count; i++)
            {
                while (true)
                {
                    if (IsStop == true)
                    {
                        ClinderR.IsStop = true;
                        AxisR.IsStop = true;
                        Meth.IsStop = true;
                        break;
                    }
                   // if (DateSave.Instance().Production.EStop == true)
                    if (DateSave.Instance().Production.EStop == true)
                    {
                        i = 2000;
                        ClinderR.Stop = true;
                        AxisR.Stop = true;
                        Meth.Stop = true;
                      //  Run_OneCase.Abort();
                        break;
                    }
                    if (parse == true)//暂停标志位
                    {
                        if (GoOnRun == true)//继续运行标志位
                        {
                            parse = false;
                            GoOnRun = false;//继续运行标志位
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (DateSave.Instance().Production.EStop == false)
                {
                    string sda = RunItem_List[i].Key;
                    sssf = RunItem_List[i].Value;
                    for (int j = 0; j < 3; j++)
                    {
                        if (IsStop == true)
                        {
                            parse = false;//运动超时报警
                            ClinderR.IsStop = true;
                            AxisR.IsStop = true;
                            Meth.IsStop = true;
                            break;
                        }
                        if (DateSave.Instance().Production.EStop == true)
                        {
                            parse = false;//运动超时报警
                            ClinderR.Stop = true;
                            AxisR.Stop = true;
                            Meth.Stop = true;
                            j = 5;
                            break;
                        }
                        //   Weld_Log.Instance().jp_writeLogWithLevel(LOG_LEVEL.LEVEL_3, "[IO检测]," + sda + "_" + sssf.Key + "_" + sssf.Value);
                        bool currentRunStatus = Run_Switch(sda, sssf.Key, sssf.Value);
                      
                        if (currentRunStatus == false)
                        {
                            if (j >= 2)
                            {
                                parse = true;//运动超时报警
                                BIZZ(sssf.Key, sssf.Value);
                            
                               // GoOnRun = false;//继续运行标志位                    
                                i = i - 1;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }



            }
            Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[运行完成],");

            IsStop = false;
            Stop = false;
            AxisR.IsStop = false;
            AxisR.Stop = false;
            ClinderR.IsStop = false;
            ClinderR.Stop = false;
            Meth.IsStop = false;
            Meth.Stop = false;
            RunClass_IsFinish = true;
            StartRun = false;
          
        //    Thread.Sleep(100);
            Run_OneCase.Abort();
        }
        public void BIZZ(string NAME, string ERR)
        {
            MessageAlarmForm AlarmForm = new MessageAlarmForm();
            Thread Bizz = new Thread(BIZZRun);
            Bizz.IsBackground = true;
            Bizz.Start();
            AlarmForm.InputBox(NAME, ERR, "");
            Bizz.Abort();
            GoOnRun = true;
            Meth.OutPut_One_Run("BIZZ", "false");
            Meth.OutPut_One_Run("三色灯红", "false");
            Meth.OutPut_One_Run("三色灯绿", "true");
        }
        public void BIZZRun()
        {
            Meth.OutPut_One_Run("三色灯红", "true");
            Meth.OutPut_One_Run("三色灯绿", "false");
            while (true)
            {
                Meth.OutPut_One_Run("BIZZ", "true");
                Thread.Sleep(2000);
                Meth.OutPut_One_Run("BIZZ", "false");
                Thread.Sleep(1000);

            }

        }
        KeyValuePair<double, double> Point_1;
        public List<double> HighDate = new List<double>();
        public List<KeyValuePair<double, double>> CamerDate = new List<KeyValuePair<double, double>>();
        List<KeyValuePair<double, double>> CamerDateNeed_Date = new List<KeyValuePair<double, double>>();
        public bool Run_Switch(string str, string str1, string CheckSta)
        {
            string[] str_camer_checkNeed = new string[5];
            string[] Offset = new string[5];
            double OffsetX = 0;
            double OffsetY = 0;
            // HighDate.Add(0.0);
            bool currentRunStatus = false;
            if (str.Contains("气缸运动"))
            {
                str = str1;
            }
            else if (str.Contains("拍照定位"))
            {
               
                str = str1;
            }
            else if (str.Contains("焊接定位"))
            {
                str = str1;
            }
            else if (str.Contains("调高定位"))
            {
                str = str1;
            }
            else if (str.Contains("焊接定位"))
            {
                str = str1;
            }
            //else if (str.Contains("拍照运动"))
            //{
            //    str = str1;
            //}
            switch (str)
            {
                case "延时":
                    try
                    {
                       int Time= int.Parse(CheckSta.Replace(" ", ""));
                        currentRunStatus = Meth.Delay(Time);
                    }
                    catch
                    {
                        currentRunStatus = false;
                    }               
                    break;
                case "IO检测":
                    delayCheckTime = 6000;
                    m_WeldingTaskGroup.AddRunMessage("[IO检测]," + str1);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO检测]," + str1);
                    currentRunStatus = Meth.WaitINPut_Check(str1, CheckSta, delayCheckTime);//检测一个输入               
                    break;
                case "IO检测等待":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO检测等待]," + str1);
                    delayCheckTime = 600000;
                    currentRunStatus = Meth.WaitINPut_Check(str1, CheckSta, delayCheckTime);//检测一个输入               
                    break;
                case "IO输出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[IO输出]," + str1);
                    currentRunStatus = Meth.OutPut_One_Run(str1, CheckSta);//一个输出
                    break;
                case "发送偏距":
                    currentRunStatus = true;
                    string[] OFFSET = CheckSta.Split(';');
                    string XX = OFFSET[0];
                    string YY = OFFSET[1];
                    string SendDate = "Offset;" + XX + ";" + YY + ";" + "0;";
                    Socket_server.Instance().sendDataToMac(SendDate);
                    break;
                case "单轴运动":
                    delayCheckTime = 6000;
                    int Asix = 0;
                     if(CheckSta=="0")
                    {
                        Asix = 0;
                    }
                    if (CheckSta == "1") { Asix = 1; }
                    if (CheckSta == "2")
                    {
                        Asix = 2;

                    }
                    if (CheckSta == "3") { Asix = 3; }
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[单轴运动]," + str);
                    currentRunStatus = AxisR.Asix_one_Run(WeldPlat_Str_Name, str1, Asix, delayCheckTime);//0 x//1 y //2  z//3 u
                    break;
                case "拍照Z轴":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照Z轴]");
                    currentRunStatus = AxisR.Asix_z_Auto_High(WeldPlat_Str_Name, "拍照Z轴", 2, 90, -20, 5, 5, delayCheckTime);
                    break;
                case "焊接Z轴":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接Z轴]");
                    currentRunStatus = AxisR.Asix_z_Auto_High(WeldPlat_Str_Name, "焊接Z轴", 2, 90, -20, 5, 5, delayCheckTime);
                    break;
                case "双轴运动":
                    //     currentRunStatus = AxisR.Asix_Two_Run();
                    break;
                case "工装板顶升气缸上升"://工装板顶升气缸上升
                    m_WeldingTaskGroup.AddRunMessage("[工装板顶升气缸上升]," + str1);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[工装板顶升气缸上升]");
                    currentRunStatus = ClinderR.Tooling_Plate_Up(CheckSta);
                    break;
                case "工装板顶升气缸下降"://工装板顶升气缸下降
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[工装板顶升气缸下降]");
                    currentRunStatus = ClinderR.Tooling_Plate_Down(CheckSta);
                    break;
                case "Y轴前模组定位气缸伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴前模组定位气缸伸出]");
                    currentRunStatus = ClinderR.Y_Axis_befor_Clinder_Location_out(CheckSta);
                    break;
                case "Y轴前模组定位气缸缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴前模组定位气缸缩回]");
                    currentRunStatus = ClinderR.Y_Axis_befor_Clinder_Location_in(CheckSta);
                    break;
                case "Y轴后模组定位气缸伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴后模组定位气缸伸出]");
                    currentRunStatus = ClinderR.Y_Axis_After_Clinder_Location_in(CheckSta);
                    break;
                case "Y轴后模组定位气缸缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴后模组定位气缸缩回]");
                    currentRunStatus = ClinderR.Y_Axis_After_Clinder_Location_out(CheckSta);
                    break;
                case "Y轴后模组顶升气缸上升":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴后模组顶升气缸上升]");
                    currentRunStatus = ClinderR.Y_Axis_After_Clinder_up(CheckSta);
                    break;
                case "Y轴后模组顶升气缸下降":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴后模组顶升气缸下降]");
                    currentRunStatus = ClinderR.Y_Axis_After_Clinder_down(CheckSta);
                    break;
                case "Y轴前模组顶升气缸上升":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴前模组顶升气缸上升]");
                    currentRunStatus = ClinderR.Y_Axis_befor_Clinder_up(CheckSta);
                    break;
                case "Y轴前模组顶升气缸下降":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Y轴前模组顶升气缸下降]");
                    currentRunStatus = ClinderR.Y_Axis_befor_Clinder_down(CheckSta);
                    break;
                case "铜嘴压板气缸上升":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[铜嘴压板气缸上升]");
                    currentRunStatus = ClinderR.Copper_Mouth_Up(CheckSta);
                    break;
                case "铜嘴压板气缸下降":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[铜嘴压板气缸下降]");
                    currentRunStatus = ClinderR.Copper_Mouth_Down(CheckSta);
                    break;
                case "工装板阻挡气缸上升":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[工装板阻挡气缸上升]");
                    currentRunStatus = ClinderR.BTooling_PlateStop_Up(CheckSta);
                    break;
                case "工装板阻挡气缸下降":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[工装板阻挡气缸下降]");
                    currentRunStatus = ClinderR.Tooling_PlateStop_Down(CheckSta);
                    break;
                case "定位板气缸伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[定位板气缸伸出]");
                    currentRunStatus = ClinderR.Batter_Board_Up(CheckSta);
                    break;
                case "定位板气缸缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[定位板气缸缩回]");
                    currentRunStatus = ClinderR.Batter_Board_Down(CheckSta);
                    break;
                case "X轴右定位气缸伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[X轴右定位伸出]");
                    currentRunStatus = ClinderR.X_Axis_right_Clinder_Location_out(CheckSta);
                    break;
                case "X轴右定位气缸缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[X轴右定位气缸缩回]");
                    currentRunStatus = ClinderR.X_Axis_right_Clinder_Location_in(CheckSta);
                    break;
                case "X轴左定位气缸伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[X轴左定位气缸伸出]");
                    currentRunStatus = ClinderR.X_Axis_left_Clinder_Location_out(CheckSta);
                    break;
                case "X轴左定位气缸缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[X轴左定位气缸缩回]");
                    currentRunStatus = ClinderR.X_Axis_left_Clinder_Location_in(CheckSta);
                    break;
                case "Z轴挡板伸出":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Z轴挡板伸出]");
                    currentRunStatus = ClinderR.Z_Baffle_Out(CheckSta);
                    break;
                case "Z轴挡板缩回":
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[Z轴挡板缩回]");
                    currentRunStatus = ClinderR.Z_Baffle_In(CheckSta);
                    break;
                case "测台阶气缸上升":
                    break;
                case "测台阶气缸下降":
                    break;
                case "调高":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高点基准]");
                    currentRunStatus = AxisR.Asix_z_Auto_High(WeldPlat_Str_Name, "调高点基准", 2, 90, -20, 5, 5, delayCheckTime);
                    break;
                case "拍照1#点坐标":
                   
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照1#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照1#点坐标", 60000);
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "拍照1#点坐标", delayCheckTime);
                   // currentRunStatus = AxisR.Asix_one_Run("运动平台", "拍照1#点坐标", 2, 60000);


                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照1#点坐标", HighDate[0], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                   // CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                    str_camer_checkNeed = CheckSta.Split(';');


                  
                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if(CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                     
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    //KeyValuePair<double, double> Point1= CamerDateNeed_Date[0].Key;
                    //CamerDate.Add(CamerDateNeed_Date[0].Key, CamerDateNeed_Date[0].Value);
                    //  CamerDate[0].Key = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]).k
                    //  CamerDate.Add( CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]));
                    // CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1])[0].Key;
                    //  CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1])[0].Value;
                    // CamerDate.Add();
                    break;
                case "拍照2#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照2#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照2#点坐标", delayCheckTime);
                   // currentRunStatus = AxisR.Asix_one_Run("运动平台", "拍照2#点坐标", 2, 60000);
              

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照2#点坐标", HighDate[1], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                     
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照3#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照3#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照3#点坐标", delayCheckTime);
                   // currentRunStatus = AxisR.Asix_one_Run("运动平台", "拍照3#点坐标", 2, 60000);
                

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照3#点坐标", HighDate[2], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    //  CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照4#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照4#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照4#点坐标", delayCheckTime);
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "拍照4#点坐标", delayCheckTime);
                   /// currentRunStatus = AxisR.Asix_one_Run("运动平台", "拍照4#点坐标", 2, 60000);
                //    Thread.Sleep(100);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照4#点坐标", HighDate[3], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    str_camer_checkNeed = CheckSta.Split(';');
                    Thread.Sleep(100);


                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                 
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照5#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照5#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照5#点坐标", delayCheckTime);
                  //  currentRunStatus = AxisR.Asix_one_Run("运动平台", "拍照5#点坐标", 2, 60000);
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "拍照5#点坐标", delayCheckTime);


                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照5#点坐标", HighDate[4], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                      
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照6#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照6#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照6#点坐标", delayCheckTime);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照6#点坐标", HighDate[5], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                       
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照7#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照7#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照7#点坐标", 60000);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照7#点坐标", HighDate[6], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                     
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照8#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照8#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照8#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照8#点坐标", HighDate[7], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                   
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照9#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照9#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照9#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照9#点坐标", HighDate[8], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                      
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照10#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照10#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照10#点坐标", delayCheckTime);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照10#点坐标", HighDate[9], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    //AxisR.Asix_one_Run("运动平台", "拍照10#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                 
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照11#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照11#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照11#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照11#点坐标", HighDate[10], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                 
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照12#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照12#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照12#点坐标", delayCheckTime);


                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照12#点坐标", HighDate[11], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照13#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照13#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照13#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照13#点坐标", HighDate[12], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                  //  AxisR.Asix_one_Run("运动平台", "拍照13#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                       
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照14#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照14#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照14#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照14#点坐标", HighDate[13], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    //AxisR.Asix_one_Run("运动平台", "拍照14#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                 
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照15#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照15#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照15#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照15#点坐标", HighDate[14], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                  //  AxisR.Asix_one_Run("运动平台", "拍照15#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                    
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照16#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照16#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照16#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照16#点坐标", HighDate[15], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                  //  AxisR.Asix_one_Run("运动平台", "拍照16#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                       
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照17#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照17#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照17#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照17#点坐标", HighDate[16], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                   // AxisR.Asix_one_Run("运动平台", "拍照17#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count==0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照18#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照18#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照18#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照18#点坐标", HighDate[17], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    //AxisR.Asix_one_Run("运动平台", "拍照18#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                     
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照19#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照19#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照19#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照19#点坐标", HighDate[18], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                   // AxisR.Asix_one_Run("运动平台", "拍照19#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
                  
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "拍照20#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[拍照20#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "拍照20#点坐标", delayCheckTime);

                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "拍照20#点坐标", HighDate[19], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                   // AxisR.Asix_one_Run("运动平台", "拍照20#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    str_camer_checkNeed = CheckSta.Split(';');



                    CamerDateNeed_Date = CamerDateNeed(str_camer_checkNeed[0], str_camer_checkNeed[1]);
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    else if (CamerDateNeed_Date.Count == 1)
                    {
                        CamerDate.Add(new KeyValuePair<double, double>(0.0, 0.0));
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);
                        }
             
                        currentRunStatus = true;
                    }
                    else if (CamerDateNeed_Date.Count == 2)
                    {
                        for (int i = 0; i < CamerDateNeed_Date.Count; i++)
                        {
                            Point_1 = new KeyValuePair<double, double>(CamerDateNeed_Date[i].Key, CamerDateNeed_Date[i].Value);
                            CamerDate.Add(Point_1);

                        }
                        currentRunStatus = true;
                    }
                    if (CamerDateNeed_Date.Count == 0)
                    {
                        currentRunStatus = false;
                    }
                    break;
                case "焊接1#点坐标":
                     Offset  = CheckSta.Split(';');
                     OffsetX = 0;
                     OffsetY = 0;
                    if (Offset.Length>0)
                    {
                         OffsetX = Convert.ToDouble(Offset[0]);
                         OffsetY = Convert.ToDouble(Offset[1]);
                    }
               
                    //激光器就绪
                    //    焊接报警指示
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接1#点坐标]");
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接1#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                   Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接1#点坐标]:" + HighDate[0]);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接1#点坐标", HighDate[0], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接1#点坐标]:" +"开始焊接");
                    if (CamerDate[0].Key==0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[0].Key, CamerDate[0].Value);//开始焊接及检测焊接完成
                    }           
                    break;
                case "焊接2#点坐标":
                   Offset = CheckSta.Split(';');
                     OffsetX = 0;
                     OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接2#点坐标]");
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接2#点坐标", delayCheckTime);
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接2#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接2#点坐标", HighDate[1], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接2#点坐标]:" + "开始焊接");
                    if (CamerDate[1].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[1].Key, CamerDate[1].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接3#点坐标":
                    delayCheckTime = 6000;

                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接3#点坐标]");

                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接3#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接3#点坐标", HighDate[2], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接3#点坐标]:" + "开始焊接");
                    if (CamerDate[2].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[2].Key, CamerDate[2].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接4#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接4#点坐标]");
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接4#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接4#点坐标]");

                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接4#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接4#点坐标", HighDate[3], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接4#点坐标]:" + "开始焊接");
                    if (CamerDate[3].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[3].Key, CamerDate[3].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接5#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接5#点坐标]");
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接5#点坐标", delayCheckTime);
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接5#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接5#点坐标", HighDate[4], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接5#点坐标]:" + "开始焊接");
                    if (CamerDate[4].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[4].Key, CamerDate[4].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接6#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接6#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接6#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接6#点坐标", HighDate[5], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接6#点坐标]:" + "开始焊接");
                    if (CamerDate[5].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[5].Key, CamerDate[5].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接7#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接7#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接7#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接7#点坐标", HighDate[6], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接7#点坐标]:" + "开始焊接");
                    if (CamerDate[6].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[6].Key, CamerDate[6].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接8#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接8#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接8#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接8#点坐标", HighDate[7], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接8#点坐标]:" + "开始焊接");
                    if (CamerDate[7].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[7].Key, CamerDate[7].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接9#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接9#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接9#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接9#点坐标", HighDate[8], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接9#点坐标]:" + "开始焊接");
                    if (CamerDate[8].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[8].Key, CamerDate[8].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接10#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接10#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接10#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接10#点坐标", HighDate[9], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接10#点坐标]:" + "开始焊接");
                    if (CamerDate[9].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[9].Key, CamerDate[9].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接11#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接11#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接11#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接11#点坐标", HighDate[10], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接11#点坐标]:" + "开始焊接");
                    if (CamerDate[10].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[10].Key, CamerDate[10].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接12#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接12#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接12#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接12#点坐标", HighDate[11], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接12#点坐标]:" + "开始焊接");
                    if (CamerDate[11].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[11].Key, CamerDate[11].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接13#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接13#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接13#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接13#点坐标", HighDate[12], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接13#点坐标]:" + "开始焊接");
                    if (CamerDate[12].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[12].Key, CamerDate[12].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接14#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接14#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接14#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接14#点坐标", HighDate[13], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接14#点坐标]:" + "开始焊接");
                    if (CamerDate[13].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[13].Key, CamerDate[13].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接15#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接15#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接15#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接15#点坐标", HighDate[14], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接15#点坐标]:" + "开始焊接");
                    if (CamerDate[14].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[14].Key, CamerDate[14].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接16#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接16#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接16#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接16#点坐标", HighDate[15], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接16#点坐标]:" + "开始焊接");
                    if (CamerDate[15].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[15].Key, CamerDate[15].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接17#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接17#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接17#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接17#点坐标", HighDate[16], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接17#点坐标]:" + "开始焊接");
                    if (CamerDate[16].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[16].Key, CamerDate[16].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接18#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接18#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接18#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接18#点坐标", HighDate[17], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接18#点坐标]:" + "开始焊接");
                    if (CamerDate[17].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[17].Key, CamerDate[17].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接19#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接19#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接19#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接19#点坐标", HighDate[18], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接19#点坐标]:" + "开始焊接");
                    if (CamerDate[18].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[18].Key, CamerDate[18].Value);//开始焊接及检测焊接完成
                    }
                    break;
                case "焊接20#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接20#点坐标]");
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "焊接6#点坐标", delayCheckTime);
                    Offset = CheckSta.Split(';');
                    OffsetX = 0;
                    OffsetY = 0;
                    if (Offset.Length > 0)
                    {
                        OffsetX = Convert.ToDouble(Offset[0]);
                        OffsetY = Convert.ToDouble(Offset[1]);
                    }
                    currentRunStatus = Meth.Weld_Asix_Line_Run("运动平台", "焊接20#点坐标", delayCheckTime, DateSave.Instance().Production.X_Setover, DateSave.Instance().Production.Y_Setover, OffsetX, OffsetY);
                    currentRunStatus = AxisR.Asix_z_Auto_High("运动平台", "焊接20#点坐标", HighDate[19], DateSave.Instance().Production.SaveHigh_Top, DateSave.Instance().Production.SaveHigh_Low, DateSave.Instance().Production.AutoZ_High_Top, DateSave.Instance().Production.AutoZ_High_Low, 6000);
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[焊接20#点坐标]:" + "开始焊接");
                    if (CamerDate[19].Key == 0.0)
                    {
                        currentRunStatus = true;
                    }
                    else
                    {
                        currentRunStatus = Weld_Check(CamerDate[19].Key, CamerDate[19].Value);//开始焊接及检测焊接完成
                    }
                    currentRunStatus = true;
                    break;
                case "调高1#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高1#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高1#点坐标", 60000);
                    currentRunStatus = AxisR.Asix_one_Run("运动平台", "调高1#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    break;
                case "调高2#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高2#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高2#点坐标", 60000);
                    currentRunStatus = AxisR.Asix_one_Run("运动平台", "调高2#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    break;
                case "调高3#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高3#点坐标]");

                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高3#点坐标", 60000);
                    currentRunStatus = AxisR.Asix_one_Run("运动平台", "调高3#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高3#点坐标", delayCheckTime);
                    break;
                case "调高4#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高4#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高4#点坐标", 60000);
                    currentRunStatus = AxisR.Asix_one_Run("运动平台", "调高4#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高4#点坐标", delayCheckTime);
                    break;
                case "调高5#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高5#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高5#点坐标", 60000);
                    currentRunStatus = AxisR.Asix_one_Run("运动平台", "调高5#点坐标", 2, 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高5#点坐标", delayCheckTime);
                    break;
                case "调高6#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高6#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高6#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高6#点坐标", delayCheckTime);
                    break;
                case "调高7#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高7#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高7#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高7#点坐标", delayCheckTime);
                    break;
                case "调高8#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高8#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高8#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高8#点坐标", delayCheckTime);
                    break;
                case "调高9#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高9#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高9#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //   currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高9#点坐标", delayCheckTime);
                    break;
                case "调高10#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高10#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高10#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高10#点坐标", delayCheckTime);
                    break;
                case "调高11#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高11#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高11#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高11#点坐标", delayCheckTime);
                    break;
                case "调高12#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高12#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高12#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高12#点坐标", delayCheckTime);
                    break;
                case "调高13#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高13#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高13#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高13#点坐标", delayCheckTime);
                    break;
                case "调高14#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高14#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高14#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高14#点坐标", delayCheckTime);
                    break;
                case "调高15#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高15#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高15#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高15#点坐标", delayCheckTime);
                    break;
                case "调高16#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高16#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高16#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //   currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高16#点坐标", delayCheckTime);
                    break;
                case "调高17#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高17#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高17#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高17#点坐标", delayCheckTime);
                    break;
                case "调高18#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高18#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高18#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高18#点坐标", delayCheckTime);
                    break;
                case "调高19#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高19#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高19#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    // currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高19#点坐标", delayCheckTime);
                    break;
                case "调高20#点坐标":
                    delayCheckTime = 6000;
                    Weld_Log.Instance().Enqueue(LOG_LEVEL.LEVEL_3, "[调高20#点坐标]");
                    currentRunStatus = Meth.Asix_Line_Run("运动平台", "调高20#点坐标", 60000);
                    Thread.Sleep(100);
                    if (调高数据() > 0)
                    {
                        HighDate.Add(调高数据());
                    }
                    else
                    {
                        //报警
                    }
                    //  currentRunStatus = AxisR.Asix_Two_Run(WeldPlat_Str_Name, "调高20#点坐标", delayCheckTime);
                    break;
            }
            return currentRunStatus;
        }
        public double HighDate_Need()
        {
            double High = 0.0;


            return High;

        }
        public string WeldFinishSta = "";
        Thread Weld = null;
        public bool   Weld_Check(double X,double Y)
        {
          return   weld_Finish(X, Y);
            //Weld = new Thread(weld_Finish);
            //Weld.IsBackground = true;
            //Weld.Start();
        }


   
        public bool  weld_Finish(double X, double Y)
        {
            int stime = 4000;
            bool sta = false;
            DateTime starttime = DateTime.Now;
            WeldFinishSta = "";
     
            if (DateSave.Instance().Production.Empty_run == true)
            {
                WeldFinishSta = "WeldFinish";
                IOManage.OUTPUT("脱机文件0触发").SetOutBit(false);
                IOManage.OUTPUT("开始焊接机").SetOutBit(false);
            }
            else
            {
                   Task Task1 = sendOffset(X,Y);
                   Task Task = WeldFinish();
            }
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (WeldFinishSta == "WeldFinish")
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
                if (Stop == true)
                {
                    sta = true;
                    break;
                }
            }
            IOManage.OUTPUT("脱机文件0触发").SetOutBit(false);
            IOManage.OUTPUT("开始焊接机").SetOutBit(false);
            Thread.Sleep(300);
            return sta;
        }
        public async Task sendOffset(double X, double Y)
        {
            await Task.Run(() =>
            {
                IOManage.OUTPUT("脱机文件0触发").SetOutBit(true);
                Thread.Sleep(300);
                IOManage.OUTPUT("开始焊接机").SetOutBit(true);
                string XX = X.ToString();
                string YY = Y.ToString();
                Socket_server.Instance().recvDate = "";
                while (true)
                {
                    if (Socket_server.Instance().recvDate.Contains("T"))
                    {
                        break;
                    }
                }
                string SendDate = "Offset;" + XX + ";" + YY + ";" + "0;";
                Socket_server.Instance().sendDataToMac(SendDate);
                return;
            });

         
        }
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
           // return;
        }
        public double 调高数据()
        {
            double high = 0.0;
            double BaselineSimulation = DateSave.Instance().Production.BaselineSimulation;//、、 获取基准模拟量  DateSave.Instance().Production.BaselineSimulation
            double date = 0.0;
            Thread.Sleep(200);
            TableManage.TableDriver("运动平台")._GetAdc(1, out date);//当前模拟量
            double ad12 = DateSave.Instance().Production.Z_AxialDatum;//获取Z基准坐标
            double sf = BaselineSimulation - date;
            if (sf > 0)
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                if (DateSave.Instance().Production.AutoZ_High_Top > z && DateSave.Instance().Production.AutoZ_High_Low < z)
                {                
                }
                else
                {
                    return 0.0;
                }
                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;//当前Z基准坐标
                double NeedCurrentZA = CurrentZA - z;
                high = NeedCurrentZA;
                if (DateSave.Instance().Production.SaveHigh_Top> high&& DateSave.Instance().Production.SaveHigh_Low< high)
                {
                   
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                double s = Math.Abs(sf);
                double z = s / DateSave.Instance().Production.High_Date;
                if (DateSave.Instance().Production.AutoZ_High_Top > z && DateSave.Instance().Production.AutoZ_High_Low < z)
                {
                }
                else
                {
                    return 0.0;
                }
                double CurrentZA = TableManage.TableDriver("运动平台").CurrentZ;
                double NeedCurrentZA = CurrentZA + z;
                high = NeedCurrentZA;
                if (DateSave.Instance().Production.SaveHigh_Top > high && DateSave.Instance().Production.SaveHigh_Low < high)
                {

                }
                else
                {
                    return 0.0;
                }
            }
            return high;
        }

        public List<KeyValuePair<double, double>> CamerDateNeed(string needGetR, string NeedCheckR)
        {
            List<KeyValuePair<double, double>> CamerDate = new List<KeyValuePair<double, double>>();
            KeyValuePair<double, double> Point1;
            KeyValuePair<double, double> Point2;
            List<LocationCircle.ResultClass> resultCirclr;
            resultCirclr = new List<LocationCircle.ResultClass>();
            string err = "";
            bool df = false;
            int needGetR_check = 0;
            if (needGetR == "2")
            {
                needGetR_check = 2;
            }
            else
            {
                needGetR_check = 1;
            }
            int NeedCheckR_check = 0;
            if (NeedCheckR == "2")
            {
                NeedCheckR_check = 2;
            }
            else
            {
                NeedCheckR_check = 1;
            }
            if (Program.form.VisionLocation(needGetR_check, NeedCheckR_check, ref resultCirclr))
            {
                for (int i = 0; i < resultCirclr.Count; i++)
                {
                    Point1 = new KeyValuePair<double, double>(resultCirclr[i].CenterPoint.X, resultCirclr[i].CenterPoint.Y);
                    CamerDate.Add(Point1);

                }

            }
            else
            {
                

            }
           ///  Program.form.VisionLocation(needGetR_check, NeedCheckR_check, ref resultCirclr);
         //   Program.form.TestVision(ref resultCirclr, df, needGetR_check, NeedCheckR_check, out err);
           
            //double X = resultCirclr[0].CenterPoint.X;
            //double Y = resultCirclr[0].CenterPoint.Y;
            //double CirclrC = resultCirclr[0].Radius;
            //double X1 = resultCirclr[1].CenterPoint.X;
            //double Y1 = resultCirclr[1].CenterPoint.Y;
            //double CirclrC1 = resultCirclr[1].Radius;
            //Point1 = new KeyValuePair<double, double>(resultCirclr[0].CenterPoint.X, resultCirclr[0].CenterPoint.Y);
            //Point2 = new KeyValuePair<double, double>(resultCirclr[1].CenterPoint.X, resultCirclr[1].CenterPoint.Y);
            //CamerDate.Add(Point1);
            //CamerDate.Add(Point2);
            return CamerDate;
        }
    }
     public  class Date_save
     {

        public double CamerDateX = 0.0;
        public double CamerDateY = 0.0;
        public double HighDateY = 0.0;
    }

  
}
