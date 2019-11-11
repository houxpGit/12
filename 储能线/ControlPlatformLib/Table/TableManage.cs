using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    /// <summary>
    /// 感应器逻辑
    /// </summary>
    public enum SenserLogic
    {
        /// <summary>
        /// 常闭
        /// </summary>
        NC,
        /// <summary>
        /// 常开
        /// </summary>
        NO,
        /// <summary>
        /// 不使用
        /// </summary>
        DISABLE
    }
    /// <summary>
    /// 脉冲逻辑
    /// </summary>
    public enum PulseMode
    {
        PLDI,
        CWCCW
    }
    ///<summary>
    ///名称：平台操作类
    ///作用：执行平台操作
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    /// 描述：所有对于轴的操作都通过这个类完成。
    ///</summary>
    static public class TableManage
    {
        static public TablesDoc tablesDoc;
        static public FormTableSetting frmTableSetting;
        static public TableDrivers tableDrivers;
        static public bool bPreResetPress = false;

        public delegate void HandleraAlarm(string alarm);
        static public event HandleraAlarm HandleraAlarmEvent;

        public delegate void HandlerMotorAlarm(string alarm,int type);
        static public event HandlerMotorAlarm HandlerMotorAlarmEvent;
        public delegate void HandlerResetMotorAlarm(string alarm, int type);
        static public event HandlerResetMotorAlarm HandlerResetMotorAlarmEvent;
        /// <summary>
        /// 返回指定平台实例
        /// </summary>
        /// <param name="strName">指定平台名称</param>
        /// <returns></returns>
        static public TableDriver TableDriver(string strName)
        {
            try
            {
                return tableDrivers.drivers[strName];
            }
            catch
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在名字为:" + strName + "的平台");
                TableDriver table = new TableDriver();
                tableDrivers.drivers.Add(strName, table);
                return tableDrivers.drivers[strName];
            }
        }
        /// <summary>
        /// 返回指定位置实例
        /// </summary>
        /// <param name="strTableName">指定平台名称</param>
        /// <param name="strPosName">指定位置名称</param>
        /// <returns></returns>
        static public TablePosItem TablePosItem(string strTableName, string strPosName)
        {
            try
            {
                return tablesDoc.m_tableDictionary[strTableName].tablePosData.tablePosItemDictionary[strPosName];

            }
            catch
            {
                
                if (tablesDoc.m_tableDictionary.Keys.Contains(strTableName) == false)
                {
                    //MainModule.FormMain.m_formAlarm.Show();
                    //HandleraAlarmEvent.Invoke("不存在名字为:" + strTableName + "的平台数据");
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在名字为:" + strTableName + "的平台数据");
                    TableData table = new TableData();
                    tablesDoc.m_tableDictionary.Add(strTableName, table);
                }
                if (tablesDoc.m_tableDictionary[strTableName].tablePosData.tablePosItemDictionary.ContainsKey(strPosName) == false)
                {
                    //MainModule.FormMain.m_formAlarm.Show();
                    //HandleraAlarmEvent("平台：" + strTableName + "不存在名字为:" + strTableName + "的点位置数据");
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("平台：" + strTableName + "不存在名字为:" + strPosName + "的点位置数据");
                    TablePosItem item = new TablePosItem();
                    tablesDoc.m_tableDictionary[strTableName].tablePosData.tablePosItemDictionary.Add(strPosName, item);

                }
                return tablesDoc.m_tableDictionary[strTableName].tablePosData.tablePosItemDictionary[strPosName];
            }
        }
        /// <summary>
        /// 载入参数
        /// </summary>
        static public void LoadData()
        {
            tablesDoc = TablesDoc.LoadObj();
        }
        /// <summary>
        /// 初始化所有平台
        /// </summary>
        static public void InitTables()
        {
            tableDrivers = new TableDrivers();

            foreach (KeyValuePair<string, TableData> item in tablesDoc.m_tableDictionary)
            {
                TableDriver taDriver = new TableDriver();
                tableDrivers.drivers.Add(item.Value.strTableName, taDriver);
            }
            foreach (KeyValuePair<string, TableDriver> item in tableDrivers.drivers)
            {
                item.Value.Init(tablesDoc.m_tableDictionary[item.Key]);
            }
            System.Threading.Thread thread = new System.Threading.Thread(ThreadScanTables);
            thread.IsBackground = true;
            thread.Start();

        }
        static public void ThreadScanTables()
        {
            double dPosX = 0, dPosY = 0, dPosZ = 0, dPosU = 0;
            bool bAlarmX = false, bAlarmY = false, bAlarmZ = false, bAlarmU = false;
            bool bCWLX = false, bCWLY = false, bCWLZ = false, bCWLU = false;
            bool bOrgX = false, bOrgY = false, bOrgZ = false, bOrgU = false;
            bool bCCWLX = false, bCCWLY = false, bCCWLZ = false, bCCWLU = false;
            bool bMovX = false, bMovY = false, bMovZ = false, bMovU = false;
            Thread.Sleep(5000);
            while (true)
            {
                Thread.Sleep(1);
                try
                {
                    foreach (KeyValuePair<string, TableDriver> item in tableDrivers.drivers)
                    {

                        item.Value.GetTableStatus(ref dPosX, ref bAlarmX, ref bCWLX, ref bOrgX, ref bCCWLX, ref bMovX,
                                            ref dPosY, ref bAlarmY, ref bCWLY, ref bOrgY, ref bCCWLY, ref bMovY,
                                            ref dPosZ, ref bAlarmZ, ref bCWLZ, ref bOrgZ, ref bCCWLZ, ref bMovZ,
                                            ref dPosU, ref bAlarmU, ref bCWLU, ref bOrgU, ref bCCWLU, ref bMovU);
                        if (bAlarmX)
                        {
                            if (bAlarmX != item.Value.tablePreStatus.bAlarmX)
                            {
                                //HandlerMotorAlarmEvent(item.Key + "X轴",0);
                                MainModule.FormMain.m_formAlarm.SetMotorAlarm(item.Key + "X轴");
                                item.Value.tablePreStatus.bAlarmX = bAlarmX;
                            }
                        }
                        else
                        {
                            if (bAlarmX != item.Value.tablePreStatus.bAlarmX)
                            {
                                MainModule.FormMain.m_formAlarm.RstMotorAlarm(item.Key + "X轴");
                                item.Value.tablePreStatus.bAlarmX = bAlarmX;
                            }
                        }
                        if (bAlarmY)
                        {
                            if (bAlarmY != item.Value.tablePreStatus.bAlarmY)
                            {
                                //HandlerMotorAlarmEvent(item.Key + "Y轴", 0);
                                MainModule.FormMain.m_formAlarm.SetMotorAlarm(item.Key + "Y轴");
                                item.Value.tablePreStatus.bAlarmY = bAlarmY;
                            }
                        }
                        else
                        {
                            if (bAlarmY != item.Value.tablePreStatus.bAlarmY)
                            {
                                MainModule.FormMain.m_formAlarm.RstMotorAlarm(item.Key + "Y轴");
                                item.Value.tablePreStatus.bAlarmU = bAlarmY;
                            }
                        }
                        if (bAlarmZ)
                        {
                            if (bAlarmZ != item.Value.tablePreStatus.bAlarmZ)
                            {
                                //HandlerMotorAlarmEvent(item.Key + "Z轴", 0);
                                MainModule.FormMain.m_formAlarm.SetMotorAlarm(item.Key + "Z轴");
                                item.Value.tablePreStatus.bAlarmZ = bAlarmZ;
                            }
                        }
                        else
                        {
                            if (bAlarmZ != item.Value.tablePreStatus.bAlarmZ)
                            {
                                MainModule.FormMain.m_formAlarm.RstMotorAlarm(item.Key + "Z轴");
                                item.Value.tablePreStatus.bAlarmZ = bAlarmZ;
                            }
                        }
                        if (bAlarmU)
                        {
                            if (bAlarmU != item.Value.tablePreStatus.bAlarmU)
                            {
                                //HandlerMotorAlarmEvent(item.Key + "U轴", 0);
                                MainModule.FormMain.m_formAlarm.SetMotorAlarm(item.Key + "U轴");
                                item.Value.tablePreStatus.bAlarmU = bAlarmU;
                            }
                        }
                        else
                        {
                            if (bAlarmU != item.Value.tablePreStatus.bAlarmU)
                            {
                                MainModule.FormMain.m_formAlarm.RstMotorAlarm(item.Key + "U轴");
                                item.Value.tablePreStatus.bAlarmU = bAlarmU;
                            }
                        }
                        if (bCWLX)
                        {
                            if (bCWLX != item.Value.tablePreStatus.bCWLX)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "X轴");
                                    //HandlerMotorAlarmEvent(item.Key + "X轴", 1);
                                //MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "X轴");
                            }
                        }
                        else
                        {
                            if (bCWLX != item.Value.tablePreStatus.bCWLX)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "X轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "X轴",1);
                                    //MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "X轴");
                            }
                        }
                        if (bCWLY)
                        {
                            if (bCWLY != item.Value.tablePreStatus.bCWLY)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "Y轴");
                                    //HandlerMotorAlarmEvent(item.Key + "Y轴", 1);
                                //MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "Y轴");
                            }
                        }
                        else
                        {
                            if (bCWLY != item.Value.tablePreStatus.bCWLY)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "Y轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "Y轴", 1);
                                
                            }
                        }
                        if (bCWLZ)
                        {
                            if (bCWLZ != item.Value.tablePreStatus.bCWLZ)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "Z轴");
                                    //HandlerMotorAlarmEvent(item.Key + "Z轴", 1);
                                //MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "Z轴");
                            }
                        }
                        else
                        {
                            if (bCWLZ != item.Value.tablePreStatus.bCWLZ)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "Z轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "Z轴", 1);
                                
                            }
                        }
                        if (bCWLU)
                        {
                            if (bCWLU != item.Value.tablePreStatus.bCWLU)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "U轴");
                                    //HandlerMotorAlarmEvent(item.Key + "U轴", 1);
                                //MainModule.FormMain.m_formAlarm.SetMotorCWLAlarm(item.Key + "U轴");
                            }
                        }
                        else
                        {
                            if (bCWLU != item.Value.tablePreStatus.bCWLU)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "U轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "U轴", 1);
                                //MainModule.FormMain.m_formAlarm.RstMotorCWLAlarm(item.Key + "U轴");
                            }
                        }
                        if (bCCWLX)
                        {
                            if (bCCWLX != item.Value.tablePreStatus.bCCWLX)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "X轴");
                                    //HandlerMotorAlarmEvent(item.Key + "X轴", 2);
                                //MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "X轴");
                            }
                        }
                        else
                        {
                            if (bCCWLX != item.Value.tablePreStatus.bCCWLX)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "X轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "X轴", 2);
                                //MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "X轴");
                            }
                        }
                        if (bCCWLY)
                        {
                            if (bCCWLY != item.Value.tablePreStatus.bCCWLY)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "Y轴");
                                    //HandlerMotorAlarmEvent(item.Key + "Y轴", 2);
                                //MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "Y轴");
                            }
                        }
                        else
                        {
                            if (bCCWLY != item.Value.tablePreStatus.bCCWLY)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "Y轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "Y轴", 2);
                                //MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "Y轴");
                            }
                        }
                        if (bCCWLZ)
                        {
                            if (bCCWLZ != item.Value.tablePreStatus.bCCWLZ)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "Z轴");
                                    //HandlerMotorAlarmEvent(item.Key + "Z轴", 2);
                                //MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "Z轴");
                            }
                        }
                        else
                        {
                            if (bCCWLZ != item.Value.tablePreStatus.bCCWLZ)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    //HandlerResetMotorAlarmEvent(item.Key + "Z轴", 2);
                                    MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "Z轴");
                            }
                        }
                        if (bCCWLU)
                        {
                            if (bCCWLU != item.Value.tablePreStatus.bCCWLU)
                            {
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "U轴");
                                    //HandlerMotorAlarmEvent(item.Key + "U轴", 2);
                                //MainModule.FormMain.m_formAlarm.SetMotorCCWLAlarm(item.Key + "U轴");
                            }
                        }
                        else
                        {
                            if (bCCWLU != item.Value.tablePreStatus.bCCWLU)
                            {
                               
                                if (MainModule.FormMain.bHomeReady)
                                    MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "U轴");
                                    //HandlerResetMotorAlarmEvent(item.Key + "U轴", 2);
                                //MainModule.FormMain.m_formAlarm.RstMotorCCWLAlarm(item.Key + "U轴");
                            }
                        }

                        item.Value.tablePreStatus.bCWLX = bCWLX;
                        item.Value.tablePreStatus.bCWLY = bCWLY;
                        item.Value.tablePreStatus.bCWLZ = bCWLZ;
                        item.Value.tablePreStatus.bCWLU = bCWLU;
                        item.Value.tablePreStatus.bCCWLX = bCCWLX;
                        item.Value.tablePreStatus.bCCWLY = bCCWLY;
                        item.Value.tablePreStatus.bCCWLZ = bCCWLZ;
                        item.Value.tablePreStatus.bCCWLU = bCCWLU;
                        if (MainModule.FormMain!=null)
                        {
                            if (MainModule.FormMain.bResetPress)
                            {
                                if (bPreResetPress == false)
                                {
                                    item.Value.tablePreStatus.bAlarmX = false;
                                    item.Value.tablePreStatus.bAlarmY = false;
                                    item.Value.tablePreStatus.bAlarmZ = false;
                                    item.Value.tablePreStatus.bAlarmU = false;
                                    //item.Value.tablePreStatus.bCWLX = false;
                                    //item.Value.tablePreStatus.bCWLY = false;
                                    //item.Value.tablePreStatus.bCWLZ = false;
                                    //item.Value.tablePreStatus.bCWLU = false;
                                    //item.Value.tablePreStatus.bCCWLX = false;
                                    //item.Value.tablePreStatus.bCCWLY = false;
                                    //item.Value.tablePreStatus.bCCWLZ = false;
                                    //item.Value.tablePreStatus.bCCWLU = false;
                                }
                            }
                            bPreResetPress = MainModule.FormMain.bResetPress;
                        }
                        
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
