
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ControlPlatformLib.ADLINK;

namespace ControlPlatformLib
{
    /*****************ADLINK MotionCard Information*****************
     *Vendor: ADLINK 凌华
     *Type: MotionCard
     *Hardware Interface Type: PCI
     *Version: 1.0.1.0
     *Author：WuChenJie
     **************************************************************/
    public class ADLINKMotionCard : HardWareBase, IMotionAction, IOutputAction, IInputAction
    {
        public int usCardNo = 0;
        /////Main form data/////////////////////////////////////////////////////////////
        const int YES = 1;
        const int NO = 0;
        const int ON = 1;
        const int OFF = 0;

        int v_board_id = -1;
        int v_card_name = 0;
        int v_channel = 0;
        int v_total_axis = 0;
        int v_is_card_initialed = 0;
        //////////////////////////////////////////////////////////////////////////////////
        int Is_Creat = NO;

        private bool[] bAlarm = new bool[16];

        private bool[] bAxisMoving = new bool[16];

        private bool[] bAxisServo = new bool[16];

        #region Card Status
        private bool[] bBitInputStatus = new bool[64];
        private bool[] bBitOutputStatus = new bool[64];
        private bool[] bEstop = new bool[16];
        private bool[] bCCWL = new bool[16];
        private bool[] bCWL = new bool[16];
        private bool[] bHome = new bool[16];
        private bool[] bHomeDone = new bool[16];
        private bool[] bHomeing = new bool[16];
        private bool[] bHomeLast = new bool[16];
        private bool[] bHomeStop = new bool[16];
        private double[] dCommandTargetPos = new double[16];
        private double[] dCurrentPos = new double[16];
        private double[] dCurrentVel = new double[16];
        private double[] dTargetPos = new double[16];
        private int[,] iColAixsNo = new int[16, 3];
        private int[] iMoveMode = new int[16];
        private MoveMode moveMode;
        #endregion
        public override bool Init(HardWareInfoBase infoHardWare)
        {
            ADLINKTechMCInfo adlinkMotionCard = infoHardWare as ADLINKTechMCInfo;
            int boardID_InBits = 0;//*BoardID_InBits：传回值，以bit表示，如果回传值为0x11，代表目前电脑上有二张卡片；
            int mode = 0;//0：系统指定卡号 1：由拨码开关指定卡号 ；
            int ret = 0;
            int i = 0;
            int card_name = 0;
            int tamp = 0;
            int StartAxisID = 0;
            int TotalAxisNum = 0;

            Global.logger.InfoFormat("初始化凌华运动控制卡,卡名称{0}", adlinkMotionCard.hardwareName);
            usCardNo = adlinkMotionCard.iCardNo;

            ret = APS168_W64.APS168.APS_initial(ref boardID_InBits, mode);//初始化(不管电脑接了几张卡，只需初始化一次) 。
            if (ret == 0)
            {
                for (i = 0; i < 16; i++)
                {
                    tamp = (boardID_InBits >> i) & 1;

                    if (tamp == 1)
                    {
                        ret = APS168_W64.APS168.APS_get_card_name(i, ref card_name);

                        if (card_name == (int)APS_Define_W32.APS_Define.DEVICE_NAME_PCI_825458
                            || card_name == (int)APS_Define_W32.APS_Define.DEVICE_NAME_AMP_20408C)
                        {
                            ret = APS168_W64.APS168.APS_get_first_axisId(i, ref StartAxisID, ref TotalAxisNum);

                            //----------------------------------------------------
                            v_card_name = card_name;
                            v_board_id = i;
                            v_total_axis = TotalAxisNum;
                            v_is_card_initialed = YES;

                            //ret = APS168_W64.APS168.APS_load_param_from_file(infoHardWare.ConfigName);
                            ret = APS168_W64.APS168.APS_load_param_from_file(System.AppDomain.CurrentDomain.BaseDirectory + adlinkMotionCard.m_strConfigPath);
                            bInitOK = true;
                            if (v_total_axis == 4)
                                v_channel = 2;
                            else if (v_total_axis >= 4)
                                v_channel = 4;

                            v_total_axis = 16;
                            //----------------------------------------------------
                            Is_Creat = NO;
                            Global.logger.InfoFormat("初始化凌华运动控制卡{0}成功。", adlinkMotionCard.hardwareName);
                            break;
                        }
                    }
                }

                if (v_board_id == -1)
                {
                    v_is_card_initialed = NO;
                    Global.logger.ErrorFormat("初始化凌华运动控制卡{0}失败。", adlinkMotionCard.hardwareName);
                    bInitOK = false;
                }
            }
            else
            {
                v_is_card_initialed = NO;
                Global.logger.ErrorFormat("初始化凌华运动控制卡{0}失败。", adlinkMotionCard.hardwareName);
                bInitOK = false;
            }
            Thread threadScan = new Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return bInitOK;
        }

