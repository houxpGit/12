namespace ControlPlatformLib
{
    partial class FormProductInfo
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Input",
            "0",
            "Clr"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Pass",
            "0",
            "Clr"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Fail",
            "0",
            "Clr"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Yield",
            "0",
            "%"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Retry",
            "0",
            "%"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "CT",
            "0.0",
            "123.0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "UPH",
            "0",
            "None"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "TTime",
            "00:00:00",
            "Clr"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "WTime",
            "00:00:00",
            "Clr"}, -1);
            this.listViewProductInfo = new WorldGeneralLib.ListViewNF();
            this.columnHeaderItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader85 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewProductInfo
            // 
            this.listViewProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewProductInfo.BackColor = System.Drawing.Color.LightSalmon;
            this.listViewProductInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderItem,
            this.columnHeaderMes,
            this.columnHeader85});
            this.listViewProductInfo.Font = new System.Drawing.Font("宋体", 18F);
            this.listViewProductInfo.FullRowSelect = true;
            this.listViewProductInfo.GridLines = true;
            listViewItem1.UseItemStyleForSubItems = false;
            listViewItem2.UseItemStyleForSubItems = false;
            listViewItem3.UseItemStyleForSubItems = false;
            listViewItem4.UseItemStyleForSubItems = false;
            listViewItem5.UseItemStyleForSubItems = false;
            listViewItem6.ToolTipText = "单品时间";
            listViewItem6.UseItemStyleForSubItems = false;
            listViewItem7.ToolTipText = "每个小时出货数量";
            listViewItem7.UseItemStyleForSubItems = false;
            listViewItem8.ToolTipText = "软件运行时间";
            listViewItem8.UseItemStyleForSubItems = false;
            listViewItem9.ToolTipText = "自动做货时间";
            listViewItem9.UseItemStyleForSubItems = false;
            this.listViewProductInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.listViewProductInfo.Location = new System.Drawing.Point(0, 0);
            this.listViewProductInfo.MultiSelect = false;
            this.listViewProductInfo.Name = "listViewProductInfo";
            this.listViewProductInfo.Size = new System.Drawing.Size(421, 285);
            this.listViewProductInfo.TabIndex = 1;
            this.listViewProductInfo.UseCompatibleStateImageBehavior = false;
            this.listViewProductInfo.View = System.Windows.Forms.View.Details;
            this.listViewProductInfo.SelectedIndexChanged += new System.EventHandler(this.listViewProductInfo_SelectedIndexChanged);
            // 
            // columnHeaderItem
            // 
            this.columnHeaderItem.Text = "Item";
            this.columnHeaderItem.Width = 89;
            // 
            // columnHeaderMes
            // 
            this.columnHeaderMes.Text = "Message";
            this.columnHeaderMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderMes.Width = 195;
            // 
            // columnHeader85
            // 
            this.columnHeader85.Text = "";
            this.columnHeader85.Width = 122;
            // 
            // FormProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 284);
            this.Controls.Add(this.listViewProductInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProductInfo";
            this.Text = "FormProductInfo";
            this.ResumeLayout(false);

        }

        #endregion

        public WorldGeneralLib.ListViewNF listViewProductInfo;
        private System.Windows.Forms.ColumnHeader columnHeaderItem;
        private System.Windows.Forms.ColumnHeader columnHeaderMes;
        private System.Windows.Forms.ColumnHeader columnHeader85;
    }
}