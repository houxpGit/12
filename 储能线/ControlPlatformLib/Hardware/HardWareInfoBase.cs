using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ControlPlatformLib.Softservo;
using ControlPlatformLib.Beckhoff;

namespace ControlPlatformLib
{
    [XmlInclude(typeof(HardWareInfoBase)), XmlInclude(typeof(DemoInputInfo)), XmlInclude(typeof(DemoInputOutputInfo)), XmlInclude(typeof(DemoMCInfo)), XmlInclude(typeof(DemoOutputInfo)),
    XmlInclude(typeof(LEISAIInputInfo)), XmlInclude(typeof(LEISAIInputOutputInfo)), XmlInclude(typeof(LEISAIMCInfo)), XmlInclude(typeof(LEISAIOutputInfo)),
    XmlInclude(typeof(GoogoTechInputInfo)), XmlInclude(typeof(GoogoTechInputOutputInfo)), XmlInclude(typeof(GoogoTechMCInfo)), XmlInclude(typeof(GoogoTechOutputInfo)),
     XmlInclude(typeof(AdvanceInputInfo)), XmlInclude(typeof(AdvanceInputOutputInfo)), XmlInclude(typeof(AdvanceMCInfo)), XmlInclude(typeof(AdvanceOutputInfo)), XmlInclude(typeof(ADLINKTechMCInfo)), XmlInclude(typeof(SoftservoControlerInfo)), XmlInclude(typeof(ADLINKInputInfo)), XmlInclude(typeof(ADLINKInputOutputInfo)), XmlInclude(typeof(BeckhoffADSInfo)), XmlInclude(typeof(GoogolTechExtCardInfo))]
    public class HardWareInfoBase
    {
        public string hardwareName = "Empty";
        public HardWardType hardwareTpye = HardWardType.MotionCard;
        public HardWardVender hardwareVender = HardWardVender.Demo;
        public ushort hardwareModel = 0;
        public string ipAddress;
        public HardWareInfoBase()
        {

        }
        virtual public void ShowSettingForm(Panel panel)
        {

        }
    }
}
