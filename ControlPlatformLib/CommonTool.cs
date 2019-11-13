using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class CommonTool
    {
        /// <summary>
        /// 圆心计算
        /// </summary>
        /// <param name="x1">起始点X坐标</param>
        /// <param name="y1">起始点Y坐标</param>
        /// <param name="x2">终点X坐标</param>
        /// <param name="y2">终点Y坐标</param>
        /// <param name="dRadius">半径</param>
        /// <param name="dir">方向,true:顺时针，false:逆时针</param>
        static public void Circle_Center(double x1, double y1, double x2, double y2, double dRadius, short iCCW, ref double centerX, ref double centerY)
        {
            double k = 0.0, k_verticle = 0.0;
            double mid_X = 0.0, mid_Y = 0.0;
            double a = 1.0;
            double b = 1.0;
            double c = 1.0;
            double centerX1, centerX2, centerY1, centerY2;
            k = (y2 - y1) / (x2 - x1);
            if (k == 0)
            {
                centerX1 = (float)((x1 + x2) / 2.0);
                centerX2 = (float)((x1 + x2) / 2.0);
                centerY1 = (float)(y1 + System.Math.Sqrt(dRadius * dRadius - (x1 - x2) * (x1 - x2) / 4.0));
                centerY2 = (float)(y2 - System.Math.Sqrt(dRadius * dRadius - (x1 - x2) * (x1 - x2) / 4.0));
            }
            else
            {
                k_verticle = -1.0 / k;
                mid_X = (x1 + x2) / 2.0;
                mid_Y = (y1 + y2) / 2.0;
                a = 1.0 + k_verticle * k_verticle;
                b = -2 * mid_X - k_verticle * k_verticle * (x1 + x2);
                c = mid_X * mid_X + k_verticle * k_verticle * (x1 + x2) * (x1 + x2) / 4.0 -
                    (dRadius * dRadius - ((mid_X - x1) * (mid_X - x1) + (mid_Y - y1) * (mid_Y - y1)));

                centerX1 = (float)((-1.0 * b + System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                centerX2 = (float)((-1.0 * b - System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                centerY1 = (float)Y_Coordinates(mid_X, mid_Y, k_verticle, centerX1);
                centerY2 = (float)Y_Coordinates(mid_X, mid_Y, k_verticle, centerX2);
            }
            if (iCCW==0)
            {
                centerX = centerX1;
                centerY = centerY1;
            }
            else
            {
                centerX = centerX2;
                centerY = centerY2;
            }
        }

        static double Y_Coordinates(double x, double y, double k, double x0)
        {
            return k * x0 - k * x + y;
        }
    }
}
