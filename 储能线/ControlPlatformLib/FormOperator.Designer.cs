namespace ControlPlatformLib
{
    partial class FormOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperator));
            this.buttonHome = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonHome
            // 
            this.buttonHome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHome.BackColor = System.Drawing.Color.Transparent;
            this.buttonHome.Font = new System.Drawing.Font("宋体", 18F);
            this.buttonHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHome.ImageIndex = 0;
            this.buttonHome.ImageList = this.imageList1;
            this.buttonHome.Location = new System.Drawing.Point(239, 0);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(132, 101);
            this.buttonHome.TabIndex = 5;
            this.buttonHome.Text = "初始化";
            this.buttonHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonHome.UseVisualStyleBackColor = false;
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "H.png");
            this.imageList1.Images.SetKeyName(1, "reset.png");
            this.imageList1.Images.SetKeyName(2, "start.png");
            this.imageList1.Images.SetKeyName(3, "stop.png");
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.Red;
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonStop.Font = new System.Drawing.Font("宋体", 18F);
            this.buttonStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStop.ImageIndex = 3;
            this.buttonStop.ImageList = this.imageList1;
            this.buttonStop.Location = new System.Drawing.Point(371, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(128, 101);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "停止";
            this.buttonStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReset.BackColor = System.Drawing.Color.Transparent;
            this.buttonReset.Font = new System.Drawing.Font("宋体", 18F);
            this.buttonReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReset.ImageKey = "reset.png";
            this.buttonReset.ImageList = this.imageList1;
            this.buttonReset.Location = new System.Drawing.Point(120, 0);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(121, 101);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "复位";
            this.buttonReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            this.buttonReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonReset_MouseDown);
            this.buttonReset.MouseLeave += new System.EventHandler(this.buttonReset_MouseLeave);
            this.buttonReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonReset_MouseUp);
            // 
            // timerScan
            // 
            this.timerScan.Interval = 700;
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Transparent;
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStart.Font = new System.Drawing.Font("宋体", 18F);
            this.buttonStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStart.ImageKey = "start.png";
            this.buttonStart.ImageList = this.imageList1;
            this.buttonStart.Location = new System.Drawing.Point(0, 0);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(121, 101);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "启动";
            this.buttonStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // FormOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 101);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonHome);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormOperator";
            this.Text = "FormOperator";
            this.Load += new System.EventHandler(this.FormOperator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.Button buttonStart;
    }
}