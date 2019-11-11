namespace ProFrame
{
    partial class CurveGraph
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picCurveShow = new System.Windows.Forms.PictureBox();
            this.labShowView = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCurveShow)).BeginInit();
            this.SuspendLayout();
            // 
            // picCurveShow
            // 
            this.picCurveShow.BackColor = System.Drawing.Color.Black;
            this.picCurveShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCurveShow.ErrorImage = null;
            this.picCurveShow.Location = new System.Drawing.Point(0, 0);
            this.picCurveShow.Name = "picCurveShow";
            this.picCurveShow.Size = new System.Drawing.Size(933, 693);
            this.picCurveShow.TabIndex = 0;
            this.picCurveShow.TabStop = false;
            this.picCurveShow.SizeChanged += new System.EventHandler(this.picCurveShow_SizeChanged);
            this.picCurveShow.MouseLeave += new System.EventHandler(this.picCurveShow_MouseLeave);
            this.picCurveShow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCurveShow_MouseMove);
            // 
            // labShowView
            // 
            this.labShowView.AutoSize = true;
            this.labShowView.BackColor = System.Drawing.Color.Black;
            this.labShowView.ForeColor = System.Drawing.Color.Red;
            this.labShowView.Location = new System.Drawing.Point(369, 300);
            this.labShowView.Name = "labShowView";
            this.labShowView.Size = new System.Drawing.Size(49, 14);
            this.labShowView.TabIndex = 1;
            this.labShowView.Text = "label1";
            this.labShowView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurveGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labShowView);
            this.Controls.Add(this.picCurveShow);
            this.Name = "CurveGraph";
            this.Size = new System.Drawing.Size(933, 693);
            this.Load += new System.EventHandler(this.CurveGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCurveShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCurveShow;
        private System.Windows.Forms.Label labShowView;
    }
}
