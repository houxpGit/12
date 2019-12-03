namespace ControlPlatformLib
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("IO检测");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("IO检测等待");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("IO输入检测", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("单点IO输出");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("多点IO输出");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("IO输出", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("单轴运动");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("多轴运动");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("直线插补运动");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("圆弧插补运动");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("轴运动", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("串口设置");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("串口发送数据设置");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("串口接受数据设置");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("串口数据处理");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("串口数据", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("网络设置");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("网络数据发送设置");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("网络数据接受设置");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("网络数据处理");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("网络数据", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("延时");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("其他", new System.Windows.Forms.TreeNode[] {
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("调高定位");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("调高", new System.Windows.Forms.TreeNode[] {
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("拍照定位");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("拍照", new System.Windows.Forms.TreeNode[] {
            treeNode26});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.流程1 = new System.Windows.Forms.TabPage();
            this.流程2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn13 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.流程3 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn14 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.流程4 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn15 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.流程5 = new System.Windows.Forms.TabPage();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn16 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.流程6 = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn9 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn10 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn17 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.流程7 = new System.Windows.Forms.TabPage();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn11 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewComboBoxColumn11 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn12 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewButtonColumn18 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束当前流程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行当前流程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停当前流程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.向上ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_U = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.插补轴号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AsixGo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IO输入检测 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IO检测Sta = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IO输出 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IO输出STA = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IO运动 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.串口发送数据 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.串口接受 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.网络发送 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.网络接受 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.流程1.SuspendLayout();
            this.流程2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.流程3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.流程4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.流程5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.流程6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.流程7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Active.png");
            this.imageList1.Images.SetKeyName(1, "ADHHeight_Active.jpg");
            this.imageList1.Images.SetKeyName(2, "ADHWidth.jpg");
            this.imageList1.Images.SetKeyName(3, "ADHWidth_Active.jpg");
            this.imageList1.Images.SetKeyName(4, "Alarm.jpg");
            this.imageList1.Images.SetKeyName(5, "Alarm_Active.jpg");
            this.imageList1.Images.SetKeyName(6, "AlarmMsg.png");
            this.imageList1.Images.SetKeyName(7, "alignment.png");
            this.imageList1.Images.SetKeyName(8, "ARW08DN.ICO");
            this.imageList1.Images.SetKeyName(9, "ARW08LT.ICO");
            this.imageList1.Images.SetKeyName(10, "ARW08RT.ICO");
            this.imageList1.Images.SetKeyName(11, "ARW08UP.ICO");
            this.imageList1.Images.SetKeyName(12, "backFrward.png");
            this.imageList1.Images.SetKeyName(13, "background.jpg");
            this.imageList1.Images.SetKeyName(14, "background1.jpg");
            this.imageList1.Images.SetKeyName(15, "background2.jpg");
            this.imageList1.Images.SetKeyName(16, "background3.jpg");
            this.imageList1.Images.SetKeyName(17, "background4.jpg");
            this.imageList1.Images.SetKeyName(18, "backgroundImage.png");
            this.imageList1.Images.SetKeyName(19, "bar1.png");
            this.imageList1.Images.SetKeyName(20, "bar3.png");
            this.imageList1.Images.SetKeyName(21, "bar4.png");
            this.imageList1.Images.SetKeyName(22, "bigImage.png");
            this.imageList1.Images.SetKeyName(23, "BlankFile.png");
            this.imageList1.Images.SetKeyName(24, "Blue Pearl.ico");
            this.imageList1.Images.SetKeyName(25, "btn.png");
            this.imageList1.Images.SetKeyName(26, "colibri.ico");
            this.imageList1.Images.SetKeyName(27, "CopyControl.png");
            this.imageList1.Images.SetKeyName(28, "cyctime.jpg");
            this.imageList1.Images.SetKeyName(29, "Cyctime_Active.jpg");
            this.imageList1.Images.SetKeyName(30, "Data.Image.bmp");
            this.imageList1.Images.SetKeyName(31, "Data.jpg");
            this.imageList1.Images.SetKeyName(32, "Data.png");
            this.imageList1.Images.SetKeyName(33, "Data_Active.jpg");
            this.imageList1.Images.SetKeyName(34, "deleteControl.png");
            this.imageList1.Images.SetKeyName(35, "DetailImage.png");
            this.imageList1.Images.SetKeyName(36, "Device.png");
            this.imageList1.Images.SetKeyName(37, "DeviceID.jpg");
            this.imageList1.Images.SetKeyName(38, "DeviceID_Active.jpg");
            this.imageList1.Images.SetKeyName(39, "DeviceMotion.png");
            this.imageList1.Images.SetKeyName(40, "DeviceMotion5.png");
            this.imageList1.Images.SetKeyName(41, "DeviceMotion6.png");
            this.imageList1.Images.SetKeyName(42, "ErrCode.jpg");
            this.imageList1.Images.SetKeyName(43, "ErrCode_Active.jpg");
            this.imageList1.Images.SetKeyName(44, "ErrorCode_alarm.jpg");
            this.imageList1.Images.SetKeyName(45, "Excel.jpg");
            this.imageList1.Images.SetKeyName(46, "Exit21.png");
            this.imageList1.Images.SetKeyName(47, "exitControl.png");
            this.imageList1.Images.SetKeyName(48, "FileFolder.png");
            this.imageList1.Images.SetKeyName(49, "FrmDebug.ico");
            this.imageList1.Images.SetKeyName(50, "FrmHome1.png");
            this.imageList1.Images.SetKeyName(51, "FrmSetting3.png");
            this.imageList1.Images.SetKeyName(52, "Gap.bmp");
            this.imageList1.Images.SetKeyName(53, "Gap1.bmp");
            this.imageList1.Images.SetKeyName(54, "Green Pearl.ico");
            this.imageList1.Images.SetKeyName(55, "Green.jpg");
            this.imageList1.Images.SetKeyName(56, "GreenAlarmLED.png");
            this.imageList1.Images.SetKeyName(57, "HoldPress.jpg");
            this.imageList1.Images.SetKeyName(58, "HoldPress_Active.jpg");
            this.imageList1.Images.SetKeyName(59, "Home.jpg");
            this.imageList1.Images.SetKeyName(60, "Home_Active.jpg");
            this.imageList1.Images.SetKeyName(61, "HSGHeight.jpg");
            this.imageList1.Images.SetKeyName(62, "HSGHeight_Active.jpg");
            this.imageList1.Images.SetKeyName(63, "HSGWidth.jpg");
            this.imageList1.Images.SetKeyName(64, "HSGWidth_Active .jpg");
            this.imageList1.Images.SetKeyName(65, "Image.jpg");
            this.imageList1.Images.SetKeyName(66, "InitDevice.bmp");
            this.imageList1.Images.SetKeyName(67, "ListImage.png");
            this.imageList1.Images.SetKeyName(68, "Login.jpg");
            this.imageList1.Images.SetKeyName(69, "Login_Active.jpg");
            this.imageList1.Images.SetKeyName(70, "LogoNoMargin.jpg");
            this.imageList1.Images.SetKeyName(71, "LogoTrans.png");
            this.imageList1.Images.SetKeyName(72, "machine1.ico");
            this.imageList1.Images.SetKeyName(73, "machine2.ico");
            this.imageList1.Images.SetKeyName(74, "Mechno-2.ico");
            this.imageList1.Images.SetKeyName(75, "Mechno-8.ico");
            this.imageList1.Images.SetKeyName(76, "MotionDebug.png");
            this.imageList1.Images.SetKeyName(77, "MyComputer.png");
            this.imageList1.Images.SetKeyName(78, "MyDeskTop.png");
            this.imageList1.Images.SetKeyName(79, "MyDriver.png");
            this.imageList1.Images.SetKeyName(80, "MyFolder.png");
            this.imageList1.Images.SetKeyName(81, "OpenFileFolder.png");
            this.imageList1.Images.SetKeyName(82, "ORB_Icons_by_001.ico");
            this.imageList1.Images.SetKeyName(83, "ORB_Icons_by_001.png");
            this.imageList1.Images.SetKeyName(84, "param.jpg");
            this.imageList1.Images.SetKeyName(85, "param_active.jpg");
            this.imageList1.Images.SetKeyName(86, "PasterControl.png");
            this.imageList1.Images.SetKeyName(87, "PastPress.jpg");
            this.imageList1.Images.SetKeyName(88, "PastPress_Active.jpg");
            this.imageList1.Images.SetKeyName(89, "pause.jpg");
            this.imageList1.Images.SetKeyName(90, "Pause_active_main.jpg");
            this.imageList1.Images.SetKeyName(91, "PickPostion.png");
            this.imageList1.Images.SetKeyName(92, "RBT_back.bmp");
            this.imageList1.Images.SetKeyName(93, "Red.jpg");
            this.imageList1.Images.SetKeyName(94, "refresh.png");
            this.imageList1.Images.SetKeyName(95, "reName.png");
            this.imageList1.Images.SetKeyName(96, "Save.jpg");
            this.imageList1.Images.SetKeyName(97, "Security Center.ico");
            this.imageList1.Images.SetKeyName(98, "smallImage.png");
            this.imageList1.Images.SetKeyName(99, "start.jpg");
            this.imageList1.Images.SetKeyName(100, "Start_active_main.jpg");
            this.imageList1.Images.SetKeyName(101, "stop.jpg");
            this.imageList1.Images.SetKeyName(102, "Stop_active_main.jpg");
            this.imageList1.Images.SetKeyName(103, "taillImage.png");
            this.imageList1.Images.SetKeyName(104, "UnActive.png");
            this.imageList1.Images.SetKeyName(105, "UnDo.png");
            this.imageList1.Images.SetKeyName(106, "UPH.jpg");
            this.imageList1.Images.SetKeyName(107, "UPH_main.jpg");
            this.imageList1.Images.SetKeyName(108, "UPH_main1.jpg");
            this.imageList1.Images.SetKeyName(109, "UserAdmin.png");
            this.imageList1.Images.SetKeyName(110, "UserNormal.png");
            this.imageList1.Images.SetKeyName(111, "UserSystemAdmin.png");
            this.imageList1.Images.SetKeyName(112, "Vision.jpg");
            this.imageList1.Images.SetKeyName(113, "Vision_Active.jpg");
            this.imageList1.Images.SetKeyName(114, "Vision_Active1.jpg");
            this.imageList1.Images.SetKeyName(115, "Vision1.jpg");
            this.imageList1.Images.SetKeyName(116, "wm-icon-glory.ico");
            this.imageList1.Images.SetKeyName(117, "Yeild1.jpg");
            this.imageList1.Images.SetKeyName(118, "YellowAlarmLED.jpg");
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(4, 22);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 9;
            treeNode1.Name = "IO检测";
            treeNode1.Text = "IO检测";
            treeNode2.ImageIndex = 10;
            treeNode2.Name = "IO检测等待";
            treeNode2.Text = "IO检测等待";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "IO输入检测";
            treeNode3.Text = "IO输入检测";
            treeNode4.ImageIndex = 15;
            treeNode4.Name = "单点IO输出";
            treeNode4.Text = "单点IO输出";
            treeNode5.ImageIndex = 16;
            treeNode5.Name = "多点IO输出";
            treeNode5.Text = "多点IO输出";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "IO输出";
            treeNode6.Text = "IO输出";
            treeNode7.ImageIndex = 22;
            treeNode7.Name = "单轴运动";
            treeNode7.Text = "单轴运动";
            treeNode8.ImageIndex = 23;
            treeNode8.Name = "多轴运动";
            treeNode8.Text = "多轴运动";
            treeNode9.ImageIndex = 25;
            treeNode9.Name = "直线插补运动";
            treeNode9.Text = "直线插补运动";
            treeNode10.ImageIndex = 26;
            treeNode10.Name = "圆弧插补运动";
            treeNode10.Text = "圆弧插补运动";
            treeNode11.ImageIndex = 3;
            treeNode11.Name = "轴运动";
            treeNode11.Text = "轴运动";
            treeNode12.ImageIndex = 27;
            treeNode12.Name = "串口设置";
            treeNode12.Text = "串口设置";
            treeNode13.ImageIndex = 28;
            treeNode13.Name = "串口发送数据设置";
            treeNode13.Text = "串口发送数据设置";
            treeNode14.ImageIndex = 29;
            treeNode14.Name = "串口接受数据设置";
            treeNode14.Text = "串口接受数据设置";
            treeNode15.ImageIndex = 30;
            treeNode15.Name = "串口数据处理";
            treeNode15.Text = "串口数据处理";
            treeNode16.ImageIndex = 4;
            treeNode16.Name = "串口数据";
            treeNode16.Text = "串口数据";
            treeNode17.ImageIndex = 31;
            treeNode17.Name = "网络设置";
            treeNode17.Text = "网络设置";
            treeNode18.ImageIndex = 32;
            treeNode18.Name = "网络数据发送设置";
            treeNode18.Text = "网络数据发送设置";
            treeNode19.ImageIndex = 33;
            treeNode19.Name = "网络数据接受设置";
            treeNode19.Text = "网络数据接受设置";
            treeNode20.ImageIndex = 34;
            treeNode20.Name = "网络数据处理";
            treeNode20.Text = "网络数据处理";
            treeNode21.ImageIndex = 5;
            treeNode21.Name = "网络数据";
            treeNode21.Text = "网络数据";
            treeNode22.Name = "延时";
            treeNode22.Text = "延时";
            treeNode23.Name = "其他";
            treeNode23.Text = "其他";
            treeNode24.Name = "调高定位";
            treeNode24.Text = "调高定位";
            treeNode25.Name = "调高";
            treeNode25.Text = "调高";
            treeNode26.Name = "拍照定位";
            treeNode26.Text = "拍照定位";
            treeNode27.Name = "拍照";
            treeNode27.Text = "拍照";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode11,
            treeNode16,
            treeNode21,
            treeNode23,
            treeNode25,
            treeNode27});
            this.treeView1.Size = new System.Drawing.Size(187, 792);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(195, 818);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具栏";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Current_X,
            this.Current_Y,
            this.Current_Z,
            this.Current_U,
            this.插补轴号,
            this.Column6,
            this.AsixGo,
            this.IO输入检测,
            this.IO检测Sta,
            this.IO输出,
            this.IO输出STA,
            this.IO运动,
            this.串口发送数据,
            this.串口接受,
            this.网络发送,
            this.网络接受});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1525, 781);
            this.dataGridView1.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.流程1);
            this.tabControl1.Controls.Add(this.流程2);
            this.tabControl1.Controls.Add(this.流程3);
            this.tabControl1.Controls.Add(this.流程4);
            this.tabControl1.Controls.Add(this.流程5);
            this.tabControl1.Controls.Add(this.流程6);
            this.tabControl1.Controls.Add(this.流程7);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(207, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1541, 818);
            this.tabControl1.TabIndex = 19;
            // 
            // 流程1
            // 
            this.流程1.Controls.Add(this.dataGridView1);
            this.流程1.Location = new System.Drawing.Point(4, 25);
            this.流程1.Margin = new System.Windows.Forms.Padding(4);
            this.流程1.Name = "流程1";
            this.流程1.Padding = new System.Windows.Forms.Padding(4);
            this.流程1.Size = new System.Drawing.Size(1533, 789);
            this.流程1.TabIndex = 0;
            this.流程1.Text = "流程1";
            this.流程1.UseVisualStyleBackColor = true;
            // 
            // 流程2
            // 
            this.流程2.Controls.Add(this.dataGridView2);
            this.流程2.Location = new System.Drawing.Point(4, 25);
            this.流程2.Margin = new System.Windows.Forms.Padding(4);
            this.流程2.Name = "流程2";
            this.流程2.Padding = new System.Windows.Forms.Padding(4);
            this.流程2.Size = new System.Drawing.Size(1533, 789);
            this.流程2.TabIndex = 1;
            this.流程2.Text = "流程2";
            this.流程2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewButtonColumn1,
            this.dataGridViewButtonColumn2,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewButtonColumn13,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(4, 4);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 20;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1525, 781);
            this.dataGridView2.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "X";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Y";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Z";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "U";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "Get";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Width = 50;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            // 
            // dataGridViewButtonColumn13
            // 
            this.dataGridViewButtonColumn13.HeaderText = "Go";
            this.dataGridViewButtonColumn13.Name = "dataGridViewButtonColumn13";
            this.dataGridViewButtonColumn13.Width = 50;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            // 
            // 流程3
            // 
            this.流程3.Controls.Add(this.dataGridView3);
            this.流程3.Location = new System.Drawing.Point(4, 25);
            this.流程3.Name = "流程3";
            this.流程3.Size = new System.Drawing.Size(1533, 789);
            this.流程3.TabIndex = 2;
            this.流程3.Text = "流程3";
            this.流程3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewButtonColumn3,
            this.dataGridViewButtonColumn4,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewComboBoxColumn4,
            this.dataGridViewButtonColumn14,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 20;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(1533, 789);
            this.dataGridView3.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn6.HeaderText = "名称";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "X";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Y";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Z";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "U";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 80;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.HeaderText = "Get";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.Width = 50;
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            // 
            // dataGridViewButtonColumn14
            // 
            this.dataGridViewButtonColumn14.HeaderText = "Go";
            this.dataGridViewButtonColumn14.Name = "dataGridViewButtonColumn14";
            this.dataGridViewButtonColumn14.Width = 50;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            // 
            // 流程4
            // 
            this.流程4.Controls.Add(this.dataGridView4);
            this.流程4.Location = new System.Drawing.Point(4, 25);
            this.流程4.Name = "流程4";
            this.流程4.Size = new System.Drawing.Size(1533, 789);
            this.流程4.TabIndex = 3;
            this.流程4.Text = "流程4";
            this.流程4.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewButtonColumn5,
            this.dataGridViewButtonColumn6,
            this.dataGridViewComboBoxColumn5,
            this.dataGridViewComboBoxColumn6,
            this.dataGridViewButtonColumn15,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45});
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 20;
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.Size = new System.Drawing.Size(1533, 789);
            this.dataGridView4.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn11.HeaderText = "名称";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "X";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 80;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Y";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 80;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Z";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 80;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "U";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            // 
            // dataGridViewButtonColumn5
            // 
            this.dataGridViewButtonColumn5.HeaderText = "Get";
            this.dataGridViewButtonColumn5.Name = "dataGridViewButtonColumn5";
            this.dataGridViewButtonColumn5.Width = 50;
            // 
            // dataGridViewButtonColumn6
            // 
            this.dataGridViewButtonColumn6.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn6.Name = "dataGridViewButtonColumn6";
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            this.dataGridViewComboBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn6
            // 
            this.dataGridViewComboBoxColumn6.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
            // 
            // dataGridViewButtonColumn15
            // 
            this.dataGridViewButtonColumn15.HeaderText = "Go";
            this.dataGridViewButtonColumn15.Name = "dataGridViewButtonColumn15";
            this.dataGridViewButtonColumn15.Width = 50;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            // 
            // 流程5
            // 
            this.流程5.Controls.Add(this.dataGridView5);
            this.流程5.Location = new System.Drawing.Point(4, 25);
            this.流程5.Name = "流程5";
            this.流程5.Size = new System.Drawing.Size(1533, 789);
            this.流程5.TabIndex = 4;
            this.流程5.Text = "流程5";
            this.流程5.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewButtonColumn7,
            this.dataGridViewButtonColumn8,
            this.dataGridViewComboBoxColumn7,
            this.dataGridViewComboBoxColumn8,
            this.dataGridViewButtonColumn16,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50});
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView5.Location = new System.Drawing.Point(0, 0);
            this.dataGridView5.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView5.MultiSelect = false;
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersWidth = 20;
            this.dataGridView5.RowTemplate.Height = 23;
            this.dataGridView5.Size = new System.Drawing.Size(1533, 789);
            this.dataGridView5.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn16.HeaderText = "名称";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "X";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 80;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "Y";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Width = 80;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Z";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.Width = 80;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "U";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.Width = 80;
            // 
            // dataGridViewTextBoxColumn46
            // 
            this.dataGridViewTextBoxColumn46.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            // 
            // dataGridViewButtonColumn7
            // 
            this.dataGridViewButtonColumn7.HeaderText = "Get";
            this.dataGridViewButtonColumn7.Name = "dataGridViewButtonColumn7";
            this.dataGridViewButtonColumn7.Width = 50;
            // 
            // dataGridViewButtonColumn8
            // 
            this.dataGridViewButtonColumn8.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn8.Name = "dataGridViewButtonColumn8";
            // 
            // dataGridViewComboBoxColumn7
            // 
            this.dataGridViewComboBoxColumn7.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
            this.dataGridViewComboBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn8
            // 
            this.dataGridViewComboBoxColumn8.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn8.Name = "dataGridViewComboBoxColumn8";
            // 
            // dataGridViewButtonColumn16
            // 
            this.dataGridViewButtonColumn16.HeaderText = "Go";
            this.dataGridViewButtonColumn16.Name = "dataGridViewButtonColumn16";
            this.dataGridViewButtonColumn16.Width = 50;
            // 
            // dataGridViewTextBoxColumn47
            // 
            this.dataGridViewTextBoxColumn47.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            // 
            // 流程6
            // 
            this.流程6.Controls.Add(this.dataGridView6);
            this.流程6.Location = new System.Drawing.Point(4, 25);
            this.流程6.Name = "流程6";
            this.流程6.Size = new System.Drawing.Size(1533, 789);
            this.流程6.TabIndex = 5;
            this.流程6.Text = "流程6";
            this.流程6.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewButtonColumn9,
            this.dataGridViewButtonColumn10,
            this.dataGridViewComboBoxColumn9,
            this.dataGridViewComboBoxColumn10,
            this.dataGridViewButtonColumn17,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn55});
            this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView6.Location = new System.Drawing.Point(0, 0);
            this.dataGridView6.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowHeadersWidth = 20;
            this.dataGridView6.RowTemplate.Height = 23;
            this.dataGridView6.Size = new System.Drawing.Size(1533, 789);
            this.dataGridView6.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn21.HeaderText = "名称";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "X";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.Width = 80;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "Y";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.Width = 80;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "Z";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.Width = 80;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "U";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 80;
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            // 
            // dataGridViewButtonColumn9
            // 
            this.dataGridViewButtonColumn9.HeaderText = "Get";
            this.dataGridViewButtonColumn9.Name = "dataGridViewButtonColumn9";
            this.dataGridViewButtonColumn9.Width = 50;
            // 
            // dataGridViewButtonColumn10
            // 
            this.dataGridViewButtonColumn10.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn10.Name = "dataGridViewButtonColumn10";
            // 
            // dataGridViewComboBoxColumn9
            // 
            this.dataGridViewComboBoxColumn9.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn9.Name = "dataGridViewComboBoxColumn9";
            this.dataGridViewComboBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn10
            // 
            this.dataGridViewComboBoxColumn10.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn10.Name = "dataGridViewComboBoxColumn10";
            // 
            // dataGridViewButtonColumn17
            // 
            this.dataGridViewButtonColumn17.HeaderText = "Go";
            this.dataGridViewButtonColumn17.Name = "dataGridViewButtonColumn17";
            this.dataGridViewButtonColumn17.Width = 50;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            // 
            // 流程7
            // 
            this.流程7.Controls.Add(this.dataGridView7);
            this.流程7.Location = new System.Drawing.Point(4, 25);
            this.流程7.Name = "流程7";
            this.流程7.Size = new System.Drawing.Size(1533, 789);
            this.流程7.TabIndex = 6;
            this.流程7.Text = "流程7";
            this.流程7.UseVisualStyleBackColor = true;
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView7.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView7.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewButtonColumn11,
            this.dataGridViewButtonColumn12,
            this.dataGridViewComboBoxColumn11,
            this.dataGridViewComboBoxColumn12,
            this.dataGridViewButtonColumn18,
            this.dataGridViewTextBoxColumn57,
            this.dataGridViewTextBoxColumn58,
            this.dataGridViewTextBoxColumn59,
            this.dataGridViewTextBoxColumn60});
            this.dataGridView7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView7.Location = new System.Drawing.Point(0, 0);
            this.dataGridView7.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView7.MultiSelect = false;
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.RowHeadersWidth = 20;
            this.dataGridView7.RowTemplate.Height = 23;
            this.dataGridView7.Size = new System.Drawing.Size(1533, 789);
            this.dataGridView7.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn26.HeaderText = "名称";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "X";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.Width = 80;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.HeaderText = "Y";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.Width = 80;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "Z";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.Width = 80;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "U";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.Width = 80;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.HeaderText = "插补轴号";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            // 
            // dataGridViewButtonColumn11
            // 
            this.dataGridViewButtonColumn11.HeaderText = "Get";
            this.dataGridViewButtonColumn11.Name = "dataGridViewButtonColumn11";
            this.dataGridViewButtonColumn11.Width = 50;
            // 
            // dataGridViewButtonColumn12
            // 
            this.dataGridViewButtonColumn12.HeaderText = "AsixGo";
            this.dataGridViewButtonColumn12.Name = "dataGridViewButtonColumn12";
            // 
            // dataGridViewComboBoxColumn11
            // 
            this.dataGridViewComboBoxColumn11.HeaderText = "IO输入检测";
            this.dataGridViewComboBoxColumn11.Name = "dataGridViewComboBoxColumn11";
            this.dataGridViewComboBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn12
            // 
            this.dataGridViewComboBoxColumn12.HeaderText = "IO输出";
            this.dataGridViewComboBoxColumn12.Name = "dataGridViewComboBoxColumn12";
            // 
            // dataGridViewButtonColumn18
            // 
            this.dataGridViewButtonColumn18.HeaderText = "Go";
            this.dataGridViewButtonColumn18.Name = "dataGridViewButtonColumn18";
            this.dataGridViewButtonColumn18.Width = 50;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.HeaderText = "串口发送数据";
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            // 
            // dataGridViewTextBoxColumn58
            // 
            this.dataGridViewTextBoxColumn58.HeaderText = "串口接受";
            this.dataGridViewTextBoxColumn58.Name = "dataGridViewTextBoxColumn58";
            // 
            // dataGridViewTextBoxColumn59
            // 
            this.dataGridViewTextBoxColumn59.HeaderText = "网络发送";
            this.dataGridViewTextBoxColumn59.Name = "dataGridViewTextBoxColumn59";
            // 
            // dataGridViewTextBoxColumn60
            // 
            this.dataGridViewTextBoxColumn60.HeaderText = "网络接受";
            this.dataGridViewTextBoxColumn60.Name = "dataGridViewTextBoxColumn60";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.58676F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.41324F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 826F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1752, 826);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.结束当前流程ToolStripMenuItem,
            this.运行当前流程ToolStripMenuItem,
            this.暂停当前流程ToolStripMenuItem,
            this.向上ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1752, 28);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存ToolStripMenuItem.Image")));
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 结束当前流程ToolStripMenuItem
            // 
            this.结束当前流程ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("结束当前流程ToolStripMenuItem.Image")));
            this.结束当前流程ToolStripMenuItem.Name = "结束当前流程ToolStripMenuItem";
            this.结束当前流程ToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.结束当前流程ToolStripMenuItem.Text = "结束当前流程";
            // 
            // 运行当前流程ToolStripMenuItem
            // 
            this.运行当前流程ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("运行当前流程ToolStripMenuItem.Image")));
            this.运行当前流程ToolStripMenuItem.Name = "运行当前流程ToolStripMenuItem";
            this.运行当前流程ToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.运行当前流程ToolStripMenuItem.Text = "运行当前流程";
            // 
            // 暂停当前流程ToolStripMenuItem
            // 
            this.暂停当前流程ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("暂停当前流程ToolStripMenuItem.Image")));
            this.暂停当前流程ToolStripMenuItem.Name = "暂停当前流程ToolStripMenuItem";
            this.暂停当前流程ToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.暂停当前流程ToolStripMenuItem.Text = "暂停当前流程";
            // 
            // 向上ToolStripMenuItem
            // 
            this.向上ToolStripMenuItem.Image = global::ControlPlatformLib.Properties.Resources.xp1g_30;
            this.向上ToolStripMenuItem.Name = "向上ToolStripMenuItem";
            this.向上ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.向上ToolStripMenuItem.Text = "上移";
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Image = global::ControlPlatformLib.Properties.Resources.xp1g_40;
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.下移ToolStripMenuItem.Text = "下移";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem.Image")));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Current_X
            // 
            this.Current_X.HeaderText = "X";
            this.Current_X.Name = "Current_X";
            this.Current_X.Width = 80;
            // 
            // Current_Y
            // 
            this.Current_Y.HeaderText = "Y";
            this.Current_Y.Name = "Current_Y";
            this.Current_Y.Width = 80;
            // 
            // Current_Z
            // 
            this.Current_Z.HeaderText = "Z";
            this.Current_Z.Name = "Current_Z";
            this.Current_Z.Width = 80;
            // 
            // Current_U
            // 
            this.Current_U.HeaderText = "U";
            this.Current_U.Name = "Current_U";
            this.Current_U.Width = 80;
            // 
            // 插补轴号
            // 
            this.插补轴号.HeaderText = "插补轴号";
            this.插补轴号.Name = "插补轴号";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Get";
            this.Column6.Name = "Column6";
            this.Column6.Width = 50;
            // 
            // AsixGo
            // 
            this.AsixGo.HeaderText = "AsixGo";
            this.AsixGo.Name = "AsixGo";
            // 
            // IO输入检测
            // 
            this.IO输入检测.HeaderText = "IO输入检测";
            this.IO输入检测.Name = "IO输入检测";
            this.IO输入检测.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IO输入检测.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IO检测Sta
            // 
            this.IO检测Sta.HeaderText = "IO检测状态";
            this.IO检测Sta.Name = "IO检测Sta";
            // 
            // IO输出
            // 
            this.IO输出.HeaderText = "IO输出";
            this.IO输出.Name = "IO输出";
            // 
            // IO输出STA
            // 
            this.IO输出STA.HeaderText = "IO输出状态";
            this.IO输出STA.Name = "IO输出STA";
            // 
            // IO运动
            // 
            this.IO运动.HeaderText = "Go";
            this.IO运动.Name = "IO运动";
            this.IO运动.Width = 50;
            // 
            // 串口发送数据
            // 
            this.串口发送数据.HeaderText = "串口发送数据";
            this.串口发送数据.Name = "串口发送数据";
            // 
            // 串口接受
            // 
            this.串口接受.HeaderText = "串口接受";
            this.串口接受.Name = "串口接受";
            // 
            // 网络发送
            // 
            this.网络发送.HeaderText = "网络发送";
            this.网络发送.Name = "网络发送";
            // 
            // 网络接受
            // 
            this.网络接受.HeaderText = "网络接受";
            this.网络接受.Name = "网络接受";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1752, 854);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.流程1.ResumeLayout(false);
            this.流程2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.流程3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.流程4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.流程5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.流程6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.流程7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 流程1;
        private System.Windows.Forms.TabPage 流程2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束当前流程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行当前流程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 暂停当前流程ToolStripMenuItem;
        private System.Windows.Forms.TabPage 流程3;
        private System.Windows.Forms.TabPage 流程4;
        private System.Windows.Forms.TabPage 流程5;
        private System.Windows.Forms.TabPage 流程6;
        private System.Windows.Forms.TabPage 流程7;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn7;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn8;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn8;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn9;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn10;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn9;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn10;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridView dataGridView7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn11;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn12;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn11;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn12;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private System.Windows.Forms.ToolStripMenuItem 向上ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_U;
        private System.Windows.Forms.DataGridViewTextBoxColumn 插补轴号;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn AsixGo;
        private System.Windows.Forms.DataGridViewComboBoxColumn IO输入检测;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IO检测Sta;
        private System.Windows.Forms.DataGridViewComboBoxColumn IO输出;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IO输出STA;
        private System.Windows.Forms.DataGridViewButtonColumn IO运动;
        private System.Windows.Forms.DataGridViewTextBoxColumn 串口发送数据;
        private System.Windows.Forms.DataGridViewTextBoxColumn 串口接受;
        private System.Windows.Forms.DataGridViewTextBoxColumn 网络发送;
        private System.Windows.Forms.DataGridViewTextBoxColumn 网络接受;
    }
}