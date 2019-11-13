
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace ProFrame
{
    public partial class CurveGraph : UserControl
    {
        public CurveGraph()
        {
            InitializeComponent();
            OldXMax = xMaxValue;
        }

        private Object thisLock = new Object();
        public DateTime StartTime ;
        public float OldXMax = 0;

        public float LastScaleHeight = 0;

        #region 曲线数据定义

        #region 标题定义
        private string title = "CurveGraph";
        /// <summary>
        /// 定义曲线窗体标题
        /// </summary>
        public string CarveTitle
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
        
        private string xString = "X轴";
        /// <summary>
        /// 定义曲线X轴标题
        /// </summary>
        public string XAxisTitle
        {
            get
            {
                return xString;
            }
            set
            {
                xString = value;
            }
        }

        private string yString = "Y轴";
        /// <summary>
        /// 定义曲线Y轴标题
        /// </summary>
        public string YAxisTitle
        {
            get
            {
                return yString;
            }
            set
            {
                yString = value;
            }
        }

        private Color titleColor = Color.Yellow;
        /// <summary>
        /// 定义标题颜色
        /// </summary>
        public Color TitleColor
        {
            get
            {
                return this.titleColor;
            }
            set
            {
                this.titleColor = value;
            }
        }

        private Font titleFont = new Font("Arial", 12);
        /// <summary>
        /// 定义标题字体
        /// </summary>
        public Font TitleFont
        {
            get 
            {
                return titleFont; 
            }
            set 
            {
                titleFont = value; 
            }
        }

        #endregion

        #region 刻度定义

        private float yMaxValue = 100.0F;
        /// <summary>
        /// 定义曲线Y轴最大值
        /// </summary>
        public float YMaxValue
        {
            get
            {
                return this.yMaxValue;
            }
            set
            {
                this.yMaxValue = value;
            }
        }

        private float yMinValue = 0.0F;
        /// <summary>
        /// 定义曲线Y轴最小值
        /// </summary>
        public float YMinValue
        {
            get
            {
                return this.yMinValue;
            }
            set
            {
                this.yMinValue = value;
            }
        }

        private float xMaxValue = 100;
        /// <summary>
        /// 定义曲线X轴最大值
        /// </summary>
        public float XMaxValue
        {
            get 
            {
                return xMaxValue; 
            }
            set
            {
                xMaxValue = value;                
            }
        }

        private float xMinValue = 0;
        /// <summary>
        /// 定义曲线X轴最小值
        /// </summary>
        public float XMinValue
        {
            get
            {
                return xMinValue;
            }
            set
            {
                xMinValue = value;
            }
        }

        private Color axisColor = Color.Yellow;
        /// <summary>
        /// 定义轴的颜色
        /// </summary>
        public Color AxisColor
        {
            get 
            {
                return axisColor;
            }
            set
            {
                axisColor = value;
            }
        }

        private float xScale = 20;
        /// <summary>
        /// 定义X轴的刻度
        /// </summary>
        public float XScale
        {
            get 
            {
                return xScale; 
            }
            set
            {
                xScale = value; 
            }
        }

        private float yScale = 20;
        /// <summary>
        /// 定义Y轴的刻度
        /// </summary>
        public float YScale
        {
            get 
            {
                return yScale;
            }
            set
            {
                yScale = value;
            }
        }

        private int axisPrecision = 5;
        /// <summary>
        /// 定义轴的精度
        /// </summary>
        public int AxisPrecision
        {
            get 
            {
                return axisPrecision;
            }
            set
            {
                axisPrecision = value;
            }
        }

        #endregion

        #region 曲线偏移
        private float xOffset = 50F;
        /// <summary>
        /// 定义坐标零点向右偏移量
        /// </summary>
        public float XOffset
        {
            get 
            {
                return xOffset;
            }
            set
            {
                xOffset = value;
            }
        }

        private float yOffset = 50F;
        /// <summary>
        /// 定义坐标零点向上偏移量
        /// </summary>
        public float YOffset
        {
            get 
            {
                return yOffset; 
            }
            set 
            {
                yOffset = value;
            }
        }
        #endregion

        #region 背景设置
        private Color backGroundColor = Color.Black;
        /// <summary>
        /// 定义背景颜色
        /// </summary>
        public Color BackGroundColor
        {
            get
            {
                return this.backGroundColor;
            }
            set
            {
                this.backGroundColor = value;
            }
        }

        private Color gridColor = Color.DarkGreen;
        /// <summary>
        /// 定义背景网格线颜色
        /// </summary>
        public Color GridColor
        {
            get
            {
                return this.gridColor;
            }
            set
            {
                this.gridColor = value;
            }
        }

        private Color gridFontColor = Color.Yellow;
        /// <summary>
        /// 定义背景网格文字颜色
        /// </summary>
        public Color GridFontColor
        {
            get
            {
                return this.gridFontColor;
            }
            set
            {
                this.gridFontColor = value;
            }
        }

        private float gridFontSize = 9;
        /// <summary>
        /// 定义背景网格文字大小
        /// </summary>
        public float GridFontSize
        {
            get
            {
                return this.gridFontSize;
            }
            set
            {
                this.gridFontSize = value;
            }
        }

        private float gridPenWidth = 1F;
        /// <summary>
        /// 定义背景网格线画笔宽度
        /// </summary>
        public float GridPenWidth
        {
            get
            {
                return this.gridPenWidth;
            }
            set
            {
                this.gridPenWidth = value;
            }
        }

        /// <summary>
        /// 定义背景网格（分隔线）宽度
        /// </summary>
        private float gridCompart = 1F;
        /// <summary>
        /// 定义背景网格（分隔线）宽度
        /// </summary>
        public float GridCompart
        {
            get
            {
                return this.gridCompart;
            }
            set
            {
                this.gridCompart = value;
            }
        }

        #endregion

        #region 绘制曲线设置

        /// <summary>
        /// 定义曲线颜色
        /// </summary>
        private Color curveColor = Color.White;
        /// <summary>
        /// 定义曲线颜色
        /// </summary>
        public Color CurveColor
        {
            get
            {
                return this.curveColor;
            }
            set
            {
                this.curveColor = value;
            }
        }



        /// <summary>
        /// 定义曲线画笔宽度
        /// </summary>
        private float curvePenWidth = 1;
        /// <summary>
        /// 定义曲线画笔宽度
        /// </summary>
        public float CurvePenWidth
        {
            get
            {
                return this.curvePenWidth;
            }
            set
            {
                this.curvePenWidth = value;
            }
        }

        private float xYPrecision = 4F;
        /// <summary>
        /// 定义显示节点数值鼠标X，Y轴容差精度
        /// </summary>
        public float XYPrecision
        {
            get
            {
                return this.xYPrecision;
            }
            set
            {
                this.xYPrecision = value;
            }
        }

        #endregion

        #region 原点定义
        private float xOrigin = 0;
        /// <summary>
        /// 坐标轴X原点
        /// </summary>
        public float XOrigin
        {
            get 
            {
                return xOrigin;
            }
            set
            {
                xOrigin = value;
            }
        }

        private float yOrigin = 0F;
        /// <summary>
        /// 坐标轴Y原点
        /// </summary>
        public float YOrigin
        {
            get 
            {
                return yOrigin;
            }
            set
            {
                yOrigin = value;
            }
        }
        #endregion







        /// <summary>
        /// 定义数值节点正方形宽度
        /// </summary>
        private float rectangleWidth = 1F;


        /// <summary>
        /// 定义正方形颜色
        /// </summary>
        private Color rectangleColor = Color.White;




        ///// <summary>
        ///// 曲线节点数据最大存储量
        ///// </summary>
        //private int maxNote = 1000;


        #endregion
       
        #region 公共属性
  





        /// <summary>
        /// 定义数值节点正方形宽度
        /// </summary>
        public float CarveRectangleWidth
        {
            get
            {
                return this.rectangleWidth;
            }
            set
            {
                this.rectangleWidth = value;
            }
        }

        /// <summary>
        /// 定义正方形颜色
        /// </summary>
        public Color CarveRectangleColor
        {
            get
            {
                return this.rectangleColor;
            }
            set
            {
                this.rectangleColor = value;
            }
        }

        #endregion

        #region 全局变量

        ///// <summary>
        ///// 背景方格移动量
        ///// </summary>
        //private float gridRemoveX = 1;

        /// <summary>
        /// 鼠标X，Y 坐标值，及该点坐标记录值、记录时间（数组）
        /// </summary>
        private List<CoordinatesValue> noteMessages = new List<CoordinatesValue>();

        #endregion

        #region 曲线数据显示


        #region 绘制背景网格

        private Bitmap createBackGround()
        {
            if (OldXMax == 0 || OldXMax == 100)
            {
                OldXMax=XMaxValue;
            }
            if (this.picCurveShow.Height < 1 || this.picCurveShow.Width < 1)
            {
                return null;
            }
            //Bitmap bitmap = new Bitmap(this.picCurveShow.Width, this.picCurveShow.Height);
            //Graphics backGroundImage = Graphics.FromImage(bitmap);
            Graphics backGroundImage;
            Bitmap bitmap;
            //创建Grape
            try
            {                
                bitmap = new Bitmap(this.picCurveShow.Width, this.picCurveShow.Height);
                backGroundImage = Graphics.FromImage(bitmap);
            }
            catch (System.Exception )
            {
                return null;
            }

            //坐标原点偏移
            backGroundImage.TranslateTransform(this.XOffset, 0);
            //填充背景色
            backGroundImage.Clear(this.backGroundColor);

            //创建网格背景线画笔
            Pen gridPen = new Pen(this.gridColor, this.gridPenWidth);

            //绘制背景横轴线

            float gridHeight = yScale / (yMaxValue - yMinValue) * (this.picCurveShow.Height - yOffset);
            if (gridHeight < 10)//0825
            {
                if (LastScaleHeight < 10)
                {
                    gridHeight = 45;
                }
                else
                {
                    gridHeight = LastScaleHeight;
                }
 
            }
            LastScaleHeight = gridHeight;
            float start = 0;
            float upEnd = 0;
            float downEnd = 0;

            if (yOrigin <= yMinValue)
            {
                start = downEnd = this.picCurveShow.Height - yOffset;
                upEnd = 0;
            }
            else if (yOrigin >= yMaxValue)
            {
                start = downEnd = this.picCurveShow.Height - yOffset;
                upEnd = 0;
            }
            else
            {
                downEnd = this.picCurveShow.Height - yOffset;
                upEnd = 0;
                start = downEnd - (yOrigin - yMinValue) / (yMaxValue - yMinValue) * downEnd;
            }

            //for (float i = start; i < this.picCurveShow.Height; i = i + gridHeight)
            //{
            //    PointF pointFBegin = new PointF(0F, i);
            //    PointF pointFEnd = new PointF(this.picCurveShow.Width, i);

            //    backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
            //}

            for (float i = start; i >= 0; i = i - gridHeight)
            {
                PointF pointFBegin = new PointF(0F, i);
                PointF pointFEnd = new PointF(this.picCurveShow.Width, i);

                backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
            }

            float gridWidth = xScale / (xMaxValue - xMinValue) * (this.picCurveShow.Width - xOffset);
            float X_start = 0;
            float leftEnd = 0;
            float rightEnd = 0;

            if (xOrigin < XMinValue)
            {
                X_start = leftEnd = 0;
                rightEnd = this.picCurveShow.Width;
            }
            else if (xOrigin > XMaxValue)
            {
                X_start = leftEnd = 0;
                rightEnd = this.picCurveShow.Width;
            }
            else
            {
                X_start = (xOrigin - XMinValue) / (XMaxValue - XMinValue) * (this.picCurveShow.Width - XOffset);
                leftEnd = 0;
                rightEnd = this.picCurveShow.Width;
            }

            for (float i = X_start; i <= rightEnd; i = i + gridWidth)
            {
                PointF pointFBegin = new PointF(i, 0F);
                PointF pointFEnd = new PointF(i, this.picCurveShow.Height - YOffset);

                backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
            }

            for (float i = X_start; i >= leftEnd; i = i - gridWidth)
            {
                PointF pointFBegin = new PointF(i, 0F);
                PointF pointFEnd = new PointF(i, this.picCurveShow.Height - YOffset);

                backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
            }
           // return bitmap;
            //创建网格文字画笔
            Pen gridCompartPen = new Pen(this.gridFontColor, this.gridCompart);

            //绘制表格背景色

            //绘制分割线
            //绘制Y轴
            backGroundImage.DrawLine(gridCompartPen, X_start, upEnd, X_start, downEnd);
            //绘制X轴
            backGroundImage.DrawLine(gridCompartPen, 0, start, this.picCurveShow.Width, start);  

            //绘制分割线刻度文字
            Font backGroungFont = new Font("Arial", this.gridFontSize);
            float fontHight = backGroungFont.GetHeight();

            SolidBrush brush = new SolidBrush(this.gridFontColor);

            float widthscale = gridWidth / axisPrecision;
            int index = 0;

            for (float i = X_start; i <= rightEnd; i = i + widthscale)
            {
                PointF point1 = new PointF(i, start);
                PointF point2 = new PointF(i, start + 10);

                if (index % axisPrecision == 0)
                {
                    string cord = String.Format("{0}", xOrigin + index / AxisPrecision * xScale);
                    backGroundImage.DrawString(cord, backGroungFont, brush, i, start + 10);
                    point2 = new PointF(i, start + 25);
                }

                backGroundImage.DrawLine(gridCompartPen, point1, point2);
                index++;
            }

            index = 0;

            for (float i = X_start; i >= leftEnd; i = i - widthscale)
            {
                PointF point1 = new PointF(i, start);
                PointF point2 = new PointF(i, start + 10);

                if (index % axisPrecision == 0)
                {
                    string cord = String.Format("{0}", xOrigin - index / AxisPrecision * xScale);
                    backGroundImage.DrawString(cord, backGroungFont, brush, i, start + 10);
                    point2 = new PointF(i, start + 25);
                }

                backGroundImage.DrawLine(gridCompartPen, point1, point2);
                index++;
            }


            index = 0;
            float hightscale = gridHeight / axisPrecision;

            for (float i = start; i >= upEnd; i = i - hightscale)
            {
                PointF point1 = new PointF(X_start, i);
                PointF point2 = new PointF(X_start - 10, i);

                if (index % axisPrecision == 0)
                {
                    string cord = String.Format("{0}", yOrigin + index / axisPrecision * YScale );

                    backGroundImage.DrawString(cord, backGroungFont, brush, X_start - 45, i);

                    point2 = new PointF(X_start - 20, i);
                }

                backGroundImage.DrawLine(gridCompartPen, point1, point2);
                index++;
            }

            index = 0;
            if (hightscale < 0) hightscale = -hightscale;
            for (float i = start; i <= downEnd; i = i + hightscale)
            {
                PointF point1 = new PointF(X_start, i);
                PointF point2 = new PointF(X_start - 10, i);

                if (index % axisPrecision == 0)
                {
                    string cord = String.Format("{0}", yOrigin - index / axisPrecision * YScale);

                    backGroundImage.DrawString(cord, backGroungFont, brush, X_start - 45, i);

                    point2 = new PointF(X_start - 20, i);
                }
                if (i < -500)
                {
                    break;//0820
                }
                backGroundImage.DrawLine(gridCompartPen, point1, point2);
                index++;
            }

            //绘制边界
            backGroundImage.DrawLine(gridCompartPen, 0, upEnd, 0, downEnd);
            backGroundImage.DrawLine(gridCompartPen, 0, downEnd, this.picCurveShow.Width, downEnd);  

            //绘制曲线窗体标题
            brush = new SolidBrush(this.titleColor);
            backGroundImage.DrawString(this.title, backGroungFont, brush, (this.picCurveShow.Width / 2 - this.title.Length * this.gridFontSize), 0);
            
            //单位标识
            Font backFont = new Font("宋体", this.gridFontSize);
            brush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))));
            backGroundImage.DrawString("时间", backFont, brush, (this.picCurveShow.Width / 2 - this.title.Length * this.gridFontSize-50), this.picCurveShow.Height-18);
            backGroundImage.DrawString("功率", backFont, brush,-45, this.picCurveShow.Location.Y +25);
            
            return bitmap;
        }

        /// <summary>
        /// 刷新背景网格线，并返回背景图片（背景判断是否滚动）
        /// </summary>
        /// <returns>返回背景图片</returns>
        private Bitmap RefAndRemoveBackground()
        {
            return this.createBackGround();
        }
        #endregion

        /// <summary>
        /// 刷新背景网格线，显示曲线
        /// </summary>
        public void ShowCurve()
        {
            Bitmap bitmap = this.RefAndRemoveBackground();
            if (bitmap == null)
            {
                return;
            } 
           
            Graphics graphics = Graphics.FromImage(bitmap);

            if (this.noteMessages.Count >= 2)
            {
                PointF[] points = new PointF[this.noteMessages.Count];

                for (int i = 0; i < this.noteMessages.Count; i++)
                {
                    points[i].X = this.noteMessages[i].X;
                    points[i].Y = this.noteMessages[i].Y;
                }

                graphics.DrawLines(new Pen(this.curveColor, this.curvePenWidth), points);
            }
            try
            {
                this.picCurveShow.Image = bitmap;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);               
            }
            
        }

        public void Clean()
        {
            this.noteMessages.Clear();
            XMaxValue = OldXMax;
            this.ShowCurve();
        }

        /// <summary>
        /// 刷新背景网格线，显示曲线，自动添加即时数值
        /// </summary>
        /// <param name="value">即时数值</param>
        public void ShowCurve(float value,float angle)
        {
            if (this.noteMessages.Count < 1)
            {
                OldXMax=XMaxValue;
                XScale = XMaxValue / 10;
                //StartTime = DateTime.Now;
            }
            this.AddNewValue(value,angle);
            this.ShowCurve();
        }
        #endregion

        #region 自动将最新采样值添加到数组

        /// <summary>
        /// 自动将最新采样数值添加到数组
        /// </summary>
        /// <param name="newValue">最新采样数值</param>
        public void AddNewValue(float newValue,float newAngle)
        {
            CoordinatesValue value;
            value.Value = newValue;
            //value.time = System.DateTime.Now;
            //TimeSpan sPan = value.time - StartTime;
            //value.getSecond = (value.time - StartTime).TotalSeconds;
            value.Angle = newAngle;
            if (value.Angle > XMaxValue)
            {
                //OldXMax = XMaxValue;0
                XMaxValue = (int)xMaxValue + 500;

                //if(XMaxValue%100==0)
                XScale = XMaxValue / 10;

                for (int i = 0; i < this.noteMessages.Count; i++)
                {

                    CoordinatesValue value1 = this.noteMessages[i];
                    value1.X = xOffset + ((float)value1.Angle - XMinValue) / (XMaxValue - XMinValue) * (this.picCurveShow.Width - XOffset);
                    noteMessages[i] = value1;
                }
            }

            value.X =  xOffset + ((float)value.Angle - XMinValue) / (XMaxValue - XMinValue) * (this.picCurveShow.Width - XOffset);
            value.Y = this.picCurveShow.Height - YOffset - (newValue - yMinValue) / (yMaxValue - yMinValue) * (this.picCurveShow.Height - yOffset);
            value.ID = this.noteMessages.Count;
           

            lock (thisLock)
            {
                this.noteMessages.Add(value);
            }
        }

        #endregion

        #region 显示鼠标当前点坐标值

        /// <summary>
        /// 显示鼠标当前点坐标值
        /// </summary>
        /// <param name="X">鼠标X坐标</param>
        /// <param name="Y">鼠标Y坐标</param>
        private void ShowMouseCoordinateMessage(int X, int Y)
        {
            float x = (int)X;
            float y = (int)Y;

            //鼠标位置在偏移量右侧时发生
            //if (x >= this.coordinate)
            if (x >= this.XOffset)
            {
                try
                {
                    foreach (CoordinatesValue valueTemp in this.noteMessages)
                    {
                        if (((valueTemp.X <= (x + this.xYPrecision)) && (valueTemp.X >= (x - this.xYPrecision)))
                            && ((valueTemp.Y >= (y - this.xYPrecision)) && (valueTemp.Y <= (y + this.xYPrecision))))
                        {
                            this.labShowView.Text = "Count:" + valueTemp.ID.ToString() +
                                " Value:" + valueTemp.Value.ToString();


                            int labX;
                            int labY;

                            if (Y <= this.labShowView.Height)
                            {
                                labY = Y + this.labShowView.Height + 5;
                            }
                            else
                            {
                                labY = Y - this.labShowView.Height;
                            }

                            if (X >= this.picCurveShow.Width - this.labShowView.Width)
                            {
                                labX = X - this.labShowView.Width;
                            }
                            else
                            {
                                labX = X + 5;
                            }

                            this.labShowView.Top = labY;
                            this.labShowView.Left = labX;
                            this.labShowView.BringToFront();
                            this.labShowView.Visible = true;

                            return;
                        }
                    }
                }
                catch (System.Exception)
                {
                    
                }
               
            }

            this.labShowView.Visible = false;
        }

        #endregion

        #region 定义鼠标X，Y坐标值，及该点坐标记录，记录时间
        /// <summary>
        /// 定义鼠标X，Y坐标值，及该点坐标记录，记录时间
        /// </summary>
        struct CoordinatesValue
        {
            public float X;
            public float Y;
            public float Value;
            public int ID;
            //public System.DateTime time;
            public float Angle;
            //public double getSecond; 
        }
        #endregion

        private void picCurveShow_MouseLeave(object sender, EventArgs e)
        {
            //鼠标离开曲线控件
            this.labShowView.Visible = false;
        }

        private void picCurveShow_MouseMove(object sender, MouseEventArgs e)
        {
            //移动鼠标
            this.ShowMouseCoordinateMessage(e.X, e.Y);
        }

        private void CurveGraph_Load(object sender, EventArgs e)
        {
            this.ShowCurve();

            labShowView.Visible = false;
        }

        public void Save(string name)
        {
            Bitmap bitmap = this.RefAndRemoveBackground();
            if (bitmap==null)
            {
                return;
                //Graphics graphics = Graphics.FromImage(bitmap);
            }
           
            Graphics graphics = Graphics.FromImage(bitmap);

            if (this.noteMessages.Count >= 2)
            {
                PointF[] points = new PointF[this.noteMessages.Count];

                for (int i = 0; i < this.noteMessages.Count; i++)
                {
                    points[i].X = this.noteMessages[i].X;
                    points[i].Y = this.noteMessages[i].Y;
                }

                graphics.DrawLines(new Pen(this.curveColor, this.curvePenWidth), points);
            }
            try
            {
                bitmap.Save(name);
            }
            catch (System.Exception )
            {
            	
            }
            
            graphics.Dispose();
        }

        private void picCurveShow_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 1 || this.Height < 1)
            {
                return;
            }
            if (this.noteMessages.Count>0)
            {
                for (int i = 0; i < this.noteMessages.Count; i++)
                {

                    CoordinatesValue value1 = this.noteMessages[i];
                    value1.X = xOffset + ((float)value1.Angle - XMinValue) / (XMaxValue - XMinValue) * (this.picCurveShow.Width - XOffset);
                    value1.Y = this.picCurveShow.Height - YOffset - (value1.Value - yMinValue) / (yMaxValue - yMinValue) * (this.picCurveShow.Height - yOffset);
                    noteMessages[i] = value1;
                }
            }
           
            //ShowCurve();\\0820
        }
    }
}
