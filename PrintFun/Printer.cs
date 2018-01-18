using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;

namespace PrintFun
{
    public class Printer
    {
        private System.Drawing.Printing.PrintPageEventArgs e;
        private int Top = 20;//提供一个基准Top，用户不提供Top时，就取该Top，每次有内容增加都会刷新
        private int Left = 20;//提供一个基准Left，用户不提供Left时，就取该Left，每次有内容增加都会刷新
        private int Interval = 5;//提供一个基准间隔值

        #region 构造函数重载
        /// <summary>
        /// 打印类
        /// 所需参数：System.Drawing.Printing.PrintPageEventArgs；第一行内容距左Left值；第一行内容距上Top值；每行内容间隔值
        /// 默认值：无
        /// </summary>
        /// <param name="e">提供打印数据</param>
        /// <param name="Left">指定第一行数据的x</param>
        /// <param name="Top">指定第一行数据的y</param>
        /// <param name="Interval">指定每行数据间隔值，默认：20像素</param>
        public Printer(System.Drawing.Printing.PrintPageEventArgs e, int Left, int Top,int Interval=20)
        {
            this.e = e;
            this.Left = Left;
            this.Top = Top;
            this.Interval = Interval;
        }

        /// <summary>
        /// 打印类
        /// 所需参数：System.Drawing.Printing.PrintPageEventArgs；每行内容间隔值
        /// 默认值：第一行内容距左Left值：25像素；第一行内容距上Top值：25像素
        /// </summary>
        /// <param name="e">提供打印数据</param>
        /// <param name="Interval">指定每行数据间隔值，默认：20像素</param>
        public Printer(System.Drawing.Printing.PrintPageEventArgs e, int Interval)
        {
            this.e = e;            
            this.Interval = Interval;
        }

        /// <summary>
        /// 打印类
        /// 所需参数：System.Drawing.Printing.PrintPageEventArgs
        /// 默认值：每行内容间隔值：25像素；第一行内容距左Left值：25像素；第一行内容距上Top值：25像素
        /// </summary>
        /// <param name="e">提供打印数据</param>
        public Printer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.e = e;            
        }
        #endregion

        #region 根据流水号生成条形码

        /// <summary>
        /// 根据流水号生成条形码
        /// 所需参数：流水号；距左Left值；距上Top值；条形码宽度；条形码高度
        /// 默认值：无
        /// </summary>       
        /// <param name="serialNum">流水号</param>
        /// <param name="left">指定条形码x</param>
        /// <param name="top">指定条形码y</param>
        /// <param name="width">指定条形码宽度，默认值：240像素</param>
        /// <param name="height">指定条形码高度，默认值：50像素</param>
        public void DrawBarCode(string serialNum, int left, int top,int width,int height)
        {
            Fath.BarcodeX barCode = new Fath.BarcodeX();//创建条码生成对象
            //生成条形码
            barCode.Text = serialNum;//条码数据
            barCode.Symbology = Fath.bcType.Code128;//设置条码格式
            barCode.ShowText = true;//同时显示文本   

            e.Graphics.DrawImage(barCode.Image(width, height), new Point(left, top));//画条形码 
            
            this.Top += height+this.Interval;//默认相隔20像素
        }

        /// <summary>
        /// 根据流水号生成条形码
        /// 所需参数：流水号；距左Left值；距上Top值
        /// 默认值：条形码宽度：240像素；条形码高度：50像素
        /// </summary>
        /// <param name="serialNum">流水号</param>
        /// <param name="left">指定条形码x</param>
        /// <param name="top">指定条形码y</param>        
        public void DrawBarCode(string serialNum, int left, int top)
        {
            Fath.BarcodeX barCode = new Fath.BarcodeX();//创建条码生成对象
            //生成条形码
            barCode.Text = serialNum;//条码数据
            barCode.Symbology = Fath.bcType.Code128;//设置条码格式
            barCode.ShowText = true;//同时显示文本   

            e.Graphics.DrawImage(barCode.Image(240, 50), new Point(left, top));//画条形码 
          
            this.Top += 50+this.Interval;//默认相隔20像素
        }

