namespace ControlPlatformLib
{
    partial class FormSystemSetting
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonHardware = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.buttonExportAll = new System.Windows.Forms.Button();
            this.buttonParam = new System.Windows.Forms.Button();
            this.buttonIO = new System.Windows.Forms.Button();
            this.buttonTable = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonHardware
            // 
            this.buttonHardware.Location = new System.Drawing.Point(30, 35);
            this.buttonHardware.Name = "buttonHardware";
            this.buttonHardware.Size = new System.Drawing.Size(122, 31);
            this.buttonHardware.TabIndex = 0;
            this.buttonHardware.Text = "硬件设定";
            this.buttonHardware.UseVisualStyleBackColor = true;
            this.buttonHardware.Click += new System.EventHandler(this.buttonHardware_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Location = new System.Drawing.Point(202, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(407, 405);
            this.panelMain.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Controls.Add(this.buttonExportAll);
            this.groupBox1.Controls.Add(this.buttonParam);
            this.groupBox1.Controls.Add(this.buttonIO);
            this.groupBox1.Controls.Add(this.buttonTable);
            this.groupBox1.Controls.Add(this.buttonHardware);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 411);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导航";
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Save.Location = new System.Drawing.Point(31, 325);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(122, 31);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保存所有参数";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // buttonExportAll
            // 
            this.buttonExportAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonExportAll.Location = new System.Drawing.Point(30, 362);
            this.buttonExportAll.Name = "buttonExportAll";
            this.buttonExportAll.Size = new System.Drawing.Size(122, 31);
            this.buttonExportAll.TabIndex = 1;
            this.buttonExportAll.Text = "导出所有名称";
            this.buttonExportAll.UseVisualStyleBackColor = true;
            this.buttonExportAll.Click += new System.EventHandler(this.buttonExportAll_Click);
            // 
            // buttonParam
            // 
            this.buttonParam.Location = new System.Drawing.Point(31, 217);
            this.buttonParam.Name = "buttonParam";
            this.buttonParam.Size = new System.Drawing.Size(122, 31);
            this.buttonParam.TabIndex = 1;
            this.buttonParam.Text = "参数设定";
            this.buttonParam.UseVisualStyleBackColor = true;
            this.buttonParam.Click += new System.EventHandler(this.buttonParam_Click);
            // 
            // buttonIO
            // 
            this.buttonIO.Location = new System.Drawing.Point(31, 160);
            this.buttonIO.Name = "buttonIO";
            this.buttonIO.Size = new System.Drawing.Size(122, 31);
            this.buttonIO.TabIndex = 1;
            this.buttonIO.Text = "IO设定";
            this.buttonIO.UseVisualStyleBackColor = true;
            this.buttonIO.Click += new System.EventHandler(this.buttonIO_Click);
            // 
            // buttonTable
            // 
            this.buttonTable.Location = new System.Drawing.Point(30, 98);
            this.buttonTable.Name = "buttonTable";
            this.buttonTable.Size = new System.Drawing.Size(122, 31);
            this.buttonTable.TabIndex = 0;
            this.buttonTable.Text = "平台设定";
            this.buttonTable.UseVisualStyleBackColor = true;
            this.buttonTable.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 429);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSystemSetting";
            this.Text = "FormUserManeage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSystemSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSystemSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonHardware;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonTable;
        private System.Windows.Forms.Button buttonIO;
        private System.Windows.Forms.Button buttonParam;
        private System.Windows.Forms.Button buttonExportAll;
        private System.Windows.Forms.Button btn_Save;
    }
}