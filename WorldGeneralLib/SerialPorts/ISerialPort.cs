using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.SerialPorts
{
    public interface ISerialPort
    {
        bool  Init(SerialPortData data);

        void DisConnect();

        void GetData(ref string data);

        bool GetData(ref double data);
    }

    public enum SerialPortDevice
    {
        SerialPort_date,
        SickAltimeter,
        Common,
        KeyenceDLRS1A,
        Honeywell3320G,
    
    }
}
