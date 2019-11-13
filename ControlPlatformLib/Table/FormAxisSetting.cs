using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPlatformLib
{
    public partial class FormAxisSetting : Form
    {
        string strAxisName = "";
        TableAxisData axisData;
        public FormAxisSetting(string axisName, TableAxisData data)
        {
            InitializeComponent();
            strAxisName = axisName;
            axisData = data;
        }
        public FormAxisSetting(string axisName)
        {
            InitializeComponent();
            strAxisName = axisName;
            //axisData = data;
        }

        private void FormAxisSetting_Load(object sender, EventArgs e)
        {
            label1.Text = strAxisName;
            foreach (HardWareInfoBase item in HardwareManage.hardDoc.m_HardWareList)
            {
                if (item.hardwareTpye == HardWardType.MotionCard)
                {
                    comboBoxCardName.Items.Add(item.hardwareName);
                }
            }
            foreach (SenserLogic item in Enum.GetValues(typeof(SenserLogic)))
            {
                comboBoxLimt.Items.Add(item);
                comboBoxOrg.Items.Add(item);
                comboBoxAlarm.Items.Add(item);
            }
            foreach (PulseMode item in Enum.GetValues(typeof(PulseMode)))
            {
                comboBoxPulseMode.Items.Add(item);
            }
            comboBoxCardName.Text = "";
            comboBoxAxisNo.Text = "0";


        }

        private void comboBoxCardName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ComboBox combox = (ComboBox)sender;
                if (combox.Name == "comboBoxCardName")
                {
                    axisData.MotionCardName = comboBoxCardName.SelectedItem.ToString();// comboBoxCardName.Text;
                }
                if (combox.Name == "comboBoxAxisNo")
                {
                    axisData.AxisNo = short.Parse(comboBoxAxisNo.SelectedItem.ToString());//short.Parse(comboBoxAxisNo.Text);
                }
                if (combox.Name == "comboBoxLimt")
                {
                    axisData.limtLogic = (SenserLogic)comboBoxLimt.SelectedIndex;
                }
                if (combox.Name == "comboBoxOrg")
                {
                    axisData.orgLogic = (SenserLogic)comboBoxOrg.SelectedIndex;
                }
                if (combox.Name == "comboBoxAlarm")
                {
                    axisData.alarmLogic = (SenserLogic)comboBoxAlarm.SelectedIndex;
                }
                if (combox.Name == "comboBoxPulseMode")
                {
                    axisData.pulseMode = (PulseMode)comboBoxPulseMode.SelectedIndex;
                }
                if (combox.Name == "comboBoxUsed")
                {
                    axisData.iUsed = comboBoxUsed.SelectedIndex;
                }
            }
            catch
            {

            }
        }
        public void UpdateData(TableAxisData data)
        {
            axisData = data;
            comboBoxCardName.Text = axisData.MotionCardName;
            comboBoxAxisNo.Text = axisData.AxisNo.ToString();
            comboBoxLimt.SelectedIndex = (int)axisData.limtLogic;
            comboBoxOrg.SelectedIndex = (int)axisData.orgLogic;
            comboBoxAlarm.SelectedIndex = (int)axisData.alarmLogic;
            comboBoxPulseMode.SelectedIndex = (int)axisData.pulseMode;
            comboBoxUsed.SelectedIndex = axisData.iUsed;
            comboBoxPlusToMM.Text = axisData.plusToMM.ToString();
            textBoxAcc.Text = axisData.dAcc.ToString();
            textBoxDec.Text = axisData.dDec.ToString();
            textBoxSpeed.Text = axisData.dSpeed.ToString();
            textBoxJobLow.Text = axisData.dJobLow.ToString();
            textBoxJobHigh.Text = axisData.dJobHigh.ToString();
            textBoxLimtSearchSpd.Text = axisData.dLimtSpd.ToString();
            textBoxOrgSpd.Text = axisData.dOrgSpd.ToString();
            comboBoxOrgMode.SelectedIndex = axisData.iOrgMode;
            checkBoxConfig.Checked = axisData.bUsedConfig;
            comboBoxCorNo.SelectedIndex = axisData.iCorNo - 1;
            txt_Alias.Text = axisData.alias;
        }

        private void comboBoxPlusToMM_Validated(object sender, EventArgs e)
        {
            axisData.plusToMM = double.Parse(comboBoxPlusToMM.Text);
        }

        private void textBoxAcc_Validated(object sender, EventArgs e)
        {
            axisData.dAcc = double.Parse(textBoxAcc.Text);
            axisData.dDec = double.Parse(textBoxDec.Text);
            axisData.dSpeed = double.Parse(textBoxSpeed.Text);
            axisData.dJobLow = double.Parse(textBoxJobLow.Text);
            axisData.dJobHigh = double.Parse(textBoxJobHigh.Text);
            axisData.dLimtSpd = double.Parse(textBoxLimtSearchSpd.Text);
            axisData.dOrgSpd = double.Parse(textBoxOrgSpd.Text);
        }

        private void comboBoxOrgMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            axisData.iOrgMode = comboBoxOrgMode.SelectedIndex;
        }

        private void comboBoxCardName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void comboBoxCardName_Validated(object sender, EventArgs e)
        {

        }

        private void checkBoxConfig_Click(object sender, EventArgs e)
        {
            axisData.bUsedConfig = checkBoxConfig.Checked;
        }

        private void comboBoxCorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            axisData.iCorNo = comboBoxCorNo.SelectedIndex + 1;
        }

        private void txt_Alias_Validated(object sender, EventArgs e)
        {
            axisData.alias = txt_Alias.Text;
        }
    }
}
