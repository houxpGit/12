using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace WorldGeneralLib.InovancePLC
{
    public partial class InovanceSettingForm : Form
    {
        public System.Drawing.Image Grey;
        public System.Drawing.Image Green;

        private int netid;

        public InovanceSettingForm(Panel panel,int plcindex)
        {
            InitializeComponent();
            this.TopLevel = false;
            panel.Controls.Add(this);
            this.Size = panel.Size;
            netid = plcindex;
            this.Show();
        }

        private void InovanceSettingForm_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InovanceSettingForm));
            Green = (System.Drawing.Image)(resources.GetObject("GreenPB.Image"));
            Grey = (System.Drawing.Image)(resources.GetObject("GreyPB.Image"));
            ConnIndicatePB.Image = Grey;

            cbAddressType.Items.Clear();
            Type addresstype = typeof(SoftElemType);
            foreach (SoftElemType type in Enum.GetValues(addresstype))
            {
                cbAddressType.Items.Add(type);
            }

            cbDataType.Items.Clear();
            Type dataType = typeof(PLCDataType);
            foreach (PLCDataType type in Enum.GetValues(dataType))
            {
                cbDataType.Items.Add(type);
            }

            foreach (ScanItem item in InovanceManage.m_inovanceDoc[netid].m_ScanDataList)
            {
                DataGridViewRow gridrow = new DataGridViewRow();

                //element name
                DataGridViewTextBoxCell elementnamecell = new DataGridViewTextBoxCell();
                elementnamecell.Value = item.strName;

                //element type
                DataGridViewTextBoxCell elementtypecell = new DataGridViewTextBoxCell();
                elementtypecell.ValueType = addresstype;
                elementtypecell.Value = item.AddressType;

                //element address
                DataGridViewTextBoxCell elementaddresscell = new DataGridViewTextBoxCell();
                elementaddresscell.Value = item.Address;

                //element data type
                DataGridViewTextBoxCell elementdatatypecell = new DataGridViewTextBoxCell();
                elementdatatypecell.ValueType = dataType;
                elementdatatypecell.Value = item.DataType;

                //scan item
                //DataGridViewCheckBoxCell scancell = new DataGridViewCheckBoxCell();
                //scancell.Value = item.bScanning;

                //value item
                DataGridViewTextBoxCell valuecell = new DataGridViewTextBoxCell();
                valuecell.Value = item.strValue;


                gridrow.Cells.Add(elementnamecell);
                gridrow.Cells.Add(elementtypecell);
                gridrow.Cells.Add(elementaddresscell);
                gridrow.Cells.Add(elementdatatypecell);
                //gridrow.Cells.Add(scancell);
                gridrow.Cells.Add(valuecell);
                dataGridView.Rows.Add(gridrow);
            }
            ipAddressControlPLC.SetAddressBytes(InovanceManage.m_inovanceDoc[netid].ipAddress.GetAddressBytes());       
        }

        public void StartThread()
        {
            Thread ScanPLCThread = new Thread(ScanPLC);
            ScanPLCThread.IsBackground = true;
            ScanPLCThread.Start();
        }

        public void ScanPLC()
        {
            if (InovanceManage.m_inovanceAPI[netid].bConnectInovanceOk == false)
                return;
            while (true)
            {
                Action action1 = () =>
                {
                    if (InovanceManage.m_inovanceAPI[netid].bConnectInovanceOk)
                        ConnIndicatePB.Image = Green;
                    else
                        ConnIndicatePB.Image = Grey;
                };
                ConnIndicatePB.Invoke(action1);

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    //if (!bool.Parse(dataGridView.Rows[i].Cells[4].Value.ToString()))
                    //{
                    //    continue;
                    //}
                    try
                    {
                        string strName = dataGridView.Rows[i].Cells[0].Value.ToString();
                        ScanItem updatedictionary = InovanceManage.m_inovanceDoc[netid].m_scanDictionary[strName];
                        ScanItem updatelist = InovanceManage.m_inovanceDoc[netid].m_ScanDataList[i];
                        
                        SoftElemType elementtype = (SoftElemType)dataGridView.Rows[i].Cells[1].Value;
                        int startaddress = int.Parse(dataGridView.Rows[i].Cells[2].Value.ToString());
                        PLCDataType datatype = (PLCDataType)dataGridView.Rows[i].Cells[3].Value;

                        string strValue = string.Empty;

                        switch (datatype)
                        {
                            case PLCDataType.BYTE:
                                byte[] tempbyte = new byte[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 1, ref tempbyte);
                                strValue = tempbyte[0].ToString();
                                break;
                            case PLCDataType.INT16:
                                short[] tempshort = new short[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 1, ref tempshort);
                                strValue = tempshort[0].ToString();
                                break;
                            case PLCDataType.INT32:
                                Int32[] templong = new Int32[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 2, ref templong);
                                strValue = templong[0].ToString();
                                break;
                            case PLCDataType.UINT16:
                                ushort[] tempushort = new ushort[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 1, ref tempushort);
                                strValue = tempushort[0].ToString();
                                break;
                            case PLCDataType.UINT32:
                                UInt32[] tempulong = new UInt32[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 2, ref tempulong);
                                strValue = tempulong[0].ToString();
                                break;
                            case PLCDataType.FLOAT:
                                float[] tempfloat = new float[1];
                                InovanceManage.m_inovanceAPI[netid].ReadH3UElement(netid, elementtype, startaddress, 2, ref tempfloat);
                                strValue = tempfloat[0].ToString();
                                break;
                        }
                        updatedictionary.strValue = strValue;
                        updatelist.strValue = strValue;
                        Action action = () =>
                            {
                                dataGridView.Rows[i].Cells[4].Value = strValue;
                            };
                        this.Invoke(action);

                    }
                    catch (Exception ex)
                    {
                        Action action = () =>
                           {
                               dataGridView.Rows[i].Cells[4].Value = ex.ToString();
                           };
                        this.Invoke(action);
                    }
                    Thread.Sleep(10);
                }
            }
        }

        
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!UniqueCheck(1))
            {
                return;
            }
            try
            {
                ScanItem item = new ScanItem();
                item.strName = textBoxName.Text;
                item.Address = int.Parse(tbStartAddress.Text);
                item.AddressType = (SoftElemType)cbAddressType.SelectedItem;
                item.DataType = (PLCDataType)cbDataType.SelectedItem;

                int length = 1;
                for (int i = 0; i < length; i++)
                {
                    ScanItem newitem = new ScanItem();
                    newitem.AddressType = (SoftElemType)cbAddressType.SelectedItem;
                    newitem.DataType = (PLCDataType)cbDataType.SelectedItem;

                    DataGridViewRow gridrow = new DataGridViewRow();

                    //element name
                    DataGridViewTextBoxCell elementnamecell = new DataGridViewTextBoxCell();
                    item.strName = textBoxName.Text;
                    if (i > 0)
                        item.strName = textBoxName.Text + "_" + i.ToString();
                    elementnamecell.Value = item.strName;

                    //element type
                    DataGridViewTextBoxCell elementtypecell = new DataGridViewTextBoxCell();
                    elementtypecell.ValueType = typeof(SoftElemType);
                    elementtypecell.Value = item.AddressType;

                    //element address
                    DataGridViewTextBoxCell elementaddresscell = new DataGridViewTextBoxCell();
                    item.Address = int.Parse(tbStartAddress.Text) + i;
                    elementaddresscell.Value = item.Address;

                    //element data type
                    DataGridViewTextBoxCell elementdatatypecell = new DataGridViewTextBoxCell();
                    elementdatatypecell.ValueType = typeof(PLCDataType);
                    elementdatatypecell.Value = item.DataType;

                    //scan item
                    //DataGridViewCheckBoxCell scancell = new DataGridViewCheckBoxCell();
                    //scancell.Value = item.bScanning;

                    //value item
                    DataGridViewTextBoxCell valuecell = new DataGridViewTextBoxCell();
                    valuecell.Value = item.strValue;

                    InovanceManage.m_inovanceDoc[netid].m_ScanDataList.Add(item);
                    InovanceManage.m_inovanceDoc[netid].m_scanDictionary.Add(item.strName, item);

                    gridrow.Cells.Add(elementnamecell);
                    gridrow.Cells.Add(elementtypecell);
                    gridrow.Cells.Add(elementaddresscell);
                    gridrow.Cells.Add(elementdatatypecell);
                    //gridrow.Cells.Add(scancell);
                    gridrow.Cells.Add(valuecell);
                    dataGridView.Rows.Add(gridrow);
                }
            }
            catch
            { 
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                InovanceManage.m_inovanceDoc[netid].m_scanDictionary.Remove(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                InovanceManage.m_inovanceDoc[netid].m_ScanDataList.RemoveAt(dataGridView.SelectedRows[0].Index);
                dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    int iCol = e.ColumnIndex;
            //    int iRow = e.RowIndex;
            //    string strName = dataGridView.Rows[iRow].Cells[0].Value.ToString();
            //    ScanItem updatedictionary = InovanceManage.m_inovanceDoc[netid].m_scanDictionary[strName];
            //    ScanItem updatelist = InovanceManage.m_inovanceDoc[netid].m_ScanDataList[iRow];
            //    switch (iCol)
            //    {
            //        case 0:
            //            updatedictionary.strName = strName;
            //            updatelist.strName = strName;
            //            break;
            //        case 1:
            //            SoftElemType elementtype = (SoftElemType)(dataGridView.Rows[iRow].Cells[1].Value);
            //            updatedictionary.AddressType = elementtype;
            //            updatelist.AddressType = elementtype;
            //            break;
            //        case 2:
            //            int elementaddress = int.Parse(dataGridView.Rows[iRow].Cells[2].Value.ToString());
            //            if (elementaddress >= 0)
            //            {
            //                updatedictionary.Address = elementaddress;
            //                updatelist.Address = elementaddress;
            //            }
            //            break;
            //        case 3:
            //            PLCDataType datatype = (PLCDataType)dataGridView.Rows[iRow].Cells[3].Value;
            //            updatedictionary.DataType = datatype;
            //            updatelist.DataType = datatype;
            //            break;
            //        case 4:
            //            bool bScanning = bool.Parse(dataGridView.Rows[iRow].Cells[4].Value.ToString());
            //            updatedictionary.bScanning = bScanning;
            //            updatelist.bScanning = bScanning;
            //            break;
            //        case 5:
            //            string strValue = dataGridView.Rows[iRow].Cells[5].Value.ToString();
            //            updatedictionary.strValue = strValue;
            //            updatelist.strValue = strValue;
            //            break;
            //    }
            //}
            //catch
            //{ 
            //}
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iCol = e.ColumnIndex;
            int iRow = e.RowIndex;
            if (iCol == -1 && iRow >= 0)
            {
                textBoxName.Text = dataGridView.Rows[iRow].Cells[0].Value.ToString();
                cbAddressType.SelectedItem = (SoftElemType)dataGridView.Rows[iRow].Cells[1].Value;
                tbStartAddress.Text = dataGridView.Rows[iRow].Cells[2].Value.ToString();
                cbDataType.SelectedItem = (PLCDataType)dataGridView.Rows[iRow].Cells[3].Value;
                tbSetValue.Text = "1";        
                return;
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选中要修改行");
                return;
            }
            int iRow = dataGridView.SelectedRows[0].Index;
            if (iRow >= 0)
            {
                if (UniqueCheck(0))
                {
                    string strName = dataGridView.Rows[iRow].Cells[0].Value.ToString();
                    ScanItem updatedictionary = InovanceManage.m_inovanceDoc[netid].m_scanDictionary[strName];
                    ScanItem updatelist = InovanceManage.m_inovanceDoc[netid].m_ScanDataList[iRow];

                    dataGridView.Rows[iRow].Cells[0].Value = textBoxName.Text;
                    updatedictionary.strName = textBoxName.Text;
                    updatelist.strName = textBoxName.Text;

                    dataGridView.Rows[iRow].Cells[1].Value = (SoftElemType)cbAddressType.SelectedItem;
                    updatedictionary.AddressType = (SoftElemType)cbAddressType.SelectedItem;
                    updatelist.AddressType = (SoftElemType)cbAddressType.SelectedItem;

                    dataGridView.Rows[iRow].Cells[2].Value = tbStartAddress.Text;
                    updatedictionary.Address = int.Parse(tbStartAddress.Text);
                    updatelist.Address = int.Parse(tbStartAddress.Text);

                    dataGridView.Rows[iRow].Cells[3].Value = (PLCDataType)cbDataType.SelectedItem;
                    updatedictionary.DataType = (PLCDataType)cbDataType.SelectedItem;
                    updatelist.DataType = (PLCDataType)cbDataType.SelectedItem;

                    //dataGridView.Rows[iRow].Cells[4].Value = false;
                    //updatedictionary.bScanning = false;
                    //updatelist.bScanning = false;
                }          
            }
        }

        //true : 名字,地址没有重复项
        //false: 名字，地址有重复项或名字为空
        //cmdindex 0: modify  1:add
        private bool UniqueCheck(int cmdindex)
        {
            //string strName,SoftElemType elementtype,int startaddress
            if (string.IsNullOrEmpty(textBoxName.Text) )
            {
                MessageBox.Show("名字为空，请检查!");
                return false;
            }
            foreach (ScanItem item in InovanceManage.m_inovanceDoc[netid].m_ScanDataList)
            {
                if (cmdindex == 0)
                {
                    if (textBoxName.Text.Equals(item.strName) && (((SoftElemType)cbAddressType.SelectedItem).Equals(item.AddressType) && int.Parse(tbStartAddress.Text) == item.Address))
                    {
                        MessageBox.Show("名字和地址有重复，请检查!");
                        return false;
                    }
                }
                else if (cmdindex == 1)
                {
                    if (textBoxName.Text.Equals(item.strName))
                    {
                        MessageBox.Show("名字有重复，请检查!");
                        return false;
                    }
                    if(((SoftElemType)cbAddressType.SelectedItem).Equals(item.AddressType) && int.Parse(tbStartAddress.Text) == item.Address)
                    {
                        MessageBox.Show("地址有重复，请检查!");
                        return false;
                    }
                }
            }
            return true;
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            bool bRet = false;
            if (dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选中要设定的行");
                return;
            }

            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("名字为空，请检查!");
                return;
            }
            if(string.IsNullOrEmpty(cbAddressType.SelectedItem.ToString()))
            {
                MessageBox.Show("元件地址为空，请检查!");
                return;
            }
            if (int.Parse(tbStartAddress.Text) < 0)
            {
                MessageBox.Show("元件地址小于零，请检查!");
                return;
            }
            if(string.IsNullOrEmpty(cbDataType.SelectedItem.ToString()))
            {
                MessageBox.Show("元件数据类型为空，请检查!");
                return;
            }
            switch ((PLCDataType)cbDataType.SelectedItem)
            {
                case PLCDataType.BYTE:
                    if (tbSetValue.Text != "0" && tbSetValue.Text != "1")
                    {
                        MessageBox.Show("字节只能赋值为0或1");
                        return;
                    }
                    break;
                case PLCDataType.INT16:
                    if (!JudgeNumber.isWhloeNumber(tbSetValue.Text))
                    {
                        MessageBox.Show("输入数据不是整形");
                        return;
                    }
                    if (Int64.Parse(tbSetValue.Text) < Int16.MinValue || Int64.Parse(tbSetValue.Text) > Int16.MaxValue)
                    {
                        MessageBox.Show("请输入-32768~32767之间的数字！");
                        return;
                    }

                    break;
                case PLCDataType.INT32:
                    if (!JudgeNumber.isWhloeNumber(tbSetValue.Text))
                    {
                        MessageBox.Show("输入数据不是整形");
                        return;
                    }
                    if (Int64.Parse(tbSetValue.Text) < Int32.MinValue || Int64.Parse(tbSetValue.Text) > Int32.MaxValue)
                    {
                        MessageBox.Show("请输入-2147483648~2147483647之间的数字！");
                        return;
                    }
                    break;
                case PLCDataType.UINT16:
                    if (!JudgeNumber.isPositiveInteger(tbSetValue.Text))
                    {
                        MessageBox.Show("输入数据不是无符号整形");
                        return;
                    }
                    if (UInt64.Parse(tbSetValue.Text) < 0 || UInt64.Parse(tbSetValue.Text) > 32767)
                    {
                        MessageBox.Show("请输入0~32767之间的数字！");
                        return;
                    }
                    break;
                case PLCDataType.UINT32:
                    if (!JudgeNumber.isPositiveInteger(tbSetValue.Text))
                    {
                        MessageBox.Show("输入数据不是无符号整形");
                        return;
                    }
                    if (UInt64.Parse(tbSetValue.Text) < 0 || UInt64.Parse(tbSetValue.Text) > 2147483647)
                    {
                        MessageBox.Show("请输入0~2147483647之间的数字！");
                        return;
                    }
                    break;
                case PLCDataType.FLOAT:
                    if(!JudgeNumber.isDecimal(tbSetValue.Text))
                    {
                        MessageBox.Show("输入数据不是浮点数");
                        return;
                    }
                    break;
            }

            string strName = textBoxName.Text;
            SoftElemType elementtype = (SoftElemType)cbAddressType.SelectedItem;
            int startaddress = int.Parse(tbStartAddress.Text);
            PLCDataType datatype = (PLCDataType)cbDataType.SelectedItem;
            string strValue = tbSetValue.Text;

            switch (datatype)
            {
                case PLCDataType.BYTE:
                    byte[] tempbyte = new byte[1];
                    tempbyte[0] = byte.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 1, tempbyte);
                    break;
                case PLCDataType.INT16:
                    Int16[] tempshort = new Int16[1];
                    tempshort[0] = short.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 1, tempshort);
                    break;
                case PLCDataType.INT32:
                    Int32[] templong = new Int32[1];
                    templong[0] = Int32.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 2, templong);
                    break;
                case PLCDataType.UINT16:
                    UInt16[] tempushort = new UInt16[1];
                    tempushort[0] = ushort.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 1, tempushort);
                    break;
                case PLCDataType.UINT32:
                    UInt32[] tempulong = new UInt32[1];
                    tempulong[0] = UInt32.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 2, tempulong);
                    break;
                case PLCDataType.FLOAT:
                    float[] tempfloat = new float[1];
                    tempfloat[0] = float.Parse(strValue);
                    bRet = InovanceManage.m_inovanceAPI[netid].WriteH3UElement(netid, elementtype, startaddress, 2, tempfloat);
                    break;
            }
        }

        private void ipAddressControlPLC_TextChanged(object sender, EventArgs e)
        {
            InovanceManage.m_inovanceDoc[netid].ipAddress = new IPAddress(ipAddressControlPLC.GetAddressBytes());
            InovanceManage.m_inovanceDoc[netid].stripAddress = InovanceManage.m_inovanceDoc[netid].ipAddress.ToString();
        }

        private void buttonExportName_Click(object sender, EventArgs e)
        {
            string filename = "InovancePLC" + netid.ToString()+".cs";
            string strTempPath = @".//TempFile/";
            string strTempName = filename;
            if(!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            string strcontent = CreatePlcnameCSFile();
            File.WriteAllText((strTempPath+strTempName),strcontent,Encoding.UTF8);
            MessageBox.Show("导出成功");
        }

        private string CreatePlcnameCSFile()
        {
            string strreturn = string.Empty;
            if (InovanceManage.m_inovanceDoc[netid].m_ScanDataList.Count < 1)
                return strreturn;
            string classname = "InovancePLC" + netid.ToString();

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace WorldGeneralLib.InovancePLC\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class "+classname+"\r\n";
            strTempHeader += "   {\r\n";
            strreturn = strTempHeader;

            string strItemHeader = "      public static string ";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = "  ;\r\n";

            foreach (ScanItem item in InovanceManage.m_inovanceDoc[netid].m_ScanDataList)
            {
                strItemName = item.strName;
                strValue = "\"" + strItemName + "\"";
                strreturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "  }\r\n";
            strTempEnd += "}\r\n";
            strreturn += strTempEnd;
            return strreturn;
        }


    }
}
