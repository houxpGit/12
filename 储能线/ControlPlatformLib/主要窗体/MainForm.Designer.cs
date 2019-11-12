using System;

namespace ControlPlatformLib
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            {

            }
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.VerShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.RunStaShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDoorOpen = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelEStop = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelHome = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLaser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMini = new System.Windows.Forms.ToolStripButton();
            this.新建NToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打开OToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.打印PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonManual = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonParameter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMES = new System.Windows.Forms.ToolStripButton();
            this.自定义流程 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUser = new System.Windows.Forms.ToolStripButton();
            this.UesrChanges = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxFilePath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInfoQuery = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timerError = new System.Windows.Forms.Timer(this.components);
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.timerResetKey = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.statusStripMain);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStripMain);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMain);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label3);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label2);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1284, 882);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1284, 882);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMessage,
            this.VerShow,
            this.RunStaShow,
            this.timeShow,
            this.toolStripStatusLabelDoorOpen,
            this.toolStripStatusLabelEStop,
            this.toolStripStatusLabelHome,
            this.toolStripStatusLabelLaser});
            this.statusStripMain.Location = new System.Drawing.Point(0, 837);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStripMain.Size = new System.Drawing.Size(1284, 45);
            this.statusStripMain.TabIndex = 10;
            // 
            // toolStripStatusLabelMessage
            // 
            this.toolStripStatusLabelMessage.Name = "toolStripStatusLabelMessage";
            this.toolStripStatusLabelMessage.Size = new System.Drawing.Size(618, 40);
            this.toolStripStatusLabelMessage.Spring = true;
            this.toolStripStatusLabelMessage.Text = "就绪";
            this.toolStripStatusLabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VerShow
            // 
            this.VerShow.BackColor = System.Drawing.Color.Green;
            this.VerShow.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.VerShow.Name = "VerShow";
            this.VerShow.Size = new System.Drawing.Size(78, 40);
            this.VerShow.Text = "VerShow";
            // 
            // RunStaShow
            // 
            this.RunStaShow.BackColor = System.Drawing.Color.Red;
            this.RunStaShow.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.RunStaShow.Name = "RunStaShow";
            this.RunStaShow.Size = new System.Drawing.Size(104, 40);
            this.RunStaShow.Text = "RunStaShow";
            // 
            // timeShow
            // 
            this.timeShow.AutoSize = false;
            this.timeShow.BackColor = System.Drawing.Color.Red;
            this.timeShow.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.timeShow.Name = "timeShow";
            this.timeShow.Size = new System.Drawing.Size(200, 40);
            this.timeShow.Text = "timeShow";
            // 
            // toolStripStatusLabelDoorOpen
            // 
            this.toolStripStatusLabelDoorOpen.BackColor = System.Drawing.Color.Green;
            this.toolStripStatusLabelDoorOpen.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelDoorOpen.Name = "toolStripStatusLabelDoorOpen";
            this.toolStripStatusLabelDoorOpen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabelDoorOpen.Size = new System.Drawing.Size(58, 40);
            this.toolStripStatusLabelDoorOpen.Text = "门开关";
            // 
            // toolStripStatusLabelEStop
            // 
            this.toolStripStatusLabelEStop.BackColor = System.Drawing.Color.Green;
            this.toolStripStatusLabelEStop.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelEStop.Name = "toolStripStatusLabelEStop";
            this.toolStripStatusLabelEStop.Size = new System.Drawing.Size(43, 40);
            this.toolStripStatusLabelEStop.Text = "急停";
            // 
            // toolStripStatusLabelHome
            // 
            this.toolStripStatusLabelHome.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelHome.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelHome.Name = "toolStripStatusLabelHome";
            this.toolStripStatusLabelHome.Size = new System.Drawing.Size(60, 40);
            this.toolStripStatusLabelHome.Text = "HOME";
            // 
            // toolStripStatusLabelLaser
            // 
            this.toolStripStatusLabelLaser.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelLaser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelLaser.Name = "toolStripStatusLabelLaser";
            this.toolStripStatusLabelLaser.Size = new System.Drawing.Size(103, 40);
            this.toolStripStatusLabelLaser.Text = "激光器准备好";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMini,
            this.新建NToolStripButton,
            this.打开OToolStripButton,
            this.toolStripSeparator1,
            this.打印PToolStripButton,
            this.toolStripButtonStart,
            this.toolStripButtonManual,
            this.toolStripButtonParameter,
            this.toolStripButtonMES,
            this.自定义流程,
            this.toolStripButtonUser,
            this.UesrChanges,
            this.保存SToolStripButton,
            this.toolStripLabel1,
            this.toolStripTextBoxFilePath,
            this.toolStripButtonExit,
            this.toolStripButtonInfoQuery});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1284, 27);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
            // 
            // toolStripButtonMini
            // 
            this.toolStripButtonMini.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMini.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMini.Image")));
            this.toolStripButtonMini.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMini.Name = "toolStripButtonMini";
            this.toolStripButtonMini.Size = new System.Drawing.Size(72, 24);
            this.toolStripButtonMini.Text = "__最小化";
            this.toolStripButtonMini.ToolTipText = "最小化";
            this.toolStripButtonMini.Visible = false;
            this.toolStripButtonMini.Click += new System.EventHandler(this.toolStripButtonMini_Click);
            // 
            // 新建NToolStripButton
            // 
            this.新建NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.新建NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripButton.Image")));
            this.新建NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripButton.Name = "新建NToolStripButton";
            this.新建NToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.新建NToolStripButton.Text = "新建(&N)";
            this.新建NToolStripButton.Visible = false;
            this.新建NToolStripButton.Click += new System.EventHandler(this.新建NToolStripButton_Click);
            // 
            // 打开OToolStripButton
            // 
            this.打开OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打开OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripButton.Image")));
            this.打开OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripButton.Name = "打开OToolStripButton";
            this.打开OToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.打开OToolStripButton.Text = "打开(&O)";
            this.打开OToolStripButton.Visible = false;
            this.打开OToolStripButton.Click += new System.EventHandler(this.打开OToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // 打印PToolStripButton
            // 
            this.打印PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打印PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打印PToolStripButton.Image")));
            this.打印PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打印PToolStripButton.Name = "打印PToolStripButton";
            this.打印PToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.打印PToolStripButton.Text = "另存为";
            this.打印PToolStripButton.Visible = false;
            this.打印PToolStripButton.Click += new System.EventHandler(this.打印PToolStripButton_Click);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.AutoSize = false;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(115, 22);
            this.toolStripButtonStart.Text = "   开始界面    ";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonManual
            // 
            this.toolStripButtonManual.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonManual.Image")));
            this.toolStripButtonManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonManual.Name = "toolStripButtonManual";
            this.toolStripButtonManual.Size = new System.Drawing.Size(125, 24);
            this.toolStripButtonManual.Text = "    视觉界面    ";
            this.toolStripButtonManual.Click += new System.EventHandler(this.toolStripButtonManual_Click);
            // 
            // toolStripButtonParameter
            // 
            this.toolStripButtonParameter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonParameter.Image")));
            this.toolStripButtonParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonParameter.Name = "toolStripButtonParameter";
            this.toolStripButtonParameter.Size = new System.Drawing.Size(125, 24);
            this.toolStripButtonParameter.Text = "    设置界面    ";
            this.toolStripButtonParameter.Click += new System.EventHandler(this.toolStripButtonParameter_Click);
            // 
            // toolStripButtonMES
            // 
            this.toolStripButtonMES.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMES.Image")));
            this.toolStripButtonMES.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMES.Name = "toolStripButtonMES";
            this.toolStripButtonMES.Size = new System.Drawing.Size(93, 24);
            this.toolStripButtonMES.Text = "调试界面";
            this.toolStripButtonMES.Click += new System.EventHandler(this.toolStripButtonIOMonitor_Click);
            // 
            // 自定义流程
            // 
            this.自定义流程.AutoSize = false;
            this.自定义流程.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.自定义流程.Image = global::ControlPlatformLib.Properties.Resources.notepad_ok_128px_1170517_easyicon_net;
            this.自定义流程.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.自定义流程.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.自定义流程.Name = "自定义流程";
            this.自定义流程.Size = new System.Drawing.Size(100, 22);
            this.自定义流程.Tag = "";
            this.自定义流程.Text = "自定义流程";
            this.自定义流程.ToolTipText = "自定义流程";
            this.自定义流程.Click += new System.EventHandler(this.自定义流程_Click);
            // 
            // toolStripButtonUser
            // 
            this.toolStripButtonUser.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUser.Image")));
            this.toolStripButtonUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUser.Name = "toolStripButtonUser";
            this.toolStripButtonUser.Size = new System.Drawing.Size(117, 24);
            this.toolStripButtonUser.Text = "    系统设定  ";
            this.toolStripButtonUser.Click += new System.EventHandler(this.toolStripButtonUser_Click);
            // 
            // UesrChanges
            // 
            this.UesrChanges.Image = global::ControlPlatformLib.Properties.Resources.Login;
            this.UesrChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UesrChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UesrChanges.Name = "UesrChanges";
            this.UesrChanges.Size = new System.Drawing.Size(93, 24);
            this.UesrChanges.Text = "切换用户";
            this.UesrChanges.Click += new System.EventHandler(this.UesrChanges_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(82, 24);
            this.保存SToolStripButton.Text = "保存(&S)";
            this.保存SToolStripButton.Click += new System.EventHandler(this.保存SToolStripButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 24);
            this.toolStripLabel1.Text = "文件路径";
            // 
            // toolStripTextBoxFilePath
            // 
            this.toolStripTextBoxFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStripTextBoxFilePath.ForeColor = System.Drawing.Color.Blue;
            this.toolStripTextBoxFilePath.Name = "toolStripTextBoxFilePath";
            this.toolStripTextBoxFilePath.ReadOnly = true;
            this.toolStripTextBoxFilePath.Size = new System.Drawing.Size(180, 27);
            this.toolStripTextBoxFilePath.ToolTipText = "当前文件路径";
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
            this.toolStripButtonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(99, 24);
            this.toolStripButtonExit.Text = "    退出     ";
            this.toolStripButtonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonExit.Visible = false;
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // toolStripButtonInfoQuery
            // 
            this.toolStripButtonInfoQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonInfoQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonInfoQuery.Image")));
            this.toolStripButtonInfoQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInfoQuery.Name = "toolStripButtonInfoQuery";
            this.toolStripButtonInfoQuery.Size = new System.Drawing.Size(125, 24);
            this.toolStripButtonInfoQuery.Text = "    信息查询    ";
            this.toolStripButtonInfoQuery.ToolTipText = "可以查询版本及其他相关信息";
            this.toolStripButtonInfoQuery.Click += new System.EventHandler(this.toolStripButtonInfoQuery_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Location = new System.Drawing.Point(3, 31);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1276, 815);
            this.panelMain.TabIndex = 9;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Fuchsia;
            this.label1.Location = new System.Drawing.Point(505, 340);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1032, 142);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(1, 844);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1283, 8);
            this.label3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(3, 142);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1279, 8);
            this.label2.TabIndex = 7;
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // timerError
            // 
            this.timerError.Interval = 50;
            this.timerError.Tick += new System.EventHandler(this.timerError_Tick);
            // 
            // timerInit
            // 
            this.timerInit.Interval = 1;
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // timerResetKey
            // 
            this.timerResetKey.Interval = 500;
            this.timerResetKey.Tick += new System.EventHandler(this.timerResetKey_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 882);
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "6KW4KW通用程序";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Leave += new System.EventHandler(this.MainForm_Leave);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton 新建NToolStripButton;
        private System.Windows.Forms.ToolStripButton 打开OToolStripButton;
        private System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        private System.Windows.Forms.ToolStripButton 打印PToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUser;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonManual;
        private System.Windows.Forms.ToolStripButton toolStripButtonParameter;
        private System.Windows.Forms.ToolStripButton toolStripButtonMES;
        private System.Windows.Forms.ToolStripButton toolStripButtonInfoQuery;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFilePath;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer timerError;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.Timer timerResetKey;
        private System.Windows.Forms.ToolStripButton toolStripButtonMini;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDoorOpen;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelHome;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelEStop;
        public bool bSNOK;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLaser;
        private System.Windows.Forms.ToolStripButton 自定义流程;
        private System.Windows.Forms.ToolStripStatusLabel timeShow;
        private System.Windows.Forms.ToolStripStatusLabel RunStaShow;
        private System.Windows.Forms.ToolStripStatusLabel VerShow;
        public System.Windows.Forms.ToolStripButton UesrChanges;
    }
}

