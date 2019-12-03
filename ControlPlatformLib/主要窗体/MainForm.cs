using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using WorldGeneralLib.InovancePLC;
using WorldGeneralLib.Cognex;
using WorldGeneralLib.CognexAsync;
using ControlPlatformLib.Models;
using System.Net;
using System.Threading;

namespace ControlPlatformLib
{
    public partial class MainForm : Form
    {
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public string m_strTitil = "激光焊接机\r\n";
        public string m_strMainWidowText = "Laser One And Two";
        public string m_strStartWindow = "主界面";
        public string m_strManualWindow = "手动";
        public string m_strParameterWindow = "任务界面";
        public string m_strMESWindow = "调试";
        public string m_strInfoQueryWindow = "InfoQuery Window";
        public string m_strSystemSetupWindow = "系统设置";
        public string m_strSN = "开始界面";

        public FormInfoQuery m_formInfoQuery;
        public Form m_formDebug;
        public Form m_formManual;
        public Form m_formParameter;
        public Form m_formStart;
        public Form m_formUserMageage;
        public MainFrm MainFrm= MainFrm.GetForm();
        public WorldGeneralLib.FormAlarm m_formAlarm;
        public delegate void SaveDelegate();
        public event SaveDelegate SaveEvent;



        private int m_iErrorStep = 0;
        private int m_iErrorFlashTime = 0;

        public bool bAuto = false;
        public bool Parse = false;
        public bool bAlarm = false;
        bool bAutoPre = false;
        public bool bEstop = false;
        public bool bAutoMode = false;
        public bool bStartPress = false;
        public bool bStopPress = false;
        public bool bResetPress = false;
        public bool bDoorOpen = false;
        public bool bHomeReady = false;
        bool bHomeReadyPre = false;
        public bool bNeedStopMotor = false;

        public bool bPre_Estop = false;
        public bool bPre_AutoMode = false;
        public bool bPre_StartPress = false;
        public bool bPre_StopPress = false;
        public bool bPre_ResetPress = false;
        public bool bPre_DoorOpen = false;
        public bool bPre_HomeReady = true;
        public bool bPre_NeedStopMotor = false;
        public int m_iUserLevel = 0;
        public bool bEmetyRun = false;

        public bool bClosing = false;

        //public FormUserLogoIn frmUserLevel;

        public Action<int> UserLevelChangedAction;
        public MainForm()
        {
            InitializeComponent();
        }
        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        //    button1.Focus();
        }
        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            if (!ControlPlatformLib.Global.bIgnoreKPIMes)
            {
                // 设备型号，项目名称，部门，设备位置，工位，上位机IP，设备状态编号（1：运行；2：故障；3：待机；4：关机；5:其他），设备状态信息，状态开始或结束时间
                ThreadPool.QueueUserWorkItem((p) => {
                    try
                    {
                        Global.m_KPIMES?.UploadEquipmentState(Environment.MachineName + ",激光清洗机,EVB,"+ ControlPlatformLib.Global.sMachinePos + "," + Global.sMachineNO + "," + Global.localaddr.MapToIPv4().ToString() + ",4,关机," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch (Exception ex)
                    {
                      //  Global.logger.Error("上传KPI出现错误:" + ex.Message);
                    }
                });
            }
            //if (MessageBox.Show("你真的要退出程序吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            bClosing = true;
            System.Threading.Thread.Sleep(500);
            m_formStart.Close();
            Environment.Exit(0);
            this.Close();
            //}
        }
       public  List<Form> listForm = new List<Form>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = m_strMainWidowText;
            VerShow.Text = m_strMainWidowText+" Ver." + System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            m_strStartWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_strSN;
            m_strManualWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_formManual.Text;
            m_strParameterWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_formParameter.Text;
            m_strMESWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_strMESWindow;
            m_strInfoQueryWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_strInfoQueryWindow;
            m_strSystemSetupWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_strSystemSetupWindow;

          //  textBoxWindowName.Text = m_strStartWindow;
            toolStripContainer1.Focus();

            DateTime dataTime = DateTime.Now;
            //   labelDateTime.Text = dataTime.ToString();
            timeShow.Text = dataTime.ToString();
            m_iUserLevel = 0;

            //DriverControlManage.StartScan();

            timerInit.Enabled = true;


            //this.Resize += new EventHandler(Main_Resize);

            //X = this.Width;
            //Y = this.Height;

            //setTag(this);
            //Main_Resize(new object(), new EventArgs());//x,y
            //this.MaximizeBox = true;
            //this.WindowState = FormWindowState.Maximized;



            this.Top = 0;

            this.Left = 0;

            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;




        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            //asc.AddControl(this);
            //asc.controlAutoSize(this);
        }
        private float X = 1024;
        private float Y = 788;
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(Main_Resize);

