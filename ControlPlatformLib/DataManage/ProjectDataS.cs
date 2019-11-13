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
    public class ProjectDataS
    {
        public string strGroupName;
        public string strRemark;
        public List<ProjectDataBase> m_dataList;
        [XmlIgnore]
        public Dictionary<string, ProjectDataBase> m_dataDictionary;
        public ProjectDataS()
        {
            strRemark = "";
            m_dataList = new List<ProjectDataBase>();
            m_dataDictionary = new Dictionary<string, ProjectDataBase>();
        }
    }
}
