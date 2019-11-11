namespace WorldGeneralLib.InovancePLC
{
    partial class InovanceSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InovanceSettingForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSetValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStartAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAddressType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.ipAddressControlPLC = new IPAddressControlLib.IPAddressControl();
            this.GreenPB = new System.Windows.Forms.PictureBox();
            this.GreyPB = new System.Windows.Forms.PictureBox();
            this.ConnIndicatePB = new System.Windows.Forms.PictureBox();
            this.buttonExportName = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Address,
            this.Column3,
            this.Column5});
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 30;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(443, 304);
            this.dataGridView.TabIndex = 15;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 73;
            this.label6.Text = "IP Address";
            // 
            // buttonSet
            // 
            this.buttonSet.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSet.Location = new System.Drawing.Point(393, 365);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(62, 36);
            this.buttonSet.TabIndex = 71;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonModify.Location = new System.Drawing.Point(67, 320);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(49, 32);
            this.buttonModify.TabIndex = 70;
            this.buttonModify.Text = "Modify";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 357);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 69;
            this.label5.Text = "Data Type";
            // 
            // cbDataType
            // 
            this.cbDataType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Location = new System.Drawing.Point(276, 375);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(41, 20);
            this.cbDataType.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 67;
            this.label4.Text = "SetValue";
            // 
            // tbSetValue
            // 
            this.tbSetValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbSetValue.Location = new System.Drawing.Point(332, 375);
            this.tbSetValue.Name = "tbSetValue";
            this.tbSetValue.Size = new System.Drawing.Size(56, 21);
            this.tbSetValue.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 65;
            this.label3.Text = "Address";
            // 
            // tbStartAddress
            // 
            this.tbStartAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbStartAddress.Location = new System.Drawing.Point(213, 375);
            this.tbStartAddress.Name = "tbStartAddress";
            this.tbStartAddress.Size = new System.Drawing.Size(49, 21);
            this.tbStartAddress.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 63;
            this.label1.Text = "Type";
            // 
            // cbAddressType
            // 
            this.cbAddressType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbAddressType.FormattingEnabled = true;
            this.cbAddressType.Location = new System.Drawing.Point(160, 375);
            this.cbAddressType.Name = "cbAddressType";
            this.cbAddressType.Size = new System.Drawing.Size(45, 20);
            this.cbAddressType.TabIndex = 62;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "Name";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRemove.Location = new System.Drawing.Point(122, 320);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(49, 32);
            this.buttonRemove.TabIndex = 59;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAdd.Location = new System.Drawing.Point(12, 320);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(49, 32);
            this.buttonAdd.TabIndex = 60;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName.Location = new System.Drawing.Point(12, 375);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(142, 21);
            this.textBoxName.TabIndex = 58;
            // 
            // ipAddressControlPLC
            // 
            this.ipAddressControlPLC.AllowInternalTab = false;
            this.ipAddressControlPLC.AutoHeight = true;
            this.ipAddressControlPLC.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlPLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlPLC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlPLC.Location = new System.Drawing.Point(250, 325);
            this.ipAddressControlPLC.MinimumSize = new System.Drawing.Size(96, 21);
            this.ipAddressControlPLC.Name = "ipAddressControlPLC";
            this.ipAddressControlPLC.ReadOnly = false;
            this.ipAddressControlPLC.Size = new System.Drawing.Size(96, 21);
            this.ipAddressControlPLC.TabIndex = 74;
            this.ipAddressControlPLC.Text = "...";
            this.ipAddressControlPLC.TextChanged += new System.EventHandler(this.ipAddressControlPLC_TextChanged);
            // 
            // GreenPB
            // 
            this.GreenPB.Image = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            this.GreenPB.Location = new System.Drawing.Point(423, 284);
            this.GreenPB.Name = "GreenPB";
            this.GreenPB.Size = new System.Drawing.Size(32, 32);
            this.GreenPB.TabIndex = 14;
            this.GreenPB.TabStop = false;
            this.GreenPB.Visible = false;
            // 
            // GreyPB
            // 
            this.GreyPB.Image = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            this.GreyPB.Location = new System.Drawing.Point(12, 282);
            this.GreyPB.Name = "GreyPB";
            this.GreyPB.Size = new System.Drawing.Size(32, 32);
            this.GreyPB.TabIndex = 75;
            this.GreyPB.TabStop = false;
            this.GreyPB.Visible = false;
            // 
            // ConnIndicatePB
            // 
            this.ConnIndicatePB.Image = ((System.Drawing.Image)(resources.GetObject("ConnIndicatePB.Image")));
            this.ConnIndicatePB.Location = new System.Drawing.Point(361, 320);
            this.ConnIndicatePB.Name = "ConnIndicatePB";
            this.ConnIndicatePB.Size = new System.Drawing.Size(32, 32);
            this.ConnIndicatePB.TabIndex = 76;
            this.ConnIndicatePB.TabStop = false;
            // 
            // buttonExportName
            // 
            this.buttonExportName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonExportName.Location = new System.Drawing.Point(406, 322);
            this.buttonExportName.Name = "buttonExportName";
            this.buttonExportName.Size = new System.Drawing.Size(49, 31);
            this.buttonExportName.TabIndex = 77;
            this.buttonExportName.Text = "Export";
            this.buttonExportName.UseVisualStyleBackColor = true;
            this.buttonExportName.Click += new System.EventHandler(this.buttonExportName_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 178;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Type";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 40;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "DataType";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 60;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Value";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // InovanceSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 410);
            this.Controls.Add(this.buttonExportName);
            this.Controls.Add(this.ConnIndicatePB);
            this.Controls.Add(this.GreyPB);
            this.Controls.Add(this.GreenPB);
            this.Controls.Add(this.ipAddressControlPLC);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDataType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSetValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbStartAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAddressType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InovanceSettingForm";
            this.Text = "InovanceSettingForm";
            this.Load += new System.EventHandler(this.InovanceSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreyPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnIndicatePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDataType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSetValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStartAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAddressType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxName;
        private IPAddressControlLib.IPAddressControl ipAddressControlPLC;
        private System.Windows.Forms.PictureBox GreenPB;
        public System.Windows.Forms.PictureBox GreyPB;
        public System.Windows.Forms.PictureBox ConnIndicatePB;
        private System.Windows.Forms.Button buttonExportName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}