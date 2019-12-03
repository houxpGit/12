using System;
using WorldGeneralLib;
using System.Threading;
namespace ControlPlatformLib
{
    /*****************Googoltech MotionCard Information*****************
     *Vendor: Googoltech 固高科技
     *Type: MotionCard
     *Hardware Interface Type: PCI
     *Version: 1.0.1.0
     *Author：WuChenJie
     **************************************************************/
    //ModeMode 0   Stop 1 Jog  2 Abs Move 3   RelMove
    /// <summary>
    /// 固高运动控制卡
    /// </summary>
    public class GoogoTechMCard : HardWareBase, IMotionAction, IInputAction, IOutputAction
    {
        public short usCardNo = 0;

        private bool[] bAlarm = new bool[8];

        private bool[] bAxisMoving = new bool[8];

        private bool[] bAxisServo = new bool[8];

        //static bool bBoardClose = false;
        private bool[] bBitInputStatus = new bool[64];

        private bool[] bBitOutputStatus = new bool[64];
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
        public GoogoTechMCard()
        {
      
        }
         
        /// <summary>
        /// 绝对点位运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">速度</param>
        /// <param name="pos">位置</param>
        /// <returns></returns>
        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            short sRtn;
            gts.mc.TTrapPrm trapPrm;
            if (axis < 8 && axis > -1)
            {
                //lock (lockObj)
                //{
                    //sRtn = gts.mc.GT_SetCaptureMode(m_iCardNo, axis, gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = dAcc;
                    trapPrm.dec = dDec;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), dSpeed / 1000.0);
                    // 设置点位模式目标位置，即原点搜索距离
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), (int)(pos));
                    // 启动运动
                    dCommandTargetPos[axis] = pos;
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
             //   }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// XY轴圆弧插补运动
        /// </summary>
        /// <param name="num">坐标系编号</param>
        /// <param name="AxisX">X位置</param>
        /// <param name="AxisY">Y位置</param>
        /// <param name="AxisZ">Z位置</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <param name="dSpeed">速度</param>
        /// <param name="posX">目标X位置</param>
        /// <param name="posY">目标Y位置</param>
        /// <param name="dR">圆弧半径</param>
        /// <param name="iCCW">0:小圆周 1:大圆周</param>
        /// <returns></returns>
        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
        //   Global.logger.Info("---------------------------------");
          // Global.logger.Info("开始XY圆弧插补运动");
            BuildCor(num, CoordinateType.XY);
            for (int i=0;i<3;i++)
            {
                if (InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0) == true)//向缓冲区添加数据
                {
                    break;
                }
                else
                {
                }

            }        
            bool result = StartCure(num,false);
            if (result)
            {
            }
            // Global.logger.Info("XY圆弧插补运动完成");
            else
            {
            }
                //  Global.logger.Info("XY圆弧插补运动失败");
          return result;
        }

        /// <summary>
        /// 建立坐标系，只能使用1，2坐标系
        /// </summary>
        /// <param name="num">坐标系编号</param>
        /// <param name="coordinateType">坐标系类型</param>
        public bool  BuildCor(short num, CoordinateType coordinateType)
        {
          // Global.logger.Info("---------------------------------");
          //  Global.logger.InfoFormat("建立运动坐标系[{0}]，坐标系类型:{1}。", num, coordinateType);
            short sRtn;
            //iColAixsNo[num - 1, 0] = AxisX;
            //iColAixsNo[num - 1, 1] = AxisY;
            //iColAixsNo[num - 1, 2] = AxisZ;
            gts.mc.TCrdPrm crdPrm;

            lock (lockObj)
            {
                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, num, out crdPrm);
                switch (coordinateType)
                {
                    case CoordinateType.XY:
                        crdPrm.dimension = 2;                        // 建立二维的坐标系
                        crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
                        crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
                        crdPrm.profile3 = 0;                       // 规划器3对应到Z轴
                        break;
                    case CoordinateType.XZ:
                        crdPrm.dimension = 2;                        // 建立二维的坐标系
                        crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
                        crdPrm.profile2 = 0;                       // 规划器2对应到Y轴
                        crdPrm.profile3 = 3;                       // 规划器3对应到Z轴
                        break;
                    case CoordinateType.YZ:
                        crdPrm.dimension = 2;                        // 建立二维的坐标系
                        crdPrm.profile1 = 0;                       // 规划器1对应到X轴                       
                        crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
                        crdPrm.profile3 = 3;                       // 规划器3对应到Z轴
                        break;
                    case CoordinateType.XYZ:
                        crdPrm.dimension = 3;                        // 建立三维的坐标系
                        crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
                        crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
                        crdPrm.profile3 = 3;                       // 规划器3对应到Z轴
                        break;
                    default:
                        break;
                }

                crdPrm.synVelMax = 500;                      // 坐标系的最大合成速度是: 500 pulse/ms
                crdPrm.synAccMax = 2;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2
                crdPrm.evenTime = 0;                         // 坐标系的最小匀速时间为0

                crdPrm.profile4 = 0;
                crdPrm.profile5 = 0;
                crdPrm.profile6 = 0;
                crdPrm.profile7 = 0;
                crdPrm.profile8 = 0;
                crdPrm.setOriginFlag = 1;                    // 需要设置加工坐标系原点位置
                crdPrm.originPos1 = 0;                       // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
                crdPrm.originPos2 = 0;
                crdPrm.originPos3 = 0;
                crdPrm.originPos4 = 0;
                crdPrm.originPos5 = 0;
                crdPrm.originPos6 = 0;
                crdPrm.originPos7 = 0;
                crdPrm.originPos8 = 0;

                // 建立1号坐标系，设置坐标系参数
                sRtn = gts.mc.GT_SetCrdPrm(usCardNo, num, ref crdPrm);
                if (sRtn == 0)
                {
                    //Global.logger.Info("建立运动坐标系完成");
                    return true;
                }


                else
                {//   Global.logger.Info("建立运动坐标系失败");
                    return false;
                    }
              
            }
        }

        public void CheckHomeReturn(object obj)
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
                if (CheckHomeCatch(axis, out iPos))
                {
                    System.Threading.Thread.Sleep(100);
                    ZeroAxisPos(axis);
                    System.Threading.Thread.Sleep(100);
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(100);
            bHomeLast[axis] = true;
            while (IsMoving(axis))
            {
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(100);
            iPos = 0;
           // ReturnToHomePos(axis, iPos);
            System.Threading.Thread.Sleep(100);
            //判断运动完成
            //while (IsMoving(axis))
            //{
            //    System.Threading.Thread.Sleep(1);
            //}
            System.Threading.Thread.Sleep(100);
            ZeroAxisPos(axis);
            bHomeDone[axis] = true;
            bHomeing[axis] = false;
        }

        override public bool Close()
        {
            //if (bBoardClose == false)
            //{
            //    LTDMC.dmc_board_close();
            //    bBoardClose = true;
            //}
            StopMove(0);
            StopMove(1);
            StopMove(2);
            StopMove(3);
            StopMove(4);
            StopMove(5);
            StopMove(6);
            StopMove(7);
            gts.mc.GT_Close(usCardNo);
            return true;
        }
        //public bool CureMoveDoneIsrunFinish(short num, out int iStep, out int runFinish)
        //{
        //    short sRtn;
        //    short run;
        //    int segment;
        //    lock (lockObj)
        //    {
        //        sRtn = gts.mc.GT_CrdStatus(usCardNo, num, out run, out segment, 0);
        //        iStep = segment;
        //        runFinish = run;
        //        // iStep = segment;
        //    }
        //    if (run == 1)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
        public bool CureMoveDone(short num, out int iStep)
        {
            short sRtn;
            short run;
            int segment;

            lock (lockObj)
            {
                sRtn = gts.mc.GT_CrdStatus(usCardNo, num, out run, out segment, 0);
                iStep = run;
             
               // iStep = segment;
            }
            if (run == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
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

        public void GetAllIOStatus()
        {
            lock (lockObj)
            {
                int uiInput = 0;
                int uiOutput = 0;
                short sRtn;
                try
                {
                    sRtn = gts.mc.GT_GetDi(usCardNo, gts.mc.MC_GPI, out uiInput);
                    sRtn = gts.mc.GT_GetDo(usCardNo, gts.mc.MC_GPO, out uiOutput);
                }
                catch
                {
                }
                bBitInputStatus[0] = ((uiInput & 0x1) == 0) ? true : false;
                bBitInputStatus[1] = ((uiInput & 0x2) == 0) ? true : false;
                bBitInputStatus[2] = ((uiInput & 0x4) == 0) ? true : false;
                bBitInputStatus[3] = ((uiInput & 0x8) == 0) ? true : false;
                bBitInputStatus[4] = ((uiInput & 0x10) == 0) ? true : false;
                bBitInputStatus[5] = ((uiInput & 0x20) == 0) ? true : false;
                bBitInputStatus[6] = ((uiInput & 0x40) == 0) ? true : false;
                bBitInputStatus[7] = ((uiInput & 0x80) == 0) ? true : false;
                bBitInputStatus[8] = ((uiInput & 0x100) == 0) ? true : false;
                bBitInputStatus[9] = ((uiInput & 0x200) == 0) ? true : false;
                bBitInputStatus[10] = ((uiInput & 0x400) == 0) ? true : false;
                bBitInputStatus[11] = ((uiInput & 0x800) == 0) ? true : false;
                bBitInputStatus[12] = ((uiInput & 0x1000) == 0) ? true : false;
                bBitInputStatus[13] = ((uiInput & 0x2000) == 0) ? true : false;
                bBitInputStatus[14] = ((uiInput & 0x4000) == 0) ? true : false;
                bBitInputStatus[15] = ((uiInput & 0x8000) == 0) ? true : false;

                bBitOutputStatus[0] = ((uiOutput & 0x1) == 0) ? true : false;
                bBitOutputStatus[1] = ((uiOutput & 0x2) == 0) ? true : false;
                bBitOutputStatus[2] = ((uiOutput & 0x4) == 0) ? true : false;
                bBitOutputStatus[3] = ((uiOutput & 0x8) == 0) ? true : false;
                bBitOutputStatus[4] = ((uiOutput & 0x10) == 0) ? true : false;
                bBitOutputStatus[5] = ((uiOutput & 0x20) == 0) ? true : false;
                bBitOutputStatus[6] = ((uiOutput & 0x40) == 0) ? true : false;
                bBitOutputStatus[7] = ((uiOutput & 0x80) == 0) ? true : false;
                bBitOutputStatus[8] = ((uiOutput & 0x100) == 0) ? true : false;
                bBitOutputStatus[9] = ((uiOutput & 0x200) == 0) ? true : false;
                bBitOutputStatus[10] = ((uiOutput & 0x400) == 0) ? true : false;
                bBitOutputStatus[11] = ((uiOutput & 0x800) == 0) ? true : false;
                bBitOutputStatus[12] = ((uiOutput & 0x1000) == 0) ? true : false;
                bBitOutputStatus[13] = ((uiOutput & 0x2000) == 0) ? true : false;
                bBitOutputStatus[14] = ((uiOutput & 0x4000) == 0) ? true : false;
                bBitOutputStatus[15] = ((uiOutput & 0x8000) == 0) ? true : false;
            }
        }

        public void GetAllMotionStatus()
        {
            double dValue = 0.0;
            int lAxisStatus = 0;
            uint uIntClock;
            short sRtn;
            lock (lockObj)
            {
                for (ushort iAxis = 0; iAxis < 8; iAxis++)
                {
                    //uint uiCurrent=0;
                    try
                    {
                        gts.mc.GT_GetPrfPos(usCardNo, (short)(iAxis + 1), out dValue, 1, out uIntClock);
                     //  gts.mc.GT_GetAxisEncPos(usCardNo, (short)(iAxis + 1), out dValue, 1, out uIntClock);
                        dCurrentPos[iAxis] = dValue;
                        sRtn = gts.mc.GT_GetSts(usCardNo, (short)(iAxis + 1), out lAxisStatus, 1, out uIntClock);
                        if ((lAxisStatus & 0x1F2) != 0)
                        {
                            sRtn = gts.mc.GT_ClrSts(usCardNo, (short)(iAxis + 1), 1);
                        }
                    }
                    catch
                    {
                    }
                    if ((lAxisStatus & 0x400) != 0)
                    {
                        bAxisMoving[iAxis] = true;
                    }
                    else
                    {
                        bAxisMoving[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x2) != 0)
                    {
                        bAlarm[iAxis] = true;
                    }
                    else
                    {
                        bAlarm[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x20) != 0)
                    {
                        bCWL[iAxis] = true;
                    }
                    else
                    {
                        bCWL[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x40) != 0)
                    {
                        bCCWL[iAxis] = true;
                    }
                    else
                    {
                        bCCWL[iAxis] = false;
                    }
                }
                int lGpiValueHome;
                sRtn = gts.mc.GT_GetDi(usCardNo, gts.mc.MC_HOME, out lGpiValueHome);
                if ((lGpiValueHome & 0x1) != 0)
                {
                    bHome[0] = false;
                }
                else
                {
                    bHome[0] = true;
                }
                if ((lGpiValueHome & 0x2) != 0)
                {
                    bHome[1] = false;
                }
                else
                {
                    bHome[1] = true;
                }
                if ((lGpiValueHome & 0x4) != 0)
                {
                    bHome[2] = false;
                }
                else
                {
                    bHome[2] = true;
                }
                if ((lGpiValueHome & 0x8) != 0)
                {
                    bHome[3] = false;
                }
                else
                {
                    bHome[3] = true;
                }
                if ((lGpiValueHome & 0x10) != 0)
                {
                    bHome[4] = false;
                }
                else
                {
                    bHome[4] = true;
                }
                if ((lGpiValueHome & 0x20) != 0)
                {
                    bHome[5] = false;
                }
                else
                {
                    bHome[5] = true;
                }
                if ((lGpiValueHome & 0x40) != 0)
                {
                    bHome[6] = false;
                }
                else
                {
                    bHome[6] = true;
                }
                if ((lGpiValueHome & 0x80) != 0)
                {
                    bHome[7] = false;
                }
                else
                {
                    bHome[7] = true;
                }
            }
        }

        public double GetCurrentPos(short Axis)
        {
            if (Axis < 8 && Axis > -1)
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
            return false;
        }

        public bool GetHome(short Axis)
        {
            return bHome[Axis];
        }

        public bool GetInputBit(int iBit)
        {
            if (iBit < 128 && iBit > -1)
            {
                return bBitInputStatus[iBit];
            }
            else
            {
                return false;
            }
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
            if (iBit < 128 && iBit > -1)
            {
                return bBitOutputStatus[iBit];
            }
            else
            {
                return false;
            }
        }

        public bool GetServoOn(short Axis)
        {
            if (Axis < 8 && Axis > -1)
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
            short sRtn;
            double dVel;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_GetVel((short)usCardNo, (short)(Axis + 1), out dVel);//设置目标速度
                    return dVel;
                }
            }
            else
            {
                return 0.0;
            }
        }

        public bool HandleErrorMessage(short errorMessage)
        {
            if (errorMessage != 0)
            {
                return false;
            }
            return true;
        }

        override public bool Init(HardWareInfoBase infoHardWare)
        {
            GoogoTechMCInfo googoTechMCInfo = infoHardWare as GoogoTechMCInfo;
      //      Global.logger.InfoFormat("初始化固高运动控制卡,卡名称{0}", googoTechMCInfo.hardwareName);
            usCardNo = (short)googoTechMCInfo.iCardNo;
            short sRtn = 0;
            sRtn = gts.mc.GT_Open((short)usCardNo, 0, 0);
            if (!HandleErrorMessage(sRtn))
            {
          //      Global.logger.ErrorFormat("初始化固高运动控制卡{0}失败", googoTechMCInfo.hardwareName);
                bInitOK = false;
                return false;
            }
            //sRtn = gts.mc.GT_Stop(0xFF, 0);

            sRtn = gts.mc.GT_Reset((short)usCardNo);
            sRtn = gts.mc.GT_LoadConfig((short)usCardNo, googoTechMCInfo.m_strConfigPath);
            sRtn = gts.mc.GT_ClrSts((short)usCardNo, 1, 8);//清除轴报警和限位
            bInitOK = true;

            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();

            BuildCor(1, CoordinateType.XY);
            return true;
        }

        /// <summary>
        /// XY 平面圆弧插补。 以终点位置和半径为输入参数平面圆弧插补。
        /// </summary>
        /// <param name="num">正整数，取值范围： [1,2] 。</param>
        /// <param name="dPosX">圆弧插补x轴的终点坐标值。取值范围[-1073741823,1073741823]</param>
        /// <param name="dPosY">圆弧插补y轴的终点坐标值。取值范围[-1073741823,1073741823]</param>
        /// <param name="dR">圆弧插补的半径值。取范围：[-1073741823,1073741823]。半径为正时，表示圆弧小于等于180°的圆弧。半径为负时，表示圆弧大于180°的圆弧。</param>
        /// <param name="dSpeed">插补段的目标合成速度。取值范围： [0 , 32767]  单位： pulse/ms。</param>
        /// <param name="iCCW">圆弧旋转方向。0：顺时针 1：逆时针</param>
        /// <param name="dAcc">插补段的合成加速度。取值范围： [0 , 32767]，单位： pulse/ms 2</param>
        /// <param name="dEndSpeed">插补 段的终点速度。取值范围： [0 , 32767] ， 单位： pulse/ms。该值只有在没使用 前瞻预处理功能时才有意义，否则该值无效。默认值为： 0</param>
        public bool InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {
            //Global.logger.Info("---------------------------------");
            //Global.logger.Info("插入XY平面圆弧插补段");
            short sRtn;
            if (num == 1)
            {
                lock (lockObj)
                {
                    gts.mc.TCrdPrm crdPrm;
                    sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
                    int iTargetPosX = (int)(dPosX);
                    int iTargetPosY = (int)(dPosY);
                    int iR = (int)(dR);
                    sRtn = gts.mc.GT_ArcXYC(usCardNo,
                            1,
                            iTargetPosX, iTargetPosY,
                            iR, 0,
                           
                            iCCW,
                            dSpeed / 10,
                            dAcc,
                            dEndSpeed / 10,
                            0);
                }
            }
            else
            {
                lock (lockObj1)
                {
                    gts.mc.TCrdPrm crdPrm1;
                    sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 2, out crdPrm1);
                    int iTargetPosX = (int)(dPosX);
                    int iTargetPosY = (int)(dPosY);
                    int iR = (int)(int)(dR);
                    sRtn = gts.mc.GT_ArcXYR(usCardNo,
                            2,
                            iTargetPosX, iTargetPosY,
                            iR,
                            iCCW,
                            dSpeed / 1000.0,
                            dAcc,
                            dEndSpeed / 1000.0,
                            0);
                }
            }

            if (sRtn == 0)
            {   //     Global.logger.Info("插入XY平面圆弧插补段完成");
            return true;}
         
            else
            { //  Global.logger.Info("插入XY平面圆弧插补段失败");
            return false;
}
              
        }

        private object lockObj1 = new object();
        public void InsertArc(double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed, int num)
        {
        //    Global.logger.Info("---------------------------------");
        //    Global.logger.Info("插入XY平面圆弧插补段");
            short sRtn;
            if (num == 0)
            {
                lock (lockObj)
                {

                    gts.mc.TCrdPrm crdPrm;
                    sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
                    int iTargetPosX = (int)(dPosX);
                    int iTargetPosY = (int)(dPosY);
                    int iR = (int)(int)(dR);
                    sRtn = gts.mc.GT_ArcXYR(usCardNo,
                            1,
                            iTargetPosX, iTargetPosY,
                            iR,
                            iCCW,
                            dSpeed / 1000.0,
                            dAcc,
                            dEndSpeed / 1000.0,
                            0);
                }
            }
            else
            {
                lock (lockObj1)
                {
                    gts.mc.TCrdPrm crdPrm1;
                    sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 2, out crdPrm1);
                    int iTargetPosX = (int)(dPosX);
                    int iTargetPosY = (int)(dPosY);
                    int iR = (int)(int)(dR);
                    sRtn = gts.mc.GT_ArcXYR(usCardNo,
                            2,
                            iTargetPosX, iTargetPosY,
                            iR,
                            iCCW,
                            dSpeed / 1000.0,
                            dAcc,
                            dEndSpeed / 1000.0,
                            0);
                }
            }

            if (sRtn == 0)
            { }
               // Global.logger.Info("插入XY平面圆弧插补段完成");
            else
            { }
              //  Global.logger.Info("插入XY平面圆弧插补段失败");
        }

        public bool  InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {
         //   Global.logger.Info("---------------------------------");
         //   Global.logger.Info("插入XY平面直线插补段");
            short sRtn;
           // gts.mc.GT_CrdClear(0,1,0);
            lock (lockObj)
            {
                gts.mc.TCrdPrm crdPrm;
                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
                int iTargetPosX = (int)(dPosX);
                int iTargetPosY = (int)(dPosY);
                //int iR = (int)(int)(dR);
                sRtn = gts.mc.GT_LnXY(
                    usCardNo,
                    1,                       // 该插补段的坐标系是坐标系1
                    iTargetPosX, iTargetPosY, // 该插补段的终点坐标(X, Y)
                    dSpeed / 1000.0,         // 该插补段的目标速度：dSpeed pulse/ms
                    dAcc,                    // 插补段的加速度：dAcc pulse/ms^2
                    dEndSpeed / 1000.0,      // 终点速度
                    0);                      // 向坐标系1的FIFO0缓存区传递该直线插补数据
            }

            if (sRtn == 0)
            {  //Global.logger.Info("插入XY平面直线插补段完成");
                return true;
            }

              
            else
            { //   Global.logger.Info("插入XY平面直线插补段失败");
                return false;
            }
            
        }

        public bool IsMoveDone(short Axis)
        {
            uint uIntClock;

            int lCurrentPos = 0;

            double dValue = 0.0;
            short sRtn;

            bool bResult;
            if (bAxisMoving[Axis])
            {
                bResult = false;
            }
            else
            {
                lock (lockObj)
                {
                   // sRtn = gts.mc.GT_GetPrfPos((short)usCardNo, (short)(Axis + 1), out dValue, 1, out uIntClock);
                   
                    sRtn = gts.mc.GT_GetAxisEncPos((short)usCardNo, (short)(Axis + 1), out dValue, 1, out uIntClock);
                }
                lCurrentPos = (int)dValue;

                double dDiffValue = Math.Abs(dCommandTargetPos[Axis] - lCurrentPos);//
                if (dDiffValue < 100)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            return bResult;
        }

        public bool IsMoving(short Axis)
        {
            if (Axis < 8 && Axis > -1)
            {
                return bAxisMoving[Axis];
            }
            else
            {
                return false;
            }
        }

        public bool JogMove(short Axis, double dAcc, double dDec, double dVel)
        {
            short sRtn;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    Axis++;
                    gts.mc.TJogPrm jog = new gts.mc.TJogPrm();
                    jog.acc = dAcc;
                    jog.dec = dDec;

                    sRtn = gts.mc.GT_ClrSts((short)usCardNo, Axis, 8);//清除轴报警和限位
                    sRtn = gts.mc.GT_PrfJog((short)usCardNo, Axis);//设置为jog模式
                    sRtn = gts.mc.GT_SetJogPrm((short)usCardNo, Axis, ref jog);//设置jog运动参数
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, Axis, dVel / 1000.0);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (Axis - 1));//更新轴运动
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        bool BuildCorFinish = false;
        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            gts.mc.TCrdPrm crdPrm;

          
            short sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
            if(crdPrm.dimension==0)
            {
                BuildCorFinish = false;
            }
            for (int i=0;i<3;i++)
            {
                if (BuildCorFinish == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    BuildCorFinish = BuildCor(num, CoordinateType.XY);
                }
            }  
            gts.mc.GT_CrdClear(0, 1, 0);     
           bool  addFinish = InsertLine(num, posX, posY, posZ, dSpeed, dAcc, 0);
            for (int i = 0; i < 3; i++)
            {
                if (addFinish == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    addFinish = InsertLine(num, posX, posY, posZ, dSpeed, dAcc, 0);
                }
            }
            //StartCure(num,false);
            return addFinish;
        }
        public bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType cordinateType)
        {
            gts.mc.GT_CrdClear(0, 1, 0);
            // BuildCor(num, cordinateType);   
            bool addFinish = InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
            for (int i = 0; i < 3; i++)
            {
                if (addFinish == true)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    addFinish = InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
                }
            }
            //StartCure(num,false);
            return addFinish;
         
          //  StartCure(num, false);
           
        }
        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            short sRtn;
            gts.mc.TTrapPrm trapPrm;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    //sRtn = gts.mc.GT_SetCaptureMode(m_iCardNo, axis, gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = dAcc;
                    trapPrm.dec = dDec;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), dSpeed / 1000.0);
                    // 设置点位模式目标位置，即原点搜索距离

                    uint uIntClock;
                    int lCurrentPos = 0;
                    double dValue = 0.0;
                    sRtn = gts.mc.GT_GetPrfPos((short)usCardNo, (short)(axis + 1), out dValue, 1, out uIntClock);
                   // sRtn = gts.mc.GT_GetAxisEncPos((short)usCardNo, (short)(axis + 1), out dValue, 1, out uIntClock);
                    lCurrentPos = (int)dValue;

                    int iTargetPos = (int)(lCurrentPos + pos);
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), iTargetPos);
                    // 启动运动
                    dCommandTargetPos[axis] = iTargetPos;
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << (axis));
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ServoOff(short axis)
        {
            short sRtn;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_AxisOff((short)usCardNo, (short)(axis + 1));
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ServoOn(short axis)
        {
            short sRtn;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_AxisOn((short)usCardNo, (short)(axis + 1));
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool SetAlarmOff(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_AlarmOff(usCardNo, (short)(axis + 1));
            }
            return true;
        }

        public bool SetAlarmOn(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_AlarmOn(usCardNo, (short)(axis + 1));
            }
            return true;
        }

        public bool SetAxisPos(short axis, double dPos)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    gts.mc.GT_SetEncPos((short)usCardNo, (short)(axis + 1), (int)dPos);
                    short rtn = gts.mc.GT_SetPos((short)usCardNo, (short)(axis + 1), (int)dPos);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool SetHomeOff(short axis)
        {
            lock (lockObj)
            {
                //LTDMC.dmc_set_home_pin_logic(usCardNo, (ushort)axis, 1, 0);
                return true;
            }
        }

        public bool SetHomeOn(short axis)
        {
            lock (lockObj)
            {
                //gts.mc.gt_s;
                return true;
            }
        }

        public bool SetLimtDisable(short axis)
        {
            lock (lockObj)
            {
                //LTDMC.dmc_set_el_mode(usCardNo, (ushort)axis, 0, 1, 0);
                return true;
            }
        }

        public bool SetLimtOff(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_LmtsOff((short)usCardNo, (short)(axis + 1), -1);
                return true;
            }
        }

        public bool SetLimtOn(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_LmtsOn((short)usCardNo, (short)(axis + 1), -1);
                return true;
            }
        }

        public bool SetNearHomeOff(short axis)
        {
            return true;
        }

        public bool SetNearHomeOn(short axis)
        {
            return true;
        }

        public bool SetOutBit(int iBit, bool bOn)
        {
            short sRtn;
            lock (lockObj)
            {
            }
            if (iBit < 128 && iBit > -1)
            {
                iBit++;
                lock (lockObj)
                {
                    short uValue = bOn ? (short)0 : (short)1;
                    sRtn = gts.mc.GT_SetDoBit(usCardNo, gts.mc.MC_GPO, (short)iBit, uValue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetPulseMode(short axis, PulseMode plm)
        {
            lock (lockObj)
            {
                if (plm == PulseMode.PLDI)
                {
                    gts.mc.GT_StepDir(usCardNo, (short)(axis + 1));
                }
                else
                {
                    gts.mc.GT_StepPulse(usCardNo, (short)(axis + 1));
                }
            }
            return true;
        }

        public bool SetVel(short Axis, double dVel)
        {
            short sRtn;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, (short)(Axis + 1), dVel);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (Axis));//更新轴运动
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public double _GetAdc(short Cardnum,short adcout ,double dbRet)
        {
            uint clk;
            //double dbRet = 0.0;
            short rtn = gts.mc.GT_GetAdc(Cardnum, adcout, out dbRet, 1, out clk);
            return dbRet;
        }
      
        public bool StartCure(short num, bool bPreWelding)
        {
            short sRtn;
            lock (lockObj)
            {
                sRtn = gts.mc.GT_CrdStart(usCardNo, num, 0);
                if (sRtn == 0)
                    return true;
                else
                    return false;
            }
        }

        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            short sRtn;
            if (bHomeing[axis] == false)
            {
                gts.mc.TTrapPrm trapPrm;
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_SetCaptureMode(usCardNo, (short)(axis + 1), gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = 0.25;
                    trapPrm.dec = 0.25;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), Math.Abs(dHomeSpd / 1000.0));
                    // 设置点位模式目标位置，即原点搜索距离
                    int dSearchDis = 0;
                    if (dHomeSpd > 0)
                        dSearchDis = 999999999;
                    else
                        dSearchDis = -999999999;
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), dSearchDis);
                    // 启动运动
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
                }
                bHomeStop[axis] = false;
                bHomeDone[axis] = false;
                bHomeing[axis] = true;
                bHomeLast[axis] = false;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(CheckHomeReturn));
                thread.IsBackground = true;
                thread.Start(axis);
            }
            return true;
        }

        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            short sRtn;
            bHomeStop[axis] = false;
            bHomeDone[axis] = false;
            bHomeLast[axis] = false;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    axis++;
                    gts.mc.TJogPrm jog = new gts.mc.TJogPrm();
                    jog.acc = dAcc;
                    jog.dec = dDec;

                    sRtn = gts.mc.GT_ClrSts((short)usCardNo, axis, 1);//清除轴报警和限位
                    sRtn = gts.mc.GT_PrfJog((short)usCardNo, axis);//设置为jog模式
                    sRtn = gts.mc.GT_SetJogPrm((short)usCardNo, axis, ref jog);//设置jog运动参数
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, axis, dCatchSpeed / 1000.0);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (axis - 1));//更新轴运动
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
            bHomeStop[Axis] = true;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    //if (bHomeLast[Axis] == false)
                    //{
                    gts.mc.GT_Stop((short)usCardNo, 1 << (Axis), 0);
                    // }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ZeroAxisPos(short axis)
        {
            short sRtn;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_ZeroPos((short)usCardNo, (short)(axis + 1), 1);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckHomeCatch(short axis, out int pos)
        {
            short sRtn;
            short capture;
            uint clk;
            lock (lockObj)
            {
                // 读取捕获状态
                sRtn = gts.mc.GT_GetCaptureStatus(usCardNo, (short)(axis + 1), out capture, out pos, 1, out clk);
                if (capture == 1)
                {
                    gts.mc.GT_Stop(usCardNo, 1 << (axis), 0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void ReturnToHomePos(short axis, int pos)
        {
            short sRtn;

            lock (lockObj)
            {
                //运动到"捕获位置+偏移量"
                sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), pos);
                HandleErrorMessage(sRtn);
                // 在运动状态下更新目标位置
                sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
                HandleErrorMessage(sRtn);
            }
        }

        private void ScanThreadFunction()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            while (true)
            {
                System.Threading.Thread.Sleep(10);
                //if (Global.bClosing)
                //    break;
                GetAllMotionStatus();
                GetAllIOStatus();
            }
        }

     

        public bool  BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            return true;
            throw new NotImplementedException();
        }

        public void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py)
        {
            short str;
            gts.mc.GT_2DCompareClear(usCardNo, chn);
            gts.mc.T2DCompareData[] Databuf = new gts.mc.T2DCompareData[1];
            gts.mc.T2DComparePrm Prm = new gts.mc.T2DComparePrm();
            str = gts.mc.GT_2DCompareMode(usCardNo, chn, 1);
            //设置参数
            Prm.encx = 1; // X 轴为轴
            Prm.ency = 2; // Y 轴为轴
            Prm.maxerr = maxerr; // 最大误差Pulse
            Prm.outputType = outputType; // 输出类型脉冲
            Prm.source = 1; // 比较源规划器
            Prm.startLevel = 0; // 无效参数
            Prm.threshold = 1; // 最优点计算阈值
            Prm.time = 500; // 脉冲宽度us
            str = gts.mc.GT_2DCompareSetPrm(usCardNo, chn, ref Prm);
            //设置数据在XY轴位置达到设定位置时通道HSIO0输出脉冲。
            //设置数据在XY轴位置达到设定位置时通道HSIO0输出脉冲。
            Databuf[0].px = (int)px;
            Databuf[0].py = (int)py;
            str = gts.mc.GT_2DCompareData(usCardNo, chn, (short)Databuf.Length, ref Databuf[0], 0);
            str = gts.mc.GT_2DCompareStart(usCardNo, chn);
            str = gts.mc.GT_SetComparePort(usCardNo, chn, output_1, output_2);

        }

        public bool StopJog(short Axis)
        {
            bHomeStop[Axis] = true;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    //if (bHomeLast[Axis] == false)
                    //{
                    gts.mc.GT_Stop((short)usCardNo, 1 << (Axis), 0);
                    gts.mc.GT_PrfTrap(usCardNo, (short)(Axis + 1));
                    // }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ArcMoveWithThroughAndEnd(short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double endPosX, double endPosY, double throughPosX, double throughPosY)
        {
            throw new NotImplementedException();
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
            return true;
        }

        public bool ResumeInterpolation()
        {
            throw new NotImplementedException();
        }


        public bool ComparePositionOut(int axis,double pos,out double different)
        {
            throw new NotImplementedException();
        }


        public void PreWelding(int preWeldingNumber, System.Collections.Generic.List<double> preWeldingPos)
        {
            throw new NotImplementedException();
        }


        public bool PreWeldingDone()
        {
            throw new NotImplementedException();
        }

        public bool StartManualPulserOperation(int axis_ID, int MultiplyingPower)
        {
            bool blFlag = false;        //内部状态
            try
            {
                gts.mc.GT_EndHandwheel(0, (short)axis_ID);
                short ret = -1;             
                ret = gts.mc.GT_HandwheelInit(0);
                ret += gts.mc.GT_SetHandwheelStopDec(0, (short)axis_ID, 0.5, 0.5);
                ret += gts.mc.GT_StartHandwheel(0, (short)axis_ID, 11, 1, (short)MultiplyingPower, 1, 0.5, 0.5, 100, 200);

                if (ret != 0)
                {
                    blFlag = false;
                    ret = gts.mc.GT_EndHandwheel(0, (short)axis_ID);
                    if (ret != 0)
                    {

                    }
                }
                else
                {
                    blFlag = true;
                }
           
            }
            catch (Exception)
            {
                blFlag = false;
                gts.mc.GT_EndHandwheel(0, (short)axis_ID);
            }
            return blFlag;
        }

        public bool StopManualPulserOperation(int axis_ID)
        {
            short ret = -1;
            bool blFlag = false;

            ret = gts.mc.GT_EndHandwheel(0, (short)axis_ID);
            if (ret != 0)
            {
                blFlag = false;
            }
            else
            {
                blFlag = true;
            }
            return blFlag;
        }

        public double GT_GetAdc(short Cardnum, short adcout,out  double dbRet)
        {
            uint clk;
            //double dbRet = 0.0;
            short rtn = gts.mc.GT_GetAdc(usCardNo, adcout, out dbRet, 1, out clk);
            return dbRet;

        }
    }
}