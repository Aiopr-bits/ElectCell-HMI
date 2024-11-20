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
        public MainWindow mainWindow;
        public List<PointF> dataPoints1;
        public List<PointF> dataPoints2;
        public List<PointF> dataPoints3;
        public List<PointF> dataPoints4;
        int comboBox1Index, comboBox2Index, comboBox3Index, comboBox4Index;

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

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox1Index+1];
                dataPoints1.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints1,pictureBox1);

            dataPoints2.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox2Index+1];
                dataPoints2.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints2, pictureBox2);

            dataPoints3.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox3Index+1];
                dataPoints3.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints3, pictureBox3);

            dataPoints4.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox4Index+1];
                dataPoints4.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints4, pictureBox4);

            if (comboBox1.Items.Count == 0)
            {
                for (int i = 1; i < Data.result.header.Count; i++)
                {
                    comboBox1.Items.Add(Data.result.header[i]);
                    comboBox2.Items.Add(Data.result.header[i]);
                    comboBox3.Items.Add(Data.result.header[i]);
                    comboBox4.Items.Add(Data.result.header[i]);
                }

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 24;
                comboBox3.SelectedIndex = 32;
                comboBox4.SelectedIndex = 40;
            }
        }

        public void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            DrawGraph(dataPoints1, pictureBox1);
            DrawGraph(dataPoints2, pictureBox2);
            DrawGraph(dataPoints3, pictureBox3);
            DrawGraph(dataPoints4, pictureBox4);
        }

        public void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox)
        {
            if (pictureBox.Width == 0 || pictureBox.Height == 0 || dataPoints.Count == 0)
                return;

            // 创建一个高分辨率的 Bitmap
            int scaleFactor = 4; // 放大倍数
            Bitmap bitmap = new Bitmap(pictureBox.Width * scaleFactor, pictureBox.Height * scaleFactor);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // 启用抗锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

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
                Pen axisPen = new Pen(Color.Black, 2 * scaleFactor);
                g.DrawLine(axisPen, 50 * scaleFactor, 10 * scaleFactor, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // Y轴
                g.DrawLine(axisPen, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor, (pictureBox.Width - 10) * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // X轴

                // 绘制网格线和坐标标签
                Pen gridPen = new Pen(Color.LightGray, 1 * scaleFactor);
                Font labelFont = new Font("Arial", 8 * scaleFactor);
                for (int i = 50 * scaleFactor; i < (pictureBox.Width - 10) * scaleFactor; i += 40 * scaleFactor) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10 * scaleFactor, i, (pictureBox.Height - 50) * scaleFactor);
                    float xValue = minX + (i - 50 * scaleFactor) / (float)((pictureBox.Width - 60) * scaleFactor) * (maxX - minX);
                    g.DrawString(xValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(i, (pictureBox.Height - 45) * scaleFactor));
                }
                for (int i = 10 * scaleFactor; i < (pictureBox.Height - 50) * scaleFactor; i += 20 * scaleFactor)
                {
                    g.DrawLine(gridPen, 50 * scaleFactor, i, (pictureBox.Width - 10) * scaleFactor, i);
                    float yValue = maxY - (i - 10 * scaleFactor) / (float)((pictureBox.Height - 60) * scaleFactor) * (maxY - minY);
                    g.DrawString(yValue.ToString("0.0"), labelFont, Brushes.Black, new PointF(5 * scaleFactor, i - 5 * scaleFactor));
                }

                // 绘制数据点
                if (dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.Blue, 2 * scaleFactor);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(50 * scaleFactor + (dataPoints[i - 1].X - minX) / (maxX - minX) * (pictureBox.Width - 60) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i - 1].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        PointF p2 = new PointF(50 * scaleFactor + (dataPoints[i].X - minX) / (maxX - minX) * (pictureBox.Width - 60) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        g.DrawLine(dataPen, p1, p2);
                    }
                }

                // 绘制标题
                //Font titleFont = new Font("Arial", 14 * scaleFactor, FontStyle.Bold);
                //g.DrawString("", titleFont, Brushes.Black, new PointF((pictureBox.Width / 2 - 50) * scaleFactor, 10 * scaleFactor));

                // 绘制坐标轴标签
                //g.DrawString("X轴", labelFont, Brushes.Black, new PointF((pictureBox.Width - 30) * scaleFactor, (pictureBox.Height - 40) * scaleFactor));
                //g.DrawString("Y轴", labelFont, Brushes.Black, new PointF(10 * scaleFactor, 20 * scaleFactor));
            }

            // 缩放 Bitmap 到 PictureBox 的大小
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = bitmap;
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1Index= comboBox1.SelectedIndex;
            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox1Index + 1];
                dataPoints1.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints1, pictureBox1);
        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2Index = comboBox2.SelectedIndex;
            dataPoints2.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox2Index + 1];
                dataPoints2.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints2, pictureBox2);
        }

        public void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3Index = comboBox3.SelectedIndex;
            dataPoints3.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox3Index + 1];
                dataPoints3.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints3, pictureBox3);
        }

        public void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4Index = comboBox4.SelectedIndex;
            dataPoints4.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox4Index + 1];
                dataPoints4.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints4, pictureBox4);
        }
    }
}
