namespace WorldGeneralLib.CognexAsync
{
    partial class CognexAsyncForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CognexAsyncForm));
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.GreyPB = new System.Windows.Forms.PictureBox();
            this.GreenPB = new System.Windows.Forms.PictureBox();
            this.ConnIndicatePB = new System.Windows.Forms.PictureBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.ipAddressControlCognex = new IPAddressControlLib.IPAddressControl();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxRecv = new System.Windows.Forms.TextBox();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.FormattingEnabled = true;
            this.listBoxStatus.HorizontalScrollbar = true;
            this.listBoxStatus.ItemHeight = 12;
            this.listBoxStatus.Location = new System.Drawing.Point(446, 4);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.ScrollAlwaysVisible = true;
            this.listBoxStatus.Size = new System.Drawing.Size(204, 244);
            this.listBoxStatus.TabIndex = 8;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(581, 261);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(69, 30);
            this.buttonConnect.TabIndex = 9;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(279, 260);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(41, 32);
            this.buttonSend.TabIndex = 12;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // GreyPB
            // 
            this.GreyPB.Image = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            this.GreyPB.Location = new System.Drawing.Point(386, 12);
            this.GreyPB.Name = "GreyPB";
            this.GreyPB.Size = new System.Drawing.Size(32, 32);
            this.GreyPB.TabIndex = 15;
            this.GreyPB.TabStop = false;
            this.GreyPB.Visible = false;
            // 
            // GreenPB
            // 
            this.GreenPB.Image = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            this.GreenPB.Location = new System.Drawing.Point(386, 50);
            this.GreenPB.Name = "GreenPB";
            this.GreenPB.Size = new System.Drawing.Size(32, 32);
            this.GreenPB.TabIndex = 14;
            this.GreenPB.TabStop = false;
            this.GreenPB.Visible = false;
            // 
            // ConnIndicatePB
            // 
            this.ConnIndicatePB.Image = ((System.Drawing.Image)(resources.GetObject("ConnIndicatePB.Image")));
            this.ConnIndicatePB.Location = new System.Drawing.Point(332, 260);
            this.ConnIndicatePB.Name = "ConnIndicatePB";
            this.ConnIndicatePB.Size = new System.Drawing.Size(32, 32);
            this.ConnIndicatePB.TabIndex = 20;
            this.ConnIndicatePB.TabStop = false;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(531, 267);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(46, 21);
            this.textBoxPort.TabIndex = 19;
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(496, 271);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 12);
            this.labelPort.TabIndex = 18;
            this.labelPort.Text = "Port:";
            // 
            // ipAddressControlCognex
            // 
            this.ipAddressControlCognex.AllowInternalTab = false;
            this.ipAddressControlCognex.AutoHeight = true;
            this.ipAddressControlCognex.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlCognex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlCognex.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlCognex.Location = new System.Drawing.Point(396, 267);
            this.ipAddressControlCognex.MinimumSize = new System.Drawing.Size(96, 21);
            this.ipAddressControlCognex.Name = "ipAddressControlCognex";
            this.ipAddressControlCognex.ReadOnly = false;
            this.ipAddressControlCognex.Size = new System.Drawing.Size(96, 21);
            this.ipAddressControlCognex.TabIndex = 17;
            this.ipAddressControlCognex.Text = "...";
            this.ipAddressControlCognex.TextChanged += new System.EventHandler(this.ipAddressControlCognex_TextChanged);
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(372, 271);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(23, 12);
            this.labelIPAddress.TabIndex = 16;
            this.labelIPAddress.Text = "IP:";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(-1, 260);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(51, 32);
            this.buttonClear.TabIndex = 21;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxRecv
            // 
            this.textBoxRecv.BackColor = System.Drawing.Color.White;
            this.textBoxRecv.Location = new System.Drawing.Point(3, 4);
            this.textBoxRecv.Multiline = true;
            this.textBoxRecv.Name = "textBoxRecv";
            this.textBoxRecv.ReadOnly = true;
            this.textBoxRecv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRecv.Size = new System.Drawing.Size(445, 239);
            this.textBoxRecv.TabIndex = 22;
            this.textBoxRecv.WordWrap = false;
            // 
            // textBoxSend
            // 
            this.textBoxSend.BackColor = System.Drawing.Color.White;
            this.textBoxSend.Location = new System.Drawing.Point(55, 267);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxSend.Size = new System.Drawing.Size(218, 21);
            this.textBoxSend.TabIndex = 23;
            // 
            // CognexAsyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 301);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.listBoxStatus);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.ConnIndicatePB);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.ipAddressControlCognex);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.GreyPB);
            this.Controls.Add(this.GreenPB);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxRecv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CognexAsyncForm";
            this.Text = "CognexAsyncForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CognexAsyncForm_FormClosing);
            this.Load += new System.EventHandler(this.CognexAsyncForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxStatus;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonSend;
        public System.Windows.Forms.PictureBox GreyPB;
        public System.Windows.Forms.PictureBox GreenPB;
        public System.Windows.Forms.PictureBox ConnIndicatePB;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private IPAddressControlLib.IPAddressControl ipAddressControlCognex;
        private System.Windows.Forms.Label labelIPAddress;
        private System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.TextBox textBoxRecv;
        public System.Windows.Forms.TextBox textBoxSend;

    }
}