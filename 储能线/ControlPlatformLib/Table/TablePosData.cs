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
    ///<summary>
    ///名称：平台点位数据
    ///作用：保存平台点位数据
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：。
    ///</summary>
    public class TablePosData
    {
        public List<TablePosItem> tablePosItemList;
        [XmlIgnore]
        public Dictionary<string, TablePosItem> tablePosItemDictionary;
        public TablePosData()
        {
            tablePosItemList = new List<TablePosItem>();
            tablePosItemDictionary = new Dictionary<string, TablePosItem>();
        }
    }
}
