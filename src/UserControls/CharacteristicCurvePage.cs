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
        public CharacteristicCurvePage()
        {
            InitializeComponent();
            dataGridView1LoadData();
            drawCurve();
        }

        void dataGridView1LoadData()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("流量", typeof(double));
            dt1.Columns.Add("扬程", typeof(double));

            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQH; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["流量"] = Data.pumpCharacteristic.characteristicQH[i][0];
                dr["扬程"] = Data.pumpCharacteristic.characteristicQH[i][1];
                dt1.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt1;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("流量", typeof(double));
            dt2.Columns.Add("压力", typeof(double));

            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQP; i++)
            {
                DataRow dr = dt2.NewRow();
                dr["流量"] = Data.pumpCharacteristic.characteristicQP[i][0];
                dr["压力"] = Data.pumpCharacteristic.characteristicQP[i][1];
                dt2.Rows.Add(dr);
            }
            dataGridView2.DataSource = dt2;
        }

        void drawCurve()
        {
            List<PointF> dataPointsQH = new List<PointF>();
            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQH; i++)
            {
                dataPointsQH.Add(new PointF((float)Data.pumpCharacteristic.characteristicQH[i][0], (float)Data.pumpCharacteristic.characteristicQH[i][1]));
            }
            DrawGraph(dataPointsQH, pictureBox1);

            List<PointF> dataPointsQP = new List<PointF>();
            for (int i = 0; i < Data.pumpCharacteristic.nCharacteristicQP; i++)
            {
                dataPointsQP.Add(new PointF((float)Data.pumpCharacteristic.characteristicQP[i][0], (float)Data.pumpCharacteristic.characteristicQP[i][1]));
            }
            DrawGraph(dataPointsQP, pictureBox2);
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
                g.DrawLine(axisPen, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor, (pictureBox.Width - 20) * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // X轴

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
                Font labelFont = new Font("Arial", 6 * scaleFactor); // 调整字体大小
                for (int i = 50 * scaleFactor; i < (pictureBox.Width - 20) * scaleFactor; i += 40 * scaleFactor) // 增加间隔
                {
                    g.DrawLine(gridPen, i, 10 * scaleFactor, i, (pictureBox.Height - 50) * scaleFactor);
                    float labelX = minX + (i - 50 * scaleFactor) / (float)((pictureBox.Width - 70) * scaleFactor) * (maxX - minX);
                    g.DrawString(labelX.ToString("E2"), labelFont, Brushes.Black, new PointF(i, (pictureBox.Height - 45) * scaleFactor)); // 使用科学计数法
                }
                for (int i = 10 * scaleFactor; i < (pictureBox.Height - 50) * scaleFactor; i += 20 * scaleFactor)
                {
                    g.DrawLine(gridPen, 50 * scaleFactor, i, (pictureBox.Width - 20) * scaleFactor, i);
                    float labelY = maxY - (i - 10 * scaleFactor) / (float)((pictureBox.Height - 60) * scaleFactor) * (maxY - minY);
                    g.DrawString(labelY.ToString("F2"), labelFont, Brushes.Black, new PointF(5 * scaleFactor, i - 5 * scaleFactor)); // 使用普通小数格式
                }

                // 绘制数据点
                if (drawDataPoints && dataPoints.Count > 1)
                {
                    Pen dataPen = new Pen(Color.Blue, 2 * scaleFactor);
                    for (int i = 1; i < dataPoints.Count; i++)
                    {
                        PointF p1 = new PointF(50 * scaleFactor + (dataPoints[i - 1].X - minX) / (maxX - minX) * (pictureBox.Width - 70) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i - 1].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        PointF p2 = new PointF(50 * scaleFactor + (dataPoints[i].X - minX) / (maxX - minX) * (pictureBox.Width - 70) * scaleFactor,
                                               (pictureBox.Height - 50) * scaleFactor - (dataPoints[i].Y - minY) / (maxY - minY) * (pictureBox.Height - 60) * scaleFactor);
                        g.DrawLine(dataPen, p1, p2);
                    }
                }
            }

            // 缩放 Bitmap 到 PictureBox 的大小
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = bitmap;
        }


    }
}
