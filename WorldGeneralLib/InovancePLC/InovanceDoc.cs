using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using System.Net;
using System.Windows.Forms;

namespace WorldGeneralLib.InovancePLC
{
    public class InovanceDoc
    {
        public string stripAddress;
        public List<ScanItem> m_ScanDataList;

        [XmlIgnore]
        public Dictionary<string, ScanItem> m_scanDictionary;
        [XmlIgnore]
        public IPAddress ipAddress;
        [XmlIgnore]
        private int nplcindex;

        public InovanceDoc()
        {
            m_ScanDataList = new List<ScanItem>();
            m_scanDictionary = new Dictionary<string, ScanItem>();
            ipAddress = IPAddress.Parse("192.168.1.88");
            stripAddress = ipAddress.ToString();
            nplcindex = 0;
        }

        public static InovanceDoc LoadDocument(int plcindex)
        {
            InovanceDoc m_Doc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InovanceDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/InovanceDoc"+plcindex.ToString()+".xml");
                m_Doc = (InovanceDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                m_Doc.m_scanDictionary = m_Doc.m_ScanDataList.ToDictionary(p => p.strName);
                m_Doc.ipAddress = IPAddress.Parse(m_Doc.stripAddress);
                m_Doc.nplcindex = plcindex;
            }
            catch(Exception ex)
            {
                if (fsReader != null)
                    fsReader.Close();
                m_Doc = new InovanceDoc();
                MessageBox.Show(ex.Message);
            }
            return m_Doc;
        }

        public bool SaveDocument(int plcindex)
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/InovanceDoc"+plcindex.ToString()+".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InovanceDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();
            return true;
        }
    }
}
