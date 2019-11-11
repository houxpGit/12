using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMX3ApiCLR;
using WMX3ApiCLR.EcApiCLRLib;

namespace ControlPlatformLib.Softservo
{
    /********Softservo Controler Information/********
   *Vendor: Softservo 软赢
   *Type: 软PLC
   *Hardware Interface Type: PCI
   *Version: 1.0.1.0
   *Author：WuChenJie
   ***********************************************/
    public class SoftservoControler : HardWareBase, IMotionAction, IInputAction, IOutputAction
    {
        WMX3Api wmx;
        CoreMotion cmApi;
        ListMotion fx_ListMotion;
        Io wmx_IO;
        CoreMotionStatus status;
        ApiBuffer fx_ApiBuffer;
        EventControl fx_EventControl;
        EcApiLib ecapi;
        Config.HomeParam homeParam;

        #region 运动参数
        /// <summary>
        /// Jog运动参数
        /// </summary>
        Motion.JogCommand s_JogCommand;

        /// <summary>
        /// 位置运动参数
        /// </summary>
        Motion.PosCommand fx_PosBlock;

        /// <summary>
        /// 直线插补参数
        /// </summary>
        Motion.LinearIntplCommand fx_LinearIntplCommand;

        /// <summary>
        /// 圆弧插补参数
        /// </summary>
        Motion.CenterAndEndCircularIntplCommand fx_CenterAndEndCir;

        Motion.ThroughAndEndCircularIntplCommand fx_ThroughAndEnd;
        #endregion

        #region Status
        private bool[] bAlarm = new bool[8];
        private bool[] bAxisMoving = new bool[8];
        private bool[] bAxisServo = new bool[8];
        private bool[] bBitInputStatus = new bool[64];
        private bool[] bBitOutputStatus = new bool[64];
        private byte[] bBitInputStatusTemp = new byte[8];
        private byte[] bBitOutputStatusTemp = new byte[8];
        private bool[] bEstop = new bool[8];
        private bool[] bCCWL = new bool[8];
        private bool[] bCWL = new bool[8];
        private bool[] bHome = new bool[8];
        private bool[] bHomeDone = new bool[8];
        private bool[] bHomeing = new bool[8];
        private bool[] bHomeLast = new bool[8];
        private bool[] bHomeStop = new bool[8];
        private double[] dCommandTargetPos = new double[8];
        private double[] dCurrentPos = new double[8];
        private double[] dCurrentVel = new double[8];
        private double[] dTargetPos = new double[8];
        private int[,] iColAixsNo = new int[16, 3];
        private int[] iMoveMode = new int[8];
        #endregion

