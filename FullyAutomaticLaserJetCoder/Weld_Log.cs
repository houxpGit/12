using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;
using ControlPlatformLib;
using System.Timers;

namespace FullyAutomaticLaserJetCoder
{
    /// <summary>
    /// 等级选项,打印不同等级的 log 日志
    /// different level log
    /// </summary>
    /// 


    public enum LOG_LEVEL
    {
        LEVEL_1,     //All log
        LEVEL_2,     //MTCP log
        LEVEL_3,     //Canyonboard log
        LEVEL_4,     //PLC log
        LEVEL_5,     //Servo log
        LEVEL_6,     //Cylinder log
    };

    public enum LOG_TYPE
    {
        INFO,
        ERROR,
        ORIGINAL
    }
    /// <summary>
    ///  log 日志类目
    ///  log class
    /// </summary>
    class Weld_Log
    {
       // private static System.Timers.Timer CheckUpdatetimer = new System.Timers.Timer();
        private ConcurrentQueue<string> mSendQueue = new ConcurrentQueue<string>();
        const string NORMAL = "NORMAL";
        const string SAVE = "Save";
        const string ORIGINAL = "original";
        public static string DateNow = DateTime.Now.ToString("yyyy_MM_dd");

        //================== main log ================
        public static string jp_LogFolderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\MainLog\\";
        public static string jp_LogFileName = DateNow + "_JspLog" + ".txt";
        public static List<string> ErrorList = new List<string>();
        private object jpLog_locker = new object();
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(jp_LogFileName);
        private StreamWriter jpWriter;
        private FileStream jpFileStream = null;

        //================= level log ================
        private static string jp_LEVEL_1_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_1\\";
        private static string jp_LEVEL_2_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_2\\";
        private static string jp_LEVEL_3_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_3\\";
        private static string jp_LEVEL_4_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_4\\";
        private static string jp_LEVEL_5_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_5\\";
        private static string jp_LEVEL_6_folderPath = System.Environment.CurrentDirectory + "\\Weld_Log\\LEVEL_6\\";

        private static string jp_LEVEL_1_fileName = jp_LEVEL_1_folderPath + DateNow + "_LEVEL_1" + ".txt";
        private static string jp_LEVEL_2_fileName = jp_LEVEL_2_folderPath + DateNow + "_LEVEL_2" + ".txt";
        private static string jp_LEVEL_3_fileName = jp_LEVEL_3_folderPath + DateNow + "_LEVEL_3" + ".txt";
        private static string jp_LEVEL_4_fileName = jp_LEVEL_4_folderPath + DateNow + "_LEVEL_4" + ".txt";
        private static string jp_LEVEL_5_fileName = jp_LEVEL_5_folderPath + DateNow + "_LEVEL_5" + ".txt";
        private static string jp_LEVEL_6_fileName = jp_LEVEL_6_folderPath + DateNow + "_LEVEL_6" + ".txt";

        private System.IO.FileInfo fileInfo_1 = new System.IO.FileInfo(jp_LEVEL_1_fileName);
        private System.IO.FileInfo fileInfo_2 = new System.IO.FileInfo(jp_LEVEL_2_fileName);
        private System.IO.FileInfo fileInfo_3 = new System.IO.FileInfo(jp_LEVEL_3_fileName);
        private System.IO.FileInfo fileInfo_4 = new System.IO.FileInfo(jp_LEVEL_4_fileName);
        private System.IO.FileInfo fileInfo_5 = new System.IO.FileInfo(jp_LEVEL_5_fileName);
        private System.IO.FileInfo fileInfo_6 = new System.IO.FileInfo(jp_LEVEL_6_fileName);

        private object jpLocker_LEVEL_1 = new object();
        private StreamWriter jpWriter_LEVEL_1;
        private FileStream jpFileStream_LEVEL_1 = null;

        private object jpLocker_LEVEL_2 = new object();
        private StreamWriter jpWriter_LEVEL_2;
        private FileStream jpFileStream_LEVEL_2 = null;

        private object jpLocker_LEVEL_3 = new object();
        private StreamWriter jpWriter_LEVEL_3;
        private FileStream jpFileStream_LEVEL_3 = null;

        private object jpLocker_LEVEL_4 = new object();
        private StreamWriter jpWriter_LEVEL_4;
        private FileStream jpFileStream_LEVEL_4 = null;

        private object jpLocker_LEVEL_5 = new object();
        private StreamWriter jpWriter_LEVEL_5;
        private FileStream jpFileStream_LEVEL_5 = null;

