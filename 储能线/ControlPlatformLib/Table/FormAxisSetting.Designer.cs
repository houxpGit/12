namespace ControlPlatformLib
{
    partial class FormAxisSetting
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
            this.comboBoxCardName = new System.Windows.Forms.ComboBox();
            this.comboBoxAxisNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxLimt = new System.Windows.Forms.ComboBox();
            this.comboBoxOrg = new System.Windows.Forms.ComboBox();
            this.comboBoxPlusToMM = new System.Windows.Forms.ComboBox();
            this.textBoxAcc = new System.Windows.Forms.TextBox();
            this.textBoxDec = new System.Windows.Forms.TextBox();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.textBoxJobLow = new System.Windows.Forms.TextBox();
            this.textBoxJobHigh = new System.Windows.Forms.TextBox();
            this.textBoxLimtSearchSpd = new System.Windows.Forms.TextBox();
            this.textBoxOrgSpd = new System.Windows.Forms.TextBox();
            this.comboBoxAlarm = new System.Windows.Forms.ComboBox();
            this.comboBoxUsed = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOrgMode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPulseMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxConfig = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBoxCorNo = new System.Windows.Forms.ComboBox();
            this.txt_Alias = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxCardName
            // 
            this.comboBoxCardName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxCardName.FormattingEnabled = true;
            this.comboBoxCardName.Location = new System.Drawing.Point(23, 32);
            this.comboBoxCardName.Name = "comboBoxCardName";
            this.comboBoxCardName.Size = new System.Drawing.Size(74, 20);
            this.comboBoxCardName.TabIndex = 1;
            this.comboBoxCardName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxCardName.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxCardName.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // comboBoxAxisNo
            // 
            this.comboBoxAxisNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxAxisNo.FormattingEnabled = true;
            this.comboBoxAxisNo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.comboBoxAxisNo.Location = new System.Drawing.Point(103, 32);
            this.comboBoxAxisNo.Name = "comboBoxAxisNo";
            this.comboBoxAxisNo.Size = new System.Drawing.Size(74, 20);
            this.comboBoxAxisNo.TabIndex = 1;
            this.comboBoxAxisNo.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxAxisNo.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxAxisNo.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Location = new System.Drawing.Point(26, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "运动卡名字";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Location = new System.Drawing.Point(103, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "轴号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.Location = new System.Drawing.Point(185, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "极限逻辑";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.Location = new System.Drawing.Point(267, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "原点逻辑";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.Location = new System.Drawing.Point(350, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "报警逻辑";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(603, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "脉冲当量";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(517, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "搜原点速度";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(268, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 5;
            this.label12.Text = "Job低速";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(435, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "搜极限速度";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.Location = new System.Drawing.Point(103, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "减速度";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Location = new System.Drawing.Point(185, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 20);
            this.label15.TabIndex = 8;
            this.label15.Text = "运行速度";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Location = new System.Drawing.Point(352, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 20);
            this.label16.TabIndex = 9;
            this.label16.Text = "Job高速";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label17.Location = new System.Drawing.Point(23, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 20);
            this.label17.TabIndex = 10;
            this.label17.Text = "加速度";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxLimt
            // 
            this.comboBoxLimt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxLimt.FormattingEnabled = true;
            this.comboBoxLimt.Location = new System.Drawing.Point(185, 32);
            this.comboBoxLimt.Name = "comboBoxLimt";
            this.comboBoxLimt.Size = new System.Drawing.Size(74, 20);
            this.comboBoxLimt.TabIndex = 1;
            this.comboBoxLimt.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxLimt.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxLimt.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // comboBoxOrg
            // 
            this.comboBoxOrg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxOrg.FormattingEnabled = true;
            this.comboBoxOrg.Location = new System.Drawing.Point(268, 32);
            this.comboBoxOrg.Name = "comboBoxOrg";
            this.comboBoxOrg.Size = new System.Drawing.Size(74, 20);
            this.comboBoxOrg.TabIndex = 1;
            this.comboBoxOrg.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxOrg.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxOrg.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // comboBoxPlusToMM
            // 
            this.comboBoxPlusToMM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxPlusToMM.FormattingEnabled = true;
            this.comboBoxPlusToMM.Items.AddRange(new object[] {
            "0.0001",
            "0.0005",
            "0.001",
            "0.005",
            "0.0045",
            "0.0025",
            "0.002",
            "0.0016",
            "0.0015",
            "0.01",
            "0.05",
            "0.1",
            "0.5",
            "1"});
            this.comboBoxPlusToMM.Location = new System.Drawing.Point(604, 86);
            this.comboBoxPlusToMM.Name = "comboBoxPlusToMM";
            this.comboBoxPlusToMM.Size = new System.Drawing.Size(74, 20);
            this.comboBoxPlusToMM.TabIndex = 1;
            this.comboBoxPlusToMM.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxPlusToMM.Validated += new System.EventHandler(this.comboBoxPlusToMM_Validated);
            // 
            // textBoxAcc
            // 
            this.textBoxAcc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxAcc.Location = new System.Drawing.Point(23, 87);
            this.textBoxAcc.Name = "textBoxAcc";
            this.textBoxAcc.Size = new System.Drawing.Size(74, 21);
            this.textBoxAcc.TabIndex = 11;
            this.textBoxAcc.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxDec
            // 
            this.textBoxDec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxDec.Location = new System.Drawing.Point(103, 88);
            this.textBoxDec.Name = "textBoxDec";
            this.textBoxDec.Size = new System.Drawing.Size(74, 21);
            this.textBoxDec.TabIndex = 11;
            this.textBoxDec.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxSpeed.Location = new System.Drawing.Point(185, 88);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(74, 21);
            this.textBoxSpeed.TabIndex = 11;
            this.textBoxSpeed.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxJobLow
            // 
            this.textBoxJobLow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxJobLow.Location = new System.Drawing.Point(268, 88);
            this.textBoxJobLow.Name = "textBoxJobLow";
            this.textBoxJobLow.Size = new System.Drawing.Size(74, 21);
            this.textBoxJobLow.TabIndex = 11;
            this.textBoxJobLow.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxJobHigh
            // 
            this.textBoxJobHigh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxJobHigh.Location = new System.Drawing.Point(352, 88);
            this.textBoxJobHigh.Name = "textBoxJobHigh";
            this.textBoxJobHigh.Size = new System.Drawing.Size(74, 21);
            this.textBoxJobHigh.TabIndex = 11;
            this.textBoxJobHigh.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxLimtSearchSpd
            // 
            this.textBoxLimtSearchSpd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxLimtSearchSpd.Location = new System.Drawing.Point(435, 89);
            this.textBoxLimtSearchSpd.Name = "textBoxLimtSearchSpd";
            this.textBoxLimtSearchSpd.Size = new System.Drawing.Size(74, 21);
            this.textBoxLimtSearchSpd.TabIndex = 11;
            this.textBoxLimtSearchSpd.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // textBoxOrgSpd
            // 
            this.textBoxOrgSpd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxOrgSpd.Location = new System.Drawing.Point(517, 87);
            this.textBoxOrgSpd.Name = "textBoxOrgSpd";
            this.textBoxOrgSpd.Size = new System.Drawing.Size(74, 21);
            this.textBoxOrgSpd.TabIndex = 11;
            this.textBoxOrgSpd.Validated += new System.EventHandler(this.textBoxAcc_Validated);
            // 
            // comboBoxAlarm
            // 
            this.comboBoxAlarm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxAlarm.FormattingEnabled = true;
            this.comboBoxAlarm.Location = new System.Drawing.Point(352, 32);
            this.comboBoxAlarm.Name = "comboBoxAlarm";
            this.comboBoxAlarm.Size = new System.Drawing.Size(74, 20);
            this.comboBoxAlarm.TabIndex = 1;
            this.comboBoxAlarm.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxAlarm.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxAlarm.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // comboBoxUsed
            // 
            this.comboBoxUsed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxUsed.FormattingEnabled = true;
            this.comboBoxUsed.Items.AddRange(new object[] {
            "NoUsed",
            "Used"});
            this.comboBoxUsed.Location = new System.Drawing.Point(517, 32);
            this.comboBoxUsed.Name = "comboBoxUsed";
            this.comboBoxUsed.Size = new System.Drawing.Size(74, 20);
            this.comboBoxUsed.TabIndex = 1;
            this.comboBoxUsed.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxUsed.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxUsed.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(513, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "是否使用";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // comboBoxOrgMode
            // 
            this.comboBoxOrgMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxOrgMode.FormattingEnabled = true;
            this.comboBoxOrgMode.Items.AddRange(new object[] {
            "CCWL",
            "CWL",
            "CWORG",
            "CCWORG"});
            this.comboBoxOrgMode.Location = new System.Drawing.Point(603, 32);
            this.comboBoxOrgMode.Name = "comboBoxOrgMode";
            this.comboBoxOrgMode.Size = new System.Drawing.Size(74, 20);
            this.comboBoxOrgMode.TabIndex = 1;
            this.comboBoxOrgMode.SelectionChangeCommitted += new System.EventHandler(this.comboBoxOrgMode_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.Location = new System.Drawing.Point(603, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "原点方式";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxPulseMode
            // 
            this.comboBoxPulseMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxPulseMode.FormattingEnabled = true;
            this.comboBoxPulseMode.Location = new System.Drawing.Point(435, 32);
            this.comboBoxPulseMode.Name = "comboBoxPulseMode";
            this.comboBoxPulseMode.Size = new System.Drawing.Size(74, 20);
            this.comboBoxPulseMode.TabIndex = 1;
            this.comboBoxPulseMode.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCardName_SelectionChangeCommitted);
            this.comboBoxPulseMode.SelectedValueChanged += new System.EventHandler(this.comboBoxCardName_SelectedValueChanged);
            this.comboBoxPulseMode.Validated += new System.EventHandler(this.comboBoxCardName_Validated);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(435, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "脉冲方式";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxConfig
            // 
            this.checkBoxConfig.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxConfig.AutoSize = true;
            this.checkBoxConfig.Location = new System.Drawing.Point(721, 37);
            this.checkBoxConfig.Name = "checkBoxConfig";
            this.checkBoxConfig.Size = new System.Drawing.Size(15, 14);
            this.checkBoxConfig.TabIndex = 12;
            this.checkBoxConfig.UseVisualStyleBackColor = true;
            this.checkBoxConfig.Click += new System.EventHandler(this.checkBoxConfig_Click);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(692, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "使用配置文件";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(695, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 13;
            this.label19.Text = "坐标系编号";
            // 
            // comboBoxCorNo
            // 
            this.comboBoxCorNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxCorNo.FormattingEnabled = true;
            this.comboBoxCorNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBoxCorNo.Location = new System.Drawing.Point(691, 87);
            this.comboBoxCorNo.Name = "comboBoxCorNo";
            this.comboBoxCorNo.Size = new System.Drawing.Size(74, 20);
            this.comboBoxCorNo.TabIndex = 14;
            this.comboBoxCorNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxCorNo_SelectedIndexChanged);
            // 
            // txt_Alias
            // 
            this.txt_Alias.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txt_Alias.Location = new System.Drawing.Point(785, 59);
            this.txt_Alias.Name = "txt_Alias";
            this.txt_Alias.Size = new System.Drawing.Size(97, 21);
            this.txt_Alias.TabIndex = 16;
            this.txt_Alias.Validated += new System.EventHandler(this.txt_Alias_Validated);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.Location = new System.Drawing.Point(785, 35);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 20);
            this.label20.TabIndex = 15;
            this.label20.Text = "轴别名";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAxisSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 118);
            this.Controls.Add(this.txt_Alias);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.comboBoxCorNo);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.checkBoxConfig);
            this.Controls.Add(this.textBoxOrgSpd);
            this.Controls.Add(this.textBoxLimtSearchSpd);
            this.Controls.Add(this.textBoxJobHigh);
            this.Controls.Add(this.textBoxJobLow);
            this.Controls.Add(this.textBoxSpeed);
            this.Controls.Add(this.textBoxDec);
            this.Controls.Add(this.textBoxAcc);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxPulseMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxOrgMode);
            this.Controls.Add(this.comboBoxUsed);
            this.Controls.Add(this.comboBoxAlarm);
            this.Controls.Add(this.comboBoxPlusToMM);
            this.Controls.Add(this.comboBoxOrg);
            this.Controls.Add(this.comboBoxLimt);
            this.Controls.Add(this.comboBoxAxisNo);
            this.Controls.Add(this.comboBoxCardName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAxisSetting";
            this.Text = "FormAxisSetting";
            this.Load += new System.EventHandler(this.FormAxisSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCardName;
        private System.Windows.Forms.ComboBox comboBoxAxisNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxLimt;
        private System.Windows.Forms.ComboBox comboBoxOrg;
        private System.Windows.Forms.ComboBox comboBoxPlusToMM;
        private System.Windows.Forms.TextBox textBoxAcc;
        private System.Windows.Forms.TextBox textBoxDec;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.TextBox textBoxJobLow;
        private System.Windows.Forms.TextBox textBoxJobHigh;
        private System.Windows.Forms.TextBox textBoxLimtSearchSpd;
        private System.Windows.Forms.TextBox textBoxOrgSpd;
        private System.Windows.Forms.ComboBox comboBoxAlarm;
        private System.Windows.Forms.ComboBox comboBoxUsed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOrgMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPulseMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxConfig;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBoxCorNo;
        private System.Windows.Forms.TextBox txt_Alias;
        private System.Windows.Forms.Label label20;
    }
}