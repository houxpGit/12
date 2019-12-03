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
using System.Text.RegularExpressions;

namespace ControlPlatformLib
{
    ///<summary>
    ///名称：平台数据存储类
    ///作用：将平台数据存储为XML格式文件
    ///作者：吴陈杰
    ///编写日期：2018-10-11
    ///更新版本日期：2018-10-11
    ///版本：1.0.0.1
    ///描述：通过XML序列化，将平台数据存储为XML格式文件。
    ///</summary>
    public class TablesDoc
    {
        public List<TableData> m_tableDataList;
        [XmlIgnore]
        public Dictionary<string, TableData> m_tableDictionary;
        public TablesDoc()
        {
            //if (m_tableDataList!=null)
            //{
                m_tableDataList = new List<TableData>();
          //  }
            //if (m_tableDictionary != null)
            //{
                m_tableDictionary = new Dictionary<string, TableData>();
            //}
     
        }
        /// <summary>
        /// 载入数据
        /// </summary>
        /// <returns></returns>
        public static TablesDoc LoadObj()
        {
            TablesDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/TablesDoc" + ".xml");
                pDoc = (TablesDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_tableDictionary = pDoc.m_tableDataList.ToDictionary(p => p.strTableName);
                foreach (TableData item in pDoc.m_tableDataList)
                {
                    //fileList = fileList.OrderBy(s => int.Parse(Regex.Match(s, @"\d+").Value)).ToArray();
                    item.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);
                }

            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new TablesDoc();
            }
            return pDoc;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/TablesDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        public bool SaveDocOtherForder(string strPath)
        {
            //if (!Directory.Exists(@".//PointDate/"))
            //{
            //    Directory.CreateDirectory(@".//PointDate/");
            //}
            //  FileStream fsWriter = new FileStream(@".//PointDate/" + strPath + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            FileStream fsWriter = new FileStream(strPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }

    public class TablesDocOther
    {
        public List<TableData> m_tableDataList;
        [XmlIgnore]
        public Dictionary<string, TableData> m_tableDictionary;
        public TablesDocOther()
        {
            m_tableDataList = new List<TableData>();
            m_tableDictionary = new Dictionary<string, TableData>();
        }
        /// <summary>
        /// 载入数据
        /// </summary>
        /// <returns></returns>
        public static TablesDocOther LoadObj()
        {
            TablesDocOther pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDocOther));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/TablesDoc" + ".xml");
                pDoc = (TablesDocOther)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_tableDictionary = pDoc.m_tableDataList.ToDictionary(p => p.strTableName);
                foreach (TableData item in pDoc.m_tableDataList)
                {
                    //fileList = fileList.OrderBy(s => int.Parse(Regex.Match(s, @"\d+").Value)).ToArray();
                  //  item.tablePosData.tablePosItemDictionary = item.tablePosData.tablePosItemList.ToDictionary(p => p.strName);
                }

            }
            catch (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new TablesDocOther();
            }
            return pDoc;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/TablesDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
        public bool SaveDocOtherForder(string strPath)
        {
            //if (!Directory.Exists(@".//PointDate/"))
            //{
            //    Directory.CreateDirectory(@".//PointDate/");
            //}
            //  FileStream fsWriter = new FileStream(@".//PointDate/" + strPath + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            FileStream fsWriter = new FileStream(strPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablesDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
