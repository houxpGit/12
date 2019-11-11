/**
* 命名空间:  FullyAutomaticLaserJetCoder.CCD
* 功 能   ： N/A
* 类 名   ： CCDStationA
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-18 16:38:28
* 负责人  :  wuchenjie 
* 变更内容:
* Copyright (c) 2018 Sunwoda Corporation. All rights reserved.
*┌───────────────────────────────┐
*│此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露│
*│版权所有：欣旺达电气技术有限公司 　　　　　　　　　　　　　　 │
*└───────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullyAutomaticLaserJetCoder.CCD
{
    public class CCDStationA
    {
        /// <summary>
        /// 指令
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// NG位置
        /// </summary>
        public List<NGPositionItem> NGPosition { get; set; }

        public CCDStationA()
        {
            NGPosition = new List<NGPositionItem>();
        }
    }

    public class NGPositionItem
    {
        /// <summary>
        /// 位置编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// X位置
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// Y位置
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// U位置（R角）
        /// </summary>
        public string U { get; set; }
    }
}
