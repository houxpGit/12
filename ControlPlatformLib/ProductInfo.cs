using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.ComponentModel;
using System.Data;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ControlPlatformLib
{
    [Serializable]
    public class ProductInfo
    {
        public int m_iInput = 0;
        public int m_iPass = 0;
        public int m_iFail = 0;
        public double m_dYeild = 0.0;
        public double m_dRetry = 0.0;
        public double m_CT = 0.0;
        public double m_dUPH = 0.0;
        public bool bWorking = false;
        public string strProjectName = "";
        [NonSerialized]
        public bool bStartShowCycle = false;
        public TimeSpan m_timeTTime = new TimeSpan();
        public TimeSpan m_TimeWork = new TimeSpan();
        public TimeSpan m_timeWeight = new TimeSpan();
        public TimeSpan m_TimePreHeat = new TimeSpan();
        [NonSerialized]
        public WorldGeneralLib.HiPerfTimer TimerCT = new WorldGeneralLib.HiPerfTimer();
        [NonSerialized]
        public Timer TimerTimeUpdate;
        [NonSerialized]
        public DateTime m_preTTime;
        [NonSerialized]
        public DateTime m_preWTime;
        [NonSerialized]
        public DateTime m_preWeightTime;
        [NonSerialized]
        public DateTime m_preHeatTime;
        [NonSerialized]
        public ListView listViewShow;
        [NonSerialized]
        public FormProductInfo frmInfo;
        public ProductInfo()
        {
        }

        void listViewShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listView = (ListView)sender;
            int i = 0;
            try
            {
                Point pt = listView.Parent.PointToClient(e.Location);
                ListViewItem lvi = listView.GetItemAt(e.X, e.Y);
                i = lvi.Index;
            }
            catch
            {
                return;
            }
            if (i == 0)
            {
                m_iInput = 0;
            }
            if (i == 1)
            {
                m_iPass = 0;
            }
            if (i == 2)
            {
                m_iFail = 0;
            }
            if (i == 6)
            {
                m_dUPH = 0.0;
            }
            if (i == 7)
            {
                m_timeTTime = DateTime.Now - DateTime.Now;
            }
            if (i == 8)
            {
                m_TimeWork = DateTime.Now - DateTime.Now;
            }
            if (i == 9)
            {
                RstWeightTime();
            }
            if (i == 10)
            {
                RstPreHTime();
            }
        }
        public void RstWeightTime()
        {
            m_preWeightTime = DateTime.Now;
            m_timeWeight = TimeSpan.Parse("01:00:00");
        }
        public void RstPreHTime()
        {
            m_preHeatTime = DateTime.Now;
            m_TimePreHeat = TimeSpan.Parse("05:00:00");
        }
        public void startUpdateTime(Panel panel)
        {
            frmInfo = new FormProductInfo();
            frmInfo.TopLevel = false;
            panel.Controls.Add(frmInfo);
            frmInfo.Size = panel.Size;
            frmInfo.Show();

            m_preTTime = DateTime.Now;
            m_preWTime = DateTime.Now;
            m_preWeightTime = DateTime.Now;
            m_preHeatTime = DateTime.Now;
            listViewShow = frmInfo.listViewProductInfo;
            TimerTimeUpdate = new Timer();
            TimerTimeUpdate.Tick += TimerTimeUpdate_Tick;
            TimerTimeUpdate.Interval = 1000;
            TimerTimeUpdate.Enabled = true;
            listViewShow.MouseDoubleClick += listViewShow_MouseDoubleClick;
        }
        private void TimerTimeUpdate_Tick(object sender, EventArgs e)
        {
            m_timeTTime = m_timeTTime + (DateTime.Now - m_preTTime);
            m_preTTime = DateTime.Now;
            if (bWorking)
            {
                m_TimeWork = m_TimeWork + (DateTime.Now - m_preWTime);
            }
            m_preWTime = DateTime.Now;

            m_timeWeight = m_timeWeight - (DateTime.Now - m_preWeightTime);
            m_preWeightTime = DateTime.Now;

            m_TimePreHeat = m_TimePreHeat - (DateTime.Now - m_preHeatTime);
            m_preHeatTime = DateTime.Now;

            listViewShow.BeginUpdate();
            listViewShow.Items[0].SubItems[1].Text = m_iInput.ToString();
            listViewShow.Items[1].SubItems[1].Text = m_iPass.ToString();
            listViewShow.Items[2].SubItems[1].Text = m_iFail.ToString();
            if (m_iInput != 0)
            {
                listViewShow.Items[3].SubItems[1].Text = (((double)m_iPass / (double)m_iInput) * 100.0).ToString("0.00");
            }
            else
            {
                listViewShow.Items[3].SubItems[1].Text = "0.0";
            }
            if (m_iInput != 0)
            {
                listViewShow.Items[4].SubItems[1].Text = (((double)m_iFail / (double)m_iInput) * 100.0).ToString("0.00");
            }
            else
            {
                listViewShow.Items[4].SubItems[1].Text = "0.0";
            }

            if (bStartShowCycle == true)
            {
                listViewShow.Items[5].SubItems[1].Text = TimerCT.Duration.ToString("0.000");
            }
            if (m_TimeWork.TotalHours != 0.0)
                m_dUPH = (double)m_iInput / m_TimeWork.TotalHours;
            else
            {
                m_dUPH = 0.0;
            }
            listViewShow.Items[6].SubItems[1].Text = m_dUPH.ToString("0.0");
            listViewShow.Items[7].SubItems[1].Text = m_timeTTime.TotalHours.ToString("0000") + ":" + m_timeTTime.Minutes.ToString("00") + ":" + m_timeTTime.Seconds.ToString("00");
            listViewShow.Items[8].SubItems[1].Text = m_TimeWork.TotalHours.ToString("0000") + ":" + m_TimeWork.Minutes.ToString("00") + ":" + m_TimeWork.Seconds.ToString("00");
            //if (m_timeWeight.TotalSeconds > 0)
            //{
            //    listViewShow.Items[9].SubItems[1].Text = Math.Abs(m_timeWeight.Hours).ToString("0000") + ":" + Math.Abs(m_timeWeight.Minutes).ToString("00") + ":" + Math.Abs(m_timeWeight.Seconds).ToString("00");
            //}
            //else
            //{
            //    listViewShow.Items[9].SubItems[1].Text = "-" + Math.Abs(m_timeWeight.Hours).ToString("0000") + ":" + Math.Abs(m_timeWeight.Minutes).ToString("00") + ":" + Math.Abs(m_timeWeight.Seconds).ToString("00");
            //}
            //if (m_TimePreHeat.TotalSeconds > 0)
            //{
            //    listViewShow.Items[10].SubItems[1].Text = Math.Abs(m_TimePreHeat.Hours).ToString("0000") + ":" + Math.Abs(m_TimePreHeat.Minutes).ToString("00") + ":" + Math.Abs(m_TimePreHeat.Seconds).ToString("00");
            //}
            //else
            //{
            //    listViewShow.Items[10].SubItems[1].Text = "-" + Math.Abs(m_TimePreHeat.Hours).ToString("0000") + ":" + Math.Abs(m_TimePreHeat.Minutes).ToString("00") + ":" + Math.Abs(m_TimePreHeat.Seconds).ToString("00");
            //}
            listViewShow.EndUpdate();
            //Program.FormMain.
        }
        public static ProductInfo LoadObj()
        {
            ProductInfo proInfo;
            BinaryFormatter fmt = new BinaryFormatter();
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/ProductInfo.dat");
                proInfo = (ProductInfo)fmt.Deserialize(fsReader);
                fsReader.Close();
                if (proInfo.m_timeTTime == null)
                {
                    proInfo.m_timeTTime = new TimeSpan();
                }
                if (proInfo.m_TimeWork == null)
                {
                    proInfo.m_TimeWork = new TimeSpan();
                }
                if (proInfo.TimerCT == null)
                {
                    proInfo.TimerCT = new WorldGeneralLib.HiPerfTimer();
                }
            }
            catch //(Exception eMy)
            {
                //MessageBox.Show(eMy.ToString());
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                proInfo = new ProductInfo();
            }
            return proInfo;
        }
        public bool SaveDocAction()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/ProductInfo.dat", FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        public void SetCTStart()
        {
            if (bStartShowCycle == false)
            {
                TimerCT.Start();
                bStartShowCycle = true;
                listViewShow.Items[5].SubItems[2].Text = TimerCT.Duration.ToString("0.000");
            }
            listViewShow.Items[5].SubItems[2].Text = TimerCT.Duration.ToString("0.000");
            bStartShowCycle = true;
            TimerCT.Start();
        }
        public void SetCTStop()
        {
            bStartShowCycle = false;
        }
    }
}
