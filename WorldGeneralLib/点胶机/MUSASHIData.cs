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
    public class MUSASHIData
    {
        public string strPortName = "Com1";
        [NonSerialized]
        public string m_strObjName = "";
        public MUSASHIData()
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
        public static MUSASHIData LoadObj(string strObjName)
        {
            MUSASHIData pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MUSASHIData));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/MUSASHIData" + strObjName + ".xml");
                pDoc = (MUSASHIData)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new MUSASHIData();
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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MUSASHIData));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
