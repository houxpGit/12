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
    public class MettlerData
    {
        public string strPortName = "COM1";
        [NonSerialized]
        public string m_strObjName = "";
        public MettlerData()
        {
 
        }
        [CategoryAttribute("通信设定")]
        public string PortName
        {
            get
            {
                return strPortName;
            }
            set
            {
                strPortName = value;
            }
        }
        public static MettlerData LoadObj(string strObjName)
        {
            MettlerData pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MettlerData));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/MettlerData" + strObjName + ".xml");
                pDoc = (MettlerData)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new MettlerData();
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
            FileStream fsWriter = new FileStream(@".//Parameter/MettlerData" + m_strObjName + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MettlerData));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
