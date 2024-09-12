using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl15 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		public UserControl15()
		{
			InitializeComponent();
			Show_pic();
			ShowChart();

			#region  初始化控件缩放
			x = Width;
			y = Height;
			setTag(this);
			#endregion
		}

		private void Show_pic()
		{
			PictureBox pictureBox1 = new PictureBox();
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.Dock = DockStyle.Fill;
			if (Data.imagePath != null)
			{
				pictureBox1.LoadAsync(Data.imagePath);
				panelFlowChart.Controls.Add(pictureBox1);
			}
		}

		// 绘制子图。20240408，由M修改添加
		private void ShowSubChart(DataTable table, Chart chart,
			string title, bool[] checkList, double left, double right)
		{
			// 读取历史数据选择状态
			chart.Series.Clear();
			chart.Titles.Add(title);
			int check_num = 0;
			if (checkList is null)
			{
				return;
			}
			for (int i = 0; i < checkList.Length; i++)
			{
				if (checkList[i])
				{
					check_num++;
				}
			}
			if (check_num == 0)
			{
				return;
			}
			int[] nums = new int[check_num];
			int m = 0;
			for (int j = 0; j < checkList.Length; j++)
			{
				if (checkList[j])
				{
					nums[m] = j + 1;
					m++;
				}
			}

			// 绘图阈值设置
			double max, min;
			max = min = (double)table.Rows[0].ItemArray[nums[0]];
			for (int i = 0; i < check_num; i++)
			{
				Series series = chart.Series.Add(table.Columns[nums[i]].ColumnName);
				series.ChartType = SeriesChartType.Spline;
				for (int k = 0; k < table.Rows.Count; k++)
				{
					series.Points.AddXY(table.Rows[k].ItemArray[0], table.Rows[k].ItemArray[nums[i]]);
					max = Math.Max(max, Convert.ToDouble(table.Rows[k].ItemArray[nums[i]]));
					min = Math.Min(min, Convert.ToDouble(table.Rows[k].ItemArray[nums[i]]));
				}
			}
			double range = max - min;
			range = range == 0 ? 1 : range;
			chart.ChartAreas[0].AxisY.Minimum = min - range * 0.05;
			chart.ChartAreas[0].AxisY.Maximum = max + range * 0.05;
			chart.ChartAreas[0].AxisX.Title = "Time";
			chart.ChartAreas[0].AxisX.Minimum = left;
			chart.ChartAreas[0].AxisX.Maximum = right;
		}

		private void ShowChart()
		{
			if (string.IsNullOrEmpty(Data.caseUsePath))
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}
			ShowSubChart(Data.ResultDataTable, chart1, "电解电流",
				Data.data7_check, Data.data7_left, Data.data7_right);
			ShowSubChart(Data.ResultDataTable, chart2, "温度",
				Data.data8_check, Data.data8_left, Data.data8_right);
			ShowSubChart(Data.ResultDataTable, chart3, "氧中氢",
				Data.data9_check, Data.data9_left, Data.data9_right);
			ShowSubChart(Data.ResultDataTable, chart4, "氢中氧",
				Data.data10_check, Data.data10_left, Data.data10_right);
		}

		private void UserControl15_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void setTag(Control cons)
		{
			foreach (Control con in cons.Controls)
			{
				con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
				if (con.Controls.Count > 0) setTag(con);
			}
		}

		private void setControls(float newx, float newy, Control cons)
		{
			foreach (Control con in cons.Controls)
			{
				if (con.Tag != null)
				{
					var mytag = con.Tag.ToString().Split(';');
					con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx);
					con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy);
					con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx);
					con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy);
					var currentSize = Convert.ToSingle(mytag[4]) * newy;

					if (currentSize > 0)
					{
						FontFamily fontFamily = new FontFamily(con.Font.Name);
						con.Font = new Font(fontFamily, currentSize, con.Font.Style, con.Font.Unit);
					}
					con.Focus();
					if (con.Controls.Count > 0) setControls(newx, newy, con);
				}
			}
		}

		private void ReWinformLayout()
		{
			var newx = Width / x;
			var newy = Height / y;
			setControls(newx, newy, this);
		}
	}
}
