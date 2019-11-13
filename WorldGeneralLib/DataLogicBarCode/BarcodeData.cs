using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.DataLogicBarCode
{
    public class BarcodeData
    {
        public string StationName { get; set; }
        public string PortName { get; set; }

        public int BaudRate { get; set; }

        public int DataBit { get; set; }

        public StopBits StopBit { get; set; }

        public Parity Parity { get; set; }

        public BarcodeData()
        {
            StationName = "";
            PortName = "COM1";
            BaudRate = 9600;
            DataBit = 8;
            StopBit = StopBits.None;
            Parity = Parity.None;
        }

        public void ShowSettingForm(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                panel.Controls.RemoveAt(i);
            }
            FormBarcodeSetting frm = new FormBarcodeSetting(this);
            frm.TopLevel = false;
            panel.Controls.Add(frm);
            frm.Size = panel.Size;
            frm.Show();
        }
    }
}
