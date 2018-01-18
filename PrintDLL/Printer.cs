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
        public Printer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.e = e;
        }

        #region 根据流水号生成条形码
        /// <summary>
        /// 根据流水号生成条形码
        /// </summary>
        /// <param name="e"></param>
        /// <param name="serialNum"></param>
        public Image DrawBarCode(string serialNum)
        {
            Fath.BarcodeX barCode = new Fath.BarcodeX();//创建条码生成对象
            //生成条形码
            barCode.Text = serialNum;//条码数据
            barCode.Symbology = Fath.bcType.Code128;//设置条码格式
            barCode.ShowText = true;//同时显示文本   

            return barCode.Image(240, 50);
            //e.Graphics.DrawImage(barCode.Image(240, 50), new Point(20, 5));//画条形码 
        }
        #endregion

        #region 根据链接生成二维码
        /// <summary>
        /// 根据链接获取二维码
        /// </summary>
        /// <param name="url">链接</param>
        /// <returns>返回二维码图片</returns>
        public Image DrawQRCodeBmp(string url)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            return qrCodeEncoder.Encode(url);
        }
        #endregion

        #region 打印内容
        /// <summary>
        /// 打印内容
        /// </summary>
        /// <param name="content">需要打印的内容</param>
        /// <param name="contentFont">内容字体</param>
        /// <param name="contentBrush">内容字体颜色，提供Brushes.Color格式</param>
        /// <param name="left">打印区域的左边界，默认值为：22</param>
        /// <param name="top">打印区域的上边界，默认值为：70</param>
        public void DrawContent(string content, Font contentFont,Brush contentBrush, float left = 22, float top = 70)
        {
            this.e.Graphics.DrawString(content, contentFont, contentBrush, left, top, new StringFormat());
        }

        public void DrawContent(List<string> content, Font contentFont, Brush contentBrush, float left = 22, float top = 70)
        {
            foreach (var item in content)
            {
                this.e.Graphics.DrawString(item, contentFont, contentBrush, left, top, new StringFormat());
            }          
        }
        #endregion

        #region 分界线
        /// <summary>
        /// 分界线
        /// </summary>
        /// <param name="startPointLeft">分界线起点，起点与纸张左侧距离</param>
        /// <param name="startPointTop">分界线起点，起点与纸张上侧距离</param>
        /// <param name="endPointLeft">分界线终点，终点与纸张左侧距离</param>
        /// <param name="endPointTop">分界线终点，终点与纸张上侧距离</param>
        /// <param name="lineColor">分界线颜色</param>
        /// <param name="lineWidth">分界线宽度，默认值为：1</param>
        public void DrawDoundary(int startPointLeft,int startPointTop, int endPointLeft,int endPointTop,Color lineColor,int lineWidth=1)
        {            
            Pen pen = new Pen(lineColor, lineWidth);
            e.Graphics.DrawLine(pen, new Point(startPointLeft, startPointTop), new Point(endPointLeft, endPointTop));
        }
        #endregion
    }
}
