namespace ControlPlatformLib
{
    partial class ADLINKInputOutputInfoForm
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
            this.comboBoxCardNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_CardType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxCardNo
            // 
            this.comboBoxCardNo.FormattingEnabled = true;
            this.comboBoxCardNo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxCardNo.Location = new System.Drawing.Point(62, 58);
            this.comboBoxCardNo.Name = "comboBoxCardNo";
            this.comboBoxCardNo.Size = new System.Drawing.Size(61, 20);
            this.comboBoxCardNo.TabIndex = 10;
            this.comboBoxCardNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxCardNo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "卡号";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(135, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(7, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "ADLINKInputOutput";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_CardType
            // 
            this.cmb_CardType.FormattingEnabled = true;
            this.cmb_CardType.Items.AddRange(new object[] {
            "PCI_6208V    ",
            "PCI_6208A",
            "PCI_6308V",
            "PCI_6308A ",
            "PCI_7200",
            "PCI_7230",
            "PCI_7233",
            "PCI_7234",
            "PCI_7248",
            "PCI_7249",
            "PCI_7250",
            "PCI_7252",
            "PCI_7296",
            "PCI_7300A_R",
            "PCI_7300A_R",
            "PCI_7432 ",
            "PCI_7433 ",
            "PCI_7434 ",
            "PCI_8554 ",
            "PCI_9111DG ",
            "PCI_9111HR ",
            "PCI_9112 ",
            "PCI_9113 ",
            "PCI_9114DG ",
            "PCI_9114HG ",
            "PCI_9118DG ",
            "PCI_9118HG ",
            "PCI_9118HR ",
            "PCI_9810",
            "PCI_9812",
            "PCI_7396",
            "PCI_9116",
            "PCI_7256",
            "PCI_7258",
            "PCI_7260",
            "PCI_7452",
            "PCI_7442"});
            this.cmb_CardType.Location = new System.Drawing.Point(62, 84);
            this.cmb_CardType.Name = "cmb_CardType";
            this.cmb_CardType.Size = new System.Drawing.Size(131, 20);
            this.cmb_CardType.TabIndex = 18;
            this.cmb_CardType.SelectedIndexChanged += new System.EventHandler(this.cmb_CardType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "卡型号";
            // 
            // ADLINKInputOutputInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 433);
            this.Controls.Add(this.cmb_CardType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxCardNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ADLINKInputOutputInfoForm";
            this.Text = "ADLINKInputOutputInfoForm";
            this.Load += new System.EventHandler(this.ADLINKInputOutputInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCardNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_CardType;
        private System.Windows.Forms.Label label4;
    }
}