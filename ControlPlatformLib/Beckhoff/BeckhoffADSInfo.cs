using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlPlatformLib.Beckhoff
{
    [Serializable]
    public class BeckhoffADSInfo:HardWareInfoBase
    {
        public int iCardNo = 0;
        public string m_strConfigPath;
        public BeckhoffADSInfo()
        {

        }
        override public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            BeckhoffADSInfoForm frm = new BeckhoffADSInfoForm(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();

        }
    }
}
