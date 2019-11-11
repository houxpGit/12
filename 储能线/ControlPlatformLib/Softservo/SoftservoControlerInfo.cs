using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ControlPlatformLib.Softservo
{
    [XmlInclude(typeof(SoftservoControlerInfo))]
    public class SoftservoControlerInfo: HardWareInfoBase
    {
        public int iCardNo = 0;
        public string m_strConfigPath;
        public SoftservoControlerInfo()
        {

        }

        public override void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            SoftservoControlerForm frm = new SoftservoControlerForm(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();
        }
    }
}