        /// <summary>
        /// 根据流水号生成条形码      
        /// 所需参数：流水号
        /// 默认值：距左Left值：25像素；距上Top值：顺位值；条形码宽度：240像素；条形码高度：50像素
        /// </summary>
        /// <param name="serialNum">流水号</param>       
        public void DrawBarCode(string serialNum)
        {
            Fath.BarcodeX barCode = new Fath.BarcodeX();//创建条码生成对象
            //生成条形码
            barCode.Text = serialNum;//条码数据
            barCode.Symbology = Fath.bcType.Code128;//设置条码格式
            barCode.ShowText = true;//同时显示文本   

            e.Graphics.DrawImage(barCode.Image(240, 50), new Point(this.Left, this.Top));//画条形码 

            this.Top += 50 + this.Interval;//默认相隔20像素
        }

        #endregion

        #region 根据链接生成二维码
        /// <summary>
        /// 根据链接获取二维码
        /// 所需参数：URL；距左Left值；距上Top值
        /// 默认值：无
        /// </summary>
        /// <param name="url">链接</param
        /// <param name="left">二维码：x</param>
        /// <param name="top">二维码：y</param>
        /// <returns>返回二维码图片的高度</returns>
        public int DrawQRCodeBmp(string url,int left,int top)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;            
            Image bmp = qrCodeEncoder.Encode(url);
            e.Graphics.DrawImage(bmp, new Point(left, top));//不同的URL图片大小不同，可以根据需要调整left坐标
           
            Top += bmp.Height + this.Interval;

