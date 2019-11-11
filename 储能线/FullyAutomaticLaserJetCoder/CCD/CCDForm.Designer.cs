namespace FullyAutomaticLaserJetCoder.CCD
{
    partial class CCDForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCDForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new IPAddressControlLib.IPAddressControl();
            this.GreyPB = new System.Windows.Forms.PictureBox();
            this.GreenPB = new System.Windows.Forms.PictureBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.ConnIndicatePB = new System.Windows.Forms.PictureBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.txtRecieve = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.GreyPB);
            this.groupBox1.Controls.Add(this.GreenPB);
            this.groupBox1.Controls.Add(this.txtSend);
            this.groupBox1.Controls.Add(this.ConnIndicatePB);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.labelPort);
            this.groupBox1.Controls.Add(this.labelIPAddress);
            this.groupBox1.Controls.Add(this.txtRecieve);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 334);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CCD";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(516, 297);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(483, 67);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(108, 21);
            this.txtName.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "Name:";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(516, 146);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 26;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(438, 146);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 25;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.AllowInternalTab = false;
            this.txtIP.AutoHeight = true;
            this.txtIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIP.Location = new System.Drawing.Point(483, 94);
            this.txtIP.MinimumSize = new System.Drawing.Size(96, 21);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = false;
            this.txtIP.Size = new System.Drawing.Size(108, 21);
            this.txtIP.TabIndex = 24;
            this.txtIP.Text = "127.0.0.1";
            // 
            // GreyPB
            // 
            this.GreyPB.Image = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            this.GreyPB.Location = new System.Drawing.Point(479, 20);
            this.GreyPB.Name = "GreyPB";
            this.GreyPB.Size = new System.Drawing.Size(32, 32);
            this.GreyPB.TabIndex = 23;
            this.GreyPB.TabStop = false;
            this.GreyPB.Visible = false;
            // 
            // GreenPB
            // 
            this.GreenPB.Image = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            this.GreenPB.Location = new System.Drawing.Point(441, 20);
            this.GreenPB.Name = "GreenPB";
            this.GreenPB.Size = new System.Drawing.Size(32, 32);
            this.GreenPB.TabIndex = 22;
            this.GreenPB.TabStop = false;
            this.GreenPB.Visible = false;
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.White;
            this.txtSend.Location = new System.Drawing.Point(8, 213);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSend.Size = new System.Drawing.Size(425, 78);
            this.txtSend.TabIndex = 15;
            // 
            // ConnIndicatePB
            // 
            this.ConnIndicatePB.Image = ((System.Drawing.Image)(resources.GetObject("ConnIndicatePB.Image")));
            this.ConnIndicatePB.Location = new System.Drawing.Point(533, 20);
            this.ConnIndicatePB.Name = "ConnIndicatePB";
            this.ConnIndicatePB.Size = new System.Drawing.Size(32, 32);
            this.ConnIndicatePB.TabIndex = 21;
            this.ConnIndicatePB.TabStop = false;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(338, 297);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(93, 31);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(483, 119);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(108, 21);
            this.txtPort.TabIndex = 20;
            this.txtPort.Text = "2018";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 299);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 31);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(439, 122);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 12);
            this.labelPort.TabIndex = 19;
            this.labelPort.Text = "Port:";
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(439, 97);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(23, 12);
            this.labelIPAddress.TabIndex = 18;
            this.labelIPAddress.Text = "IP:";
            // 
            // txtRecieve
            // 
            this.txtRecieve.BackColor = System.Drawing.Color.White;
            this.txtRecieve.Location = new System.Drawing.Point(8, 12);
            this.txtRecieve.Multiline = true;
            this.txtRecieve.Name = "txtRecieve";
            this.txtRecieve.ReadOnly = true;
            this.txtRecieve.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRecieve.Size = new System.Drawing.Size(425, 195);
            this.txtRecieve.TabIndex = 14;
            this.txtRecieve.WordWrap = false;
            // 
            // CCDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 334);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CCDForm";
            this.Text = "CCDForm";
            this.Load += new System.EventHandler(this.CCDForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.PictureBox GreyPB;
        public System.Windows.Forms.PictureBox GreenPB;
        public System.Windows.Forms.TextBox txtSend;
        public System.Windows.Forms.PictureBox ConnIndicatePB;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelIPAddress;
        public System.Windows.Forms.TextBox txtRecieve;
        private IPAddressControlLib.IPAddressControl txtIP;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
    }
}