using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    public  class ClinderRun
    {
        public bool IsStop = false;
        public bool Stop = false;
        private static ClinderRun ClinderR;
        public static ClinderRun Instance()
        {
            if (ClinderR == null)
            {
                ClinderR = new ClinderRun();
            }
            return ClinderR;
        }
        public bool Y_Axis_befor_Clinder_up(string IsCheck)//  Y轴前模组顶升上   
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴前模组顶升气缸上升").SetOutBit(true);
            IOManage.OUTPUT("Y轴前模组顶升气缸下降").SetOutBit(false);              
                DateTime starttime = DateTime.Now;
                while (true)
                {
                    DateTime endtime = DateTime.Now;
                    TimeSpan spantime = endtime - starttime;
                    if (IOManage.INPUT("Y轴前模组顶升气缸上升").On&& IOManage.INPUT("Y轴前模组顶升气缸下降").Off)
                    {
                    runOK = true;
                        break;
                    }
                    if (spantime.TotalMilliseconds > stime)
                    {
                    runOK = false;
                        break;
                    }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }                
            return runOK;

        }
        public bool Y_Axis_befor_Clinder_down(string IsCheck)    //  Y轴前模组顶升下
        {
            bool runOK = false;
            int stime = 6000;
          
            IOManage.OUTPUT("Y轴前模组顶升气缸上升").SetOutBit(false);
            IOManage.OUTPUT("Y轴前模组顶升气缸下降").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴前模组顶升气缸上升").Off && IOManage.INPUT("Y轴前模组顶升气缸下降").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;          
        }
        public bool Y_Axis_After_Clinder_up(string IsCheck)//  Y轴后模组顶升上
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴后模组顶升气缸上升").SetOutBit(true);
            IOManage.OUTPUT("Y轴后模组顶升气缸下降").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴后模组顶升气缸上升").On && IOManage.INPUT("Y轴后模组顶升气缸下降").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;

        }
        public bool Y_Axis_After_Clinder_down(string IsCheck)//  Y轴后模组顶升下
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴后模组顶升气缸上升").SetOutBit(false);
            IOManage.OUTPUT("Y轴后模组顶升气缸下降").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴后模组顶升气缸上升").Off && IOManage.INPUT("Y轴后模组顶升气缸下降").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        // Y轴前模组定位伸出,
  
        public bool Y_Axis_befor_Clinder_Location_out(string IsCheck)//  Y轴前模组定位伸出,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴前模组定位气缸伸出").SetOutBit(true);
            IOManage.OUTPUT("Y轴前模组定位气缸缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴前模组定位气缸缩回").Off && IOManage.INPUT("Y轴前模组定位气缸伸出").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                  if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Y_Axis_befor_Clinder_Location_in(string IsCheck)      //    Y轴前模组定位缩回,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴前模组定位气缸伸出").SetOutBit(false);
            IOManage.OUTPUT("Y轴前模组定位气缸缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴前模组定位气缸缩回").On && IOManage.INPUT("Y轴前模组定位气缸伸出").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        // Y轴后模组定位伸出,
  

        public bool Y_Axis_After_Clinder_Location_out(string IsCheck)    //    Y轴后模组定位缩回,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴后模组定位气缸伸出").SetOutBit(false);
            IOManage.OUTPUT("Y轴后模组定位气缸缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴后模组定位气缸缩回").On && IOManage.INPUT("Y轴后模组定位气缸伸出").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        //    Y轴后模组定位缩回,
        public bool Y_Axis_After_Clinder_Location_in(string IsCheck)      // Y轴后模组定位伸出,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Y轴后模组定位气缸伸出").SetOutBit(true);
            IOManage.OUTPUT("Y轴后模组定位气缸缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Y轴后模组定位气缸缩回").Off && IOManage.INPUT("Y轴后模组定位气缸伸出").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        // X轴左模组定位伸出,


        public bool X_Axis_left_Clinder_Location_out(string IsCheck)    //       // X轴左模组定位伸出,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("X轴左定位气缸伸出").SetOutBit(true);
            IOManage.OUTPUT("X轴左定位气缸缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("X轴左定位气缸伸出").On && IOManage.INPUT("X轴左定位气缸缩回").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        // X轴左模组定位缩回,
        public bool X_Axis_left_Clinder_Location_in(string IsCheck)      // Y轴后模组定位缩回
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("X轴左定位气缸伸出").SetOutBit(false);
            IOManage.OUTPUT("X轴左定位气缸缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("X轴左定位气缸伸出").Off && IOManage.INPUT("X轴左定位气缸缩回").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }

        // X轴右模组定位伸出,


        public bool X_Axis_right_Clinder_Location_out(string IsCheck)    //            // X轴右模组定位伸出,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("X轴右定位气缸伸出").SetOutBit(true);
            IOManage.OUTPUT("X轴右定位气缸缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("X轴右定位气缸伸出").On && IOManage.INPUT("X轴右定位气缸缩回").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        // X轴右模组定位缩回,
        public bool X_Axis_right_Clinder_Location_in(string IsCheck)      // X轴右模组定位缩回,
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("X轴右定位气缸伸出").SetOutBit(false);
            IOManage.OUTPUT("X轴右定位气缸缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("X轴右定位气缸伸出").Off && IOManage.INPUT("X轴右定位气缸缩回").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Tooling_Plate_Up(string IsCheck)//工装板上升
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("工装板顶升气缸上升").SetOutBit(true);
            IOManage.OUTPUT("工装板顶升气缸下降").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("工装板顶升气缸上升").On && IOManage.INPUT("工装板顶升气缸下降").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Tooling_Plate_Down(string IsCheck)//工装板下降
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("工装板顶升气缸上升").SetOutBit(false);
            IOManage.OUTPUT("工装板顶升气缸下降").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("工装板顶升气缸上升").Off && IOManage.INPUT("工装板顶升气缸下降").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }


        public bool Copper_Mouth_Up(string IsCheck)//铜嘴上升
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("铜嘴压板气缸上升").SetOutBit(true);
            IOManage.OUTPUT("铜嘴压板气缸下降").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("铜嘴压板气缸上升").On && IOManage.INPUT("铜嘴压板气缸下降").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Copper_Mouth_Down(string IsCheck)//铜嘴下降
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("铜嘴压板气缸上升").SetOutBit(false);
            IOManage.OUTPUT("铜嘴压板气缸下降").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("铜嘴压板气缸上升").Off && IOManage.INPUT("铜嘴压板气缸下降").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }

        public bool Batter_Board_Up(string IsCheck)//定位板上升
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("定位板气缸伸出").SetOutBit(true);
            IOManage.OUTPUT("定位板气缸缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("定位板气缸伸出").On && IOManage.INPUT("定位板气缸缩回").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Batter_Board_Down(string IsCheck)//定位板下降
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("定位板气缸伸出").SetOutBit(false);
            IOManage.OUTPUT("定位板气缸缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("定位板气缸伸出").Off && IOManage.INPUT("定位板气缸缩回").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Tooling_PlateStop_Down(string IsCheck)//工装板阻挡下降
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("工装板阻挡气缸上升").SetOutBit(false);
            IOManage.OUTPUT("工装板阻挡气缸下降").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("工装板阻挡气缸上升").Off && IOManage.INPUT("工装板阻挡气缸下降").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool BTooling_PlateStop_Up(string IsCheck)//工装板阻挡上升
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("工装板阻挡气缸上升").SetOutBit(true);
            IOManage.OUTPUT("工装板阻挡气缸下降").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("工装板阻挡气缸上升").On && IOManage.INPUT("工装板阻挡气缸下降").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Z_Baffle_Out(string IsCheck)//Z轴挡板伸出
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Z轴挡板伸出").SetOutBit(true);
            IOManage.OUTPUT("Z轴挡板缩回").SetOutBit(false);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Z轴挡板伸出").On && IOManage.INPUT("Z轴挡板缩回").Off)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;

                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
        public bool Z_Baffle_In(string IsCheck)//Z轴挡板缩回
        {
            int stime = 6000;
            bool runOK = false;
            IOManage.OUTPUT("Z轴挡板伸出").SetOutBit(false);
            IOManage.OUTPUT("Z轴挡板缩回").SetOutBit(true);
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (IOManage.INPUT("Z轴挡板伸出").Off && IOManage.INPUT("Z轴挡板缩回").On)
                {
                    runOK = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    runOK = false;
                    break;
                }
                if (IsStop == true)
                {
                    runOK = true;
                    break;
                }
                if (DateSave.Instance().Production.EStop == true)
                {
                    runOK = true;
                    break;
                }
                if (IsCheck == "不检测")
                {
                    runOK = true;
                    break;
                }
            }
            return runOK;
        }
    }
}
