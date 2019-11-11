using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlPlatformLib.ADLINK;

namespace ControlPlatformLib
{
    [Serializable]
    public class ADLINKTechMCInfo : HardWareInfoBase
    {
        public int iCardNo = 0;
        public string m_strConfigPath;
        public ADLINKTechMCInfo()
        {

        }
        override public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            ADLINKTechMCInfoForm frm = new ADLINKTechMCInfoForm(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();

        }
    }
}
