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
    public class ProjectDataBase
    {
        public string strName;
        public ProjectDataType dataType;
        public object objValue;
        public string strRemark;
        public ProjectDataBase()
        {
            dataType = ProjectDataType.STRING;
            strRemark = "";
        }
    }
}
