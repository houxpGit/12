using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;
using System.Net;

namespace WorldGeneralLib.Cognex
{
    public class CognexDoc
    {
        public string hostipaddress;
        public int nhostport;
        [XmlIgnore]
        public IPAddress ipAddress;

        public CognexDoc()
        {
            ipAddress = IPAddress.Parse("192.168.1.48");
            hostipaddress = ipAddress.ToString();
            nhostport = 6000;
        }

        public static CognexDoc LoadDocument()
        {
            CognexDoc m_Doc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CognexDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/CognexDoc.xml");
                m_Doc = (CognexDoc)xmlSerializer.Deserialize(fsReader);
                m_Doc.ipAddress = IPAddress.Parse(m_Doc.hostipaddress);
                fsReader.Close();
            }
            catch
            {
                if (fsReader != null)
                    fsReader.Close();
                m_Doc = new CognexDoc();
            }
            return m_Doc;
        }

        public bool SaveDocument()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/CognexDoc.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CognexDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();
            return true;
        }


    }
}
