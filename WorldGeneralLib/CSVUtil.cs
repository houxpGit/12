using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Windows.Forms;

namespace WorldGeneralLib
{
    public class CSVUtil
    {
        private CSVUtil() 
        {   
        }  
        //write a new file, existed file will be overwritten  
        public static void WriteCSV(string filePathName,String[] ls) 
        {  
            WriteCSV(filePathName,true,ls);   
        }  
       //write a file, existed file will be overwritten if append = false  
       public static void WriteCSV(string filePathName,bool append, String[] ls) 
       {
           try
           {
               StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);

               fileWriter.WriteLine(String.Join(",", ls));

               fileWriter.Flush();
               fileWriter.Close();
           }
           catch 
           {
               //MessageBox.Show(ex.Message);
           }
       }
       public static void WriteCSV(string filePathName, bool append, String strMessage)
       {
           StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
           fileWriter.WriteLine(strMessage);
           fileWriter.Flush();
           fileWriter.Close();
       }  
  
   
       public static List<String[]> ReadCSV(string filePathName)  
       {  
            List<String[]> ls = new List<String[]>();  
            StreamReader fileReader=new  
            StreamReader(filePathName);   
            string strLine=""; 
            while (strLine != null) 
            {  
                strLine = fileReader.ReadLine(); 
                if (strLine != null && strLine.Length>0) 
                {  
                    ls.Add(strLine.Split(',')); 
                } 
            } 
            fileReader.Close(); 
            return ls; 
       } 
    }
}
