
using ControlPlatformLib;
using FullyAutomaticLaserJetCoder.CCD;
using Newtonsoft.Json;
/**
* 命名空间:  FullyAutomaticLaserJetCoder.MainTask
* 功 能   ： N/A
* 类 名   ： MarkTask
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-24 16:27:37
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
using System.Threading;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    //打标任务
    public class MarkTask : TaskUnit
    {
        private object lockObj = new object();
        private CCDStationA ccdStationA;
        private int iMarkPosition;
        private string[] arrBStation;
        private string[] arrCStation;
        private Queue<NGPositionItem> ngItemQueue;
        NGPositionItem ngItem;
        public enum flowChar
        {
            任务开始 = 0,
            工装板顶升上升 ,
            工装板顶升下降,
            Y轴前模组定位伸出,
            Y轴前模组定位缩回,
            Y轴后模组上伸出,
            Y轴后模组上缩回,
            铜嘴压板上升,
            铜嘴压板下降,
            工装板阻挡上升,
            工装板阻挡下降,

            Y轴前模组顶升上,
            Y轴前模组顶升下,
            X轴右定位伸出,
            X轴右定位缩回,
            定位板伸出,
            定位板缩回,
            Y轴后模组升降上,
            Y轴后模组升降下,

            X轴左定位伸出,
            X轴左定位缩回,
            防滑出工位板上,
            防滑出工位板下,
            风阀打开,
            风阀关闭,
            Z轴挡板伸出,
            Z轴挡板缩回,
            测台阶上升,
            测台阶下降,

            完成,
            报警,
        }
        public MarkTask(string name, TaskGroup taskGroup) : base(name, taskGroup)
        {
            ngItemQueue = new Queue<NGPositionItem>();
        }

        public override void Process()
        {
            try
            {
                lock (lockObj)
                {

                    bool bAutoTrag = false;
                    bool bManualTrag = false;
                    bool bTragCondition = false;
                    if (taskInfo.bTaskAlarm)
                    {
                        if (MainModule.FormMain.bResetPress)
                        {
                            taskInfo.bTaskAlarm = false;
                            Thread.Sleep(10);
                            m_taskTime.Start();
                        }
                        return;
                    }
                    if (!MainModule.FormMain.bAuto)
                        return;
                    bTragCondition = true;
                    bAutoTrag = MainModule.FormMain.bAuto && (!taskInfo.bTaskFinish) && (!taskInfo.bTaskOnGoing);
                    bManualTrag = m_manualStart;
                    switch (taskInfo.iTaskStep)
                    {
                        case (int)flowChar.任务开始://任务开始
                            if ((bAutoTrag | bManualTrag) && bTragCondition)
                            {
                                m_taskTime.Start();
                                m_taskGroup.AddRunMessage("打标任务0，任务开始。");
                                taskInfo.bTaskOnGoing = true;
                                //taskInfo.iTaskStep = 100;
                                taskInfo.iTaskStep = (int)flowChar.工装板顶升上升;
                            }
                            break;
                        case (int)flowChar.工装板顶升上升:
                           
                            break;
                        case 200:
                            //TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //           TableManage.TablePosItem("打标平台", "激光等待位").dPosX,
                            //           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //               TableManage.TablePosItem("打标平台", "激光等待位").dPosY,
                            //               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //m_taskTime.Start();
                            //taskInfo.iTaskStep = 300;
                        
                            break;
                        case 300:
                            //if (m_taskTime.TimeUp(30))
                            //{
                            //    taskInfo.bTaskAlarm = true;
                            //    m_taskGroup.AddAlarmMessage("打标任务300报警，打标平台走等待位超时！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 300;
                            //}
                            //else
                            //{
                            //    if (TableManage.TableDriver("打标平台").MoveDone(TableAxisName.X)
                            //        && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                            //    {
                            //        //Program.bCanTransfer = true;
                            //        m_taskGroup.AddRunMessage("打标任务300，打标平台走到等待位。");
                            //        m_taskTime.Start();
                            //        taskInfo.iTaskStep = 400;
                            //    }
                            //}
                            break;
                        case 400:
                            //if (Program.bFeederOK
                            //    && IOManage.INPUT("上料托盘有料感应器").On
                            //    && IOManage.INPUT("上料托盘到位感应器").On
                            //    && TableManage.TableDriver("移载喷码平台").CurrentY < 200)
                            //{
                            //    //Program.bLaserNG = false;
                            //    Program.bFeederOK = false;
                            //    m_taskGroup.AddRunMessage("打标任务400，触发CCD拍照。");
                            //    if (!Properties.Settings.Default.IngnoreCCDA)
                            //    {
                            //        //触发CCD拍照
                            //        string rec = CCD.CCDClientManage.ccdClient.Send("A\r\n");//工位1拍照触发
                            //        if (string.IsNullOrEmpty(rec))
                            //        {
                            //            m_taskGroup.AddAlarmMessage("打标任务400，CCD拍照返回结果异常！");
                            //            taskInfo.iTaskStep = 8888;
                            //            m_taskTime.Start();
                            //        }
                            //        else
                            //        {
                            //            m_taskGroup.AddRunMessage(string.Format("打标任务400，接收到CCD返回结果：{0}！", rec));
                            //            try
                            //            {
                            //                ccdStationA = JsonConvert.DeserializeObject<CCDStationA>(rec);
                            //            }
                            //            catch (Exception)
                            //            {
                            //                ccdStationA = new CCDStationA();
                            //                ccdStationA.Result = "NG";
                            //            }
                            //            if (ccdStationA == null || ccdStationA.Order != "A")
                            //            {
                            //                m_taskGroup.AddAlarmMessage("打标任务400，CCD拍照返回JSON字符串异常！" + rec);
                            //                taskInfo.iTaskStep = 8888;
                            //                m_taskTime.Start();
                            //            }
                            //            else
                            //            {
                            //                taskInfo.iTaskStep = 500;
                            //                m_taskTime.Start();
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        m_taskGroup.AddRunMessage("打标任务400，屏蔽粗定位CCD拍照。");
                            //        ccdStationA = new CCDStationA();
                            //        Program.ccdStationA.Enqueue(ccdStationA);
                            //        ccdStationA.NGPosition.Add(new NGPositionItem() { ID = "1", X = "0", Y = "0", U = "0" });
                            //        taskInfo.iTaskStep = 600;
                            //        m_taskTime.Start();
                            //    }
                            //}
                            break;
                        case 500:
                            //if (m_taskTime.TimeUp(3))
                            //{
                            //    taskInfo.bTaskAlarm = true;
                            //    m_taskGroup.AddAlarmMessage("打标任务500，CCD拍照超时！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 8888;
                            //}
                            //else
                            //{
                            //    if (ccdStationA.Result == "OK" ? true : false)//CCD拍照结果 true or false
                            //    {
                            //        Program.ccdStationA.Enqueue(ccdStationA);
                            //        //获取NG位置
                            //        //Program.bNG = false;
                            //        //Program.bLaserNG = false;
                            //        m_taskGroup.AddRunMessage("打标任务500，CCD拍照OK！");
                            //        m_taskTime.Start();
                            //        taskInfo.iTaskStep = 510;
                            //    }
                            //    else
                            //    {
                            //        //Program.bNG = true;
                            //        Program.bLaserNG = true;
                            //        iMarkPosition = 0;
                            //        m_taskGroup.AddRunMessage("打标任务500，CCD拍照NG！");
                            //        m_taskTime.Start();
                            //        taskInfo.iTaskStep = 8888;
                            //    }
                            //}
                            break;
                            //动作
                        //case 510:
                        //    if (ccdStationA.NGPosition.Count > 0)
                        //    {
                        //        ngItemQueue.Clear();
                        //        //精定位拍照点
                        //        if (Properties.Settings.Default.Symmetrical)
                        //        {
                        //            if (Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) < TableManage.TablePosItem("打标平台", "对称点").dPosX)
                        //            {
                        //                //精定位拍照点
                        //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                        //                              Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd,
                        //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                        //                m_taskGroup.AddRunMessage("打标任务510，对称板子正常面，X轴走激光打标精定位检测位！");
                        //                m_taskTime.Start();
                        //                taskInfo.iTaskStep = 511;
                        //            }
                        //            break;
                        //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                        //                                   Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd,
                        //                                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                        //            }
                        //            else
                        //            {
                        //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                        //                              Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd + Properties.Settings.Default.LaserRangeX,
                        //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                        //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                        //                                   Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd + Properties.Settings.Default.LaserRangeY,
                        //                                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            //精定位拍照点
                        //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                        //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd,
                        //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                        //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                        //                               Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd,
                        //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                        //        }
                        //        m_taskGroup.AddRunMessage("打标任务510，走激光打标精定位检测位！");
                        //        m_taskTime.Start();
                        //        taskInfo.iTaskStep = 520;
                        //    }
                        //    else
                        //    {
                        //        Program.bNG = false;
                        //        m_taskGroup.AddRunMessage("打标任务510，无标记X点位！");
                        //        m_taskTime.Start();
                        //        taskInfo.iTaskStep = 900;
                        //    }
                        //    break;
                        //case 515:
                        //    if (TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                        //    {
                        //        TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                        //                              Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd,
                        //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                        //        m_taskGroup.AddRunMessage("打标任务510，走激光打标精定位检测位！");
                        //        m_taskTime.Start();
                        //        taskInfo.iTaskStep = 520;
                        //    }
                        //    break;
                        case 510:
                            //if (ccdStationA.NGPosition.Count > 0)
                            //{
                            //    ngItemQueue.Clear();
                            //    if (Properties.Settings.Default.Symmetrical)
                            //    {
                            //        if (Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) < TableManage.TablePosItem("打标平台", "对称点").dPosX)
                            //        {
                            //            //精定位拍照点
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                               Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd,
                            //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //            //m_taskGroup.AddRunMessage("打标任务510，不对称板子，走激光打标精定位检测位！");
                            //            //m_taskTime.Start();
                            //            //taskInfo.iTaskStep = 520;
                            //        }
                            //        else
                            //        {
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd + Properties.Settings.Default.LaserRangeX,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                               Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd + Properties.Settings.Default.LaserRangeY,
                            //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //            //m_taskGroup.AddRunMessage("打标任务510，对称板子，走激光打标精定位检测位！");
                            //            //m_taskTime.Start();
                            //            //taskInfo.iTaskStep = 520;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        //精定位拍照点
                            //        TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                      Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd,
                            //                       TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //        TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                           Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //    }
                            //    m_taskGroup.AddRunMessage("打标任务510，走激光打标精定位检测位！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 520;
                            //}
                            //else
                            //{
                            //    //Program.bNG = false;
                            //    Program.bLaserNG = false;
                            //    m_taskGroup.AddRunMessage("打标任务510，无标记X点位！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 900;
                            //}
                            break;
                        case 520:
                            //if (m_taskTime.TimeUp(10))
                            //{
                            //    taskInfo.bTaskAlarm = true;
                            //    m_taskGroup.AddAlarmMessage(string.Format("打标任务520，走精定位超时！"));
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 8888;
                            //}
                            //else
                            //{
                            //    if (TableManage.TableDriver("打标平台").MoveDone(TableAxisName.X)
                            //        && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                            //    {
                            //        taskInfo.iTaskStep = 525;
                            //        m_taskGroup.AddRunMessage("打标任务520，走到精定位，等待触发D拍照");
                            //        m_taskTime.Start();
                            //    }
                            //}
                            break;
                        case 525:
                            //if (m_taskTime.TimeUp(0.01))//拍照延迟
                            //{
                            //    string rec = "";
                            //    if (!Properties.Settings.Default.IgnoreCCD)
                            //    {
                            //        m_taskGroup.AddRunMessage("打标任务520，触发D拍照");
                            //        //B,OK,0.12,-0.23,0.02
                            //        rec = CCD.CCDClientManage.ccdClient.Send("D\r\n");//工位1拍照触发
                            //    }
                            //    else
                            //    {
                            //        rec = "D,OK,0,0";
                            //    }
                            //    m_taskGroup.AddRunMessage(string.Format("打标任务520，接收到D工位CCD返回结果：{0}！", rec));
                            //    if (rec.Contains(","))
                            //    {
                            //        arrBStation = rec.Split(',');
                            //        if (arrBStation.Length != 4)
                            //        {
                            //            m_taskGroup.AddAlarmMessage("打标任务520，D工位CCD拍照返回字符串长度错误！");
                            //            taskInfo.iTaskStep = 8888;
                            //            m_taskTime.Start();
                            //        }
                            //        else
                            //        {
                            //            m_taskGroup.AddRunMessage("打标任务520，接收到D工位CCD返回结果OK！");
                            //            taskInfo.iTaskStep = 530;
                            //            m_taskTime.Start();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        m_taskGroup.AddAlarmMessage("打标任务520，D工位CCD拍照返回字符串异常！");
                            //        taskInfo.iTaskStep = 8888;
                            //        m_taskTime.Start();
                            //    }
                            //}
                            //else
                            //{
                            //    taskInfo.iTaskStep = 525;
                            //}
                            break;
                        case 530:
                            //if (m_taskTime.TimeUp(5))
                            //{
                            //    taskInfo.bTaskAlarm = true;
                            //    m_taskGroup.AddAlarmMessage("打标任务530，精定位CCD拍照超时！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 8888;
                            //}
                            //else
                            //{
                            //    if (arrBStation[1] != "OK")
                            //    {
                            //        m_taskGroup.AddAlarmMessage("打标任务530，D工位CCD拍照失败！");
                            //        taskInfo.iTaskStep = 8888;
                            //        m_taskTime.Start();
                            //    }
                            //    else
                            //    {
                            //        double i = 0.0;
                            //        double j = 0.0;
                            //        if (!double.TryParse(arrBStation[2], out Program.dLaserToMarkX))//out i
                            //        {
                            //            m_taskGroup.AddAlarmMessage("打标任务530，D工位返回参数X转换失败！");
                            //            taskInfo.iTaskStep = 8888;
                            //            m_taskTime.Start();
                            //        }
                            //        if (!double.TryParse(arrBStation[3], out Program.dLaserToMarkY))//out j
                            //        {
                            //            m_taskGroup.AddAlarmMessage("打标任务530，D工位返回参数Y转换失败！");
                            //            taskInfo.iTaskStep = 8888;
                            //            m_taskTime.Start();
                            //        }
                            //        //NGPositionItem item = new NGPositionItem();
                            //        //item.X = i.ToString();
                            //        //item.Y = j.ToString();
                            //        //ngItemQueue.Enqueue(item);
                            //        //拍照点+坐标-标定点(拍照点 = 粗定位给的坐标 - 相机的距离）
                            //        m_taskGroup.AddRunMessage("打标任务530，走打标点,收到i:" + i.ToString() + ",j:" + j.ToString());
                            //        //if (Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosY + Program.dLaserToMarkY + Properties.Settings.Default.LaserOffsetY < TableManage.TablePosItem("打标平台", "对称点").dPosY)
                            //        if (Properties.Settings.Default.Symmetrical)
                            //        {
                            //            if (Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) < TableManage.TablePosItem("打标平台", "对称点").dPosY)
                            //            {
                            //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosX + Program.dLaserToMarkX + Properties.Settings.Default.LaserOffsetX,//i,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                                   Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosY + Program.dLaserToMarkY + Properties.Settings.Default.LaserOffsetY,//j,
                            //                                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //            }
                            //            else
                            //            {
                            //                //如果板子是对称的话，加上偏移量
                            //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosX + Program.dLaserToMarkX + Properties.Settings.Default.LaserRangeX,//i,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                                   Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosY + Program.dLaserToMarkY + Properties.Settings.Default.LaserOffsetY + Properties.Settings.Default.LaserRangeY,//j,
                            //                                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //            }
                            //        }
                            //        else
                            //        {
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                          Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X) - Properties.Settings.Default.XLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosX + Program.dLaserToMarkX + Properties.Settings.Default.LaserOffsetX,//i,
                            //                           TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                               Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y) - Properties.Settings.Default.YLaserToDccd - TableManage.TablePosItem("打标平台", "精定位标定点").dPosY + Program.dLaserToMarkY + Properties.Settings.Default.LaserOffsetY,//j,
                            //                               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //        }
                            //        m_taskTime.Start();
                            //        taskInfo.iTaskStep = 700;
                            //    }
                            //}
                            break;
                        case 600:
                            //if (ccdStationA.NGPosition.Count > 0)
                            //{
                            //    ngItemQueue.Clear();
                            //    TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //                   TableManage.TablePosItem("打标平台", "激光打标位").dPosX,
                            //                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //    TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //                       TableManage.TablePosItem("打标平台", "激光打标位").dPosY,
                            //                       TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);

                            //    //TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                            //    //              Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].X),
                            //    //               TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            //    //TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                            //    //                   Convert.ToDouble(ccdStationA.NGPosition[iMarkPosition].Y),
                            //    //                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //    //m_taskGroup.AddRunMessage("打标任务600，走激光打标精定位检测位！");
                            //    //m_taskTime.Start();
                            //    //taskInfo.iTaskStep = 700;
                            //}
                            //else
                            //{
                            //    Program.bNG = false;
                            //    m_taskGroup.AddRunMessage("打标任务600，无标记X点位！");
                            //    m_taskTime.Start();
                            //    taskInfo.iTaskStep = 900;
                            //}
                            break;
                        case 700:
                            if (m_taskTime.TimeUp(30))
                            {
                                taskInfo.bTaskAlarm = true;
                                m_taskGroup.AddAlarmMessage("打标任务700，走打标位超时！");
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 8888;
                            }
                            else
                            {
                                if (TableManage.TableDriver("打标平台").MoveDone(TableAxisName.X)
                                    && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                                {
                                    m_taskGroup.AddRunMessage("打标任务700，走到激光打标位！");
                                    IOManage.OUTPUT("打标启动").SetOutBit(true);
                                    m_taskTime.Start();
                                    taskInfo.iTaskStep = 800;
                                }
                            }
                            break;
                        case 800:
                            if (m_taskTime.TimeUp(10))
                            {
                                taskInfo.bTaskAlarm = true;
                                m_taskGroup.AddAlarmMessage("打标任务800，打标超时！");
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 8888;
                            }
                            else
                            {
                                if (IOManage.INPUT("打标完成").On)
                                {
                                    m_taskGroup.AddRunMessage("打标任务800，打标完成！");
                                    IOManage.OUTPUT("打标启动").SetOutBit(false);
                                    m_taskTime.Start();
                                    taskInfo.iTaskStep = 900;
                                }
                            }
                            break;
                        case 900:
                            if (ccdStationA.NGPosition.Count > iMarkPosition + 1)
                            {
                                m_taskGroup.AddRunMessage("打标任务900，还剩余NG打标位置，继续打标循环！");
                                iMarkPosition++;
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 510;
                            }
                            else
                            {
                                m_taskGroup.AddRunMessage("打标任务900，无剩余NG打标位置，打标循环完成！");
                                //IOManage.OUTPUT("上料托盘左右电磁阀").SetOutBit(false);
                                //IOManage.OUTPUT("上料托盘前后电磁阀").SetOutBit(false);
                                IOManage.OUTPUT("上料托盘电磁阀").SetOutBit(false);
                                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                                       TableManage.TablePosItem("打标平台", "激光等待位").dPosX,
                                       TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                                TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                                       TableManage.TablePosItem("打标平台", "激光等待位").dPosY,
                                       TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                                iMarkPosition = 0;
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 1000;
                            }
                            break;
                        case 1000:
                            if (m_taskTime.TimeUp(10))
                            {
                                taskInfo.bTaskAlarm = true;
                                m_taskGroup.AddAlarmMessage("打标任务1000，上料托盘前后左右气缸缩回超时！");
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 1000;
                            }
                            else
                            {
                                //if (IOManage.INPUT("上料托盘左右气缸原位").On
                                //    && IOManage.INPUT("上料托盘前后气缸原位").On)
                                if(IOManage.OUTPUT("上料托盘电磁阀").GetOff() && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.X) && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                                {
                                    //IOManage.OUTPUT("上料托盘电磁阀").SetOutBit(false);
                                    Program.bMarkOK = true;
                                    m_taskGroup.AddRunMessage("打标任务1000，上料托盘前后左右气缸缩回到位！");
                                    Program.bLaserNG = false;
                                    m_taskTime.Start();
                                    taskInfo.iTaskStep = 200;
                                }
                            }
                            break;
                        case 8888:
                            IOManage.OUTPUT("上料托盘电磁阀").SetOutBit(false);
                            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.X,
                                       TableManage.TablePosItem("打标平台", "激光等待位").dPosX,
                                       TableManage.tablesDoc.m_tableDictionary["打标平台"].axisXData.dSpeed);
                            TableManage.TableDriver("打标平台").AbsMove(TableAxisName.Y,
                                   TableManage.TablePosItem("打标平台", "激光等待位").dPosY,
                                   TableManage.tablesDoc.m_tableDictionary["打标平台"].axisYData.dSpeed);
                            //Program.bMarkOK = true;
                            m_taskGroup.AddRunMessage("打标任务8888！");
                            m_taskTime.Start();
                            taskInfo.iTaskStep = 9999;
                            break;
                        case 9999:
                            if (IOManage.OUTPUT("上料托盘电磁阀").GetOff() && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.X) && TableManage.TableDriver("打标平台").MoveDone(TableAxisName.Y))
                            {
                                //IOManage.OUTPUT("上料托盘电磁阀").SetOutBit(false);
                                Program.bMarkOK = true;
                                Program.bLaserNG = true;
                                //Program.bNG = true;
                                m_taskGroup.AddRunMessage("打标任务1000，上料托盘前后左右气缸缩回到位！");
                                m_taskTime.Start();
                                taskInfo.iTaskStep = 100;
                            }
                                break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                m_taskGroup.AddAlarmMessage(string.Format("执行打标流程{0}时出现错误！错误信息：{1}", taskInfo.iTaskStep, ex.Message));
            }
        }
        private void MappingPos1(double x, double y, ref double mappingX, ref double mappingY)
        {
            double x1 = Properties.Settings.Default.MappingA * x
                + Properties.Settings.Default.MappingB * y
                + Properties.Settings.Default.MappingTx;
            mappingX = x1;
            double y1 = Properties.Settings.Default.MappingC * x
                + Properties.Settings.Default.MappingD * y
                + Properties.Settings.Default.MappingTy;
            mappingY = y1;

            m_taskGroup.AddRunMessage(string.Format("映射拍照位X:{0} Y:{1}！", mappingX, mappingY));
        }
    }
}
