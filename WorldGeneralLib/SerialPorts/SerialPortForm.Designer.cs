namespace WorldGeneralLib.SerialPorts
{
    partial class SerialPortForm
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
            this.groupBoxBarcodeSetting = new System.Windows.Forms.GroupBox();
            this.cmb_Model = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDataBit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBarcodeSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBarcodeSetting
            // 
            this.groupBoxBarcodeSetting.BackColor = System.Drawing.Color.White;
            this.groupBoxBarcodeSetting.Controls.Add(this.cmb_Model);
            this.groupBoxBarcodeSetting.Controls.Add(this.label6);
            this.groupBoxBarcodeSetting.Controls.Add(this.btnSave);
            this.groupBoxBarcodeSetting.Controls.Add(this.cbParity);
            this.groupBoxBarcodeSetting.Controls.Add(this.label5);
            this.groupBoxBarcodeSetting.Controls.Add(this.cbStopBits);
            this.groupBoxBarcodeSetting.Controls.Add(this.label4);
            this.groupBoxBarcodeSetting.Controls.Add(this.cbDataBit);
            this.groupBoxBarcodeSetting.Controls.Add(this.label3);
            this.groupBoxBarcodeSetting.Controls.Add(this.cbBaudRate);
            this.groupBoxBarcodeSetting.Controls.Add(this.label2);
            this.groupBoxBarcodeSetting.Controls.Add(this.cbPortName);
            this.groupBoxBarcodeSetting.Controls.Add(this.label1);
            this.groupBoxBarcodeSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBarcodeSetting.Location = new System.Drawing.Point(0, 0);
            this.groupBoxBarcodeSetting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBarcodeSetting.Name = "groupBoxBarcodeSetting";
            this.groupBoxBarcodeSetting.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBarcodeSetting.Size = new System.Drawing.Size(308, 320);
            this.groupBoxBarcodeSetting.TabIndex = 38;
            this.groupBoxBarcodeSetting.TabStop = false;
            this.groupBoxBarcodeSetting.Tag = "串口参数设置";
            this.groupBoxBarcodeSetting.Text = "SerialPortSetting";
            // 
            // cmb_Model
            // 
            this.cmb_Model.FormattingEnabled = true;
            this.cmb_Model.Location = new System.Drawing.Point(109, 228);
            this.cmb_Model.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_Model.Name = "cmb_Model";
            this.cmb_Model.Size = new System.Drawing.Size(160, 23);
            this.cmb_Model.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 231);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Model:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(24, 272);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(109, 195);
            this.cbParity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(160, 23);
            this.cbParity.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 199);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Parity:";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(109, 156);
            this.cbStopBits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(160, 23);
            this.cbStopBits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "StopBits:";
            // 
            // cbDataBit
            // 
            this.cbDataBit.FormattingEnabled = true;
            this.cbDataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBit.Location = new System.Drawing.Point(109, 114);
            this.cbDataBit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDataBit.Name = "cbDataBit";
            this.cbDataBit.Size = new System.Drawing.Size(160, 23);
            this.cbDataBit.TabIndex = 5;
            this.cbDataBit.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "DataBit:";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(109, 72);
            this.cbBaudRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(160, 23);
            this.cbBaudRate.TabIndex = 3;
            this.cbBaudRate.Text = "9600";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "BaudRate:";
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15"});
            this.cbPortName.Location = new System.Drawing.Point(109, 34);
            this.cbPortName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(160, 23);
            this.cbPortName.TabIndex = 1;
            this.cbPortName.Text = "COM1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "PortName:";
            // 
            // SerialPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 320);
            this.Controls.Add(this.groupBoxBarcodeSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SerialPortForm";
            this.Text = "SerialPortForm";
            this.Load += new System.EventHandler(this.SerialPortForm_Load);
            this.groupBoxBarcodeSetting.ResumeLayout(false);
            this.groupBoxBarcodeSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBarcodeSetting;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDataBit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Model;
        private System.Windows.Forms.Label label6;
    }
}