using ControlPlatformLib.Softservo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlPlatformLib.Beckhoff;

namespace ControlPlatformLib
{

    public partial class FormHardSetting : Form
    {
        public FormHardSetting()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxHardWareName.Text == "")
            {
                return;
            }
            try
            {
                #region Demo
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.Demo)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputCard)
                    {
                        DemoInputInfo demoInputInfo = new DemoInputInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputOutputCard)
                    {
                        DemoInputOutputInfo demoInfo = new DemoInputOutputInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        DemoMCInfo demoInfo = new DemoMCInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.OutputCard)
                    {
                        DemoOutputInfo demoInfo = new DemoOutputInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
                #region LEISAI
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.LEADTECH)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputCard)
                    {
                        LEISAIInputInfo demoInputInfo = new LEISAIInputInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputOutputCard)
                    {
                        LEISAIInputOutputInfo demoInfo = new LEISAIInputOutputInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        LEISAIMCInfo demoInfo = new LEISAIMCInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.OutputCard)
                    {
                        LEISAIOutputInfo demoInfo = new LEISAIOutputInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
                #region GOOGOL
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.GOOGOL)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        GoogoTechMCInfo demoInfo = new GoogoTechMCInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.ExpansionModule)
                    {
                        GoogolTechExtCardInfo demoInfo = new GoogolTechExtCardInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
                #region ADVANCE
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.ADVANTECH)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputCard)
                    {
                        AdvanceInputInfo demoInputInfo = new AdvanceInputInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.OutputCard)
                    {
                        AdvanceOutputInfo demoInfo = new AdvanceOutputInfo();
                        demoInfo.hardwareName = textBoxHardWareName.Text;
                        demoInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInfo.hardwareName, demoInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInfo.hardwareName);
                        lvi.SubItems.Add(demoInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
                #region ADLINK
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.ADLINK)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        ADLINKTechMCInfo demoInputInfo = new ADLINKTechMCInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputOutputCard)
                    {
                        ADLINKInputOutputInfo demoInputInfo = new ADLINKInputOutputInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.InputCard)
                    {
                        ADLINKInputInfo demoInputInfo = new ADLINKInputInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
                #region Softservo
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.SOFTSERVO)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        SoftservoControlerInfo demoInputInfo = new SoftservoControlerInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                }
                if ((HardWardVender)comboBoxVender.SelectedItem == HardWardVender.BECKHOFF)
                {
                    if ((HardWardType)comboBoxType.SelectedItem == HardWardType.MotionCard)
                    {
                        BeckhoffADSInfo demoInputInfo = new BeckhoffADSInfo();
                        demoInputInfo.hardwareName = textBoxHardWareName.Text;
                        demoInputInfo.hardwareVender = (HardWardVender)comboBoxVender.SelectedItem;
                        demoInputInfo.hardwareTpye = (HardWardType)comboBoxType.SelectedItem;
                        HardwareManage.hardDoc.m_HardWareDictionary.Add(demoInputInfo.hardwareName, demoInputInfo);
                        HardwareManage.hardDoc.m_HardWareList.Add(demoInputInfo);
                        ListViewItem lvi = listViewNFHardWare.Items.Add(demoInputInfo.hardwareName);
                        lvi.SubItems.Add(demoInputInfo.hardwareVender.ToString());
                        lvi.SubItems.Add(demoInputInfo.hardwareTpye.ToString());
                    }
                }
                #endregion
            }
            catch
            {

            }
        }

        private void FormHardSetting_Load(object sender, EventArgs e)
        {
            foreach (HardWardVender item in Enum.GetValues(typeof(HardWardVender)))
            {
                comboBoxVender.Items.Add(item);
            }
            if (comboBoxVender.Items.Count > 0)
            {
                comboBoxVender.SelectedIndex = 0;
            }
            foreach (HardWardType item in Enum.GetValues(typeof(HardWardType)))
            {
                comboBoxType.Items.Add(item);
            }
            if (comboBoxType.Items.Count > 0)
            {
                comboBoxType.SelectedIndex = 0;
            }
            foreach (HardWareInfoBase item in HardwareManage.hardDoc.m_HardWareList)
            {
                ListViewItem lvi = listViewNFHardWare.Items.Add(item.hardwareName);
                lvi.SubItems.Add(item.hardwareVender.ToString());
                lvi.SubItems.Add(item.hardwareTpye.ToString());
            }

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewNFHardWare.SelectedItems.Count > 0)
            {
                HardwareManage.hardDoc.m_HardWareDictionary.Remove(listViewNFHardWare.SelectedItems[0].Text);
                HardwareManage.hardDoc.m_HardWareList.RemoveAt(listViewNFHardWare.SelectedItems[0].Index);
                listViewNFHardWare.SelectedItems[0].Remove();
                if (panel1.Controls.Count > 0)
                {
                    panel1.Controls.RemoveAt(0);
                }
            }
        }

        private void listViewNFHardWare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNFHardWare.SelectedItems.Count > 0)
            {
                HardWareInfoBase hwInfoB = HardwareManage.hardDoc.m_HardWareDictionary[listViewNFHardWare.Items[listViewNFHardWare.SelectedItems[0].Index].Text];
                hwInfoB.ShowSettingForm(panel1);
            }
        }
    }
}
