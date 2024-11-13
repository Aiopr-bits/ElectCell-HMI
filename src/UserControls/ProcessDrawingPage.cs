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
    public partial class ProcessDrawingPage : UserControl
    {
        private MainWindow mainWindow;
        private List<PointF> dataPoints1;
        private List<PointF> dataPoints2;
        private List<PointF> dataPoints3;
        private List<PointF> dataPoints4;
        int comboBox1Index, comboBox2Index, comboBox3Index, comboBox4Index;

        public ProcessDrawingPage(MainWindow mainWindow)
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

            // 初始化每个 PictureBox
            DrawGraph(new List<PointF>(), pictureBox1,false);
            DrawGraph(new List<PointF>(), pictureBox2, false);
            DrawGraph(new List<PointF>(), pictureBox3, false);
            DrawGraph(new List<PointF>(), pictureBox4, false);
        }

        private void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            if (Data.result.result == null || Data.result.result.Count == 0)
            {
                return;
            }
            //this.label1.Text = ((double)Data.result.result[Data.result.result.Count - 1][0]).ToString();
            //this.label2.Text = ((double)Data.result.result[Data.result.result.Count - 1][1]).ToString();
            //this.label3.Text = ((double)Data.result.result[Data.result.result.Count - 1][2]).ToString();
            //this.label4.Text = ((double)Data.result.result[Data.result.result.Count - 1][3]).ToString();

            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox1Index + 1];
                dataPoints1.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints1, pictureBox1);

            dataPoints2.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox2Index + 1];
                dataPoints2.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints2, pictureBox2);

            dataPoints3.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox3Index + 1];
                dataPoints3.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints3, pictureBox3);

            dataPoints4.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox4Index + 1];
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

        private void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            DrawGraph(dataPoints1, pictureBox1);
            DrawGraph(dataPoints2, pictureBox2);
            DrawGraph(dataPoints3, pictureBox3);
            DrawGraph(dataPoints4, pictureBox4);
        }

        private void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox, bool drawDataPoints = true)
        {
            if (pictureBox.Width == 0 || pictureBox.Height == 0)
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

                // 设置坐标轴
                Pen axisPen = new Pen(Color.Black, 2 * scaleFactor);
                g.DrawLine(axisPen, 50 * scaleFactor, 10 * scaleFactor, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // Y轴
                g.DrawLine(axisPen, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor, (pictureBox.Width - 10) * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // X轴

                // 计算数据点的最小值和最大值
                float minX = dataPoints.Count > 0 ? dataPoints.Min(p => p.X) : 0;
                float maxX = dataPoints.Count > 0 ? dataPoints.Max(p => p.X) : 100;
                float minY = dataPoints.Count > 0 ? dataPoints.Min(p => p.Y) : 0;
                float maxY = dataPoints.Count > 0 ? dataPoints.Max(p => p.Y) : 100;

                // 如果 minY 和 maxY 相等，设置一个默认范围
                if (minY == maxY)
                {
                    minY -= 1;
                    maxY += 1;
                }

                // 绘制网格线和坐标标签
                Pen gridPen = new Pen(Color.LightGray, 1 * scaleFactor);
                Font labelFont = new Font("Arial", 8 * scaleFactor);
                for (int i = 50 * scaleFactor; i < (pictureBox.Width - 10) * scaleFactor; i += 40 * scaleFactor) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10 * scaleFactor, i, (pictureBox.Height - 50) * scaleFactor);
                    float labelX = minX + (i - 50 * scaleFactor) / (float)((pictureBox.Width - 60) * scaleFactor) * (maxX - minX);
                    g.DrawString(Math.Round(labelX).ToString(), labelFont, Brushes.Black, new PointF(i, (pictureBox.Height - 45) * scaleFactor));
                }
                for (int i = 10 * scaleFactor; i < (pictureBox.Height - 50) * scaleFactor; i += 20 * scaleFactor)
                {
                    g.DrawLine(gridPen, 50 * scaleFactor, i, (pictureBox.Width - 10) * scaleFactor, i);
                    float labelY = maxY - (i - 10 * scaleFactor) / (float)((pictureBox.Height - 60) * scaleFactor) * (maxY - minY);
                    g.DrawString(Math.Round(labelY).ToString(), labelFont, Brushes.Black, new PointF(5 * scaleFactor, i - 5 * scaleFactor));
                }

                // 绘制数据点
                if (drawDataPoints && dataPoints.Count > 1)
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
            }

            // 缩放 Bitmap 到 PictureBox 的大小
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = bitmap;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1Index = comboBox1.SelectedIndex;
            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox1Index + 1];
                dataPoints1.Add(new PointF(x, y));
            }
            DrawGraph(dataPoints1, pictureBox1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
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
