using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class Date
    {
        //步骤定义·
        public string StepNmae = "";
        //运行步骤
        public int RunStep= 0;
        //单轴运动轴号
        public int AxisNum = 0;
        //多轴运动轴号
        public List<int> CombAxisNum = new List<int>();
        //xyz定位位置
        public double X_POSITION = 0.0;
        public double Y_POSITION = 0.0;
        public double Z_POSITION = 0.0;
        public double U_POSITION = 0.0;
        //xyz当前位置
        public double X_Current = 0.0;
        public double Y_Current = 0.0;
        public double Z_Current = 0.0;
        public double U_Current = 0.0;
        //延时
        public int DelayTime =-1;//-1是一直等待。  大于0的默认是等待
        //IO输入定义单独
        public string  Input = "";
        //IO输出定义定义单独
        public string Output = "";
        //IO输入定义多个
        public List<string> InputComb = new List<string>();
        //IO输出定义定义多个
        public List <string> OutputComb = new List<string>();
        //步骤失败后跳转
        public int StepFailNextStep = 0;
        //步骤成功后跳转
        public int StepOkNextStep = 0;
        //气缸检测工作位
        public int CylinderWorkingPosition = 0;
        //延时
        public int CylinderWorkingPositionDelay = -1;//-1是一直等待。  大于0的默认是等待
        //气缸检测待机位
        public int CylinderStandbyPosition = 0;
        //延时
        public int CylinderStandbyPositionDelay = -1;//-1是一直等待。  大于0的默认是等待
        //气缸运动工作位
        public int CylinderMovementWorkPosition = 0;
        //气缸运动待机位
        public int CylinderMovementStandbyPosition = 0;
        //视觉给出的中心
        public double[] VisualCentreX = new double[30];
        public double[] VisualCentreY = new double[30];
        public double[] VisualCentreZ = new double[30];
        //视觉偏距
        public double VisualOffsetX = 0.0;
        public double VisualOffsetY = 0.0;
        public double VisualOffsetZ = 0.0;
        //串口
        public string portCom="";//串口号
        public string BaudRate = "";//波特率
        public string DateBit = "";//串口号
        public string StopBit = "";//波特率
        public string oddEvenCheck = "";//奇偶校验
        public string SengDate = "";//发送的数据
        public string receiveDate = "";//接受的数据              
        //网口
        public string IPadd = "";//IP地址
        public string NET_SengDate = "";//发送的数据
        public string NET_receiveDate = "";//接受的数据

        //运行的步骤
        public int StartStep = 0;//开始步骤
        public int EndStep = 0;//结束步骤
        //单独运行步骤
        public int AloneStep = 0;//单独运行步骤号
        //是否屏蔽
        public bool shield = true;
        //线程
        public bool GoOnRun = true;//继续运行
        public bool Pause = true;//暂停
        public bool Alarm = true;//报警
        public bool IOInput_Check(int IOInputNum)//检测一个
        {
            bool CheckIsOk = false;
            CheckIsOk = delay(-1,1);
            return CheckIsOk;
        }
        //IOInputNum输入多个IO集合
        public bool IOInput_CheckComb(List<int> IOInputNum)//检测多个
        {
            bool CheckIsOk = false;
            CheckIsOk = delay(-1, 1);
            return CheckIsOk;
        }
        public bool OUTInput_Check(int OUTNum)//输出一个
        {
            bool CheckIsOk = false;
           
            return CheckIsOk;
        }
        //OUTNum输出多个IO集合
        //
        //

        public bool OUTInput_CheckComb(List<string> OUTNum)//输出多个// 
        {
            bool CheckIsOk = false;
            IOManage.OUTPUT("Y轴后模组定位上").SetOutBit(true);
            return CheckIsOk;
        }
        public bool Axis_ONE_Run(int AxisNum)  //单轴运动
        {
            bool CheckIsOk = false;
            //单轴运动
            double xdfsd = TableManage.TablePosItem("运动平台", "焊接" + "1" + "#点坐标").dPosX;
            double xddb = TableManage.TablePosItem("运动平台", "焊接" + "1" + "#点坐标").dPosY;
            TableManage.TableDriver("运动平台").ArcMove(0.50, 0.50, 0.50, xdfsd, xddb, 5, 0, (CoordinateType)0);
            TableManage.TableDriver("运动平台").StartCure(false);
            return CheckIsOk;
        }
        public bool Axis_TWO_Run(List<int> CombAxisNum)  //多轴运动
        {
            bool CheckIsOk = false;
            //多轴运动
            return CheckIsOk;
        }
        public bool Axis_LINE_Run(List<int> CombAxisNum)  //两轴直线运动
        {
            bool CheckIsOk = false;
            //两轴直线运动
            return CheckIsOk;
        }
        public bool Axis_ARC_Run(List<int> CombAxisNum)  //两轴圆弧运动
        {
            bool CheckIsOk = false;
            //两轴圆弧运动
            return CheckIsOk;
        }
        public bool delay(int timeLong,int IOInputNum)
        {
            bool CheckIsOk = true;
            DateTime starttime = DateTime.Now;
            int stime = timeLong;
            while (true)
            {
                if (timeLong == -1)
                {
                    if (IOInputNum==1)
                    {
                        CheckIsOk = true;
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(1);
                    DateTime endtime = DateTime.Now;
                    TimeSpan spantime = endtime - starttime;
                    if (spantime.TotalSeconds > stime&& IOInputNum==0)
                    {
                        CheckIsOk = false;
                    }
                }               
           }
            return CheckIsOk;
        }
        public enum flowChar
        {
            单个输出 = 1,
            多个输出,
            单个输入检测,
            多个输入检测,
            单轴运动,
            多轴运动,
            直线插补,
            圆弧插补,
            串口设置,
            串口接收,
            网口设置,
            网口接收,       
        }
        public void run_thend()
        {
            switch (RunStep)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }


        }
    }
}
