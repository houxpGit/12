using ControlPlatformLib;
using FullyAutomaticLaserJetCoder.CCD;
using FullyAutomaticLaserJetCoder.MainTask;
//using LocationCircle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WorldGeneralLib.SerialPorts;

namespace FullyAutomaticLaserJetCoder
{
      static class Program
    {
        public static int TracePaint = 1;
        public static bool bFeederOK;
        public static bool bLaserNG;
        public static bool bNG;
        public static bool bMarkOK;
        public static bool bTransOK;
        public static bool bTransBlankOK;
        public static bool bCanTransfer;
        public static bool bNeedBlank;
        public static Queue<CCDStationA> ccdStationA;
        public static Queue<bool> resultQueue;
        public static int num = 0;
        public static double dLaserToMarkX = 0;
        public static double dLaserToMarkY = 0;
        /// <summary>
        /// OK托盘是否允许下料
        /// </summary>
        public static bool bCanOKBlanking;
        /// <summary>
        /// NG托盘是否允许下料
        /// </summary>
        public static bool bCanNGBlanking;

        public static StartForm stratForm;
       // public static StartForm startForm;
        public static SettingsForm settingForm;
  //      public static CCDForm ccdForm;
        public static LocationCircle.Setting form;



        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
         static void Main()
        {
            bool newMutexCreated = true;
            using (new Mutex(true, Assembly.GetExecutingAssembly().FullName, out newMutexCreated))
            {
                if (!newMutexCreated)
                {
                    MessageBox.Show("程序已启动！请不要启动多个程序！");
                    Environment.Exit(0);
                }
                else
                {
                    CommonTools.Tracing.AddId(TracePaint);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
               
                    LoginForm loginForm = new LoginForm();

                    loginForm.StartPosition = FormStartPosition.CenterScreen;
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                    else
                        return;



                    SerialPortDataManage.LoadData();
                    SerialPortDataManage.InitSerialPorts();
                    Splasher.Show(typeof(FrmSplashScreen));
                    Splasher.Status = "正在加载程序......";
                    MainModule.LoadData();
                    Splasher.Status = "正在加载参数......";
                    MainModule.LoadData();
                    Splasher.Status = "正在加载硬件参数......";
                    MainModule.InitHardware();
                    Splasher.Status = "正在初始化硬件......";

                    Splasher.Status = "正在初始化扫码枪......";
                    Splasher.Status = "正在加载程序......";
                      
                 //   LocationCircle.Setting form = new Setting();
                   // form.TopLevel = false;
                    ccdStationA = new Queue<CCDStationA>();
                    resultQueue=new Queue<bool>();
                //    CCD.CCDClientManage.Load();
                    stratForm = new StartForm();
                    Splasher.Close();
                    form = new LocationCircle.Setting();
                    // stratForm.Hide();
                    //视觉定位系统.FormMain ccdForm = new 视觉定位系统.FormMain();
                  
                    MainModule.StartModule(stratForm, loginForm, form, new SettingsForm(), "6KW4KW通用程序", null);
                    //ccdForm.UserControl(1);
                    CommonTools.Tracing.Terminate();
                }
            }
        }
        //public string[] ReadXml(string filename, string[] strArrayData)//string[] strArrayRow)
        //{
        //    try
        //    {
        //        //<1>实例化一个XML文档操作对象.
        //        XmlDocument doc = new XmlDocument();
        //        //<2>使用XML对象加载XML.
        //        doc.Load(filename);
        //        //<3>获取根节点.
        //        XmlNode root = doc.SelectSingleNode("Parameter");
        //        //<4>获取根节点下所有子节点.
        //        XmlNodeList nodeList = root.ChildNodes;

        //        //<5>遍历输出.
        //        foreach (XmlNode node in nodeList)
        //        {
        //            for (int i = 0; i < node.ChildNodes.Count; i++)
        //            {
        //                strArrayData[i] = node.ChildNodes[i].InnerText;
        //            }
        //        }
        //        return strArrayData;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //        //throw;
        //    }
        //}
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject);
        }
    }
}
