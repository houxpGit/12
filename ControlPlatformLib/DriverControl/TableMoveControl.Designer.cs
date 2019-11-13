namespace ControlPlatformLib
{
    partial class TableMoveControl
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
            this.buttonBit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBit
            // 
            this.buttonBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBit.Location = new System.Drawing.Point(0, 0);
            this.buttonBit.Name = "buttonBit";
            this.buttonBit.Size = new System.Drawing.Size(91, 35);
            this.buttonBit.TabIndex = 0;
            this.buttonBit.Text = "Move";
            this.buttonBit.UseVisualStyleBackColor = true;
            this.buttonBit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonBit_MouseDown);
            // 
            // TableMoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBit);
            this.Name = "TableMoveControl";
            this.Size = new System.Drawing.Size(91, 35);
            this.Load += new System.EventHandler(this.TableMoveControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBit;
    }
}
