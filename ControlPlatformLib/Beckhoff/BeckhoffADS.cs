using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwinCAT.Ads;
using System.IO;
using System.Threading;
using TwinCAT;

namespace ControlPlatformLib.Beckhoff
{
    //X轴：0 Y轴：1 Z轴：2
    public class BeckhoffADS : HardWareBase, IMotionAction, IInputAction, IOutputAction
    {
        private TcAdsClient tcClient;
        private string tcNetID;
        //private TcAdsPlcServer adsPlcServer;
        //
        private int _notificationHandle = 0;
        private int[] hConnect;
        private AdsStream dataStream;
        private BinaryReader binRead;
        private bool manual;
        StateInfo clientStateInfo;


        int allHome, xHome, yHome, zHome, axisNumber, stop, startContInterpolation, resetAlarm, resetInterpolation
;
        int hInt, bInterpolationFinishedInt, ioInt, preWeldingInt, preWeldingSatrtInt, preWeldingNumberInt, preWeldingPosInt,bPreWeldingDoneInt,bFullWeldingStart,fullWeldingPosX,fullWeldingPosY;
        int[] jogVel, acc, dec, vel, target, jogForward, jogBackward, abs, rel, excute, isMovingInt, isMoveDoneInt, posInt, alarmInt, cwInt, ccwInt, homeInt, homeStart, homeDoneInt, setPosStartInt, realTargetInt, absTargetInt;
        double[] pos;
        bool[] isMoving, isMoveDone, isAlarm, cw, ccw, home, homeDone;
        int[,] inputIntNot, outputIntNot, outputInt;
        bool[,] input, output;
        bool bInterpolationFinished;
        private double[] dCommandTargetPos = new double[8];
        private object objLock = new object();
        private object ioLock = new object();
        private MoveMode moveMode;
        private int stopInterpolation;
        private BeckhoffIO io;
        private int tech;
        private bool bTech;
        //private int bInterpolationFinishedInt;

        public override bool Init(HardWareInfoBase infoHardWare)
        {
            try
            {
                //adsPlcServer = new TcAdsPlcServer();
                clientStateInfo = new StateInfo();
                dataStream = new AdsStream(31);
                //Encoding is set to ASCII, to read strings
                binRead = new BinaryReader(dataStream, System.Text.Encoding.ASCII);

                tcNetID = infoHardWare.ipAddress;
                tcClient = new TcAdsClient();
                // tcClient.Connect(tcNetID, 801);
                tcClient.Connect(tcNetID, 801);
                Global.bPLCConnected = true;
                bInitOK = true;
                allHome = tcClient.CreateVariableHandle("MAIN_Logic.all_Home");
                hInt = tcClient.CreateVariableHandle("MAIN_Logic.home_Step");
                startContInterpolation = tcClient.CreateVariableHandle("MAIN_Fast_2ms.bExec");
                resetAlarm = tcClient.CreateVariableHandle(".DATA.ResetAlarm");
                resetInterpolation = tcClient.CreateVariableHandle("MAIN_Fast_2ms.nci_Sequence.reset");

                stopInterpolation = tcClient.CreateVariableHandle("MAIN_Fast_2ms.b_NCIStop");
                preWeldingInt = tcClient.CreateVariableHandle("MAIN_Fast_2ms.preWelding");
                preWeldingSatrtInt = tcClient.CreateVariableHandle("MAIN_Logic.preWeldingStart");
                preWeldingNumberInt = tcClient.CreateVariableHandle("MAIN_Logic.preWeldingNumber");
                bPreWeldingDoneInt = tcClient.CreateVariableHandle("MAIN_Logic.fbPreWeldingDone");
                preWeldingPosInt = tcClient.CreateVariableHandle("MAIN_Logic.preWeldingPos");
                tech = tcClient.CreateVariableHandle("MAIN_Fast_2ms.posTech.Pos");
                //MAIN_Fast_2ms.nci_Sequence.overide
                InitParameter();
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("连接倍福PLC失败,地址：{0}，错误信息：{1}", tcNetID, ex.Message);
                Global.bPLCConnected = false;
                bInitOK = false;
                return false;
            }
        }

        

