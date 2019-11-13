using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace WorldGeneralLib
{
    public class JudgeNumber
    {
        //ture   : is number
        //false  : not number

        /// <summary>
        /// 验证是否是数字
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool VerifyNumber(string text)
        {
            Regex reg = new Regex("^[0-9]+$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否是正整数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isPositiveInteger(string text)
        {
            Regex reg = new Regex("^[1-9]\\d*$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是负整数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isNegativeInteger(string text)
        {
            Regex reg = new Regex("^-[1-9]\\d*$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是整数（包括0）
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isWhloeNumber(string text)
        {
            if (text.Equals("0") || isPositiveInteger(text) || isNegativeInteger(text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是正十进制数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isPositiveDecimal(string text)
        {
            Regex reg = new Regex("^[0]\\.[1-9]*|^[1-9]\\d*\\.\\d*");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是负十进制数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isNegativeDecimal(string text)
        {
            Regex reg = new Regex("^-[0]\\.[1-9]*|^-[1-9]\\d*\\.\\d*");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是十进制数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool isDecimal(string text)
        {
            return isPositiveDecimal(text) || isNegativeDecimal(text);
        }

        /// <summary>
        /// 判断是否是十进制整数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool isRealNumber(string text)
        {
            return isWhloeNumber(text) || isDecimal(text);
        }

        /// <summary>
        /// 判断是否为浮点数
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static bool IsFloat(string text)
        {
            string regextext = @"^(-?\d+)(\.\d+)?$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(text);
        }
    }
}
