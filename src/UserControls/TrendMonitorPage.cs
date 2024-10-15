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
        private List<PointF> dataPoints;

        public TrendMonitorPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainWindow.TimerTicked += MainWindow_TimerTicked;
            this.Resize += TrendMonitorPage_Resize; // 添加Resize事件处理程序
            dataPoints = new List<PointF>();
        }

        private void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            // 清空现有数据点
            dataPoints.Clear();

            // 假设 Data.result.result 是一个二维数组或列表
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][1];
                dataPoints.Add(new PointF(x, y));
            }

            // 重新绘制图形
            DrawGraph();
        }

        private void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            DrawGraph();
        }

        private void DrawGraph()
        {
            if (pictureBox1.Width == 0 || pictureBox1.Height == 0|| dataPoints.Count == 0)
                return;

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // 计算数据点的最大值和最小值
                float minX = dataPoints.Min(p => p.X);
                float maxX = dataPoints.Max(p => p.X);
                float minY = dataPoints.Min(p => p.Y);
                float maxY = dataPoints.Max(p => p.Y);

                // 设置坐标轴
                Pen axisPen = new Pen(Color.Black, 2);
                g.DrawLine(axisPen, 50, 10, 50, pictureBox1.Height - 50); // Y轴
                g.DrawLine(axisPen, 50, pictureBox1.Height - 50, pictureBox1.Width - 10, pictureBox1.Height - 50); // X轴

                // 绘制网格线和坐标标签
                Pen gridPen = new Pen(Color.LightGray, 1);
                Font labelFont = new Font("Arial", 8);
                for (int i = 50; i < pictureBox1.Width - 10; i += 40) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10, i, pictureBox1.Height - 50);
                    float xValue = minX + (i - 50) / (float)(pictureBox1.Width - 60) * (maxX - minX);
                    g.DrawString(xValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(i, pictureBox1.Height - 45));
                }
                for (int i = 10; i < pictureBox1.Height - 50; i += 20)
                {
                    g.DrawLine(gridPen, 50, i, pictureBox1.Width - 10, i);
                    float yValue = maxY - (i - 10) / (float)(pictureBox1.Height - 60) * (maxY - minY);
                    g.DrawString(yValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(5, i - 5));
                }

                // 绘制数据点
                if (dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.Blue, 2);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(50 + (dataPoints[i - 1].X - minX) / (maxX - minX) * (pictureBox1.Width - 60),
                                               pictureBox1.Height - 50 - (dataPoints[i - 1].Y - minY) / (maxY - minY) * (pictureBox1.Height - 60));
                        PointF p2 = new PointF(50 + (dataPoints[i].X - minX) / (maxX - minX) * (pictureBox1.Width - 60),
                                               pictureBox1.Height - 50 - (dataPoints[i].Y - minY) / (maxY - minY) * (pictureBox1.Height - 60));
                        g.DrawLine(dataPen, p1, p2);
                    }
                }

                // 绘制标题
                Font titleFont = new Font("Arial", 14, FontStyle.Bold);
                g.DrawString("二维曲线图", titleFont, Brushes.Black, new PointF(pictureBox1.Width / 2 - 50, 10));

                // 绘制坐标轴标签
                g.DrawString("X轴", labelFont, Brushes.Black, new PointF(pictureBox1.Width - 30, pictureBox1.Height - 40));
                g.DrawString("Y轴", labelFont, Brushes.Black, new PointF(10, 20));
            }

            pictureBox1.Image = bitmap;
        }
    }
}
