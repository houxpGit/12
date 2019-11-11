using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    ///<summary>
    ///名称：平台整合类
    ///作用：整合所有平台
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    /// 描述：。
    ///</summary>
    public class TableDrivers
    {
        public Dictionary<string, TableDriver> drivers;
        public TableDrivers()
        {
            drivers = new Dictionary<string, TableDriver>();
        }
    }
}
