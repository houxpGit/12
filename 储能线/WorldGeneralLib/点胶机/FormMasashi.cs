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
    public partial class FormMasashi : Form
    {
        MUSASHIData m_data;
        public FormMasashi(MUSASHIData data)
        {
            InitializeComponent();
            m_data = data;
        }

        private void FormMasashi_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = m_data;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_data.SaveDoc();
        }
    }
}
