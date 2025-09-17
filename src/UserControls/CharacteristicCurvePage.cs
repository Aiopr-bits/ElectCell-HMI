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
    public partial class CharacteristicCurvePage : UserControl
    {
        List<PointF> dataPointsQH = new List<PointF>();
        List<PointF> dataPointsQP = new List<PointF>();
        public CharacteristicCurvePage()
        {
            this.Resize += TrendMonitorPage_Resize;
            InitializeComponent();
            dataGridView1LoadData();
            drawCurve();
        }

        public void TrendMonitorPage_Resize(object sender, EventArgs e)
        {
            // 中文标题与单位
            DrawGraph(dataPointsQH, pictureBox1, true, "流量-扬程特性曲线", "流量 (m^3/h)", "扬程 (m)");
            DrawGraph(dataPointsQP, pictureBox2, true, "流量-压力特性曲线", "流量 (m^3/h)", "压力 (Pa)");
        }

        void dataGridView1LoadData()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("流量 (m^3/h)", typeof(double));
            dt1.Columns.Add("扬程 (m)", typeof(double));

            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQH; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["流量 (m^3/h)"] = Data.pumpCharacteristic.characteristicQH[i][0];
                dr["扬程 (m)"] = Data.pumpCharacteristic.characteristicQH[i][1];
                dt1.Rows.Add(dr);
            }

            dt1.Columns.Add("序号", typeof(int)).SetOrdinal(0);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i]["序号"] = i + 1;
            }
            dataGridView1.DataSource = dt1;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("流量 (m^3/h)", typeof(double));
            dt2.Columns.Add("压力 (Pa)", typeof(double));

            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQP; i++)
            {
                DataRow dr = dt2.NewRow();
                dr["流量 (m^3/h)"] = Data.pumpCharacteristic.characteristicQP[i][0];
                dr["压力 (Pa)"] = Data.pumpCharacteristic.characteristicQP[i][1];
                dt2.Rows.Add(dr);
            }

            dt2.Columns.Add("序号", typeof(int)).SetOrdinal(0);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i]["序号"] = i + 1;
            }
            dataGridView2.DataSource = dt2;
        }

        void drawCurve()
        {
            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQH; i++)
            {
                dataPointsQH.Add(new PointF((float)Data.pumpCharacteristic.characteristicQH[i][0], (float)Data.pumpCharacteristic.characteristicQH[i][1]));
            }
            DrawGraph(dataPointsQH, pictureBox1, true, "流量-扬程特性曲线", "流量 (m^3/h)", "扬程 (m)");

            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQP; i++)
            {
                dataPointsQP.Add(new PointF((float)Data.pumpCharacteristic.characteristicQP[i][0], (float)Data.pumpCharacteristic.characteristicQP[i][1]));
            }
            DrawGraph(dataPointsQP, pictureBox2, true, "流量-压力特性曲线", "流量 (m^3/h)", "压力 (Pa)");
        }

        public void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox, bool drawDataPoints = true, string curveName = "", string xLabel = "", string yLabel = "")
        {
            if (pictureBox.Width == 0 || pictureBox.Height == 0)
                return;

            int scaleFactor = 4; // 放大倍数
            Bitmap bitmap = new Bitmap(pictureBox.Width * scaleFactor, pictureBox.Height * scaleFactor);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // 启用抗锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // 基础边距
                int T = 10 * scaleFactor;
                int B = 50 * scaleFactor;
                int R = 10 * scaleFactor;
                int LBase = 50 * scaleFactor; // 基础左边距

                // 计算数据点的最小值和最大值
                float minX = dataPoints.Count > 0 ? dataPoints.Min(p => p.X) : 0;
                float maxX = dataPoints.Count > 0 ? dataPoints.Max(p => p.X) : 100;
                float minY = dataPoints.Count > 0 ? dataPoints.Min(p => p.Y) : 0;
                float maxY = dataPoints.Count > 0 ? dataPoints.Max(p => p.Y) : 100;
                if (minY == maxY)
                {
                    minY -= 1;
                    maxY += 1;
                }

                // 字体与画笔
                Pen axisPen = new Pen(Color.Black, 2 * scaleFactor);
                Pen gridPen = new Pen(Color.FromArgb(0, 84, 27), 1 * scaleFactor);
                Font labelFont = new Font("Arial", 6 * scaleFactor); // 刻度字体
                Font axisLabelFont = new Font("Arial", 8 * scaleFactor, FontStyle.Bold); // 轴标题字体
                StringFormat stringFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat stringFormatFar = new StringFormat { Alignment = StringAlignment.Far };

                // 预计算纵轴刻度文本最大宽度，用于动态左边距，减少与纵轴标题的遮挡
                float maxYLabelWidth = 0f;
                for (int i = T; i < (pictureBox.Height - B) * scaleFactor / scaleFactor; i += 20 * scaleFactor)
                {
                    // 这里不真正用 i 作像素，而是只估算文字宽度；按相同格式生成一个代表性值
                    float labelY = maxY - (i - T) / (float)((pictureBox.Height - (T + B) / scaleFactor) * scaleFactor) * (maxY - minY);
                    string yText = labelY.ToString("F2");
                    SizeF size = g.MeasureString(yText, labelFont);
                    if (size.Width > maxYLabelWidth) maxYLabelWidth = size.Width;
                }
                int L = LBase + (int)Math.Ceiling(maxYLabelWidth) + 10 * scaleFactor;

                // 绘制曲线区域背景颜色
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), L, T, (pictureBox.Width * scaleFactor - R - L), (pictureBox.Height * scaleFactor - B - T));

                // 坐标轴
                g.DrawLine(axisPen, L, T, L, pictureBox.Height * scaleFactor - B); // Y轴
                g.DrawLine(axisPen, L, pictureBox.Height * scaleFactor - B, pictureBox.Width * scaleFactor - R, pictureBox.Height * scaleFactor - B); // X轴

                // 可视区域尺寸
                float plotWidth = pictureBox.Width * scaleFactor - R - L;
                float plotHeight = pictureBox.Height * scaleFactor - B - T;

                // X 轴网格与标签
                for (int x = 0; x <= plotWidth; x += 40 * scaleFactor)
                {
                    int xi = L + x;
                    g.DrawLine(gridPen, xi, T, xi, T + (int)plotHeight);
                    float labelX = minX + x / plotWidth * (maxX - minX);
                    g.DrawString(labelX.ToString("E0"), labelFont, Brushes.Black, new PointF(xi, pictureBox.Height * scaleFactor - B + 5 * scaleFactor), stringFormatCenter);
                }

                // Y 轴网格与标签
                for (int y = 0; y <= plotHeight; y += 20 * scaleFactor)
                {
                    int yi = T + y;
                    g.DrawLine(gridPen, L, yi, L + (int)plotWidth, yi);
                    float labelY = maxY - y / plotHeight * (maxY - minY);
                    g.DrawString(labelY.ToString("F2"), labelFont, Brushes.Black, new PointF(L - 5 * scaleFactor, yi - 5 * scaleFactor), stringFormatFar);
                }

                // 轴标题（单位）
                if (!string.IsNullOrEmpty(xLabel))
                {
                    g.DrawString(xLabel, axisLabelFont, Brushes.Black, new PointF(L + plotWidth / 2f, pictureBox.Height * scaleFactor - 20 * scaleFactor), stringFormatCenter);
                }
                if (!string.IsNullOrEmpty(yLabel))
                {
                    var state = g.Save();
                    g.TranslateTransform(15 * scaleFactor, T + plotHeight / 2f); // 更靠左，避免遮挡
                    g.RotateTransform(-90);
                    g.DrawString(yLabel, axisLabelFont, Brushes.Black, new PointF(0, 0), stringFormatCenter);
                    g.Restore(state);
                }

                // 数据曲线
                if (drawDataPoints && dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.FromArgb(200, 213, 13), 2 * scaleFactor);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(
                            L + (dataPoints[i - 1].X - minX) / (maxX - minX) * plotWidth,
                            T + plotHeight - (dataPoints[i - 1].Y - minY) / (maxY - minY) * plotHeight);
                        PointF p2 = new PointF(
                            L + (dataPoints[i].X - minX) / (maxX - minX) * plotWidth,
                            T + plotHeight - (dataPoints[i].Y - minY) / (maxY - minY) * plotHeight);
                        g.DrawLine(dataPen, p1, p2);
                    }
                }

                // 图例
                if (curveName != "")
                {
                    Font legendFont = new Font("Arial", 10 * scaleFactor, FontStyle.Bold);
                    SizeF legendSize = g.MeasureString(curveName, legendFont);
                    RectangleF legendRect = new RectangleF(L + plotWidth - legendSize.Width - 60 * scaleFactor, T + 5 * scaleFactor, legendSize.Width + 60 * scaleFactor, legendSize.Height + 5 * scaleFactor);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), legendRect);
                    g.DrawString(curveName, legendFont, Brushes.White, new PointF(legendRect.X + 5 * scaleFactor, legendRect.Y + 2.5f * scaleFactor));

                    Pen legendPen = new Pen(Color.FromArgb(200, 213, 13), 2 * scaleFactor);
                    g.DrawLine(legendPen, legendRect.X + legendSize.Width + 10 * scaleFactor, legendRect.Y + legendRect.Height / 2, legendRect.X + legendSize.Width + 50 * scaleFactor, legendRect.Y + legendRect.Height / 2);
                }
            }

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = bitmap;
        }
    }
}
