namespace WorldGeneralLib.DataLogicBarCode
{
    partial class FormDatalogicBarcode
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
            this.labelName = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.txtBarcodeName = new System.Windows.Forms.TextBox();
            this.panelBarcodeData = new System.Windows.Forms.Panel();
            this.listViewBarcode = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 204);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 12);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name:";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Location = new System.Drawing.Point(106, 240);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(97, 29);
            this.buttonRemove.TabIndex = 9;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(3, 240);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(97, 29);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // txtBarcodeName
            // 
            this.txtBarcodeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBarcodeName.Location = new System.Drawing.Point(53, 201);
            this.txtBarcodeName.Name = "txtBarcodeName";
            this.txtBarcodeName.Size = new System.Drawing.Size(150, 21);
            this.txtBarcodeName.TabIndex = 8;
            // 
            // panelBarcodeData
            // 
            this.panelBarcodeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBarcodeData.Location = new System.Drawing.Point(209, 0);
            this.panelBarcodeData.Name = "panelBarcodeData";
            this.panelBarcodeData.Size = new System.Drawing.Size(255, 286);
            this.panelBarcodeData.TabIndex = 13;
            // 
            // listViewBarcode
            // 
            this.listViewBarcode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewBarcode.Location = new System.Drawing.Point(0, 0);
            this.listViewBarcode.Name = "listViewBarcode";
            this.listViewBarcode.Size = new System.Drawing.Size(209, 188);
            this.listViewBarcode.TabIndex = 14;
            this.listViewBarcode.UseCompatibleStateImageBehavior = false;
            this.listViewBarcode.View = System.Windows.Forms.View.Details;
            this.listViewBarcode.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewBarcode_ItemSelectionChanged);
            this.listViewBarcode.SelectedIndexChanged += new System.EventHandler(this.listViewBarcode_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 281;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewBarcode);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Controls.Add(this.txtBarcodeName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 286);
            this.panel1.TabIndex = 15;
            // 
            // FormDatalogicBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 286);
            this.Controls.Add(this.panelBarcodeData);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDatalogicBarcode";
            this.Text = "FormDatalogicBarcode";
            this.Load += new System.EventHandler(this.FormDatalogicBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox txtBarcodeName;
        private System.Windows.Forms.Panel panelBarcodeData;
        private System.Windows.Forms.ListView listViewBarcode;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
    }
}