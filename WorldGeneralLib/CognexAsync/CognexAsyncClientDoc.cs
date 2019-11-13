using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Xml.Serialization;
using System.Net;


namespace WorldGeneralLib.CognexAsync
{
    public class CognexAsyncClientDoc
    {
        public string hostipaddress;
        public int nhostport;
        [XmlIgnore]
        public IPAddress ipAddress;

        public CognexAsyncClientDoc()
        {
            hostipaddress = string.Empty;
            IPAddress[] IP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ipitem in IP)
            {
                if (ipitem.AddressFamily.ToString() == "InterNetwork")
                {
                    ipAddress = ipitem;
                    hostipaddress = ipitem.ToString();
                    break;
                }
            }
            if (string.IsNullOrEmpty(hostipaddress))
            {
                ipAddress = IPAddress.Parse("192.168.1.253");
                hostipaddress = ipAddress.ToString();
            }
            nhostport = 8080;
        }

        public static CognexAsyncClientDoc LoadDocument()
        {
            CognexAsyncClientDoc m_Doc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CognexAsyncClientDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/CognexAsyncClientDoc.xml");
                m_Doc = (CognexAsyncClientDoc)xmlSerializer.Deserialize(fsReader);
                m_Doc.ipAddress = IPAddress.Parse(m_Doc.hostipaddress);
                fsReader.Close();
            }
            catch
            {
                if (fsReader != null)
                    fsReader.Close();
                m_Doc = new CognexAsyncClientDoc();
            }
            return m_Doc;
        }

        public bool SaveDocument()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/CognexAsyncClientDoc.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CognexAsyncClientDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();
            return true;
        }


    }
}
