using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    //ModeMode 0   Stop 1 Jog  2 Abs Move 3   RelMove
    public class AdvanceMCard : HardWareBase, IMotionAction, IInputAction, IOutputAction
    {
        private bool[] bBitInputStatus = new bool[64];
        private bool[] bBitOutputStatus = new bool[64];

        private bool[] bAxisServo = new bool[8];
        private bool[] bAxisMoving = new bool[8];

        private double[] dCurrentPos = new double[8];
        private double[] dTargetPos = new double[8];
        private double[] dCurrentVel = new double[8];

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
            return true;
        }
        public bool GetAlarm(short Axis)
        {
            return false;
        }
        public bool GetLimtCW(short Axis)
        {
            return false;
        }
        public bool GetLimtCCW(short Axis)
        {
            return false;
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
                if (dCurrentPos[Axis] == dTargetPos[Axis])
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

            if (Axis < 8 && Axis > -1)
            {
                dCurrentVel[Axis] = dVel;
                return true;
            }
            else
            {
                return false;
            }
        }
        public double GetVel(short Axis)
        {
            if (Axis < 8 && Axis > -1)
            {
                return dCurrentVel[Axis];
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
                iMoveMode[Axis] = 1;
                dCurrentVel[Axis] = dVel;
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
                iMoveMode[Axis] = 0;
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
            return true;
        }
        public bool FinishSearchLimit(short axis)
        {
            return true;
        }
        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            return true;
        }
        public bool FinishSearchHome(short axis)
        {
            return true;
        }
        public bool SetAxisPos(short axis, double dPos)
        {
            if (axis < 8 && axis > -1)
            {
                dCurrentPos[axis] = dPos;
                return true;
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
                dCurrentPos[axis] = 0.0;
                return true;
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
                bAxisServo[axis] = true;
                return true;
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
                bAxisServo[axis] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetLimtOn(short axis)
        {
            return true;
        }
        public bool SetLimtOff(short axis)
        {
            return true;
        }
        public bool SetLimtDisable(short axis)
        {
            return true;
        }
        public bool SetHomeOn(short axis)
        {
            return true;
        }
        public bool SetHomeOff(short axis)
        {
            return true;
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
                iMoveMode[axis] = 2;
                dCurrentVel[axis] = dSpeed;
                dTargetPos[axis] = pos;
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
                iMoveMode[axis] = 2;
                dCurrentVel[axis] = dSpeed;
                dTargetPos[axis] = dCurrentPos[axis] + pos;
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
                bBitOutputStatus[iBit] = bOn;
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
            bInitOK = true;
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            WorldGeneralLib.HiPerfTimer timer = new WorldGeneralLib.HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            Random rdm = new Random();
            int iBit = rdm.Next(0, 63);
            int iStep = 0;

            int i = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                #region Input 动态该更
                switch (iStep)
                {
                    case 0:
                        {
                            timer.Start();

                            iStep = 10;
                        }
                        break;
                    case 10:
                        {
                            if (timer.TimeUp(1))
                            {
                                timer.Start();
                                for (i = 0; i < 64; i++)
                                    bBitInputStatus[i] = true;
                                iStep = 20;
                            }
                        }
                        break;
                    case 20:
                        {
                            if (timer.TimeUp(1))
                            {
                                for (i = 0; i < 64; i++)
                                    bBitInputStatus[i] = false;

                                iStep = 0;
                            }
                        }
                        break;
                    default:
                        break;
                }
                #endregion
                #region 运动执行
                for (i = 0; i < 8; i++)
                {
                    if (iMoveMode[i] == 1)
                    {
                        dCurrentPos[i] = dCurrentPos[i] + dCurrentVel[i];
                    }
                    if (iMoveMode[i] == 2 || iMoveMode[i] == 3)
                    {
                        iMoveMode[i] = 0;
                        dCurrentPos[i] = dTargetPos[i];
                    }
                }
                #endregion
            }
        }
        public bool SetAlarmOn(short axis)
        {

            return true;
        }
        public bool SetAlarmOff(short axis)
        {

            return true;
        }
        public bool SetPulseMode(short axis, PulseMode plm)
        {

            return true;
        }
        override public bool Close()
        {

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
            StartCure(num);
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
        public void StartCure(short num)
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

        public bool  BuildCor(short num, CoordinateType interpolationAxisType)
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

        public double GT_GetAdc(short Cardnum, short adcout, out double dbRet)
        {
            throw new NotImplementedException();
        }
    }
}
