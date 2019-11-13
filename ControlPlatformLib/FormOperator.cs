using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class FormOperator : Form
    {
        public delegate void StartButtonPushed();
        static public event StartButtonPushed startButtonPushed;
        public delegate void HomeButtonPushed();
        public event HomeButtonPushed homeButtonPushed;
        public delegate void ResetDown();
        public event ResetDown resetDown;
        public delegate void ResetUp();
        public event ResetUp resetUp;
        public bool bPreAuto = false;
        public bool bPreHomeReady = false;

        public static event StopButtonPushed stopButtonPushed;
        public delegate void StopButtonPushed();
        public FormOperator()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
           
            if (startButtonPushed == null)
            {

            }
            else
            {
                startButtonPushed();
                //if (startButtonPushed(sender, e))
                //{

                //}
                //else
                //{
                //    return;
                //}
            }
            if (MainModule.FormMain.Parse == true&& MainModule.FormMain.bHomeReady&& MainModule.FormMain.bAuto==true)
            {
                //  MainModule.FormMain.bAuto = false;
                //if (buttonStart.Text == "暂停")
                //{
                    buttonStart.Text = "启动";
             //   }
             //   buttonStart.Text = "启动";
                buttonStart.BackColor = Color.GreenYellow;
                MainModule.FormMain.Parse = false;

            }
            else if (MainModule.FormMain.Parse == false && MainModule.FormMain.bHomeReady && MainModule.FormMain.bAuto == true)
            {
                showCount = 0;
                buttonStart.BackColor = Color.Green;
                //MainModule.FormMain.bAuto = true;
              //  MainModule.FormMain.Parse = true;
                //if (buttonStart.Text == "启动")
                //{
                //    buttonStart.Text = "暂停";
                //}


                //}
                //if (MainModule.FormMain.bAuto)
                //{

                //    MainModule.FormMain.bAuto = false;
                //}
                //else
                //{

                //    MainModule.FormMain.bAuto = true;
            }
        }
        static public void StartPushed()
        {
            if (startButtonPushed == null)
            {

            }
            else
            {
                startButtonPushed();
                //if (startButtonPushed(null, null))
                //{

                //}
                //else
                //{
                //    return;
                //}
            }
            if (MainModule.FormMain.bAuto == false)
            {
                MainModule.FormMain.bAuto = true;
            }
            
        }
        static public void StopPushed()
        {
            if (MainModule.FormMain.bAuto)
            {
                MainModule.FormMain.bAuto = false;
                try
                {

                    //if (!ControlPlatformLib.Global.bIgnoreMes)
                    //{
                    //    bool? result = Global.m_KPIMES?.UploadState(4, Global.sMachineNO + Environment.MachineName, DateTime.Now);
                    //    Global.logger.Info("更新停止状态" + result);
                    //}
                }
                catch (Exception ex)
                {
                    Global.logger.Error("更新运行状态出现错误：" + ex.Message);
                }
            }
            if (stopButtonPushed!=null)
            {
                stopButtonPushed.Invoke();
            }
        }
        private void FormOperator_Load(object sender, EventArgs e)
        {
            MainModule.FormMain.m_formAlarm.alarmChangedEvent += m_formAlarm_alarmChangedEvent;
            timerScan.Enabled = true;
        }

        void m_formAlarm_alarmChangedEvent(object sender, bool bAlarm, bool bSilence)
        {
            MainModule.FormMain.bAlarm = bAlarm && !bSilence;
            if (bAlarm)
            {
                buttonReset.BackColor = Color.Yellow;
                if (IOManage.OutputDrivers.drivers.ContainsKey("RESET"))
                {
                    //IOManage.OutputDrivers.drivers["RESET"].SetOutBit(true);
                }

                if (IOManage.OutputDrivers.drivers.ContainsKey("BIZZ"))
                {
                    if (bSilence)
                    {
                        IOManage.OutputDrivers.drivers["BIZZ"].SetOutBit(false);
                    }
                    else
                    {
                        IOManage.OutputDrivers.drivers["BIZZ"].SetOutBit(true);
                    }
                }

            }
            else
            {
                buttonReset.BackColor = Color.FromKnownColor(KnownColor.Control);
                if (IOManage.OutputDrivers.drivers.ContainsKey("RESET"))
                {
                    IOManage.OutputDrivers.drivers["RESET"].SetOutBit(false);
                }

                if (IOManage.OutputDrivers.drivers.ContainsKey("BIZZ"))
                {

                    IOManage.OutputDrivers.drivers["BIZZ"].SetOutBit(false);

                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (homeButtonPushed == null)
            {

            }
            else
            {
                homeButtonPushed();
                //if (homeButtonPushed(sender, e))
                //{

                //}
                //else
                //{
                //    return;
                //}
            }
            //MainModule.FormMain.m_formAlarm.RstOtherAlarm();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            //if (homeButtonPushed == null)
            //{

            //}
            //else
            //{
            //    homeButtonPushed();
            //    //if (homeButtonPushed(sender, e))
            //    //{

            //    //}
            //    //else
            //    //{
            //    //    return;
            //    //}
            //}
        }

        private void buttonReset_MouseDown(object sender, MouseEventArgs e)
        {
            //if (resetDown != null)
            //{
            //    resetDown();
            //}
            //MainModule.FormMain.m_formAlarm.RstOtherAlarm();
            //MainModule.FormMain.bResetPress = true;
        }

        private void buttonReset_MouseUp(object sender, MouseEventArgs e)
        {
            //if (resetUp != null)
            //{
            //    resetUp();
            //}
            //MainModule.FormMain.bResetPress = false;
        }

        private void buttonReset_MouseLeave(object sender, EventArgs e)
        {
            MainModule.FormMain.bResetPress = false;
        }
        int showCount = 0;
        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (MainModule.FormMain.bAuto)
            {
                if (MainModule.FormMain.Parse == true&& showCount==0)
                {
                    showCount++;
                    buttonStart.Text = "暂停";
                    buttonStart.BackColor = Color.GreenYellow;
                    // MainModule.FormMain.Parse = false;
                }
                else
                {
                   

                }
                if (bPreAuto == false)
                {
                    buttonStart.BackColor = Color.Green;
                   //  buttonStart.Text = "启动";
                    buttonStart.ImageIndex = 2;
                    if (IOManage.OutputDrivers.drivers.ContainsKey("START"))
                    {
                        IOManage.OUTPUT("START").SetOutBit(true);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("STOP"))
                    {
                        IOManage.OUTPUT("STOP").SetOutBit(false);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯红"))
                    {
                        IOManage.OUTPUT("三色灯红").SetOutBit(false);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯黄"))
                    {
                        IOManage.OUTPUT("三色灯黄").SetOutBit(false);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯绿"))
                    {
                        IOManage.OUTPUT("三色灯绿").SetOutBit(true);
                    }
                }
            }
            else
            {
                if (bPreAuto)
                {
                    buttonStart.BackColor = Color.Red;
                    buttonStart.Text = "启动";
                    buttonStart.ImageIndex = 3;
                    if (IOManage.OutputDrivers.drivers.ContainsKey("START"))
                    {
                        IOManage.OUTPUT("START").SetOutBit(false);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("STOP"))
                    {
                        IOManage.OUTPUT("STOP").SetOutBit(true);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯红"))
                    {
                        IOManage.OUTPUT("三色灯红").SetOutBit(false);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯黄"))
                    {
                        IOManage.OUTPUT("三色灯黄").SetOutBit(true);
                    }
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯绿"))
                    {
                        IOManage.OUTPUT("三色灯绿").SetOutBit(false);
                    }
                }
                else
                {
                    if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯黄")&&IOManage.OUTPUT("三色灯黄").GetOn())
                    {
                        IOManage.OUTPUT("三色灯黄").SetOutBit(false);
                    }
                    else if (IOManage.OutputDrivers.drivers.ContainsKey("三色灯黄")&&IOManage.OUTPUT("三色灯黄").GetOff())
                    {
                        IOManage.OUTPUT("三色灯黄").SetOutBit(true);
                    }
                }

            }
            bPreAuto = MainModule.FormMain.bAuto;
            if (MainModule.FormMain.bHomeReady)
            {
                if (bPreHomeReady == false)
                {
                    buttonHome.BackColor = Color.Green;
                }
            }
            else
            {
                if (bPreHomeReady)
                {
                    buttonHome.BackColor = Color.Red;
                }
            }
            bPreHomeReady = MainModule.FormMain.bHomeReady;

            if (MainModule.FormMain.bAlarm && IOManage.OutputDrivers.drivers.ContainsKey("BIZZ") && IOManage.OUTPUT("BIZZ").GetOn())
            {
                IOManage.OUTPUT("BIZZ").SetOutBit(false);
                IOManage.OUTPUT("三色灯红").SetOutBit(false);
                IOManage.OUTPUT("三色灯黄").SetOutBit(false);
            }
            else if (MainModule.FormMain.bAlarm && IOManage.OutputDrivers.drivers.ContainsKey("BIZZ") && IOManage.OUTPUT("BIZZ").GetOff())
            {
                IOManage.OUTPUT("BIZZ").SetOutBit(true);
                IOManage.OUTPUT("三色灯红").SetOutBit(true);
                IOManage.OUTPUT("三色灯黄").SetOutBit(false);
                IOManage.OUTPUT("三色灯绿").SetOutBit(false);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            MainModule.FormMain.Parse = false;
            StopPushed();
        }

        private void buttonHome_MouseDown(object sender, MouseEventArgs e)
        {
            if (resetDown != null)
            {
                resetDown();
            }
            MainModule.FormMain.m_formAlarm.RstOtherAlarm();
            MainModule.FormMain.bResetPress = true;
        }

        private void buttonHome_MouseUp(object sender, MouseEventArgs e)
        {
            if (resetUp != null)
            {
                resetUp();
            }
            MainModule.FormMain.bResetPress = false;
        }
    }
}
