
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

namespace ControlPlatformLib
{
    /// <summary>
    /// 运动模式
    /// </summary>
    public enum MoveMode
    {
        /// <summary>
        /// 默认模式
        /// </summary>
        Default,//默认模式
        /// <summary>
        /// 停止
        /// </summary>
        STOP,   //停止
        /// <summary>
        /// 点动
        /// </summary>
        JOG,    //点动
        /// <summary>
        /// 绝对运动
        /// </summary>
        ABS,    //绝对运动
        /// <summary>
        /// 相对运动
        /// </summary>
        REL,    //相对运动
        /// <summary>
        /// 搜极限运动
        /// </summary>
        LIMIT,  //搜极限运动
        /// <summary>
        /// 回原点运动
        /// </summary>
        HOME,   //回原点运行
        /// <summary>
        /// 圆弧插补运动
        /// </summary>
        ARC     //圆弧插补运动
    }

    /// <summary>
    /// 轴名称
    /// </summary>
    public enum TableAxisName
    {
        X,  //X轴
        Y,  //Y轴
        Z,  //Z轴
        U,  //U轴
        ALL //所有轴
    }

    ///<summary>
    ///名称：平台驱动类
    ///作用：控制平台运动
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    /// 描述：每个平台驱动类有4个轴，分别为X、Y、Z、U四个轴。可以对单轴进行点动、绝对运动、相对运动、回原点运动、搜极限运动等，可获取单轴的状态。可以对多轴进行圆弧插补、直线插补运动。不论任何品牌控制卡或者控制器。
    ///</summary>
    public class TableDriver
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public string strDriverName;            //平台名称
        /// <summary>
        /// 平台设置数据
        /// </summary>
        public TableData tableData;             //平台设置数据
        /// <summary>
        /// 平台轴状态
        /// </summary>
        public TablePreStatus tablePreStatus;   //平台轴状态
        /// <summary>
        /// U轴控制实例
        /// </summary>
        private IMotionAction actionU;          //U轴控制实例
        /// <summary>
        /// X轴控制实例
        /// </summary>
        private IMotionAction actionX;          //X轴控制实例
        /// <summary>
        /// Y轴控制实例
        /// </summary>
        private IMotionAction actionY;          //Y轴控制实例
        /// <summary>
        /// Z轴控制实例
        /// </summary>
        private IMotionAction actionZ;          //Z轴控制实例
        /// <summary>
        /// U轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// </summary>
        public bool bHomingU;                   //U轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// <summary>
        /// X轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// </summary>
        public bool bHomingX;                   //X轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// <summary>
        /// Y轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// </summary>
        public bool bHomingY;                   //Y轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// <summary>
        /// Z轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// </summary>
        public bool bHomingZ;                   //Z轴是否回原中 TRUE:正在回原 FALSE:不在回原状态
        /// <summary>
        /// U轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// </summary>
        private bool bReadyU;                   //U轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// <summary>
        /// X轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// </summary>
        private bool bReadyX;                   //X轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// <summary>
        /// Y轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// </summary>
        private bool bReadyY;                   //Y轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// <summary>
        /// Z轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// </summary>
        private bool bReadyZ;                   //Z轴是否准备好 TRUE:准备好了 FALSE:未准备好
        /// <summary>
        /// U轴加速度
        /// </summary>
        private double dAccU;
        /// <summary>
        /// X轴加速度
        /// </summary>
        private double dAccX;
        /// <summary>
        /// Y轴加速度
        /// </summary>
        private double dAccY;
        /// <summary>
        /// Z轴加速度
        /// </summary>
        private double dAccZ;
        /// <summary>
        /// U轴减速度
        /// </summary>
        private double dDecU;
        /// <summary>
        /// X轴减速度
        /// </summary>
        private double dDecX;
        /// <summary>
        /// Y轴减速度
        /// </summary>
        private double dDecY;
        /// <summary>
        /// Z轴减速度
        /// </summary>
        private double dDecZ;
        /// <summary>
        /// U轴指令位置
        /// </summary>
        private double dPosU;
        /// <summary>
        /// X轴指令位置
        /// </summary>
        private double dPosX;
        /// <summary>
        /// Y轴指令位置
        /// </summary>
        private double dPosY;
        /// <summary>
        /// Z轴指令位置
        /// </summary>
        private double dPosZ;
        /// <summary>
        /// U轴指令速度
        /// </summary>
        private double dSpeedU;
        /// <summary>
        /// X轴指令速度
        /// </summary>
        private double dSpeedX;
        /// <summary>
        /// Y轴指令速度
        /// </summary>
        private double dSpeedY;
        /// <summary>
        /// Z轴指令速度
        /// </summary>
        private double dSpeedZ;
        /// <summary>
        /// U轴轴编号
        /// </summary>
        private short iAxisNOU;
        /// <summary>
        /// X轴轴编号
        /// </summary>
        private short iAxisNOX;
        /// <summary>
        /// Y轴轴编号
        /// </summary>
        private short iAxisNOY;
        /// <summary>
        /// Z轴轴编号
        /// </summary>
        private short iAxisNOZ;
        /// <summary>
        /// U轴当前运动状态
        /// </summary>
        private MoveMode moveModeU = MoveMode.STOP;
        /// <summary>
        /// X轴当前运动状态
        /// </summary>
        private MoveMode moveModeX = MoveMode.STOP;
        /// <summary>
        /// Y轴当前运动状态
        /// </summary>
        private MoveMode moveModeY = MoveMode.STOP;
        /// <summary>
        /// Z轴当前运动状态
        /// </summary>
        private MoveMode moveModeZ = MoveMode.STOP;
        /// <summary>
        /// X轴当前位置
        /// </summary>
        private double dCurrentX;
        /// <summary>
        /// Y轴当前位置
        /// </summary>
        private double dCurrentY;
        /// <summary>
        /// Z轴当前位置
        /// </summary>
        private double dCurrentZ;
        /// <summary>
        /// U轴当前位置
        /// </summary>
        private double dCurrentU;

        //bool HomeReadyX;
        //bool HomeReadyY;
        //bool HomeReadyZ;
        //bool HomeReadyU;

