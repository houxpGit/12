using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
  public partial class mes
    {
        private static mes mesDate;
        public static mes Instance()
        {
            if (mesDate == null)
            {
                mesDate = new mes();
            }
            return mesDate;
        }
        public ImgFtpUpd ImageUpDate = new ImgFtpUpd();
        public HttpPost mespost = null;
        public FtpUpd theFtp = null;
        public string userCode = "";
        public string deviceCode = "";
        public string DataReceivedstrSN = "";
        public mes()
        {
         //   mespost = new HttpPost("http://172.30.7.22:7081/mesproject/mesinterface/");
        }
        public void  Load (string Ls)
        {
            mespost = new HttpPost(Ls);

        }
        public string ImageUpLoad(string soucePath,string SN, string imgPath, string imType)
        {
            string  isok  =   ImageUpDate.Upload(soucePath);//将图片上传到服务器
            string UpLoadok = "";
            if (isok=="OK")
            {
               UpLoadok= UploadImagePath(SN.Replace("\r\n",""), imgPath, imType);//将图片数据传送到MES
            }               
            return UpLoadok;

        }
        public static Dictionary<string, string> ConverFromJson(string JsonStr)
        {
            Dictionary<string, string> dcValues = new Dictionary<string, string>();
            string[] strs = JsonStr.Trim().Trim(',').TrimStart('{').TrimEnd('}').Split(',');
            if (strs != null && strs.Length > 0)
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    int idx1 = strs[i].IndexOf('\"');
                    if (idx1 >= 0 && strs[i].Length > idx1 + 1)
                    {
                        int idx2 = strs[i].IndexOf('\"', idx1 + 1);
                        if (idx2 > idx1)
                        {
                            string tKey = strs[i].Substring(idx1 + 1, idx2 - idx1 - 1);
                            if (strs[i].Length > idx2 + 1)
                            {
                                int idx3 = strs[i].IndexOf(':', idx2 + 1);
                                if (idx3 > idx2)
                                {
                                    int idx4 = strs[i].IndexOf('\"', idx3);
                                    if (idx4 > idx3 && strs[i].Length > idx4 + 1)
                                    {
                                        int idx5 = strs[i].IndexOf('\"', idx4 + 1);
                                        if (idx5 > idx4)
                                        {
                                            string tValue = strs[i].Substring(idx4 + 1, idx5 - idx4 - 1);
                                            dcValues.Add(tKey, tValue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dcValues;
        }

        protected string getMsg_mes(string result)
        {
            Dictionary<string, string> dcValue = ConverFromJson(result);
            if (dcValue.ContainsKey("result"))
            {
                if (dcValue["result"].ToUpper() == "TRUE")
                {
                    if (dcValue.ContainsKey("message"))
                    {
                        return dcValue["message"];
                    }
                    //return "OK";
                }
                else
                {
                    return "";
                }
              
            }
            return "登陆失败";
          
        }
        protected  string getMsg(string result)
        {
            Dictionary<string, string> dcValue = ConverFromJson(result);
            if (dcValue.ContainsKey("result"))
            {
                if (dcValue["result"].ToUpper() == "TRUE")
                {
                    return "OK";
                }

                if (dcValue.ContainsKey("message"))
                {
                    return dcValue["message"];
                }
            }
            return "登陆失败";
            //Dictionary<string, string> dcValue = ConverFromJson(result);
            //if (dcValue.ContainsKey("result"))
            //{
            //    if (dcValue["result"].ToUpper() == "TRUE")
            //        return "OK";
            //    else
            //    {
            //        if (dcValue.ContainsKey("message"))
            //            return dcValue["message"];
            //    }
            //}
            //return "登陆失败";
        }
        public  string Login(string tUserNo, string tPassWord, string tMachineNo)
        {
            //if (tUserNo == "MES1" || tUserNo == "MES2")
            //    return "";
            string mesResult = "";
            try
            {
                userCode = tUserNo;
                deviceCode = tMachineNo;
                string jsonData = "jsonData={\"userCode\":\"" + tUserNo + "\",\"password\":\"" + tPassWord + "\",\"deviceCode\":\"" + tMachineNo + "\",\"res\":null}";
                mesResult = mespost.getResult("CheckUserDo.action?", jsonData, 10000);
                return getMsg(mesResult);
               
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string Group(string productSn)//过站验证  校验工序
        {
         return   GroupTest(productSn, Properties.Settings.Default.UserName, Properties.Settings.Default.MachineNo);

        }

        public  string GroupTest(string productSn, string userCode, string deviceCode)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("GroupTest.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string WipTest(string productSn, string result)//上传数据
        {
            string mesResult = "";
            mesResult = WipTest(productSn, result, Properties.Settings.Default.UserName, Properties.Settings.Default.MachineNo,"","");
            return mesResult;
        }
        public  string WipTest(string productSn, string result, string userCode, string deviceCode, string errorCodeAllData, string itemValue)//上传数据
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"errorCodeAllData\":\"");
                sbJsonData.Append(errorCodeAllData);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"itemValue\":\"");
                sbJsonData.Append(itemValue);
                sbJsonData.Append("\",\"result\":\"");
                sbJsonData.Append(result);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("WipTest.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string OfflineUploadData(string productSn, string groupCode, string errCode, string result, string errorCodeAllData, string itemValue)//上传数据
        {
            string mesResult = "";
            OfflineUploadData( productSn,  groupCode,  errCode,  result, Properties.Settings.Default.UserName, Properties.Settings.Default.MachineNo, errorCodeAllData,itemValue);
            return mesResult;
        }
        public string OfflineUploadData(string productSn, string groupCode, string errCode, string result, string userCode, string deviceCode, string errorCodeAllData, string itemValue)//上传数据
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"productSn\":\"");
                sbJsonData.Append(productSn.Replace("\r\n", ""));
                sbJsonData.Append("\",\"groupCode\":\"");
                sbJsonData.Append(groupCode);
                sbJsonData.Append("\",\"errorCode\":\"");
                sbJsonData.Append(errCode);
                sbJsonData.Append("\",\"userCode\":\"");
                sbJsonData.Append(userCode);
                sbJsonData.Append("\",\"deviceCode\":\"");
                sbJsonData.Append(deviceCode);
                sbJsonData.Append("\",\"errorCodeAllData\":\"");
                sbJsonData.Append(errorCodeAllData);
                sbJsonData.Append("\",\"itemValue\":\"");
                sbJsonData.Append(itemValue);
                sbJsonData.Append("\",\"result\":\"");
                sbJsonData.Append(result);
                sbJsonData.Append("\",\"res\":null}");
                mesResult = mespost.getResult("OfflineUploadData.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
        public string UploadImagePath(string productSn, string imgPath, string imType)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData=[{\"productSn\":\"");
                sbJsonData.Append(productSn);
                sbJsonData.Append("\",\"imgPath\":\"");
                sbJsonData.Append(imgPath);
                sbJsonData.Append("\",\"imgType\":\"");
                sbJsonData.Append(imType);
                //sbJsonData.Append("\",\"res\":null}");
                sbJsonData.Append("\"}]");


                mesResult = mespost.getResult("UploadImgPath.action?", sbJsonData.ToString(), 10000);
                return getMsg(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }

        public string CellToolingPlate(string MeProductSn)//过站验证  校验工序
        {
            string mesResult = "";
            try
            {
                StringBuilder sbJsonData = new StringBuilder();
                sbJsonData.Append("jsonData={\"plateSn\":\"");
                sbJsonData.Append(MeProductSn);
                sbJsonData.Append("\",\"productSn\":\"");
                sbJsonData.Append(" ");
                sbJsonData.Append("\",\"linkType\":\"");
                sbJsonData.Append("1");
                //sbJsonData.Append("\",\"res\":null}");
                sbJsonData.Append("\"}");
                mesResult = mespost.getResult("CellToolingPlate.action?", sbJsonData.ToString(), 10000);
                return getMsg_mes(mesResult);
            }
            catch (Exception ex1)
            {
                return mesResult.Replace("[", "【").Replace("]", "】").Replace("\"", "“").Replace(":", "：").Replace("{", "｛").Replace("}", "｝").Replace(",", "，") + "；" + ex1.Message;
            }
        }
    }
}
