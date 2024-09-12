using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl10 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		public UserControl10()
		{
			InitializeComponent();
			ShowChart();

			#region  初始化控件缩放
			x = Width;
			y = Height;
			setTag(this);
			#endregion
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

		//重写了该方法逻辑。20240401，由M修改
		private void ShowChart()
		{
			if (string.IsNullOrEmpty(Data.caseUsePath))
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}

			dataGridView1.DataSource = Data.ResultDataTable;
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.ReadOnly = true;

			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

			// 双缓冲
			Type dgvType = dataGridView1.GetType();
			PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(dataGridView1, true, null);

			chart1.Series.Clear();
			chart1.Titles.Add("仿真结果");
			int check_num = 0;
			if (Data.data17_check == null)
			{
				return;
			}
			for (int i = 0; i < Data.data17_check.Length; i++)
			{
				if (Data.data17_check[i])
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
			for (int j = 0; j < Data.data17_check.Length; j++)
			{
				if (Data.data17_check[j])
				{
					nums[m] = j + 1;
					m++;
				}
			}

			// 绘图阈值设置。20240401，由M添加
			double max, min;
			max = min = (double)dataGridView1[nums[0], 0].Value;
			for (int i = 0; i < check_num; i++)
			{
				Series series1 = chart1.Series.Add(dataGridView1.Columns[nums[i]].Name);
				series1.ChartType = SeriesChartType.Spline;
				for (int k = 0; k < dataGridView1.RowCount; k++)
				{
					series1.Points.AddXY(dataGridView1[0, k].Value, dataGridView1[nums[i], k].Value);
					max = Math.Max(max, Convert.ToDouble(dataGridView1[nums[i], k].Value));
					min = Math.Min(min, Convert.ToDouble(dataGridView1[nums[i], k].Value));
				}
			}
			double range = max - min;
			range = range == 0 ? 1 : range;
			chart1.ChartAreas[0].AxisY.Maximum = max + range * 0.05;
			chart1.ChartAreas[0].AxisY.Minimum = min - range * 0.05;
			chart1.ChartAreas[0].AxisX.Title = "Time";
		}

		private void UserControl10_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}
	}
}
