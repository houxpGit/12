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
    ///名称：平台设置数据
    ///作用：保存平台设置数据
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：每个平台有4个轴，分别为X、Y、Z、U四个轴。可以保存单轴数据及点位数据。
    ///</summary>
    [XmlInclude(typeof(TableData)), XmlInclude(typeof(TableAxisData)), XmlInclude(typeof(TablePosData)), XmlInclude(typeof(TablePosItem))]
    public class TableData
    {
        public string strTableName = "";
        public TableAxisData axisXData;
        public TableAxisData axisYData;
        public TableAxisData axisZData;
        public TableAxisData axisUData;
        public TablePosData tablePosData;
        public TableData()
        {
            axisXData = new TableAxisData();
            axisYData = new TableAxisData();
            axisZData = new TableAxisData();
            axisUData = new TableAxisData();
            tablePosData = new TablePosData();
        }
        public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            FormTableItem frm = new FormTableItem(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();

        }
    }
}
