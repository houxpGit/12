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
    public partial class FormSHSF : Form
    {
        
        SHSFData m_data;

        public FormSHSF(SHSFData data)
        {
            InitializeComponent();
            m_data = data;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_data.SaveDoc();
        }

        private void FormSHSF_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = m_data;
        }
    }
}
