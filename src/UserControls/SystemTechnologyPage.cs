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
    public partial class SystemTechnologyPage : UserControl
    {
        public MainWindow mainWindow;
        public List<PointF> dataPoints1;
        public List<PointF> dataPoints2;
        public List<PointF> dataPoints3;
        public List<PointF> dataPoints4;
        int comboBox1Index, comboBox2Index, comboBox3Index, comboBox4Index;

        public SystemTechnologyPage(MainWindow mainWindow)
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
            DrawGraph(new List<PointF>(), pictureBox1, false);
            DrawGraph(new List<PointF>(), pictureBox2, false);
            DrawGraph(new List<PointF>(), pictureBox3, false);
            DrawGraph(new List<PointF>(), pictureBox4, false);
        }

        public void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            if (Data.result.result == null || Data.result.result.Count == 0)
            {
                return;
            }

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
                Dictionary<string, string> variableNames = new Dictionary<string, string>
                {
                    { "ec 1-1n", "电解槽1阴极物质量" },
                    { "ec 1-1n-h2", "电解槽1阴极氢气物质量" },
                    { "ec 1-1n-o2", "电解槽1阴极氧气物质量" },
                    { "ec 1-2n", "电解槽1阳极物质量" },
                    { "ec 1-2n-h2", "电解槽1阳极氢气物质量" },
                    { "ec 1-2n-o2", "电解槽1阳极氧气物质量" },
                    { "ec 2-1n", "电解槽2阴极物质量" },
                    { "ec 2-1n-h2", "电解槽2阴极氢气物质量" },
                    { "ec 2-1n-o2", "电解槽2阴极氧气物质量" },
                    { "ec 2-2n", "电解槽2阳极物质量" },
                    { "ec 2-2n-h2", "电解槽2阳极氢气物质量" },
                    { "ec 2-2n-o2", "电解槽2阳极氧气物质量" },
                    { "ec 3-1n", "电解槽3阴极物质量" },
                    { "ec 3-1n-h2", "电解槽3阴极氢气物质量" },
                    { "ec 3-1n-o2", "电解槽3阴极氧气物质量" },
                    { "ec 3-2n", "电解槽3阳极物质量" },
                    { "ec 3-2n-h2", "电解槽3阳极氢气物质量" },
                    { "ec 3-2n-o2", "电解槽3阳极氧气物质量" },
                    { "ec 4-1n", "电解槽4阴极物质量" },
                    { "ec 4-1n-h2", "电解槽4阴极氢气物质量" },
                    { "ec 4-1n-o2", "电解槽4阴极氧气物质量" },
                    { "ec 4-2n", "电解槽4阳极物质量" },
                    { "ec 4-2n-h2", "电解槽4阳极氢气物质量" },
                    { "ec 4-2n-o2", "电解槽4阳极氧气物质量" },
                    { "cs 1n", "阴极分离器液空间物质量" },
                    { "cs 1n-h2", "阴极分离器氢气物质量" },
                    { "cs 1n-o2", "阴极分离器氧气物质量" },
                    { "cs 1-l_g", "阴极分离器液体高度" },
                    { "cs 2-p", "阴极分离器气空间压力" },
                    { "cs 2n", "阴极分离器气空间物质量" },
                    { "cs 2n-h2", "阴极分离器气空间氢气物质量" },
                    { "cs 2n-o2", "阴极分离器气空间氧气物质量" },
                    { "as 1n", "阳极分离器液空间物质量" },
                    { "as 1n-h2", "阳极分离器氢气物质量" },
                    { "as 1n-o2", "阳极分离器氧气物质量" },
                    { "as 1-l_g", "阳极分离器液体高度" },
                    { "as 2-p", "阳极分离器气空间压力" },
                    { "as 2n", "阳极分离器气空间物质量" },
                    { "as 2n-h2", "阳极分离器气空间氢气物质量" },
                    { "as 2n-o2", "阳极分离器气空间氧气物质量" },
                    { "bp 1n", "平衡管线物质量" },
                    { "bp 1n-h2", "平衡管线氢气物质量" },
                    { "bp 1n-o2", "平衡管线氧气物质量" }
                };

                for (int i = 1; i < Data.result.header.Count; i++)
                {
                    string englishName = Data.result.header[i];
                    englishName = englishName.Trim();
                    if (variableNames.ContainsKey(englishName))
                    {
                        string chineseName = variableNames[englishName];
                        comboBox1.Items.Add(chineseName);
                        comboBox2.Items.Add(chineseName);
                        comboBox3.Items.Add(chineseName);
                        comboBox4.Items.Add(chineseName);
                    }
                    else
                    {
                        comboBox1.Items.Add(englishName);
                        comboBox2.Items.Add(englishName);
                        comboBox3.Items.Add(englishName);
                        comboBox4.Items.Add(englishName);
                    }
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

        public void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox, bool drawDataPoints = true)
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

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
