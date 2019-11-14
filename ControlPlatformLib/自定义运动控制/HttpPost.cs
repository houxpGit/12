using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ControlPlatformLib
{
    public  class HttpPost
    {
        string UrlStr;
        public HttpPost(string turl)
        {
            UrlStr = turl;
        }
        public string getResult(string CommandName, string Arguments, int TimeOut)
        {
            try
            {               
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(UrlStr + CommandName);             
                webRequest.Method = "POST";
                //webRequest.KeepAlive = true;
                webRequest.Timeout = TimeOut;

               // webRequest.ContentType = "application/json;charset=utf-8";//;charset=utf-8
                webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                byte[] bytes = Encoding.UTF8.GetBytes(Arguments);
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                //Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);

                Stream stream = null;
                StreamReader streamReader = null;
                string result;
                try
                {
                    stream = httpWebResponse.GetResponseStream();
                    streamReader = new StreamReader(stream, Encoding.UTF8);
                    result = streamReader.ReadToEnd();
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
