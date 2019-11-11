using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldGeneralLib
{
    public class ListViewNF:ListView
    {
        public ListViewNF()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