        private void ScanThreadFunction()
        {
            Thread.Sleep(1000);
            while (true)
            {
                Thread.Sleep(10);
                //if (Global.bClosing)
                //    break;
                GetAllMotionStatus();
                GetAllIOStatus();
            }
        }

        public override bool Close()
        {
            int Axis_ID = 0;
            int Servo_On = OFF;
            int ret = 0;
            if (v_is_card_initialed == YES)
            {
                for (Axis_ID = 0; Axis_ID < v_total_axis; Axis_ID++)
                {
                    ret = APS168_W64.APS168.APS_set_servo_on(Axis_ID, Servo_On);
                }
                bInitOK = false;
                APS168_W64.APS168.APS_close();
            }
            return true;
        }
        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    moveMode = MoveMode.ABS;
                    dCommandTargetPos[axis] = pos;
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ACC, dAcc / 0.001);
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_DEC, dDec / 0.001);
                    sRtn = APS168_W64.APS168.APS_absolute_move(axis, (int)pos, (int)dSpeed) == 0 ? true : false;
                }
                return sRtn;
            }
            else
            {
                return sRtn;
            }
        }

        public bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType interpolationAxisType)
        {
            Global.logger.Info("---------------------------------");
            Global.logger.Info("开始XY圆弧插补运动");
            BuildCor(num, CoordinateType.XY);
            InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
            bool result = StartCure(num,false);
            if (result)
                Global.logger.Info("XY圆弧插补运动完成");
            else
                Global.logger.Info("XY圆弧插补运动失败");
            return result;
        }

        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            if (!bInitOK) return false;

            try
            {
                double[] centerArray = new double[] { 1000.0, 0.0 };
                double angle = (90.0 * 3.14159265 / 180.0);
                double transPara = 0;
                APS168_W64.ASYNCALL p = new APS168_W64.ASYNCALL();
                int[] axisArr = new int[] { AxisX, AxisY };
                double[] endArr = new double[] { posX, posY };
                // config speed profile
                APS168_W64.APS168.APS_set_axis_param_f(AxisX, (Int32)APS_Define_W32.APS_Define.PRA_SF, 0.5);
                APS168_W64.APS168.APS_set_axis_param_f(AxisX, (Int32)APS_Define_W32.APS_Define.PRA_ACC, dAcc / 0.001);
                APS168_W64.APS168.APS_set_axis_param_f(AxisX, (Int32)APS_Define_W32.APS_Define.PRA_DEC, dDec / 0.001);
                APS168_W64.APS168.APS_set_axis_param_f(AxisX, (Int32)APS_Define_W32.APS_Define.PRA_VM, dSpeed);

                ////servo on
                //APS168_W64.APS168.APS_set_servo_on(Axis_ID_Array[0], 1);
                //Thread.Sleep(500); // Wait stable.

                //servo on
                //APS168.APS_set_servo_on(Axis_ID_Array[1], 1);
                //Thread.Sleep(500); // Wait stable.

                // Start a 2 dimension linear interpolation
                APS168_W64.APS168.APS_arc2_ce(
                    axisArr,
                    0,
                    centerArray,
                    endArr,
                    iCCW,
                    ref transPara,
                    ref p
                );

                return true;
            }
            catch (Exception e)
            {
                Global.logger.ErrorFormat("凌华运动控制卡圆弧插补运动失败，错误：{0}。", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据两点坐标、半径及方向输出圆心坐标
        /// </summary>
        /// <param name="x1">X1</param>
        /// <param name="y1">Y1</param>
        /// <param name="x2">X2</param>
        /// <param name="y2">Y2</param>
        /// <param name="r">半径</param>
        /// <param name="x">圆心坐标</param>
        /// <param name="y">圆心坐标</param>
        /// <param name="angle">角度</param>
        /// <param name="dir">方向 0：顺时针 1：逆时针</param>
        private void YuanXin(double x1, double y1, double x2, double y2, double r, ref double x, ref double y, ref double angle, int dir)
        {
            double c1 = (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1) / (2 * (x2 - x1));
            double c2 = (y2 - y1) / (x2 - x1);
            double A = (c2 * c2 + 1);
            double B = (2 * x1 * c2 - 2 * c1 * c2 - 2 * y1);
            double C = x1 * x1 - 2 * x1 * c1 + c1 * c1 + y1 * y1 - r * r;
            //cout << B*B - 4 * A*C << endl;  

            switch (dir)
            {
                case 0://逆时针
                    y = (-B - System.Math.Sqrt(B * B - 4 * A * C)) / (2 * A);
                    break;
                case 1://顺时针
                    y = (-B + System.Math.Sqrt(B * B - 4 * A * C)) / (2 * A);
                    break;
                default:
                    break;
            }
            x = c1 - c2 * y;
            //double a = (System.Math.Pow((x1 - x2), 2) + System.Math.Pow((y1 - y2), 2))/(2*r);
            //angle = 2 * System.Math.Asin(System.Math.Sqrt(a));
            angle = System.Math.Atan((y2 - y) / (x2 - x)) - System.Math.Atan((y1 - y) / (x1 - x));
            //cout << "x=" << x << "y=" << y << endl;  
        }

        public bool BuildCor(short num, CoordinateType interpolationAxisType)
        {
            return true;
        }

        public bool  BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            return true;
            throw new NotImplementedException();
        }

        public bool CureMoveDone(short num, out int iStep)
        {
            throw new NotImplementedException();
        }

        public bool FinishSearchHome(short axis)
        {
            return !bAxisMoving[axis] && bHomeDone[axis];
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
            if (Axis < 16 && Axis > -1)
            {
                return dCurrentPos[Axis];
            }
            else
            {
                return 0.0;
            }
        }

        public bool GetEstop(short Axis)
        {
            return bEstop[Axis];
        }

        public bool GetHome(short Axis)
        {
            return bHome[Axis];
        }

        public bool GetLimtCCW(short Axis)
        {
            return bCCWL[Axis];
        }

        public bool GetLimtCW(short Axis)
        {
            return bCWL[Axis];
        }

        public bool GetServoOn(short Axis)
        {
            if (Axis < 16 && Axis > -1)
            {
                return bAxisServo[Axis];
            }
            else
            {
                return false;
            }
        }

        public double GetVel(short Axis)
        {
            int sRtn;
            double dVel = 0.0;
            if (Axis < 16 && Axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = APS168_W64.APS168.APS_get_feedback_velocity_f(Axis, ref dVel);//获取目标速度
                    return dVel;
                }
            }
            else
            {
                return dVel;
            }
        }

        public bool HandleErrorMessage(short errorMessage)
        {
            return true;
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
            if (!bInitOK)
                return false;
            int axis_id = Axis;
            int msts = 0;
            int m_stop_code = 0;

            msts = APS168_W64.APS168.APS_motion_status(axis_id); // Get motion status
            msts = (msts >> 5) & 0x1;                   // Get motion done bit

            // Get stop code.
            APS168_W64.APS168.APS_get_stop_code(axis_id, ref m_stop_code);
            if (IsMoving(Axis))
            {
                return false;
            }
            else
            {
                double dDiffValue = Math.Abs(dCommandTargetPos[Axis] - dCurrentPos[Axis]);//
                if (dDiffValue < 100)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //if (msts == 1)
                //{
                //    // Check move success or not
                //    msts = APS168_W64.APS168.APS_motion_status(axis_id); // Get motion status
                //    msts = (msts >> 16) & 0x1;                           // Get abnormal stop bit

                //    if (msts == 1)
                //    { // Error handling ...
                //        APS168_W64.APS168.APS_get_stop_code(axis_id, ref m_stop_code);
                //        return false; //error
                //    }
                //    else
                //    {   // Motion success.
                //        //double dDiffValue = Math.Abs(dCommandTargetPos[axis_id] - dCurrentPos[axis_id]);//
                //        //if (dDiffValue < 0.001)
                //        //{
                //        //    return true;
                //        //}
                //        //else
                //        //{
                //        //    return false;
                //        //}

                //        double dDiffValue = Math.Abs(dCommandTargetPos[Axis] - dCurrentPos[Axis]);//
                //        if (dDiffValue < 100)
                //        {
                //            return true;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //}
            }
        }

        public bool IsMoving(short Axis)
        {
            if (!bInitOK)
                return false;
            int axis_id = Axis;
            int msts = 0;
            int m_stop_code = 0;

            msts = APS168_W64.APS168.APS_motion_status(axis_id); // Get motion status
            msts = (msts >> 5) & 0x1;                   // Get motion done bit

            //// Get stop code.
            //APS168_W64.APS168.APS_get_stop_code(axis_id, ref m_stop_code);

            if (msts == 0)
                return true;
            else
                return false;
        }

        public bool JogMove(short Axis, double dAcc, double dDec, double dVel)
        {
            bool sRtn = false;
            if (!bInitOK)
                return false;

            if (Is_axis_err(Axis) == YES)
                return false;

            lock (lockObj)
            {
                APS168_W64.APS168.APS_jog_start(Axis, 0);
                APS168_W64.APS168.APS_set_axis_param(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_MODE, 0);  // Set jog mode
                APS168_W64.APS168.APS_set_axis_param(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_DIR, dVel > 0 ? 0 : 1);  // Set jog direction 0: Positive direction, 1: Negative direction

                APS168_W64.APS168.APS_set_axis_param_f(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_CURVE, 0.0);
                APS168_W64.APS168.APS_set_axis_param_f(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_ACC, dAcc / 0.001);
                APS168_W64.APS168.APS_set_axis_param_f(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_DEC, dDec / 0.001);
                APS168_W64.APS168.APS_set_axis_param_f(Axis, (int)APS_Define_W32.APS_Define.PRA_JG_VM, Math.Abs(dVel));

                //APS168_W64.APS168.APS_set_servo_on(Axis,1);
                // Create a rising edge.
                sRtn = APS168_W64.APS168.APS_jog_start(Axis, 1) == 0 ? true : false;  //Jog ON
            }

            return sRtn;
        }

        public int Is_axis_err(int Axis_ID)
        {
            if (Axis_ID < 0 || Axis_ID >= 16)
                return YES;
            else
                return NO;
        }

        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            bool sRtn = false;
            double[] PositionArray = new double[] { posX, posY };
            double TransPara = 0;

            APS168_W64.ASYNCALL p = new APS168_W64.ASYNCALL();
            int[] Axis_ID_Array = new int[] { AxisX, AxisY };
            // config speed profile
            APS168_W64.APS168.APS_set_axis_param_f(Axis_ID_Array[0], (int)APS_Define_W32.APS_Define.PRA_SF, 0.5);
            APS168_W64.APS168.APS_set_axis_param_f(Axis_ID_Array[0], (int)APS_Define_W32.APS_Define.PRA_ACC, dAcc);
            APS168_W64.APS168.APS_set_axis_param_f(Axis_ID_Array[0], (int)APS_Define_W32.APS_Define.PRA_DEC, dDec);
            APS168_W64.APS168.APS_set_axis_param_f(Axis_ID_Array[0], (int)APS_Define_W32.APS_Define.PRA_VM, dSpeed);

            ////servo on
            //APS168_W64.APS168.APS_set_servo_on(Axis_ID_Array[0], 1);
            //Thread.Sleep(500); // Wait stable.

            ////servo on
            //APS168_W64.APS168.APS_set_servo_on(Axis_ID_Array[1], 1);
            //Thread.Sleep(500); // Wait stable.

            // Start a 2 dimension linear interpolation
            sRtn = APS168_W64.APS168.APS_line(
                  2 // I32 Dimension
                , Axis_ID_Array // I32 *Axis_ID_Array
                , (int)APS_Define_W32.APS_Define.OPT_ABSOLUTE  // I32 Option
                , PositionArray // F64 *PositionArray
                , ref TransPara    // F64 *TransPara
                , ref p // ASYNCALL *Wait
            ) == 0 ? true : false;

            return sRtn;
        }

        public void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py)
        {
            throw new NotImplementedException();
        }

        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    double iTargetPos = dCurrentPos[axis] + pos;
                    dCommandTargetPos[axis] = iTargetPos;
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ACC, dAcc / 0.001);
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_DEC, dDec / 0.001);
                    sRtn = APS168_W64.APS168.APS_relative_move(axis, (int)pos, (int)dSpeed) == 0 ? true : false;
                }
                return sRtn;
            }
            else
            {
                return sRtn;
            }
        }

        public bool ServoOff(short axis)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = APS168_W64.APS168.APS_set_servo_on(axis, 0) == 0 ? true : false;
                    return sRtn;
                }
            }
            else
            {
                return sRtn;
            }
        }

        public bool ServoOn(short axis)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = APS168_W64.APS168.APS_set_servo_on(axis, 1) == 0 ? true : false;
                    return sRtn;
                }
            }
            else
            {
                return sRtn;
            }
        }

        public bool SetAlarmOff(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ALM_LOGIC, 0) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetAlarmOn(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ALM_LOGIC, 1) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetAxisPos(short axis, double dPos)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = APS168_W64.APS168.APS_set_command_f(axis, dPos) == 0 ? true : false;
                    sRtn = APS168_W64.APS168.APS_set_position_f(axis, dPos) == 0 ? true : false;
                    return sRtn;
                }
            }
            else
            {
                return sRtn;
            }
        }

        public bool SetHomeOff(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ORG_LOGIC, 0) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetHomeOn(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_ORG_LOGIC, 1) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetLimtDisable(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_EL_LOGIC, 0) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetLimtOff(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_EL_LOGIC, 0) == 0 ? true : false;
            }
            return sRtn;
        }

        public bool SetLimtOn(short axis)
        {
            bool sRtn = false;
            lock (lockObj)
            {
                sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_EL_LOGIC, 1) == 0 ? true : false;
            }
            return sRtn;
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
            bool sRtn = false;
            lock (lockObj)
            {
                if (psm == PulseMode.PLDI)
                {
                    sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_PLS_IPT_MODE, 0) == 0 ? true : false;
                }
                else
                {
                    sRtn = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_PLS_IPT_MODE, 1) == 0 ? true : false;
                }
            }
            return sRtn;
        }

        public bool SetVel(short Axis, double dVel)
        {
            throw new NotImplementedException();
        }

        public bool StartCure(short num, bool bPreWelding)
        {
            throw new NotImplementedException();
        }

        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            bool sRtn;
            if (bHomeing[axis] == false)
            {
                lock (lockObj)
                {
                    //This example shows how home move operation
                    int axis_id = axis;
                    int return_code = 0;

                    // 1. Select home mode and config home parameters 
                    APS168_W64.APS168.APS_set_axis_param(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_MODE, 0); // Set home mode
                    APS168_W64.APS168.APS_set_axis_param(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_DIR, 1); // Set home APS168_W64.direction
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_CURVE, 0); // Set APS168_W64.acceleration paten (T-curve)
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_ACC, dAcc / 0.001); // Set APS168_W64.homing acceleration rate
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_VM, dHomeSpd); // Set homing APS168_W64.maximum velocity.
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_VO, dHomeSpd); // Set homing APS168_W64.VO speed
                    APS168_W64.APS168.APS_set_axis_param(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_EZA, 0); // Set EZ signal APS168_W64.alignment (yes or no)
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_SHIFT, 0); // Set home APS168_W64.position shfit distance. 
                    APS168_W64.APS168.APS_set_axis_param_f(axis_id, (int)APS_Define_W32.APS_Define.PRA_HOME_POS, 0); // Set final home position.


                    ////servo on
                    //APS168_W64.APS168.APS_set_servo_on(axis_id, 1);
                    //Thread.Sleep(500); // Wait stable.


                    // 2. Start home move
                    return_code = APS168_W64.APS168.APS_home_move(axis_id); //Start homing 
                    if (return_code != (int)APS_Define_W32.APS_Define.ERR_NoError)
                    { /* Error handling */
                        Global.logger.ErrorFormat("凌华运动控制卡回原点出错，错误码：{0}", return_code);
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
            int iPos;

            while (true)
            {
                if (bHomeStop[axis])
                {
                    bHomeing[axis] = false;
                    bHomeStop[axis] = false;
                    return;
                }
                if (IsMoveDone(axis))
                {
                    break;
                }
                Thread.Sleep(1);
            }
            bHomeLast[axis] = true;
            while (IsMoving(axis))
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(100);

            //ReturnToHomePos(axis, iPos);
            //Thread.Sleep(50);
            ////判断运动完成
            //while (IsMoving(axis))
            //{
            //    Thread.Sleep(1);
            //}
            //Thread.Sleep(10);
            ZeroAxisPos(axis);
            bHomeDone[axis] = true;
            bHomeing[axis] = false;
        }

        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            bool sRtn;
            bHomeStop[axis] = false;
            bHomeDone[axis] = false;
            bHomeLast[axis] = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    //axis++;
                    APS168_W64.APS168.APS_jog_start(axis, 0);
                    APS168_W64.APS168.APS_set_axis_param(axis, (int)APS_Define_W32.APS_Define.PRA_JG_MODE, 0);  // Set jog mode
                    APS168_W64.APS168.APS_set_axis_param(axis, (int)APS_Define_W32.APS_Define.PRA_JG_DIR, dCatchSpeed > 0 ? 0 : 1);  // Set jog direction 0: Positive direction, 1: Negative direction

                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_JG_CURVE, 0.0);
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_JG_ACC, dAcc / 0.001);
                    APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_JG_DEC, dDec / 0.001);
                    int res = APS168_W64.APS168.APS_set_axis_param_f(axis, (int)APS_Define_W32.APS_Define.PRA_JG_VM, Math.Abs(dCatchSpeed));

                    Thread.Sleep(100);
                    //servo on
                    //APS168_W64.APS168.APS_set_servo_on(Axis, 1);
                    //Thread.Sleep(100); // Wait stable.

                    // Create a rising edge.
                    sRtn = APS168_W64.APS168.APS_jog_start(axis, 1) == 0 ? true : false;  //Jog ON
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StopMove(short Axis)
        {
            bool sRtn = false;
            bHomeStop[Axis] = true;
            if (Axis < 16 && Axis > -1)
            {
                lock (lockObj)
                {
                    //APS168_W64.APS168.APS_set_axis_param_f(Axis, (int)APS_Define_W32.APS_Define.PRA_STP_DEC, 10000.0);
                    sRtn = APS168_W64.APS168.APS_stop_move(Axis) == 0 ? true : false;
                }
                return sRtn;
            }
            else
            {
                return sRtn;
            }
        }

        public bool ZeroAxisPos(short axis)
        {
            bool sRtn = false;
            if (axis < 16 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = APS168_W64.APS168.APS_set_command_f(axis, 0.0) == 0 ? true : false;
                    sRtn = APS168_W64.APS168.APS_set_position_f(axis, 0.0) == 0 ? true : false;
                    return sRtn;
                }
            }
            else
            {
                return sRtn;
            }
        }

        public void GetAllIOStatus()
        {
            if (!bInitOK)
            {
                return;
            }
            lock (lockObj)
            {
                try
                {
                    int __MAX_DO_CH = (24);
                    int __MAX_DI_CH = (24);

                    int digital_output_value = 0;
                    int digital_input_value = 0;

                    int i;

                    //****** Read digital output channels *****************************
                    APS168_W64.APS168.APS_read_d_output(usCardNo
                        , 0                     // I32 DO_Group
                        , ref digital_output_value // I32 *DO_Data
                    );

                    for (i = 0; i < __MAX_DO_CH; i++)
                        bBitOutputStatus[i] = ((digital_output_value >> i) & 1) == 1 ? true : false;

                    //**** Read digital input channels **********************************
                    APS168_W64.APS168.APS_read_d_input(usCardNo
                        , 0                    // I32 DI_Group
                        , ref digital_input_value // I32 *DI_Data
                    );

                    for (i = 0; i < __MAX_DI_CH; i++)
                        bBitInputStatus[i] = ((digital_input_value >> i) & 1) == 1 ? true : false;



                    //-----------------------------------
                    APS168_W64.APS168.APS_read_d_output(1
                        , 0                     // I32 DO_Group
                        , ref digital_output_value // I32 *DO_Data
                    );
                    int j = 0;
                    for (i = 24; i < 48; i++)
                    {
                        bBitOutputStatus[i] = ((digital_output_value >> j) & 1) == 1 ? true : false;
                        j++;
                    }

                    //**** Read digital input channels **********************************
                    APS168_W64.APS168.APS_read_d_input(1
                        , 0                    // I32 DI_Group
                        , ref digital_input_value // I32 *DI_Data
                    );
                    j = 0;
                    for (i = 24; i < 48; i++)
                    {
                        bBitInputStatus[i] = ((digital_input_value >> j) & 1) == 1 ? true : false;
                        j++;
                    }

                }
                catch (Exception)
                {

                }
            }
        }
        private void GetAllMotionStatus()
        {
            if (!bInitOK)
            {
                return;
            }
            double dValue = 0.000;
            int sRtn = 0;
            int axis_ID = 0;
            int io_Status = 0;
            lock (lockObj)
            {
                for (axis_ID = 0; axis_ID < v_total_axis; axis_ID++)
                {
                    try
                    {
                        //APS168_W64.APS168.APS_get_command_f(axis_ID, ref dValue);
                        APS168_W64.APS168.APS_get_position_f(axis_ID, ref dValue);

                        dCurrentPos[axis_ID] = dValue;
                        sRtn = APS168_W64.APS168.APS_motion_status(axis_ID);
                        io_Status = APS168_W64.APS168.APS_motion_io_status(axis_ID);
                    }
                    catch
                    {
                    }
                    if (((sRtn >> 5) & 0x1) == 0)
                    {
                        bAxisMoving[axis_ID] = true;
                    }
                    else
                    {
                        bAxisMoving[axis_ID] = false;
                    }

                    if ((io_Status & 0x1) == 0)
                    {
                        bAlarm[axis_ID] = false;
                    }
                    else
                    {
                        bAlarm[axis_ID] = true;
                    }

                    if (((io_Status >> 1) & 0x1) == 0)
                    {
                        bCWL[axis_ID] = false;
                    }
                    else
                    {
                        bCWL[axis_ID] = true;
                    }

                    if (((io_Status >> 2) & 0x1) == 0)
                    {
                        bCCWL[axis_ID] = false;
                    }
                    else
                    {
                        bCCWL[axis_ID] = true;
                    }

                    if (((io_Status >> 3) & 0x1) == 0)
                    {
                        bHome[axis_ID] = false;
                    }
                    else
                    {
                        bHome[axis_ID] = true;
                    }

                    if (((io_Status >> 4) & 0x1) == 0)
                    {
                        bEstop[axis_ID] = false;
                    }
                    else
                    {
                        bEstop[axis_ID] = true;
                    }
                }
            }
        }

        public bool StopJog(short Axis)
        {
            try
            {
                bool sRtn = APS168_W64.APS168.APS_jog_start(Axis, 0) == 0 ? true : false;
                return sRtn;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetOutBit(int iBit)
        {
            return bBitOutputStatus[iBit];
        }

        public bool SetOutBit(int iBit, bool bOn)
        {
            try
            {
                int do_Data = 0;
                if (bOn)
                    do_Data = 1;

                bool sRtn=false;
                if (iBit>=24)
                {
                    v_board_id = 1;
                    sRtn = APS168_W64.APS168.APS_write_d_channel_output(
                   1,
                   0,
                   iBit-24,
                   do_Data) == 0 ? true : false;
                }
                else
                {
                    v_board_id = 0;
                    sRtn = APS168_W64.APS168.APS_write_d_channel_output(
                    0,
                    0,
                    iBit,
                    do_Data) == 0 ? true : false;
                }
               
                return sRtn;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ArcMoveWithThroughAndEnd(short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double endPosX, double endPosY, double throughPosX, double throughPosY)
        {
            return true;
        }

        public bool GetInputBit(int iBit)
        {
            return bBitInputStatus[iBit];
        }

        public bool GantryMove(short gantryAxis1, short gantryAxis2, double dAcc, double dDec, double dSpeed, double endPos)
        {
            return true;
        }


        public bool ResetAxisAlarm()
        {
            return true;
        }


        public bool StopInterpolation()
        {
            return true;
        }

        public bool ResumeInterpolation()
        {
            return true;
        }


        public bool ComparePositionOut(int axis,double pos,out double different)
        {
            different = 0;
            return true;
        }


        public void PreWelding(int preWeldingNumber, List<double> preWeldingPos)
        {
            return;
        }


        public bool PreWeldingDone()
        {
            return true;
        }

        public bool StartManualPulserOperation(int axis_ID, int MultiplyingPower)
        {
            try
            {
                bool bRtn = false;
                bRtn = APS168_W64.APS168.APS_manual_pulser_start(0, 0)==0?true:false; // Disable pulser process
                bRtn = APS168_W64.APS168.APS_set_axis_param(axis_ID, 352, 2) == 0 ? true : false; // Set input mode: 0: 1xAB; 2: 4xAB 
                bRtn = APS168_W64.APS168.APS_set_axis_param(axis_ID, 353, 0) == 0 ? true : false; //Set logic: 0: InvPA = 0, InvPB = 0 
                bRtn = APS168_W64.APS168.APS_set_axis_param(axis_ID, 354, 0) == 0 ? true : false; // Set direction: 0: InvPA = 0, InvPB = 0 
                bRtn = APS168_W64.APS168.APS_set_axis_param_f(axis_ID, 355, 1) == 0 ? true : false; // Set ratio 
                bRtn = APS168_W64.APS168.APS_set_axis_param_f(axis_ID, 360, 123456) == 0 ? true : false; // Set acceleration 
                bRtn = APS168_W64.APS168.APS_set_axis_param_f(axis_ID, 361, 12345678) == 0 ? true : false; // Set jerk
                bRtn = APS168_W64.APS168.APS_manual_pulser_velocity_move(axis_ID, 12345) == 0 ? true : false; // Start velocity move 
                bRtn = APS168_W64.APS168.APS_manual_pulser_start(0, 1) == 0 ? true : false; // Enable pulser
                return bRtn;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StopManualPulserOperation(int axis_ID)
        {
            try
            {
                APS168_W64.APS168.APS_manual_pulser_start(0, 0);
                APS168_W64.APS168.APS_emg_stop(axis_ID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public double GT_GetAdc(short Cardnum, short adcout,out double dbRet)
        {
            throw new NotImplementedException();
        }
    }
}
