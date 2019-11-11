/**
* 命名空间:  FullyAutomaticLaserJetCoder.CCD
* 功 能   ： N/A
* 类 名   ： CCDCommunication
* Ver     :  ver1.0.0.0
* 变更日期:  2019-04-18 10:16:14
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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FullyAutomaticLaserJetCoder.CCD
{
    public class CCDCommunication
    {
        public string Name { get; set; }
        public int Port { get; set; }
        public string IPAddress { get; set; }
        [XmlIgnore]
        public IPAddress IP { get; set; }

        public CCDCommunication()
        {
            IPAddress = "127.0.0.1";
            Port = 2018;
        }

        public static CCDCommunication LoadDocument()
        {
            CCDCommunication m_Doc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CCDCommunication));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/CCDCommunication.xml");
                m_Doc = (CCDCommunication)xmlSerializer.Deserialize(fsReader);
                m_Doc.IP = System.Net.IPAddress.Parse(m_Doc.IPAddress);
                fsReader.Close();
            }
            catch
            {
                if (fsReader != null)
                    fsReader.Close();
                m_Doc = new CCDCommunication();
            }
            return m_Doc;
        }

        public bool SaveDocument()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/CCDCommunication.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CCDCommunication));
            xmlSerializer.Serialize(fsWriter, this);
            fsWriter.Close();
            return true;
        }
    }
}
