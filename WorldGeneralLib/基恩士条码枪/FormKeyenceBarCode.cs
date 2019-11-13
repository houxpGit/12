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
    public partial class FormKeyenceBarCode : Form
    {
        keyenceSRData m_keyenceData;
        public FormKeyenceBarCode(keyenceSRData keyenceData)
        {
            InitializeComponent();
            m_keyenceData = keyenceData;
        }

        private void FormKeyenceBarCode_Load(object sender, EventArgs e)
        {
            this.Text = m_keyenceData.m_strBarName;
            propertyGrid1.SelectedObject = m_keyenceData;
        }

        private void propertyGrid1_Validated(object sender, EventArgs e)
        {
            m_keyenceData.SaveDoc();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_keyenceData.SaveDoc();
        }
    }
}
