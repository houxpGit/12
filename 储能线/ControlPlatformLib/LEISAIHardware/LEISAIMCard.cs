using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    /********Leadtech MotionCard Information/********
    *Vendor: Leadtech 雷塞
    *Type: MotionCard
    *Hardware Interface Type: PCI
    *Version: 1.0.1.0
    *Author：WuChenJie
    ***********************************************/
    //ModeMode 0   Stop 1 Jog  2 Abs Move 3   RelMove
    public class LEISAIMCard : HardWareBase, IMotionAction, IInputAction, IOutputAction
    {
        static int nCardTotal;
        //static bool bBoardClose = false;
        private bool[] bBitInputStatus = new bool[64];
        private bool[] bBitOutputStatus = new bool[64];

        private bool[] bAxisServo = new bool[8];
        private bool[] bAxisMoving = new bool[8];
        private bool[] bHome = new bool[8];
        private bool[] bAlarm = new bool[8];
        private bool[] bCWL = new bool[8];
        private bool[] bCCWL = new bool[8];

        private double[] dCurrentPos = new double[8];
        private double[] dTargetPos = new double[8];
        private double[] dCurrentVel = new double[8];


        public ushort usCardNo = 0;
        private int[] iMoveMode = new int[8];

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
        public bool GetHome(short Axis)
        {
            return bHome[Axis];
        }
        public bool GetAlarm(short Axis)
        {
            return bAlarm[Axis];
        }
        public bool GetLimtCW(short Axis)
        {
            return bCWL[Axis];
        }
        public bool GetLimtCCW(short Axis)
        {
            return bCCWL[Axis];
        }
        public bool GetEstop(short Axis)
        {
            return false;
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
        public bool IsMoveDone(short Axis)
        {
            bool bResult;
            if (bAxisMoving[Axis])
            {
                bResult = false;
            }
            else
            {
                lock (lockObj)
                {
                    dTargetPos[Axis] = LTDMC.dmc_get_target_position(usCardNo, (ushort)Axis);
                }
                if (dCurrentPos[Axis] == dTargetPos[Axis] && (!bAlarm[Axis]) && (!bAxisMoving[Axis]))
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
        public bool SetVel(short Axis, double dVel)
        {
            lock (lockObj)
            {
                if (Axis < 8 && Axis > -1)
                {
                    LTDMC.dmc_change_speed(usCardNo, (ushort)Axis, dVel, 0.0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public double GetVel(short Axis)
        {

            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    return LTDMC.dmc_read_current_speed(usCardNo, (ushort)Axis);
                }
            }
            else
            {
                return 0.0;
            }

        }
        public bool JobMove(short Axis, double dAcc, double dDec, double dVel)
        {
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    ushort iDir = 0;

                    LTDMC.dmc_set_profile(usCardNo, (ushort)Axis, 0.0, Math.Abs(dVel), dAcc, dDec, 0);
                    if (dVel < 0)
                    {
                        iDir = 0;
                    }
                    else
                    {
                        iDir = 1;
                    }
                    LTDMC.dmc_vmove(usCardNo, (ushort)Axis, iDir);

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
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_stop(usCardNo, (ushort)Axis, 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HandleErrorMessage(short errorMessage)
        {
            return true;
        }
        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            lock (lockObj)
            {
                ushort iDir = 0;
                double dVel = Math.Abs(dCatchSpeed);
                LTDMC.dmc_set_profile(usCardNo, (ushort)axis, 0.0, dVel, dAcc, dDec, 0);
                if (dCatchSpeed < 0)
                {
                    iDir = 0;
                }
                else
                {
                    iDir = 1;
                }
                LTDMC.dmc_vmove(usCardNo, (ushort)axis, iDir);
            }
            return true;
        }
        public bool FinishSearchLimit(short axis)
        {
            return bCCWL[axis] || bCWL[axis];
        }
        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            lock (lockObj)
            {
                ushort iDir = 0;
                double dVel = Math.Abs(dHomeSpd);
                LTDMC.dmc_set_profile(usCardNo, (ushort)axis, 0.0, dVel, dAcc, dDec, 0);
                if (dHomeSpd < 0)
                {
                    iDir = 0;
                }
                else
                {
                    iDir = 1;
                }
                LTDMC.dmc_set_homemode(usCardNo, (ushort)axis, iDir, dVel, 2, 0);
                LTDMC.dmc_home_move(usCardNo, (ushort)axis);
            }
            return true;
        }
        public bool FinishSearchHome(short axis)
        {
            return !bAxisMoving[axis] && bHome[axis];
        }
        public bool SetAxisPos(short axis, double dPos)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_position(usCardNo, (ushort)axis, (int)dPos);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ZeroAxisPos(short axis)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_position(usCardNo, (ushort)axis, 0);
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
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_write_sevon_pin(usCardNo, (ushort)axis, 0);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        public bool ServoOff(short axis)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_write_sevon_pin(usCardNo, (ushort)axis, 1);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool SetLimtOn(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(usCardNo, (ushort)axis, 1, 0, 0);
                return true;
            }
        }
        public bool SetLimtOff(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(usCardNo, (ushort)axis, 1, 1, 0);
                return true;
            }
        }
        public bool SetLimtDisable(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(usCardNo, (ushort)axis, 0, 1, 0);
                return true;
            }
        }
        public bool SetHomeOn(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_home_pin_logic(usCardNo, (ushort)axis, 0, 0);
                return true;
            }
        }
        public bool SetHomeOff(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_home_pin_logic(usCardNo, (ushort)axis, 1, 0);
                return true;
            }
        }
        public bool SetNearHomeOn(short axis)
        {
            return true;
        }
        public bool SetNearHomeOff(short axis)
        {
            return true;
        }
        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_profile(usCardNo, (ushort)axis, 0.0, dSpeed, dAcc, dDec, 0);
                    LTDMC.dmc_pmove(usCardNo, (ushort)axis, (int)pos, 1);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_profile(usCardNo, (ushort)axis, 0.0, dSpeed, dAcc, dDec, 0);
                    LTDMC.dmc_pmove(usCardNo, (ushort)axis, (int)pos, 0);
                }
                return true;
            }
            else
            {
                return false;
            }
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
        public bool SetOutBit(int iBit, bool bOn)
        {
            if (iBit < 128 && iBit > -1)
            {
                lock (lockObj)
                {
                    ushort uValue = bOn ? (ushort)0 : (ushort)1;
                    short sValue = LTDMC.dmc_write_outbit(usCardNo, (ushort)iBit, uValue);
                }
                return true;
            }
            else
            {
                return false;
            }
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
        override public bool Init(HardWareInfoBase infoHardWare)
        {
            LEISAIMCInfo tempInfo = (LEISAIMCInfo)infoHardWare;
            if (nCardTotal > 0)
            {
                if (nCardTotal >= tempInfo.iCardNo)
                {
                    bInitOK = true;
                }
                else
                {
                    bInitOK = false;
                }
            }
            else
            {
                nCardTotal = LTDMC.dmc_board_init();
                if (nCardTotal <= 0)//控制卡初始化
                {
                    bInitOK = false;
                    return false;
                }
                if (nCardTotal >= tempInfo.iCardNo)
                {
                    bInitOK = true;
                }
                else
                {
                    bInitOK = false;
                }
            }
            usCardNo = (ushort)tempInfo.iCardNo;
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            while (true)
            {
                System.Threading.Thread.Sleep(10);
                if (MainModule.FormMain.bClosing)
                    break;
                GetAllMotionStatus();
                GetAllIOStatus();


            }
        }
        public void GetAllMotionStatus()
        {
            lock (lockObj)
            {
                for (ushort iAxis = 0; iAxis < 8; iAxis++)
                {
                    uint uiCurrent = 0;
                    try
                    {
                        dTargetPos[iAxis] = LTDMC.dmc_get_target_position(usCardNo, iAxis);
                        dCurrentPos[iAxis] = LTDMC.dmc_get_position(usCardNo, iAxis);

                        if (LTDMC.dmc_check_done(usCardNo, iAxis) == 1)
                        {
                            bAxisMoving[iAxis] = false;
                        }
                        else
                        {
                            bAxisMoving[iAxis] = true;
                        }
                        uiCurrent = LTDMC.dmc_axis_io_status(usCardNo, iAxis);
                    }
                    catch
                    {

                    }

                    if ((uiCurrent & 0x1) == 0)
                    {
                        bAlarm[iAxis] = false;
                    }
                    else
                    {
                        bAlarm[iAxis] = true;
                    }
                    if ((uiCurrent & 0x2) == 0)
                    {
                        bCWL[iAxis] = false;
                    }
                    else
                    {
                        bCWL[iAxis] = true;
                    }
                    if ((uiCurrent & 0x4) == 0)
                    {
                        bCCWL[iAxis] = false;
                    }
                    else
                    {
                        bCCWL[iAxis] = true;
                    }
                    if ((uiCurrent & 0x10) == 0)
                    {
                        bHome[iAxis] = false;
                    }
                    else
                    {
                        bHome[iAxis] = true;
                    }
                }
            }
        }
        public void GetAllIOStatus()
        {
            lock (lockObj)
            {
                uint uiInput = 0;
                uint uiOutput = 0;
                try
                {
                    uiInput = LTDMC.dmc_read_inport(usCardNo, 0);
                    uiOutput = LTDMC.dmc_read_outport(usCardNo, 0);
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
        public bool SetAlarmOn(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_alm_mode(usCardNo, (ushort)axis, 1, (ushort)0, (ushort)0);
            }
            return true;
        }
        public bool SetAlarmOff(short axis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_alm_mode(usCardNo, (ushort)axis, 1, (ushort)1, (ushort)0);
            }
            return true;
        }
        public bool SetPulseMode(short axis, PulseMode plm)
        {
            lock (lockObj)
            {
                if (plm == PulseMode.PLDI)
                {
                    LTDMC.dmc_set_pulse_outmode(usCardNo, (ushort)axis, 0);
                }
                else
                {
                    LTDMC.dmc_set_pulse_outmode(usCardNo, (ushort)axis, 4);
                }
            }
            return true;
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
            return true;
        }
        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            return true;
        }
        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            BuildCor(num, AxisX, AxisY, AxisZ);
            InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
            StartCure(num,false);
            return true;
        }
        public bool BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            return true;

        }
        public bool  InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {
            return true;


        }
        public bool InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {
            return true;

        }
        public void StartCure(short num, bool bPreWelding)
        {

        }
        public bool CureMoveDone(short num, out int iStep)
        {
            iStep = 0;
            return true;


        }

        public bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType interpolationAxisType)
        {
            throw new NotImplementedException();
        }

        public bool BuildCor(short num, CoordinateType interpolationAxisType)
        {
            throw new NotImplementedException();
        }

        public void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py)
        {
            throw new NotImplementedException();
        }

        public bool JogMove(short Axis, double dAcc, double dDec, double dVel)
        {
            throw new NotImplementedException();
        }

        public bool StopJog(short Axis)
        {
            throw new NotImplementedException();
        }

        bool IMotionAction.StartCure(short num, bool bPreWelding)
        {
            throw new NotImplementedException();
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

        public double GT_GetAdc(short Cardnum, short adcout,out double dbRet)
        {
            throw new NotImplementedException();
        }
    }
}
