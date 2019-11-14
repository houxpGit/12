using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.InteropServices;//for INI
using System.Windows.Forms;

#region CardVarType
using TCard = System.Int16;
using TAxis = System.Int16;
using TMode = System.Int16;
using TSpeed = System.Double;
using TAcc = System.Single;

using TReturn = System.Int16;
using TPusle = System.Int32;

using TIOPoint = System.UInt16;
#endregion

namespace ControlPlatformLib
{

    public class ConfINI
    {
        private static ConfINI ConfIN;
        public static ConfINI Instance()
        {
            if (ConfIN == null)
            {
                ConfIN = new ConfINI();
            }
            return ConfIN;
        }
        public const int POS_RADIO = 1000;
        #region//   call kernel32
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
        public static void writeINI(string section, string key, string val, string path)    //Writes data to the INI file
        {
            WritePrivateProfileString(section, key, val, path);
        }
        public static string GetINI(string section, string key, string errDefault, string path)//Read the data from the INI file
        {
            StringBuilder get = new StringBuilder(500);
            GetPrivateProfileString(section, key, errDefault, get, 500, path);
            return get.ToString();
        }

        #region//  variable definition
        public const string STPARA = "AxisParas";
        public const string Compensation = "AxisCompensation";
        public const string TemperatureTEC = "TecSeting";
        //public const string TemperatureDUT = "time1";
        public const string TemperatureDUT = "DUTSeting";
        public const string Speed = "AxisSpeed";
        public static string SavePath = Application.StartupPath + "\\SettingLINE.ini";
        public static string TestPath = Application.StartupPath + "\\SettingINT.ini";
        public static string CompensationPath = Application.StartupPath + "\\SettingBug.ini";
    //    public const string STPARA = "AxisParas";

       // public static string SavePath = Application.StartupPath + "\\Setting.ini";
       // public static string TestPath = Application.StartupPath + "\\testSetting.ini";
        public const string PRODUCTION = "Production";// for save parameter
        internal static object ini;
    }
    #endregion
}