            return bmp.Height;
        }

        /// <summary>
        /// 根据链接获取二维码
        /// 所需参数：URL；距左Left值
        /// 默认值：距上Top值：顺位值
        /// </summary>
        /// <param name="url">链接</param
        /// <param name="left">二维码：x</param>        
        /// <returns>返回二维码图片的高度</returns>
        public int DrawQRCodeBmp(string url, int left)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image bmp = qrCodeEncoder.Encode(url);
            e.Graphics.DrawImage(bmp, new Point(left, this.Top));//不同的URL图片大小不同，可以根据需要调整left坐标

            Top += bmp.Height + this.Interval;

            return bmp.Height;
        }
        /// <summary>
        /// 根据链接获取二维码
        /// 所需参数：URL
        /// 默认值：距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="url">链接</param>
        /// <returns></returns>
        public int DrawQRCodeBmp(string url)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image bmp = qrCodeEncoder.Encode(url);
            e.Graphics.DrawImage(bmp, new Point(Left, Top));//不同的URL图片大小不同，可以根据需要调整left坐标
           
            Top += bmp.Height + this.Interval;

            return bmp.Height;
        }
        #endregion

        #region 打印内容

        #region 打印单条内容
        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容；内容字体；内容字体颜色；距左Left值；距上Top值
        /// 默认值：无
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        /// <param name="left">打印区域的左边界</param>
        /// <param name="top">打印区域的上边界</param>
        public void DrawContent(string content, Font contentFont, Brush contentBrush, int left , int top )
        {
            this.e.Graphics.DrawString(content, contentFont, contentBrush, left, top, new StringFormat());
           
            Top += Convert.ToInt32(contentFont.GetHeight(e.Graphics)) + this.Interval;  
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容；内容字体；内容字体颜色；距左Left值
        /// 默认值：距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        /// <param name="left">打印区域的左边界</param>        
        public void DrawContent(string content, Font contentFont, Brush contentBrush, int left)
        {
            this.e.Graphics.DrawString(content, contentFont, contentBrush, left, this.Top, new StringFormat());

            Top += Convert.ToInt32(contentFont.GetHeight(e.Graphics)) + this.Interval; 
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容；内容字体；内容字体颜色
        /// 默认值：距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        public void DrawContent(string content, Font contentFont, Brush contentBrush)
        {
            this.e.Graphics.DrawString(content, contentFont, contentBrush, Left, Top, new StringFormat());
            
            Top += Convert.ToInt32(contentFont.GetHeight(e.Graphics)) + this.Interval;
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容；内容字体
        /// 默认值：字体：仿宋8号；内容字体颜色：黑色；距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>        
        public void DrawContent(string content, Font contentFont)
        {
            this.e.Graphics.DrawString(content, contentFont, Brushes.Black, Left, Top, new StringFormat());

            Top += Convert.ToInt32(contentFont.GetHeight(e.Graphics)) + this.Interval;
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容
        /// 默认值：字体：仿宋8号；内容字体颜色：黑色；距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体：默认仿宋，8号</param>
        /// <param name="contentBrush">内容字体颜色：默认黑色</param>
        public void DrawContent(string content)
        {
            Font contentFont = new Font("仿宋", 8);           
            this.e.Graphics.DrawString(content, contentFont, Brushes.Black, Left, Top, new StringFormat());

            Top += Convert.ToInt32(contentFont.GetHeight(e.Graphics)) + this.Interval;
        }
        #endregion

        #region 打印多条内容

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容集合；内容字体；内容字体颜色；距左Left值；距上Top值
        /// 默认值：无
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>       
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        /// <param name="left">打印区域的左边界</param>
        /// <param name="top">打印区域的上边界</param>
        public void DrawContent(List<string> contentList, Font contentFont,Brush contentBrush, int left, int top)
        {
            int height = Convert.ToInt32(contentFont.GetHeight(e.Graphics));
            int nextTop = Top + this.Interval;
            for (int i=0;i< contentList.Count;i++)
            {                
                this.e.Graphics.DrawString(contentList[i], contentFont, contentBrush, left, nextTop , new StringFormat());
                nextTop += height;
            }
            Top += height * contentList.Count + this.Interval ;
        }

        /// <summary>
        /// 打印内容：
        /// 所需参数：打印内容集合；内容字体；内容字体颜色
        /// 默认值：距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>        
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        public void DrawContent(List<string> contentList, Font contentFont, Brush contentBrush)
        {
            int height = Convert.ToInt32(contentFont.GetHeight(e.Graphics));
            int nextTop = Top + this.Interval;
            for (int i = 0; i < contentList.Count; i++)
            {              
                this.e.Graphics.DrawString(contentList[i], contentFont, contentBrush, Left, nextTop, new StringFormat());
                nextTop += height;
            }
            Top += height * contentList.Count + this.Interval ;
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容集合；内容字体
        /// 默认值：内容字体颜色：黑色；距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>
        /// <param name="contentBrush">内容字体颜色：默认黑色</param>
        public void DrawContent(List<string> contentList, Font contentFont)
        {
            int height = Convert.ToInt32(contentFont.GetHeight(e.Graphics));
            int nextTop = Top + this.Interval;
            for (int i = 0; i < contentList.Count; i++)
            {
                
                this.e.Graphics.DrawString(contentList[i], contentFont, Brushes.Black, Left, nextTop, new StringFormat());
                nextTop += height;
            }
            Top += height * contentList.Count + this.Interval ;
        }

        /// <summary>
        /// 打印内容
        /// 所需参数：打印内容集合
        /// 默认值：字体：仿宋8号；内容字体颜色：黑色；距左Left值：25像素；距上Top值：顺位值
        /// </summary>
        /// <param name="content">需要打印的内容</param>       
        public void DrawContent(List<string> contentList)
        {
            Font contentFont = new Font("仿宋", 8);
            int height = Convert.ToInt32(contentFont.GetHeight(e.Graphics));
            int nextTop = Top  + this.Interval;
            for (int i = 0; i < contentList.Count; i++)
            {                
                this.e.Graphics.DrawString(contentList[i], contentFont, Brushes.Black, Left, nextTop, new StringFormat());
                nextTop += height;
            }
            Top += height * contentList.Count + this.Interval ;
        }
        #endregion
        #endregion

        #region 分界线
        /// <summary>
        /// 分界线
        /// 所需参数：起点Left坐标；起点Top坐标；终点Left坐标；终点Top坐标；线颜色；线宽：1像素
        /// 默认值：线宽：1像素
        /// </summary>
        /// <param name="startPointLeft">分界线起点，起点与纸张左侧距离</param>
        /// <param name="startPointTop">分界线起点，起点与纸张上侧距离</param>
        /// <param name="endPointLeft">分界线终点，终点与纸张左侧距离</param>
        /// <param name="endPointTop">分界线终点，终点与纸张上侧距离</param>
        /// <param name="lineColor">分界线颜色</param>
        /// <param name="lineWidth">分界线宽度，默认值为：1</param>
        public void DrawDoundary(int startPointLeft, int startPointTop, int endPointLeft, int endPointTop, Color lineColor, int lineWidth = 1)
        {
            Pen pen = new Pen(lineColor, lineWidth);
            e.Graphics.DrawLine(pen, new Point(startPointLeft, startPointTop), new Point(endPointLeft, endPointTop));
            Top += lineWidth + this.Interval;
        }

        /// <summary>
        /// 分界线
        /// 所需参数：起点Left坐标；起点Top坐标；分界线长度；线颜色；线宽：1像素
        /// 默认值：线宽：1像素
        /// </summary>
        /// <param name="startPointLeft">分界线起点，起点与纸张左侧距离</param>
        /// <param name="startPointTop">分界线起点，起点与纸张上侧距离</param>
        /// <param name="lineLength">分界线长度</param>
        /// <param name="lineColor">分界线颜色</param>
        /// <param name="lineWidth">分界线宽度，默认值为：1</param>
        public void DrawDoundary(int startPointLeft, int startPointTop, int lineLength, Color lineColor, int lineWidth = 1)
        {
            Pen pen = new Pen(lineColor, lineWidth);
            e.Graphics.DrawLine(pen, new Point(startPointLeft, startPointTop), new Point(startPointLeft+lineLength, startPointTop));
            Top += lineWidth + this.Interval;
        }

        /// <summary>
        /// 分界线
        /// 所需参数：分界线长度；线颜色；线宽：1像素
        /// 默认值：距上Top值：顺位值；线宽：1像素
        /// </summary>
        /// <param name="lineLength">分界线长度</param>
        /// <param name="lineColor">分界线颜色</param>
        /// <param name="lineWidth">分界线宽度，默认值为：1</param>
        public void DrawDoundary(int lineLength, Color lineColor, int lineWidth = 1)
        {
            Pen pen = new Pen(lineColor, lineWidth);
            e.Graphics.DrawLine(pen, new Point(Left, Top), new Point(Left + lineLength, Top));
            Top += lineWidth + this.Interval;
        }

        /// <summary>
        /// 分界线
        /// 所需参数：分界线长度；线宽：1像素
        /// 默认值：距左Left值：25像素；距上Top值：顺位值；线宽：1像素
        /// </summary>
        /// <param name="lineLength">分界线长度</param>
        /// <param name="lineWidth">分界线宽度，默认值为：1</param>        
        public void DrawDoundary(int lineLength, int lineWidth = 1)
        {
            Pen pen = new Pen(Color.Green, lineWidth);
            e.Graphics.DrawLine(pen, new Point(Left, Top), new Point(Left + lineLength, Top));
            Top += lineWidth + this.Interval;
        }

        /// <summary>
        /// 分界线
        /// 所需参数：起点Left坐标；终点Left坐标；线宽：1像素
        /// 默认值：距上Top值：顺位值；线颜色：绿色；线宽：1像素
        /// </summary>
        /// <param name="startPointLeft">开始点</param>
        /// <param name="endPointLeft">结束点</param>
        /// <param name="lineWidth">线宽：默认1像素</param>
        /// <param name="lineColor">分界线颜色：默认绿色</param>
        public void DrawDoundary(int startPointLeft,int endPointLeft, int lineWidth = 1)
        {
            Pen pen = new Pen(Color.Green, lineWidth);
            e.Graphics.DrawLine(pen, new Point(startPointLeft, Top), new Point(endPointLeft, Top));
            Top += lineWidth + this.Interval;
        }

        /// <summary>
        /// 分界线
        /// 所需参数：起点Left坐标；终点Left坐标；线颜色；线宽：1像素
        /// 默认值：距上Top值：顺位值；线宽：1像素
        /// </summary>
        /// <param name="startPointLeft">开始点</param>
        /// <param name="endPointLeft">结束点</param>
        /// <param name="lineWidth">线宽：默认1像素</param>
        /// <param name="lineColor">分界线颜色</param>
        public void DrawDoundary(int startPointLeft, int endPointLeft, Color lineColor, int lineWidth = 1)
        {
            Pen pen = new Pen(lineColor, lineWidth);
            e.Graphics.DrawLine(pen, new Point(startPointLeft, Top), new Point(endPointLeft, Top));
            Top += lineWidth + this.Interval;
        }
        #endregion
    }
}
