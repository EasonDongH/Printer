using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrintFun;

namespace PrintDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //关联打印对象的事件
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.LotteryPrintPage);
        }
        private PrintDocument printDoc = new PrintDocument();//创建打印对象


        private void button1_Click(object sender, EventArgs e)
        {
            this.printDoc.Print();
        }

        //具体打印实现过程
        //private void LotteryPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    int left = 2; //打印区域的左边界
        //    int top = 70;//打印区域的上边界
        //    Font titlefont = new Font("仿宋", 10);//标题字体
        //    Font font = new Font("仿宋", 8);//内容字体     
        //    Printer objPrint = new Printer(e);

        //    //生成彩票信息

        //    //条形码
        //    string serialNum = DateTime.Now.ToString("yyyyMMddHHmmssms");//流水号（生成条码使用）
        //    objPrint.DrawBarCode(serialNum, 20, 5);
        //    //标题
        //    objPrint.DrawContent("天津百万奖彩票中心", titlefont, Brushes.Blue, left + 20, top);
        //    //分界线
        //    objPrint.DrawDoundary((int)left - 2, (int)top + 20, (int)left + (int)180, (int)top + 20, Color.Green, 1);

        //    //写入内容
        //    List<string> num = new List<string>();
        //    num.Add("4  9  6  1  0  3    9");
        //    num.Add("7  0  1  2  3  5    5");
        //    num.Add("7  1  3  2  4  6    5");
        //    num.Add("5  7  8  2  7  8    5");
        //    num.Add("2  1  2  1  1  6    9");

        //    objPrint.DrawContent(num, font, Brushes.Blue, left, (int)(top + titlefont.GetHeight(e.Graphics)));

        //    //分界线
        //    float topPoint = titlefont.GetHeight(e.Graphics) + font.GetHeight(e.Graphics) * (num.Count) + 22;
        //    objPrint.DrawDoundary((int)left - 2, (int)top + (int)topPoint, (int)left + (int)180, (int)top + (int)topPoint, Color.Green, 1);

        //    string time = "购买时间：" + DateTime.Now.ToString("yyy-MM-dd  HH:mm:ss");
        //    objPrint.DrawContent(time, font, Brushes.Blue, left, top + (int)titlefont.GetHeight(e.Graphics)
        //        + (int)font.GetHeight(e.Graphics) * (num.Count + 1) + 12);

        //    //二维码图片left和top坐标
        //    int qrcodetop = (int)(top + titlefont.GetHeight(e.Graphics) + font.GetHeight(e.Graphics) * (num.Count + 3) + 12);
        //    int qrcodeleft = (int)left + 32;
        //    int height = objPrint.DrawQRCodeBmp("http://www.baidu.com", qrcodeleft, qrcodetop);

        //    objPrint.DrawContent("扫描二维码可直接查询兑奖结果", font, Brushes.Blue, left, qrcodetop + height + 10);
        //}


        /// <summary>
        /// 全部使用默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void LotteryPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    Printer objPrint = new Printer(e);

        //    //生成彩票信息
        //    //条形码
        //    string serialNum = DateTime.Now.ToString("yyyyMMddHHmmssms");//流水号（生成条码使用）
        //    objPrint.DrawBarCode(serialNum);
        //    //标题
        //    objPrint.DrawContent("天津百万奖彩票中心");
        //    //分界线
        //    objPrint.DrawDoundary(100);
        //    //写入内容
        //    List<string> num = new List<string>();
        //    num.Add("4  9  6  1  0  3    9");
        //    num.Add("7  0  1  2  3  5    5");
        //    num.Add("7  1  3  2  4  6    5");
        //    num.Add("5  7  8  2  7  8    5");
        //    num.Add("2  1  2  1  1  6    9");
        //    objPrint.DrawContent(num);
        //    //分界线            
        //    objPrint.DrawDoundary(100);
        //    //购买时间
        //    string time = "购买时间：" + DateTime.Now.ToString("yyy-MM-dd  HH:mm:ss");
        //    objPrint.DrawContent(time);
        //    //二维码图片         
        //    int height = objPrint.DrawQRCodeBmp("http://www.baidu.com");
        //    objPrint.DrawContent("扫描二维码可直接查询兑奖结果");
        //}


        private void LotteryPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Printer objPrint = new Printer(e);

            //生成彩票信息
            //条形码
            string serialNum = DateTime.Now.ToString("yyyyMMddHHmmssms");//流水号（生成条码使用）
            objPrint.DrawBarCode(serialNum);
            //标题
            objPrint.DrawContent("中国百万奖彩票中心",new Font("仿宋",12),Brushes.Black,25);
            //分界线
            objPrint.DrawDoundary(150);
            //写入内容
            List<string> num = new List<string>();
            num.Add("4  9  6  1  0  3    9");
            num.Add("7  0  1  2  3  5    5");
            num.Add("7  1  3  2  4  6    5");
            num.Add("5  7  8  2  7  8    5");
            num.Add("2  1  2  1  1  6    9");
            objPrint.DrawContent(num);
            //分界线            
            objPrint.DrawDoundary(150);
            //购买时间
            string time = "购买时间：" + DateTime.Now.ToString("yyy-MM-dd  HH:mm:ss");
            objPrint.DrawContent(time);
            //二维码图片         
            int height = objPrint.DrawQRCodeBmp("http://www.baidu.com",40);
            objPrint.DrawContent("扫描二维码可直接查询兑奖结果");
        }
    }
}
