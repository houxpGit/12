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
    [XmlInclude(typeof(InputData)), XmlInclude(typeof(OutputData))]
    public class IODoc
    {
        public List<InputData> m_InputDataList;
        [XmlIgnore]
        public Dictionary<string, InputData> m_InputDictionary;

        public List<OutputData> m_OutputDataList;
        [XmlIgnore]
        public Dictionary<string, OutputData> m_OutputDictionary;
        public IODoc()
        {

            m_InputDataList = new List<InputData>();
            m_InputDictionary = new Dictionary<string, InputData>();
            m_OutputDataList = new List<OutputData>();
            m_OutputDictionary = new Dictionary<string, OutputData>();
        }
        public static IODoc LoadObj()
        {
            IODoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IODoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/IODoc" + ".xml");
                pDoc = (IODoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_InputDictionary = pDoc.m_InputDataList.ToDictionary(p => p.strIOName);
                pDoc.m_OutputDictionary = pDoc.m_OutputDataList.ToDictionary(p => p.strIOName);


            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new IODoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/IODoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IODoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
