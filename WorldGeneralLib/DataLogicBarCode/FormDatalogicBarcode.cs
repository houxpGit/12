using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.DataLogicBarCode
{
    public partial class FormDatalogicBarcode : Form
    {
        BarcodeData currentbarcodeData;
        string currentListViewItem;
        public FormDatalogicBarcode()
        {
            InitializeComponent();
        }
       
        private void FormDatalogicBarcode_Load(object sender, EventArgs e)
        {
            Update();     
        }

        public void Update()
        {
            listViewBarcode.Items.Clear();
            foreach (var item in BarcodeDataManage.barcodeSettingDataDoc.barcodeData)
            {
                listViewBarcode.Items.Add(item.StationName);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listViewBarcode.Items.Add(txtBarcodeName.Text);
            BarcodeData barcodeData = new BarcodeData();
            barcodeData.StationName = txtBarcodeName.Text;
            BarcodeDataManage.barcodeSettingDataDoc.barcodeData.Add(barcodeData);
            BarcodeDataManage.barcodeSettingDataDoc.barcodeDataDic.Add(txtBarcodeName.Text.Trim(), barcodeData);
            BarcodeDataManage.barcodeSettingDataDoc.SaveDoc();           
        }     

        private void listViewBarcode_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentListViewItem = e.Item.Text;
            //ListView item = (ListView)sender;
            //e.Item.Text
            currentbarcodeData = BarcodeDataManage.barcodeSettingDataDoc.barcodeDataDic[e.Item.Text];
            currentbarcodeData.ShowSettingForm(panelBarcodeData);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            BarcodeDataManage.barcodeSettingDataDoc.barcodeData.Remove(currentbarcodeData);
            BarcodeDataManage.barcodeSettingDataDoc.barcodeDataDic.Remove(currentListViewItem);
            BarcodeDataManage.barcodeSettingDataDoc.SaveDoc();
            listViewBarcode.Items.Clear();
            foreach (var item in BarcodeDataManage.barcodeSettingDataDoc.barcodeData)
            {
                listViewBarcode.Items.Add(item.StationName);
            }
        }

        private void listViewBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
