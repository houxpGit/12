using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    ///<summary>
    ///名称：平台轴状态类
    ///作用：存储轴状态
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：。
    ///</summary>
    public class TablePreStatus
    {
        /// <summary>
        /// X轴报警状态指示
        /// </summary>
        public bool bAlarmX;
        /// <summary>
        /// Y轴报警状态指示
        /// </summary>
        public bool bAlarmY;
        /// <summary>
        /// Z轴报警状态指示
        /// </summary>
        public bool bAlarmZ;
        /// <summary>
        /// U轴报警状态指示
        /// </summary>
        public bool bAlarmU;
        /// <summary>
        /// X轴正限位状态指示
        /// </summary>
        public bool bCWLX;
        /// <summary>
        /// Y轴正限位状态指示
        /// </summary>
        public bool bCWLY;
        /// <summary>
        /// Z轴正限位状态指示
        /// </summary>
        public bool bCWLZ;
        /// <summary>
        /// U轴正限位状态指示
        /// </summary>
        public bool bCWLU;
        /// <summary>
        /// X轴负限位状态指示
        /// </summary>
        public bool bCCWLX;
        /// <summary>
        /// Y轴负限位状态指示
        /// </summary>
        public bool bCCWLY;
        /// <summary>
        /// Z轴负限位状态指示
        /// </summary>
        public bool bCCWLZ;
        /// <summary>
        /// U轴负限位状态指示
        /// </summary>
        public bool bCCWLU;
        public TablePreStatus()
        {

        }

    }
}