        public override bool Close()
        {
            try
            {
                if (bInitOK)
                {
                    clientStateInfo.AdsState = AdsState.Stop;
                    tcClient.WriteControl(clientStateInfo);
                }
                for (int i = 0; i < 3; i++)
                {
                    tcClient.DeleteDeviceNotification(posInt[i]);
                    tcClient.DeleteDeviceNotification(isMovingInt[i]);
                    tcClient.DeleteDeviceNotification(alarmInt[i]);
                    tcClient.DeleteDeviceNotification(homeDoneInt[i]);
                    //tcClient.DeleteDeviceNotification(cwInt[i]);
                    //tcClient.DeleteDeviceNotification(ccwInt[i]);
                    //tcClient.DeleteDeviceNotification(homeInt[i]);
                    tcClient.DeleteDeviceNotification(isMoveDoneInt[i]);
                }
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 16; j++)
                    {
                        tcClient.DeleteDeviceNotification(inputIntNot[i, j]);
                        tcClient.DeleteDeviceNotification(outputIntNot[i, j]);
                    }
                }
                Global.bPLCConnected = false;
                bInitOK = false;
                if (tcClient != null)
                {
                    tcClient.Disconnect();
                    tcClient.Dispose();
                }
            }
            catch (Exception ex)
            {
                Global.bPLCConnected = false;
                bInitOK = false;
            }
            return true;
        }

        private void InitParameter()
        {
            try
            {
                hConnect = new int[20];
                axisNumber = 3;

                pos = new double[axisNumber];//当前位置
                posInt = new int[axisNumber];//当前位置指针
                acc = new int[axisNumber];//加速度
                dec = new int[axisNumber];//减速度
                vel = new int[axisNumber];//速度
                target = new int[axisNumber];//目标位置
                jogForward = new int[axisNumber];//正转指针
                jogBackward = new int[axisNumber];//反转指针
                abs = new int[axisNumber];//绝对运动指针
                rel = new int[axisNumber];//相对运动指针
                excute = new int[axisNumber];//执行运动
                homeStart = new int[axisNumber];//回原开始
                setPosStartInt = new int[axisNumber];//设置当前位置指针

                isMovingInt = new int[axisNumber];//运动状态指针
                alarmInt = new int[axisNumber];//报警指针
                isMoveDoneInt = new int[axisNumber];//运动完成指针
                isMoving = new bool[axisNumber];//运动中状态
                isMoveDone = new bool[axisNumber];//运动完成状态
                isAlarm = new bool[axisNumber];//报警状态
                cwInt = new int[axisNumber];//正限位指针
                ccwInt = new int[axisNumber];//负限位指针
                homeInt = new int[axisNumber];//原点指针
                homeDoneInt = new int[axisNumber];//回原完成指针
                cw = new bool[axisNumber];//正限位
                ccw = new bool[axisNumber];//负限位
                home = new bool[axisNumber];//原位
                homeDone = new bool[axisNumber];//回原完成标志
                realTargetInt = new int[axisNumber];
                absTargetInt = new int[axisNumber];
                jogVel=new int[axisNumber];
                //.axis[0].ref.Status.InTargetPosition
                //.DATA.AutoStoping
                stop = tcClient.CreateVariableHandle(".DATA.AutoStoping");
                homeDoneInt[0] = tcClient.AddDeviceNotificationEx("MAIN_Logic.fbHomeX.Done", AdsTransMode.OnChange, 100, 0, homeDone[0], typeof(bool));
                homeDoneInt[1] = tcClient.AddDeviceNotificationEx("MAIN_Logic.fbHomeY.Done", AdsTransMode.OnChange, 100, 0, homeDone[1], typeof(bool));
                homeDoneInt[2] = tcClient.AddDeviceNotificationEx(".axis[2].ref.Status.Homed", AdsTransMode.OnChange, 100, 0, homeDone[2], typeof(bool));
                for (int i = 0; i < axisNumber; i++)
                {
                    acc[i] = tcClient.CreateVariableHandle(".axis[" + i + "].Motion.Acc");
                    dec[i] = tcClient.CreateVariableHandle(".axis[" + i + "].Motion.Dec");
                    vel[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.Velo");
                    target[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.Target");
                    rel[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.MoveRelFromRemote");
                    abs[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.MoveAbs");
                    jogForward[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.JogPosFromRemote");
                    jogBackward[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.JogNegFromRemote");
                    jogVel[i] = tcClient.CreateVariableHandle(".axis[" + i + "].AxHMI.VeloFromRemote");

                    homeStart[i] = tcClient.CreateVariableHandle(".axisHome["+i+"]");

                    posInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].ref.NcToPlc.ActPos", AdsTransMode.Cyclic, 100, 0, pos[i], typeof(double));
                    //.axis[0].ref.Status.Moving
                    isMovingInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].ref.Status.Moving", AdsTransMode.OnChange, 100, 0, isMoving[i], typeof(bool));
                    alarmInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].ref.Status.Error", AdsTransMode.OnChange, 100, 0, isAlarm[i], typeof(bool));

                   
                    cwInt[i] = tcClient.CreateVariableHandle(".axis[" + i + "].IO.LimitPos.INPUT");
                    cw[i] = (bool)tcClient.ReadAny(cwInt[i], typeof(bool));
                    cwInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].IO.LimitPos.INPUT", AdsTransMode.OnChange, 100, 0, cw[i], typeof(bool));
                    ccwInt[i] = tcClient.CreateVariableHandle(".axis[" + i + "].IO.LimitNeg.INPUT");
                    ccw[i] = (bool)tcClient.ReadAny(ccwInt[i], typeof(bool));
                    ccwInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].IO.LimitNeg.INPUT", AdsTransMode.OnChange, 100, 0, ccw[i], typeof(bool));
                    homeInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].IO.HomePos.INPUT", AdsTransMode.OnChange, 100, 0, home[i], typeof(bool));
                    //home[i] = (bool)tcClient.ReadAny(homeInt[i], typeof(bool));
                    isMoveDoneInt[i] = tcClient.AddDeviceNotificationEx(".axis[" + i + "].Motion.Done", AdsTransMode.OnChange, 100, 0, isMoveDone[i], typeof(bool));
                }
                bInterpolationFinishedInt = tcClient.AddDeviceNotificationEx("MAIN_Fast_2ms.nci_Sequence.bFinished", AdsTransMode.OnChange, 100, 0, bInterpolationFinished, typeof(bool));
                inputIntNot = new int[7, 17];
                outputIntNot = new int[7, 17];
                input = new bool[7, 17];
                output = new bool[7, 17];
                outputInt = new int[7, 17];
                //.EL1889[1,1] .EL1889[1,1] .EL2889[1,1] EL1889	[1	,1]
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 16; j++)
                    {
                        //Console.WriteLine(i+":"+j);
                        outputInt[i, j] = tcClient.CreateVariableHandle(".EL2889[" + i + "," + j + "]");
                        output[i, j] = (bool)tcClient.ReadAny(outputInt[i, j],typeof(bool));
                        outputIntNot[i, j] = tcClient.AddDeviceNotificationEx(".EL2889[" + i + "," + j + "]", AdsTransMode.OnChange, 100, 0, output[i, j], typeof(bool));
                        inputIntNot[i, j] = tcClient.CreateVariableHandle(".EL1889[" + i + "," + j + "]");
                        input[i, j] = (bool)tcClient.ReadAny(inputIntNot[i, j], typeof(bool));
                        //tcClient.DeleteDeviceNotification(inputIntNot[i, j]);
                        inputIntNot[i, j] = tcClient.AddDeviceNotificationEx(".EL1889[" + i + "," + j + "]", AdsTransMode.OnChange, 100, 0, input[i, j], typeof(bool));
                        
                    }
                }

                ioInt = tcClient.CreateVariableHandle("MAIN_Logic.io");
                
                tcClient.AdsNotificationEx += new AdsNotificationExEventHandler(ADSClient_AdsNotificationEx);
                //tcClient.AdsStateChanged += TcClient_AdsStateChanged;
                tcClient.ConnectionStateChanged += TcClient_ConnectionStateChanged;
                clientStateInfo.AdsState = AdsState.Reset;
                tcClient.WriteControl(clientStateInfo);
                clientStateInfo.AdsState = AdsState.Run;
                tcClient.WriteControl(clientStateInfo);

                tcClient.WriteAny(stopInterpolation, false);

                //Thread thread = new Thread(ScanIO);
                //thread.IsBackground = true;
                //thread.Start();
            }
            catch (Exception ex)
            {
                bInitOK = false;
                Global.logger.ErrorFormat("初始化倍福ADS通讯错误：{0}", ex.Message);
            }
        }

        private void TcClient_ConnectionStateChanged( object sender,ConnectionStateChangedEventArgs e)
        {
            Global.logger.InfoFormat("连接状态改变：{0}",e.NewState);
        }

        private void ScanIO()
        {
            while (true)
            {
                Thread.Sleep(1);
                io = (BeckhoffIO)tcClient.ReadAny(ioInt, typeof(BeckhoffIO));
            }
        }




        private void ADSClient_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {
            if (!bInitOK)
                return;
            try
            {
                for (int i = 0; i < axisNumber; i++)
                {
                    if (e.NotificationHandle == posInt[i])//轴当前位置
                    {
                        pos[i] = (double)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == isMovingInt[i])//轴运动状态
                    {
                        isMoving[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == alarmInt[i])//轴报警状态
                    {
                        isAlarm[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == cwInt[i])//轴正限位
                    {
                        cw[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == ccwInt[i])//轴负限位
                    {
                        ccw[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == homeInt[i])//轴原位
                    {
                        home[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == homeDoneInt[i])//轴回原完成标志
                    {
                        homeDone[i] = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == bInterpolationFinishedInt)//轴回原完成标志
                    {
                        bInterpolationFinished = (bool)e.Value;
                        return;
                    }
                    else if (e.NotificationHandle == isMoveDoneInt[i])
                    {
                        isMoveDone[i] = (bool)e.Value;
                        return;
                    }
                }
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 16; j++)
                    {
                        if (e.NotificationHandle == inputIntNot[i, j])
                        {
                            input[i, j] = (bool)e.Value;
                            return;
                        }
                        if (e.NotificationHandle == outputIntNot[i, j])
                        {
                            output[i, j] = (bool)e.Value;
                            return;
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("ADS通讯错误：{0}。", ex.Message);
            }
        }

        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    moveMode = MoveMode.ABS;
                    dCommandTargetPos[axis] = pos;
                    tcClient.WriteAny(abs[axis], false);
                    tcClient.WriteAny(target[axis], pos);

                    tcClient.WriteAny(acc[axis], dAcc);
                    tcClient.WriteAny(dec[axis], dDec);
                    tcClient.WriteAny(vel[axis], Math.Abs(dSpeed));
                    Thread.Sleep(10);
                    tcClient.WriteAny(abs[axis], true);
                    if ((bool)tcClient.ReadAny(abs[axis], typeof(bool)))
                    {
                        Thread.Sleep(10);
                        tcClient.WriteAny(abs[axis], false);
                    }
                    else
                    {
                        tcClient.WriteAny(abs[axis], true);
                    }
                    Thread.Sleep(10);
                    tcClient.WriteAny(abs[axis], false);
                }
                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} 执行绝对运动指令失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }
        }

        public bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType interpolationAxisType)
        {
            return true;
        }

        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            return true;
        }

        public bool  BuildCor(short num, CoordinateType interpolationAxisType)
        {
            return true;
        }

        public bool  BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            return true;
        }

        public bool CureMoveDone(short num, out int iStep)
        {
            iStep = 1;
            if (bInterpolationFinished)
            {
                tcClient.WriteAny(preWeldingInt, true);
            }
            return bInterpolationFinished;
        }

        public bool FinishSearchHome(short axis)
        {
            try
            {
                if (!bInitOK)
                    return false;
                if (homeDone[axis])
                {
                    lock (objLock)
                    {
                        tcClient.WriteAny(homeStart[axis], false);
                    }
                }
                return homeDone[axis];
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("获取回原点完成信号失败，错误信息：{0}。",ex.Message);
                return false;
            }
        }

        public bool FinishSearchLimit(short axis)
        {
            return true;
        }

        public bool GetAlarm(short axis)
        {
            return isAlarm[axis];
        }

        public double GetCurrentPos(short axis)
        {
            return pos[axis];
        }

        public bool GetEstop(short axis)
        {
            return false;
        }

        public bool GetHome(short axis)
        {
            return home[axis];
        }

        public bool GetLimtCCW(short axis)
        {
            return !ccw[axis];
        }

        public bool GetLimtCW(short axis)
        {
            return !cw[axis];
        }

        public bool GetServoOn(short axis)
        {
            return true;
        }

        public double GetVel(short axis)
        {
            return 0.0;
        }

        public bool HandleErrorMessage(short errorMessage)
        {
            return false;
        }

        public bool InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {
            return true;
        }

        public bool  InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {
            return true;
        }

        public bool IsMoveDone(short axis)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    bool bResult;
                    double lCurrentPos;
                    if (isMoving[axis])
                    {
                        bResult = false;
                    }
                    else
                    {
                        lCurrentPos = pos[axis];

                        double dDiffValue = Math.Abs(dCommandTargetPos[axis] - lCurrentPos);//
                        if (dDiffValue < 0.001)
                        {
                            //abs[axis] = tcClient.CreateVariableHandle(".axis[" + axis + "].AxHMI.MoveAbs");
                            //target[axis] = tcClient.CreateVariableHandle(".axis[" + axis + "].AxHMI.Target");
                            tcClient.WriteAny(abs[axis], false);
                            tcClient.WriteAny(rel[axis], false);
                            //tcClient.DeleteVariableHandle(abs[axis]);
                            //tcClient.DeleteVariableHandle(target[axis]);
                            bResult = true;
                        }
                        else
                        {
                            //if (moveMode == MoveMode.ABS)
                            //{
                            //    tcClient.WriteAny(target[axis], dCommandTargetPos[axis]);
                            //    tcClient.WriteAny(abs[axis], false);
                            //    tcClient.WriteAny(abs[axis], true);
                            //    tcClient.WriteAny(abs[axis], false);
                            //}
                            bResult = false;
                        }
                    }
                    return bResult;
                }
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("获取运动完成信号失败，错误信息：{0}。",ex.Message);
                return false;
            }
        }

        public bool IsMoving(short axis)
        {
            return isMoving[axis];
        }

        public bool JogMove(short axis, double dAcc, double dDec, double dVel)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    moveMode = MoveMode.JOG;
                    tcClient.WriteAny(acc[axis], dAcc);
                    tcClient.WriteAny(acc[axis], dDec);
                    tcClient.WriteAny(jogVel[axis], Math.Abs(dVel));
                    if (dVel >= 0)
                        tcClient.WriteAny(jogForward[axis], true);
                    else
                        tcClient.WriteAny(jogBackward[axis], true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} 执行Jog运动指令失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }
        }

        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            return true;
        }

        public void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py)
        {
            return;
        }

        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    moveMode = MoveMode.REL;
                    tcClient.WriteAny(abs[axis], false);
                    tcClient.WriteAny(rel[axis], false);
                    tcClient.WriteAny(acc[axis], dAcc);
                    tcClient.WriteAny(dec[axis], dDec);
                    tcClient.WriteAny(vel[axis], dSpeed);
                    tcClient.WriteAny(target[axis], pos);
                    dCommandTargetPos[axis] = pos + this.pos[axis];
                    tcClient.WriteAny(rel[axis], true);
                    tcClient.WriteAny(rel[axis], false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} 执行相对运动指令失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }
        }

        public bool ServoOff(short axis)
        {
            return true;
        }

        public bool ServoOn(short axis)
        {
            return true;
        }

        public bool SetAlarmOff(short axis)
        {
            return true;
        }

        public bool SetAlarmOn(short axis)
        {
            return true;
        }

        public bool SetAxisPos(short axis, double dPos)
        {
            return true;
        }

        public bool SetHomeOff(short axis)
        {
            return true;
        }

        public bool SetHomeOn(short axis)
        {
            return true;
        }

        public bool SetLimtDisable(short axis)
        {
            return true;
        }

        public bool SetLimtOff(short axis)
        {
            return true;
        }

        public bool SetLimtOn(short axis)
        {
            return true;
        }

        public bool SetNearHomeOff(short axis)
        {
            return true;
        }

        public bool SetNearHomeOn(short axis)
        {
            return true;
        }

        public bool SetPulseMode(short axis, PulseMode psm)
        {
            return true;
        }

        public bool SetVel(short axis, double dVel)
        {
            return true;
        }

        public bool StartCure(short num,bool bPreWelding)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    if (bPreWelding)
                        tcClient.WriteAny(preWeldingInt, true);
                    else
                        tcClient.WriteAny(preWeldingInt, false);
                    
                    tcClient.WriteAny(stopInterpolation, false);
                    tcClient.WriteAny(startContInterpolation, true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("执行插补指令失败，错误信息：{0}。", ex.Message);
                return false;
            }
        }

        public bool StartPreWeldingCure(short num)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    tcClient.WriteAny(stopInterpolation, false);
                    tcClient.WriteAny(preWeldingInt, true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("执行插补指令失败，错误信息：{0}。", ex.Message);
                return false;
            }
        }

        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    tcClient.WriteAny(acc[axis], dAcc);
                    tcClient.WriteAny(dec[axis], dDec);
                    tcClient.WriteAny(vel[axis], Math.Abs(dHomeSpd));
                    tcClient.WriteAny(homeStart[axis], true);
                    tcClient.WriteAny(homeStart[axis], false);
                }

                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} 回原指令失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }
        }

        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            throw new NotImplementedException();
        }

        public bool StopJog(short axis)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    tcClient.WriteAny(jogForward[axis], false);
                    tcClient.WriteAny(jogBackward[axis], false);
                }
                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} Jog停止失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }
            //tcClient.WriteAny(jogForward[axis], false);
            //tcClient.WriteAny(jogBackward[axis], false);
            //return true;
        }

        public bool StopMove(short axis)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    tcClient.WriteAny(stop, true);
                    tcClient.WriteAny(stop, false);
                }
                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("轴：{0} 停止失败，错误信息：{1}。", axis, ex.Message);
                return false;
            }

        }

        public bool ZeroAxisPos(short axis)
        {
            return true;
        }

        public bool GetInputBit(int iBit)
        {
            if (!bInitOK)
                return false;
            try
            {
                if (input == null)
                {
                    return false;
                }
                int point, index;
                if (iBit % 16 == 0)
                {
                    point = 16;
                    index = iBit / 16 - 1;
                }
                else
                {
                    point = iBit % 16;
                    index = iBit / 16;
                }
                return input[index + 1, point];
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("获取输入点位：{0} 失败，错误信息：{1}。", iBit, ex.Message);
                return false;
            }

        }

        public bool GetOutBit(int iBit)
        {
            if (!bInitOK)
                return false;
            try
            {
                if (output == null)
                {
                    return false;
                }
                int point, index;
                if (iBit % 16==0)
                {
                    point = 16;
                    index = iBit / 16-1;
                }
                else
                {
                    point = iBit % 16;
                    index = iBit / 16;
                }
                
                return output[index + 1, point];
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("获取输出点位：{0} 失败，错误信息：{1}。", iBit, ex.Message);
                return false;
            }
        }

        public bool SetOutBit(int iBit, bool bOn)
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    //int point = iBit % 16;
                    //int index = iBit / 16;
                    int point, index;
                    if (iBit % 16 == 0)
                    {
                        point = 16;
                        index = iBit / 16 - 1;
                    }
                    else
                    {
                        point = iBit % 16;
                        index = iBit / 16;
                    }
                    tcClient.WriteAny(outputInt[index + 1, point], bOn);
                }
                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("输出点位：{0} 失败，错误信息：{1}。", iBit, ex.Message);
                return false;
            }
        }

        public bool ResetAxisAlarm()
        {
            if (!bInitOK)
                return false;
            try
            {
                lock (objLock)
                {
                    for (int i = 0; i < axisNumber; i++)
                    {
                        tcClient.WriteAny(homeStart[i], false);
                        tcClient.WriteAny(abs[i], false);
                        tcClient.WriteAny(rel[i], false);
                    }

                    tcClient.WriteAny(resetAlarm, true);
                    tcClient.WriteAny(resetAlarm, false);

                    //clientStateInfo.AdsState = AdsState.Reset;
                    //tcClient.WriteControl(clientStateInfo);
                    
                    ////tcClient.WriteAny(resetInterpolation, true);
                    //clientStateInfo.AdsState = AdsState.Run;
                    //tcClient.WriteControl(clientStateInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("复位报警失败，错误信息：{0}。", ex.Message);
                return false;
            }
        }


        public bool ArcMoveWithThroughAndEnd(short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double endPosX, double endPosY, double throughPosX, double throughPosY)
        {
            return true;
        }

        public bool GantryMove(short gantryAxis1, short gantryAxis2, double dAcc, double dDec, double dSpeed, double endPos)
        {
            return true;
        }


        public bool StopInterpolation()
        {
            if (!bInitOK)
                return false;
            try
            {
                tcClient.WriteAny(stopInterpolation,true);

                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("暂停插补报警，错误信息：{0}。", ex.Message);
                return false;
            }
        }

        public bool ResumeInterpolation()
        {
            if (!bInitOK)
                return false;
            try
            {
                tcClient.WriteAny(stopInterpolation, false);

                return true;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("恢复插补报警，错误信息：{0}。", ex.Message);
                return false;
            }
        }

        public bool ComparePositionOut(int axis,double pos,out double different)
        {
            different = this.pos[axis] - pos;
            if (Math.Abs(different)<=3)
                return true;
            else
                return false;
        }


        public void PreWelding(int preWeldingNumber, List<double> preWeldingPos)
        {
            if (!bInitOK)
                return;
            try
            {
                double[] dPosArr = new double[16];
                for (int i = 0; i < preWeldingNumber; i++)
                {
                    dPosArr[i]=preWeldingPos[i];
                }

                tcClient.WriteAny(preWeldingNumberInt, preWeldingNumber-1);
                tcClient.WriteAny(preWeldingPosInt, dPosArr);
                tcClient.WriteAny(preWeldingSatrtInt, true);
                StartCure(0,true);
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("执行预焊失败，错误信息：{0}。", ex.Message);
            }
        }

        public void FullWelding(double dPos)
        {
            if (!bInitOK)
                return;
            try
            {

                //tcClient.WriteAny(preWeldingNumberInt, 0);
                //tcClient.WriteAny(preWeldingPosInt, dPosArr);
                //tcClient.WriteAny(preWeldingSatrtInt, true);
                //StartCure(0, true);
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("执行预焊失败，错误信息：{0}。", ex.Message);
            }
        }


        public bool PreWeldingDone()
        {
            if (!bInitOK)
                return false;
            try
            {
                bool result = (bool)tcClient.ReadAny(bPreWeldingDoneInt, typeof(bool));
                return result;
            }
            catch (Exception ex)
            {
                Close();
                Global.logger.ErrorFormat("获取预焊结果失败，错误信息：{0}。", ex.Message);
                return false;
            }
        }

        public bool StartManualPulserOperation(int axis_ID, int MultiplyingPower)
        {
            throw new NotImplementedException();
        }

        public bool StopManualPulserOperation(int axis_ID)
        {
            throw new NotImplementedException();
        }

        public double GT_GetAdc(short Cardnum, short adcout,out  double dbRet)
        {
            throw new NotImplementedException();
        }
    }
}
