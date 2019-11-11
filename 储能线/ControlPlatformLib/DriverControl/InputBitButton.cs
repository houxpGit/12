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

    public partial class InputBitButton : UserControl, IControlDriver
    {

        public object objlock = new object();
        public bool bCurrentStatus;
        private bool bPreStatus;
        private string m_strInputName;

        private Color colorOnForeColor;
        private Color colorOffForeColor;
        private Color colorOnBackColor;
        private Color colorOffBackColor;
        private String strOnText;
        private String strOffText;

        private bool bAlwaysFresh = false;


        public string InputName
        {
            get
            {
                return m_strInputName;
            }
            set
            {
                m_strInputName = value;
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
                    bCurrentStatus = IOManage.InputDrivers.drivers[m_strInputName].GetOn();
                    bRet = true;
                }
                catch
                {
                    bRet = false;
                }

            }
            return bRet;
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

                    }
                    else
                    {
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
                    bPreStatus = bCurrentStatus;
                };
            buttonBit.Invoke(atc);
        }

        public InputBitButton()
        {
            InitializeComponent();
            bCurrentStatus = false;
        }

        private void InputBitButton_Load(object sender, EventArgs e)
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
