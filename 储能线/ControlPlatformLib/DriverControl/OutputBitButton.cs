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
    public enum ButtonType
    {
        PUSH = 0,
        REVERSE,
        ON,
        OFF
    }
    public enum ButtonStyle
    {
        LAMP = 0,
        SWITCH,
    }
    public partial class OutputBitButton : UserControl, IControlDriver
    {
        public delegate bool CheckCanAction(object sender, EventArgs e);
        public event CheckCanAction checkCanAction;
        public object objlock = new object();
        public bool bCurrentStatus;
        private bool bPreStatus;
        private string m_strOutputName;

        private Color colorOnForeColor;
        private Color colorOffForeColor;
        private Color colorOnBackColor;
        private Color colorOffBackColor;
        private String strOnText;
        private String strOffText;

        private bool bAlwaysFresh = false;

        private ButtonType buttonType;
        public ButtonType ButtonType
        {
            get
            {
                return buttonType;
            }
            set
            {
                buttonType = value;
            }
        }
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
        public string OutputName
        {
            get
            {
                return m_strOutputName;
            }
            set
            {
                m_strOutputName = value;
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
                if (checkCanAction != null)
                {
                    if (checkCanAction(sender, e) == false)
                    {
                        return;
                    }
                }
                if (buttonType == ButtonType.PUSH)
                {
                    SetDriverStatus(true);
                    FreshDriverStatus();
                }
                if (buttonType == ButtonType.ON)
                {
                    SetDriverStatus(true);
                    FreshDriverStatus();
                }
                if (buttonType == ButtonType.OFF)
                {
                    SetDriverStatus(false);
                    FreshDriverStatus();
                }
                if (buttonType == ButtonType.REVERSE)
                {
                    if (bCurrentStatus)
                    {
                        SetDriverStatus(false);
                        FreshDriverStatus();
                    }
                    else
                    {
                        SetDriverStatus(true);
                        FreshDriverStatus();
                    }
                }
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
                    bCurrentStatus = IOManage.OutputDrivers.drivers[m_strOutputName].GetOn();
                    bRet = true;
                }
                catch
                {
                    bRet = false;
                }

            }
            return bRet;
        }
        public void SetDriverStatus(bool bOn)
        {
            bool bRet = false;
            lock (objlock)
            {
                try
                {
                    bRet = IOManage.OutputDrivers.drivers[m_strOutputName].SetOutBit(bOn);
                }
                catch
                {
                    bRet = false;
                }

            }
            bCurrentStatus = bOn;
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
            lock (objlock)
            {
                if (buttonStyle == ButtonStyle.LAMP)
                {
                    return;
                }
                if (buttonType == ButtonType.PUSH)
                {
                    SetDriverStatus(false);
                    FreshDriverStatus();
                }
            }
        }

        public OutputBitButton()
        {
            InitializeComponent();
            bCurrentStatus = false;
        }



        private void OutputBitButton_Load(object sender, EventArgs e)
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
