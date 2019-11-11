using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.DataLogicBarCode
{
    public class BarcodeDataManage
    {
        public static BarcodeSettingDataDoc barcodeSettingDataDoc;
        public static Dictionary<string, Datalogic_Scanner> scannerDic;

        public static void Load()
        {
            barcodeSettingDataDoc = BarcodeSettingDataDoc.LoadObj();
            scannerDic = new Dictionary<string, Datalogic_Scanner>();
        }

        public static void InitBarcodeScanner()
        {
            foreach (var item in barcodeSettingDataDoc.barcodeData)
            {
                Datalogic_Scanner scanner = new Datalogic_Scanner();
                scanner.InitReader(item);
                scannerDic.Add(item.StationName, scanner);
            }
        }

        public static void CloseBarcodeScanner()
        {
            foreach (var item in scannerDic)
            {
                item.Value.Close();
            }
        }
    }
}
