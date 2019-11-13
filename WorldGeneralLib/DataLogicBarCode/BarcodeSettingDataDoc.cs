using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.DataLogicBarCode
{
    public class BarcodeSettingDataDoc
    {
        public List<BarcodeData> barcodeData;
        [XmlIgnore]
        public Dictionary<string, BarcodeData> barcodeDataDic;

        public BarcodeSettingDataDoc()
        {
            barcodeData = new List<BarcodeData>();
            barcodeDataDic = new Dictionary<string, BarcodeData>();
        }

        public static BarcodeSettingDataDoc LoadObj()
        {
            BarcodeSettingDataDoc m_Doc = new BarcodeSettingDataDoc();
            try
            {
                if (File.Exists(@".//Parameter/BarcodeData" + ".xml"))
                {
                    using (FileStream file = File.OpenRead(@".//Parameter/BarcodeData" + ".xml"))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(BarcodeSettingDataDoc));
                        m_Doc = (BarcodeSettingDataDoc)xml.Deserialize(file);
                        m_Doc.barcodeDataDic = m_Doc.barcodeData.ToDictionary(m => m.StationName);
                        return m_Doc;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return m_Doc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            using (FileStream fileStream = new FileStream(@".//Parameter/BarcodeData" + ".xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(BarcodeSettingDataDoc));
                xml.Serialize(fileStream, this);
                return true;
            }
        }
    }
}
