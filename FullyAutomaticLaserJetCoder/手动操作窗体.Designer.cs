namespace FullyAutomaticLaserJetCoder
{
    partial class 手动操作窗体
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.手动过MES_Sn = new System.Windows.Forms.TextBox();
            this.手动过MES = new System.Windows.Forms.Button();
            this.扫码 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.机种保存 = new System.Windows.Forms.Button();
            this.运行流程 = new System.Windows.Forms.Button();
            this.运行流程选择 = new System.Windows.Forms.ComboBox();
            this.机种更新 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.机种选择 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputCardNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputNoColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.板卡IO = new System.Windows.Forms.Button();
            this.模块1 = new System.Windows.Forms.Button();
            this.模块2 = new System.Windows.Forms.Button();
            this.模块3 = new System.Windows.Forms.Button();
            this.模块4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.模块5 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.最小化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新机种号 = new System.Windows.Forms.TextBox();
            this.添加机种 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.添加数据 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.数据名号 = new System.Windows.Forms.TextBox();
            this.机种择 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.机种设置选择 = new System.Windows.Forms.ComboBox();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 手动过MES_Sn
            // 
            this.手动过MES_Sn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.手动过MES_Sn.Location = new System.Drawing.Point(124, 126);
            this.手动过MES_Sn.Multiline = true;
            this.手动过MES_Sn.Name = "手动过MES_Sn";
            this.手动过MES_Sn.Size = new System.Drawing.Size(285, 40);
            this.手动过MES_Sn.TabIndex = 362;
            this.手动过MES_Sn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 手动过MES
            // 
            this.手动过MES.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.手动过MES.Location = new System.Drawing.Point(7, 126);
            this.手动过MES.Margin = new System.Windows.Forms.Padding(4);
            this.手动过MES.Name = "手动过MES";
            this.手动过MES.Size = new System.Drawing.Size(110, 40);
            this.手动过MES.TabIndex = 361;
            this.手动过MES.Text = "手动过MES";
            this.手动过MES.UseVisualStyleBackColor = false;
            this.手动过MES.Click += new System.EventHandler(this.手动过MES_Click);
            // 
            // 扫码
            // 
            this.扫码.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.扫码.Location = new System.Drawing.Point(416, 126);
            this.扫码.Margin = new System.Windows.Forms.Padding(4);
            this.扫码.Name = "扫码";
            this.扫码.Size = new System.Drawing.Size(110, 40);
            this.扫码.TabIndex = 360;
            this.扫码.Text = "扫码";
            this.扫码.UseVisualStyleBackColor = false;
            this.扫码.Click += new System.EventHandler(this.扫码_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.机种保存);
            this.groupBox1.Controls.Add(this.运行流程);
            this.groupBox1.Controls.Add(this.运行流程选择);
            this.groupBox1.Controls.Add(this.机种更新);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.机种选择);
            this.groupBox1.Controls.Add(this.手动过MES_Sn);
            this.groupBox1.Controls.Add(this.扫码);
            this.groupBox1.Controls.Add(this.手动过MES);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 173);
            this.groupBox1.TabIndex = 366;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "流程按钮";
            // 
            // 机种保存
            // 
            this.机种保存.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.机种保存.Location = new System.Drawing.Point(7, 51);
            this.机种保存.Margin = new System.Windows.Forms.Padding(4);
            this.机种保存.Name = "机种保存";
            this.机种保存.Size = new System.Drawing.Size(120, 38);
            this.机种保存.TabIndex = 368;
            this.机种保存.Text = "机种数据保存";
            this.机种保存.UseVisualStyleBackColor = false;
            this.机种保存.Click += new System.EventHandler(this.机种保存_Click);
            // 
            // 运行流程
            // 
            this.运行流程.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.运行流程.Location = new System.Drawing.Point(407, 51);
            this.运行流程.Margin = new System.Windows.Forms.Padding(4);
            this.运行流程.Name = "运行流程";
            this.运行流程.Size = new System.Drawing.Size(274, 38);
            this.运行流程.TabIndex = 367;
            this.运行流程.Text = "运行流程";
            this.运行流程.UseVisualStyleBackColor = false;
            this.运行流程.Click += new System.EventHandler(this.运行流程_Click);
            // 
            // 运行流程选择
            // 
            this.运行流程选择.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.运行流程选择.ForeColor = System.Drawing.Color.Black;
            this.运行流程选择.FormattingEnabled = true;
            this.运行流程选择.Location = new System.Drawing.Point(407, 21);
            this.运行流程选择.Name = "运行流程选择";
            this.运行流程选择.Size = new System.Drawing.Size(274, 23);
            this.运行流程选择.TabIndex = 366;
            // 
            // 机种更新
            // 
            this.机种更新.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.机种更新.Location = new System.Drawing.Point(135, 51);
            this.机种更新.Margin = new System.Windows.Forms.Padding(4);
            this.机种更新.Name = "机种更新";
            this.机种更新.Size = new System.Drawing.Size(266, 38);
            this.机种更新.TabIndex = 365;
            this.机种更新.Text = "机种数据加载";
            this.机种更新.UseVisualStyleBackColor = false;
            this.机种更新.Click += new System.EventHandler(this.机种更新_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 21);
            this.label1.TabIndex = 364;
            this.label1.Text = "机种选择";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 机种选择
            // 
            this.机种选择.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.机种选择.ForeColor = System.Drawing.Color.Black;
            this.机种选择.FormattingEnabled = true;
            this.机种选择.Location = new System.Drawing.Point(135, 21);
            this.机种选择.Name = "机种选择";
            this.机种选择.Size = new System.Drawing.Size(266, 23);
            this.机种选择.TabIndex = 363;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewOutput);
            this.groupBox2.Controls.Add(this.dataGridViewInput);
            this.groupBox2.Location = new System.Drawing.Point(12, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1165, 510);
            this.groupBox2.TabIndex = 367;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IO操作";
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOutput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.CardName,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewButtonColumn1});
            this.dataGridViewOutput.Location = new System.Drawing.Point(587, 16);
            this.dataGridViewOutput.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.RowHeadersWidth = 10;
            this.dataGridViewOutput.RowTemplate.Height = 23;
            this.dataGridViewOutput.Size = new System.Drawing.Size(557, 487);
            this.dataGridViewOutput.TabIndex = 20;
            this.dataGridViewOutput.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellClick);
            this.dataGridViewOutput.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 45;
            // 
            // CardName
            // 
            this.CardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CardName.HeaderText = "CardName";
            this.CardName.Name = "CardName";
            this.CardName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardName.Width = 77;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "ON/OFF";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "ON";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "OFF";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Width = 40;
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToAddRows = false;
            this.dataGridViewInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.InputCardNameColumn,
            this.InputNoColumn});
            this.dataGridViewInput.Location = new System.Drawing.Point(7, 16);
            this.dataGridViewInput.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.RowHeadersWidth = 10;
            this.dataGridViewInput.RowTemplate.Height = 23;
            this.dataGridViewInput.Size = new System.Drawing.Size(572, 487);
            this.dataGridViewInput.TabIndex = 15;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.DataPropertyName = "strIOName";
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 45;
            // 
            // InputCardNameColumn
            // 
            this.InputCardNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.InputCardNameColumn.DataPropertyName = "InputCardName";
            this.InputCardNameColumn.HeaderText = "CardName";
            this.InputCardNameColumn.Name = "InputCardNameColumn";
            this.InputCardNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InputCardNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputCardNameColumn.Width = 77;
            // 
            // InputNoColumn
            // 
            this.InputNoColumn.DataPropertyName = "iInputNo";
            this.InputNoColumn.HeaderText = "状态";
            this.InputNoColumn.Name = "InputNoColumn";
            this.InputNoColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InputNoColumn.Width = 50;
            // 
            // 板卡IO
            // 
            this.板卡IO.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.板卡IO.Location = new System.Drawing.Point(706, 151);
            this.板卡IO.Margin = new System.Windows.Forms.Padding(4);
            this.板卡IO.Name = "板卡IO";
            this.板卡IO.Size = new System.Drawing.Size(110, 30);
            this.板卡IO.TabIndex = 366;
            this.板卡IO.Text = "板卡IO";
            this.板卡IO.UseVisualStyleBackColor = false;
            this.板卡IO.Click += new System.EventHandler(this.板卡IO_Click);
            // 
            // 模块1
            // 
            this.模块1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.模块1.Location = new System.Drawing.Point(706, 183);
            this.模块1.Margin = new System.Windows.Forms.Padding(4);
            this.模块1.Name = "模块1";
            this.模块1.Size = new System.Drawing.Size(110, 30);
            this.模块1.TabIndex = 368;
            this.模块1.Text = "模块1";
            this.模块1.UseVisualStyleBackColor = false;
            this.模块1.Click += new System.EventHandler(this.模块1_Click);
            // 
            // 模块2
            // 
            this.模块2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.模块2.Location = new System.Drawing.Point(824, 151);
            this.模块2.Margin = new System.Windows.Forms.Padding(4);
            this.模块2.Name = "模块2";
            this.模块2.Size = new System.Drawing.Size(110, 30);
            this.模块2.TabIndex = 369;
            this.模块2.Text = "模块2";
            this.模块2.UseVisualStyleBackColor = false;
            this.模块2.Click += new System.EventHandler(this.模块2_Click);
            // 
            // 模块3
            // 
            this.模块3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.模块3.Location = new System.Drawing.Point(824, 183);
            this.模块3.Margin = new System.Windows.Forms.Padding(4);
            this.模块3.Name = "模块3";
            this.模块3.Size = new System.Drawing.Size(110, 30);
            this.模块3.TabIndex = 370;
            this.模块3.Text = "模块3";
            this.模块3.UseVisualStyleBackColor = false;
            this.模块3.Click += new System.EventHandler(this.模块3_Click);
            // 
            // 模块4
            // 
            this.模块4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.模块4.Location = new System.Drawing.Point(942, 151);
            this.模块4.Margin = new System.Windows.Forms.Padding(4);
            this.模块4.Name = "模块4";
            this.模块4.Size = new System.Drawing.Size(110, 30);
            this.模块4.TabIndex = 371;
            this.模块4.Text = "模块4";
            this.模块4.UseVisualStyleBackColor = false;
            this.模块4.Click += new System.EventHandler(this.模块4_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 模块5
            // 
            this.模块5.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.模块5.Location = new System.Drawing.Point(942, 183);
            this.模块5.Margin = new System.Windows.Forms.Padding(4);
            this.模块5.Name = "模块5";
            this.模块5.Size = new System.Drawing.Size(110, 30);
            this.模块5.TabIndex = 372;
            this.模块5.Text = "模块5";
            this.模块5.UseVisualStyleBackColor = false;
            this.模块5.Click += new System.EventHandler(this.模块5_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最小化ToolStripMenuItem,
            this.最大化ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1189, 28);
            this.menuStrip1.TabIndex = 375;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            this.menuStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseMove);
            // 
            // 最小化ToolStripMenuItem
            // 
            this.最小化ToolStripMenuItem.Name = "最小化ToolStripMenuItem";
            this.最小化ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.最小化ToolStripMenuItem.Text = "最小化";
            this.最小化ToolStripMenuItem.Click += new System.EventHandler(this.最小化ToolStripMenuItem_Click);
            // 
            // 最大化ToolStripMenuItem
            // 
            this.最大化ToolStripMenuItem.Name = "最大化ToolStripMenuItem";
            this.最大化ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.最大化ToolStripMenuItem.Text = "最大化";
            // 
            // 新机种号
            // 
            this.新机种号.Location = new System.Drawing.Point(832, 59);
            this.新机种号.Name = "新机种号";
            this.新机种号.Size = new System.Drawing.Size(241, 25);
            this.新机种号.TabIndex = 376;
            // 
            // 添加机种
            // 
            this.添加机种.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.添加机种.Location = new System.Drawing.Point(1079, 57);
            this.添加机种.Margin = new System.Windows.Forms.Padding(4);
            this.添加机种.Name = "添加机种";
            this.添加机种.Size = new System.Drawing.Size(98, 29);
            this.添加机种.TabIndex = 369;
            this.添加机种.Text = "添加机种";
            this.添加机种.UseVisualStyleBackColor = false;
            this.添加机种.Click += new System.EventHandler(this.添加机种_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(706, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 21);
            this.label2.TabIndex = 369;
            this.label2.Text = "机种名:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 添加数据
            // 
            this.添加数据.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.添加数据.Location = new System.Drawing.Point(1079, 87);
            this.添加数据.Margin = new System.Windows.Forms.Padding(4);
            this.添加数据.Name = "添加数据";
            this.添加数据.Size = new System.Drawing.Size(98, 29);
            this.添加数据.TabIndex = 377;
            this.添加数据.Text = "添加数据";
            this.添加数据.UseVisualStyleBackColor = false;
            this.添加数据.Click += new System.EventHandler(this.添加数据_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(706, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 21);
            this.label3.TabIndex = 378;
            this.label3.Text = "数据名:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 数据名号
            // 
            this.数据名号.Location = new System.Drawing.Point(832, 90);
            this.数据名号.Name = "数据名号";
            this.数据名号.Size = new System.Drawing.Size(241, 25);
            this.数据名号.TabIndex = 379;
            // 
            // 机种择
            // 
            this.机种择.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.机种择.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.机种择.Location = new System.Drawing.Point(706, 122);
            this.机种择.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.机种择.Name = "机种择";
            this.机种择.Size = new System.Drawing.Size(121, 21);
            this.机种择.TabIndex = 381;
            this.机种择.Text = "设置选择：";
            this.机种择.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.Location = new System.Drawing.Point(1079, 119);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 29);
            this.button1.TabIndex = 380;
            this.button1.Text = "机种设置";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // 机种设置选择
            // 
            this.机种设置选择.FormattingEnabled = true;
            this.机种设置选择.Location = new System.Drawing.Point(832, 123);
            this.机种设置选择.Name = "机种设置选择";
            this.机种设置选择.Size = new System.Drawing.Size(241, 23);
            this.机种设置选择.TabIndex = 382;
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Image = global::FullyAutomaticLaserJetCoder.Properties.Resources.Exit21;
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 手动操作窗体
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 732);
            this.Controls.Add(this.机种设置选择);
            this.Controls.Add(this.机种择);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.数据名号);
            this.Controls.Add(this.添加数据);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.添加机种);
            this.Controls.Add(this.新机种号);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.模块5);
            this.Controls.Add(this.模块4);
            this.Controls.Add(this.模块3);
            this.Controls.Add(this.模块2);
            this.Controls.Add(this.模块1);
            this.Controls.Add(this.板卡IO);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "手动操作窗体";
            this.Text = "手动操作窗体";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.手动操作窗体_FormClosed);
            this.Load += new System.EventHandler(this.手动操作窗体_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.手动操作窗体_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.手动操作窗体_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox 手动过MES_Sn;
        private System.Windows.Forms.Button 手动过MES;
        private System.Windows.Forms.Button 扫码;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button 板卡IO;
        private System.Windows.Forms.Button 模块1;
        private System.Windows.Forms.Button 模块2;
        private System.Windows.Forms.Button 模块3;
        private System.Windows.Forms.Button 模块4;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.DataGridView dataGridViewOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button 模块5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputCardNameColumn;
        private System.Windows.Forms.DataGridViewLinkColumn InputNoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 最小化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.Button 机种更新;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox 机种选择;
        private System.Windows.Forms.ComboBox 运行流程选择;
        private System.Windows.Forms.Button 运行流程;
        private System.Windows.Forms.Button 机种保存;
        private System.Windows.Forms.TextBox 新机种号;
        private System.Windows.Forms.Button 添加机种;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button 添加数据;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox 数据名号;
        private System.Windows.Forms.Label 机种择;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox 机种设置选择;
    }
}