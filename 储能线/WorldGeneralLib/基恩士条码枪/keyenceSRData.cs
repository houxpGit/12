using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WorldGeneralLib
{
    public class keyenceSRData
    {
        private string _robotRomoteIp = "127.0.0.1";
        private int _robotRomotePort = 2015;
        private TcpIpType _tcpIpType;
        private bool bContinueMode = false;
        [NonSerialized]
        public string m_strBarName = "";
        public keyenceSRData()
        {
 
        }
        public static keyenceSRData LoadObj(string strBarName)
        {
            keyenceSRData pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(keyenceSRData));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/keyence"+strBarName+".xml");
                pDoc = (keyenceSRData)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new keyenceSRData();
            }
            pDoc.m_strBarName = strBarName;
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/keyence" + m_strBarName + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(keyenceSRData));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        [CategoryAttribute("通信设定")]
        public string RobotRomoteIp
        {
            get
            {
                return _robotRomoteIp;
            }
            set
            {
                _robotRomoteIp = value;
            }
        }
        [CategoryAttribute("通信设定")]
        public int RobotRomotePort
        {
            get
            {
                return _robotRomotePort;
            }
            set
            {
                _robotRomotePort = value;
            }
        }
        [CategoryAttribute("通信设定")]
        public TcpIpType TcpIpType
        {
            get
            {
                return _tcpIpType;
            }
            set
            {
                _tcpIpType = value;
            }
        }
        [CategoryAttribute("连续读取")]
        public bool 连续读取
        {
            get
            {
                return bContinueMode;
            }
            set
            {
                bContinueMode = value;
            }
        }
    }
}
