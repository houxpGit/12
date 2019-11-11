
using ControlPlatformLib;
/**
* 命名空间:  FullyAutomaticLaserJetCoder.CCD
* 功 能   ： N/A
* 类 名   ： CCDClientManage
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-18 16:42:15
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
    public class CCDClientManage
    {
        public static CCDClient ccdClient;
        public static CCDCommunication ccdDoc;

        public static void Load()
        {
            ccdDoc = CCDCommunication.LoadDocument();
            ccdClient = new CCDClient(ccdDoc);
            ccdClient.C1TaskEvent += CcdClient_C1TaskEvent;
            ccdClient.C2TaskEvent += CcdClient_C2TaskEvent;
            ccdClient.C3TaskEvent += CcdClient_C3TaskEvent;
        }
        private static void CcdClient_C3TaskEvent()
        {
            string ret = "C3";
            for (int i = 1; i <= 9; i++)
            {
                ret += ",";
                ret += TableManage.TablePosItem("打标平台", "精定位" + i).dPosX;
                ret += ",";
                ret += TableManage.TablePosItem("打标平台", "精定位" + i).dPosY;
            }
            ret += "\r\n";
            ccdClient.Send(ret);
        }

        private static void CcdClient_C2TaskEvent()
        {
            string ret = "C2";
            for (int i = 1; i <= 9; i++)
            {
                ret += ",";
                ret += TableManage.TablePosItem("移载喷码平台", "喷码标定位" + i).dPosX;
                ret += ",";
                ret += TableManage.TablePosItem("移载喷码平台", "喷码标定位" + i).dPosU;
            }
            ret += "\r\n";
            ccdClient.Send(ret);
        }

        private static void CcdClient_C1TaskEvent()
        {
            //C1,0.0,0.1,0.2,03
            string ret = "C1";
            for (int i = 1; i <= 9; i++)
            {
                ret += ",";
                ret += TableManage.TablePosItem("打标平台", "上料托盘标定位"+i).dPosX;
                ret += ",";
                ret+= TableManage.TablePosItem("打标平台", "上料托盘标定位"+i).dPosY;
            }
            ret += "\r\n";
            ccdClient.Send(ret);
        }
    }
}
