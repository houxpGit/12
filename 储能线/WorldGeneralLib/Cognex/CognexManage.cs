using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Cognex
{
    public static class CognexManage
    {
        public static CognexDoc m_cognexDoc = new CognexDoc();
        public static CognexClient m_cognexclient = new CognexClient();
        public static CognexForm m_cognexform;

        public static void LoadData()
        {
            m_cognexDoc = CognexDoc.LoadDocument();
            m_cognexclient = new CognexClient();
        }
    }
}
