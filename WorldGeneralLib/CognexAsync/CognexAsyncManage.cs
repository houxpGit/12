using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.CognexAsync
{
    public static class CognexAsyncManage
    {
        public static CognexAsyncClientDoc m_cognexAsyDoc = new CognexAsyncClientDoc();
        public static CognexAsyncForm m_cognexasyform;

        public static void LoadData()
        {
            m_cognexAsyDoc = CognexAsyncClientDoc.LoadDocument();
        }

        //left channal nozzle1 x current position
        public static double m_dxleftn1curPos = 0.0;
        //left channal nozzle1 y current position
        public static double m_dyleftn1curPos = 0.0;
        //left channal nozzle1 z current position
        public static double m_dzleftn1curPos = 0.0;
        //left channal nozzle2 x current position
        public static double m_dxleftn2curPos = 0.0;
        //left channel nozzle2 y current position 
        public static double m_dyleftn2curPos = 0.0;
        //left channal nozzle2 z current position
        public static double m_dzleftn2curPos = 0.0;

        //right channal nozzle1 x current position
        public static double m_dxrightn1curPos = 0.0;
        //right channal nozzle1 y current position
        public static double m_dyrightn1curPos = 0.0;
        //right channal nozzle1 z current position
        public static double m_dzrightn1curPos = 0.0;
        //right channal nozzle2 x current position
        public static double m_dxrightn2curPos = 0.0;
        //right channel nozzle2 y current position 
        public static double m_dyrightn2curPos = 0.0;
        //right channal nozzle2 z current position
        public static double m_dzrightn2curPos = 0.0;

        public static double m_dp300dlvalmax = 1000;
        public static double m_dp300dlvalmin = 0.0;
        public static double m_dp300urvalmax = 1000;
        public static double m_dp300urvalmin = 0.0;

        public static double m_dp400dlvalmax = 1000;
        public static double m_dp400dlvalmin = 0.0;
        public static double m_dp400urvalmax = 1000;
        public static double m_dp400urvalmin = 0.0;

        public static string leftsn = string.Empty;
        public static string rightsn = string.Empty;
        public static int colornumber = 1;
    }
}
