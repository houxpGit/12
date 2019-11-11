using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin.SkinControl;
using CCWin;

namespace ControlPlatformLib
{
    public partial class LoginForm : Form
    {
        public static bool landingFinish = false;
        public static bool landingOk = false;
        bool bSaveNamePassword;
        bool bIgnoreMes;
        WebReference.MESInterface mes = new WebReference.MESInterface();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_User.Text))
                {
                    MessageBox.Show("请输入用户名！");
                    return;
                }
                if (string.IsNullOrEmpty(txt_Password.Text))
                {
                    MessageBox.Show("请输入密码！");
                    return;
                }
                if (string.IsNullOrEmpty(txt_MachineNO.Text))
                {
                    MessageBox.Show("请输入设备编号！");
                    return;
                }
                if (string.IsNullOrEmpty(txt_Pos.Text))
                {
                    MessageBox.Show("请输入设备位置！");
                    return;
                }
                if (bSaveNamePassword)
                {
                    Properties.Settings.Default.UserName = txt_User.Text;
                    Properties.Settings.Default.Password = txt_Password.Text;
                    Properties.Settings.Default.MachineNo = txt_MachineNO.Text;
                    Properties.Settings.Default.MakeOrder = txt_MO.Text;
                    Properties.Settings.Default.MachineNoRight = txt_MachineNoRight.Text;
                    Properties.Settings.Default.DoubleStation = cb_DoubleStation.Checked;
                    Properties.Settings.Default.MachinePosition = txt_Pos.Text;
                    Properties.Settings.Default.Save();
                }
                if (!Properties.Settings.Default.DoubleStation)
                {
                    txt_MachineNoRight.Text = txt_MachineNO.Text;
                }
                Global.sMachineNO = txt_MachineNO.Text;
                Global.sMachineNORight = txt_MachineNoRight.Text;
                Global.sMakeOrder = txt_MO.Text;
                Global.sUserName = txt_User.Text;
                Global.sMachinePos = txt_Pos.Text;
                if (bIgnoreMes)
                {
                    if (txt_Password.Text == "123456")
                    {
                        landingOk = true;
                        landingFinish = true;
                           DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        landingFinish = false;
                        MessageBox.Show("密码错误！");
                        return;
                    }
                }
                else
                {
                  
                    string result = mes.CheckUserDo(txt_User.Text, txt_Password.Text, txt_MachineNO.Text);
                    string result1 = mes.CheckUserDo(txt_User.Text, txt_Password.Text, txt_MachineNoRight.Text);

                    if (result.ToUpper() == "TRUE"&& result1.ToUpper()=="TRUE")
                    {
                        landingOk = true;
                        landingFinish = true;

                         DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        landingFinish = false;
                        MessageBox.Show("登录失败！"+ result+result1);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txt_User.Text = Properties.Settings.Default.UserName;
            txt_Password.Text = Properties.Settings.Default.Password;
            txt_MachineNO.Text = Properties.Settings.Default.MachineNo;
            txt_MO.Text = Properties.Settings.Default.MakeOrder;
            txt_MachineNoRight.Text = Properties.Settings.Default.MachineNoRight;
            txt_Pos.Text = Properties.Settings.Default.MachinePosition;
            cb_DoubleStation.Checked = Properties.Settings.Default.DoubleStation;
            timer1.Start();
        }

        private void cb_SaveNamePassword_CheckedChanged(object sender, EventArgs e)
        {
            bSaveNamePassword = cb_SaveNamePassword.Checked;
        }

        private void cb_IgnoreMes_CheckedChanged(object sender, EventArgs e)
        {
            bIgnoreMes = cb_IgnoreMes.Checked;
            Global.bIgnoreMes = cb_IgnoreMes.Checked;
        }

        private void cb_DoubleStation_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DoubleStation = cb_DoubleStation.Checked;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.DoubleStation)
            {
                skinLine1.Location = new Point() { X=skinLine1.Location.X,Y=skinLine1.Location.Y+38};
                txt_MachineNoRight.Location = new Point() { X = txt_MachineNoRight.Location.X, Y = txt_MachineNoRight.Location.Y + 38 };
                cb_DoubleStation.Location = new Point() { X = cb_DoubleStation.Location.X, Y = cb_DoubleStation.Location.Y + 38 };
                cb_IgnoreMes.Location = new Point() { X = cb_IgnoreMes.Location.X, Y = cb_IgnoreMes.Location.Y + 38 };
                cb_SaveNamePassword.Location = new Point() { X = cb_SaveNamePassword.Location.X, Y = cb_SaveNamePassword.Location.Y + 38 };
                btn_Login.Location = new Point() { X = btn_Login.Location.X, Y = btn_Login.Location.Y + 38 };
                label1.Visible = true;
                label2.Visible = true;
                txt_MachineNoRight.Visible = true;
            }
            else
            {
                skinLine1.Location = new Point() { X = skinLine1.Location.X, Y = skinLine1.Location.Y - 38 };
                txt_MachineNoRight.Location = new Point() { X = txt_MachineNoRight.Location.X, Y = txt_MachineNoRight.Location.Y - 38 };
                cb_DoubleStation.Location = new Point() { X = cb_DoubleStation.Location.X, Y = cb_DoubleStation.Location.Y - 38 };
                cb_IgnoreMes.Location = new Point() { X = cb_IgnoreMes.Location.X, Y = cb_IgnoreMes.Location.Y - 38 };
                cb_SaveNamePassword.Location = new Point() { X = cb_SaveNamePassword.Location.X, Y = cb_SaveNamePassword.Location.Y - 38 };
                btn_Login.Location = new Point() { X = btn_Login.Location.X, Y = btn_Login.Location.Y - 38 };
                txt_MachineNoRight.Text = txt_MachineNO.Text;
                label1.Visible = false;
                label2.Visible = false;
                txt_MachineNoRight.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Top = 0;

            this.Left = 0;

            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            skinPanel1.SetBounds(this.Width/2 - skinPanel1.Width / 2, this.Height/ 2 - skinPanel1.Height / 2, skinPanel1.Width, skinPanel1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.Top = 0;

            //this.Left = 0;

            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //skinPanel1.SetBounds(this.Width / 2 - skinPanel1.Width / 2, this.Height / 2 - skinPanel1.Height / 2, skinPanel1.Width, skinPanel1.Height);
            ////if (this.Width / 2 - skinPanel1.Width / 2== skinPanel1.Top)
            ////{

            //timer1.Stop();
        
    }
    }
}
