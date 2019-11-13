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
    [XmlInclude(typeof(ProjectDataS)), XmlInclude(typeof(ProjectDataBase))]
    public class ProjectDataDoc
    {
        public List<ProjectDataS> m_dataList;
        [XmlIgnore]
        public Dictionary<string, ProjectDataS> m_dataDictionary;

        public ProjectDataDoc()
        {

            m_dataList = new List<ProjectDataS>();
            m_dataDictionary = new Dictionary<string, ProjectDataS>();
        }
        public static ProjectDataDoc LoadObj()
        {
            ProjectDataDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectDataDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/ProjectDataDoc" + ".xml");
                pDoc = (ProjectDataDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_dataDictionary = pDoc.m_dataList.ToDictionary(p => p.strGroupName);
                foreach (ProjectDataS item in pDoc.m_dataList)
                {
                    item.m_dataDictionary = item.m_dataList.ToDictionary(p => p.strName);
                }



            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new ProjectDataDoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/ProjectDataDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectDataDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
