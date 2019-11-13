using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder
{  
     /// <summary>
    /// FTP客户端操作类
    /// </summary>
    public partial class ImgFtpUpd
    {
        //private static string FTPConstr = "ftp://172.30.7.220/";
        //private static string FTPUserName = "picftp";
        //private static string FTPPassword = "pic2018ftp1314";
        public  string FTPConstr = "";
        public  string FTPUserName = "";
        public  string FTPPassword = "";
        //定义全局变量
        private static string ReturnPath = "";
        public ImgFtpUpd()
        {

        }
        public  bool UploadFile(string ftpPath, string path, string StrFileName)
        {
            float percent = 0;
            FileInfo f = new FileInfo(path);
            path = path.Replace("\\", "/");
            bool b = MakeDir(ftpPath);
            if (b == false)
            {
                return false;
            }
            path = FTPConstr + ftpPath + StrFileName;
            //实例化FTP连接
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFtp.UseBinary = true;// 指定数据传输类型
            reqFtp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);// ftp用户名和密码
            // 默认为true，连接不会被关闭
            reqFtp.KeepAlive = false;
            // 指定执行什么命令
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;// 上传文件时通知服务器文件的大小
            int buffLength = 2048;// 缓冲大小设置为2kb
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = f.OpenRead();// 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            int allbye = (int)f.Length;
            int startbye = 0;
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    startbye = contentLen + startbye;
                    percent = (float)startbye / (float)allbye * 100;
                    if (percent <= 100)
                    {
                        int i = (int)percent;
                        //if (pb != null)
                        //{
                        //    pb.BeginInvoke(new updateui(upui), new object[] { allbye, i, pb });
                        //}
                    }

                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
                ReturnPath = path;
                return true;
            }
            catch (Exception ex)
            {
                ReturnPath = string.Format("因{0},无法完成上传", ex.Message);
                return false;
            }
        }
        public static void upui(long rowCount, int i, ProgressBar PB)
        {
            try
            {
                PB.Value = i;
            }
            catch { }
        }

        private delegate void updateui(long rowCount, int i, ProgressBar PB);
        /// <summary>
        ///在ftp服务器上创建文件目录
        /// </summary>
        /// <param name="dirName">文件目录</param>
        /// <returns></returns>
        public  bool MakeDir(string dirName)
        {
            try
            {
                bool b = RemoteFtpDirExists(dirName);
                //路径存在返回true
                if (b)
                {
                    return true;
                }
                string url = FTPConstr + dirName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                // reqFtp.KeepAlive = false;
                //指定FTP操作类型为创建目录
                reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFtp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                //获取FTP服务器的响应
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception )
            {
                //ReturnPath = string.Format("因{0},无法下载", ex.Message);
                return false;
            }

        }
        /// <summary>
        /// 判断ftp上的文件目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  bool RemoteFtpDirExists(string path)
        {

            path = FTPConstr + path;
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse resFtp = null;
            try
            {
                resFtp = (FtpWebResponse)reqFtp.GetResponse();
                FtpStatusCode code = resFtp.StatusCode;//OpeningData
                resFtp.Close();
                return true;
            }
            catch
            {
                if (resFtp != null)
                {
                    resFtp.Close();
                }
                return false;
            }
        }

        public string Upload(string ImgPath)
        {
            string StrFileName = Path.GetFileName(ImgPath);//图片路径
            //按日期命名文件夹
            string StrftpPath = DateTime.Now.ToString("yyyyMMdd");
            UploadFile(StrftpPath + "/", ImgPath, StrFileName);
            return StrftpPath;
        }
      //  #region 从ftp服务器删除文件的功能
        /// <summary>
        /// 从ftp服务器删除文件的功能
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public  bool DeleteFile(string fileName)
        {
            try
            {
                string url = FTPConstr + fileName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFtp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception )
            {
                //ReturnPath = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }
    }
}
