namespace ControlPlatformLib
{
    partial class FormIOSetting
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
            this.textBoxInputName = new System.Windows.Forms.TextBox();
            this.buttonAddInput = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRemoveInput = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRemoveOutput = new System.Windows.Forms.Button();
            this.buttonAddOutput = new System.Windows.Forms.Button();
            this.textBoxOutputName = new System.Windows.Forms.TextBox();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputCardNameColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InputNoColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonExport = new System.Windows.Forms.Button();
            this.wIPTESTSAMPLECompletedEventArgsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wIPTESTSAMPLECompletedEventArgsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInputName
            // 
            this.textBoxInputName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxInputName.Location = new System.Drawing.Point(282, 588);
            this.textBoxInputName.Name = "textBoxInputName";
            this.textBoxInputName.Size = new System.Drawing.Size(133, 21);
            this.textBoxInputName.TabIndex = 3;
            // 
            // buttonAddInput
            // 
            this.buttonAddInput.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAddInput.Location = new System.Drawing.Point(421, 571);
            this.buttonAddInput.Name = "buttonAddInput";
            this.buttonAddInput.Size = new System.Drawing.Size(76, 39);
            this.buttonAddInput.TabIndex = 4;
            this.buttonAddInput.Text = "Add";
            this.buttonAddInput.UseVisualStyleBackColor = true;
            this.buttonAddInput.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 571);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name";
            // 
            // buttonRemoveInput
            // 
            this.buttonRemoveInput.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRemoveInput.Location = new System.Drawing.Point(501, 570);
            this.buttonRemoveInput.Name = "buttonRemoveInput";
            this.buttonRemoveInput.Size = new System.Drawing.Size(76, 39);
            this.buttonRemoveInput.TabIndex = 4;
            this.buttonRemoveInput.Text = "Remove";
            this.buttonRemoveInput.UseVisualStyleBackColor = true;
            this.buttonRemoveInput.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(236, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "Input";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 20F);
            this.label3.Location = new System.Drawing.Point(799, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 27);
            this.label3.TabIndex = 13;
            this.label3.Text = "Output";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(899, 574);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Name";
            // 
            // buttonRemoveOutput
            // 
            this.buttonRemoveOutput.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRemoveOutput.Location = new System.Drawing.Point(1056, 573);
            this.buttonRemoveOutput.Name = "buttonRemoveOutput";
            this.buttonRemoveOutput.Size = new System.Drawing.Size(76, 39);
            this.buttonRemoveOutput.TabIndex = 10;
            this.buttonRemoveOutput.Text = "Remove";
            this.buttonRemoveOutput.UseVisualStyleBackColor = true;
            this.buttonRemoveOutput.Click += new System.EventHandler(this.buttonRemoveOutput_Click);
            // 
            // buttonAddOutput
            // 
            this.buttonAddOutput.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAddOutput.Location = new System.Drawing.Point(976, 574);
            this.buttonAddOutput.Name = "buttonAddOutput";
            this.buttonAddOutput.Size = new System.Drawing.Size(76, 39);
            this.buttonAddOutput.TabIndex = 11;
            this.buttonAddOutput.Text = "Add";
            this.buttonAddOutput.UseVisualStyleBackColor = true;
            this.buttonAddOutput.Click += new System.EventHandler(this.buttonAddOutput_Click);
            // 
            // textBoxOutputName
            // 
            this.textBoxOutputName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxOutputName.Location = new System.Drawing.Point(837, 591);
            this.textBoxOutputName.Name = "textBoxOutputName";
            this.textBoxOutputName.Size = new System.Drawing.Size(133, 21);
            this.textBoxOutputName.TabIndex = 9;
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToAddRows = false;
            this.dataGridViewInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.InputCardNameColumn,
            this.InputNoColumn,
            this.Column4,
            this.Column5});
            this.dataGridViewInput.Location = new System.Drawing.Point(108, 45);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.RowHeadersWidth = 10;
            this.dataGridViewInput.RowTemplate.Height = 23;
            this.dataGridViewInput.Size = new System.Drawing.Size(497, 508);
            this.dataGridViewInput.TabIndex = 14;
            this.dataGridViewInput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInput_CellEndEdit);
            this.dataGridViewInput.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInput_CellValidated);
            this.dataGridViewInput.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewInput_DataError);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "strIOName";
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // InputCardNameColumn
            // 
            this.InputCardNameColumn.DataPropertyName = "InputCardName";
            this.InputCardNameColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.InputCardNameColumn.HeaderText = "CardName";
            this.InputCardNameColumn.Name = "InputCardNameColumn";
            // 
            // InputNoColumn
            // 
            this.InputNoColumn.DataPropertyName = "iInputNo";
            this.InputNoColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.InputNoColumn.HeaderText = "No";
            this.InputNoColumn.Name = "InputNoColumn";
            this.InputNoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.InputNoColumn.Width = 40;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "bignore";
            this.Column4.HeaderText = "Ignore";
            this.Column4.Name = "Column4";
            this.Column4.Width = 50;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "strRemark";
            this.Column5.HeaderText = "Remard";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.AllowUserToAddRows = false;
            this.dataGridViewOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewOutput.Location = new System.Drawing.Point(629, 45);
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.RowHeadersWidth = 10;
            this.dataGridViewOutput.RowTemplate.Height = 23;
            this.dataGridViewOutput.Size = new System.Drawing.Size(503, 508);
            this.dataGridViewOutput.TabIndex = 15;
            this.dataGridViewOutput.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellClick);
            this.dataGridViewOutput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellEndEdit);
            this.dataGridViewOutput.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellValidated);
            this.dataGridViewOutput.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewInput_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.dataGridViewComboBoxColumn1.HeaderText = "CardName";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.dataGridViewComboBoxColumn2.HeaderText = "No";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Width = 40;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Ignore";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Remard";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonExport.Location = new System.Drawing.Point(651, 571);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(76, 39);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // wIPTESTSAMPLECompletedEventArgsBindingSource
            // 
            this.wIPTESTSAMPLECompletedEventArgsBindingSource.DataSource = typeof(ControlPlatformLib.WebReference.WIPTESTSAMPLECompletedEventArgs);
            // 
            // FormIOSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1151, 631);
            this.Controls.Add(this.dataGridViewOutput);
            this.Controls.Add(this.dataGridViewInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonRemoveOutput);
            this.Controls.Add(this.buttonAddOutput);
            this.Controls.Add(this.textBoxOutputName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonRemoveInput);
            this.Controls.Add(this.buttonAddInput);
            this.Controls.Add(this.textBoxInputName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormIOSetting";
            this.Text = "FormHardSetting";
            this.Load += new System.EventHandler(this.FormIOSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wIPTESTSAMPLECompletedEventArgsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInputName;
        private System.Windows.Forms.Button buttonAddInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRemoveInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRemoveOutput;
        private System.Windows.Forms.Button buttonAddOutput;
        private System.Windows.Forms.TextBox textBoxOutputName;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.DataGridView dataGridViewOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.BindingSource wIPTESTSAMPLECompletedEventArgsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn InputCardNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn InputNoColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}