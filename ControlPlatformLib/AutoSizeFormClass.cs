using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;

namespace ControlPlatformLib
{
    class AutoSizeFormClass
    {
        //(1).声明结构,只记录窗体和其控件的初始位置和大小。
        public struct controlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public string strName;
        }
        //用于记录每个控件初始位置
        //注意这里不能使用控件列表记录 List nCtrl;，因为控件的关联性，记录的始终是当前的大小。
        public List<controlRect> oldCtrl = new List<controlRect>();

        private void getParentsName(Control fm, ref string strGetName)
        {
            strGetName += fm.Name.ToString();
            if (fm.Parent != null)
            {
                fm = fm.Parent;
                getParentsName(fm, ref strGetName);
            }
        }

        //记录窗体和其控件的初始位置和大小,
        public  void AddControl(Control ctl)
        {
           //首先记录主窗体原大小
                controlRect objCtrlFm;
                objCtrlFm.Left = ctl.Left; objCtrlFm.Top = ctl.Top; objCtrlFm.Width = ctl.Width; objCtrlFm.Height = ctl.Height;
                objCtrlFm.strName = ctl.Name.ToString();
                oldCtrl.Add(objCtrlFm);

            foreach (Control c in ctl.Controls)
            { //放在这里，是先记录控件的子控件，后记录控件本身
                //if (c.Controls.Count > 0)
                // AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                controlRect objCtrl;
                objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                string newName = "";
                getParentsName(c, ref  newName);

                objCtrl.strName = newName;
                oldCtrl.Add(objCtrl);


                //**放在这里，是先记录控件本身，后记录控件的子控件
                if (c.Controls.Count > 0)
                    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
        //控件自适应大小,
        public void controlAutoSize(Control mForm)
        {//should pass in form

            float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
            float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;
            
            AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }
        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            int ctrLeft0=0, ctrTop0=0, ctrWidth0=0, ctrHeight0=0;
            foreach (Control c in ctl.Controls)
            { //**放在这里，是先缩放控件的子控件，后缩放控件本身
                string newName = "";
                getParentsName(c, ref  newName);
                for(int iIndex=0 ;iIndex<oldCtrl.Count ; iIndex++)
                {
                    if (oldCtrl[iIndex].strName == newName)
                    {
                        ctrLeft0 = oldCtrl[iIndex].Left;
                        ctrTop0 = oldCtrl[iIndex].Top;
                        ctrWidth0 = oldCtrl[iIndex].Width;
                        ctrHeight0 = oldCtrl[iIndex].Height;
                        break;
                    }
                }

                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                c.Top = (int)((ctrTop0) * hScale);//
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                c.Height = (int)(ctrHeight0 * hScale);//
                
                if (c.Controls.Count > 0)
                    AutoScaleControl(c, wScale, hScale); ;//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
    }
}
