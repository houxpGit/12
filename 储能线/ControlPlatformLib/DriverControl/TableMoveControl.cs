using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class TableMoveControl : UserControl, IControlDriver
    {
        public delegate bool CheckCanAction(object sender, EventArgs e);
        public event CheckCanAction buttonPushed;
        public object objlock = new object();
        public bool bCurrentStatus;
        private bool bPreStatus;
        private string m_strTableName;
        private string m_strPositionName;

        private Color colorOnForeColor;
        private Color colorOffForeColor;
        private Color colorOnBackColor;
        private Color colorOffBackColor;
        private String strOnText;
        private String strOffText;

        private bool bAlwaysFresh = false;
        private ButtonStyle buttonStyle;
        public ButtonStyle ButtonStyle
        {
            get
            {
                return buttonStyle;
            }
            set
            {
                buttonStyle = value;
            }
        }
        public string TableName
        {
            get
            {
                return m_strTableName;
            }
            set
            {
                m_strTableName = value;
            }
        }
        public string PositionName
        {
            get
            {
                return m_strPositionName;
            }
            set
            {
                m_strPositionName = value;
            }
        }
        public Color ColorOnForeColor
        {
            get
            {
                return colorOnForeColor;
            }
            set
            {
                colorOnForeColor = value;
                buttonBit.ForeColor = value;
            }
        }
        public Color ColorOffForeColor
        {
            get
            {
                return colorOffForeColor;
            }
            set
            {
                colorOffForeColor = value;
                buttonBit.ForeColor = value;
            }
        }
        public Color ColorOnBackColor
        {
            get
            {
                return colorOnBackColor;
            }
            set
            {
                colorOnBackColor = value;
                buttonBit.BackColor = value;
            }
        }
        public Color ColorOffBackColor
        {
            get
            {
                return colorOffBackColor;
            }
            set
            {
                colorOffBackColor = value;
                buttonBit.BackColor = value;
            }
        }
        public String StrOnText
        {
            get
            {
                return strOnText;
            }
            set
            {
                strOnText = value;
                buttonBit.Text = value;
            }
        }
        public String StrOffText
        {
            get
            {
                return strOffText;
            }
            set
            {
                strOffText = value;
                buttonBit.Text = value;
            }
        }
        public bool AlwayFresh
        {
            get
            {
                return bAlwaysFresh;
            }
            set
            {
                bAlwaysFresh = value;
            }
        }

        private void buttonBit_MouseDown(object sender, MouseEventArgs e)
        {
            lock (objlock)
            {
                if (buttonStyle == ButtonStyle.LAMP)
                {
                    return;
                }
                if (buttonPushed != null)
                {
                    if (buttonPushed(sender, e) == false)
                    {
                        return;
                    }
                }
                TableMove();
            }
        }
        public bool GetDriverStatus()
        {
            bool bRet = false;
            if (Visible == false)
            {
                return bRet;
            }
            lock (objlock)
            {
                try
                {
                    bCurrentStatus = TableManage.tableDrivers.drivers[m_strTableName].IsOnPos(m_strPositionName);
                    //tab
                    bRet = true;
                }
                catch
                {
                    bRet = false;
                }

            }
            return bRet;
        }
        public void TableMove()
        {

            try
            {
                TableManage.tableDrivers.drivers[m_strTableName].StartPosMove(m_strPositionName);
            }
            catch
            {

            }


        }
        public void FreshDriverStatus()
        {
            if (Visible == false)
            {
                return;
            }
            Action atc = () =>
                {
                    if (bPreStatus == bCurrentStatus)
                    {
                        return;
                    }
                    if (bCurrentStatus)
                    {
                        buttonBit.Text = strOnText;
                        buttonBit.ForeColor = colorOnForeColor;
                        buttonBit.BackColor = colorOnBackColor;
                    }
                    else
                    {
                        buttonBit.Text = strOffText;
                        buttonBit.ForeColor = colorOffForeColor;
                        buttonBit.BackColor = colorOffBackColor;
                    }
                    bPreStatus = bCurrentStatus;
                };
            buttonBit.Invoke(atc);
        }
        private void buttonBit_MouseUp(object sender, MouseEventArgs e)
        {
            //lock (objlock)
            //{
            //    if (buttonStyle == ButtonStyle.LAMP)
            //    {
            //        return;
            //    }
            //    if (buttonType == ButtonType.PUSH)
            //    {
            //        SetDriverStatus(false);
            //        FreshDriverStatus();
            //    }
            //}
        }

        public TableMoveControl()
        {
            InitializeComponent();
            bCurrentStatus = false;
        }

        private void TableMoveControl_Load(object sender, EventArgs e)
        {
            try
            {
                DriverControlManage.controls.Add(this);
            }
            catch
            {

            }
            buttonBit.Font = Font;
            if (bCurrentStatus)
            {
                buttonBit.Text = strOnText;
                buttonBit.ForeColor = colorOnForeColor;
                buttonBit.BackColor = colorOnBackColor;
            }
            else
            {
                buttonBit.Text = strOffText;
                buttonBit.ForeColor = colorOffForeColor;
                buttonBit.BackColor = colorOffBackColor;
            }
        }
    }
}
