namespace WorldGeneralLib.CognexAsync
{
    partial class ImageConfirmForm
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
            this.buttonForceQuit = new System.Windows.Forms.Button();
            this.buttonRecheck = new System.Windows.Forms.Button();
            this.buttonIgnore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonForceQuit
            // 
            this.buttonForceQuit.Location = new System.Drawing.Point(197, 120);
            this.buttonForceQuit.Name = "buttonForceQuit";
            this.buttonForceQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonForceQuit.TabIndex = 5;
            this.buttonForceQuit.Text = "强制退出";
            this.buttonForceQuit.UseVisualStyleBackColor = true;
            this.buttonForceQuit.Click += new System.EventHandler(this.buttonForceQuit_Click);
            // 
            // buttonRecheck
            // 
            this.buttonRecheck.Location = new System.Drawing.Point(105, 120);
            this.buttonRecheck.Name = "buttonRecheck";
            this.buttonRecheck.Size = new System.Drawing.Size(75, 23);
            this.buttonRecheck.TabIndex = 4;
            this.buttonRecheck.Text = "重拍";
            this.buttonRecheck.UseVisualStyleBackColor = true;
            this.buttonRecheck.Click += new System.EventHandler(this.buttonRecheck_Click);
            // 
            // buttonIgnore
            // 
            this.buttonIgnore.Location = new System.Drawing.Point(12, 120);
            this.buttonIgnore.Name = "buttonIgnore";
            this.buttonIgnore.Size = new System.Drawing.Size(75, 23);
            this.buttonIgnore.TabIndex = 3;
            this.buttonIgnore.Text = "忽略";
            this.buttonIgnore.UseVisualStyleBackColor = true;
            this.buttonIgnore.Click += new System.EventHandler(this.buttonIgnore_Click);
            // 
            // ImageConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonForceQuit);
            this.Controls.Add(this.buttonRecheck);
            this.Controls.Add(this.buttonIgnore);
            this.Name = "ImageConfirmForm";
            this.Text = "图像结果确认操作框";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonForceQuit;
        private System.Windows.Forms.Button buttonRecheck;
        private System.Windows.Forms.Button buttonIgnore;
    }
}