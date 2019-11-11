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
    public partial class FormAlarm : Form
    {
        private bool bMouseDown = false;
        private int iMouseDownX = 0;
        private int iMouseDownY = 0;
        private bool bEstop = false;
        private bool bDoorOpen = false;
        private bool[] bMotorAlarm;
        private bool[] bMotorCWLim;
        private bool[] bMotorCCWLim;
        public FormAlarm()
        {
            bMotorAlarm = new bool[200];
            bMotorCWLim = new bool[200];
            bMotorCCWLim = new bool[200];
            for (int i = 0; i < 20; i++)
            {
                bMotorAlarm[i] = false;
                bMotorCWLim[i] = false;
                bMotorCCWLim[i] = false;
            }
            InitializeComponent();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listViewAlarmCur.Items.Count > 0)
            {
                if (this.Visible == false)
                {
                    this.Show();
                    this.BringToFront();
                }
                else
                {
                    this.BringToFront();
                }
            }
            else
            {
                if (this.Visible)
                {
                    this.Hide();
                }
            }
            if (this.Visible)
            {
                //Program.FormMain.m_MotionCard1.SetOutputBit(8);
                //Program.FormMain.m_MotionCard1.SetOutputBit(12);
                //if (checkBoxAlramOff.Checked == false)
                //{
                //    Program.FormMain.m_MotionCard1.SetOutputBit(9);
                //}
                //else
                //{
                //    Program.FormMain.m_MotionCard1.RstOutputBit(9);
                //}
                //Program.FormMain.m_MotionCard1.RstOutputBit(0);
                //Program.FormMain.m_MotionCard1.RstOutputBit(1);
                //Program.FormMain.m_MotionCard1.RstOutputBit(10);
                //Program.FormMain.m_MotionCard1.RstOutputBit(11);
            }
            else
            {
                //if (Program.FormMain.bAuto)
                //{
                //    Program.FormMain.m_MotionCard1.SetOutputBit(1);
                //    Program.FormMain.m_MotionCard1.SetOutputBit(10);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(0);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(8);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(9);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(11);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(12);

                //}
                //else
                //{
                //    Program.FormMain.m_MotionCard1.SetOutputBit(0);
                //    Program.FormMain.m_MotionCard1.SetOutputBit(11);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(1);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(8);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(9);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(10);
                //    Program.FormMain.m_MotionCard1.RstOutputBit(12);

                //}

            }
        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {

            listViewAlarmCur.Columns.Add("Message");
            listViewAlarmCur.Columns.Add("Time");
            listViewAlarmCur.Columns[0].Width = listViewAlarmCur.Width - 350;
            listViewAlarmCur.Columns[1].Width = 350;
            listViewAlarmHis.Columns.Add("Message");
            listViewAlarmHis.Columns.Add("Time");
            listViewAlarmHis.Columns[0].Width = listViewAlarmHis.Width - 350;
            listViewAlarmHis.Columns[1].Width = 350;

        }
        public void InsertAlarmMessage(string strMessage)
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, strMessage, 1);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, strMessage, 1);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }

        private void FormAlarm_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;

            iMouseDownX = e.X;
            iMouseDownY = e.Y;
            label3.Text = "X:" + iMouseDownX.ToString() + "Y:" + iMouseDownY.ToString();
        }

        private void FormAlarm_MouseUp(object sender, MouseEventArgs e)
        {

            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;

                label1.Text = "X:" + iMouseDownX.ToString() + "Y:" + iMouseDownY.ToString();

                label2.Text = "X:" + iXoffset.ToString() + "Y:" + iYoffset.ToString();
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;

            }
            bMouseDown = false;
        }

        private void FormAlarm_MouseMove(object sender, MouseEventArgs e)
        {
            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;

                label1.Text = "X:" + e.X.ToString() + "Y:" + e.Y.ToString();

                label2.Text = "X:" + iXoffset.ToString() + "Y:" + iYoffset.ToString();
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;

            }

        }
        private bool GetAlarm()
        {
            if (bEstop || bDoorOpen)
                return true;
            for (int i = 0; i < 20; i++)
            {
                if (bMotorAlarm[i])
                {
                    return true;
                }
                if (bMotorCWLim[i])
                {
                    return true;
                }
                if (bMotorCCWLim[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void SetEstopAlarm()
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, "ESTOP", "E-Stop", 0);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            RemoveHisItem();
            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, "ESTOP", "E-Stop", 0);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }
        public void RstEstopAlarm()
        {
            listViewAlarmCur.Items.RemoveByKey("ESTOP");
        }
        public void SetDoorOpenAlarm()
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, "DoorOpen", "DoorOpen", 0);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            RemoveHisItem();
            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, "DoorOpen", "DoorOpen", 0);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }
        public void RstDoorOpenAlarm()
        {
            listViewAlarmCur.Items.RemoveByKey("DoorOpen");
        }
        public void SetMotorAlarm(string MotorName)
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "Alarm", MotorName + "马达报警", 0);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            RemoveHisItem();
            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "Alarm", MotorName + "马达报警", 0);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }
        public void RstMotorAlarm(string MotorName)
        {
            listViewAlarmCur.Items.RemoveByKey(MotorName + "Alarm");
        }
        public void SetMotorCWLAlarm(string MotorName)
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "CWL", MotorName + "正极限报警", 0);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            RemoveHisItem();
            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "CWL", MotorName + "正极限报警", 0);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }
        public void RstMotorCWLAlarm(string MotorName)
        {
            listViewAlarmCur.Items.RemoveByKey(MotorName + "CWL");
        }
        public void SetMotorCCWLAlarm(string MotorName)
        {
            ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "CCWL", MotorName + "负极限报警", 0);
            listViewItem.SubItems.Add(DateTime.Now.ToString());

            RemoveHisItem();
            ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "CCWL", MotorName + "负极限报警", 0);
            listViewItemHis.SubItems.Add(DateTime.Now.ToString());
        }
        public void RstMotorCCWLAlarm(string MotorName)
        {
            listViewAlarmCur.Items.RemoveByKey(MotorName + "CCWL");
        }
        public void RstOtherAlarm()
        {
            for (int i = listViewAlarmCur.Items.Count - 1; i > -1; i--)
            {
                if (listViewAlarmCur.Items[i].ImageIndex != 0)
                {
                    listViewAlarmCur.Items.RemoveAt(i);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RstOtherAlarm();
        }
        public void RemoveHisItem()
        {
            if (listViewAlarmHis.Items.Count > 200)
            {
                for (int i = listViewAlarmHis.Items.Count - 1; i > 100; i--)
                {
                    listViewAlarmHis.Items.RemoveAt(i);
                }
            }
        }

        private void checkBoxAlramOff_Click(object sender, EventArgs e)
        {
            if (checkBoxAlramOff.Checked)
            {

            }
        }
    }
}
