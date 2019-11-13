using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.DataLogicBarCode
{
    public partial class FormBarcodeSetting : Form
    {
        private BarcodeData barcodeData;

        public FormBarcodeSetting()
        {
            InitializeComponent();
        }

        public FormBarcodeSetting(BarcodeData barcodeData)
        {
            this.barcodeData = barcodeData;
            InitializeComponent();
        }

        private void FormBarcodeSetting_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            cbParity.DataSource = Enum.GetNames(typeof(Parity));
            cbStopBits.DataSource = Enum.GetNames(typeof(StopBits));

            cbStopBits.SelectedItem = Enum.GetName(typeof(StopBits), barcodeData.StopBit);
            cbParity.SelectedItem = Enum.GetName(typeof(Parity), barcodeData.Parity);
            cbPortName.SelectedItem = barcodeData.PortName;
            cbDataBit.SelectedItem = barcodeData.DataBit.ToString(); 
            cbBaudRate.SelectedItem = barcodeData.BaudRate.ToString();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = (from item in BarcodeDataManage.barcodeSettingDataDoc.barcodeData
                         where item.StationName == barcodeData.StationName
                         select item).First();
            result.PortName = cbPortName.Text;
            result.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            result.DataBit = Convert.ToInt32(cbDataBit.Text);
            result.StopBit = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
            result.Parity = (Parity)Enum.Parse(typeof(Parity),cbParity.Text);
            BarcodeDataManage.barcodeSettingDataDoc.SaveDoc();
        }
    }
}
