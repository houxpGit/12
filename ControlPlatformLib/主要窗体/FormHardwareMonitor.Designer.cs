namespace ControlPlatformLib
{
    partial class FormHardwareMonitor
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
            this.comboBoxTables = new System.Windows.Forms.ComboBox();
            this.panelTable = new System.Windows.Forms.Panel();
            this.panelIO = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTables
            // 
            this.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTables.FormattingEnabled = true;
            this.comboBoxTables.Location = new System.Drawing.Point(3, 3);
            this.comboBoxTables.Name = "comboBoxTables";
            this.comboBoxTables.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTables.TabIndex = 0;
            this.comboBoxTables.SelectionChangeCommitted += new System.EventHandler(this.comboBoxTables_SelectionChangeCommitted);
            // 
            // panelTable
            // 
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(3, 25);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(771, 260);
            this.panelTable.TabIndex = 1;
            // 
            // panelIO
            // 
            this.panelIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIO.Location = new System.Drawing.Point(3, 291);
            this.panelIO.Name = "panelIO";
            this.panelIO.Size = new System.Drawing.Size(771, 168);
            this.panelIO.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxTables, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelIO, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelTable, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.93998F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.66426F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.39576F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 462);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FormHardwareMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHardwareMonitor";
            this.Text = "FormIOMonitor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormHardwareMonitor_FormClosed);
            this.Load += new System.EventHandler(this.FormIOMonitor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTables;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Panel panelIO;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}