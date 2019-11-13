/**
* 命名空间:  ControlPlatformLib.CommonTools
* 功 能   ： N/A
* 类 名   ： LimitedQueue
* Ver     :  ver1.0.0.0
* 变更日期:  2018-12-29 16:26:14
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

namespace ControlPlatformLib.CommonTools
{
    /// <summary>
    /// 定长队列
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class LimitedQueue<T> : Queue<T>
    {
        private int limit = -1;
        /// <summary>
        /// 队列最大长度限制
        /// </summary>
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="limit">队列最大长度限制</param>
        public LimitedQueue(int limit)
            : base(limit)
        {
            this.Limit = limit;
        }
        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="item">对象</param>
        public new void Enqueue(T item)
        {
            if (this.Count >= this.Limit)
            {
                this.Dequeue();
            }
            base.Enqueue(item);
        }
    }
}
