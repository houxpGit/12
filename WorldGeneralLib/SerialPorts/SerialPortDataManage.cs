using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.SerialPorts
{
    public class SerialPortDataManage
    {
        public static SerialPortDoc m_SerialPortDoc;
        public static Dictionary<string, ISerialPort> m_SerilPorts;

        public static void LoadData()
        {
            m_SerilPorts = new Dictionary<string, ISerialPort>();
            m_SerialPortDoc = SerialPortDoc.LoadObj();
        }

        public static void SaveData()
        {
            m_SerialPortDoc.SaveDoc(); ;
        }

        public static void InitSerialPorts()
        {
            m_SerilPorts.Clear();
            foreach (var item in SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList)
            {
                switch (item.SerialPortDevice)
                {
                    case SerialPortDevice.SickAltimeter:
                        SickAltimeter sickAltmeter = new SickAltimeter();
                        sickAltmeter.Init(item);
                        m_SerilPorts.Add(item.StationName,sickAltmeter);
                        break;
                    case SerialPortDevice.Common:
                        break;
                    case SerialPortDevice.KeyenceDLRS1A:
                        KeyenceDLRS1A keyenceDLRS1A = new KeyenceDLRS1A();
                        keyenceDLRS1A.Init(item);
                        m_SerilPorts.Add(item.StationName,keyenceDLRS1A);
                        break;
                    case SerialPortDevice.Honeywell3320G:
                        Honeywell3320G honeywell3320G = new Honeywell3320G();
                        honeywell3320G.Init(item);
                        m_SerilPorts.Add(item.StationName,honeywell3320G);
                        break;
                    case SerialPortDevice.SerialPort_date:
                        SerialPort_date SerialPort = new SerialPort_date();
                        SerialPort.Init(item);
                        m_SerilPorts.Add(item.StationName, SerialPort);
                        break;
                    default:
                        break;
                }
                //Datalogic_Scanner scanner = new Datalogic_Scanner();
                //scanner.InitReader(item);
                //scannerDic.Add(item.StationName, scanner);
            }
        }
    }
}
