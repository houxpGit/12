using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldGeneralLib.SerialPorts
{
    public partial class SerialPortForm : Form
    {
        SerialPortData data;
        public SerialPortForm(SerialPortData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void SerialPortForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            cbParity.DataSource = Enum.GetNames(typeof(Parity));
            cbStopBits.DataSource = Enum.GetNames(typeof(StopBits));
            cmb_Model.DataSource = Enum.GetNames(typeof(SerialPortDevice));


            cmb_Model.SelectedItem = Enum.GetName(typeof(SerialPortDevice), data.SerialPortDevice);
            cbStopBits.SelectedItem = Enum.GetName(typeof(StopBits), data.StopBit);
            cbParity.SelectedItem = Enum.GetName(typeof(Parity), data.Parity);
            

            cbPortName.SelectedItem = data.PortName;
            cbDataBit.SelectedItem = data.DataBit.ToString();
            cbBaudRate.SelectedItem = data.BaudRate.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = (from item in SerialPortDataManage.m_SerialPortDoc.m_SerialPortDataList
                          where item.StationName == data.StationName
                          select item).First();
            result.PortName = cbPortName.Text;
            result.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            result.DataBit = Convert.ToInt32(cbDataBit.Text);
            result.StopBit = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
            result.Parity = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
            result.SerialPortDevice= (SerialPortDevice)Enum.Parse(typeof(SerialPortDevice), cmb_Model.Text);
            SerialPortDataManage.m_SerialPortDoc.SaveDoc();
        }
    }
}
