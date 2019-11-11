using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace WorldGeneralLib.InovancePLC
{
    [XmlInclude(typeof(InovanceDoc))]
    public class ScanItem
    {
        public string strName {get;set;}
        public SoftElemType AddressType { get; set; }
        public int Address { get; set; }
        public PLCDataType DataType { get; set; }
        [XmlIgnore]
        public bool bScanning { get; set; }
        [XmlIgnore]
        public string strValue { get; set; }

        public ScanItem()
        {
            strName = string.Empty;
            AddressType = SoftElemType.X;
            Address = 0;
            DataType = PLCDataType.BYTE;
            bScanning = false;
            strValue = string.Empty;
        }
    }
}
