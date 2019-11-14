/**
* 命名空间:  FullyAutomaticLaserJetCoder
* 功 能   ： N/A
* 类 名   ： TestDateCom
* Ver     :  ver1.0.0.0
* 变更日期:  2019-05-16 09:29:10
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
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using FullyAutomaticLaserJetCoder.MainTask;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace FullyAutomaticLaserJetCoder
{

    public partial class ProductionData
    {
        //  public List<ProductionData> m_ProductionData;
      
        string stfPath = System.Environment.CurrentDirectory + "\\Production\\ProductionData.xml";
        public string stfPathForder = System.Environment.CurrentDirectory;
        private static ProductionData ProductionD;
        public static ProductionData Instance()
        {
            if (ProductionD == null)
            {
                ProductionD = new ProductionData(0);
            }
            return ProductionD;
        }
        public ProductionData()
        {
            // CreatFolderNew(stfPathForder);
        }
        public void CreatFolderNew(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
        }

        public List<string> RunDocument = new List<string>();//运行文档列表
        public string ModelNo;//机种号
        public int OK_date;
        public int NG_date;
        public int CT_TIME;
        /// <summary>
        /// 清理铜嘴次数
        /// </summary>
        public int Clear_TIME;
        /// <summary>
        /// 清理铜嘴当前次数
        /// </summary>
        public int Current_TIME;
        /// <summary>
        /// 调高最小模拟量
        /// </summary>
        public double IncreaseMminimumAnalog;//调高最小模拟量      
        /// <summary>      
        /// 调高最大模拟量              
        /// </summary>
        public double IncreaseMaximumAnalog;//调高最大模拟量

        /// <summary>      
        /// Z轴最小坐标              
        /// </summary>
        public double Z_AxisMinimumCoordinate;

        /// <summary>      
        /// Z轴最大坐标              
        /// </summary>
        public double Z_AxisMaximumCoordinate;

        /// <summary>      
        /// 调高关联的数据              
        /// </summary>
        public double High_Date;
        /// <summary>      
        /// Z轴基准坐标             
        /// </summary>
        public double Z_AxialDatum;
        /// <summary>      
        /// 基准模拟量       
        /// </summary>
        public double BaselineSimulation;
        /// <summary>      
        /// X轴基准坐标             
        /// </summary>
        public double X_AxialDatum;

        /// <summary>      
        /// Y轴基准坐标             
        /// </summary>
        public double Y_AxialDatum;


        /// <summary>
        /// 当前点位
        /// </summary>
        public int TheCurrentpoint;
        /// <summary>
        /// 当前产量
        /// </summary>
        public int TheCurrentProduction;
        /// <summary>
        /// 焊接功率
        /// </summary>
        public int TheWeldingPower;
        /// <summary>
        /// 焊接速度
        /// </summary>
        public int TheWeldingSpeed;
        /// <summary>
        /// X偏距
        /// </summary>
        public double X_Setover;
        /// <summary>
        /// Y偏距
        /// </summary>
        /// 
        public double Y_Setover;

        /// <summary>
        /// X相机X位置
        /// </summary>
        public double X_Camera_Position;
        /// <summary>
        /// 相机Y位置
        /// </summary>
        public double Y_Camera_Position;
        /// <summary>
        /// 激光X位置
        /// </summary>
        public double X_Laser_Position;
        /// <summary>
        ///激光Y位置
        /// </summary>
        public double Y_Laser_Position;
        /// <summary>
        ///开始运行时间
        /// </summary>
       // DateTime starttime = DateTime.Now;
        public DateTime starttime;
        /// <summary>
        ///结束运行时间
        /// </summary>

        public DateTime endtime;

        /// <summary>
        ///结束CT时间
        /// </summary>

        public string CTtime="0.00";
        /// <summary>
        ///Z轴安全高度最高
        /// </summary>

        public int SaveHigh_Top ;
        /// <summary>
        ///Z轴安全高度最低:
        /// </summary>

        public int SaveHigh_Low ;
        
        /// <summary>
        ///调高最高::
        /// </summary>

        public int AutoZ_High_Top ;

        /// <summary>
        ///调高最低:
        /// </summary>

        public int AutoZ_High_Low ;
        public string SN;
        public bool LeftRun;
        public bool RightRun;

        /// <summary>
        ///空跑
        /// </summary>

        public bool  Empty_run=false;
        /// <summary>
        ///急停top
        /// </summary>

        public bool EStop;
        /// <summary>
        ///停止
        /// </summary>
        public bool IsStop;
        /// <summary>
        ///工位有无料标志位
        /// </summary>
        public bool StationMaterial ;//工位有无料标志位

        /// <summary>
        //焊接波形数据
        /// </summary>
        public List<float> WeldDate=new List<float> ();//工位有无料标志位

        /// <summary>
        //设备最大功率
        /// </summary>
        public int WeldPower;//工位有无料标志位
        /// <summary>
        //机台选择  通用与6KW
        /// </summary>
        public int WeldOther;//工位有无料标志位
        /// <summary>
        //Sn
        /// </summary>
        public string  DataReceivedstrSN;//工位有无料标志位
       /// <summary>
        //登录用户名
        /// </summary>
        public string MesUserCode;//工位有无料标志位
        /// <summary>
        //登录密码
        /// </summary>
        public string MesPassWord;//工位有无料标志位
        /// <summary>
        //设备编号
        /// </summary>
        public string MesdeviceCode;//工位有无料标志位
        public ProductionData(int value)
        {
            Date_Clear(value);
        }
        public void Date_Clear(int value)
        {
            OK_date = value;
            NG_date = value;
            TheCurrentpoint = value;
            TheCurrentProduction = value;
            CT_TIME = value;
            SN = "";
            CTtime = "0.00";
         //   Clear_TIME = value;
            Current_TIME = value; 
        }
        public void cleardate(int value)
        {
            OK_date = value;
            NG_date = value;
            TheCurrentpoint = value;
            TheCurrentProduction = value;
            TheWeldingPower = value;
            TheWeldingSpeed = value;
            SN = "";
        }
    
        //public string[] ReadXml(string filename, string[] strArrayData)//string[] strArrayRow)
        //{
        //    try
        //    {
        //        //<1>实例化一个XML文档操作对象.
        //        XmlDocument doc = new XmlDocument();
        //        //<2>使用XML对象加载XML.
        //        doc.Load(filename);
        //        //<3>获取根节点.
        //        XmlNode root = doc.SelectSingleNode("Production");
        //        //<4>获取根节点下所有子节点.
        //        XmlNodeList nodeList = root.ChildNodes;

        //        //<5>遍历输出.
        //        foreach (XmlNode node in nodeList)
        //        {
        //            for (int i = 0; i < node.ChildNodes.Count; i++)
        //            {
        //                strArrayData[i] = node.ChildNodes[i].InnerText;
        //            }
        //        }
        //        return strArrayData;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //        //throw;
        //    }
        //}
        //public void WriteXml(string[] strArrayColumn, string[] strArrayRow, string path)
        //{
        //    //创建一个数据集，将其写入xml文件
        //    //path = "ParameterData.xml";
        //    System.Data.DataSet ds = new System.Data.DataSet("Production");
        //    System.Data.DataTable table = new System.Data.DataTable("ProductionData");
        //    ds.Tables.Add(table);
        //    foreach (var item in strArrayColumn)
        //    {
        //        table.Columns.Add(item, typeof(string));
        //    }

        //    System.Data.DataRow row = table.NewRow();
        //    for (int i = 0; i < strArrayRow.Length; i++)
        //    {
        //        row[i] = strArrayRow[i];
        //    }
        //    ds.Tables["ProductionData"].Rows.Add(row);
        //    ds.WriteXml(path);
        //}
    }

    [XmlInclude(typeof(ProductionData))]
    public partial class DateSave
    {
        public ProductionData Production = ProductionData.Instance();
        public List<ProductionData> m_ProductionData;
        [XmlIgnore]
        public static Dictionary<string, ProductionData> m_ProductionDataDictionary = new Dictionary<string, ProductionData>();
        public  static DateSave DateSav;
        public static DateSave Instance()
        {
            if (DateSav == null)
            {
                DateSav = new DateSave();
            }
            return DateSav;
        }
        //public List<OutputData> m_OutputDataList;
        //[XmlIgnore]
        //public static Dictionary<string, OutputData> m_OutputDictionary = new Dictionary<string, OutputData>();

        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/ProductionData" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            // XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateSave));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateSave));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        public bool SaveDoc_Other(string path)
        {
            if (!Directory.Exists(@".//FlowDocument/"))
            {
                Directory.CreateDirectory(@".//FlowDocument/");
            }
            FileStream fsWriter = new FileStream(@".//FlowDocument//" + path + "/ProductionData" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            // XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateSave));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateSave));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        public  DateSave LoadObj()
        {
            DateSave pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateSave));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/ProductionData" + ".xml");
                pDoc = (DateSave)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
               
                pDoc = new DateSave();
              
               
            }
            return pDoc;
        }
        public DateSave()
        {

            m_ProductionData = new List<ProductionData>();
          //  m_ProductionDataDictionary = new Dictionary<string, ProductionData>();
            //m_OutputDataList = new List<OutputData>();
            //m_OutputDictionary = new Dictionary<string, OutputData>();
        }
    }

}
