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

namespace ControlPlatformLib
{
    //[XmlInclude(typeof(HardwareDoc))] 
    public class HardwareDoc
    {
        public List<HardWareInfoBase> m_HardWareList;
        [XmlIgnore]
        public Dictionary<string, HardWareInfoBase> m_HardWareDictionary;
        public HardwareDoc()
        {
            m_HardWareList = new List<HardWareInfoBase>();
            m_HardWareDictionary = new Dictionary<string, HardWareInfoBase>();
        }
        public static HardwareDoc LoadObj()
        {
            HardwareDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HardwareDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/HardwareDoc" + ".xml");
                pDoc = (HardwareDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_HardWareDictionary = pDoc.m_HardWareList.ToDictionary(p => p.hardwareName);

            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new HardwareDoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/HardwareDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HardwareDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }

    }
}
