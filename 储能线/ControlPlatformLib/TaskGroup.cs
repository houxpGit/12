using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WorldGeneralLib.DataLogicBarCode;
using System.Net;

namespace ControlPlatformLib
{
    public class TaskGroup
    {
        private TextBox textBoxMessage;
        private ListView listViewMonitor;
        string strPath = "D:\\LogFile\\";
        public WorldGeneralLib.TasInfo m_tskFresh;
        public List<TaskUnit> listTask;
        public List<bool> bPreOnGoingList;
        private int m_iPeriod;
        private object lockObj = new object();
        public TaskGroup(TextBox textBox, ListView listView)
        {
            //lockObj = new object();
            m_iPeriod = 10;
            m_tskFresh = new WorldGeneralLib.TasInfo();
            textBoxMessage = textBox;
            listViewMonitor = listView;
            strPath = "D:\\LogFile\\";
            listTask = new List<TaskUnit>();
            bPreOnGoingList = new List<bool>();
        }

        public TaskGroup(TextBox textBox)
        {
            //lockObj = new object();
            m_iPeriod = 10;
            m_tskFresh = new WorldGeneralLib.TasInfo();
            textBoxMessage = textBox;
            strPath = "D:\\LogFile\\";
            listTask = new List<TaskUnit>();
            bPreOnGoingList = new List<bool>();
        }

