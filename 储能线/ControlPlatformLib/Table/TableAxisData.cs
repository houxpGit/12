using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    ///<summary>
    ///名称：单轴数据
    ///作用：保存单轴数据
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：用于保存单轴数据
    ///</summary>
    public class TableAxisData
    {
        //string strAxisName = "";
        /// <summary>
        /// 硬件名称
        /// </summary>
        public string MotionCardName = "";
        /// <summary>
        /// 轴号
        /// </summary>
        public short AxisNo;
        /// <summary>
        /// 限位逻辑
        /// </summary>
        public SenserLogic limtLogic;
        /// <summary>
        /// 原点逻辑
        /// </summary>
        public SenserLogic orgLogic;
        /// <summary>
        /// 近原点逻辑
        /// </summary>
        public SenserLogic orgNearLogic;
        /// <summary>
        /// 报警逻辑
        /// </summary>
        public SenserLogic alarmLogic;
        /// <summary>
        /// 脉冲模式
        /// </summary>
        public PulseMode pulseMode;
        /// <summary>
        /// 轴细分（1毫米对应脉冲数）
        /// </summary>
        public double plusToMM;
        /// <summary>
        /// 是否使用
        /// </summary>
        public int iUsed;
        /// <summary>
        /// 加速度
        /// </summary>
        public double dAcc;
        /// <summary>
        /// 减速度
        /// </summary>
        public double dDec;
        /// <summary>
        /// 速度
        /// </summary>
        public double dSpeed;
        /// <summary>
        /// 点动低速
        /// </summary>
        public double dJobLow;
        /// <summary>
        /// 点动高速
        /// </summary>
        public double dJobHigh;
        /// <summary>
        /// 搜极限速度
        /// </summary>
        public double dLimtSpd;
        /// <summary>
        /// 搜原点速度
        /// </summary>
        public double dOrgSpd;
        /// <summary>
        /// 回原模式
        /// </summary>
        public int iOrgMode;
        /// <summary>
        /// 脉冲模式
        /// </summary>
        public int iPlusMode;
        /// <summary>
        /// 是否使用配置文件
        /// </summary>
        public bool bUsedConfig;
        /// <summary>
        /// 坐标系编号
        /// </summary>
        public int iCorNo;
        /// <summary>
        /// 轴别名
        /// </summary>
        public string alias;
        public TableAxisData()
        {
            MotionCardName = "";
            AxisNo = 0;
            limtLogic = SenserLogic.NC;
            orgLogic = SenserLogic.NC;
            orgNearLogic = SenserLogic.NC;
            plusToMM = 0.001;
            iUsed = 0;
            dAcc = 1.0;
            dDec = 1.0;
            dSpeed = 1.0;
            dJobLow = 1.0;
            dJobHigh = 1.0;
            dLimtSpd = 1.0;
            dOrgSpd = 1.0;
            iCorNo = 1;
            alias = "";
        }
    }
}
