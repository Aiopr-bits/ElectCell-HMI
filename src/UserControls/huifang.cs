using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ElectCell_HMI.Forms
{
    public partial class huifang : UserControl
    {
        public huifang()
        {
            InitializeComponent();

            DataTable dt1 = new DataTable();
            dataGridView1.DataSource = dt1;
            dt1.Columns.Add("序号", typeof(string));
            dt1.Columns.Add("信号名", typeof(string));
            dt1.Columns.Add("描述", typeof(string));
            dt1.Columns.Add("图表", typeof(string)); 

            DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.HeaderText = "图表";
            textBoxColumn.Name = "图表";
            textBoxColumn.DataPropertyName = "图表";

            dataGridView1.Columns.Remove("图表");
            dataGridView1.Columns.Add(textBoxColumn);
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

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

            foreach (var variable in variableNames)
            {
                DataRow row = dt1.NewRow();
                row["序号"] = dt1.Rows.Count + 1;
                row["信号名"] = variable.Key;
                row["描述"] = variable.Value;
                if (row["序号"].ToString() == "1") row["图表"] = "图表1";
                else if (row["序号"].ToString() == "2") row["图表"] = "图表2";
                else if (row["序号"].ToString() == "3") row["图表"] = "图表3";
                else row["图表"] = "无";
                dt1.Rows.Add(row);
            }

            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            UpdateGraphs();
        }

        public void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["图表"].Index && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                comboBoxCell.Items.AddRange("无", "图表1", "图表2", "图表3");
                comboBoxCell.Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] = comboBoxCell;
                dataGridView1.BeginEdit(true);
            }
        }

        public void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["图表"].Index && e.RowIndex >= 0)
            {
                var currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (currentValue == "图表1" || currentValue == "图表2" || currentValue == "图表3")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (i == e.RowIndex) continue;

                        if (dataGridView1.Rows[i].Cells["图表"].Value?.ToString() == currentValue)
                        {
                            dataGridView1.Rows[i].Cells["图表"].Value = "无";
                        }
                    }
                }

                // 延迟修改单元格类型
                var value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                    textBoxCell.Value = value;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] = textBoxCell;
                }));

                UpdateGraphs();
            }
        }

        public void DrawGraph(List<PointF> dataPoints, System.Windows.Forms.PictureBox pictureBox, bool drawDataPoints = true, string curveName = "")
        {
            if (pictureBox.Width == 0 || pictureBox.Height == 0)
                return;

            int scaleFactor = 4; 
            Bitmap bitmap = new Bitmap(pictureBox.Width * scaleFactor, pictureBox.Height * scaleFactor);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), 50 * scaleFactor, 10 * scaleFactor, (pictureBox.Width - 60) * scaleFactor, (pictureBox.Height - 60) * scaleFactor);

                Pen axisPen = new Pen(Color.Black, 2 * scaleFactor);
                g.DrawLine(axisPen, 50 * scaleFactor, 10 * scaleFactor, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // Y轴
                g.DrawLine(axisPen, 50 * scaleFactor, (pictureBox.Height - 50) * scaleFactor, (pictureBox.Width - 10) * scaleFactor, (pictureBox.Height - 50) * scaleFactor); // X轴

                float minX = dataPoints.Count > 0 ? dataPoints.Min(p => p.X) : 0;
                float maxX = dataPoints.Count > 0 ? dataPoints.Max(p => p.X) : 100;
                float minY = dataPoints.Count > 0 ? dataPoints.Min(p => p.Y) : 0;
                float maxY = dataPoints.Count > 0 ? dataPoints.Max(p => p.Y) : 100;

                if (minY == maxY)
                {
                    minY -= 1;
                    maxY += 1;
                }

                Pen gridPen = new Pen(Color.FromArgb(0, 84, 27), 1 * scaleFactor);
                Font labelFont = new Font("Arial", 8 * scaleFactor);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center; 

                for (int i = 50 * scaleFactor; i < (pictureBox.Width - 10) * scaleFactor; i += 40 * scaleFactor) 
                {
                    g.DrawLine(gridPen, i, 10 * scaleFactor, i, (pictureBox.Height - 50) * scaleFactor);
                    float labelX = minX + (i - 50 * scaleFactor) / (float)((pictureBox.Width - 60) * scaleFactor) * (maxX - minX);
                    g.DrawString(Math.Round(labelX).ToString(), labelFont, Brushes.Black, new PointF(i, (pictureBox.Height - 45) * scaleFactor), stringFormat); 
                }
                for (int i = 10 * scaleFactor; i < (pictureBox.Height - 50) * scaleFactor; i += 20 * scaleFactor)
                {
                    g.DrawLine(gridPen, 50 * scaleFactor, i, (pictureBox.Width - 10) * scaleFactor, i);
                    float labelY = maxY - (i - 10 * scaleFactor) / (float)((pictureBox.Height - 60) * scaleFactor) * (maxY - minY);
                    g.DrawString(Math.Round(labelY).ToString(), labelFont, Brushes.Black, new PointF(45 * scaleFactor, i - 5 * scaleFactor), new StringFormat { Alignment = StringAlignment.Far }); // 调整标签位置和对齐方式
                }

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
                    Font legendFont = new Font("Arial", 10 * scaleFactor, FontStyle.Bold); 
                    SizeF legendSize = g.MeasureString(curveName, legendFont);
                    RectangleF legendRect = new RectangleF((pictureBox.Width - 10) * scaleFactor - legendSize.Width - 60 * scaleFactor, 10 * scaleFactor, legendSize.Width + 60 * scaleFactor, legendSize.Height + 5 * scaleFactor); // 调整图例区域宽度
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 64)), legendRect); 
                    g.DrawString(curveName, legendFont, Brushes.White, new PointF(legendRect.X + 5 * scaleFactor, legendRect.Y + 2.5f * scaleFactor)); 

                    Pen legendPen = new Pen(Color.FromArgb(200, 213, 13), 2 * scaleFactor);
                    g.DrawLine(legendPen, legendRect.X + legendSize.Width + 10 * scaleFactor, legendRect.Y + legendRect.Height / 2, legendRect.X + legendSize.Width + 50 * scaleFactor, legendRect.Y + legendRect.Height / 2); // 曲线长度设置为现在的2倍
                }
            }
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = bitmap;
        }

        public void UpdateGraphs()
        {
            // 清空所有图表
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;

            // 遍历 DataGridView 的所有行
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 获取当前行的 "图表" 列值
                string chartSelection = row.Cells["图表"].Value?.ToString();
                if (chartSelection == "无" || string.IsNullOrEmpty(chartSelection))
                {
                    continue; // 跳过未选择图表的行
                }

                // 获取当前行的序号（减 1 作为索引）
                int index = int.Parse(row.Cells["序号"].Value.ToString()) - 1;

                // 准备数据点
                List<PointF> dataPoints = new List<PointF>();
                for (int i = 0; i < Data.result.result.Count; i++)
                {
                    float x = (float)Data.result.result[i][0]; // X 值
                    float y = (float)Data.result.result[i][index + 1]; // Y 值
                    dataPoints.Add(new PointF(x, y));
                }

                // 获取当前行的描述列值
                string description = row.Cells["描述"].Value?.ToString();

                // 根据 "图表" 列的值绘制到对应的 PictureBox
                if (chartSelection == "图表1")
                {
                    DrawGraph(dataPoints, pictureBox1, true, description);
                }
                else if (chartSelection == "图表2")
                {
                    DrawGraph(dataPoints, pictureBox2, true, description);
                }
                else if (chartSelection == "图表3")
                {
                    DrawGraph(dataPoints, pictureBox3, true, description);
                }
            }
        }
    }
}
