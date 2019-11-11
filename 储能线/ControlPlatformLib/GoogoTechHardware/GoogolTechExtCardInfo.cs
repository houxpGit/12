using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    [Serializable]
    public class GoogolTechExtCardInfo: HardWareInfoBase
    {
        public int iCardNo = 0;
        public int iExtCardNo = 0;
        public string m_strConfigPath;
        public string m_strExtDllName;
        public GoogolTechExtCardInfo()
        {

        }
        override public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            GoogolTechExtCardInfoForm frm = new GoogolTechExtCardInfoForm(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();

        }
    }
}
