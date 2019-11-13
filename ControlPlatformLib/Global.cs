using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class Global
    {
        public static log4net.ILog logger;
        //public static WorldGeneralLib.FormAlarm m_formAlarm;
        public static bool bHomeReady;
        public static bool bResetPress;
        public static bool weldingStarted;
        public static bool bPLCConnected;
        public static bool bTestWeldingPath;
        public static string sMachineNO;
        public static string sMachineNORight;
        public static string sUserName;
        public static string sMakeOrder;
        public static string sMachinePos;
        public static bool bIgnoreMes;
        public static bool bIgnoreKPIMes;
        public static string hostName;
        public static IPAddress localaddr;

        public delegate void HandleraAlarm(string alarm);
        static public event HandleraAlarm HandleraAlarmEvent;

        static public UploadDataMES.ServiceClient m_KPIMES;
        static Global()
        {
            try
            {
                //m_formAlarm = new WorldGeneralLib.FormAlarm();
                var logCfg = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
                XmlConfigurator.ConfigureAndWatch(logCfg);
                logger = log4net.LogManager.GetLogger("版本：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());//获取一个日志记录器 
                m_KPIMES = new UploadDataMES.ServiceClient();

                hostName = Dns.GetHostName();   //获取本机名
                IPHostEntry localhost = Dns.GetHostEntry(hostName);
                localaddr = localhost.AddressList[0];
            }
            catch (Exception)
            {
            }
            
        }
    }
}
