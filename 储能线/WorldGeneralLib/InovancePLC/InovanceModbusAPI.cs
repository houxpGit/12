using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace WorldGeneralLib.InovancePLC
{
    public enum SoftElemType
    {
        //H3U
        Y = 0x20,      //Y元件的定义	
        X = 0x21,		//X元件的定义							
        S = 0x22,		//S元件的定义				
        M = 0x23,		//M元件的定义							
        TB = 0x24,		//T位元件的定义				
        TW = 0x25,		//T字元件的定义				
        CB = 0x26,		//C位元件的定义				
        CW = 0x27,		//C字元件的定义				
        DW = 0x28,		//D字元件的定义				
        CW2 = 0x29,	    //C双字元件的定义
        SM = 0x2a,		//SM
        SD = 0x2b,		//
        R = 0x2c		//
    }

    public enum PLCDataType
    {
        BYTE,
        INT16,
        INT32,
        UINT16,
        UINT32,
        FLOAT
    }

    public class InovanceModbusAPI
    {
        #region //标准库
        /******************************************************************************
         1.功能描述 :创建网络连接	                  
         2.返 回 值 :TRUE 成功  FALSE 失败
         3.参    数 : sIpAddr:以太网IP地址，
                      nNetId:网络链接编号,用于标记是第几条网络链接,取值范围0~255,默认0 
			          IpPort:以太网端口号,默认502(modbusTcp协议默认端口号为502)
         4.注意事项 : 
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "Init_ETH_String", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init_ETH_String(string sIpAddr, int nNetId = 0, int IpPort = 502);

        /******************************************************************************
         1.功能描述 :关闭网络连接                  
         2.返 回 值 :TRUE 成功  FALSE 失败
         3.参    数 : nNetId:网络链接编号,与Init_ETH（）调用的ID一样
         4.注意事项 : 
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        /******************************************************************************
         1.功能描述 : 写H3u软元件
         2.返 回 值 :1 成功  0 失败
         3.参    数 : nNetId:网络链接编号
			          eType：软元件类型  
				          REGI_H3U_Y    = 0x20,     //Y元件的定义	
				          REGI_H3U_X    = 0x21,		//X元件的定义							
				          REGI_H3U_S    = 0x22,		//S元件的定义				
				          REGI_H3U_M    = 0x23,		//M元件的定义							
				          REGI_H3U_TB   = 0x24,		//T位元件的定义				
				          REGI_H3U_TW   = 0x25,		//T字元件的定义				
				          REGI_H3U_CB   = 0x26,		//C位元件的定义				
				          REGI_H3U_CW   = 0x27,		//C字元件的定义				
				          REGI_H3U_DW   = 0x28,		//D字元件的定义				
				          REGI_H3U_CW2  = 0x29,	    //C双字元件的定义
				          REGI_H3U_SM   = 0x2a,		//SM
				          REGI_H3U_SD   = 0x2b,		//SD
				          REGI_H3U_R    = 0x2c		//SD
			          nStartAddr:软元件起始地址
			          nCount：软元件个数
			          pValue：数据缓存区
         4.注意事项 : 1.x和y元件地址需为8进制; 2. 当元件位C元件双字寄存器时，每个寄存器需占4个字节的数据
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, Int32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem_UInt16(SoftElemType eType, int nStartAddr, int nCount, ushort[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem_UInt32(SoftElemType eType, int nStartAddr, int nCount, UInt32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);


        /******************************************************************************
         1.功能描述 : 读H3u软元件
         2.返 回 值 :1 成功  0 失败
         3.参    数 : nNetId:网络链接编号
			          eType：软元件类型  
				          REGI_H3U_Y    = 0x20,     //Y元件的定义	
				          REGI_H3U_X    = 0x21,		//X元件的定义							
				          REGI_H3U_S    = 0x22,		//S元件的定义				
				          REGI_H3U_M    = 0x23,		//M元件的定义							
				          REGI_H3U_TB   = 0x24,		//T位元件的定义				
				          REGI_H3U_TW   = 0x25,		//T字元件的定义				
				          REGI_H3U_CB   = 0x26,		//C位元件的定义				
				          REGI_H3U_CW   = 0x27,		//C字元件的定义				
				          REGI_H3U_DW   = 0x28,		//D字元件的定义				
				          REGI_H3U_CW2  = 0x29,	    //C双字元件的定义
				          REGI_H3U_SM   = 0x2a,		//SM
				          REGI_H3U_SD   = 0x2b,		//SD
				          REGI_H3U_R    = 0x2c		//SD
			          nStartAddr:软元件起始地址
			          nCount：软元件个数
			          pValue：返回数据缓存区
         4.注意事项 : 1.x和y元件地址需为8进制; 
                      2. 当元件位C元件双字寄存器时，每个寄存器需占4个字节的数据
			          3.如果是读位元件，每个位元件的值存储在一个字节中，pValue数据缓存区字节数必须是8的整数倍
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, Int32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_UInt16(SoftElemType eType, int nStartAddr, int nCount, ushort[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_UInt32(SoftElemType eType, int nStartAddr, int nCount, UInt32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);


        /******************************************************************************
         1.功能描述 : 写Am600软元件
         2.返 回 值 :1 成功  0 失败
         3.参    数 : nNetId:网络链接编号
			          eType：软元件类型    ELEM_QX = 0//QX元件  ELEM_MW = 1 //MW元件
			          nStartAddr:软元件起始地址（QX元件由于带小数点，地址需要乘以10去掉小数点，如QX10.1，请输入101，MW元件直接就是它的元件地址不用处理）
			          nCount：软元件个数
			          pValue：数据缓存区
        4.注意事项 :  1.x和y元件地址需为8进制; 
			          2. 当元件位C元件双字寄存器时，每个寄存器需占4个字节的数据
			          3.如果是写位元件，每个位元件的值存储在一个字节中
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, Int32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem_UInt16(SoftElemType eType, int nStartAddr, int nCount, ushort[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem_UInt32(SoftElemType eType, int nStartAddr, int nCount, UInt32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);


        /******************************************************************************
         1.功能描述 : 读Am600软元件
         2.返 回 值 :1 成功  0 失败
         3.参    数 : nNetId:网络链接编号
			          eType：软元件类型   ELEM_QX = 0//QX元件  ELEM_MW = 1 //MW元件
			          nStartAddr:软元件起始地址（QX元件由于带小数点，地址需要乘以10去掉小数点，如QX10.1，请输入101，其它元件不用处理）
			          nCount：软元件个数
			          pValue：返回数据缓存区
         4.注意事项 : 
        ******************************************************************************/
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, Int32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem_UInt16(SoftElemType eType, int nStartAddr, int nCount, ushort[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem_UInt32(SoftElemType eType, int nStartAddr, int nCount, UInt32[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);
        #endregion

        public bool bConnectInovanceOk = false;
        private object objLock = new object();

        public bool ConnectToInovance(string ipaddress,int nNetid)
        {
            if(Init_ETH_String(ipaddress,nNetid,502))
                bConnectInovanceOk = true;
            else
                bConnectInovanceOk = false;
            return bConnectInovanceOk;
        }

        public bool DisconnectToInovance(int nNetid)
        {
            bConnectInovanceOk = false;
            return Exit_ETH(nNetid);
        }

        /******************************************************************************
        1.功能描述 : 写值到H3u软元件
        2.返 回 值 :
        3.参    数 :  netid:网络链接编号
                      startaddress:数据寄存器地址
			          ncount：软元件个数
			          value：要写入的值
        4.注意事项 : 
       ******************************************************************************/
        public bool WriteH3UElement(int netid,SoftElemType elementtype,int startaddress,int ncount,byte[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, short[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem_Int16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, Int32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem_Int32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ushort[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem_UInt16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, UInt32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem_UInt32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, float[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Write_Soft_Elem_Float(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************************************************************************
           1.功能描述 : 读H3u值 
           2.返 回 值 :
           3.参    数 :  netid:网络链接编号
                         startaddress:数据寄存器地址
                         ncount：软元件个数
                         value：数据缓存
           4.注意事项 : 
          ******************************************************************************/
        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref byte[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref short[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem_Int16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref Int32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem_Int32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref ushort[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem_UInt16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref UInt32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem_UInt32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadH3UElement(int netid, SoftElemType elementtype, int startaddress, int ncount, ref float[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = H3u_Read_Soft_Elem_Float(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************************************************************************
      1.功能描述 : 写值到AM600软元件
      2.返 回 值 :
      3.参    数 :  netid:网络链接编号
                    startaddress:数据寄存器地址
                    ncount：软元件个数
                    value：要写入的值
      4.注意事项 : 
     ******************************************************************************/
        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, byte[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, short[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem_Int16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, Int32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem_Int32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ushort[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem_UInt16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, UInt32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem_UInt32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, float[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Write_Soft_Elem_Float(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************************************************************************
           1.功能描述 : 读Am600值 
           2.返 回 值 :
           3.参    数 :  netid:网络链接编号
                         startaddress:数据寄存器地址
                         ncount：软元件个数
                         value：数据缓存
           4.注意事项 : 
          ******************************************************************************/
        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref byte[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref short[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem_Int16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref Int32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem_Int32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref ushort[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem_UInt16(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref UInt32[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem_UInt32(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadAm600Element(int netid, SoftElemType elementtype, int startaddress, int ncount, ref float[] value)
        {
            int nRet = 0;
            if (!bConnectInovanceOk)
                return false;
            lock (objLock)
            {
                nRet = Am600_Read_Soft_Elem_Float(elementtype, startaddress, ncount, value, netid);
            }
            if (1 == nRet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