        private object jpLocker_LEVEL_6 = new object();
        private StreamWriter jpWriter_LEVEL_6;
        private FileStream jpFileStream_LEVEL_6 = null;
        //=============================================
        private static int CheckUpDateLock = 0;
        private static System.Timers.Timer CheckUpdatetimer = new System.Timers.Timer();
        private static object LockObject = new Object();

        public  void GetTimerStart()
        {
            // 循环间隔时间(10分钟)
            CheckUpdatetimer.Interval = 100;
            // 允许Timer执行
            CheckUpdatetimer.Enabled = true;
            // 定义回调
            CheckUpdatetimer.Elapsed += new ElapsedEventHandler(CheckUpdatetimer_Elapsed);
            // 定义多次循环
            CheckUpdatetimer.AutoReset = true;
            CheckUpdatetimer.Start();
            //   CheckUpdatetimer.Stop();
        }
        public  void CheckUpdatetimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // 加锁检查更新锁
            lock (LockObject)
            {
                if (CheckUpDateLock == 0) CheckUpDateLock = 1;
                else return;
            }

            //More code goes here.
            //具体实现功能的方法
            Check();
            // 解锁更新检查锁
            lock (LockObject)
            {
                CheckUpDateLock = 0;
            }
        }
        public  void Check()
        {
            string en = "";
            string sendd = "";
            sendd = Dequeue(en);
            if (sendd != "")
            {
                WriteLog(LOG_LEVEL.LEVEL_3, sendd, LOG_TYPE.INFO);

            }
        }
        private static Weld_Log jasperLog;
        public static Weld_Log Instance()
        {
            if (jasperLog == null)
            {
             
                jasperLog = new Weld_Log();
            
            }
            return jasperLog;
        }
        object lock1 = new object();
        public Weld_Log()
        {
            GetTimerStart();
            //ThendS();
        }
        public void Enqueue(LOG_LEVEL log_level, string sendStr)//添加数据到队列
        {   //R00501
            lock (lock1)
            {
                try
                {
                    mSendQueue.Enqueue(sendStr);
                     // Global.logger.Info(sendStr);
                     //   DisplayLogOnWindow(log_level, sendStr);
                  //  WriteLog(LOG_LEVEL.LEVEL_3, sendStr, LOG_TYPE.INFO);
                }
                catch
                {
                }
               
           
            }
        }
        public string Dequeue(string def)//出队列
        {
            string sendStr = "";
            if (mSendQueue.TryDequeue(out sendStr))
            {
                return sendStr;
            }
            else
            {
                return def;
            }
        }
        public void ThendS()
        {
            Thread WriteLog = new Thread(WriteLogFun);
            WriteLog.IsBackground = true;
            WriteLog.Start();

        }
       public void WriteLogFun()
        {
            while (true)
            {
                string en = "";
                string sendd = "";
                sendd = Dequeue(en);
                if (sendd != "")
                {
                    WriteLog(LOG_LEVEL.LEVEL_3, sendd, LOG_TYPE.INFO);

                }
                else
                {

                   // Thread.Sleep(200);
                }
            }
       
          

        }
        //回调的定义   
        //Definition of callback
        public static event Action<string> Level_Log_CallBack;

        /// <summary>
        /// used this function to write the log and display to GUI
        /// </summary>
        /// <param name="log_level"> level of log </param>
        /// <param name="inputString">input string </param>
        /// 

        private object jpLog_locker12 = new object();
        public  void jp_writeLogWithLevel(LOG_LEVEL log_level, string inputString)
        {
            lock (jpLog_locker12)
            {
                try
                {
                    WriteLog(log_level, inputString, LOG_TYPE.INFO);

                }
                catch { }
            }
    
          
        }

        public void jp_writeErrorWithLevel(LOG_LEVEL log_level, string inputString)
        {
            //FinalError = inputString;
            WriteLog(log_level, inputString, LOG_TYPE.ERROR);
        }

        public void jp_writeFinalError(LOG_LEVEL log_level)
        {
            if (ErrorList.Count > 0)
            {
                WriteLog(log_level, ErrorList.Last(), LOG_TYPE.ORIGINAL);
            }
        }

