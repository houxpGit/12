using ControlPlatformLib.Softservo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlPlatformLib.Beckhoff;

namespace ControlPlatformLib
{
    public enum HardWardType
    {
        MotionCard,
        InputCard,
        OutputCard,
        InputOutputCard,
        ExpansionModule
    }
    public enum HardWardVender
    {
        Demo,
        GOOGOL,
        ADVANTECH,
        LEADTECH,
        ADLINK,
        SOFTSERVO,
        BECKHOFF
    }
    static public class HardwareManage
    {
        static public HardwareDoc hardDoc;
        static public FormHardSetting frmHardwareSetting;
        static public Dictionary<string, HardWareBase> hardwardDictionary;
        static public void LoadData()
        {
            hardDoc = HardwareDoc.LoadObj();
        }
        static public void InitHardWare()
        {
            hardwardDictionary = new Dictionary<string, HardWareBase>();
            foreach (KeyValuePair<string, HardWareInfoBase> item in hardDoc.m_HardWareDictionary)
            {
                #region Demo
                if (item.Value.hardwareVender == HardWardVender.Demo)
                {
                    if (item.Value.hardwareTpye == HardWardType.InputCard)
                    {
                        DemoInputCard demoCard = new DemoInputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.InputOutputCard)
                    {
                        DemoInputOutputCard demoCard = new DemoInputOutputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        DemoMCard demoCard = new DemoMCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.OutputCard)
                    {
                        DemoOutputCard demoCard = new DemoOutputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region LEISAI
                if (item.Value.hardwareVender == HardWardVender.LEADTECH)
                {
                    if (item.Value.hardwareTpye == HardWardType.InputCard)
                    {
                        LEISAIInputCard demoCard = new LEISAIInputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.InputOutputCard)
                    {
                        LEISAIInputOutputCard demoCard = new LEISAIInputOutputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        LEISAIMCard demoCard = new LEISAIMCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        HardWareInfoBase tempInfo = item.Value;
                        LEISAIMCInfo tempInfoLEISAI = (LEISAIMCInfo)tempInfo;
                        demoCard.usCardNo = (ushort)tempInfoLEISAI.iCardNo;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.OutputCard)
                    {
                        LEISAIOutputCard demoCard = new LEISAIOutputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region GOOGOL
                if (item.Value.hardwareVender == HardWardVender.GOOGOL)
                {
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        GoogoTechMCard demoCard = new GoogoTechMCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        HardWareInfoBase tempInfo = item.Value;
                        GoogoTechMCInfo tempInfoLEISAI = (GoogoTechMCInfo)tempInfo;
                        demoCard.usCardNo = (short)tempInfoLEISAI.iCardNo;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.ExpansionModule)
                    {
                        GoogolTechExtCard demoCard = new GoogolTechExtCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        HardWareInfoBase tempInfo = item.Value;
                        GoogolTechExtCardInfo tempInfoGoogol = (GoogolTechExtCardInfo)tempInfo;
                        demoCard.usCardNo = (short)tempInfoGoogol.iCardNo;
                        demoCard.usExtNo = (short)tempInfoGoogol.iExtCardNo;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region ADVANCE
                if (item.Value.hardwareVender == HardWardVender.ADVANTECH)
                {
                    if (item.Value.hardwareTpye == HardWardType.InputCard)
                    {
                        AdvanceInputCard demoCard = new AdvanceInputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.OutputCard)
                    {
                        AdvanceOutputCard demoCard = new AdvanceOutputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region ADLINK
                if (item.Value.hardwareVender == HardWardVender.ADLINK)
                {
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        ADLINKMotionCard demoCard = new ADLINKMotionCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        demoCard.hardwareModel = item.Value.hardwareModel;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.InputOutputCard)
                    {
                        ADLINKInputOuputCard demoCard = new ADLINKInputOuputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        demoCard.hardwareModel = item.Value.hardwareModel;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                    if (item.Value.hardwareTpye == HardWardType.InputCard)
                    {
                        ADLINKInputCard demoCard = new ADLINKInputCard();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        demoCard.hardwareModel = item.Value.hardwareModel;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region Softservo
                if (item.Value.hardwareVender == HardWardVender.SOFTSERVO)
                {
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        SoftservoControler demoCard = new SoftservoControler();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
                #region Beckhoff
                if (item.Value.hardwareVender == HardWardVender.BECKHOFF)
                {
                    if (item.Value.hardwareTpye == HardWardType.MotionCard)
                    {
                        BeckhoffADS demoCard = new BeckhoffADS();
                        demoCard.hardwareName = item.Value.hardwareName;
                        demoCard.hardwareVender = item.Value.hardwareVender;
                        demoCard.hardwareTpye = item.Value.hardwareTpye;
                        hardwardDictionary.Add(demoCard.hardwareName, demoCard);
                    }
                }
                #endregion
            }
            foreach (KeyValuePair<string, HardWareBase> item in hardwardDictionary)
            {
                item.Value.Init(hardDoc.m_HardWareDictionary[item.Key]);
            }
            
        }

        static public void CloseHardWare()
        {
            foreach (KeyValuePair<string, HardWareBase> item in hardwardDictionary)
            {
                item.Value.Close();
            }
        }
    }
    
}
