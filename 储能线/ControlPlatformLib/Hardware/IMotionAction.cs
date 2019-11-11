

using System.Collections.Generic;
namespace ControlPlatformLib
{
    public enum CoordinateType
    {
        XY,
        XZ,
        YZ,
        XYZ
    }
    public interface IMotionAction
    {
        bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos);

        bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW);

        bool ArcMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW, CoordinateType interpolationAxisType);

        bool  BuildCor(short num, short AxisX, short AxisY, short AxisZ);

        bool  BuildCor(short num, CoordinateType interpolationAxisType);

        bool CureMoveDone(short num, out int iStep);
       // bool CureMoveDoneIsrunFinish(short num, out int iStep);

        bool FinishSearchHome(short axis);

        bool FinishSearchLimit(short axis);

        bool GetAlarm(short Axis);

        double GetCurrentPos(short Axis);

        void PositionCompareOut(short num, short chn, short outputType, short maxerr, short output_1, short output_2, double px, double py);

        bool GetEstop(short Axis);

        bool GetHome(short Axis);

        bool GetLimtCCW(short Axis);

        bool GetLimtCW(short Axis);

        bool GetServoOn(short Axis);

        double GetVel(short Axis);

        bool HandleErrorMessage(short errorMessage);

        bool InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed);

        bool  InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed);

        bool IsMoveDone(short Axis);

        bool IsMoving(short Axis);

        bool JogMove(short Axis, double dAcc, double dDec, double dVel);

        bool StopJog(short Axis);

        bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ);

        bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos);

        bool ServoOff(short axis);

        bool ServoOn(short axis);

        bool SetAlarmOff(short axis);

        bool SetAlarmOn(short axis);

        bool SetAxisPos(short axis, double dPos);

        bool SetHomeOff(short axis);

        bool SetHomeOn(short axis);

        bool SetLimtDisable(short axis);

        bool SetLimtOff(short axis);

        bool SetLimtOn(short axis);

        bool SetNearHomeOff(short axis);

        bool SetNearHomeOn(short axis);

        bool SetPulseMode(short axis, PulseMode psm);

        bool SetVel(short Axis, double dVel);

        bool StartCure(short num,bool bPreWelding);
        double GT_GetAdc(short Cardnum, short adcout, out double dbRet);
        bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd);

        bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed);

        bool StopMove(short Axis);

        bool ZeroAxisPos(short axis);

        /// <summary>
        /// 开始一个圆弧插补运动。 圆弧由插补过程中轴通过的圆弧上的绝对过渡点位置和圆弧的绝对终点位置指定。
        /// </summary>
        bool ArcMoveWithThroughAndEnd(short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double endPosX, double endPosY, double throughPosX, double throughPosY);

        bool GantryMove(short gantryAxis1, short gantryAxis2, double dAcc, double dDec, double dSpeed, double endPos);

        bool ResetAxisAlarm();

        bool StopInterpolation();

        bool ResumeInterpolation();

        bool ComparePositionOut(int axis,double pos,out double different);

        void PreWelding(int preWeldingNumber,List<double> preWeldingPos);

        bool PreWeldingDone();

        /// <summary>
        /// 启动手动脉冲器操作
        /// </summary>
        /// <returns></returns>
        bool StartManualPulserOperation(int axis_ID, int MultiplyingPower);

        /// <summary>
        /// 停止手动脉冲器操作
        /// </summary>
        /// <returns></returns>
        bool StopManualPulserOperation(int axis_ID);
    }
}