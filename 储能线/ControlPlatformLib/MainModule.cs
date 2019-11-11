using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using ControlPlatformLib.Models;
using WorldGeneralLib.DataLogicBarCode;

namespace ControlPlatformLib
{
    public static class MainModule
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        public static MainForm FormMain;
        public static bool bLogin;


        [STAThread]
        public static void StartModule(Form startForm,Form cadEditForm,Form settingForm, string strMachineName)
        {
            FormMain = new MainForm();
            FormMain.m_formStart = startForm;
            FormMain.m_formManual = cadEditForm;
            //FormMain.m_formInfoQuery = InfoQueryForm;
            FormMain.m_formDebug =FormHardwareMonitor.GetForm();
            FormMain.m_formUserMageage = FormSystemSetting.GetForm() ;
          //  FormMain.MainFrm = MainFrm.GetForm();
            FormMain.m_strMainWidowText = strMachineName;
            FormMain.m_formParameter = settingForm;
          //  FormMain =  MainFrm.GetForm();
          
            Application.Run(FormMain);
        }
        public static bool LoadData()
        {
            HardwareManage.LoadData();
            TableManage.LoadData();
            IOManage.LoadData();
            DataManage.LoadData();
            PathDataManage.InitData();
            BarcodeDataManage.Load();
            
            return true;
        }
        public static bool InitHardware()
        {
            HardwareManage.InitHardWare();
            TableManage.InitTables();
            IOManage.InitIOs();
            return true;
        }

        public static bool CloseHardware()
        {
            HardwareManage.CloseHardWare();
            return true;
        }
        /// <summary>
        /// 界面启动
        /// </summary>
        /// <param name="startForm">开始界面</param>
        /// <param name="cadEditForm">左二界面</param>
        /// <param name="settingForm">左三界面</param>
        /// <param name="strMachineName">设备名称</param>
        /// <param name="userLevelChanged">用户权限更新回调函数</param>
        [STAThread]
        public static void StartModule(Form startForm, Form  Log_In, Form form2, Form settingForm, string strMachineName, Action<int> userLevelChanged)
        {
            FormMain = new MainForm();
            FormMain.UserLevelChangedAction += userLevelChanged;
            FormMain.listForm.Add(startForm);
            FormMain.listForm.Add(Log_In);
           // FormMain.m_formStart = startForm;
           // startForm.Hide();
            FormMain.m_formStart = startForm;
           // Log_In.SetBounds(50, 100, FormMain.m_formStart.Width, FormMain.m_formStart.Height);
         
            FormMain.m_formManual = form2;
            //FormMain.m_formInfoQuery = InfoQueryForm;
            FormMain.m_formDebug = FormHardwareMonitor.GetForm();
            FormMain.m_formUserMageage = FormSystemSetting.GetForm();
            FormMain.m_strMainWidowText = strMachineName;
            FormMain.m_formParameter = settingForm;
            //Global.m_KPIMES = new UploadDataMES.ServiceClient();
            Application.Run(FormMain);
        }
        //public static string this[string strGroup,string strItem]
        //{
        //    get
        //    {
        //        return m_glabatDoc.m_StrParaGroupDictionary[strGroup].m_strParaDictionary[strItem];
        //    }

        //}
    }
}
