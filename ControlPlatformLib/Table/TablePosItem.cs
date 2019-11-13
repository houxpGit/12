using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public enum WeldingType
    {
        [Description("焊2点")]
        焊2点 = 0,
        [Description("焊4点")]
        焊4点 = 1,
        [Description("焊4点，左工位不焊")]
        焊4点左工位不焊 = 2,
        [Description("焊4点，右工位不焊")]
        焊4点右工位不焊 = 3,
        [Description("左右都不焊")]
        左右都不焊 = 4
    }
    ///<summary>
    ///名称：单点数据
    ///作用：单点数据类
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：单点数据，默认单点数据为绝对位置。
    ///</summary>
    public class TablePosItem
    {
        //public string strName;
        /// <summary>
        /// 点位名称
        /// </summary>
        public string strName { get; set; }
        /// <summary>
        /// X轴位置
        /// </summary>
        public double dPosX;
        /// <summary>
        /// Y轴位置
        /// </summary>
        public double dPosY;
        /// <summary>
        /// Z轴位置
        /// </summary>
        public double dPosZ;
        /// <summary>
        /// U轴位置
        /// </summary>
        public double dPosU;
        /// <summary>
        /// 是否使用X轴
        /// </summary>
        public bool bActionX;
        /// <summary>
        /// 是否使用Y轴
        /// </summary>
        public bool bActionY;
        /// <summary>
        /// 是否使用Z轴
        /// </summary>
        public bool bActionZ;
        /// <summary>
        /// 是否使用U轴
        /// </summary>
        public bool bActionU;
        /// <summary>
        /// 是否为相对运动，默认为绝对位置，若要设置相对位置，则将bRel设置为TRUE
        /// </summary>
        public bool bRel;
        /// <summary>
        /// 焊接类型
        /// </summary>
        public WeldingType WeldingType { get; set; }
        /// <summary>
        /// 焊接功率
        /// </summary>
        public string WeldingPower { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1},{2}",strName,dPosX,dPosY) ;
        }

        public TablePosItem()
        {
            dPosX = 0.0;
            dPosY = 0.0;
            dPosZ = 0.0;
            dPosU = 0.0;
            bActionX = false;
            bActionY = false;
            bActionZ = false;
            bActionU = false;
            bRel = false;
            WeldingType = WeldingType.焊4点;
            WeldingPower = "功率1";
        }

    }
}
