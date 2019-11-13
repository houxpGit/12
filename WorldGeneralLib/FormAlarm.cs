using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WorldGeneralLib
{
    
    public partial class FormAlarm : Form
    {
        public delegate void AlarmStatusChanged(object sender, bool bAlarm,bool bSilence);
        public event AlarmStatusChanged alarmChangedEvent;
        public delegate void ResetMouseDown();
        public event ResetMouseDown resetMouseDownEvent;
        public delegate void ResetMouseUp();
        public event ResetMouseUp resetMouseUpEvent;
        public Dictionary<string, AlarmItem> alarmDictionary;
        private bool bMouseDown = false;
        private int iMouseDownX = 0;
        private int iMouseDownY = 0;
        private bool bEstop = false;
        private bool bDoorOpen = false;
        private bool[] bMotorAlarm;
        private bool[] bMotorCWLim;
        private bool[] bMotorCCWLim;
        private object objLock;
        public bool bRetry = false;
        public bool bNeglect = false;
        public FormAlarm()
        {           
            bMotorAlarm=new bool[50];
            bMotorCWLim=new bool[50];
            bMotorCCWLim=new bool[50];
            for(int i=0;i<50;i++)
            {
                bMotorAlarm[i] = false;
                bMotorCWLim[i] = false;
                bMotorCCWLim[i] = false;
            }
            objLock = new object();
            alarmDictionary = new Dictionary<string, AlarmItem>();
            InitializeComponent();
             
        }
        public FormAlarm(Control control)
        {
            bMotorAlarm = new bool[50];
            bMotorCWLim = new bool[50];
            bMotorCCWLim = new bool[50];
            for (int i = 0; i < 50; i++)
            {
                bMotorAlarm[i] = false;
                bMotorCWLim[i] = false;
                bMotorCCWLim[i] = false;
            }
            objLock = new object();
            alarmDictionary = new Dictionary<string, AlarmItem>();
            InitializeComponent();

            TopLevel = false;
            control.Controls.Add(this);
            this.Left = control.Width / 2 - this.Width / 2;
            this.Top = control.Height / 2 - this.Height / 2;
            if (Left < 0 || Top < 0)
            {
                Left = 0;
                Top = 0;
            }
            this.Hide();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible==false)
            {
                if (listViewAlarmCur.Items.Count > 0)
                {
                    bool bSilence = checkBoxAlramOff.Checked;
                    if (alarmChangedEvent != null)
                    {
                        alarmChangedEvent(this, true, bSilence);
                    }
                }
               
            }
            else
            {
                if (listViewAlarmCur.Items.Count <= 0)
                {
                    this.Hide();
                    if (alarmChangedEvent != null)
                    {
                        bool bSilence = checkBoxAlramOff.Checked;
                        alarmChangedEvent(this, false, bSilence);
                    }
                }
               
            }
            if (listViewAlarmCur.Items.Count > 0)
            {
                if (this.Visible == false)
                {
                    this.Show();
                    Action action = () => {
                        this.BringToFront();
                    };
                    this.Invoke(action);
                }
                else 
                {
                    Action action = () => {
                        this.BringToFront();
                    };
                    this.Invoke(action);
                }
            }
            else
            {
                if (this.Visible )
                {
                    this.Hide();
                }
            }
            
        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {
            
            listViewAlarmCur.Columns.Add("Message");
            listViewAlarmCur.Columns.Add("Code");
            listViewAlarmCur.Columns.Add("Time");
            listViewAlarmCur.Columns[0].Width = listViewAlarmCur.Width -20- 100-130;
            listViewAlarmCur.Columns[1].Width = 100;
            listViewAlarmCur.Columns[2].Width = 130;
            listViewAlarmHis.Columns.Add("Message");
            listViewAlarmHis.Columns.Add("Time");
            listViewAlarmHis.Columns[0].Width = listViewAlarmHis.Width - 350;
            listViewAlarmHis.Columns[1].Width = 350;
            listViewSuggest.Columns.Add("处理方法");
            listViewSuggest.Columns.Add("报警代号");
            listViewSuggest.Columns[0].Width = listViewAlarmHis.Width - 350;
            listViewSuggest.Columns[1].Width = 350;

            CreateAlarmItemHeader();
           
        }
        public void InsertAlarmMessage(string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.Normal;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {   
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }

                                alarmItem.strPicturePath = @".//Pic/Test.jpg";

                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);

                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], 0);
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], 0);
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                pictureBox1.ImageLocation = alarmItem.strPicturePath;
                            }
                            //alarmDictionary.Add(strMessage)
                        }
                        catch
                        {
 
                        }
                        AddAlarmToLog(strMessage);
                        RemoveHisItem();
                    };
                this.Invoke(action);

                //if (listViewAlarmCur.IsHandleCreated)
                //{
                //    listViewAlarmCur.Invoke(action);
                //}
            }
        }
        public void InsertAlarmMessageRobot(string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    try
                    {
                        string[] strArrange = strMessage.Split(';');
                        AlarmItem alarmItem = new AlarmItem();
                        alarmItem.alarmType = AlarmType.Robot;
                        if (strArrange.Length > 0)
                        {
                            alarmItem.stringAlarmMessage = strArrange[0];
                        }
                        if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                        {
                            if (strArrange.Length > 1)
                            {
                                alarmItem.strAlarmCode = strArrange[1];
                            }
                            if (strArrange.Length > 2)
                            {
                                for (int i = 2; i < strArrange.Length; i++)
                                {
                                    alarmItem.listHandMessage.Add(strArrange[i]);
                                }
                            }

                           

                            alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alarmDictionary.Add(strArrange[0], alarmItem);

                            ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], (Int32)(AlarmType.Robot));
                            listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                            listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                            ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.Robot));
                            listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                            SetListSelectItem(listViewAlarmCur.Items.Count - 1);


                           

                        }
                    }
                    catch
                    {

                    }
                    AddAlarmToLog(strMessage);
                    RemoveHisItem();
                };
                this.Invoke(action);
            }
        }
        public void InsertAlarmPLC(string strMessage)
        {
            lock(objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.PLC;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.PLC));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.PLC));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strMessage);
                            }
                        }
                        catch
                        {

                        }
                        AddAlarmToLog(strMessage);
                        RemoveHisItem();
                    };
                this.Invoke(action);
            }
            
        }
        public void RemoveAlarmPLC(string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        
                    };
                this.Invoke(action);
            }
        }
        private void FormAlarm_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;
           
            iMouseDownX = e.X;
            iMouseDownY = e.Y;
        }

        private void FormAlarm_MouseUp(object sender, MouseEventArgs e)
        {
           
            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;
  
            }
            bMouseDown = false;    
        }

        private void FormAlarm_MouseMove(object sender, MouseEventArgs e)
        {
            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;

            }
            
        }
        private bool GetAlarm()
        {
            if (bEstop || bDoorOpen)
                return true;
            for (int i = 0; i < 50; i++)
            {
                if (bMotorAlarm[i])
                {
                    return true;
                }
                if (bMotorCWLim[i])
                {
                    return true;
                }
                if (bMotorCCWLim[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void SetEstopAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        string strMessage = "ESTOP;00;松开急停按键;急停线路可能有问题";
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.MotorEstop;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        RemoveHisItem();
                        AddAlarmToLog(strMessage);
                    };
                this.Invoke(action);
            }
        }
        public void RstEstopAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string strMessage = "ESTOP;00;松开急停按键;急停线路可能有问题";
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);
                //AddAlarmToLog("E-Stop");

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }

        public void SetAIRAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    string strMessage = "气压异常;00;气压异常;气路可能有问题";
                    try
                    {
                        string[] strArrange = strMessage.Split(';');
                        AlarmItem alarmItem = new AlarmItem();
                        alarmItem.alarmType = AlarmType.MotorEstop;
                        if (strArrange.Length > 0)
                        {
                            alarmItem.stringAlarmMessage = strArrange[0];
                        }
                        if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                        {
                            if (strArrange.Length > 1)
                            {
                                alarmItem.strAlarmCode = strArrange[1];
                            }
                            if (strArrange.Length > 2)
                            {
                                for (int i = 2; i < strArrange.Length; i++)
                                {
                                    alarmItem.listHandMessage.Add(strArrange[i]);
                                }
                            }
                            alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alarmDictionary.Add(strArrange[0], alarmItem);
                            ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                            listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                            listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                            ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                            listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                            SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                            AddAlarmToLog(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                    RemoveHisItem();
                    AddAlarmToLog(strMessage);
                };
                this.Invoke(action);
            }
        }
        public void RstAIRAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    try
                    {
                        string strMessage = "气压异常;00;气压异常;气路可能有问题";
                        string[] strArrange = strMessage.Split(';');
                        if (alarmDictionary.ContainsKey(strArrange[0]))
                        {
                            listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                            alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                            alarmDictionary.Remove(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);
                //AddAlarmToLog("E-Stop");

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }

        public void SetDoorOpenAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        string strMessage = "DoorOpen;01;门被打开了;门控线路可能有问题";
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.MotorEstop;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        RemoveHisItem();
                        AddAlarmToLog(strMessage);
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //     this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstDoorOpenAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string strMessage = "DoorOpen;01;门被打开了;门控线路可能有问题";
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }

        public void SetGratingAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    string strMessage = "Grating;04;光栅被触发了;光栅线路可能有问题";
                    try
                    {
                        string[] strArrange = strMessage.Split(';');
                        AlarmItem alarmItem = new AlarmItem();
                        alarmItem.alarmType = AlarmType.Grating;
                        if (strArrange.Length > 0)
                        {
                            alarmItem.stringAlarmMessage = strArrange[0];
                        }
                        if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                        {
                            if (strArrange.Length > 1)
                            {
                                alarmItem.strAlarmCode = strArrange[1];
                            }
                            if (strArrange.Length > 2)
                            {
                                for (int i = 2; i < strArrange.Length; i++)
                                {
                                    alarmItem.listHandMessage.Add(strArrange[i]);
                                }
                            }
                            alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alarmDictionary.Add(strArrange[0], alarmItem);
                            ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                            listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                            listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                            ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                            listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                            SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                            AddAlarmToLog(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                    RemoveHisItem();
                    AddAlarmToLog(strMessage);
                };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstGratingAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    try
                    {
                        string strMessage = "Grating;04;光栅被触发了;光栅线路可能有问题";
                        string[] strArrange = strMessage.Split(';');
                        if (alarmDictionary.ContainsKey(strArrange[0]))
                        {
                            listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                            alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                            alarmDictionary.Remove(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }

        public void SetLaserAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    string strMessage = "Laser;05;激光器报警被触发了;激光器可能有问题";
                    try
                    {
                        string[] strArrange = strMessage.Split(';');
                        AlarmItem alarmItem = new AlarmItem();
                        alarmItem.alarmType = AlarmType.LaserAlarm;
                        if (strArrange.Length > 0)
                        {
                            alarmItem.stringAlarmMessage = strArrange[0];
                        }
                        if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                        {
                            if (strArrange.Length > 1)
                            {
                                alarmItem.strAlarmCode = strArrange[1];
                            }
                            if (strArrange.Length > 2)
                            {
                                for (int i = 2; i < strArrange.Length; i++)
                                {
                                    alarmItem.listHandMessage.Add(strArrange[i]);
                                }
                            }
                            alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alarmDictionary.Add(strArrange[0], alarmItem);
                            ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.LaserAlarm));
                            listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                            listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                            ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.LaserAlarm));
                            listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                            SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                            AddAlarmToLog(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                    RemoveHisItem();
                    AddAlarmToLog(strMessage);
                };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstLaserAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    try
                    {
                        string strMessage = "Laser;05;激光器报警被触发了;激光器可能有问题";
                        string[] strArrange = strMessage.Split(';');
                        if (alarmDictionary.ContainsKey(strArrange[0]))
                        {
                            listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                            alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                            alarmDictionary.Remove(strArrange[0]);
                        }
                    }
                    catch
                    {

                    }
                };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }

        public void SetMotorAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        string strMessage = MotorName + "马达报警" + ";02;马达过载;马达线路可能有问题;马达刹车线可能有问题";
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.MotorEstop;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        RemoveHisItem();
                        AddAlarmToLog(strMessage);
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstMotorAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string strMessage = MotorName + "马达报警"+";02;马达过载;马达线路可能有问题;马达刹车线可能有问题";
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void SetMotorCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        string strMessage = MotorName + "正极限报警" + ";03;门被打开了;门控线路可能有问题";
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.MotorEstop;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        RemoveHisItem();
                        AddAlarmToLog(strMessage);
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstMotorCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string strMessage = MotorName + "正极限报警" + ";03;门被打开了;门控线路可能有问题";
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
            } 
        }
        public void SetMotorCCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        string strMessage = MotorName + "负极限报警" + ";03;门被打开了;门控线路可能有问题";
                        try
                        {
                            string[] strArrange = strMessage.Split(';');
                            AlarmItem alarmItem = new AlarmItem();
                            alarmItem.alarmType = AlarmType.MotorEstop;
                            if (strArrange.Length > 0)
                            {
                                alarmItem.stringAlarmMessage = strArrange[0];
                            }
                            if (alarmDictionary.ContainsKey(alarmItem.stringAlarmMessage) == false)
                            {
                                if (strArrange.Length > 1)
                                {
                                    alarmItem.strAlarmCode = strArrange[1];
                                }
                                if (strArrange.Length > 2)
                                {
                                    for (int i = 2; i < strArrange.Length; i++)
                                    {
                                        alarmItem.listHandMessage.Add(strArrange[i]);
                                    }
                                }
                                alarmItem.strHappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                alarmDictionary.Add(strArrange[0], alarmItem);
                                ListViewItem listViewItem = listViewAlarmCur.Items.Add(strArrange[0], strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItem.SubItems.Add(alarmItem.strAlarmCode);
                                listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));

                                ListViewItem listViewItemHis = listViewAlarmHis.Items.Add(strArrange[0], (Int32)(AlarmType.MotorEstop));
                                listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                                SetListSelectItem(listViewAlarmCur.Items.Count - 1);

                                AddAlarmToLog(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                        RemoveHisItem();
                        AddAlarmToLog(strMessage);
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);

                if (listViewAlarmCur.InvokeRequired)
                {
                    listViewAlarmCur.Invoke(action);
                }
            }
        }
        public void RstMotorCCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        try
                        {
                            string strMessage = MotorName + "负极限报警" + ";03;门被打开了;门控线路可能有问题";
                            string[] strArrange = strMessage.Split(';');
                            if (alarmDictionary.ContainsKey(strArrange[0]))
                            {
                                listViewAlarmCur.Items.RemoveByKey(strArrange[0]);
                                alarmDictionary[strArrange[0]].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                WriteAlarmResumeItem(alarmDictionary[strArrange[0]]);
                                alarmDictionary.Remove(strArrange[0]);
                            }
                        }
                        catch
                        {

                        }
                    };
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(action);
                //}
                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);
            }
        }
        public void RstOtherAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                    {
                        for (int i = listViewAlarmCur.Items.Count - 1; i > -1; i--)
                        {
                            if (listViewAlarmCur.Items[i].ImageIndex == (int)AlarmType.Normal||
                                listViewAlarmCur.Items[i].ImageIndex == (int)AlarmType.Robot)
                            {
                                if (alarmDictionary.ContainsKey(listViewAlarmCur.Items[i].Text))
                                {
                                    alarmDictionary[listViewAlarmCur.Items[i].Text].strResetTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    WriteAlarmResumeItem(alarmDictionary[listViewAlarmCur.Items[i].Text]);
                                    alarmDictionary.Remove(listViewAlarmCur.Items[i].Text);
                                    

                                }
                                listViewAlarmCur.Items.RemoveAt(i);
                            }
                        }
                    };
                //if (listViewAlarmCur.InvokeRequired)
                //{
                //    listViewAlarmCur.Invoke(action);
                //}
                if (listViewAlarmCur.IsHandleCreated)
                {
                    listViewAlarmCur.Invoke(action);
                }
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(action);
                //}
                //this.Invoke(action);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lock (objLock)
            {
                RstOtherAlarm();
            }
        }
        public void RemoveHisItem()
        {

            if (listViewAlarmHis.Items.Count > 200)
            {
                for (int i = listViewAlarmHis.Items.Count - 1; i > 100; i--)
                {
                    listViewAlarmHis.Items.RemoveAt(i);
                }
            }

        }
        private void AddAlarmToLog(string strMessage)
        {
            //TextLogWrite.AppendLog(strMessage);
        }

        private void checkBoxAlramOff_Click(object sender, EventArgs e)
        {
            if(checkBoxAlramOff.Checked)
                alarmChangedEvent(this, true, true);
            else
                alarmChangedEvent(this, true, false);
        }
        void SetListSelectItem(int iSelect)
        {
            listViewAlarmCur.Items[iSelect].Selected=true;
            listViewSuggest.Items.Clear();
            string strAlarmMessage = listViewAlarmCur.Items[iSelect].Text;
            if (alarmDictionary.ContainsKey(strAlarmMessage))
            {
                for (int i = 0; i < alarmDictionary[strAlarmMessage].listHandMessage.Count; i++)
                {
                    ListViewItem itemListView=listViewSuggest.Items.Add(alarmDictionary[strAlarmMessage].listHandMessage[i],0);
                    itemListView.SubItems.Add(alarmDictionary[strAlarmMessage].strAlarmCode);
                }
            }
        }
        private void listViewAlarmCur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewAlarmCur.SelectedItems.Count>0)
            {
                SetListSelectItem(listViewAlarmCur.SelectedItems[0].Index);
            }
        }
        private void CreateAlarmItemHeader()
        {

            if (!Directory.Exists(@".//LogFile/"))
            {
                Directory.CreateDirectory(@".//LogFile/");
            }
            string strFileName = Application.StartupPath + "//LogFile//" + DateTime.Now.ToString("yyyy-MM-dd") +".csv";
            FileInfo fileInfo = new FileInfo(strFileName);
            if (fileInfo.Exists)
            {
                
            }
            else
            {
                string[] strArrange = new string[4];
                strArrange[0] = "Message";
                strArrange[1] = "AlarmCode";
                strArrange[2] = "startTime";
                strArrange[3] = "ResetTime";
                WorldGeneralLib.CSVUtil.WriteCSV(strFileName, strArrange);            
            }
        }
        private void WriteAlarmResumeItem(AlarmItem item)
        {
            CreateAlarmItemHeader();
            string strFileName = Application.StartupPath + "//LogFile//" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            string[] strArrange = new string[4];
            strArrange[0] = item.stringAlarmMessage;
            strArrange[1] = item.strAlarmCode;
            strArrange[2] = item.strHappenTime;
            strArrange[3] = item.strResetTime;
            WorldGeneralLib.CSVUtil.WriteCSV(strFileName, strArrange);       
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            bRetry = true;
            bNeglect = false;
        }

        private void btnNeglect_Click(object sender, EventArgs e)
        {
            bNeglect = true;
            bRetry = false;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (resetMouseDownEvent!=null)
            {
                resetMouseDownEvent.Invoke();
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (resetMouseUpEvent != null)
            {
                resetMouseUpEvent.Invoke();
            }
        }
    }
}
