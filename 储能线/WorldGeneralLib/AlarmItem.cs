using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib
{
    public enum AlarmType
    {
        Normal,
        MotorEstop,
        PLC,
        Robot,
        EStop,
        DoorOpen,
        Input,
        Grating,
        LaserAlarm
    }
    public class AlarmItem
    {
        public string stringAlarmMessage;
        public AlarmType alarmType;
        public string strAlarmCode;
        public List<string> listHandMessage;
        public string strHappenTime;
        public string strResetTime;
        public string strPicturePath;
        public AlarmItem()
        {
            listHandMessage = new List<string>();
            stringAlarmMessage = "";
            alarmType = AlarmType.Normal;
            strAlarmCode = "Undefine";

        }
    }
}
