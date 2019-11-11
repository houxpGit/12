using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class FormUserLogoIn : Form
    {
        

        public FormUserLogoIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateUserLevel();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateUserLevel();
            }
        }
        private void UpdateUserLevel()
        {
            if (this.textBox1.Text == "123456")
            {
                //MainModule.FormMain.labelUserLevel.Text = "用户:工程师";
                MainModule.FormMain.UesrChanges.Text = "用户:工程师";
                MainModule.FormMain.m_iUserLevel = 1;
                
                Close();
                return;
            }
            if (this.textBox1.Text == "654321")
            {
                MainModule.FormMain.UesrChanges.Text = "用户:管理员";
              //  MainModule.FormMain.labelUserLevel.Text = "用户:管理员";
                MainModule.FormMain.m_iUserLevel = 2;
                
                Close();
                return;
            }
            if (this.textBox1.Text == "zdh666")
            {
                MainModule.FormMain.UesrChanges.Text = "用户:超级管理员";
              //  MainModule.FormMain.labelUserLevel.Text = "用户:超级管理员";
                MainModule.FormMain.m_iUserLevel = 3;
                Close();
                return;
            }
            MainModule.FormMain.UesrChanges.Text = "用户:操作员";
           // MainModule.FormMain.labelUserLevel.Text = "用户:操作员";
            MainModule.FormMain.m_iUserLevel = 0;
            Close();
            return;
        }

        private void FormUserLogoIn_Load(object sender, EventArgs e)
        {

        }
    }
}
