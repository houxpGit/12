namespace WorldGeneralLib
{
    partial class FormAlarm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarm));
            this.listViewAlarmCur = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewSuggest = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewAlarmHis = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxAlramOff = new System.Windows.Forms.CheckBox();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnNeglect = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewAlarmCur
            // 
            this.listViewAlarmCur.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewAlarmCur.FullRowSelect = true;
            this.listViewAlarmCur.LargeImageList = this.imageList1;
            this.listViewAlarmCur.Location = new System.Drawing.Point(3, 3);
            this.listViewAlarmCur.Name = "listViewAlarmCur";
            this.listViewAlarmCur.Size = new System.Drawing.Size(740, 449);
            this.listViewAlarmCur.SmallImageList = this.imageList1;
            this.listViewAlarmCur.TabIndex = 2;
            this.listViewAlarmCur.UseCompatibleStateImageBehavior = false;
            this.listViewAlarmCur.View = System.Windows.Forms.View.Details;
            this.listViewAlarmCur.SelectedIndexChanged += new System.EventHandler(this.listViewAlarmCur_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "stop.ico");
            this.imageList1.Images.SetKeyName(1, "warn.ico");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(30, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 481);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewAlarmCur);
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "现在报警";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(3, 116);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(740, 336);
            this.tabControl2.TabIndex = 5;
            this.tabControl2.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewSuggest);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(732, 310);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "处理方式建议";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewSuggest
            // 
            this.listViewSuggest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSuggest.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewSuggest.FullRowSelect = true;
            this.listViewSuggest.Location = new System.Drawing.Point(3, 6);
            this.listViewSuggest.Name = "listViewSuggest";
            this.listViewSuggest.Size = new System.Drawing.Size(723, 301);
            this.listViewSuggest.TabIndex = 5;
            this.listViewSuggest.UseCompatibleStateImageBehavior = false;
            this.listViewSuggest.View = System.Windows.Forms.View.Details;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(732, 310);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "图示";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(61, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(620, 297);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewAlarmHis);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "历史报警";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewAlarmHis
            // 
            this.listViewAlarmHis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.listViewAlarmHis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAlarmHis.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewAlarmHis.Location = new System.Drawing.Point(3, 3);
            this.listViewAlarmHis.Name = "listViewAlarmHis";
            this.listViewAlarmHis.Size = new System.Drawing.Size(743, 449);
            this.listViewAlarmHis.SmallImageList = this.imageList1;
            this.listViewAlarmHis.TabIndex = 3;
            this.listViewAlarmHis.UseCompatibleStateImageBehavior = false;
            this.listViewAlarmHis.View = System.Windows.Forms.View.Details;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(310, 499);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "复位报警";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // checkBoxAlramOff
            // 
            this.checkBoxAlramOff.AutoSize = true;
            this.checkBoxAlramOff.Location = new System.Drawing.Point(721, 507);
            this.checkBoxAlramOff.Name = "checkBoxAlramOff";
            this.checkBoxAlramOff.Size = new System.Drawing.Size(84, 16);
            this.checkBoxAlramOff.TabIndex = 6;
            this.checkBoxAlramOff.Text = "屏蔽蜂鸣器";
            this.checkBoxAlramOff.UseVisualStyleBackColor = true;
            this.checkBoxAlramOff.Click += new System.EventHandler(this.checkBoxAlramOff_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(455, 499);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(129, 31);
            this.btnRetry.TabIndex = 7;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Visible = false;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnNeglect
            // 
            this.btnNeglect.Location = new System.Drawing.Point(590, 499);
            this.btnNeglect.Name = "btnNeglect";
            this.btnNeglect.Size = new System.Drawing.Size(129, 31);
            this.btnNeglect.TabIndex = 8;
            this.btnNeglect.Text = "Neglect";
            this.btnNeglect.UseVisualStyleBackColor = true;
            this.btnNeglect.Visible = false;
            this.btnNeglect.Click += new System.EventHandler(this.btnNeglect_Click);
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(817, 533);
            this.Controls.Add(this.btnNeglect);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.checkBoxAlramOff);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAlarm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormAlarm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseUp);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView listViewAlarmCur;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.ListView listViewAlarmHis;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxAlramOff;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.ListView listViewSuggest;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnNeglect;
    }
}