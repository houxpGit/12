using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace ControlPlatformLib.Models
{
    public class PathDataDriver
    {
        public string Name { get; set; }
        public List<PathData> PathDataList { get; set; }

        public PathDataDriver()
        {
            PathDataList = new List<PathData>();
        }
    }

    static public class PathDataManage
    {
        static public PathDataDriverDoc pathDoc;

        static public void InitData()
        {
            pathDoc = PathDataDriverDoc.LoadObj();
        }
    }

    public class PathDataDriverDoc
    {
        public List<PathDataDriver> m_pathDataDriverList;

        [XmlIgnore]
        public Dictionary<string, PathDataDriver> m_pathDataDriverDictionary;

        public PathDataDriverDoc()
        {
            m_pathDataDriverList = new List<PathDataDriver>();
            m_pathDataDriverDictionary = new Dictionary<string, PathDataDriver>();
        }

        public static PathDataDriverDoc LoadObj()
        {
            PathDataDriverDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PathDataDriverDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/PathDoc" + ".xml");
                pDoc = (PathDataDriverDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.m_pathDataDriverDictionary = pDoc.m_pathDataDriverList.ToDictionary(p => p.Name);
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new PathDataDriverDoc();
            }
            return pDoc;
        }

        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/PathDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PathDataDriverDoc));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }

    public class PathData
    {
        public InterpolationType PathType { get; set; }

        public double R { get; set; }

        public short CircleDir { get; set; }


        public TablePosItem PosStart { get; set; }

        public TablePosItem PosEnd { get; set; }

        public TablePosItem PosMid { get; set; }

        public PathData()
        {
            R = 0;
            PosMid = null;
        }

    }
}
