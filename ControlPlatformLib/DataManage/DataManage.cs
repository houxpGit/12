using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public enum ProjectDataType
    {
        STRING,
        INT,
        SHORT,
        DOUBLE,
        BOOL
    }
    static public class DataManage
    {
        static public ProjectDataDoc m_Doc;
        static public FormProjectDataSetting frmProjectData;
        static public void LoadData()
        {
            m_Doc = ProjectDataDoc.LoadObj();
        }
        static public string StrValue(string strGroupName, string strItemName)
        {
            string strReturn = "";
            try
            {
                strReturn = m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue.ToString();
            }
            catch
            {
                if (m_Doc.m_dataDictionary.ContainsKey(strGroupName) == false)
                {

                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在组名字为:" + strGroupName + "的参数据");
                    ProjectDataS group = new ProjectDataS();
                    m_Doc.m_dataDictionary.Add(strGroupName, group);
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.ContainsKey(strItemName) == false)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    ProjectDataBase data = new ProjectDataBase();
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.Add(strItemName, data);
                    data.strName = strItemName;
                    data.dataType = ProjectDataType.STRING;
                    data.objValue = "";
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType != ProjectDataType.STRING)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是字符");
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType = ProjectDataType.STRING;
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue = "";
                }
            }
            return strReturn;
        }
        static public int IntValue(string strGroupName, string strItemName)
        {
            int iReturn = 0;
            try
            {
                iReturn = int.Parse(m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue.ToString());
            }
            catch
            {
                if (m_Doc.m_dataDictionary.ContainsKey(strGroupName) == false)
                {

                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在组名字为:" + strGroupName + "的参数据");
                    ProjectDataS group = new ProjectDataS();
                    m_Doc.m_dataDictionary.Add(strGroupName, group);
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.ContainsKey(strItemName) == false)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    ProjectDataBase data = new ProjectDataBase();
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.Add(strItemName, data);
                    data.strName = strItemName;
                    data.dataType = ProjectDataType.INT;
                    data.objValue = 0;
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType != ProjectDataType.INT)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是整形");
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType = ProjectDataType.INT;
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue = 0;
                }
            }
            return iReturn;
        }
        static public short ShortValue(string strGroupName, string strItemName)
        {
            short sReturn = 0;
            try
            {
                sReturn = short.Parse(m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue.ToString());
            }
            catch
            {
                if (m_Doc.m_dataDictionary.ContainsKey(strGroupName) == false)
                {

                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在组名字为:" + strGroupName + "的参数据");
                    ProjectDataS group = new ProjectDataS();
                    m_Doc.m_dataDictionary.Add(strGroupName, group);
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.ContainsKey(strItemName) == false)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    ProjectDataBase data = new ProjectDataBase();
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.Add(strItemName, data);
                    data.strName = strItemName;
                    data.dataType = ProjectDataType.SHORT;
                    data.objValue = 0;
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType != ProjectDataType.SHORT)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是Short");
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType = ProjectDataType.SHORT;
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue = 0;
                }
            }
            return sReturn;
        }
        static public double DoubleValue(string strGroupName, string strItemName)
        {
            double dReturn = 0.0;
            try
            {
                dReturn = double.Parse(m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue.ToString());
            }
            catch
            {
                if (m_Doc.m_dataDictionary.ContainsKey(strGroupName) == false)
                {

                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在组名字为:" + strGroupName + "的参数据");
                    ProjectDataS group = new ProjectDataS();
                    m_Doc.m_dataDictionary.Add(strGroupName, group);
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.ContainsKey(strItemName) == false)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    ProjectDataBase data = new ProjectDataBase();
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.Add(strItemName, data);
                    data.strName = strItemName;
                    data.dataType = ProjectDataType.DOUBLE;
                    data.objValue = 0.0;
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType != ProjectDataType.DOUBLE)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + ":" + strItemName + "的参数" + "浮点型的");
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType = ProjectDataType.DOUBLE;
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue = 0.0;
                }
            }
            return dReturn;
        }
        static public bool BOOLValue(string strGroupName, string strItemName)
        {
            bool bReturn = false;
            try
            {
                bReturn = bool.Parse(m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue.ToString());
            }
            catch
            {
                if (m_Doc.m_dataDictionary.ContainsKey(strGroupName) == false)
                {

                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("不存在组名字为:" + strGroupName + "的参数据");
                    ProjectDataS group = new ProjectDataS();
                    m_Doc.m_dataDictionary.Add(strGroupName, group);
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.ContainsKey(strItemName) == false)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    ProjectDataBase data = new ProjectDataBase();
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary.Add(strItemName, data);
                    data.strName = strItemName;
                    data.dataType = ProjectDataType.BOOL;
                    data.objValue = false;
                }
                if (m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType != ProjectDataType.BOOL)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是布尔型");
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].dataType = ProjectDataType.BOOL;
                    m_Doc.m_dataDictionary[strGroupName].m_dataDictionary[strItemName].objValue = false;
                }
            }
            return bReturn;
        }

    }
}
