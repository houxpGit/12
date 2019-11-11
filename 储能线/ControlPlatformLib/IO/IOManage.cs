using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public enum OutPutLogic
    {
        NC,
        NO
    }
    public enum IOType
    { 
        ESTOP,
        DOOR,
        START,
        RESET,
        STOP,
        LaserReady
    }
    static public class IOManage
    {
        static public IODoc IODoc;
        static public FormIOSetting frmIoSetting;
        static public InputDrivers InputDrivers;
        static public OutputDrivers OutputDrivers;

        public delegate void HandlerUpdateStatus(IOType type, bool bOn);
        static public event HandlerUpdateStatus UpdateStatusEvent;

        public delegate void HandlerStartClick();
        static public event HandlerStartClick StartClickEvent;

        public delegate void HandlerStopClick();
        static public event HandlerStopClick StopClickEvent;

        public delegate void HandlerEStopClick();
        static public event HandlerEStopClick EStopClickEvent;

        public delegate void HandlerEStopReset();
        static public event HandlerEStopReset EStopResetEvent;

        public delegate void HandlerResetClick();
        static public event HandlerResetClick ResetClickEvent;

        public delegate void HandlerResetRelease();
        static public event HandlerResetRelease ResetReleaseEvent;

        public delegate void HandlerDoorOpen();
        static public event HandlerDoorOpen HandlerDoorOpenEvent;

        public delegate void HandlerDoorClose();
        static public event HandlerDoorClose HandlerDoorCloseEvent;

        public delegate void HandleraAlarm(string alarm);
        static public event HandleraAlarm HandleraAlarmEvent;

        public delegate void HandlerGratingOpen();
        static public event HandlerGratingOpen HandlerGratingOpenEvent;

        public delegate void HandlerGratingClose();
        static public event HandlerGratingClose HandlerGratingCloseEvent;

        static public void LoadData()
        {
            IODoc = IODoc.LoadObj();
        }
        static public void InitIOs()
        {
            InputDrivers = new InputDrivers();
            foreach (KeyValuePair<string, InputData> item in IODoc.m_InputDictionary)
            {
                InputDriver Driver = new InputDriver();
                InputDrivers.drivers.Add(item.Value.strIOName, Driver);
            }
            foreach (KeyValuePair<string, InputDriver> item in InputDrivers.drivers)
            {
                item.Value.Init(IODoc.m_InputDictionary[item.Key]);
            }

            OutputDrivers = new OutputDrivers();
            foreach (KeyValuePair<string, OutputData> item in IODoc.m_OutputDictionary)
            {
                OutputDriver Driver = new OutputDriver();
                OutputDrivers.drivers.Add(item.Value.strIOName, Driver);
            }
            foreach (KeyValuePair<string, OutputDriver> item in OutputDrivers.drivers)
            {
                item.Value.Init(IODoc.m_OutputDictionary[item.Key]);
            }
            System.Threading.Thread thread = new System.Threading.Thread(ThreadScanIOs);
            thread.IsBackground = true;
            thread.Start();
        }

        static bool bPLCConnectedPressed;

        static public void ThreadScanIOs()
        {
            bool bETOP = false;
            bool bRightETOP = false;
            bool bDOOR = false;
            bool bSTART = false;
            bool bSTOP = false;
            bool bRESET = false;
            bool bLaserReady = false;
            bool bAir = false;
            bool bLaserFinished = false;
            Thread.Sleep(5000);
            if (InputDrivers.drivers.ContainsKey("ESTOP"))
            {
                InputDrivers.drivers["ESTOP"].bPreStatus = true;
            }
            if (InputDrivers.drivers.ContainsKey("右边急停"))
            {
                InputDrivers.drivers["右边急停"].bPreStatus = true;
            }
            if (InputDrivers.drivers.ContainsKey("DOOR"))
            {
                InputDrivers.drivers["DOOR"].bPreStatus = true;
            }
            if (InputDrivers.drivers.ContainsKey("气压信号"))
            {
                InputDrivers.drivers["气压信号"].bPreStatus = true;
            }
            if (OutputDrivers.drivers.ContainsKey("三色灯黄"))
            {
                OUTPUT("三色灯黄").SetOutBit(true);
            }
            while (true)
            {
                try
                {
                    #region Estop
                    if (InputDrivers.drivers.ContainsKey("ESTOP"))
                    {
                        bETOP = InputDrivers.drivers["ESTOP"].GetOn();
                        if (!bETOP)
                        {
                            if (InputDrivers.drivers["ESTOP"].bPreStatus == true )
                            {
                                MainModule.FormMain.bAuto = false;
                                MainModule.FormMain.bEstop = true;
                                MainModule.FormMain.m_formAlarm.SetEstopAlarm();
                                MainModule.FormMain.SetEtopStatus(true);
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.SuspendMove();
                                    //itemDriver.Value.JogStop(TableAxisName.ALL);
                                    itemDriver.Value.bHomingU = false;
                                    itemDriver.Value.bHomingX = false;
                                    itemDriver.Value.bHomingY = false;
                                    itemDriver.Value.bHomingZ = false;
                                }
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(true);
                                }
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(true);
                                }
                                //if (UpdateStatusEvent!=null)
                                //{
                                //    UpdateStatusEvent(IOType.ESTOP, bETOP);
                                //}

                                //EStopClickEvent.Invoke();
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["ESTOP"].bPreStatus == false)
                            {
                                MainModule.FormMain.m_formAlarm.RstEstopAlarm();
                                MainModule.FormMain.SetEtopStatus(false);
                                MainModule.FormMain.bEstop = false;
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(false);
                                }
                                if (UpdateStatusEvent != null)
                                    UpdateStatusEvent(IOType.ESTOP, bETOP);

                                EStopResetEvent?.Invoke();

                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["ESTOP"].bPreStatus = bETOP;
                    }
                    #endregion
                    #region 右边急停
                    if (InputDrivers.drivers.ContainsKey("右边急停"))
                    {
                        bRightETOP = InputDrivers.drivers["右边急停"].GetOn();
                        if (!bRightETOP)
                        {
                            if (InputDrivers.drivers["右边急停"].bPreStatus == true)
                            {
                                MainModule.FormMain.bAuto = false;
                                MainModule.FormMain.bEstop = true;
                                MainModule.FormMain.m_formAlarm.SetEstopAlarm();
                                MainModule.FormMain.SetEtopStatus(true);
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.SuspendMove();
                                    //itemDriver.Value.JogStop(TableAxisName.ALL);
                                    itemDriver.Value.bHomingU = false;
                                    itemDriver.Value.bHomingX = false;
                                    itemDriver.Value.bHomingY = false;
                                    itemDriver.Value.bHomingZ = false;
                                }
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(true);
                                }
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(true);
                                }
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["右边急停"].bPreStatus == false)
                            {
                                MainModule.FormMain.m_formAlarm.RstEstopAlarm();
                                MainModule.FormMain.SetEtopStatus(false);
                                MainModule.FormMain.bEstop = false;
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(false);
                                }
                                if (UpdateStatusEvent != null)
                                    UpdateStatusEvent(IOType.ESTOP, bRightETOP);

                                EStopResetEvent?.Invoke();

                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["右边急停"].bPreStatus = bRightETOP;
                    }
                    #endregion
                    #region DOOR
                    if (InputDrivers.drivers.ContainsKey("DOOR"))
                    {
                        bDOOR = InputDrivers.drivers["DOOR"].GetOn();
                        if (!bDOOR)
                        {
                            if (InputDrivers.drivers["DOOR"].bPreStatus == true)
                            {
                                MainModule.FormMain.bAuto = false;
                                MainModule.FormMain.bDoorOpen = true;
                                MainModule.FormMain.m_formAlarm.SetDoorOpenAlarm();
                                MainModule.FormMain.SetDoorStatus(true);
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.SuspendMove();
                                    //itemDriver.Value.JogStop(TableAxisName.ALL);
                                }
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(true);
                                }
                                UpdateStatusEvent?.Invoke(IOType.DOOR, bDOOR);
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(true);
                                }
                                //HandlerDoorOpenEvent.Invoke();
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["DOOR"].bPreStatus == false)
                            {
                                MainModule.FormMain.m_formAlarm.RstDoorOpenAlarm();
                                MainModule.FormMain.bDoorOpen = false;
                                MainModule.FormMain.SetDoorStatus(false);
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(false);
                                }
                                UpdateStatusEvent?.Invoke(IOType.DOOR, bDOOR);
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.ResumeMove();
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(false);
                                }
                                //HandlerDoorCloseEvent.Invoke();
                            }
                        }
                        InputDrivers.drivers["DOOR"].bPreStatus = bDOOR;
                    }
                    #endregion
                    #region Start
                    if (InputDrivers.drivers.ContainsKey("START"))
                    {
                        bSTART = InputDrivers.drivers["START"].GetOn();
                        if (bSTART)
                        {
                            if (InputDrivers.drivers["START"].bPreStatus == false)
                            {
                                if (OutputDrivers.drivers.ContainsKey("START"))
                                {
                                    OUTPUT("START").SetOutBit(true);
                                }
                                FormOperator.StartPushed();
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["START"].bPreStatus == true)
                            {
                                if (OutputDrivers.drivers.ContainsKey("START"))
                                {
                                    OUTPUT("START").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["START"].bPreStatus = bSTART;
                    }
                    #endregion
                    #region Stop
                    if (InputDrivers.drivers.ContainsKey("STOP"))
                    {
                        bSTOP = InputDrivers.drivers["STOP"].GetOn();
                        if (bSTOP)
                        {
                            if (InputDrivers.drivers["STOP"].bPreStatus == false)
                            {
                                MainModule.FormMain.bAuto = false;
                                MainModule.FormMain.bStopPress = true;
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.SuspendMove();
                                    //itemDriver.Value.JogStop(TableAxisName.ALL);
                                }
                                if (OutputDrivers.drivers.ContainsKey("STOP"))
                                {
                                    OUTPUT("STOP").SetOutBit(true);
                                }
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(true);
                                }
                                FormOperator.StopPushed();
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["STOP"].bPreStatus == true)
                            {
                                if (OutputDrivers.drivers.ContainsKey("STOP"))
                                {
                                    OUTPUT("STOP").SetOutBit(false);
                                }
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.ResumeMove();
                                }
                                if (OutputDrivers.drivers.ContainsKey("打标急停"))
                                {
                                    OUTPUT("打标急停").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["STOP"].bPreStatus = bSTOP;
                    }
                    #endregion
                    #region RESET
                    if (InputDrivers.drivers.ContainsKey("RESET"))
                    {
                        bRESET = InputDrivers.drivers["RESET"].GetOn();
                        if (bRESET)
                        {
                            if (InputDrivers.drivers["RESET"].bPreStatus == false)
                            {
                                MainModule.FormMain.bResetPress = true;
                                MainModule.FormMain.m_formAlarm.RstOtherAlarm();
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.bHomingU = false;
                                    itemDriver.Value.bHomingX = false;
                                    itemDriver.Value.bHomingY = false;
                                    itemDriver.Value.bHomingZ = false;
                                }
                                if (OutputDrivers.drivers.ContainsKey("RESET"))
                                {
                                    OUTPUT("RESET").SetOutBit(true);
                                }
                                ResetClickEvent?.Invoke();
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["RESET"].bPreStatus == true)
                            {
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                                MainModule.FormMain.bResetPress = false;
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.ResumeMove();
                                }
                                ResetReleaseEvent?.Invoke();
                                if (OutputDrivers.drivers.ContainsKey("RESET"))
                                {
                                    OUTPUT("RESET").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["RESET"].bPreStatus = bRESET;
                    }
                    #endregion
                    #region 激光器准备好
                    if (InputDrivers.drivers.ContainsKey("激光器准备好"))
                    {
                        bLaserReady = InputDrivers.drivers["激光器准备好"].GetOn();
                        if (bLaserReady)
                        {
                            if (InputDrivers.drivers["激光器准备好"].bPreStatus == false)
                            {
                                if (OutputDrivers.drivers.ContainsKey("激光器准备好"))
                                {
                                    OUTPUT("激光器准备好").SetOutBit(true);
                                }
                                MainModule.FormMain.SetLaserStatus(true);
                                //if (UpdateStatusEvent != null)
                                //{
                                //    UpdateStatusEvent(IOType.LaserReady, bLaserReady);
                                //}
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["激光器准备好"].bPreStatus == true)
                            {
                                if (OutputDrivers.drivers.ContainsKey("激光器准备好"))
                                {
                                    OUTPUT("激光器准备好").SetOutBit(false);
                                }
                                MainModule.FormMain.SetLaserStatus(false);
                            }
                        }
                        InputDrivers.drivers["激光器准备好"].bPreStatus = bLaserReady;
                    }
                    #endregion
                    #region 气压信号
                    if (InputDrivers.drivers.ContainsKey("气压信号"))
                    {
                        bAir = InputDrivers.drivers["气压信号"].GetOn();
                        if (!bAir)
                        {
                            if (InputDrivers.drivers["气压信号"].bPreStatus == true)
                            {
                                MainModule.FormMain.bAuto = false;
                                MainModule.FormMain.m_formAlarm.SetAIRAlarm();
                                foreach (KeyValuePair<string, TableDriver> itemDriver in TableManage.tableDrivers.drivers)
                                {
                                    itemDriver.Value.SuspendMove();
                                    //itemDriver.Value.JogStop(TableAxisName.ALL);
                                    itemDriver.Value.bHomingU = false;
                                    itemDriver.Value.bHomingX = false;
                                    itemDriver.Value.bHomingY = false;
                                    itemDriver.Value.bHomingZ = false;
                                }
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(true);
                                }
                                if (OutputDrivers.drivers.ContainsKey("清洗触发"))
                                {
                                    OUTPUT("清洗触发").SetOutBit(false);
                                }
                            }
                        }
                        else
                        {
                            if (InputDrivers.drivers["气压信号"].bPreStatus == false)
                            {
                                MainModule.FormMain.m_formAlarm.RstAIRAlarm();
                                if (OutputDrivers.drivers.ContainsKey("BIZZ"))
                                {
                                    OUTPUT("BIZZ").SetOutBit(false);
                                }
                            }
                        }
                        InputDrivers.drivers["气压信号"].bPreStatus = bAir;
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Global.logger.ErrorFormat("IO 异常:{0}", e.Message);
                    MessageBox.Show(e.Message, "IO异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Thread.Sleep(1);
            }
        }
        static public InputDriver INPUT(string strInputName)
        {
            try
            {
                return InputDrivers.drivers[strInputName];
            }
            catch
            {
                Action action = () =>
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在名字为:" + strInputName + "的输入");
                };
                MainModule.FormMain.Invoke(action);
                InputDriver driver = new InputDriver();
                InputDrivers.drivers.Add(strInputName, driver);
                return InputDrivers.drivers[strInputName];

            }
        }
        static public OutputDriver OUTPUT(string strOutputName)
        {
            try
            {
                return OutputDrivers.drivers[strOutputName];
            }
            catch
            {
                Action action = () =>
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在名字为:" + strOutputName + "的输出");
                };
                MainModule.FormMain.Invoke(action);
                OutputDriver driver = new OutputDriver();
                OutputDrivers.drivers.Add(strOutputName, driver);
                return OutputDrivers.drivers[strOutputName];
            }
        }

       
    }
}
