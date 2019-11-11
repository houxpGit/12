using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorldGeneralLib.SerialPorts
{
    public class SerialPortDoc
    {
        public List<SerialPortData> m_SerialPortDataList;
        [XmlIgnore]
        public Dictionary<string, SerialPortData> m_SerialPortDataDictionary;
        public SerialPortDoc()
        {
            m_SerialPortDataList = new List<SerialPortData>();
            m_SerialPortDataDictionary = new Dictionary<string, SerialPortData>();
        }
        /// <summary>
        /// 载入数据
        /// </summary>
        /// <returns></returns>
        public static SerialPortDoc LoadObj()
        {
            SerialPortDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerialPortDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/SerialPortDoc" + ".xml");
                pDoc = (SerialPortDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_SerialPortDataDictionary = pDoc.m_SerialPortDataList.ToDictionary(p => p.StationName);
            }
            catch (Exception ex)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new SerialPortDoc();
            }
            return pDoc;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/SerialPortDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerialPortDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
