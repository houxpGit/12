namespace FullyAutomaticLaserJetCoder
{
    partial class 波形展示
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.最小化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curveGraph1 = new ProFrame.CurveGraph();
            this.功率 = new System.Windows.Forms.TextBox();
            this.设备功率 = new System.Windows.Forms.Label();
            this.设置最大功率 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最小化ToolStripMenuItem,
            this.最大化ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1073, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            this.menuStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseMove);
            // 
            // 最小化ToolStripMenuItem
            // 
            this.最小化ToolStripMenuItem.Name = "最小化ToolStripMenuItem";
            this.最小化ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.最小化ToolStripMenuItem.Text = "最小化";
            // 
            // 最大化ToolStripMenuItem
            // 
            this.最大化ToolStripMenuItem.Name = "最大化ToolStripMenuItem";
            this.最大化ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.最大化ToolStripMenuItem.Text = "最大化";
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Image = global::FullyAutomaticLaserJetCoder.Properties.Resources.Exit21;
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // curveGraph1
            // 
            this.curveGraph1.AxisColor = System.Drawing.Color.Yellow;
            this.curveGraph1.AxisPrecision = 5;
            this.curveGraph1.BackGroundColor = System.Drawing.Color.Black;
            this.curveGraph1.CarveRectangleColor = System.Drawing.Color.White;
            this.curveGraph1.CarveRectangleWidth = 1F;
            this.curveGraph1.CarveTitle = "CurveGraph";
            this.curveGraph1.CurveColor = System.Drawing.Color.White;
            this.curveGraph1.CurvePenWidth = 1F;
            this.curveGraph1.GridColor = System.Drawing.Color.DarkGreen;
            this.curveGraph1.GridCompart = 1F;
            this.curveGraph1.GridFontColor = System.Drawing.Color.Yellow;
            this.curveGraph1.GridFontSize = 9F;
            this.curveGraph1.GridPenWidth = 1F;
            this.curveGraph1.Location = new System.Drawing.Point(15, 38);
            this.curveGraph1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.curveGraph1.Name = "curveGraph1";
            this.curveGraph1.Size = new System.Drawing.Size(894, 550);
            this.curveGraph1.TabIndex = 0;
            this.curveGraph1.TitleColor = System.Drawing.Color.Yellow;
            this.curveGraph1.TitleFont = new System.Drawing.Font("Arial", 12F);
            this.curveGraph1.XAxisTitle = "X轴";
            this.curveGraph1.XMaxValue = 2F;
            this.curveGraph1.XMinValue = 0F;
            this.curveGraph1.XOffset = 50F;
            this.curveGraph1.XOrigin = 0F;
            this.curveGraph1.XScale = 0.2F;
            this.curveGraph1.XYPrecision = 4F;
            this.curveGraph1.YAxisTitle = "Y轴";
            this.curveGraph1.YMaxValue = 6000F;
            this.curveGraph1.YMinValue = 0F;
            this.curveGraph1.YOffset = 180F;
            this.curveGraph1.YOrigin = 0F;
            this.curveGraph1.YScale = 500F;
            // 
            // 功率
            // 
            this.功率.Location = new System.Drawing.Point(915, 85);
            this.功率.Name = "功率";
            this.功率.Size = new System.Drawing.Size(118, 25);
            this.功率.TabIndex = 2;
            // 
            // 设备功率
            // 
            this.设备功率.AutoSize = true;
            this.设备功率.Location = new System.Drawing.Point(915, 67);
            this.设备功率.Name = "设备功率";
            this.设备功率.Size = new System.Drawing.Size(82, 15);
            this.设备功率.TabIndex = 3;
            this.设备功率.Text = "设备功率：";
            // 
            // 设置最大功率
            // 
            this.设置最大功率.Location = new System.Drawing.Point(915, 116);
            this.设置最大功率.Name = "设置最大功率";
            this.设置最大功率.Size = new System.Drawing.Size(118, 35);
            this.设置最大功率.TabIndex = 4;
            this.设置最大功率.Text = "设置";
            this.设置最大功率.UseVisualStyleBackColor = true;
            this.设置最大功率.Click += new System.EventHandler(this.设置最大功率_Click);
            // 
            // 波形展示
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 629);
            this.Controls.Add(this.设置最大功率);
            this.Controls.Add(this.设备功率);
            this.Controls.Add(this.功率);
            this.Controls.Add(this.curveGraph1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "波形展示";
            this.Text = "波形展示";
            this.Load += new System.EventHandler(this.波形展示_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProFrame.CurveGraph curveGraph1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 最小化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.TextBox 功率;
        private System.Windows.Forms.Label 设备功率;
        private System.Windows.Forms.Button 设置最大功率;
    }
}