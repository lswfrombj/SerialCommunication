﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SerialCommunication
{
    public partial class ClockControl : UserControl
    {
        double xs = ((double)(Screen.PrimaryScreen.WorkingArea.Width) / 1140.0) > ((double)(Screen.PrimaryScreen.WorkingArea.Height) / 754.0) ? ((double)(Screen.PrimaryScreen.WorkingArea.Height) / 754.0) : ((double)(Screen.PrimaryScreen.WorkingArea.Width) / 1140.0);
//        const int screenWidth = (int)(xs * 176); //屏幕宽度
//        const int screenHeight = 176; //屏幕高度
        int screenWidth = 176; //屏幕宽度
        int screenHeight = 176; //屏幕高度

        public ClockControl()
        {
            InitializeComponent();

            screenWidth = (int)(xs * 176); //屏幕宽度
            screenHeight = (int)(xs * 176); //屏幕高度

            this.Width = screenWidth + 1;
            this.Height = screenHeight + 1;
            this.DoubleBuffered = true; //控件缓冲，避免闪烁
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            clockTimer.Start();
        }
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string dayOfWeek = dtNow.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));//星期几
            Brush brush = new SolidBrush(Color.Black); //填充图形
            Pen pen = new Pen(Color.Black, (int)(xs * 1)); //画笔
            Font hourFont = new Font("Arial", (int)(xs * 10), FontStyle.Bold);//时钟数字的字体
            Font dateFont = new Font("Arial", (int)(xs * 9)); //日期的字体
            int dialRadius = Math.Min(screenWidth, screenHeight) / 2; //圆的半径

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            //默认坐标系统原点是左上角，现在把原点移到屏幕中心, 右下左上对应的轴：x,y,-x,-y
            g.TranslateTransform(dialRadius, dialRadius);

            //画时钟最外层的圆线(pen,x,y,width,height)
            //圆的中心点坐标计算：(width/2+x,height/2+y),据此可得出要使圆在坐标原点(0,0)的x,y坐标值           
            g.DrawEllipse(pen, -screenWidth / 2, -dialRadius, screenWidth, screenHeight);

            GraphicsState state = g.Save();
            //画矩形、日期、星期几       
            int rectWidth = (int)(xs * 64);
            int rectHeight = (int)(xs * 26);
            g.DrawRectangle(pen, -rectWidth / 2, rectHeight, rectWidth + 2 * 2, rectHeight + 2 * 2);
            g.DrawString(dtNow.ToString("yyyy-MM-dd"), dateFont, brush, -rectWidth / 2 + 2, rectHeight + 2);
            g.DrawString(dayOfWeek.PadLeft(8, ' '), dateFont, brush, -rectWidth / 2 + 2, rectHeight + (int)(xs * 15));
            g.Restore(state);

            // 画时钟的60个圆点
            //Save(),Restore(state)配合使用，使得平移、缩放、旋转等操作只对它们作用域之间的代码有效，
            //save开始到restore之间这绘画，就像有绘制了一个图层，restore之后将两个图层放到一起
            state = g.Save();
            for (int i = 0; i < 60; i++)
            {
                int w = i % 5 == 0 ? (int)(xs * 5) : (int)(xs * 3);
                g.FillEllipse(brush, 0, -dialRadius, w, w);
                //围绕指定点按照顺时针方向旋转角度360 / 60 = 6度
                g.RotateTransform(6);
            }
            g.Restore(state);

            //画时钟的12个数字，如果用上面RotateTransform方法则数字会倾斜、倒立，故不用
            state = g.Save();
            for (int i = 0; i < 12; i++)
            {
                //已知圆中心占坐标(x0,y0),半径r，角度a0,则圆上任一点坐标（x,y）计算：
                //x = x0 + r * cos(ao * 3.14 /180) 
                //y = y0 + r * sin(ao * 3.14 /180) 
                Point point = new Point(-(int)(xs * 6), -(int)(xs * 6)); //当为(0,0)时全部数字偏右下移，故手动调整
                double dd = Math.PI / 180 * i * (360 / 12); //每次转360/12度
                float x = point.X + (float)((dialRadius - (int)(xs * 12)) * Math.Cos(dd));
                float y = point.Y + (float)((dialRadius - (int)(xs * 12)) * Math.Sin(dd));

                //因为是从顺时钟3点钟开始画，所以索引i需要加上3
                int j = i + 3;
                if (j > 12)
                    j = j - 12;
                g.DrawString(j.ToString(), hourFont, brush, x, y);
            }
            g.Restore(state);

            // 画时钟的图形
            state = g.Save();
            g.RotateTransform((dtNow.Hour - 12 + dtNow.Minute / 60f) * 360f / 12f);
            //时钟指针默认指向12点钟方向,分钟指针也一样
            g.DrawPolygon(new Pen(brush), new Point[]
            {
                new Point(0,  (int)(xs * 3)), new Point( (int)(xs * 3), 0),
                new Point(0, -(int)(xs * 53)), new Point(-(int)(xs * 3), 0)
            });
            g.Restore(state);

            // 画分钟的图形
            state = g.Save();
            g.RotateTransform((dtNow.Minute + dtNow.Second / 60f) * 360f / 60f);
            g.DrawPolygon(new Pen(brush), new Point[]
            {
                new Point(0,  (int)(xs * 3)), new Point( (int)(xs * 2), 0),
                new Point(0, -(int)(xs * 70)), new Point(-(int)(xs * 2), 0)
            });
            g.Restore(state);

            // 画秒钟的图形
            state = g.Save();
            g.RotateTransform(dtNow.Second * 360f / 60f);
            g.FillRectangle(brush, -1, -dialRadius + 10, 2, dialRadius);
            g.Restore(state);
        }
    }
}
