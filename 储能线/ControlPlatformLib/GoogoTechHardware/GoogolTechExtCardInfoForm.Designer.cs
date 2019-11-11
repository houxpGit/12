namespace ControlPlatformLib
{
    partial class GoogolTechExtCardInfoForm
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
            this.textBoxConfigName = new System.Windows.Forms.TextBox();
            this.comboBoxCardNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_DllName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_ExtCardNumber = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxConfigName
            // 
            this.textBoxConfigName.Location = new System.Drawing.Point(108, 146);
            this.textBoxConfigName.Name = "textBoxConfigName";
            this.textBoxConfigName.Size = new System.Drawing.Size(137, 21);
            this.textBoxConfigName.TabIndex = 11;
            this.textBoxConfigName.Validated += new System.EventHandler(this.textBoxConfigName_Validated);
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
            this.comboBoxCardNo.Location = new System.Drawing.Point(108, 89);
            this.comboBoxCardNo.Name = "comboBoxCardNo";
            this.comboBoxCardNo.Size = new System.Drawing.Size(137, 20);
            this.comboBoxCardNo.TabIndex = 10;
            this.comboBoxCardNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxCardNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "配置文件名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "控制卡号";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(90, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(7, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "GOOGOLEXT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_DllName
            // 
            this.txt_DllName.Location = new System.Drawing.Point(108, 173);
            this.txt_DllName.Name = "txt_DllName";
            this.txt_DllName.Size = new System.Drawing.Size(137, 21);
            this.txt_DllName.TabIndex = 13;
            this.txt_DllName.Validated += new System.EventHandler(this.txt_DllName_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "DLL文件名";
            // 
            // cmb_ExtCardNumber
            // 
            this.cmb_ExtCardNumber.FormattingEnabled = true;
            this.cmb_ExtCardNumber.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cmb_ExtCardNumber.Location = new System.Drawing.Point(108, 115);
            this.cmb_ExtCardNumber.Name = "cmb_ExtCardNumber";
            this.cmb_ExtCardNumber.Size = new System.Drawing.Size(137, 20);
            this.cmb_ExtCardNumber.TabIndex = 15;
            this.cmb_ExtCardNumber.SelectedIndexChanged += new System.EventHandler(this.cmb_ExtCardNumber_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "拓展卡号";
            // 
            // GoogolTechExtCardInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 427);
            this.Controls.Add(this.cmb_ExtCardNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_DllName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxConfigName);
            this.Controls.Add(this.comboBoxCardNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GoogolTechExtCardInfoForm";
            this.Text = "GoogolTechExtCardInfoForm";
            this.Load += new System.EventHandler(this.GoogolTechExtCardInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConfigName;
        private System.Windows.Forms.ComboBox comboBoxCardNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DllName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_ExtCardNumber;
        private System.Windows.Forms.Label label6;
    }
}