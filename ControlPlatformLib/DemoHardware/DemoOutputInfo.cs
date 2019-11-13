using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ControlPlatformLib
{
    [Serializable]
    public class DemoOutputInfo : HardWareInfoBase
    {
        public DemoOutputInfo()
        {

        }
        override public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            DemoOutputInfoForm frm = new DemoOutputInfoForm(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();

        }

    }
}
