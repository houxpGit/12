using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class TaskUnit
    {
        public TaskGroup m_taskGroup;
        public string strName;
        public WorldGeneralLib.TasInfo taskInfo;
        public WorldGeneralLib.HiPerfTimer m_taskTime;
        public bool m_manualStart = false;
        public TaskUnit(string name, TaskGroup taskGroup)
        {
            strName = name;
            m_taskGroup = taskGroup;
            taskInfo = new WorldGeneralLib.TasInfo();
            m_taskTime = new WorldGeneralLib.HiPerfTimer();
        }

        public TaskUnit()
        {
        }

        virtual public void Process()
        {
            bool bAutoTrag = false;
            bool bManualTrag = false;
            bool bTragCondition = false;
            bTragCondition = true;
            if (taskInfo.bTaskAlarm)
            {
                if (MainModule.FormMain.bResetPress)
                {
                    taskInfo.bTaskAlarm = false;
                    m_taskTime.Start();
                }
                return;
            }
            bAutoTrag = MainModule.FormMain.bAuto && (!taskInfo.bTaskFinish) && (!taskInfo.bTaskOnGoing);
            bManualTrag = m_manualStart;
            switch (taskInfo.iTaskStep)
            {
                case 0://判断是否有触发 
                    if ((bAutoTrag | bManualTrag) && bTragCondition)
                    {
                        m_manualStart = false;
                        m_taskTime.Start();
                        taskInfo.bTaskOnGoing = true;
                        m_taskGroup.AddRunMessage("任务开始了");
                        taskInfo.iTaskStep = 10;
                    }
                    break;
                case 10:
                    {
                        if (m_taskTime.TimeUp(1))
                        {
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 20;
                            m_taskGroup.AddRunMessage("任务10了");
                        }
                    }
                    break;
                case 20:
                    {
                        if (m_taskTime.TimeUp(1))
                        {
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 30;
                            m_taskGroup.AddRunMessage("任务20了");
                        }
                    }
                    break;
                case 30:
                    {
                        if (m_taskTime.TimeUp(1))
                        {
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 40;
                            m_taskGroup.AddRunMessage("任务30了");
                        }
                    }
                    break;
                case 40:
                    {
                        if (m_taskTime.TimeUp(1))
                        {
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 50;
                            taskInfo.bTaskOnGoing = false;
                            m_taskGroup.AddRunMessage("任务40了");
                        }
                    }
                    break;
                case 50:
                    {
                        if (m_taskTime.TimeUp(1))
                        {
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 0;
                            m_taskGroup.AddRunMessage("任务50了");
                            taskInfo.bTaskOnGoing = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
