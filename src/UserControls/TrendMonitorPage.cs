using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI
{
    public partial class TrendMonitorPage : UserControl
    {
        private MainWindow mainWindow;
        private List<PointF> dataPoints1;
        private List<PointF> dataPoints2;
        private List<PointF> dataPoints3;
        private List<PointF> dataPoints4;

        public TrendMonitorPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainWindow.TimerTicked += MainWindow_TimerTicked;
            this.Resize += TrendMonitorPage_Resize; 
            dataPoints1 = new List<PointF>();
            dataPoints2 = new List<PointF>();
            dataPoints3 = new List<PointF>();
            dataPoints4 = new List<PointF>();
        }

        private void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][1];
                dataPoints1.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints1,pictureBox1);

            dataPoints2.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][2];
                dataPoints2.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints2, pictureBox2);

            dataPoints3.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][3];
                dataPoints3.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints3, pictureBox3);

            //添加一个随时间变化的正交曲线
            dataPoints4.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0]/100;
                float y = (float)Math.Sin(x);
                dataPoints4.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints4, pictureBox4);

        }

        private void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            DrawGraph(dataPoints1, pictureBox1);
            DrawGraph(dataPoints2, pictureBox2);
            DrawGraph(dataPoints3, pictureBox3);
            DrawGraph(dataPoints4, pictureBox4);
        }

        private void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox)
        {
            if (pictureBox.Width == 0 || pictureBox.Height == 0 || dataPoints.Count == 0)
                return;

            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // 计算数据点的最大值和最小值
                float minX = dataPoints.Min(p => p.X);
                float maxX = dataPoints.Max(p => p.X);
                float minY = dataPoints.Min(p => p.Y);
                float maxY = dataPoints.Max(p => p.Y);

                // 如果 minY 和 maxY 相等，设置一个默认范围
                if (minY == maxY)
                {
                    minY -= 1;
                    maxY += 1;
                }

                // 设置坐标轴
                Pen axisPen = new Pen(Color.Black, 2);
                g.DrawLine(axisPen, 50, 10, 50, pictureBox.Height - 50); // Y轴
                g.DrawLine(axisPen, 50, pictureBox.Height - 50, pictureBox.Width - 10, pictureBox.Height - 50); // X轴

                // 绘制网格线和坐标标签
                Pen gridPen = new Pen(Color.LightGray, 1);
                Font labelFont = new Font("Arial", 8);
                for (int i = 50; i < pictureBox.Width - 10; i += 40) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10, i, pictureBox.Height - 50);
                    float xValue = minX + (i - 50) / (float)(pictureBox.Width - 60) * (maxX - minX);
                    g.DrawString(xValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(i, pictureBox.Height - 45));
                }
                for (int i = 10; i < pictureBox.Height - 50; i += 20)
                {
                    g.DrawLine(gridPen, 50, i, pictureBox.Width - 10, i);
                    float yValue = maxY - (i - 10) / (float)(pictureBox.Height - 60) * (maxY - minY);
                    g.DrawString(yValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(5, i - 5));
                }

                // 绘制数据点
                if (dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.Blue, 2);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(50 + (dataPoints[i - 1].X - minX) / (maxX - minX) * (pictureBox.Width - 60),
                                               pictureBox.Height - 50 - (dataPoints[i - 1].Y - minY) / (maxY - minY) * (pictureBox.Height - 60));
                        PointF p2 = new PointF(50 + (dataPoints[i].X - minX) / (maxX - minX) * (pictureBox.Width - 60),
                                               pictureBox.Height - 50 - (dataPoints[i].Y - minY) / (maxY - minY) * (pictureBox.Height - 60));
                        g.DrawLine(dataPen, p1, p2);
                    }
                }

                // 绘制标题
                //Font titleFont = new Font("Arial", 14, FontStyle.Bold);
                //g.DrawString("", titleFont, Brushes.Black, new PointF(pictureBox.Width / 2 - 50, 10));

                // 绘制坐标轴标签
                //g.DrawString("X轴", labelFont, Brushes.Black, new PointF(pictureBox.Width - 30, pictureBox.Height - 40));
                //g.DrawString("Y轴", labelFont, Brushes.Black, new PointF(10, 20));
            }

            pictureBox.Image = bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
