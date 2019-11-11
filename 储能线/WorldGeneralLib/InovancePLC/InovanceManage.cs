using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.InovancePLC
{
    public static class InovanceManage
    {
        public static InovanceDoc[] m_inovanceDoc = new InovanceDoc[5];
        public static InovanceModbusAPI[] m_inovanceAPI = new InovanceModbusAPI[5];
        public static InovanceSettingForm[] m_inovanceSettingForm = new InovanceSettingForm[5];

        public static void LoadData(int plcindex)
        {
            m_inovanceDoc[plcindex] = InovanceDoc.LoadDocument(plcindex);
            m_inovanceAPI[plcindex] = new InovanceModbusAPI();
        }

        public static bool SetPLCValueByte(int plcindex, ScanItem item, bool bset)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            byte[] value = new byte[1];
            if (bset)
                value[0] = 1;
            else
                value[0] = 0;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 1, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueByte(ScanItem item,ref bool bread)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                bread = false;
                return false; ;
            }
            else
            {
                if (item.strValue == "0")
                {
                    bread = false;
                }
                else if (item.strValue == "1")
                {
                    bread = true;
                }
                return true; ;
            }
        }

        public static bool SetPLCValueInt16(int plcindex, ScanItem item, Int16 setvalue)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            Int16[] value = new Int16[1];
            value[0] = setvalue;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 1, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueInt16(ScanItem item, ref Int16 readvalue)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                readvalue = 0;
                return false; ;
            }
            else
            {
                readvalue = Int16.Parse(item.strValue);
                return true; ;
            }
        }

        public static bool SetPLCValueInt32(int plcindex, ScanItem item, Int32 setvalue)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            Int32[] value = new Int32[1];
            value[0] = setvalue;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 2, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueInt32(ScanItem item, ref Int32 readvalue)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                readvalue = 0;
                return false; ;
            }
            else
            {
                readvalue = Int32.Parse(item.strValue);
                return true; ;
            }
        }

        public static bool SetPLCValueUInt16(int plcindex, ScanItem item, UInt16 setvalue)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            UInt16[] value = new UInt16[1];
            value[0] = setvalue;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 1, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueUInt16(ScanItem item, ref UInt16 readvalue)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                readvalue = 0;
                return false; ;
            }
            else
            {
                readvalue = UInt16.Parse(item.strValue);
                return true; ;
            }
        }

        public static bool SetPLCValueUInt32(int plcindex, ScanItem item, UInt32 setvalue)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            UInt32[] value = new UInt32[1];
            value[0] = setvalue;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 2, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueUInt32(ScanItem item, ref UInt32 readvalue)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                readvalue = 0;
                return false; ;
            }
            else
            {
                readvalue = UInt32.Parse(item.strValue);
                return true; ;
            }
        }


        public static bool SetPLCValueFloat(int plcindex, ScanItem item, float setvalue)
        {
            SoftElemType type = item.AddressType;
            int address = item.Address;
            float[] value = new float[1];
            value[0] = setvalue;

            if (m_inovanceAPI[plcindex].WriteH3UElement(plcindex, type, address, 2, value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReadPLCValueFloat(ScanItem item, ref double readvalue)
        {
            if (string.IsNullOrEmpty(item.strValue))
            {
                readvalue = 0.0f;
                return false; ;
            }
            else
            {
                readvalue = double.Parse(item.strValue);
                return true; ;
            }
        }

       
    }
}