            X = this.Width;
            Y = this.Height;

            setTag(this);
            Main_Resize(new object(), new EventArgs());//x,y
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
        }
        private void Main_Resize(object sender, EventArgs e)   //Form automatic size
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            //setControls(newx, newy, this);
            //   this.Text = this.Width.ToString() + " " + this.Height.ToString();
            // this.Text = "Jasper Test Station";
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    //setControls(newx, newy, con);
                }
            }
        }
        public void SetSN()
        {
            Action action = () => {
                m_strStartWindow = m_strTitil + m_strMainWidowText + "\r\n" + m_strSN;
                //textBoxWindowName.Text = m_strStartWindow;
            };
            this.Invoke(action);
        }

        public void UpdaeDocInfo()
        {
            m_formInfoQuery.UpdateDocInfo();
            // m_formMES.UpdateDocInfo();
            //m_formManual.UpdateDocInfo();
            //m_formParameter.UpdateDocInfo();
            //m_formStart.UpdateDocInfo();
            //m_formUserMageage.UpdateDocInfo();
        }
        private void timerMain_Tick(object sender, EventArgs e)
        {
            //if (LoginForm.landingFinish == true)
            //{
            //    //     this.Top = 0;

            //    //  this.Left = 0;

            //    // this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //    // this.Height = Screen.PrimaryScreen.WorkingArea.Height;


            //    listForm[1].Hide();
            //    listForm[0].Width = listForm[1].Width;
            //    listForm[0].Height = listForm[1].Height;
            //    // panelMain.Controls.Clear();//移除所有控件
            //    listForm[0].TopLevel = false;
            //    //  listForm[0].FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //    panelMain.Controls.Add(listForm[0]);
            //    listForm[0].Show();
            //    listForm[0].SetBounds(listForm[1].Top, listForm[1].Left, panelMain.Width, panelMain.Height);
            //    LoginForm.landingFinish = false;
            //}



            //listForm[0]
            DateTime dataTime = DateTime.Now;
            //labelDateTime.Text = dataTime.ToString();
            timeShow.Text = dataTime.ToString();
            if (bAutoPre != bAuto)
            {
                if (bAuto)
                {
                    RunStaShow.BackColor = Color.Green;
                    //labelMachineStatus.Text = "AUTO";
                  //  labelMachineStatus.BackColor = Color.Green;
                }
                else
                {
                    RunStaShow.BackColor = Color.Red;
                   // labelMachineStatus.Text = "STOP";
                   // labelMachineStatus.BackColor = Color.Red;
                }
            }
            bAutoPre = bAuto;
            if (bHomeReadyPre != bHomeReady)
            {
                if (bHomeReady)
                {
                    toolStripStatusLabelHome.BackColor = Color.Green;
                }
                else
                {
                    toolStripStatusLabelHome.BackColor = Color.Red;
                }
            }
            if (Parse == true)
            {
                RunStaShow.BackColor = Color.Red;
           //     labelMachineStatus.Text = "STOP";
                toolStripStatusLabelHome.BackColor = Color.Red;
            }
            else
            {
                RunStaShow.BackColor = Color.Green;
                //labelMachineStatus.Text = "AUTO";
                toolStripStatusLabelHome.BackColor = Color.Green;
            }
            bHomeReadyPre = bHomeReady;
        }
        private void IntiAllForm()
        {
            m_formInfoQuery = new FormInfoQuery();
            m_formInfoQuery.TopLevel = false;
            panelMain.Controls.Add(m_formInfoQuery);
            m_formInfoQuery.Size = panelMain.Size;
            //m_formInfoQuery.mainForm = this;
            m_formInfoQuery.Hide();

            //m_formMES = new FormMES();
            m_formDebug.TopLevel = false;
            panelMain.Controls.Add(m_formDebug);
            m_formDebug.Size = panelMain.Size;
            //m_formMES.mainForm = this;
            m_formDebug.Hide();

            //m_formManual = new FormManual();
            m_formManual.TopLevel = false;
            panelMain.Controls.Add(m_formManual);
            m_formManual.Size = panelMain.Size;
            //m_formManual.mainForm = this;
            m_formManual.Hide();

            //m_formParameter = new FormParameter();
            m_formParameter.TopLevel = false;
            panelMain.Controls.Add(m_formParameter);
            m_formParameter.Size = panelMain.Size;
            //m_formParameter.mainForm = this;
            m_formParameter.Hide();

            //m_formStart = new FormStart();
            m_formStart.TopLevel = false;
            panelMain.Controls.Add(m_formStart);
            m_formStart.Size = panelMain.Size;
            //m_formStart.mainForm = this;
            m_formStart.Hide();

            // m_formUserMageage = new FormSystemSetting();
            m_formUserMageage.TopLevel = false;
            panelMain.Controls.Add(m_formUserMageage);
            m_formUserMageage.Size = panelMain.Size;
            //m_formUserMageage.mainForm = this;
            m_formUserMageage.Hide();

            MainFrm.TopLevel = false;
            panelMain.Controls.Add(MainFrm);
            MainFrm.Size = panelMain.Size;
            //m_formUserMageage.mainForm = this;
            MainFrm.Hide();

            m_formAlarm = new WorldGeneralLib.FormAlarm();
            m_formAlarm.TopLevel = false;
            panelMain.Controls.Add(m_formAlarm);
            m_formAlarm.Left = panelMain.Width / 2 - m_formAlarm.Width / 2;
            m_formAlarm.Top = panelMain.Height / 2 - m_formAlarm.Height / 2;
            m_formAlarm.Hide();

        }
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            listForm[0].Show();
            m_formInfoQuery.Hide();
            m_formDebug.Hide();
            m_formManual.Hide();
            m_formParameter.Hide();
            m_formUserMageage.Hide();
            MainFrm.Hide();
            m_formStart.Show();
       
            //textBoxWindowName.Text = m_strStartWindow;
        }
        private void toolStripButtonManual_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            //if (m_iUserLevel < 1)
            //{
            //    SetOperationError("你目前没有权限！");
            //    m_formAlarm.InsertAlarmMessage("你目前没有权限！");
            //    return;
            //}
            m_formInfoQuery.Hide();
            m_formDebug.Hide();
            m_formManual.Show();
            m_formParameter.Hide();
            m_formUserMageage.Hide();
            m_formStart.Hide();
            listForm[0].Hide();
             MainFrm.Hide();
            //textBoxWindowName.Text = m_strManualWindow;

        }
        private void toolStripButtonParameter_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            if (m_iUserLevel < 2)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            listForm[0].Hide();
            m_formInfoQuery.Hide();
            m_formDebug.Hide();
            m_formManual.Hide();
            m_formParameter.Show();
            m_formUserMageage.Hide();
            m_formStart.Hide();
            MainFrm.Hide();
            //textBoxWindowName.Text = m_strParameterWindow;
        }
        private void toolStripButtonIOMonitor_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            if (m_iUserLevel < 1)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            listForm[0].Hide();
            m_formInfoQuery.Hide();
            m_formDebug.Show();
            m_formManual.Hide();
            m_formParameter.Hide();
            m_formUserMageage.Hide();
            m_formStart.Hide();
            MainFrm.Hide();
            //textBoxWindowName.Text = m_strMESWindow;
        }
        private void toolStripButtonInfoQuery_Click(object sender, EventArgs e)
        {
            if (m_iUserLevel < 1)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            m_formInfoQuery.Show();
            m_formDebug.Hide();
            m_formManual.Hide();
            m_formParameter.Hide();
            m_formUserMageage.Hide();
            m_formStart.Hide();

            //textBoxWindowName.Text = m_strInfoQueryWindow;
        }
        private void toolStripButtonUser_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            if (m_iUserLevel < 3)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            listForm[0].Hide();
            m_formInfoQuery.Hide();
            m_formDebug.Hide();
            m_formManual.Hide();
            m_formParameter.Hide();
            m_formUserMageage.Show();
            m_formStart.Hide();
           MainFrm.Hide();
            //textBoxWindowName.Text = m_strSystemSetupWindow;
        }
        private void labelUserLevel_Click(object sender, EventArgs e)
        {
            FormUserLogoIn frmUserLevel = new FormUserLogoIn();
            frmUserLevel.ShowDialog();
            UserLevelChangedAction?.Invoke(m_iUserLevel);
        }
        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            if (m_iUserLevel < 3)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            //m_DocManageMent.NewDocAction();
            //toolStripTextBoxFilePath.Text = m_DocManageMent.m_GloabelDocumentClass.m_strFilePath;
            UpdaeDocInfo();
            //textBox1.Text = m_DocManageMent.m_MyDocumentClass.m_Param1Class.dong.ToString();
            //textBox2.Text = m_DocManageMent.m_MyDocumentClass.m_Parameter2Class.wei.ToString();
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void SaveData()
        {
            HardwareManage.hardDoc.SaveDoc();
            TableManage.tablesDoc.SaveDoc();
            IOManage.IODoc.SaveDoc();
            DataManage.m_Doc.SaveDoc();
            PathDataManage.pathDoc.SaveDoc();
            for (int i = 0; i < 5; i++)
            {
                if (InovanceManage.m_inovanceDoc[i] != null)
                {
                    InovanceManage.m_inovanceDoc[i].SaveDocument(i);
                }
            }
            CognexManage.m_cognexDoc.SaveDocument();
            CognexAsyncManage.m_cognexAsyDoc.SaveDocument();
            SetOperationError("Saving Data");

            if (SaveEvent != null)
            {
                SaveEvent.Invoke();
            }
        }
        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            if (m_iUserLevel < 2)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            //m_DocManageMent.OpenDocAction();
            //toolStripTextBoxFilePath.Text = m_DocManageMent.m_GloabelDocumentClass.m_strFilePath;
            UpdaeDocInfo();
        }
        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            //m_DocManageMent.SaveAsDocAction();
            //toolStripTextBoxFilePath.Text = m_DocManageMent.m_GloabelDocumentClass.m_strFilePath;
        }
        public void SetOperationError(string strErrorMessage)
        {
            toolStripStatusLabelMessage.Text = strErrorMessage;
            timerError.Enabled = true;
        }
        private void timerError_Tick(object sender, EventArgs e)
        {
            switch (m_iErrorStep)
            {
                case 0:
                    m_iErrorFlashTime++;
                    m_iErrorStep = 10;
                    toolStripStatusLabelMessage.BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    break;
                case 10:
                    m_iErrorStep = 20;
                    toolStripStatusLabelMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                    break;
                case 20:
                    if (m_iErrorFlashTime > 1)
                    {
                        m_iErrorFlashTime = 0;
                        m_iErrorStep = 30;
                    }
                    else
                    {
                        m_iErrorStep = 0;
                    }
                    break;
                case 30:
                    m_iErrorFlashTime = 0;
                    m_iErrorStep = 0;
                    toolStripStatusLabelMessage.Text = "就绪";
                    toolStripStatusLabelMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                    timerError.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void timerInit_Tick(object sender, EventArgs e)
        {
            timerInit.Enabled = false;

            //MainModule.LoadData();         

            //初始化界面
            label1.Text = "初始化界面中.......";
            label1.Refresh();
            IntiAllForm();
            label1.BringToFront();
            UpdaeDocInfo();

            //初始化硬件
            label1.Text = "初始化硬件中.......";
            label1.Refresh();
            label1.BringToFront();

            // MainModule.InitHardware();


            label1.Visible = false;

            m_formStart.Show();

            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.S);
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.T);
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.R);
            HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.Ctrl, Keys.E);

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ControlPlatformLib.Global.bIgnoreKPIMes)
            {
                // 设备型号，项目名称，部门，设备位置，工位，上位机IP，设备状态编号（1：运行；2：故障；3：待机；4：关机；5:其他），设备状态信息，状态开始或结束时间
                ThreadPool.QueueUserWorkItem((p) => {
                    try
                    {
                        Global.m_KPIMES?.UploadEquipmentState(Environment.MachineName + ",激光清洗机,EVB," + ControlPlatformLib.Global.sMachinePos + "," + Global.sMachineNO + "," + Global.localaddr.MapToIPv4().ToString() + ",4,关机," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch (Exception ex)
                    {
                   //     Global.logger.Error("上传KPI出现错误:" + ex.Message);
                    }
                });
            }
            //if (MessageBox.Show("你真的要退出程序吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            bClosing = true;
            System.Threading.Thread.Sleep(500);
            m_formStart.Close();
            Environment.Exit(0);
            this.Close();



            HotKey.UnregisterHotKey(Handle, 100);
            HotKey.UnregisterHotKey(Handle, 101);
            HotKey.UnregisterHotKey(Handle, 102);
            HotKey.UnregisterHotKey(Handle, 103);
            //foreach (KeyValuePair<string, HardWareBase> item in HardwareManage.hardwardDictionary)
            //{
            //    item.Value.Close();
            //}
            //m_formStart.productInfo.SaveDocAction();
            //m_formCognex.Close();
        }
        private void panelWorldLogo_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelWorldLogo_Click(object sender, EventArgs e)
        {
            if (bEmetyRun)
            {
                //panelWorldLogo.BackgroundImage = ControlPlatformLib.Properties.Resources.欣旺达logo;
                bEmetyRun = false;
            }
            else
            {
             //   panelWorldLogo.BackgroundImage = ControlPlatformLib.Properties.Resources.World1;
                bEmetyRun = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                SaveData();
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

        }

        private void MainForm_Leave(object sender, EventArgs e)
        {


        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键   
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:     //按下的是Clr+S   
                            SaveData();
                            break;
                        case 101:     //按下的是Clr+T   
                                      //bAuto = true;
                            break;
                        case 102:     //按下的是Clr+R   
                            if (timerResetKey.Enabled == false)
                            {
                                MainModule.FormMain.m_formAlarm.RstOtherAlarm();
                                MainModule.FormMain.bResetPress = true;
                                timerResetKey.Enabled = true;
                            }
                            break;
                        case 103:     //按下的是Clr+E   
                                      //bAuto = false;
                            break;
                    }

                    break;
            }
            base.WndProc(ref m);
        }
        public void SetEtopStatus(bool bOn)
        {
            Action action = () =>
                {
                    if (bOn)
                    {
                        toolStripStatusLabelEStop.BackColor = Color.Red;
                    }
                    else
                    {
                        toolStripStatusLabelEStop.BackColor = Color.Green;
                    }
                };
            statusStripMain.Invoke(action);
        }
        public void SetDoorStatus(bool bOn)
        {
            Action action = () =>
            {
                if (bOn)
                {
                    toolStripStatusLabelDoorOpen.BackColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabelDoorOpen.BackColor = Color.Green;
                }
            };
            statusStripMain.Invoke(action);
        }
        public void SetLaserStatus(bool bOn)
        {
            Action action = () =>
            {
                if (bOn)
                {
                    toolStripStatusLabelLaser.BackColor = Color.Green;
                }
                else
                {
                    toolStripStatusLabelLaser.BackColor = Color.Red;
                }
            };
            statusStripMain.Invoke(action);
        }
        public void SetHomeStatus(bool bOn)
        {
            Action action = () =>
            {
                if (bOn)
                {
                    toolStripStatusLabelHome.BackColor = Color.Green;
                }
                else
                {
                    toolStripStatusLabelHome.BackColor = Color.Red;
                }
            };
            statusStripMain.Invoke(action);
        }

        private void timerResetKey_Tick(object sender, EventArgs e)
        {
            timerResetKey.Enabled = false;
            MainModule.FormMain.bResetPress = false;
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButtonMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void 自定义流程_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            if (m_iUserLevel < 3)
            {
                SetOperationError("你目前没有权限！");
                m_formAlarm.InsertAlarmMessage("你目前没有权限！");
                return;
            }
            listForm[0].Hide();
            m_formInfoQuery.Hide();
            m_formDebug.Hide();
            m_formManual.Hide();
            m_formParameter.Hide();
            m_formUserMageage.Hide();
            m_formStart.Hide();



            MainFrm.Show();
            //textBoxWindowName.Text = m_strSystemSetupWindow;
        }

        private void UesrChanges_Click(object sender, EventArgs e)
        {
            if (LoginForm.landingOk == false)
            {
                return;
            }
            FormUserLogoIn frmUserLevel = new FormUserLogoIn();
            frmUserLevel.ShowDialog();
            UserLevelChangedAction?.Invoke(m_iUserLevel);

        }
    }


}
