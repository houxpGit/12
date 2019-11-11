namespace FullyAutomaticLaserJetCoder.Mark
{
    partial class MarkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
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
            this.txtBautRate = new IPAddressControlLib.IPAddressControl();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(520, 297);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 45;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(501, 65);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(108, 21);
            this.txtCom.TabIndex = 44;
            this.txtCom.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "端口:";
            this.label1.Visible = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(521, 168);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 42;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(440, 168);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 41;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // GreyPB
            // 
            this.GreyPB.Image = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            this.GreyPB.Location = new System.Drawing.Point(483, 20);
            this.GreyPB.Name = "GreyPB";
            this.GreyPB.Size = new System.Drawing.Size(32, 32);
            this.GreyPB.TabIndex = 39;
            this.GreyPB.TabStop = false;
            this.GreyPB.Visible = false;
            // 
            // GreenPB
            // 
            this.GreenPB.Image = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            this.GreenPB.Location = new System.Drawing.Point(445, 20);
            this.GreenPB.Name = "GreenPB";
            this.GreenPB.Size = new System.Drawing.Size(32, 32);
            this.GreenPB.TabIndex = 38;
            this.GreenPB.TabStop = false;
            this.GreenPB.Visible = false;
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.White;
            this.txtSend.Location = new System.Drawing.Point(12, 213);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSend.Size = new System.Drawing.Size(425, 78);
            this.txtSend.TabIndex = 31;
            // 
            // ConnIndicatePB
            // 
            this.ConnIndicatePB.Image = ((System.Drawing.Image)(resources.GetObject("ConnIndicatePB.Image")));
            this.ConnIndicatePB.Location = new System.Drawing.Point(537, 20);
            this.ConnIndicatePB.Name = "ConnIndicatePB";
            this.ConnIndicatePB.Size = new System.Drawing.Size(32, 32);
            this.ConnIndicatePB.TabIndex = 37;
            this.ConnIndicatePB.TabStop = false;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(342, 297);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(93, 31);
            this.btnSend.TabIndex = 32;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(501, 119);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(108, 21);
            this.txtPort.TabIndex = 36;
            this.txtPort.Text = "2018";
            this.txtPort.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(10, 299);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 31);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(443, 122);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(59, 12);
            this.labelPort.TabIndex = 35;
            this.labelPort.Text = "奇偶校检:";
            this.labelPort.Visible = false;
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(443, 97);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(47, 12);
            this.labelIPAddress.TabIndex = 34;
            this.labelIPAddress.Text = "波特率:";
            this.labelIPAddress.Visible = false;
            // 
            // txtRecieve
            // 
            this.txtRecieve.BackColor = System.Drawing.Color.White;
            this.txtRecieve.Location = new System.Drawing.Point(12, 12);
            this.txtRecieve.Multiline = true;
            this.txtRecieve.Name = "txtRecieve";
            this.txtRecieve.ReadOnly = true;
            this.txtRecieve.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRecieve.Size = new System.Drawing.Size(425, 195);
            this.txtRecieve.TabIndex = 30;
            this.txtRecieve.WordWrap = false;
            // 
            // txtBautRate
            // 
            this.txtBautRate.AllowInternalTab = false;
            this.txtBautRate.AutoHeight = true;
            this.txtBautRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtBautRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBautRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBautRate.Location = new System.Drawing.Point(501, 94);
            this.txtBautRate.MinimumSize = new System.Drawing.Size(96, 21);
            this.txtBautRate.Name = "txtBautRate";
            this.txtBautRate.ReadOnly = false;
            this.txtBautRate.Size = new System.Drawing.Size(108, 21);
            this.txtBautRate.TabIndex = 40;
            this.txtBautRate.Text = "255...";
            this.txtBautRate.Visible = false;
            // 
            // MarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 347);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtBautRate);
            this.Controls.Add(this.GreyPB);
            this.Controls.Add(this.GreenPB);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.ConnIndicatePB);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.txtRecieve);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MarkForm";
            this.Text = "MarkForm";
            this.Load += new System.EventHandler(this.MarkForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
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
        private IPAddressControlLib.IPAddressControl txtBautRate;
    }
}