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

            hideAllLabel();
        }

        public void hideAllLabel()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
        }

        public void showAllLabel()
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
        }

        public void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            if (Data.result.result == null || Data.result.result.Count == 0)
            {
                return;
            }


            try
            {
                showAllLabel();

                // 电解槽1
                var sb = new StringBuilder();
                sb.AppendLine("电解槽1");
                int rowIndex = 1;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (variableName.StartsWith("ec 1-"))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb.AppendLine($"{rowIndex}. {variableName}: {valueStr}");
                        rowIndex++;
                    }
                }
                label1.Text = sb.ToString();

                // 电解槽2
                var sb2 = new StringBuilder();
                sb2.AppendLine("电解槽2");
                int rowIndex2 = 7;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (variableName.StartsWith("ec 2-"))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb2.AppendLine($"{rowIndex2}. {variableName}: {valueStr}");
                        rowIndex2++;
                    }
                }
                label2.Text = sb2.ToString();

                // 电解槽3
                var sb3 = new StringBuilder();
                sb3.AppendLine("电解槽3");
                int rowIndex3 = 13;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (variableName.StartsWith("ec 3-"))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb3.AppendLine($"{rowIndex3}. {variableName}: {valueStr}");
                        rowIndex3++;
                    }
                }
                label3.Text = sb3.ToString();

                // 电解槽4
                var sb4 = new StringBuilder();
                sb4.AppendLine("电解槽4");
                int rowIndex4 = 19;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (variableName.StartsWith("ec 4-"))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb4.AppendLine($"{rowIndex4}. {variableName}: {valueStr}");
                        rowIndex4++;
                    }
                }
                label4.Text = sb4.ToString();

                // 阴极分离器
                var sb5 = new StringBuilder();
                sb5.AppendLine("阴极分离器");
                string[] cathodeVars = { "cs 1n", "cs 1n-h2", "cs 1n-o2", "cs 1-l_g", "cs 2-p", "cs 2n", "cs 2n-h2", "cs 2n-o2" };
                int rowIndex5 = 25;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (cathodeVars.Contains(variableName))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb5.AppendLine($"{rowIndex5}. {variableName}: {valueStr}");
                        rowIndex5++;
                    }
                }
                label5.Text = sb5.ToString();

                // 阳极分离器
                var sb6 = new StringBuilder();
                sb6.AppendLine("阳极分离器");
                string[] anodeVars = { "as 1n", "as 1n-h2", "as 1n-o2", "as 1-l_g", "as 2-p", "as 2n", "as 2n-h2", "as 2n-o2" };
                int rowIndex6 = 33;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (anodeVars.Contains(variableName))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb6.AppendLine($"{rowIndex6}. {variableName}: {valueStr}");
                        rowIndex6++;
                    }
                }
                label6.Text = sb6.ToString();

                // 平衡管线
                var sb7 = new StringBuilder();
                sb7.AppendLine("平衡管线");
                string[] pipeVars = { "bp 1n", "bp 1n-h2", "bp 1n-o2" };
                int rowIndex7 = 41;
                for (int j = 1; j < Data.result.header.Count; j++)
                {
                    string variableName = Data.result.header[j].Trim();
                    if (pipeVars.Contains(variableName))
                    {
                        var value = Data.result.result.LastOrDefault()?[j];
                        string valueStr = value != null ? $"{value:F4}" : "";
                        sb7.AppendLine($"{rowIndex7}. {variableName}: {valueStr}");
                        rowIndex7++;
                    }
                }
                label7.Text = sb7.ToString();
            }
            catch
            {
            }

            dataPoints1.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox1Index + 1];
                dataPoints1.Add(new PointF(x, y));
            }
            string comboBox1Text = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints1, pictureBox1, true, comboBox1Text);

            dataPoints2.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox2Index + 1];
                dataPoints2.Add(new PointF(x, y));
            }
            string comboBox2Text = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints2, pictureBox2, true, comboBox2Text);

            dataPoints3.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox3Index + 1];
                dataPoints3.Add(new PointF(x, y));
            }
            string comboBox3Text = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints3, pictureBox3, true, comboBox3Text);

            dataPoints4.Clear();
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                float x = (float)Data.result.result[i][0];
                float y = (float)Data.result.result[i][comboBox4Index + 1];
                dataPoints4.Add(new PointF(x, y));
            }
            string comboBox4Text = comboBox4.SelectedItem != null ? comboBox4.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints4, pictureBox4, true, comboBox4Text);

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
                comboBox2.SelectedIndex = 1;
                comboBox3.SelectedIndex = 2;
                comboBox4.SelectedIndex = 3;
            }
        }

        public void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            string comboBox1Text = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            string comboBox2Text = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : string.Empty;
            string comboBox3Text = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : string.Empty;
            string comboBox4Text = comboBox4.SelectedItem != null ? comboBox4.SelectedItem.ToString() : string.Empty;

            DrawGraph(dataPoints1, pictureBox1, true, comboBox1Text);
            DrawGraph(dataPoints2, pictureBox2, true, comboBox2Text);
            DrawGraph(dataPoints3, pictureBox3, true, comboBox3Text);
            DrawGraph(dataPoints4, pictureBox4, true, comboBox4Text);
        }

        public void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox, bool drawDataPoints = true, string curveName = "")
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

                // 绘制曲线区域背景颜色
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), 50 * scaleFactor, 10 * scaleFactor, (pictureBox.Width - 60) * scaleFactor, (pictureBox.Height - 60) * scaleFactor);

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
                Pen gridPen = new Pen(Color.FromArgb(0, 84, 27), 1 * scaleFactor);
                Font labelFont = new Font("Arial", 8 * scaleFactor);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center; // 居中对齐

                for (int i = 50 * scaleFactor; i < (pictureBox.Width - 10) * scaleFactor; i += 40 * scaleFactor) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10 * scaleFactor, i, (pictureBox.Height - 50) * scaleFactor);
                    float labelX = minX + (i - 50 * scaleFactor) / (float)((pictureBox.Width - 60) * scaleFactor) * (maxX - minX);
                    g.DrawString(Math.Round(labelX).ToString(), labelFont, Brushes.Black, new PointF(i, (pictureBox.Height - 45) * scaleFactor), stringFormat); // 调整标签位置和对齐方式
                }
                for (int i = 10 * scaleFactor; i < (pictureBox.Height - 50) * scaleFactor; i += 20 * scaleFactor)
                {
                    g.DrawLine(gridPen, 50 * scaleFactor, i, (pictureBox.Width - 10) * scaleFactor, i);
                    float labelY = maxY - (i - 10 * scaleFactor) / (float)((pictureBox.Height - 60) * scaleFactor) * (maxY - minY);
                    g.DrawString(Math.Round(labelY).ToString(), labelFont, Brushes.Black, new PointF(45 * scaleFactor, i - 5 * scaleFactor), new StringFormat { Alignment = StringAlignment.Far }); // 调整标签位置和对齐方式
                }

                // 绘制数据点
                if (drawDataPoints && dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.FromArgb(200, 213, 13), 2 * scaleFactor);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(50 * scaleFactor + (dataPoints[i - 1].X - minX) / (maxX - minX) * (pictureBox.Width - 60) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i - 1].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        PointF p2 = new PointF(50 * scaleFactor + (dataPoints[i].X - minX) / (maxX - minX) * (pictureBox.Width - 60) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        g.DrawLine(dataPen, p1, p2);
                    }
                }

                if (curveName != "")
                {
                    // 绘制图例
                    Font legendFont = new Font("Arial", 10 * scaleFactor, FontStyle.Bold); // 调整字体大小
                    SizeF legendSize = g.MeasureString(curveName, legendFont);
                    RectangleF legendRect = new RectangleF((pictureBox.Width - 10) * scaleFactor - legendSize.Width - 60 * scaleFactor, 10 * scaleFactor, legendSize.Width + 60 * scaleFactor, legendSize.Height + 5 * scaleFactor); // 调整图例区域宽度
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), legendRect); // 设置背景颜色
                    g.DrawString(curveName, legendFont, Brushes.White, new PointF(legendRect.X + 5 * scaleFactor, legendRect.Y + 2.5f * scaleFactor)); // 设置字体颜色为白色

                    // 绘制曲线示例
                    Pen legendPen = new Pen(Color.FromArgb(200, 213, 13), 2 * scaleFactor);
                    g.DrawLine(legendPen, legendRect.X + legendSize.Width + 10 * scaleFactor, legendRect.Y + legendRect.Height / 2, legendRect.X + legendSize.Width + 50 * scaleFactor, legendRect.Y + legendRect.Height / 2); // 曲线长度设置为现在的2倍
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
            string comboBox1Text = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints1, pictureBox1, true, comboBox1Text);
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
            string comboBox2Text = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints2, pictureBox2, true, comboBox2Text);
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
            string comboBox3Text = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints3, pictureBox3, true, comboBox3Text);
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
            string comboBox4Text = comboBox4.SelectedItem != null ? comboBox4.SelectedItem.ToString() : string.Empty;
            DrawGraph(dataPoints4, pictureBox4, true, comboBox4Text);
        }
    }
}
