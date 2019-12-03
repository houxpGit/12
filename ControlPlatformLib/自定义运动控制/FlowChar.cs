using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControlPlatformLib
{
    [XmlInclude(typeof(FlowChar))]
    public class FlowChar
    {
        public string strName { get; set; }
        public string Current_X { get; set; }
        public string Current_Y { get; set; }
        public string Current_Z { get; set; }
        public string Current_U { get; set; }
        public string 插补轴号 { get; set; }
        public string 获取位置 { get; set; }
        public string AsixGo { get; set; }
        public string IO检测状态 { get; set; }
        public string IO输出状态 { get; set; }
        public string IO运动 { get; set; }
        public string ioColumn { get; set; }  
        public string ioColumn1 { get; set; }
    }
}
