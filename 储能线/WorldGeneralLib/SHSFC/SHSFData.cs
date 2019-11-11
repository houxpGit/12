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
    public class SHSFData
    {
        public string strConnectStr = "";
         public string strBuName = "";
         public string strLineName = "";
         public string strStation = "";
        [NonSerialized]
        public string m_strObjName = "";
        public SHSFData()
        {
 
        }
        [CategoryAttribute("服务器设定")]
        public string ConnectStr
        {
            get
            {
                return strConnectStr;
            }
            set
            {
                strConnectStr = value;
            }
        }
        [CategoryAttribute("服务器设定")]
        public string BuName
        {
            get
            {
                return strBuName;
            }
            set
            {
                strBuName = value;
            }
        }
        [CategoryAttribute("服务器设定")]
        public string LineNmae
        {
            get
            {
                return strLineName;
            }
            set
            {
                strLineName = value;
            }
        }
        [CategoryAttribute("服务器设定")]
        public string Station
        {
            get
            {
                return strStation;
            }
            set
            {
                strStation = value;
            }
        }
        public static SHSFData LoadObj(string strObjName)
        {
            SHSFData pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SHSFData));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/MUSASHIData" + strObjName + ".xml");
                pDoc = (SHSFData)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new SHSFData();
            }
            pDoc.m_strObjName = strObjName;
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/MUSASHIData" + m_strObjName + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SHSFData));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