        /// <summary>
        /// U轴当前位置
        /// </summary>
        public double CurrentU
        {
            get
            {
                if (bReadyU)
                {
                    try
                    {
                        return actionU.GetCurrentPos(tableData.axisUData.AxisNo) * tableData.axisUData.plusToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        /// <summary>
        /// X轴当前位置
        /// </summary>
        public double CurrentX
        {
            get
            {
                if (bReadyX)
                {
                    try
                    {
                        return actionX.GetCurrentPos(tableData.axisXData.AxisNo) * tableData.axisXData.plusToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        /// <summary>
        /// Y轴当前位置
        /// </summary>
        public double CurrentY
        {
            get
            {
                if (bReadyY)
                {
                    try
                    {
                        return actionY.GetCurrentPos(tableData.axisYData.AxisNo) * tableData.axisYData.plusToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        /// <summary>
        /// Z轴当前位置
        /// </summary>
        public double CurrentZ
        {
            get
            {
                if (bReadyZ)
                {
                    try
                    {
                        return actionZ.GetCurrentPos(tableData.axisZData.AxisNo) * tableData.axisZData.plusToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }

        /// <summary>
        /// 轴是否在原点位
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <returns>轴原点状态 TRUE:在原点 FALSE:不在原点</returns>
        public bool Org(TableAxisName axis)
        {
            switch (axis)
            {
                case TableAxisName.X:
                    return actionX.GetHome(tableData.axisXData.AxisNo);
                case TableAxisName.Y:
                    return actionY.GetHome(tableData.axisYData.AxisNo);
                case TableAxisName.Z:
                    return actionZ.GetHome(tableData.axisZData.AxisNo);
                case TableAxisName.U:
                    return actionU.GetHome(tableData.axisUData.AxisNo);
                case TableAxisName.ALL:
                    return false;
                default:
                    return false;
            }
        }

        //AbsMove(WeidyFrame.TableAxisName.Z, WeidyFrame.DataManage.DoubleValue("HorizontalLeftZHeight", "Middle"), WeidyFrame.DataManage.DoubleValue("HorizontalLeftZSpeed", "Middle"));//获取参数);
        /// <summary>
        /// 绝对点位运动
        /// </summary>
        /// <param name="Axis">轴名称</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">速度</param>
        /// <param name="dPos">目标位置</param>
        public void AbsMove(TableAxisName axis, double dAcc, double dDec, double dSpeed, double dPos)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.AbsPosMove(tableData.axisXData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisXData.plusToMM,
                                            dPos / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.ABS;
                        dPosX = dPos;
                        dSpeedX = dSpeed;
                        dAccX = dAcc;
                        dDecX = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.AbsPosMove(tableData.axisYData.AxisNo,
                                           dAcc,
                                           dDec,
                                           dSpeed / tableData.axisYData.plusToMM,
                                            dPos / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.ABS;
                        dPosY = dPos;
                        dSpeedY = dSpeed;
                        dAccY = dAcc;
                        dDecY = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.AbsPosMove(tableData.axisZData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisZData.plusToMM,
                                            dPos / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.ABS;
                        dPosZ = dPos;
                        dSpeedZ = dSpeed;
                        dAccZ = dAcc;
                        dDecZ = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.AbsPosMove(tableData.axisUData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisUData.plusToMM,
                                            dPos / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.ABS;
                        dPosU = dPos;
                        dSpeedU = dSpeed;
                        dAccU = dAcc;
                        dDecU = dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }

        //AbsMove(WeidyFrame.TableAxisName.Z, WeidyFrame.DataManage.DoubleValue("HorizontalLeftZHeight", "Middle"), WeidyFrame.DataManage.DoubleValue("HorizontalLeftZSpeed", "Middle"));//获取参数);
        /// <summary>
        /// 绝对点位运动
        /// </summary>
        /// <param name="Axis">轴名称</param>
        /// <param name="dPos">目标位置</param>
        /// <param name="dSpeed">速度</param>
        public void AbsMove(TableAxisName axis, double dPos, double dSpeed)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.AbsPosMove(tableData.axisXData.AxisNo,
                                            tableData.axisXData.dAcc,
                                            tableData.axisXData.dDec,
                                            dSpeed / tableData.axisXData.plusToMM,
                                            dPos / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.ABS;
                        dPosX = dPos;
                        dSpeedX = dSpeed;
                        dAccX = tableData.axisXData.dAcc;
                        dDecX = tableData.axisXData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.AbsPosMove(tableData.axisYData.AxisNo,
                                           tableData.axisYData.dAcc,
                                           tableData.axisYData.dDec,
                                           dSpeed / tableData.axisYData.plusToMM,
                                            dPos / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.ABS;
                        dPosY = dPos;
                        dSpeedY = dSpeed;
                        dAccY = tableData.axisYData.dAcc;
                        dDecY = tableData.axisYData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.AbsPosMove(tableData.axisZData.AxisNo,
                                            tableData.axisZData.dAcc,
                                            tableData.axisZData.dDec,
                                            dSpeed / tableData.axisZData.plusToMM,
                                            dPos / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.ABS;
                        dPosZ = dPos;
                        dSpeedZ = dSpeed;
                        dAccZ = tableData.axisZData.dAcc;
                        dDecZ = tableData.axisZData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.AbsPosMove(tableData.axisUData.AxisNo,
                                            tableData.axisUData.dAcc,
                                            tableData.axisUData.dDec,
                                            dSpeed / tableData.axisUData.plusToMM,
                                            dPos / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.ABS;
                        dPosU = dPos;
                        dSpeedU = dSpeed;
                        dAccU = tableData.axisUData.dAcc;
                        dDecU = tableData.axisUData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 绝对点位运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="dPos">目标位置</param>
        public void AbsMove(TableAxisName axis, double dPos)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.AbsPosMove(tableData.axisXData.AxisNo,
                                            tableData.axisXData.dAcc,
                                            tableData.axisXData.dDec,
                                            tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                            dPos / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.ABS;
                        dPosX = dPos;
                        dSpeedX = tableData.axisXData.dSpeed;
                        dAccX = tableData.axisXData.dAcc;
                        dDecX = tableData.axisXData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.AbsPosMove(tableData.axisYData.AxisNo,
                                            tableData.axisYData.dAcc,
                                            tableData.axisYData.dDec,
                                            tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                            dPos / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.ABS;
                        dPosY = dPos;
                        dSpeedY = tableData.axisYData.dSpeed;
                        dAccY = tableData.axisYData.dAcc;
                        dDecY = tableData.axisYData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.AbsPosMove(tableData.axisZData.AxisNo,
                                            tableData.axisZData.dAcc,
                                            tableData.axisZData.dDec,
                                            tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                            dPos / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.ABS;
                        dPosZ = dPos;
                        dSpeedZ = tableData.axisZData.dSpeed;
                        dAccZ = tableData.axisZData.dAcc;
                        dDecZ = tableData.axisZData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.AbsPosMove(tableData.axisUData.AxisNo,
                                            tableData.axisUData.dAcc,
                                            tableData.axisUData.dDec,
                                            tableData.axisUData.dSpeed / tableData.axisUData.plusToMM,
                                            dPos / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.ABS;
                        dPosU = dPos;
                        dSpeedU = tableData.axisUData.dSpeed;
                        dAccU = tableData.axisUData.dAcc;
                        dDecU = tableData.axisUData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// XY轴圆弧插补运动
        /// </summary>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">速度</param>
        /// <param name="posX">X轴目标位置</param>
        /// <param name="posY">Y轴目标位置</param>
        /// <param name="dR">目标圆弧半径</param>
        /// <param name="iCCW">旋转方向</param>
        /// <returns>执行结果 TRUE:成功 FALSE:失败</returns>
        public bool ArcXYMove(double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            actionX.ArcXYMove((short)tableData.axisXData.iCorNo,
                tableData.axisXData.AxisNo,
                tableData.axisYData.AxisNo,
                tableData.axisZData.AxisNo,
                dAcc,
                dDec,
                dSpeed / tableData.axisXData.plusToMM,
                posX / tableData.axisXData.plusToMM,
                posY / tableData.axisYData.plusToMM,
                dR / tableData.axisZData.plusToMM,
                iCCW);
            return true;
        }

        /// <summary>
        /// 圆弧插补运动
        /// </summary>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">速度</param>
        /// <param name="posX">目标位置1</param>
        /// <param name="posY">目标位置2</param>
        /// <param name="dR">目标圆弧半径</param>
        /// <param name="iCCW">旋转方向</param>
        /// <param name="coordinateType">插补轴类型</param>
        /// <returns>执行结果 TRUE:成功 FALSE:失败</returns>
        public bool ArcMove(double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType coordinateType)
        {
         bool runFinsh=   actionX.ArcMove((short)tableData.axisXData.iCorNo,
                tableData.axisXData.AxisNo,
                tableData.axisYData.AxisNo,
                tableData.axisZData.AxisNo,
                dAcc,
                dDec,
                dSpeed / tableData.axisXData.plusToMM,
                posX / tableData.axisXData.plusToMM,
                posY / tableData.axisYData.plusToMM,
                dR / tableData.axisZData.plusToMM,
                iCCW,
                coordinateType);
            if (runFinsh==true)
            {
                runFinsh = true;
            }
            else
            {
                runFinsh = false;
            }
            return runFinsh;
        }

        /// <summary>
        /// 创建坐标系 默认创建XYZ坐标系
        /// </summary>
        public void BuildCor()
        {
            actionX.BuildCor((short)tableData.axisXData.iCorNo,
                tableData.axisXData.AxisNo,
                tableData.axisYData.AxisNo,
                tableData.axisZData.AxisNo);
        }
        public bool StartManualPulser(int Axis,int Speed)
        {
            bool ISok = false;
            ISok= actionX.StartManualPulserOperation(Axis, Speed);
            return ISok;
      
        }
        public bool StopManualPulser(int Axis)
        {
            bool ISok = false;
            ISok = actionX.StopManualPulserOperation(Axis); 
            return ISok;
          
        }
        /// <summary>
        /// 创建坐标系类型
        /// </summary>
        /// <param name="coordinateType">创建坐标系类型</param>
        public void BuildCor(CoordinateType coordinateType)
        {
            actionX.BuildCor((short)tableData.axisXData.iCorNo, coordinateType);
        }
        /// <summary>
        /// 插补执行状态
        /// </summary>
        /// <param name="iStep">返回插补执行到哪一步</param>
        /// <returns>插补执行状态 TRUE:插补执行完成 FALSE:插补执行未完成</returns>
        public bool CureMoveDone(out int iStep)
        {
            return actionX.CureMoveDone((short)tableData.axisXData.iCorNo, out iStep);
            //return true;
        }
        //public bool CureMoveDoneIsrunFinish(out int iStep, out int iStepIS)
        //{
        //    return actionX.CureMoveDoneIsrunFinish((short)tableData.axisXData.iCorNo, out iStep,out iStepIS);
        //    //return true;
        //}
        /// <summary>
        /// 获取平台状态
        /// </summary>
        /// <param name="ddPosX">当前X轴位置</param>
        /// <param name="bAlarmX">当前X轴报警状态</param>
        /// <param name="bCWLimitX">当前X轴正限位状态</param>
        /// <param name="bOrgX">当前X轴原点状态</param>
        /// <param name="bCCWLimitX">当前X轴负限位状态</param>
        /// <param name="bMovingX">当前X轴运动状态</param>
        /// <param name="dPosY">当前Y轴位置</param>
        /// <param name="bAlarmY">当前Y轴报警状态</param>
        /// <param name="bCWLimitY">当前Y轴正极限状态</param>
        /// <param name="bOrgY">当前Y轴原点状态</param>
        /// <param name="bCCWLimitY">当前Y轴负极限状态</param>
        /// <param name="bMovingY">当前Y轴运动状态</param>
        /// <param name="dPosZ">当前Z轴位置</param>
        /// <param name="bAlarmZ">当前Z轴报警状态</param>
        /// <param name="bCWLimitZ">当前Z轴正限位状态</param>
        /// <param name="bOrgZ">当前Z轴原点状态</param>
        /// <param name="bCCWLimitZ">当前Z轴负限位状态</param>
        /// <param name="bMovingZ">当前Z轴运动状态</param>
        /// <param name="dPosU">当前U轴位置</param>
        /// <param name="bAlarmU">当前U轴报警状态</param>
        /// <param name="bCWLimitU">当前U轴正限位状态</param>
        /// <param name="bOrgU">当前U轴原点状态</param>
        /// <param name="bCCWLimitU">当前U轴负限位状态</param>
        /// <param name="bMovingU">当前U轴运动状态</param>
        public void GetTableStatus(ref double ddPosX, ref bool bAlarmX, ref bool bCWLimitX, ref bool bOrgX, ref bool bCCWLimitX, ref bool bMovingX,
            ref double dPosY, ref bool bAlarmY, ref bool bCWLimitY, ref bool bOrgY, ref bool bCCWLimitY, ref bool bMovingY,
            ref double dPosZ, ref bool bAlarmZ, ref bool bCWLimitZ, ref bool bOrgZ, ref bool bCCWLimitZ, ref bool bMovingZ,
            ref double dPosU, ref bool bAlarmU, ref bool bCWLimitU, ref bool bOrgU, ref bool bCCWLimitU, ref bool bMovingU)
        {
            ddPosX = 0.0;
            bAlarmX = false;
            bCWLimitX = false;
            bOrgX = false;
            bCCWLimitX = false;
            bMovingX = false;

            dPosY = 0.0;
            bAlarmY = false;
            bCWLimitY = false;
            bOrgY = false;
            bCCWLimitY = false;
            bMovingY = false;

            dPosZ = 0.0;
            bAlarmZ = false;
            bCWLimitZ = false;
            bOrgZ = false;
            bCCWLimitZ = false;
            bMovingZ = false;

            dPosU = 0.0;
            bAlarmU = false;
            bCWLimitU = false;
            bOrgU = false;
            bCCWLimitU = false;
            bMovingU = false;
            if (bReadyX)
            {
                try
                {
                    ddPosX = actionX.GetCurrentPos(tableData.axisXData.AxisNo) * tableData.axisXData.plusToMM;
                    bAlarmX = actionX.GetAlarm(tableData.axisXData.AxisNo);
                    bCWLimitX = actionX.GetLimtCW(tableData.axisXData.AxisNo);
                    bOrgX = actionX.GetHome(tableData.axisXData.AxisNo);
                    bCCWLimitX = actionX.GetLimtCCW(tableData.axisXData.AxisNo);
                    bMovingX = actionX.IsMoving(tableData.axisXData.AxisNo);
                }
                catch
                {
                }
            }
            if (bReadyY)
            {
                try
                {
                    dPosY = actionY.GetCurrentPos(tableData.axisYData.AxisNo) * tableData.axisYData.plusToMM; 
                    
                    bAlarmY = actionY.GetAlarm(tableData.axisYData.AxisNo);
                    bCWLimitY = actionY.GetLimtCW(tableData.axisYData.AxisNo);
                    bOrgY = actionY.GetHome(tableData.axisYData.AxisNo);
                    bCCWLimitY = actionY.GetLimtCCW(tableData.axisYData.AxisNo);
                    bMovingY = actionY.IsMoving(tableData.axisYData.AxisNo);
                }
                catch
                {
                }
            }
            if (bReadyZ)
            {
                try
                {
                    dPosZ = actionZ.GetCurrentPos(tableData.axisZData.AxisNo) * tableData.axisZData.plusToMM; ;
                    bAlarmZ = actionZ.GetAlarm(tableData.axisZData.AxisNo);
                    bCWLimitZ = actionZ.GetLimtCW(tableData.axisZData.AxisNo);
                    bOrgZ = actionZ.GetHome(tableData.axisZData.AxisNo);
                    bCCWLimitZ = actionZ.GetLimtCCW(tableData.axisZData.AxisNo);
                    bMovingZ = actionZ.IsMoving(tableData.axisZData.AxisNo);
                }
                catch
                {
                }
            }
            if (bReadyU)
            {
                try
                {
                    dPosU = actionU.GetCurrentPos(tableData.axisUData.AxisNo) * tableData.axisUData.plusToMM; ;
                    bAlarmU = actionU.GetAlarm(tableData.axisUData.AxisNo);
                    bCWLimitU = actionU.GetLimtCW(tableData.axisUData.AxisNo);
                    bOrgU = actionU.GetHome(tableData.axisUData.AxisNo);
                    bCCWLimitU = actionU.GetLimtCCW(tableData.axisUData.AxisNo);
                    bMovingU = actionU.IsMoving(tableData.axisUData.AxisNo);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 回原点运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// 

        //Thread HomeX = null;
        //Thread HomeY = null;
        //Thread HomeZ = null;
        //Thread HomeU = null;
        //List<Thread> threadList = new List<Thread>();
        public void Home(TableAxisName axis)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    if (bHomingX == false)
                    {
                        try
                        {
                            System.Threading.Thread thread = new System.Threading.Thread(ThreadHomeXFunction);
                            thread.IsBackground = true;
                            thread.Start();
                            bHomingX = true;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    if (bHomingY == false)
                    {
                        try
                        {
                            System.Threading.Thread thread = new System.Threading.Thread(ThreadHomeYFunction);
                            thread.IsBackground = true;
                            thread.Start();
                            bHomingY = true;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    if (bHomingZ == false)
                    {
                        try
                        {
                            System.Threading.Thread thread = new System.Threading.Thread(ThreadHomeZFunction);
                            thread.IsBackground = true;
                            thread.Start();
                            bHomingZ = true;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    if (bHomingU == false)
                    {
                        try
                        {
                            System.Threading.Thread thread = new System.Threading.Thread(ThreadHomeUFunction);
                            thread.IsBackground = true;
                            thread.Start();
                            bHomingU = true;
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 回原完成状态 TRUE:回原点完成 FALSE:回原点未完成
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <returns>TRUE:回原点完成 FALSE:回原点未完成</returns>
        public bool HomeDone(TableAxisName axis)
        {
            bool bDone = false;
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        bDone = actionX.FinishSearchHome(tableData.axisXData.AxisNo);
                        if (bDone)
                        {
                            bHomingX = false;
                        }
                        if (bDone)
                        {
                            moveModeX = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        bDone = actionY.FinishSearchHome(tableData.axisYData.AxisNo);
                        if (bDone)
                        {
                            bHomingY = false;
                        }
                        if (bDone)
                        {
                            moveModeY = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        bDone = actionZ.FinishSearchHome(tableData.axisZData.AxisNo);
                        if (bDone)
                        {
                            bHomingZ = false;
                        }
                        if (bDone)
                        {
                            moveModeZ = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        bDone = actionU.FinishSearchHome(tableData.axisUData.AxisNo);
                        if (bDone)
                        {
                            bHomingU = false;
                        }
                        if (bDone)
                        {
                            moveModeU = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return bDone;
        }
        /// <summary>
        /// 初始化平台
        /// </summary>
        /// <param name="data">平台设置数据</param>
        public void Init(TableData data)
        {
            tableData = data;
            strDriverName = data.strTableName;
            tablePreStatus = new TablePreStatus();
            try
            {
                if (HardwareManage.hardwardDictionary[tableData.axisXData.MotionCardName] is IMotionAction)
                {
                    if (tableData.axisXData.iUsed == 1)
                    {
                        actionX = (IMotionAction)HardwareManage.hardwardDictionary[tableData.axisXData.MotionCardName];
                        iAxisNOX = tableData.axisXData.AxisNo;
                        if (HardwareManage.hardwardDictionary[tableData.axisXData.MotionCardName].bInitOK)
                        {
                            bReadyX = true;
                            actionX.ServoOn(tableData.axisXData.AxisNo);
                            if (tableData.axisXData.bUsedConfig == false)
                            {
                                if (tableData.axisXData.limtLogic == SenserLogic.NC)
                                {
                                    actionX.SetLimtOn(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.limtLogic == SenserLogic.NO)
                                {
                                    actionX.SetLimtOff(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.limtLogic == SenserLogic.DISABLE)
                                {
                                    actionX.SetLimtDisable(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.orgLogic == SenserLogic.NC)
                                {
                                    actionX.SetHomeOff(tableData.axisXData.AxisNo);
                                }
                                else
                                {
                                    actionX.SetHomeOn(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.orgNearLogic == SenserLogic.NC)
                                {
                                    actionX.SetNearHomeOn(tableData.axisXData.AxisNo);
                                }
                                else
                                {
                                    actionX.SetNearHomeOn(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.alarmLogic == SenserLogic.NC)
                                {
                                    actionX.SetAlarmOn(tableData.axisXData.AxisNo);
                                }
                                else
                                {
                                    actionX.SetAlarmOff(tableData.axisXData.AxisNo);
                                }
                                if (tableData.axisXData.pulseMode == PulseMode.PLDI)
                                {
                                    actionX.SetPulseMode(tableData.axisXData.AxisNo, PulseMode.PLDI);
                                }
                                else
                                {
                                    actionX.SetPulseMode(tableData.axisXData.AxisNo, PulseMode.CWCCW);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (HardwareManage.hardwardDictionary[tableData.axisYData.MotionCardName] is IMotionAction)
                {
                    if (tableData.axisYData.iUsed == 1)
                    {
                        actionY = (IMotionAction)HardwareManage.hardwardDictionary[tableData.axisYData.MotionCardName];
                        iAxisNOY = tableData.axisYData.AxisNo;

                        if (HardwareManage.hardwardDictionary[tableData.axisYData.MotionCardName].bInitOK)
                        {
                            bReadyY = true;
                            actionY.ServoOn(tableData.axisYData.AxisNo);
                            if (tableData.axisYData.bUsedConfig == false)
                            {
                                if (tableData.axisYData.limtLogic == SenserLogic.NC)
                                {
                                    actionY.SetLimtOn(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.limtLogic == SenserLogic.NO)
                                {
                                    actionY.SetLimtOff(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.limtLogic == SenserLogic.DISABLE)
                                {
                                    actionY.SetLimtDisable(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.orgLogic == SenserLogic.NC)
                                {
                                    actionY.SetHomeOff(tableData.axisYData.AxisNo);
                                }
                                else
                                {
                                    actionY.SetHomeOn(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.orgNearLogic == SenserLogic.NC)
                                {
                                    actionY.SetNearHomeOn(tableData.axisYData.AxisNo);
                                }
                                else
                                {
                                    actionY.SetNearHomeOn(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.alarmLogic == SenserLogic.NC)
                                {
                                    actionY.SetAlarmOn(tableData.axisYData.AxisNo);
                                }
                                else
                                {
                                    actionY.SetAlarmOff(tableData.axisYData.AxisNo);
                                }
                                if (tableData.axisYData.pulseMode == PulseMode.PLDI)
                                {
                                    actionY.SetPulseMode(tableData.axisYData.AxisNo, PulseMode.PLDI);
                                }
                                else
                                {
                                    actionY.SetPulseMode(tableData.axisYData.AxisNo, PulseMode.CWCCW);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (HardwareManage.hardwardDictionary[tableData.axisZData.MotionCardName] is IMotionAction)
                {
                    if (tableData.axisZData.iUsed == 1)
                    {
                        actionZ = (IMotionAction)HardwareManage.hardwardDictionary[tableData.axisZData.MotionCardName];
                        iAxisNOZ = tableData.axisZData.AxisNo;
                        if (HardwareManage.hardwardDictionary[tableData.axisZData.MotionCardName].bInitOK)
                        {
                            bReadyZ = true;
                            actionZ.ServoOn(tableData.axisZData.AxisNo);
                            if (tableData.axisZData.bUsedConfig == false)
                            {
                                if (tableData.axisZData.limtLogic == SenserLogic.NC)
                                {
                                    actionZ.SetLimtOn(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.limtLogic == SenserLogic.NO)
                                {
                                    actionZ.SetLimtOff(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.limtLogic == SenserLogic.DISABLE)
                                {
                                    actionZ.SetLimtDisable(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.orgLogic == SenserLogic.NC)
                                {
                                    actionZ.SetHomeOff(tableData.axisZData.AxisNo);
                                }
                                else
                                {
                                    actionZ.SetHomeOn(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.orgNearLogic == SenserLogic.NC)
                                {
                                    actionZ.SetNearHomeOn(tableData.axisZData.AxisNo);
                                }
                                else
                                {
                                    actionZ.SetNearHomeOn(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.alarmLogic == SenserLogic.NC)
                                {
                                    actionZ.SetAlarmOn(tableData.axisZData.AxisNo);
                                }
                                else
                                {
                                    actionZ.SetAlarmOff(tableData.axisZData.AxisNo);
                                }
                                if (tableData.axisZData.pulseMode == PulseMode.PLDI)
                                {
                                    actionZ.SetPulseMode(tableData.axisZData.AxisNo, PulseMode.PLDI);
                                }
                                else
                                {
                                    actionZ.SetPulseMode(tableData.axisZData.AxisNo, PulseMode.CWCCW);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (HardwareManage.hardwardDictionary[tableData.axisUData.MotionCardName] is IMotionAction)
                {
                    if (tableData.axisUData.iUsed == 1)
                    {
                        actionU = (IMotionAction)HardwareManage.hardwardDictionary[tableData.axisUData.MotionCardName];
                        iAxisNOU = tableData.axisUData.AxisNo;

                        if (HardwareManage.hardwardDictionary[tableData.axisUData.MotionCardName].bInitOK)
                        {
                            bReadyU = true;
                            actionU.ServoOn(tableData.axisUData.AxisNo);
                            if (tableData.axisUData.bUsedConfig == false)
                            {
                                if (tableData.axisUData.limtLogic == SenserLogic.NC)
                                {
                                    actionU.SetLimtOn(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.limtLogic == SenserLogic.NO)
                                {
                                    actionU.SetLimtOff(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.limtLogic == SenserLogic.DISABLE)
                                {
                                    actionU.SetLimtDisable(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.orgLogic == SenserLogic.NC)
                                {
                                    actionU.SetHomeOff(tableData.axisUData.AxisNo);
                                }
                                else
                                {
                                    actionU.SetHomeOn(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.orgNearLogic == SenserLogic.NC)
                                {
                                    actionU.SetNearHomeOn(tableData.axisUData.AxisNo);
                                }
                                else
                                {
                                    actionU.SetNearHomeOn(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.alarmLogic == SenserLogic.NC)
                                {
                                    actionU.SetAlarmOn(tableData.axisUData.AxisNo);
                                }
                                else
                                {
                                    actionU.SetAlarmOff(tableData.axisUData.AxisNo);
                                }
                                if (tableData.axisUData.pulseMode == PulseMode.PLDI)
                                {
                                    actionU.SetPulseMode(tableData.axisUData.AxisNo, PulseMode.PLDI);
                                }
                                else
                                {
                                    actionU.SetPulseMode(tableData.axisUData.AxisNo, PulseMode.CWCCW);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 插入圆弧插补运动
        /// </summary>
        /// <param name="ddPosX">X轴目标位置</param>
        /// <param name="dPosY">Y轴目标位置</param>
        /// <param name="dR">目标圆弧半径</param>
        /// <param name="dSpeed">目标合成速度</param>
        /// <param name="iCCW">旋转方向</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dEndSpeed">结束速度</param>
        public void InsertArc(double ddPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {
            actionX.InsertArc((short)tableData.axisXData.iCorNo,
                ddPosX / tableData.axisXData.plusToMM,
                dPosY / tableData.axisYData.plusToMM,
                dR / tableData.axisZData.plusToMM,
                dSpeed / tableData.axisZData.plusToMM,
                iCCW,
                dAcc,
                dEndSpeed / tableData.axisXData.plusToMM);
        }
        /// <summary>
        /// 插入直线插补运动
        /// </summary>
        /// <param name="ddPosX">X轴目标位置</param>
        /// <param name="dPosY">Y轴目标位置</param>
        /// <param name="dPosZ">Z轴目标位置</param>
        /// <param name="dSpeed">目标合成速度</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dEndSpeed">目标结束速度</param>
        public void InsertLine(double ddPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {
            actionX.InsertLine((short)tableData.axisXData.iCorNo,
                ddPosX / tableData.axisXData.plusToMM,
                dPosY / tableData.axisYData.plusToMM,
                dPosZ / tableData.axisZData.plusToMM,
                dSpeed / tableData.axisZData.plusToMM,
                dAcc,
                dEndSpeed / tableData.axisXData.plusToMM);
        }
        /// <summary>
        /// 是否在某个点位上
        /// </summary>
        /// <param name="strPosName">点位名称</param>
        /// <returns>TRUE:在点位上 FALSE:不在点位上</returns>
        public bool IsOnPos(string strPosName)
        {
            double dTargetPos = 0.0;
            double dCurrentPos = 0.0;
            bool bRec = true;
            if (bReadyX)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionX)
                    {
                        dTargetPos = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX;
                        dCurrentPos = actionX.GetCurrentPos(tableData.axisXData.AxisNo) * tableData.axisXData.plusToMM;
                        if (Math.Abs(dTargetPos - dCurrentPos) < 0.01)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyY)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionY)
                    {
                        dTargetPos = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY;
                        dCurrentPos = actionY.GetCurrentPos(tableData.axisYData.AxisNo) * tableData.axisYData.plusToMM;
                        if (Math.Abs(dTargetPos - dCurrentPos) <0.001)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyZ)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionZ)
                    {
                        dTargetPos = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ;
                        dCurrentPos = actionZ.GetCurrentPos(tableData.axisZData.AxisNo) * tableData.axisZData.plusToMM;
                        if (Math.Abs(dTargetPos - dCurrentPos) < 0.001)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyU)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionU)
                    {
                        dTargetPos = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU;
                        dCurrentPos = actionU.GetCurrentPos(tableData.axisUData.AxisNo) * tableData.axisUData.plusToMM;
                        if (Math.Abs(dTargetPos - dCurrentPos) < 0.001)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                }
            }
            return bRec;
        }
        /// <summary>
        /// JOG运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="bCW">运动方向 TRUE:正方向 FALSE:负方向</param>
        /// <param name="bHighSpd">是否高速运动</param>
        public void JogMove(TableAxisName axis, bool bCW, bool bHighSpd)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                actionX.JogMove(tableData.axisXData.AxisNo,
                                                    tableData.axisXData.dAcc,
                                                    tableData.axisXData.dDec,
                                                    tableData.axisXData.dJobHigh / tableData.axisXData.plusToMM);
                                moveModeX = MoveMode.JOG;
                                dSpeedX = tableData.axisXData.dJobHigh;
                                dAccX = tableData.axisZData.dAcc;
                                dDecX = tableData.axisZData.dDec;
                            }
                            else
                            {
                                bool b = actionX.JogMove(tableData.axisXData.AxisNo,
                                                   tableData.axisXData.dAcc,
                                                   tableData.axisXData.dDec,
                                                   tableData.axisXData.dJobLow / tableData.axisXData.plusToMM);
                                moveModeX = MoveMode.JOG;
                                dSpeedX = tableData.axisXData.dJobLow;
                                dAccX = tableData.axisZData.dAcc;
                                dDecX = tableData.axisZData.dDec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                actionX.JogMove(tableData.axisXData.AxisNo,
                                                    tableData.axisXData.dAcc,
                                                    tableData.axisXData.dDec,
                                                    -tableData.axisXData.dJobHigh / tableData.axisXData.plusToMM);
                                moveModeX = MoveMode.JOG;
                                dSpeedX = -tableData.axisXData.dJobHigh;
                                dAccX = tableData.axisZData.dAcc;
                                dDecX = tableData.axisZData.dDec;
                            }
                            else
                            {
                                actionX.JogMove(tableData.axisXData.AxisNo,
                                                   tableData.axisXData.dAcc,
                                                   tableData.axisXData.dDec,
                                                   -tableData.axisXData.dJobLow / tableData.axisXData.plusToMM);
                                moveModeX = MoveMode.JOG;
                                dSpeedX = -tableData.axisXData.dJobLow;
                                dAccX = tableData.axisZData.dAcc;
                                dDecX = tableData.axisZData.dDec;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                actionY.JogMove(tableData.axisYData.AxisNo,
                                                    tableData.axisYData.dAcc,
                                                    tableData.axisYData.dDec,
                                                    tableData.axisYData.dJobHigh / tableData.axisYData.plusToMM);
                                moveModeY = MoveMode.JOG;
                                dSpeedY = tableData.axisYData.dJobHigh;
                                dAccY = tableData.axisYData.dAcc;
                                dDecY = tableData.axisYData.dDec;
                            }
                            else
                            {
                                actionY.JogMove(tableData.axisYData.AxisNo,
                                                   tableData.axisYData.dAcc,
                                                   tableData.axisYData.dDec,
                                                   tableData.axisYData.dJobLow / tableData.axisYData.plusToMM);
                                moveModeY = MoveMode.JOG;
                                dSpeedY = tableData.axisYData.dJobLow;
                                dAccY = tableData.axisYData.dAcc;
                                dDecY = tableData.axisYData.dDec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                actionY.JogMove(tableData.axisYData.AxisNo,
                                                    tableData.axisYData.dAcc,
                                                    tableData.axisYData.dDec,
                                                    -tableData.axisYData.dJobHigh / tableData.axisYData.plusToMM);
                                moveModeY = MoveMode.JOG;
                                dSpeedY = -tableData.axisYData.dJobHigh;
                                dAccY = tableData.axisYData.dAcc;
                                dDecY = tableData.axisYData.dDec;
                            }
                            else
                            {
                                actionY.JogMove(tableData.axisYData.AxisNo,
                                                   tableData.axisYData.dAcc,
                                                   tableData.axisYData.dDec,
                                                   -tableData.axisYData.dJobLow / tableData.axisYData.plusToMM);
                                moveModeY = MoveMode.JOG;
                                dSpeedY = -tableData.axisYData.dJobLow;
                                dAccY = tableData.axisYData.dAcc;
                                dDecY = tableData.axisYData.dDec;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                actionZ.JogMove(tableData.axisZData.AxisNo,
                                                    tableData.axisZData.dAcc,
                                                    tableData.axisZData.dDec,
                                                    tableData.axisZData.dJobHigh / tableData.axisZData.plusToMM);
                                moveModeZ = MoveMode.JOG;
                                dSpeedZ = tableData.axisZData.dJobHigh;
                                dAccZ = tableData.axisZData.dAcc;
                                dDecZ = tableData.axisZData.dDec;
                            }
                            else
                            {
                                actionZ.JogMove(tableData.axisZData.AxisNo,
                                                   tableData.axisZData.dAcc,
                                                   tableData.axisZData.dDec,
                                                   tableData.axisZData.dJobLow / tableData.axisZData.plusToMM);
                                moveModeZ = MoveMode.JOG;
                                dSpeedZ = tableData.axisZData.dJobLow;
                                dAccZ = tableData.axisZData.dAcc;
                                dDecZ = tableData.axisZData.dDec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                actionZ.JogMove(tableData.axisZData.AxisNo,
                                                    tableData.axisZData.dAcc,
                                                    tableData.axisZData.dDec,
                                                    -tableData.axisZData.dJobHigh / tableData.axisZData.plusToMM);
                                moveModeZ = MoveMode.JOG;
                                dSpeedZ = -tableData.axisZData.dJobHigh;
                                dAccZ = tableData.axisZData.dAcc;
                                dDecZ = tableData.axisZData.dDec;
                            }
                            else
                            {
                                actionZ.JogMove(tableData.axisZData.AxisNo,
                                                   tableData.axisZData.dAcc,
                                                   tableData.axisZData.dDec,
                                                   -tableData.axisZData.dJobLow / tableData.axisZData.plusToMM);
                                moveModeZ = MoveMode.JOG;
                                dSpeedZ = -tableData.axisZData.dJobLow;
                                dAccZ = tableData.axisZData.dAcc;
                                dDecZ = tableData.axisZData.dDec;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                actionU.JogMove(tableData.axisUData.AxisNo,
                                                    tableData.axisUData.dAcc,
                                                    tableData.axisUData.dDec,
                                                    tableData.axisUData.dJobHigh / tableData.axisUData.plusToMM);
                                moveModeU = MoveMode.JOG;
                                dSpeedU = tableData.axisUData.dJobHigh;
                                dAccU = tableData.axisUData.dAcc;
                                dDecU = tableData.axisUData.dDec;
                            }
                            else
                            {
                                actionU.JogMove(tableData.axisUData.AxisNo,
                                                   tableData.axisUData.dAcc,
                                                   tableData.axisUData.dDec,
                                                   tableData.axisUData.dJobLow / tableData.axisUData.plusToMM);
                                moveModeU = MoveMode.JOG;
                                dSpeedU = tableData.axisUData.dJobLow;
                                dAccU = tableData.axisUData.dAcc;
                                dDecU = tableData.axisUData.dDec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                actionU.JogMove(tableData.axisUData.AxisNo,
                                                    tableData.axisUData.dAcc,
                                                    tableData.axisUData.dDec,
                                                    -tableData.axisUData.dJobHigh / tableData.axisUData.plusToMM);
                                moveModeU = MoveMode.JOG;
                                dSpeedU = -tableData.axisUData.dJobHigh;
                                dAccU = tableData.axisUData.dAcc;
                                dDecU = tableData.axisUData.dDec;
                            }
                            else
                            {
                                actionU.JogMove(tableData.axisUData.AxisNo,
                                                   tableData.axisUData.dAcc,
                                                   tableData.axisUData.dDec,
                                                   -tableData.axisUData.dJobLow / tableData.axisUData.plusToMM);
                                moveModeU = MoveMode.JOG;
                                dSpeedU = -tableData.axisUData.dJobLow;
                                dAccU = tableData.axisUData.dAcc;
                                dDecU = tableData.axisUData.dDec;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// JOG运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">目标速度</param>
        public void JogMove(TableAxisName axis, double dAcc, double dDec, double dSpeed)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.JogMove(tableData.axisXData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.JOG;
                        dSpeedX = dSpeed;
                        dAccX = dAcc;
                        dDecX = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.JogMove(tableData.axisYData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.JOG;
                        dSpeedY = dSpeed;
                        dAccY = dAcc;
                        dDecY = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.JogMove(tableData.axisZData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.JOG;
                        dSpeedZ = dSpeed;
                        dAccZ = dAcc;
                        dDecZ = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.JogMove(tableData.axisUData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.JOG;
                        dSpeedU = dSpeed;
                        dAccU = dAcc;
                        dDecU = dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public bool LimitDone(TableAxisName Axis)
        {
            bool bDone = false;
            if (Axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        if (tableData.axisXData.iOrgMode == 0 || tableData.axisXData.iOrgMode == 1)
                        {
                            bDone = actionX.FinishSearchLimit(tableData.axisXData.AxisNo);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            moveModeX = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        if (tableData.axisYData.iOrgMode == 0 || tableData.axisYData.iOrgMode == 1)
                        {
                            bDone = actionY.FinishSearchLimit(tableData.axisYData.AxisNo);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            moveModeY = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        if (tableData.axisZData.iOrgMode == 0 || tableData.axisZData.iOrgMode == 1)
                        {
                            bDone = actionZ.FinishSearchLimit(tableData.axisZData.AxisNo);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            moveModeZ = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        if (tableData.axisUData.iOrgMode == 0 || tableData.axisUData.iOrgMode == 1)
                        {
                            bDone = actionU.FinishSearchLimit(tableData.axisUData.AxisNo);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            moveModeU = MoveMode.STOP;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return bDone;
        }


     
        /// <summary>
        /// 直线插补运动
        /// </summary>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">目标合成速度</param>
        /// <param name="posX">X轴目标位置</param>
        /// <param name="posY">Y轴目标位置</param>
        /// <param name="posZ">Z轴目标位置</param>
        /// <returns>TRUE:执行成功 FALSE:执行失败</returns>
        public bool LineXYZMove(double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
     bool  LineXYZMove = actionX.LineXYZMove((short)tableData.axisXData.iCorNo,
                tableData.axisXData.AxisNo, tableData.axisYData.AxisNo,
                tableData.axisZData.AxisNo, dAcc,
                dDec,
                  dSpeed / tableData.axisXData.plusToMM,
                posX / tableData.axisXData.plusToMM,
                posY / tableData.axisYData.plusToMM,
                posZ / tableData.axisZData.plusToMM);

            if (LineXYZMove == true)
            {
                LineXYZMove = true;
            }
            else
            {
                LineXYZMove = false;
            }
            return LineXYZMove;
        }
        /// <summary>
        /// 所有轴是否运动完成
        /// </summary>
        /// <returns>TRUE:运动完成 FALSE:运动未完成</returns>
        public bool MoveDone()
        {
            bool bMoveDoneX = false;
            bool bMoveDoneY = false;
            bool bMoveDoneZ = false;
            bool bMoveDoneU = false;
            if (bReadyX)
            {
                bMoveDoneX = actionX.IsMoveDone(tableData.axisXData.AxisNo);
                if (bMoveDoneX)
                {
                    //moveModeX = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneX = true;
            }
            if (bReadyY)
            {
                bMoveDoneY = actionY.IsMoveDone(tableData.axisYData.AxisNo);
                if (bMoveDoneY)
                {
                    //moveModeY = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneY = true;
            }
            if (bReadyZ)
            {
                bMoveDoneZ = actionZ.IsMoveDone(tableData.axisZData.AxisNo);
                if (bMoveDoneZ)
                {
                    //moveModeZ = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneZ = true;
            }
            if (bReadyU)
            {
                bMoveDoneU = actionU.IsMoveDone(tableData.axisUData.AxisNo);
                if (bMoveDoneU)
                {
                    // moveModeU = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneU = true;
            }
            return bMoveDoneX && bMoveDoneY && bMoveDoneZ && bMoveDoneU;
        }
        /// <summary>
        /// 指定轴是否运动完成
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <returns>TRUE:运动完成 FALSE:运动未完成</returns>
        public bool MoveDone(TableAxisName axis)
        {
            bool bMoveDone = false;
            bool bMoveDoneX = false;
            bool bMoveDoneY = false;
            bool bMoveDoneZ = false;
            bool bMoveDoneU = false;
            switch (axis)
            {
                case TableAxisName.X:
                    if (bReadyX)
                    {
                        bMoveDone = actionX.IsMoveDone(tableData.axisXData.AxisNo);
                        if (bMoveDone)
                        {
                            moveModeX = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDone = true;
                    }
                    break;
                case TableAxisName.Y:
                    if (bReadyY)
                    {
                        bMoveDone = actionY.IsMoveDone(tableData.axisYData.AxisNo);
                        if (bMoveDone)
                        {
                            moveModeY = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDone = true;
                    }
                    break;
                case TableAxisName.Z:
                    if (bReadyZ)
                    {
                        bMoveDone = actionZ.IsMoveDone(tableData.axisZData.AxisNo);
                        if (bMoveDone)
                        {
                            moveModeZ = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDone = true;
                    }
                    break;
                case TableAxisName.U:
                    if (bReadyU)
                    {
                        bMoveDone = actionU.IsMoveDone(tableData.axisUData.AxisNo);
                        if (bMoveDone)
                        {
                            moveModeU = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDone = true;
                    }
                    break;
                case TableAxisName.ALL:
                    if (bReadyX)
                    {
                        bMoveDoneX = actionX.IsMoveDone(tableData.axisXData.AxisNo);
                        if (bMoveDoneX)
                        {
                            moveModeX = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDoneX = true;
                    }
                    if (bReadyY)
                    {
                        bMoveDoneY = actionY.IsMoveDone(tableData.axisYData.AxisNo);
                        if (bMoveDoneY)
                        {
                            moveModeY = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDoneY = true;
                    }
                    if (bReadyZ)
                    {
                        bMoveDoneZ = actionZ.IsMoveDone(tableData.axisZData.AxisNo);
                        if (bMoveDoneZ)
                        {
                            moveModeZ = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDoneZ = true;
                    }
                    if (bReadyU)
                    {
                        bMoveDoneU = actionU.IsMoveDone(tableData.axisUData.AxisNo);
                        if (bMoveDoneU)
                        {
                            moveModeU = MoveMode.STOP;
                        }
                    }
                    else
                    {
                        bMoveDoneU = true;
                    }
                    return bMoveDoneX && bMoveDoneY && bMoveDoneZ && bMoveDoneU;
                default:
                    break;
            }
            return bMoveDone;
        }
        /// <summary>
        /// 指定轴是否在运动中
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <returns>TRUE:在运动中 FALSE:不在运动中</returns>
        public bool Moving(TableAxisName axis)
        {
            bool bMoving = false;
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    bMoving = actionX.IsMoving(tableData.axisXData.AxisNo);
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    bMoving = actionY.IsMoving(tableData.axisYData.AxisNo);
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    bMoving = actionZ.IsMoving(tableData.axisZData.AxisNo);
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    bMoving = actionU.IsMoving(tableData.axisUData.AxisNo);
                }
            }
            return bMoving;
        }
        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">目标速度</param>
        /// <param name="dPos">目标巨鹿</param>
        public void RelMove(TableAxisName axis, double dAcc, double dDec, double dSpeed, double dPos)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.ReferPosMove(tableData.axisXData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisXData.plusToMM,
                                            dPos / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.REL;
                        dPosX = dPos;
                        dSpeedX = dSpeed;
                        dAccX = dAcc;
                        dDecX = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.ReferPosMove(tableData.axisYData.AxisNo,
                                           dAcc,
                                           dDec,
                                           dSpeed / tableData.axisYData.plusToMM,
                                            dPos / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.REL;
                        dPosY = dPos;
                        dSpeedY = dSpeed;
                        dAccY = dAcc;
                        dDecY = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.ReferPosMove(tableData.axisZData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisZData.plusToMM,
                                            dPos / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.REL;
                        dPosZ = dPos;
                        dSpeedZ = dSpeed;
                        dAccZ = dAcc;
                        dDecZ = dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.ReferPosMove(tableData.axisUData.AxisNo,
                                            dAcc,
                                            dDec,
                                            dSpeed / tableData.axisUData.plusToMM,
                                            dPos / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.REL;
                        dPosU = dPos;
                        dSpeedU = dSpeed;
                        dAccU = dAcc;
                        dDecU = dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="dPos">目标距离</param>
        /// <param name="bHighSpd">是否高速运动</param>
        public void RelMove(TableAxisName axis, double dPos, bool bHighSpd)
        {
            if (axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.ReferPosMove(tableData.axisXData.AxisNo,
                                            tableData.axisXData.dAcc,
                                            tableData.axisXData.dDec,
                                            tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                            dPos / tableData.axisXData.plusToMM);
                        moveModeX = MoveMode.REL;
                        dPosX = dPos;
                        dSpeedX = tableData.axisXData.dSpeed;
                        dAccX = tableData.axisXData.dAcc;
                        dDecX = tableData.axisXData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.ReferPosMove(tableData.axisYData.AxisNo,
                                            tableData.axisYData.dAcc,
                                            tableData.axisYData.dDec,
                                            tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                            dPos / tableData.axisYData.plusToMM);
                        moveModeY = MoveMode.REL;
                        dPosY = dPos;
                        dSpeedY = tableData.axisYData.dSpeed;
                        dAccY = tableData.axisYData.dAcc;
                        dDecY = tableData.axisYData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.ReferPosMove(tableData.axisZData.AxisNo,
                                            tableData.axisZData.dAcc,
                                            tableData.axisZData.dDec,
                                            tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                            dPos / tableData.axisZData.plusToMM);
                        moveModeZ = MoveMode.REL;
                        dPosZ = dPos;
                        dSpeedZ = tableData.axisZData.dSpeed;
                        dAccZ = tableData.axisZData.dAcc;
                        dDecZ = tableData.axisZData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.ReferPosMove(tableData.axisUData.AxisNo,
                                            tableData.axisUData.dAcc,
                                            tableData.axisUData.dDec,
                                            tableData.axisUData.dSpeed / tableData.axisUData.plusToMM,
                                            dPos / tableData.axisUData.plusToMM);
                        moveModeU = MoveMode.REL;
                        dPosU = dPos;
                        dSpeedU = tableData.axisUData.dSpeed;
                        dAccU = tableData.axisUData.dAcc;
                        dDecU = tableData.axisUData.dDec;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 恢复运动，可恢复之前正在执行的运动动作，除JOG运动
        /// </summary>
        public void ResumeMove()
        {
            if (bReadyX)
            {
                try
                {
                    if (moveModeX == MoveMode.ABS)
                    {
                        AbsMove(TableAxisName.X, dPosX);
                    }
                    if (moveModeX == MoveMode.REL)
                    {
                        //AbsMove(TableAxisName.X, ddPosX);
                    }
                    if (moveModeX == MoveMode.JOG)
                    {
                        //if (dSpeedX > 0)
                        //{
                        //    JogMove(TableAxisName.X, true, false);
                        //}
                        //else
                        //{
                        //    JogMove(TableAxisName.X, false, false);
                        //}
                    }
                    if (moveModeX == MoveMode.LIMIT)
                    {
                        StartLimit(TableAxisName.X);
                    }
                    if (moveModeX == MoveMode.HOME)
                    {
                        StartHome(TableAxisName.X);
                    }
                }
                catch
                {
                }
            }

            if (bReadyY)
            {
                try
                {
                    if (moveModeY == MoveMode.ABS)
                    {
                        AbsMove(TableAxisName.Y, dPosY);
                    }
                    if (moveModeY == MoveMode.REL)
                    {
                        //AbsMove(TableAxisName.Y, dPosY);
                    }
                    if (moveModeY == MoveMode.JOG)
                    {
                        //if (dSpeedY > 0)
                        //{
                        //    JogMove(TableAxisName.Y, true, false);
                        //}
                        //else
                        //{
                        //    JogMove(TableAxisName.Y, false, false);
                        //}
                    }
                    if (moveModeY == MoveMode.LIMIT)
                    {
                        StartLimit(TableAxisName.Y);
                    }
                    if (moveModeY == MoveMode.HOME)
                    {
                        StartHome(TableAxisName.Y);
                    }
                }
                catch
                {
                }
            }

            if (bReadyZ)
            {
                try
                {
                    if (moveModeZ == MoveMode.ABS)
                    {
                        AbsMove(TableAxisName.Z, dPosZ);
                    }
                    if (moveModeZ == MoveMode.REL)
                    {
                        //AbsMove(TableAxisName.Z, dPosZ);
                    }
                    if (moveModeZ == MoveMode.JOG)
                    {
                        //if (dSpeedZ > 0)
                        //{
                        //    JogMove(TableAxisName.Z, true, false);
                        //}
                        //else
                        //{
                        //    JogMove(TableAxisName.Z, false, false);
                        //}
                    }
                    if (moveModeZ == MoveMode.LIMIT)
                    {
                        StartLimit(TableAxisName.Z);
                    }
                    if (moveModeZ == MoveMode.HOME)
                    {
                        StartHome(TableAxisName.Z);
                    }
                }
                catch
                {
                }
            }

            if (bReadyU)
            {
                try
                {
                    if (moveModeU == MoveMode.ABS)
                    {
                        AbsMove(TableAxisName.U, dPosU);
                    }
                    if (moveModeU == MoveMode.REL)
                    {
                        //AbsMove(TableAxisName.U, dPosU);
                    }
                    if (moveModeU == MoveMode.JOG)
                    {
                        //if (dSpeedU > 0)
                        //{
                        //    JogMove(TableAxisName.U, true, false);
                        //}
                        //else
                        //{
                        //    JogMove(TableAxisName.U, false, false);
                        //}
                    }
                    if (moveModeU == MoveMode.LIMIT)
                    {
                        StartLimit(TableAxisName.U);
                    }
                    if (moveModeU == MoveMode.HOME)
                    {
                        StartHome(TableAxisName.U);
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 恢复插补运动，可恢复之前正在执行的插补运动动作
        /// </summary>
        public void ResumeCure()
        {
            actionX.ResumeInterpolation();
        }
        /// <summary>
        /// 开始插补运动
        /// </summary>
        /// <param name="bPreWelding"></param>
        public bool  StartCure(bool bPreWelding)
        {
            bool StartC=   actionX.StartCure((short)tableData.axisXData.iCorNo, bPreWelding);
            return StartC;
        }
        public double _GetAdc( short abc,   out double bPreWelding)
        {
           actionX.GT_GetAdc((short)tableData.axisXData.iCorNo, abc, out  bPreWelding);
           return bPreWelding;
        }

        
        /// <summary>
        /// 开始插补运动
        /// </summary>
        public void StartCure()
        {
            actionX.StartCure((short)tableData.axisXData.iCorNo, false);
        }
        /// <summary>
        /// 指定轴开始回原点运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        public void StartHome(TableAxisName axis)
        {
            if (axis == TableAxisName.X)
            {
                bHomingX = true;
                if (bReadyX)
                {
                    try
                    {
                        if (tableData.axisXData.iOrgMode == 0 || tableData.axisXData.iOrgMode == 2)
                        {
                            actionX.StartSearchHome(tableData.axisXData.AxisNo,
                                                        tableData.axisXData.dAcc,
                                                        tableData.axisXData.dDec,
                                                        tableData.axisXData.dOrgSpd / tableData.axisXData.plusToMM);
                        }
                        else
                        {
                            actionX.StartSearchHome(tableData.axisXData.AxisNo,
                                                        tableData.axisXData.dAcc,
                                                        tableData.axisXData.dDec,
                                                        -tableData.axisXData.dOrgSpd / tableData.axisXData.plusToMM);
                        }
                        moveModeX = MoveMode.HOME;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    bHomingY = true;
                    try
                    {
                        if (tableData.axisYData.iOrgMode == 0 || tableData.axisYData.iOrgMode == 2)
                        {
                            actionY.StartSearchHome(tableData.axisYData.AxisNo,
                                                        tableData.axisYData.dAcc,
                                                        tableData.axisYData.dDec,
                                                        tableData.axisYData.dOrgSpd / tableData.axisYData.plusToMM);
                        }
                        else
                        {
                            actionY.StartSearchHome(tableData.axisYData.AxisNo,
                                                        tableData.axisYData.dAcc,
                                                        tableData.axisYData.dDec,
                                                        -tableData.axisYData.dOrgSpd / tableData.axisYData.plusToMM);
                        }
                        moveModeY = MoveMode.HOME;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    bHomingZ = true;
                    try
                    {
                        if (tableData.axisZData.iOrgMode == 0 || tableData.axisZData.iOrgMode == 2)
                        {
                            actionZ.StartSearchHome(tableData.axisZData.AxisNo,
                                                        tableData.axisZData.dAcc,
                                                        tableData.axisZData.dDec,
                                                        tableData.axisZData.dOrgSpd / tableData.axisZData.plusToMM);
                        }
                        else
                        {
                            actionZ.StartSearchHome(tableData.axisZData.AxisNo,
                                                        tableData.axisZData.dAcc,
                                                        tableData.axisZData.dDec,
                                                        -tableData.axisZData.dOrgSpd / tableData.axisZData.plusToMM);
                        }
                        moveModeZ = MoveMode.HOME;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    bHomingU = true;
                    try
                    {
                        if (tableData.axisUData.iOrgMode == 0 || tableData.axisUData.iOrgMode == 2)
                        {
                            actionU.StartSearchHome(tableData.axisUData.AxisNo,
                                                        tableData.axisUData.dAcc,
                                                        tableData.axisUData.dDec,
                                                        tableData.axisUData.dOrgSpd / tableData.axisUData.plusToMM);
                        }
                        else
                        {
                            actionU.StartSearchHome(tableData.axisUData.AxisNo,
                                                        tableData.axisUData.dAcc,
                                                        tableData.axisUData.dDec,
                                                        -tableData.axisUData.dOrgSpd / tableData.axisUData.plusToMM);
                        }
                        moveModeU = MoveMode.HOME;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 指定轴开始搜索极限运动
        /// </summary>
        /// <param name="Axis">轴名称</param>
        public void StartLimit(TableAxisName Axis)
        {
            if (Axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    bHomingX = true;
                    try
                    {
                        if (tableData.axisXData.iOrgMode == 0)
                        {
                            actionX.StartSearchLimit(tableData.axisXData.AxisNo,
                                                    tableData.axisXData.dAcc,
                                                    tableData.axisXData.dDec,
                                                    -tableData.axisXData.dLimtSpd / tableData.axisXData.plusToMM);
                        }
                        if (tableData.axisXData.iOrgMode == 1)
                        {
                            actionX.StartSearchLimit(tableData.axisXData.AxisNo,
                                                    tableData.axisXData.dAcc,
                                                    tableData.axisXData.dDec,
                                                    tableData.axisXData.dLimtSpd / tableData.axisXData.plusToMM);
                        }
                        moveModeX = MoveMode.LIMIT;
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    bHomingY = true;
                    try
                    {
                        if (tableData.axisYData.iOrgMode == 0)
                        {
                            actionY.StartSearchLimit(tableData.axisYData.AxisNo,
                                                    tableData.axisYData.dAcc,
                                                    tableData.axisYData.dDec,
                                                    -tableData.axisYData.dLimtSpd / tableData.axisYData.plusToMM);
                        }
                        if (tableData.axisYData.iOrgMode == 1)
                        {
                            actionY.StartSearchLimit(tableData.axisYData.AxisNo,
                                                    tableData.axisYData.dAcc,
                                                    tableData.axisYData.dDec,
                                                    tableData.axisYData.dLimtSpd / tableData.axisYData.plusToMM);
                        }
                        moveModeY = MoveMode.LIMIT;
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    bHomingZ = true;
                    try
                    {
                        if (tableData.axisZData.iOrgMode == 0)
                        {
                            actionZ.StartSearchLimit(tableData.axisZData.AxisNo,
                                                    tableData.axisZData.dAcc,
                                                    tableData.axisZData.dDec,
                                                    -tableData.axisZData.dLimtSpd / tableData.axisZData.plusToMM);
                        }
                        if (tableData.axisZData.iOrgMode == 1)
                        {
                            actionZ.StartSearchLimit(tableData.axisZData.AxisNo,
                                                    tableData.axisZData.dAcc,
                                                    tableData.axisZData.dDec,
                                                    tableData.axisZData.dLimtSpd / tableData.axisZData.plusToMM);
                        }
                        moveModeZ = MoveMode.LIMIT;
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    bHomingU = true;
                    try
                    {
                        if (tableData.axisUData.iOrgMode == 0)
                        {
                            actionU.StartSearchLimit(tableData.axisUData.AxisNo,
                                                    tableData.axisUData.dAcc,
                                                    tableData.axisUData.dDec,
                                                    -tableData.axisUData.dLimtSpd / tableData.axisUData.plusToMM);
                        }
                        if (tableData.axisUData.iOrgMode == 1)
                        {
                            actionU.StartSearchLimit(tableData.axisUData.AxisNo,
                                                    tableData.axisUData.dAcc,
                                                    tableData.axisUData.dDec,
                                                    tableData.axisUData.dLimtSpd / tableData.axisUData.plusToMM);
                        }
                        moveModeU = MoveMode.LIMIT;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 点位运动，运动到指定点位
        /// </summary>
        /// <param name="strPosName">点位名称</param>
        public void StartPosMove(string strPosName)
        {
            if (bReadyX)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionX)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionX.AbsPosMove(tableData.axisXData.
                                                AxisNo, tableData.axisXData.dAcc,
                                                tableData.axisXData.dDec,
                                                tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX / tableData.axisXData.plusToMM);
                            moveModeX = MoveMode.ABS;
                            dPosX = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX;
                            dSpeedX = tableData.axisXData.dSpeed;
                            dAccX = tableData.axisXData.dAcc;
                            dDecX = tableData.axisXData.dDec;
                        }
                        else
                        {
                            actionX.ReferPosMove(tableData.axisXData.
                                                AxisNo, tableData.axisXData.dAcc,
                                                tableData.axisXData.dDec,
                                                tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX / tableData.axisXData.plusToMM);
                            moveModeX = MoveMode.REL;
                            dPosX = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX;
                            dSpeedX = tableData.axisXData.dSpeed;
                            dAccX = tableData.axisXData.dAcc;
                            dDecX = tableData.axisXData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyY)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionY)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionY.AbsPosMove(tableData.axisYData.
                                                AxisNo, tableData.axisYData.dAcc,
                                                tableData.axisYData.dDec,
                                                tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY / tableData.axisYData.plusToMM);
                            moveModeY = MoveMode.ABS;
                            dPosY = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY;
                            dSpeedY = tableData.axisYData.dSpeed;
                            dAccY = tableData.axisYData.dAcc;
                            dDecY = tableData.axisYData.dDec;
                        }
                        else
                        {
                            actionY.ReferPosMove(tableData.axisYData.
                                                AxisNo, tableData.axisYData.dAcc,
                                                tableData.axisYData.dDec,
                                                tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY / tableData.axisYData.plusToMM);
                            moveModeY = MoveMode.REL;
                            dPosY = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY;
                            dSpeedY = tableData.axisYData.dSpeed;
                            dAccY = tableData.axisYData.dAcc;
                            dDecY = tableData.axisYData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyZ)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionZ)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionZ.AbsPosMove(tableData.axisZData.
                                                AxisNo, tableData.axisZData.dAcc,
                                                tableData.axisZData.dDec,
                                                tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ / tableData.axisZData.plusToMM);
                            moveModeZ = MoveMode.ABS;
                            dPosZ = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ;
                            dSpeedZ = tableData.axisZData.dSpeed;
                            dAccZ = tableData.axisZData.dAcc;
                            dDecZ = tableData.axisZData.dDec;
                        }
                        else
                        {
                            actionZ.ReferPosMove(tableData.axisZData.
                                                AxisNo, tableData.axisZData.dAcc,
                                                tableData.axisZData.dDec,
                                                tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ / tableData.axisZData.plusToMM);
                            moveModeZ = MoveMode.REL;
                            dPosZ = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ;
                            dSpeedZ = tableData.axisZData.dSpeed;
                            dAccZ = tableData.axisZData.dAcc;
                            dDecZ = tableData.axisZData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyU)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionU)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionU.AbsPosMove(tableData.axisUData.
                                                AxisNo, tableData.axisUData.dAcc,
                                                tableData.axisUData.dDec,
                                                tableData.axisUData.dSpeed / tableData.axisUData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU / tableData.axisUData.plusToMM);
                            moveModeU = MoveMode.ABS;
                            dPosU = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU;
                            dSpeedU = tableData.axisUData.dSpeed;
                            dAccU = tableData.axisUData.dAcc;
                            dDecU = tableData.axisUData.dDec;
                        }
                        else
                        {
                            actionU.ReferPosMove(tableData.axisUData.
                                                AxisNo, tableData.axisUData.dAcc,
                                                tableData.axisUData.dDec,
                                                tableData.axisUData.dSpeed / tableData.axisUData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU / tableData.axisUData.plusToMM);
                            moveModeU = MoveMode.REL;
                            dPosU = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU;
                            dSpeedU = tableData.axisUData.dSpeed;
                            dAccU = tableData.axisUData.dAcc;
                            dDecU = tableData.axisUData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 停止指定轴动作
        /// </summary>
        /// <param name="axis">轴名称</param>
        public void Stop(TableAxisName axis)
        {
            //actionX.StopInterpolation();
            if (axis == TableAxisName.X || axis == TableAxisName.ALL)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.StopMove(tableData.axisXData.AxisNo);
                        //moveModeX = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y || axis == TableAxisName.ALL)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.StopMove(tableData.axisYData.AxisNo);
                        //moveModeY = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z || axis == TableAxisName.ALL)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.StopMove(tableData.axisZData.AxisNo);
                        //moveModeZ = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U || axis == TableAxisName.ALL)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.StopMove(tableData.axisUData.AxisNo);
                        //moveModeU = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 停止指定轴JOG运动
        /// </summary>
        /// <param name="axis">轴名称</param>
        public void JogStop(TableAxisName axis)
        {
            if (axis == TableAxisName.X || axis == TableAxisName.ALL)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.StopJog(tableData.axisXData.AxisNo);
                        moveModeX = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Y || axis == TableAxisName.ALL)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.StopJog(tableData.axisYData.AxisNo);
                        moveModeY = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.Z || axis == TableAxisName.ALL)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.StopJog(tableData.axisZData.AxisNo);
                        moveModeZ = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
            if (axis == TableAxisName.U || axis == TableAxisName.ALL)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.StopJog(tableData.axisUData.AxisNo);
                        moveModeU = MoveMode.STOP;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 停止所有轴运动
        /// </summary>
        public void SuspendMove()
        {
            //actionX.StopInterpolation();
            if (bReadyX)
            {
                try
                {
                    actionX.StopMove(tableData.axisXData.AxisNo);
                }
                catch
                {
                }
            }

            if (bReadyY)
            {
                try
                {
                    actionY.StopMove(tableData.axisYData.AxisNo);
                }
                catch
                {
                }
            }

            if (bReadyZ)
            {
                try
                {
                    actionZ.StopMove(tableData.axisZData.AxisNo);
                }
                catch
                {
                }
            }

            if (bReadyU)
            {
                try
                {
                    actionU.StopMove(tableData.axisUData.AxisNo);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 设置指定轴当前位置为脉冲0位
        /// </summary>
        /// <param name="Axis">轴名称</param>
        public void SetPosZero(TableAxisName Axis)
        {
            if (Axis == TableAxisName.X)
            {
                if (bReadyX)
                {
                    try
                    {
                        actionX.ZeroAxisPos(tableData.axisXData.AxisNo);
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Y)
            {
                if (bReadyY)
                {
                    try
                    {
                        actionY.ZeroAxisPos(tableData.axisYData.AxisNo);
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.Z)
            {
                if (bReadyZ)
                {
                    try
                    {
                        actionZ.ZeroAxisPos(tableData.axisZData.AxisNo);
                    }
                    catch
                    {
                    }
                }
            }
            if (Axis == TableAxisName.U)
            {
                if (bReadyU)
                {
                    try
                    {
                        actionU.ZeroAxisPos(tableData.axisUData.AxisNo);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void ThreadHomeUFunction()
        {
            StartLimit(TableAxisName.U);
            while (LimitDone(TableAxisName.U) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(200);
            StartHome(TableAxisName.U);
            while (HomeDone(TableAxisName.U) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(300);
            SetPosZero(TableAxisName.U);
            bHomingU = false;

            //StartLimit(TableAxisName.U);
            //while (LimitDone(TableAxisName.U) == false)
            //{
            //    System.Threading.Thread.Sleep(10);
            //}
            //StartHome(TableAxisName.U);
            //while (HomeDone(TableAxisName.U) == false)
            //{
            //    System.Threading.Thread.Sleep(10);
            //}
            //System.Threading.Thread.Sleep(300);
            //SetPosZero(TableAxisName.U);
            //bHomingU = false;
        }

        private void ThreadHomeXFunction()
        {
            bool s=  GetCCW(TableAxisName.X);//负限位
            bool sw = GetCW(TableAxisName.X);//正限位
            if(sw==true)
            {
                JogMove(TableAxisName.X,false,true);
                Thread.Sleep(1000);
                JogStop(TableAxisName.X);
                Thread.Sleep(1000);
            }
            //StartLimit(TableAxisName.X);
            //while (LimitDone(TableAxisName.X) == false)
            //{
            //    System.Threading.Thread.Sleep(10);
            //}
            //System.Threading.Thread.Sleep(200);
            //SetPosZero(TableAxisName.X);
            //System.Threading.Thread.Sleep(200);
            //StartHome(TableAxisName.X);
            //while (HomeDone(TableAxisName.X) == false)
            //{
            //    System.Threading.Thread.Sleep(10);
            //}
            //System.Threading.Thread.Sleep(300);
            //SetPosZero(TableAxisName.X);
            //bHomingX = false;

            StartLimit(TableAxisName.X);
            while (LimitDone(TableAxisName.X) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(200);
            StartHome(TableAxisName.X);
            while (HomeDone(TableAxisName.X) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(300);
            SetPosZero(TableAxisName.X);
            bHomingX = false;
        }

        private void ThreadHomeYFunction()
        {
            bool sw = GetCW(TableAxisName.Y);//正限位
            if (sw == true)
            {
                JogMove(TableAxisName.Y, false, true);
                Thread.Sleep(1000);
                JogStop(TableAxisName.Y);
                Thread.Sleep(1000);
            }
            StartLimit(TableAxisName.Y);
            while (LimitDone(TableAxisName.Y) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(200);
            StartHome(TableAxisName.Y);
            while (HomeDone(TableAxisName.Y) == false)
            {
                System.Threading.Thread.Sleep(10);
            }
            System.Threading.Thread.Sleep(300);
            SetPosZero(TableAxisName.Y);
            bHomingY = false;
        }

        private void ThreadHomeZFunction()
        {
            bool sw = GetCW(TableAxisName.Z);//正限位
            if (sw == true)
            {
                JogMove(TableAxisName.Z, false, true);
                Thread.Sleep(1000);
                JogStop(TableAxisName.Z);
                Thread.Sleep(1000);
            }
            StartLimit(TableAxisName.Z);
            while (LimitDone(TableAxisName.Z) == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            System.Threading.Thread.Sleep(200);
            StartHome(TableAxisName.Z);
            while (HomeDone(TableAxisName.Z) == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            System.Threading.Thread.Sleep(300);
            SetPosZero(TableAxisName.Z);
            bHomingZ = false;
        }

        /// <summary>
        /// 位置比较输出
        /// </summary>
        /// <param name="chn">通道 端口HSIO（5V）：0 端口GPO（通用输出端口 12V）：1</param>
        /// <param name="outputType">输出模式 0：脉冲 1：电平</param>
        /// <param name="maxerr">比较范围最大误差</param>
        /// <param name="output_1">指定通道0输出</param>
        /// <param name="output_2">指定通道1输出</param>
        /// <param name="posX">比较位置X</param>
        /// <param name="posY">比较位置Y</param>
        public void PositionCompareOut(short chn, short outputType, short maxerr, short output_1, short output_2, double posX, double posY)
        {
            actionX.PositionCompareOut((short)tableData.axisXData.iCorNo,
                chn,
                outputType,
                maxerr,
                output_1,
                output_2,
                posX / tableData.axisXData.plusToMM,
                posY / tableData.axisXData.plusToMM);
        }
        /// <summary>
        /// 复位轴报警
        /// </summary>
        public void ResetAlarm()
        {
            actionX.ResetAxisAlarm();
        }
        /// <summary>
        /// 低速点位运动
        /// </summary>
        /// <param name="strPosName">目标位置名称</param>
        /// <param name="bCanXYMove">XY轴是否允许运动</param>
        public void StartPosMoveLowSpeed(string strPosName, bool bCanXYMove)
        {
            //bool bCanXYMove=false;
            //if (!bCanXYMove)
            //{
            //    MessageBox.Show("请将Z轴移动到安全位置！");
            //    return;
            //}
            //if (tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ > tableData.tablePosData.tablePosItemDictionary["Z轴安全位"].dPosZ)
            //{
            //    bCanXYMove = false;
            //    MessageBox.Show("请将Z轴移动到安全位置！");
            //    return;
            //}
            //else
            //{
            //    bCanXYMove = true;
            //}
            if (bReadyX && bCanXYMove)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionX)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionX.AbsPosMove(tableData.axisXData.
                                                AxisNo, tableData.axisXData.dAcc,
                                                tableData.axisXData.dDec,
                                                tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX / tableData.axisXData.plusToMM);
                            moveModeX = MoveMode.ABS;
                            dPosX = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX;
                            dSpeedX = tableData.axisXData.dSpeed;
                            dAccX = tableData.axisXData.dAcc;
                            dDecX = tableData.axisXData.dDec;
                        }
                        else
                        {
                            actionX.ReferPosMove(tableData.axisXData.
                                                AxisNo, tableData.axisXData.dAcc,
                                                tableData.axisXData.dDec,
                                                tableData.axisXData.dSpeed / tableData.axisXData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX / tableData.axisXData.plusToMM);
                            moveModeX = MoveMode.REL;
                            dPosX = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosX;
                            dSpeedX = tableData.axisXData.dSpeed;
                            dAccX = tableData.axisXData.dAcc;
                            dDecX = tableData.axisXData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyY && bCanXYMove)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionY)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionY.AbsPosMove(tableData.axisYData.
                                                AxisNo, tableData.axisYData.dAcc,
                                                tableData.axisYData.dDec,
                                                tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY / tableData.axisYData.plusToMM);
                            moveModeY = MoveMode.ABS;
                            dPosY = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY;
                            dSpeedY = tableData.axisYData.dSpeed;
                            dAccY = tableData.axisYData.dAcc;
                            dDecY = tableData.axisYData.dDec;
                        }
                        else
                        {
                            actionY.ReferPosMove(tableData.axisYData.
                                                AxisNo, tableData.axisYData.dAcc,
                                                tableData.axisYData.dDec,
                                                tableData.axisYData.dSpeed / tableData.axisYData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY / tableData.axisYData.plusToMM);
                            moveModeY = MoveMode.REL;
                            dPosY = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosY;
                            dSpeedY = tableData.axisYData.dSpeed;
                            dAccY = tableData.axisYData.dAcc;
                            dDecY = tableData.axisYData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyZ)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionZ)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionZ.AbsPosMove(tableData.axisZData.
                                                AxisNo, tableData.axisZData.dAcc,
                                                tableData.axisZData.dDec,
                                                tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ / tableData.axisZData.plusToMM);
                            moveModeZ = MoveMode.ABS;
                            dPosZ = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ;
                            dSpeedZ = tableData.axisZData.dSpeed;
                            dAccZ = tableData.axisZData.dAcc;
                            dDecZ = tableData.axisZData.dDec;
                        }
                        else
                        {
                            actionZ.ReferPosMove(tableData.axisZData.
                                                AxisNo, tableData.axisZData.dAcc,
                                                tableData.axisZData.dDec,
                                                tableData.axisZData.dSpeed / tableData.axisZData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ / tableData.axisZData.plusToMM);
                            moveModeZ = MoveMode.REL;
                            dPosZ = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosZ;
                            dSpeedZ = tableData.axisZData.dSpeed;
                            dAccZ = tableData.axisZData.dAcc;
                            dDecZ = tableData.axisZData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
            if (bReadyU)
            {
                try
                {
                    if (tableData.tablePosData.tablePosItemDictionary[strPosName].bActionU)
                    {
                        if (tableData.tablePosData.tablePosItemDictionary[strPosName].bRel == false)
                        {
                            actionU.AbsPosMove(tableData.axisUData.
                                                AxisNo, tableData.axisUData.dAcc,
                                                tableData.axisUData.dDec,
                                                tableData.axisUData.dJobLow / tableData.axisUData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU / tableData.axisUData.plusToMM);
                            moveModeU = MoveMode.ABS;
                            dPosU = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU;
                            dSpeedU = tableData.axisUData.dJobLow;
                            dAccU = tableData.axisUData.dAcc;
                            dDecU = tableData.axisUData.dDec;
                        }
                        else
                        {
                            actionU.ReferPosMove(tableData.axisUData.
                                                AxisNo, tableData.axisUData.dAcc,
                                                tableData.axisUData.dDec,
                                                tableData.axisUData.dJobLow / tableData.axisUData.plusToMM,
                                                tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU / tableData.axisUData.plusToMM);
                            moveModeU = MoveMode.REL;
                            dPosU = tableData.tablePosData.tablePosItemDictionary[strPosName].dPosU;
                            dSpeedU = tableData.axisUData.dJobLow;
                            dAccU = tableData.axisUData.dAcc;
                            dDecU = tableData.axisUData.dDec;
                        }
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 位置比较
        /// </summary>
        /// <param name="axis">轴名称</param>
        /// <param name="pos">目标位置</param>
        /// <param name="different">与目标位置的差值</param>
        /// <returns></returns>
        public bool CompareOut(TableAxisName axis,double pos,out double different)
        {
            different = 0;
            switch (axis)
            {
                case TableAxisName.X:
                    return actionX.ComparePositionOut(0, pos,out different);
                case TableAxisName.Y:
                    return actionX.ComparePositionOut(1, pos, out different);
                case TableAxisName.Z:
                    return actionX.ComparePositionOut(2, pos,out different);
                case TableAxisName.U:
                    return actionX.ComparePositionOut(3, pos, out different);
                case TableAxisName.ALL:
                    return false;
                default:
                    return false;
            }
           
        }
        /// <summary>
        /// 开始预焊
        /// </summary>
        public void StartPreWelding()
        {
            List<double> preWeldingPos = tableData.tablePosData.tablePosItemList.Where(p=>p.strName.Contains("预焊位")).Select(p=>p.dPosX).ToList();
            if (preWeldingPos.Count > 0)
                actionX.PreWelding(preWeldingPos.Count, preWeldingPos);
            else
                return;
            
        }
        /// <summary>
        /// 预焊结束
        /// </summary>
        /// <returns></returns>
        public bool PreWeldingDone()
        {
            return actionX.PreWeldingDone();
        }

        public bool GetCCW(TableAxisName axis)
        {
            switch (axis)
            {
                case TableAxisName.X:
                    return actionX.GetLimtCCW(tableData.axisXData.AxisNo);
                case TableAxisName.Y:
                    return actionY.GetLimtCCW(tableData.axisYData.AxisNo);
                case TableAxisName.Z:
                    return actionZ.GetLimtCCW(tableData.axisZData.AxisNo);
                case TableAxisName.U:
                    return actionU.GetLimtCCW(tableData.axisUData.AxisNo);
                default:
                    return false;
            }
        }

        public bool GetCW(TableAxisName axis)
        {
            switch (axis)
            {
                case TableAxisName.X:
                    return actionX.GetLimtCW(tableData.axisXData.AxisNo);
                case TableAxisName.Y:
                    return actionY.GetLimtCW(tableData.axisYData.AxisNo);
                case TableAxisName.Z:
                    return actionZ.GetLimtCW(tableData.axisZData.AxisNo);
                case TableAxisName.U:
                    return actionU.GetLimtCW(tableData.axisUData.AxisNo);
                default:
                    return false;
            }
        }
    }
}