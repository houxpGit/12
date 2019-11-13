namespace WorldGeneralLib.Cognex
{
    partial class CognexForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CognexForm));
            this.textBoxCognex = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.ipAddressControlCognex = new IPAddressControlLib.IPAddressControl();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.ConnIndicatePB = new System.Windows.Forms.PictureBox();
            this.GreenPB = new System.Windows.Forms.PictureBox();
            this.GreyPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxCognex
            // 
            this.textBoxCognex.BackColor = System.Drawing.Color.White;
            this.textBoxCognex.Location = new System.Drawing.Point(0, 12);
            this.textBoxCognex.Multiline = true;
            this.textBoxCognex.Name = "textBoxCognex";
            this.textBoxCognex.ReadOnly = true;
            this.textBoxCognex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCognex.Size = new System.Drawing.Size(691, 276);
            this.textBoxCognex.TabIndex = 1;
            this.textBoxCognex.WordWrap = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 302);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(62, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(330, 302);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(61, 23);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.BackColor = System.Drawing.Color.White;
            this.textBoxSend.Location = new System.Drawing.Point(88, 305);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxSend.Size = new System.Drawing.Size(236, 21);
            this.textBoxSend.TabIndex = 4;
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(463, 308);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(23, 12);
            this.labelIPAddress.TabIndex = 7;
            this.labelIPAddress.Text = "IP:";
            // 
            // ipAddressControlCognex
            // 
            this.ipAddressControlCognex.AllowInternalTab = false;
            this.ipAddressControlCognex.AutoHeight = true;
            this.ipAddressControlCognex.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlCognex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlCognex.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlCognex.Location = new System.Drawing.Point(487, 304);
            this.ipAddressControlCognex.MinimumSize = new System.Drawing.Size(96, 21);
            this.ipAddressControlCognex.Name = "ipAddressControlCognex";
            this.ipAddressControlCognex.ReadOnly = false;
            this.ipAddressControlCognex.Size = new System.Drawing.Size(96, 21);
            this.ipAddressControlCognex.TabIndex = 8;
            this.ipAddressControlCognex.Text = "...";
            this.ipAddressControlCognex.TextChanged += new System.EventHandler(this.ipAddressControlCognex_TextChanged);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(588, 308);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 12);
            this.labelPort.TabIndex = 9;
            this.labelPort.Text = "Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(629, 304);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(46, 21);
            this.textBoxPort.TabIndex = 10;
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            // 
            // ConnIndicatePB
            // 
            this.ConnIndicatePB.Image = ((System.Drawing.Image)(resources.GetObject("ConnIndicatePB.Image")));
            this.ConnIndicatePB.Location = new System.Drawing.Point(425, 294);
            this.ConnIndicatePB.Name = "ConnIndicatePB";
            this.ConnIndicatePB.Size = new System.Drawing.Size(32, 32);
            this.ConnIndicatePB.TabIndex = 11;
            this.ConnIndicatePB.TabStop = false;
            // 
            // GreenPB
            // 
            this.GreenPB.Image = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            this.GreenPB.Location = new System.Drawing.Point(629, 12);
            this.GreenPB.Name = "GreenPB";
            this.GreenPB.Size = new System.Drawing.Size(32, 32);
            this.GreenPB.TabIndex = 12;
            this.GreenPB.TabStop = false;
            this.GreenPB.Visible = false;
            // 
            // GreyPB
            // 
            this.GreyPB.Image = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            this.GreyPB.Location = new System.Drawing.Point(629, 50);
            this.GreyPB.Name = "GreyPB";
            this.GreyPB.Size = new System.Drawing.Size(32, 32);
            this.GreyPB.TabIndex = 13;
            this.GreyPB.TabStop = false;
            this.GreyPB.Visible = false;
            // 
            // CognexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 348);
            this.Controls.Add(this.GreyPB);
            this.Controls.Add(this.GreenPB);
            this.Controls.Add(this.ConnIndicatePB);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.ipAddressControlCognex);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.textBoxCognex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CognexForm";
            this.Text = "CognexForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CognexForm_FormClosing);
            this.Load += new System.EventHandler(this.CognexForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxCognex;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSend;
        public System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label labelIPAddress;
        private IPAddressControlLib.IPAddressControl ipAddressControlCognex;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        public System.Windows.Forms.PictureBox ConnIndicatePB;
        public System.Windows.Forms.PictureBox GreenPB;
        public System.Windows.Forms.PictureBox GreyPB;

    }
}