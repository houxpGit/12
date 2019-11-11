using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldGeneralLib.SerialPorts
{
    public partial class SerialPortSettingsForm : Form
    {
        SerialPortData currentData;
        string currentListViewItem;
        public SerialPortSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listViewBarcode.Items.Add(txt_StationName.Text);
            SerialPortData barcodeData = new SerialPortData();
            barcodeData.StationName = txt_StationName.Text;
            SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList.Add(barcodeData);
            SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataDictionary.Add(txt_StationName.Text.Trim(), barcodeData);
            SerialPortDataManage.m_SerialPortDoc.SaveDoc();
        }

        private void SerialPortSettingsForm_Load(object sender, EventArgs e)
        {
            foreach (var item in SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList)
            {
                listViewBarcode.Items.Add(item.StationName);
            }
        }

        private void listViewBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listViewBarcode_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentListViewItem = e.Item.Text;
            //ListView item = (ListView)sender;
            //e.Item.Text
            currentData = SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataDictionary[e.Item.Text];
            currentData.ShowSettingForm(panelBarcodeData);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (currentData==null)
            {
                MessageBox.Show("请选择要删除的串口！");
                return;
            }
            if (MessageBox.Show(string.Format("您是否要删除当前串口{0}？", currentData.StationName), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            
            SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList.Remove(currentData);
            SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataDictionary.Remove(currentListViewItem);
            SerialPortDataManage.m_SerialPortDoc.SaveDoc();
            listViewBarcode.Items.Clear();
            foreach (var item in SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList)
            {
                listViewBarcode.Items.Add(item.StationName);
            }
        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            SerialPortDataManage.InitSerialPorts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in SerialPortDataManage.m_SerilPorts)
            {
                item.Value.DisConnect();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
       
        }
    }
}
