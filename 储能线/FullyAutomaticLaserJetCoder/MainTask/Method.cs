using ControlPlatformLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    public class Method
    {
        public bool IsStop = false;
        public bool Stop= false;
        private static Method Meth;
        public static Method Instance()
        {
            if (Meth == null)
            {
                Meth = new Method();
            }
            return Meth;
        }
        public bool Delay(int stime)    //一个检测
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;               
                if (spantime.TotalMilliseconds > stime)
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
        public bool WaitINPut_Check(string IONum, string CheckSta,int stime)    //一个检测
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;       
            while (true)
            {           
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
         
                if (IOManage.INPUT(IONum).On&& CheckSta.ToLower()=="true")
                {
                    sta = true;
                    break;
                }
                if (IOManage.INPUT(IONum).Off  && CheckSta.ToLower() == "false")
                {
                    sta = true;
                    break;
                }
                if (spantime.TotalMilliseconds > stime)
                {
                    sta= false;
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

        public bool WaitINPut_More_Check(List<string> IONum, int stime)  //多个检测
        {
            List < bool> InPut_More_List = new List<bool>();
            for (int i = 0; i < IONum.Count; i++)
            {
                InPut_More_List.Add(false);
            }
            int  staNum = 0;
            bool sta = false;
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                for (int i=0;i< IONum.Count;i++)
                {               
                    if (IOManage.INPUT(IONum[i]).On)
                    {
                        // sta = true;
                        InPut_More_List[staNum] = true;
                       
                    }
                    else
                    {
                       // sta = false;
                        InPut_More_List[staNum] = false;
                    }
                    staNum++;
                }
                for (int i = 0; i < InPut_More_List.Count; i++)
                {
                    if (InPut_More_List[i]==false)
                    {
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

        public bool OutPut_One_Run(string str, string sta)//一个输出
        {
            if (sta.ToLower()=="true")
            {
                IOManage.OUTPUT(str).SetOutBit(true);
            }
            else
            {
                IOManage.OUTPUT(str).SetOutBit(false);
            }
         
            return true;
        }
        public bool OutPut_Two_Run(string Work_str, bool Work_sta, string Home_str, bool Home_sta, string  Work_Str, bool Work_Senson, string Home_Str, bool Home_Senson, int stime)//2个输出
        {
            IOManage.OUTPUT(Work_str).SetOutBit(Work_sta);
            IOManage.OUTPUT(Home_str).SetOutBit(Home_sta);
            bool sta = false;
            bool sta1 = false;
            DateTime starttime = DateTime.Now;
            while (true)
            {
                DateTime endtime = DateTime.Now;
                TimeSpan spantime = endtime - starttime;
                if (Work_Senson == true)
                {
                    if (IOManage.INPUT(Work_Str).On)
                    {
                        sta = true;
                    }
                }
                else
                {
                    if (IOManage.INPUT(Work_Str).Off)
                    {
                        sta = true;
                    }
                }
                if (Home_Senson == true)
                {
                    if (IOManage.INPUT(Home_Str).On)
                    {
                        sta1 = true;                    
                    }
                }
                else
                {
                    if (IOManage.INPUT(Home_Str).Off)
                    {
                        sta1 = true;                      
                    }
                }
                if (sta1==true && sta==true)
                {                
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
        public List<string> OutPut_More_List = new List<string>();
        public bool OutPut_More_Run(List<string> str, bool sta)//多个输出
        {
            foreach (var keys in str)
            {
                IOManage.OUTPUT(keys).SetOutBit(sta);
            }
            return true;
        }
        public bool Asix_one_Run(string PlatformName,string Position, int  Asix_str,int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            if (Asix_str==0)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.X,
                       TableManage.TablePosItem(PlatformName, Position).dPosX,
                       TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed);
            }
            else if (Asix_str==1)
            {
                TableManage.TableDriver(PlatformName).AbsMove(TableAxisName.Y,
                        TableManage.TablePosItem(PlatformName, Position).dPosY,
                        TableManage.tablesDoc.m_tableDictionary[PlatformName].axisYData.dSpeed);
            }
            else if (Asix_str==2)
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
                if (TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.X)&& TableManage.TableDriver(PlatformName).MoveDone(TableAxisName.Y))
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
        public bool Asix_Line_Run(string PlatformName, string Position, int stime)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double X = TableManage.TablePosItem(PlatformName, Position).dPosX;
            double Y = TableManage.TablePosItem(PlatformName, Position).dPosY;
            bool LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dAcc, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dDec, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed, X, Y, 500);
            for (int i = 0; i < 3; i++)
            {
                if (LineXYZMove == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(10);
                     LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dAcc, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dDec, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed, X, Y, 500);
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
               double CurrentX=TableManage.TableDriver(PlatformName).CurrentX;
               double CurrentY = TableManage.TableDriver(PlatformName).CurrentY;
                if ((X- 0.005 < CurrentX && X + 0.005 > CurrentX )&&( Y - 0.005 < CurrentY && Y + 0.005 > CurrentY))
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

        public bool Weld_Asix_Line_Run(string PlatformName, string Position, int stime,double X_Setover, double Y_Setover, double X_Camer, double Y_Camer)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double X = TableManage.TablePosItem(PlatformName, Position).dPosX + X_Setover + X_Camer;
            double Y = TableManage.TablePosItem(PlatformName, Position).dPosY + Y_Setover + Y_Camer;
            bool LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dAcc, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dDec, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed, X, Y, 500);
            for (int i = 0; i < 3; i++)
            {
                if (LineXYZMove == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(10);
                    LineXYZMove = TableManage.TableDriver(PlatformName).LineXYZMove(TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dAcc, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dDec, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed, X, Y, 500);
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
        public bool Asix_Arc_Run(string PlatformName, string Position, int stime,int R)
        {
            bool sta = false;
            DateTime starttime = DateTime.Now;
            double X = TableManage.TablePosItem(PlatformName, Position).dPosX;
            double Y = TableManage.TablePosItem(PlatformName, Position).dPosY; 
            bool ArcXYMove =    TableManage.TableDriver(PlatformName).ArcMove(TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dAcc, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dDec, TableManage.tablesDoc.m_tableDictionary[PlatformName].axisXData.dSpeed, X, Y, R, 0, (CoordinateType)0);
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
           bool ArcXYMove_run =  TableManage.TableDriver(PlatformName).StartCure(false);
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



    }
}
