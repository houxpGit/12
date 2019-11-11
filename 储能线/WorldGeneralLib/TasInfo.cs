using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace WorldGeneralLib
{
    public class TasInfo
    {
        public int iTaskStep;
        public int iTaskAlarmStep;
        public string strTaskMes;
        public bool bTaskOnGoing;
        public bool bTaskAlarm;
        public bool bTaskFinish;
        public bool bStepOnGoing;
        public bool bNextStepAction;
        public HiPerfTimer htTimer;
        private TextBox m_txtBox;
        private string strPath = "";
        public string FilePath
        {
            get
            { 
                return strPath;
            }
            set
            {
                try
                {
                    if (!Directory.Exists(value))
                    {
                        Directory.CreateDirectory(value);
                    }
                    strPath = value;
                }
                catch
                {
                    strPath = "";
                }
                
            }
        }
        public TasInfo()
        {
            iTaskStep=0;
            iTaskAlarmStep = 0;
            strTaskMes="";
            bTaskOnGoing=false;
            bTaskAlarm=false;
            bTaskFinish=false;
            htTimer=new HiPerfTimer();
        }
        public TasInfo(TextBox txtBox)
        {
            iTaskStep = 0;
            iTaskAlarmStep = 0;
            strTaskMes = "";
            bTaskOnGoing = false;
            bTaskAlarm = false;
            bTaskFinish = false;
            htTimer = new HiPerfTimer();
            m_txtBox = txtBox;
        }
    
    }
}