        public  void WriteLog(LOG_LEVEL log_level, string inputString, LOG_TYPE type)
        {
            if (!Directory.Exists(jp_LogFolderPath))
            {
                Directory.CreateDirectory(jp_LogFolderPath);
            }

            try
            {
                lock (jpLog_locker)
                {
                    string logPath = Path.Combine(jp_LogFolderPath, jp_LogFileName);
                    fileInfo = new System.IO.FileInfo(logPath);
                    //Log file execution rearrangement, 24.10.2018 Charles
                    if (!fileInfo.Exists)
                    {
                        jpFileStream = fileInfo.Create();
                        jpFileStream.Close();
                        jpFileStream.Dispose();
                        //fileInfo = new System.IO.FileInfo(logPath);
                    }
                    //try
                    //{
                    //    jpFileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);

                    //}
                    //catch
                    //{
                    //    jpWriter.Close();
                    //    jpWriter.Dispose();
                    //    jpFileStream.Close();
                    //    jpFileStream.Dispose();
                    //}
                    jpFileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    jpWriter = new StreamWriter(jpFileStream);
                  
                    //在不同的 level 回调到不同的 view
                    // The callback is different at different levelsif
                        
                    string logStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "," + type.ToString() +"," + inputString;
                    if (type == LOG_TYPE.ERROR)
                    {
                        ErrorList.Add(logStr);
                    }
                    else if (type == LOG_TYPE.ORIGINAL)
                    {
                        logStr = inputString;
                    }

                    jpWriter.WriteLine(logStr);
                    DisplayLogOnWindow(log_level, logStr);
                    jpWriter.Close();
                    jpWriter.Dispose();
                    jpFileStream.Close();
                    jpFileStream.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging " + inputString + " at path: " + jp_LogFileName + "->" + e.Message);
                jpWriter.Close();
                jpWriter.Dispose();
                jpFileStream.Close();
                jpFileStream.Dispose();
                //MessageBox.Show("ATSLOG  " + e.ToString());
            }
            finally
            {
                if (jpWriter != null)
                {
                    jpWriter.Close();
                    jpWriter.Dispose();
                    jpFileStream.Close();
                    jpFileStream.Dispose();
                }
            }
        }

        public void ClearHistoryError()
        {
            ErrorList.Clear();
        }

        private  void jp_LEVEL_1_log(string inputString)//Level 1 writes data
        {
            if (!Directory.Exists(jp_LEVEL_1_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_1_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_1)
                {
                    if (!fileInfo_1.Exists)
                    {
                        jpFileStream_LEVEL_1 = fileInfo_1.Create();
                        jpWriter_LEVEL_1 = new StreamWriter(jpFileStream_LEVEL_1);
                    }
                    else
                    {
                        jpFileStream_LEVEL_1 = fileInfo_1.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_1 = new StreamWriter(jpFileStream_LEVEL_1);
                    }

                    jpWriter_LEVEL_1.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_1 ---" + inputString);

                    jpWriter_LEVEL_1.Close();
                    jpWriter_LEVEL_1.Dispose();
                    jpFileStream_LEVEL_1.Close();
                    jpFileStream_LEVEL_1.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_1 " + inputString + " at path: " + jp_LEVEL_1_fileName + "->" + e.Message);
                jpWriter_LEVEL_1.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_1 " + inputString + " at path: " + jp_LEVEL_1_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        private  void jp_LEVEL_2_log(string inputString)//Level 2 writes data
        {
            if (!Directory.Exists(jp_LEVEL_2_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_2_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_2)
                {
                    if (!fileInfo_2.Exists)
                    {
                        jpFileStream_LEVEL_2 = fileInfo_2.Create();
                        jpWriter_LEVEL_2 = new StreamWriter(jpFileStream_LEVEL_2);
                    }
                    else
                    {
                        jpFileStream_LEVEL_2 = fileInfo_2.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_2 = new StreamWriter(jpFileStream_LEVEL_2);
                    }

                    jpWriter_LEVEL_2.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_2 ---" + inputString);

                    jpWriter_LEVEL_2.Close();
                    jpWriter_LEVEL_2.Dispose();
                    jpFileStream_LEVEL_2.Close();
                    jpFileStream_LEVEL_2.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_2 " + inputString + " at path: " + jp_LEVEL_2_fileName + "->" + e.Message);

                jpWriter_LEVEL_2.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_2 " + inputString + " at path: " + jp_LEVEL_2_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        private  void jp_LEVEL_3_log(string inputString)//Level 3 writes data
        {
            if (!Directory.Exists(jp_LEVEL_3_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_3_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_3)
                {
                    if (!fileInfo_3.Exists)
                    {
                        jpFileStream_LEVEL_3 = fileInfo_3.Create();
                        jpWriter_LEVEL_3 = new StreamWriter(jpFileStream_LEVEL_3);
                    }
                    else
                    {
                        jpFileStream_LEVEL_3 = fileInfo_3.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_3 = new StreamWriter(jpFileStream_LEVEL_3);
                    }

                    jpWriter_LEVEL_3.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_3 ---" + inputString);

                    jpWriter_LEVEL_3.Close();
                    jpWriter_LEVEL_3.Dispose();
                    jpFileStream_LEVEL_3.Close();
                    jpFileStream_LEVEL_3.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_3 " + inputString + " at path: " + jp_LEVEL_3_fileName + "->" + e.Message);

                jpWriter_LEVEL_3.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_3 " + inputString + " at path: " + jp_LEVEL_3_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        private  void jp_LEVEL_4_log(string inputString)//Level 4 writes data
        {
            if (!Directory.Exists(jp_LEVEL_4_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_4_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_4)
                {
                    if (!fileInfo_4.Exists)
                    {
                        jpFileStream_LEVEL_4 = fileInfo_4.Create();
                        jpWriter_LEVEL_4 = new StreamWriter(jpFileStream_LEVEL_4);
                    }
                    else
                    {
                        jpFileStream_LEVEL_4 = fileInfo_4.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_4 = new StreamWriter(jpFileStream_LEVEL_4);
                    }

                    jpWriter_LEVEL_4.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_4 ---" + inputString);

                    jpWriter_LEVEL_4.Close();
                    jpWriter_LEVEL_4.Dispose();
                    jpFileStream_LEVEL_4.Close();
                    jpFileStream_LEVEL_4.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_4 " + inputString + " at path: " + jp_LEVEL_4_fileName + "->" + e.Message);

                jpWriter_LEVEL_4.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_4 " + inputString + " at path: " + jp_LEVEL_4_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        private void jp_LEVEL_5_log(string inputString)//Level 5 writes data
        {
            if (!Directory.Exists(jp_LEVEL_5_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_5_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_5)
                {
                    if (!fileInfo_5.Exists)
                    {
                        jpFileStream_LEVEL_5 = fileInfo_5.Create();
                        jpWriter_LEVEL_5 = new StreamWriter(jpFileStream_LEVEL_5);
                    }
                    else
                    {
                        jpFileStream_LEVEL_5 = fileInfo_5.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_5 = new StreamWriter(jpFileStream_LEVEL_5);
                    }

                    jpWriter_LEVEL_5.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_5 ---" + inputString);

                    jpWriter_LEVEL_5.Close();
                    jpWriter_LEVEL_5.Dispose();
                    jpFileStream_LEVEL_5.Close();
                    jpFileStream_LEVEL_5.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_5 " + inputString + " at path: " + jp_LEVEL_5_fileName + "->" + e.Message);

                jpWriter_LEVEL_5.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_5 " + inputString + " at path: " + jp_LEVEL_5_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        private void jp_LEVEL_6_log(string inputString)//Level 6 writes data
        {
            if (!Directory.Exists(jp_LEVEL_6_folderPath))
            {
                Directory.CreateDirectory(jp_LEVEL_6_folderPath);
            }

            try
            {
                lock (jpLocker_LEVEL_6)
                {
                    if (!fileInfo_6.Exists)
                    {
                        jpFileStream_LEVEL_6 = fileInfo_6.Create();
                        jpWriter_LEVEL_6 = new StreamWriter(jpFileStream_LEVEL_6);
                    }
                    else
                    {
                        jpFileStream_LEVEL_6 = fileInfo_6.Open(FileMode.Append, FileAccess.Write);
                        jpWriter_LEVEL_6 = new StreamWriter(jpFileStream_LEVEL_6);
                    }

                    jpWriter_LEVEL_6.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + "--- LEVEL_6 ---" + inputString);

                    jpWriter_LEVEL_6.Close();
                    jpWriter_LEVEL_6.Dispose();
                    jpFileStream_LEVEL_6.Close();
                    jpFileStream_LEVEL_6.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_6 " + inputString + " at path: " + jp_LEVEL_6_fileName + "->" + e.Message);

                jpWriter_LEVEL_6.WriteLine("ATSLog.writeLogByPath exception for logging LEVEL_6 " + inputString + " at path: " + jp_LEVEL_6_fileName + "->" + e.Message);
            }
            finally
            {
                if (jpWriter != null)
                {

                }
            }
        }

        public void DisplayLogOnWindow(LOG_LEVEL log_level,  string inputString)
        {
            //BeginInvoke(new MethodInvoker(delegate
            //{
            //    Program.stratForm.richTextBox1.AppendText(inputString);
            //}));
           
            //Task.Run(() =>
            //{
               Level_Log_CallBack?.Invoke( inputString);
            //});      
        }
    }

}