        public SoftservoControler()
        {
            wmx = new WMX3Api();
            status = new CoreMotionStatus();
            s_JogCommand = new Motion.JogCommand();
            fx_PosBlock = new Motion.PosCommand();
            fx_LinearIntplCommand = new Motion.LinearIntplCommand();
            fx_CenterAndEndCir = new Motion.CenterAndEndCircularIntplCommand();
            fx_ThroughAndEnd = new Motion.ThroughAndEndCircularIntplCommand();
        }
        public override bool Init(HardWareInfoBase infoHardWare)
        {
            
            int ret = wmx.CreateDevice(@"C:\Program Files\SoftServo\WMX3\", DeviceType.DeviceTypeNormal, 0);
            if (ret != 0)
            {
                string errorCode = "";
                errorCode = WMX3Api.ErrorToString(ret);
                Global.logger.ErrorFormat("WMX3初始化失败！错误信息：{0}", errorCode);
                //return false;
            }
            else
            {
                bInitOK = true;
            }
            //建立通讯
            Thread thread = new Thread(new ParameterizedThreadStart(EstablishCommunication));
            thread.IsBackground = true;
            thread.Start(infoHardWare); 
            return true;
        }

        public override bool Close()
        {
            cmApi.Motion.FreeSplineBuffer(5);

            for (int i = 0; i < 8; i++)
            {
                cmApi.AxisControl.SetServoOn(i, 0);
            }

            Thread.Sleep(500);

            //断通讯
            if (wmx != null)
            {
                wmx.StopCommunication();
            }

            Thread.Sleep(500);

            //关闭“设备”，去掉引擎
            if (wmx != null)
            {
                wmx.CloseDevice();
            }

            return true;
        }

        /// <summary>
        /// 建立通讯
        /// </summary>
        /// <param name="infoHardWare"></param>
        private void EstablishCommunication(object infoHardWare)
        {
            var softservoInfo = infoHardWare as SoftservoControlerInfo;
            int ret;
            Thread.Sleep(5000);

            cmApi = new CoreMotion(wmx);
            fx_EventControl = new EventControl(wmx);
            fx_ApiBuffer = new ApiBuffer(wmx);
            ecapi = new EcApiLib(wmx);

            fx_ListMotion = new ListMotion(wmx);
            
            wmx_IO = new Io(wmx);
            //设置系统参数
            ret = cmApi.Config.ImportAndSetAll(softservoInfo.m_strConfigPath);
            if (ret != 0)
            {
                string errorCode = "";
                errorCode = WMX3Api.ErrorToString(ret);
                Global.logger.ErrorFormat("WMX3参数获取失败！错误信息：{0}", errorCode);
                //return false;
            }
            //开始进行通讯
            int wd_count = 0;
            do
            {
                ret = wmx.StartCommunication();//通讯函数
                Thread.Sleep(500);
                wd_count++;
                cmApi.GetStatus(ref status);
            } while ((status.EngineState == EngineState.Communicating) && (wd_count < 10));//确认是否通讯成功

            if (status.EngineState != EngineState.Communicating)
            {
                // string ErrorCode = "";
                // wmx.ErrorToString(ret, ref ErrorCode, 128);
                string errorCode = WMX3Api.ErrorToString(ret); //获取错误代码
                Global.logger.ErrorFormat("WMX3通讯失败，错误信息：{0}！", errorCode);
                //return false;
            }

            //清除轴警信息
            for (int i = 0; i < 8; i++)
            {
                cmApi.AxisControl.ClearAmpAlarm(i);
                cmApi.AxisControl.ClearAxisAlarm(i);
            }

            //轴上使能
            for (int i = 0; i < 8; i++)
            {
                cmApi.AxisControl.SetServoOn(i, 1);
            }

            //检查所用到的轴有没上使能
            Thread.Sleep(1000);
            cmApi.GetStatus(ref status);
            for (int i = 0; i < 8; i++)
            {
                if (status.AxesStatus[i].ServoOn == false)
                {
                    //使用失败的处理。
                    Global.logger.ErrorFormat("WMX3 轴{0}使能失败！", i);
                    //return false;
                }
            }
            //timer1.Interval = 50;
            //timer1.Enabled = true;
            ret = cmApi.Motion.CreateSplineBuffer(5, 1024);
            if (ret != 0)
            {
                Global.logger.ErrorFormat("WMX3创建缓存出现错误！错误信息：{0}", WMX3Api.ErrorToString(ret));
            }

            while (bInitOK)
            {
                Thread.Sleep(10);
                GetAllMotionStatus();
                GetAllIOStatus();
            }
        }
        private void GetAllIOStatus()
        {
            lock (lockObj)
            {
                //[in]byte 头字节的字节地址。 
                //[in]size 得到的字节数。 
                //[out]pData 指向一个将接收数据的char数组的指针。数组的大小必须大于或等于对于大小参数。 
                wmx_IO.GetInBytesEx(0, 8, ref bBitInputStatusTemp);
                wmx_IO.GetOutBytesEx(0, 8, ref bBitOutputStatusTemp);
                for (int k = 0; k < 8; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                bBitInputStatus[i + k * 8] = (bBitInputStatusTemp[k] & 0x01) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = (bBitOutputStatusTemp[k] & 0x01) == 1 ? true : false;
                                break;
                            case 1:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x02) >> 1) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x02) >> 1) == 1 ? true : false;
                                break;
                            case 2:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x04) >> 2) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x04) >> 2) == 1 ? true : false;
                                break;
                            case 3:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x08) >> 3) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x08) >> 3) == 1 ? true : false;
                                break;
                            case 4:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x10) >> 4) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x10) >> 4) == 1 ? true : false;
                                break;
                            case 5:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x20) >> 5) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x20) >> 5) == 1 ? true : false;
                                break;
                            case 6:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x40) >> 6) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x40) >> 6) == 1 ? true : false;
                                break;
                            case 7:
                                bBitInputStatus[i + k * 8] = ((bBitInputStatusTemp[k] & 0x80) >> 7) == 1 ? true :false;
                                bBitOutputStatus[i + k * 8] = ((bBitOutputStatusTemp[k] & 0x80) >> 7) == 1 ? true : false;
                                break;
                            default: break;
                        }
                    }
                }
            }
        }

        private void GetAllMotionStatus()
        {
            lock (lockObj)
            {
                try
                {
                    cmApi.GetStatus(ref status);
                    for (ushort iAxis = 0; iAxis < 8; iAxis++)
                    {
                        dCurrentPos[iAxis] = status.AxesStatus[iAxis].ActualPos;
                        bAxisMoving[iAxis] = status.AxesStatus[iAxis].OpState == OperationState.Idle ? false : true;
                        bAlarm[iAxis] = status.AxesStatus[iAxis].AmpAlarm;
                        bCWL[iAxis] = status.AxesStatus[iAxis].PositiveLS;
                        bCCWL[iAxis] = status.AxesStatus[iAxis].NegativeLS;
                        bHome[iAxis] = status.AxesStatus[iAxis].HomeSwitch;
                        bHomeDone[iAxis] = status.AxesStatus[iAxis].HomeDone;
                        bAxisServo[iAxis] = status.AxesStatus[iAxis].ServoOn;
                        //bEstop[iAxis]=status.AxesStatus[iAxis].OpState
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            try
            {
                fx_PosBlock.Axis = axis;//运动的轴号
                fx_PosBlock.Profile.Acc = dAcc/0.001;
                fx_PosBlock.Profile.Dec = dDec / 0.001;
                fx_PosBlock.Profile.Velocity = dSpeed;
                fx_PosBlock.Profile.Type = ProfileType.SCurve;///建议使用S型曲线(特殊运动联系我司)
                fx_PosBlock.Profile.EndVelocity = 0;
                fx_PosBlock.Profile.StartingVelocity = 0;
                fx_PosBlock.Target = pos;
                fx_PosBlock.Profile.JerkAcc = dAcc / 0.001;//加加速的值越大，平滑度越小，速度提升的越快，设备震动的幅度会增大
                fx_PosBlock.Profile.JerkDec = dDec / 0.001;//减减速的值越大，平滑度越小，速度减少的越快，设备震动的幅度会增大

                int rtn = cmApi.Motion.StartPos(fx_PosBlock); ;
                if (rtn != 0) {
                    Global.logger.ErrorFormat("WMX3,轴{0}绝对点位运动失败，错误信息：{1}",axis, WMX3Api.ErrorToString(rtn));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3,轴{0}执行绝对点位运动出现异常，异常信息：{1}", axis, ex.Message);
                return false;
            }
        }

        public bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType interpolationAxisType)
        {
            try
            {
                fx_CenterAndEndCir.Profile.Type = ProfileType.SCurve;
                fx_CenterAndEndCir.Profile.StartingVelocity = 0;
                fx_CenterAndEndCir.Profile.EndVelocity = 0;

                switch (interpolationAxisType)
                {
                    case CoordinateType.XY:
                        fx_CenterAndEndCir.Axis[0] = AxisX;
                        fx_CenterAndEndCir.Axis[1] = AxisY;
                        fx_CenterAndEndCir.CenterPos[0] = 0.0;//单位为脉冲pulse(相对)
                        fx_CenterAndEndCir.CenterPos[1] = 0.0;//单位为脉冲pulse(相对)

                        CommonTool.Circle_Center(dCurrentPos[AxisX], dCurrentPos[AxisY], posX, posY, dR, iCCW, ref fx_CenterAndEndCir.CenterPos[0], ref fx_CenterAndEndCir.CenterPos[1]);

                        fx_CenterAndEndCir.EndPos[0] = posX;//单位为脉冲pulse
                        fx_CenterAndEndCir.EndPos[1] = posY;//单位为脉冲pulse
                        break;
                    case CoordinateType.XZ:
                        fx_CenterAndEndCir.Axis[0] = AxisX;
                        fx_CenterAndEndCir.Axis[1] = AxisZ;
                        fx_CenterAndEndCir.CenterPos[0] = 0.0;//单位为脉冲pulse(相对)
                        fx_CenterAndEndCir.CenterPos[1] = 0.0;//单位为脉冲pulse(相对)

                        CommonTool.Circle_Center(dCurrentPos[AxisX], dCurrentPos[AxisZ], posX, posY, dR, iCCW, ref fx_CenterAndEndCir.CenterPos[0], ref fx_CenterAndEndCir.CenterPos[1]);

                        fx_CenterAndEndCir.EndPos[0] = posX;//单位为脉冲pulse
                        fx_CenterAndEndCir.EndPos[1] = posY;//单位为脉冲pulse
                        break;
                    case CoordinateType.YZ:
                        break;
                    case CoordinateType.XYZ:
                        break;
                    default:
                        break;
                }

                fx_CenterAndEndCir.Profile.Velocity = dSpeed;//单位为deg
                
                fx_CenterAndEndCir.Profile.Acc = dAcc / 0.001;//单位为deg/s
                fx_CenterAndEndCir.Profile.Dec = dDec / 0.001;//单位为deg/s
                fx_CenterAndEndCir.Profile.JerkAcc = dAcc / 0.001;//单位为deg/s^2
                fx_CenterAndEndCir.Profile.JerkDec = dDec / 0.001;//单位为deg/s^2
                fx_CenterAndEndCir.Clockwise = (byte)(iCCW == 0 ? 1 : 0);//如果为0，则电弧将以逆时针方向。 如果为1，则弧线将按顺时针方向旋转。 
                int ret = cmApi.Motion.StartCircularIntplPos(fx_CenterAndEndCir);
                if (ret != 0)
                {
                    Global.logger.ErrorFormat("WMX3 执行圆弧插补出现错误！错误信息：{0}", WMX3Api.ErrorToString(ret));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3 执行圆弧插补出现异常！异常信息：{0}", ex.Message);
                return false;
            }
        }

        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            try
            {
                fx_CenterAndEndCir = new Motion.CenterAndEndCircularIntplCommand();
                fx_CenterAndEndCir.Profile.Type = ProfileType.SCurve;
                fx_CenterAndEndCir.Profile.StartingVelocity = 0;
                fx_CenterAndEndCir.Profile.EndVelocity = 0;
                fx_CenterAndEndCir.Axis[0] = AxisX;
                fx_CenterAndEndCir.Axis[1] = AxisY;
                fx_CenterAndEndCir.CenterPos[0] = 0.0;//单位为脉冲pulse(相对)
                fx_CenterAndEndCir.CenterPos[1] = 0.0;//单位为脉冲pulse(相对)

                CommonTool.Circle_Center(dCurrentPos[AxisX], 
                    dCurrentPos[AxisY], 
                    posX, 
                    posY, 
                    dR, 
                    iCCW,
                    ref fx_CenterAndEndCir.CenterPos[0], 
                    ref fx_CenterAndEndCir.CenterPos[1]);

                fx_CenterAndEndCir.Profile.Velocity = dSpeed;//单位为deg
                fx_CenterAndEndCir.EndPos[0] = posX;//单位为脉冲pulse
                fx_CenterAndEndCir.EndPos[1] = posY;//单位为脉冲pulse
                fx_CenterAndEndCir.Profile.Acc = dAcc / 0.001;//单位为deg/s
                fx_CenterAndEndCir.Profile.Dec = dDec / 0.001;//单位为deg/s
                fx_CenterAndEndCir.Profile.JerkAcc = dAcc / 0.001;//单位为deg/s^2
                fx_CenterAndEndCir.Profile.JerkDec = dDec / 0.001;//单位为deg/s^2
                fx_CenterAndEndCir.Clockwise = (byte)(iCCW == 0 ? 1 : 0);//如果为0，则电弧将以逆时针方向。 如果为1，则弧线将按顺时针方向旋转。 
                int ret = cmApi.Motion.StartCircularIntplPos(fx_CenterAndEndCir);
                if (ret != 0)
                {
                    Global.logger.ErrorFormat("WMX3 执行圆弧插补出现错误！错误信息：{0}", WMX3Api.ErrorToString(ret));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3 执行圆弧插补出现异常！异常信息：{0}", ex.Message);
                return false;
            }
        }

        public bool BuildCor(short num, CoordinateType interpolationAxisType)
        {
            return true;
            
        }

        public bool BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            return true;
            
        }

        public bool CureMoveDone(short num, out int iStep)
        {
            throw new NotImplementedException();
        }

        public bool FinishSearchHome(short axis)
        {
            return bHomeDone[axis];
        }

        public bool FinishSearchLimit(short axis)
        {
            return bCCWL[axis] || bCWL[axis];
        }

        public bool GetAlarm(short Axis)
        {
            return bAlarm[Axis];
        }

        public double GetCurrentPos(short Axis)
        {
            return dCurrentPos[Axis];
        }

        public bool GetEstop(short Axis)
        {
            return false;
        }

        public bool GetHome(short Axis)
        {
            return bHome[Axis];
        }

        public bool GetInputBit(int iBit)
        {
            return bBitInputStatus[iBit];
        }

        public bool GetLimtCCW(short Axis)
        {
            return bCCWL[Axis];
        }

        public bool GetLimtCW(short Axis)
        {
            return bCWL[Axis];
        }

        public bool GetOutBit(int iBit)
        {
            return bBitOutputStatus[iBit];
        }

        public bool GetServoOn(short Axis)
        {
            return bAxisServo[Axis];
        }

        public double GetVel(short Axis)
        {
            return 0;
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

        public bool IsMoveDone(short Axis)
        {
            return !bAxisMoving[Axis];
        }

        public bool IsMoving(short Axis)
        {
            return bAxisMoving[Axis];
        }

        public bool JogMove(short Axis, double dAcc, double dDec, double dVel)
        {
            if (!bInitOK)
                return false;

            lock (lockObj)
            {
                s_JogCommand.Axis = Axis;                        //运动的轴号
                s_JogCommand.Profile.Type = ProfileType.SCurve;  //运动曲线类型，建议用SCurve
                s_JogCommand.Profile.Velocity = dVel;            //运动速度
                s_JogCommand.Profile.Acc = dAcc/0.001;                 //运动加速度
                s_JogCommand.Profile.JerkAcc = dAcc / 0.001;              //运动加加速(加速度的加速度)  
                s_JogCommand.Profile.Dec = dDec / 0.001;                 //运动减速度
                s_JogCommand.Profile.JerkDec = dDec / 0.001;              //运动减减速(减速度的加速度)
                int rtn = 0;
                rtn = cmApi.Motion.StartJog(s_JogCommand);

                if (rtn != 0)
                {
                    string errorCode = "";
                    errorCode = WMX3Api.ErrorToString(rtn);
                    Global.logger.ErrorFormat("WMX3初始化失败！错误信息：{0}", errorCode);
                    return false;
                }
            }
            return true;
        }

        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            lock (this)
            {
                try
                {
                    #region 直线插补参数设置
                    fx_LinearIntplCommand.AxisCount = 2;////代表几轴插补(2轴插补)
                    fx_LinearIntplCommand.Axis[0] = AxisX;
                    fx_LinearIntplCommand.Axis[1] = AxisY;
                    fx_LinearIntplCommand.MaxAcc[0] = dAcc / 0.001;
                    fx_LinearIntplCommand.MaxAcc[1] = dAcc / 0.001;
                    fx_LinearIntplCommand.MaxDec[0] = dDec / 0.001;
                    fx_LinearIntplCommand.MaxDec[1] = dDec / 0.001;
                    fx_LinearIntplCommand.MaxJerkAcc[0] = dAcc / 0.001;
                    fx_LinearIntplCommand.MaxJerkAcc[1] = dAcc / 0.001;
                    fx_LinearIntplCommand.MaxJerkDec[0] = dDec / 0.001;
                    fx_LinearIntplCommand.MaxJerkDec[1] = dDec / 0.001;
                    fx_LinearIntplCommand.Target[0] = posX;
                    fx_LinearIntplCommand.Target[1] = posY;
                    fx_LinearIntplCommand.Profile.Type = ProfileType.Trapezoidal;
                    fx_LinearIntplCommand.Profile.Velocity = dSpeed / 0.001;//合成速度
                    fx_LinearIntplCommand.Profile.Acc = dAcc / 0.001;//合成加速度
                    fx_LinearIntplCommand.Profile.Dec = dDec / 0.001;//合成减速度
                    fx_LinearIntplCommand.Profile.JerkAcc = dAcc / 0.001;//合成加加速
                    fx_LinearIntplCommand.Profile.JerkDec = dDec / 0.001;//合成减减速 
                    #endregion

                    int rtn = cmApi.Motion.StartLinearIntplPos(fx_LinearIntplCommand);
                    if (rtn != 0) {
                        Global.logger.ErrorFormat("WMX3 直线插补出现错误，错误信息：{0}", WMX3Api.ErrorToString(rtn));
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Global.logger.ErrorFormat("WMX3 直线插补出现异常，异常信息：{0}", ex.Message);
                    return false;
                }
            }
        }

        public void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py)
        {
            
        }

        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            try
            {
                fx_PosBlock.Axis = axis;//运动的轴号
                fx_PosBlock.Profile.Acc = dAcc/0.001;
                fx_PosBlock.Profile.Dec = dDec / 0.001;
                fx_PosBlock.Profile.Velocity = dSpeed;
                fx_PosBlock.Profile.Type = ProfileType.SCurve;///建议使用S型曲线(特殊运动联系我司)
                fx_PosBlock.Profile.EndVelocity = dSpeed;
                fx_PosBlock.Profile.StartingVelocity = dSpeed;
                fx_PosBlock.Target = pos;
                fx_PosBlock.Profile.JerkAcc = dAcc / 0.001;//加加速的值越大，平滑度越小，速度提升的越快，设备震动的幅度会增大
                fx_PosBlock.Profile.JerkDec = dDec / 0.001;//减减速的值越大，平滑度越小，速度减少的越快，设备震动的幅度会增大

                int rtn = cmApi.Motion.StartMov(fx_PosBlock); ;
                if (rtn != 0)
                {
                    Global.logger.ErrorFormat("WMX3,轴{0}相对点位运动失败，错误信息：{1}", axis, WMX3Api.ErrorToString(rtn));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3,轴{0}执行相对点位运动出现异常，异常信息：{1}", axis, ex.Message);
                return false;
            }
        }

        public bool ServoOff(short axis)
        {
            try
            {
                int rtn = cmApi.AxisControl.SetServoOn(axis, 0);
                if (rtn != 0)
                {
                    Global.logger.ErrorFormat("WMX3关闭伺服{0}出现错误，错误信息：{1}", axis, WMX3Api.ErrorToString(rtn));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3关闭伺服{0}出现异常，异常信息：{1}", axis, ex.Message);
                return false;
            }
        }

        public bool ServoOn(short axis)
        {
            try
            {
                int rtn = cmApi.AxisControl.SetServoOn(axis, 1);
                if (rtn != 0)
                {
                    Global.logger.ErrorFormat("WMX3使能伺服{0}出现错误，错误信息：{1}",axis ,WMX3Api.ErrorToString(rtn));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3使能伺服{0}出现异常，异常信息：{1}", axis, ex.Message);
                return false;
            }
        }

        public bool SetAlarmOff(short axis)
        {
            return false;
        }

        public bool SetAlarmOn(short axis)
        {
            return false;
        }

        public bool SetAxisPos(short axis, double dPos)
        {
            return false;
        }

        public bool SetHomeOff(short axis)
        {
            return false;
        }

        public bool SetHomeOn(short axis)
        {
            return false;
        }

        public bool SetLimtDisable(short axis)
        {
            return false;
        }

        public bool SetLimtOff(short axis)
        {
            return false;
        }

        public bool SetLimtOn(short axis)
        {
            return false;
        }

        public bool SetNearHomeOff(short axis)
        {
            return false;
        }

        public bool SetNearHomeOn(short axis)
        {
            return false;
        }

        public bool SetOutBit(int iBit, bool bOn)
        {
            int pos = iBit / 8;
            int offset = iBit % 8;
            int ret;
            if (bOn)
                ret = wmx_IO.SetOutBit(pos, offset, 1);
            else
                ret = wmx_IO.SetOutBit(pos, offset, 0);

            return ret == 0 ? true:false ;
        }

        public bool SetPulseMode(short axis, PulseMode psm)
        {
            return false;
        }

        public bool SetVel(short Axis, double dVel)
        {
            return false;
        }

        public bool StartCure(short num, bool bPreWelding)
        {
            return false;
        }

        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            if (bHomeing[axis] == false)
            {
                lock (lockObj)
                {
                    try
                    {
                        homeParam = new Config.HomeParam();
                        int rtn = cmApi.Config.GetHomeParam(axis, ref homeParam);
                        homeParam.HomeDirection = Config.HomeDirection.Normal;//回零方向：正向(Normal)；负向(Reverse)
                        homeParam.HomeType = Config.HomeType.HS;//HomeType 回零模式：当前点(CurrentPos) Z 相(IndexPulse) 原点开关(HS) 两次原点回零(HSHS) 原点 + Z 相(HSReverseIndexPulse) 限位开关(LS)限位开关 + Z 相(LSReverseIndexPulse) 额外限位(ExternalLS))
                        homeParam.HomingVelocityFast = dHomeSpd;
                        homeParam.HomingVelocityFastAcc = dAcc;
                        homeParam.HomingVelocityFastDec = dDec;
                        homeParam.HomingVelocitySlow = dHomeSpd;
                        homeParam.HomingVelocitySlowAcc = dAcc;
                        homeParam.HomingVelocitySlowDec = dDec;
                        homeParam.HomeShiftDistance = 0;//找到原点后，偏移的距离。正负号表明偏移的方向。 偏移到新的位置后，把此位置作为实际的原点。 
                        homeParam.HomeShiftVelocity = dHomeSpd;//回零中，在偏移阶段采用的速度。
                        homeParam.HomeShiftAcc = dAcc;//homeShiftVelocity的加速度。
                        homeParam.HomeShiftDec = dDec;//homeShiftVelocity的减速度。 

                        rtn = cmApi.Config.SetHomeParam(axis, homeParam);

                        rtn = cmApi.Home.StartHome(axis);

                        if (rtn != 0)
                        {
                            Global.logger.ErrorFormat("WMX3 回原点出现错误，错误信息：{0}", WMX3Api.ErrorToString(rtn));
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Global.logger.ErrorFormat("WMX3 回原点出现异常，异常信息：{0}", ex.Message);
                        return false;
                    }
                }
                bHomeStop[axis] = false;
                bHomeDone[axis] = false;
                bHomeing[axis] = true;
                bHomeLast[axis] = false;
                Thread thread = new Thread(new ParameterizedThreadStart(CheckHomeReturn));
                thread.IsBackground = true;
                thread.Start(axis);
            }
            return true;
        }

        private void CheckHomeReturn(object obj)
        {
            short axis = (short)obj;
            while (true)
            {
                if (bHomeStop[axis])
                {
                    bHomeing[axis] = false;
                    bHomeStop[axis] = false;
                    bHomeDone[axis] = false;
                    return;
                }
                if (bHomeDone[axis])
                {
                    bHomeLast[axis] = true;
                    ZeroAxisPos(axis);
                    bHomeing[axis] = false;
                    homeParam.HomePosition = dCurrentPos[axis];
                    return;
                }
                Thread.Sleep(1);
            }
        }

        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            bool sRtn;
            bHomeStop[axis] = false;
            bHomeDone[axis] = false;
            bHomeLast[axis] = false;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    s_JogCommand.Axis = axis;                        //运动的轴号
                    s_JogCommand.Profile.Type = ProfileType.SCurve;  //运动曲线类型，建议用SCurve
                    s_JogCommand.Profile.Velocity = dCatchSpeed;            //运动速度
                    s_JogCommand.Profile.Acc = dAcc/0.001;                 //运动加速度
                    s_JogCommand.Profile.JerkAcc = dAcc / 0.001;              //运动加加速(加速度的加速度)  
                    s_JogCommand.Profile.Dec = dDec / 0.001;                 //运动减速度
                    s_JogCommand.Profile.JerkDec = dDec / 0.001;              //运动减减速(减速度的加速度)
                    int rtn = 0;
                    rtn = cmApi.Motion.StartJog(s_JogCommand);

                    if (rtn != 0)
                    {
                        string errorCode = "";
                        errorCode = WMX3Api.ErrorToString(rtn);
                        Global.logger.ErrorFormat("WMX3初始化失败！错误信息：{0}", errorCode);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StopJog(short Axis)
        {
            int rtn = 0;
            try
            {
                if (Axis < 8 && Axis > -1)
                {
                    lock (lockObj)
                    {
                        rtn = cmApi.Motion.Stop(Axis);
                        if (rtn != 0)
                        {
                            string errorCode = "";
                            errorCode = WMX3Api.ErrorToString(rtn);
                            Global.logger.ErrorFormat("WMX3 轴{0}停止失败！错误信息：{1}", Axis, errorCode);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3 轴{0}停止异常！异常信息：{1}", Axis, ex.Message);
                return false;
            }
        }

        public bool StopMove(short Axis)
        {
            int rtn = 0;
            bHomeStop[Axis] = true;
            try
            {
                if (Axis < 8 && Axis > -1)
                {
                    lock (lockObj)
                    {
                        rtn = cmApi.Motion.Stop(Axis);
                        if (rtn != 0)
                        {
                            string errorCode = "";
                            errorCode = WMX3Api.ErrorToString(rtn);
                            Global.logger.ErrorFormat("WMX3 轴{0}停止失败！错误信息：{1}", Axis, errorCode);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3 轴{0}停止异常！异常信息：{1}", Axis, ex.Message);
                return false;
            }
            
        }

        public bool ZeroAxisPos(short axis)
        {
            bool sRtn = false;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    //cmApi.Motion
                    sRtn = APS168_W32.APS168.APS_set_command_f(axis, 0.0) == 0 ? true : false;
                    sRtn = APS168_W32.APS168.APS_set_position_f(axis, 0.0) == 0 ? true : false;
                    return sRtn;
                }
            }
            else
            {
                return sRtn;
            }
        }

        public bool ArcMoveWithThroughAndEnd(short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double endPosX, double endPosY, double throughPosX, double throughPosY)
        {
            try
            {
                int rtn = 0;
                fx_ThroughAndEnd.Axis[0] = AxisX;
                fx_ThroughAndEnd.Axis[1] = AxisY;
                fx_ThroughAndEnd.EndPos[0] = endPosX;
                fx_ThroughAndEnd.EndPos[1] = endPosY;
                fx_ThroughAndEnd.ThroughPos[0] = throughPosX;
                fx_ThroughAndEnd.ThroughPos[1] = throughPosY;
                fx_ThroughAndEnd.Profile.Acc = dAcc/0.001;
                fx_ThroughAndEnd.Profile.Dec = dDec / 0.001;
                fx_ThroughAndEnd.Profile.Velocity = dSpeed;
                fx_ThroughAndEnd.Profile.StartingVelocity = dSpeed;//该参数确定加减速曲线的初始速度
                fx_ThroughAndEnd.Profile.SecondVelocity = dSpeed;//此参数确定加减速曲线的第二个速度
                fx_ThroughAndEnd.Profile.Type = ProfileType.SCurve;//动作的曲线类型。SCurve:产生S形速度曲线。 
                fx_ThroughAndEnd.Profile.EndVelocity = dSpeed;
                fx_ThroughAndEnd.Profile.JerkAcc = dAcc / 0.001;
                fx_ThroughAndEnd.Profile.JerkDec = dDec / 0.001;
                rtn = cmApi.Motion.StartCircularIntplPos(fx_ThroughAndEnd);

                if (rtn != 0)
                {
                    string errorCode = "";
                    errorCode = WMX3Api.ErrorToString(rtn);
                    Global.logger.ErrorFormat("WMX3 执行 ThroughAndEndCircularIntplCommand 圆弧插补失败！错误信息：{0}", errorCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.logger.ErrorFormat("WMX3 执行 ThroughAndEndCircularIntplCommand 圆弧插补出现异常！异常信息：{0}", ex.Message);
                return false;
            }
        }

        public bool GantryMove(short gantryAxis1, short gantryAxis2, double dAcc, double dDec, double dSpeed, double endPos)
        {
            throw new NotImplementedException();
        }


        public bool ResetAxisAlarm()
        {
            throw new NotImplementedException();
        }


        public bool StopInterpolation()
        {
            throw new NotImplementedException();
        }

        public bool ResumeInterpolation()
        {
            throw new NotImplementedException();
        }


        public bool ComparePositionOut(int axis,double pos,out double different)
        {
            throw new NotImplementedException();
        }


        public void PreWelding(int preWeldingNumber, List<double> preWeldingPos)
        {
            throw new NotImplementedException();
        }


        public bool PreWeldingDone()
        {
            throw new NotImplementedException();
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