        public TaskGroup()
        {
            m_iPeriod = 10;
            m_tskFresh = new WorldGeneralLib.TasInfo();
            strPath = "D:\\LogFile\\";
            listTask = new List<TaskUnit>();
            bPreOnGoingList = new List<bool>();
        }
        public void AddTaskUnit(TaskUnit task)
        {
            listTask.Add(task);
            bPreOnGoingList.Add(false);
            if (listViewMonitor != null)
            {
                ListViewItem lvi = listViewMonitor.Items.Add(task.strName);
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
            }
        }
        public void StartThread()
        {
            Thread SupplyThread = new Thread(threadFunction);
            SupplyThread.Priority = ThreadPriority.Highest;
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThreadAlwayScan()
        {
            Thread SupplyThread = new Thread(threadFunctionAlwayScan);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThread(int period)
        {
            m_iPeriod = period;
            Thread SupplyThread = new Thread(threadFunction);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThreadAlwayScan(int period)
        {
            m_iPeriod = period;
            Thread SupplyThread = new Thread(threadFunctionAlwayScan);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        private void  threadFunction()
        {
            Thread.Sleep(1000);
            //BarcodeDataManage.scannerDic["扫码枪"].RecieveMessageAction += RecieveMessage;
            try
            {
                while (true)
                {
                    Thread.Sleep(1);
                    //if (BarcodeDataManage.scannerDic["扫码枪"].m_BarcodeQueue.Count>0)
                    //{
                    //    MainModule.FormMain.m_strSN = BarcodeDataManage.scannerDic["扫码枪"].m_BarcodeQueue.FirstOrDefault();
                    //    MainModule.FormMain.SetSN();
                    //    MainModule.FormMain.bSNOK = true;
                    //    //Program.b_SNOk = true;
                    //}
                    //else
                    //{
                    //    MainModule.FormMain.m_strSN = "请扫码！";
                    //    MainModule.FormMain.SetSN();
                    //}
                    if (MainModule.FormMain.bEstop || (!MainModule.FormMain.bHomeReady))
                    {
                        continue;
                    }
                    foreach (TaskUnit taskItem in listTask)
                    {
                        taskItem.Process();
                    }

                    if (listViewMonitor != null)
                    {
                        if (listViewMonitor.Visible)
                            FreshTaskStatus();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"异常",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void RecieveMessage()
        {
            IOManage.OUTPUT("扫码完成信号").SetOutBit(true);
        }

        private void threadFunctionAlwayScan()
        {
            Thread.Sleep(1000);

            while (true)
            {
                System.Threading.Thread.Sleep(m_iPeriod);
                foreach (TaskUnit taskItem in listTask)
                {
                    taskItem.Process();
                }
                if (listViewMonitor != null)
                {
                    if (listViewMonitor.Visible)
                        FreshTaskStatus();
                }
            }

        }
        public void FreshTaskStatus()
        {
            switch (m_tskFresh.iTaskStep)
            {
                case 0:
                    {
                        m_tskFresh.htTimer.Start();
                        m_tskFresh.iTaskStep = 10;
                    }
                    break;
                case 10:
                    if (m_tskFresh.htTimer.TimeUp(0.1))
                    {
                        try
                        {
                            if (listViewMonitor != null)
                            {
                                Action action = () =>
                                    {
                                        listViewMonitor.BeginUpdate();
                                        for (int i = 0; i < listTask.Count; i++)
                                        {
                                            #region TaskRefresh
                                            if (listTask[i].taskInfo.bTaskOnGoing)
                                            {
                                                listViewMonitor.Items[i].SubItems[1].BackColor = Color.Green;
                                                listViewMonitor.Items[i].SubItems[1].Text = listTask[i].taskInfo.iTaskStep.ToString();
                                                listViewMonitor.Items[i].SubItems[4].Text = listTask[i].taskInfo.htTimer.Duration.ToString("0.0");
                                            }
                                            else
                                            {
                                                listViewMonitor.Items[i].SubItems[1].BackColor = Color.LightSalmon;
                                                listViewMonitor.Items[i].SubItems[1].Text = listTask[i].taskInfo.iTaskStep.ToString();
                                            }
                                            if (listTask[i].taskInfo.bTaskFinish)
                                            {
                                                listViewMonitor.Items[i].SubItems[2].BackColor = Color.Green;
                                            }
                                            else
                                            {
                                                listViewMonitor.Items[i].SubItems[2].BackColor = Color.LightSalmon;
                                            }
                                            if (listTask[i].taskInfo.bTaskAlarm)
                                            {
                                                listViewMonitor.Items[i].SubItems[3].BackColor = Color.Yellow;
                                            }
                                            else
                                            {
                                                listViewMonitor.Items[i].SubItems[3].BackColor = Color.LightSalmon;
                                            }

                                            if (bPreOnGoingList[i] != listTask[i].taskInfo.bTaskOnGoing)
                                            {
                                                if (listTask[i].taskInfo.bTaskOnGoing)
                                                {
                                                    listTask[i].taskInfo.htTimer.Start();
                                                }
                                            }
                                            bPreOnGoingList[i] = listTask[i].taskInfo.bTaskOnGoing;
                                            #endregion
                                        }

                                        listViewMonitor.EndUpdate();
                                    };
                                listViewMonitor.Invoke(action);

                            }
                        }
                        catch
                        {
                            //MessageBox.Show(ex.Message);
                        }
                        m_tskFresh.iTaskStep = 20;
                    }
                    break;
                case 20:
                    if (true)
                    {
                        m_tskFresh.iTaskStep = 0;
                    }
                    break;
                default:
                    break;
            }
        }
        public void AddRunMessage(string strMessage)
        {
            Global.logger.Info(strMessage);
            WriteMessageText(strMessage);
            //WriteLogFile(strMessage);
        }
        public void AddAlarmMessage(string strMessage)
        {
            Global.logger.Error(strMessage);
           
            //MainModule.FormMain.m_formAlarm.InsertAlarmMessage(strMessage);
            WriteMessageText(strMessage);
            //ControlPlatformLib.MainModule.FormMain.bAuto = false;
            ControlPlatformLib.MainModule.FormMain.m_formAlarm.InsertAlarmMessage(strMessage);
            try
            {
                if (!ControlPlatformLib.Global.bIgnoreKPIMes)
                {
                    //设备型号，项目名称，部门，设备位置，工位，上位机IP，报警项编号，报警信息，值（1：代表报警开始，0：代表报警结束），报警开始或结束时间
                    ThreadPool.QueueUserWorkItem((p)=> {
                        try
                        {
                            Global.m_KPIMES?.UploadEquipmentAlarm(Environment.MachineName + ",激光清洗机,EVB,"+ ControlPlatformLib.Global.sMachinePos + "," + Global.sMachineNO + "," + Global.localaddr.MapToIPv4().ToString() + ",0," + strMessage + ",1," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch (Exception ex)
                        {
                            Global.logger.Error("上传KPI出现错误:" + ex.Message);
                        }
                    });
                    // 设备型号，项目名称，部门，设备位置，工位，上位机IP，设备状态编号（1：运行；2：故障；3：待机；4：关机；5:其他），设备状态信息，状态开始或结束时间
                    ThreadPool.QueueUserWorkItem((p)=> {
                        try
                        {
                            Global.m_KPIMES?.UploadEquipmentState(Environment.MachineName + ",激光清洗机,EVB,"+ ControlPlatformLib.Global.sMachinePos + "," + Global.sMachineNO + "," + Global.localaddr.MapToIPv4().ToString() + ",2,报警," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch (Exception ex)
                        {
                            Global.logger.Error("上传KPI出现错误:" + ex.Message);
                        }
                    });
                    Global.logger.Error("更新错误信息到KPIMES："+strMessage);
                }
            }
            catch (Exception ex)
            {
                Global.logger.Error("上传KPI出现错误:" + ex.Message);
            }

            //WriteLogFile(strMessage);
        }
        private void WriteMessageText(string strMessage)
        {
            if (textBoxMessage == null)
                return;
            Action action = () =>
            {
                try
                {
                    int iTotal = 0;
                    int iLenght = textBoxMessage.Lines.Length;
                    textBoxMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + ":  " + strMessage + "\r\n");
                    if (textBoxMessage.Lines.Length > 200)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            iTotal = iTotal + textBoxMessage.Lines[i].Length + 2;
                        }
                        textBoxMessage.Text = textBoxMessage.Text.Substring(iTotal);
                    }
                }
                catch
                {

                }
            };
            try
            {
                textBoxMessage.Invoke(action);
            }
            catch
            {

            }
        }
        private void WriteLogFile(string strMessage)
        {
            //if (strPath == "")
            //    return;
            //WorldGeneralLib.TextLogWrite.AppendLog(strMessage, strPath);
        }
    }
}
