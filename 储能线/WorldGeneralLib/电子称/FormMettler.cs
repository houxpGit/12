using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib
{
    public partial class FormMettler : Form
    {
        MettlerData m_data;
        public FormMettler(MettlerData data)
        {
            InitializeComponent();
            m_data = data;
        }

        private void FormMettler_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = m_data;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_data.SaveDoc();
        }
    }
}
