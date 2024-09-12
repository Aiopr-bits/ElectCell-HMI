using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimulationDesignPlatform.UserControls
{
    public partial class UserControl16 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl16()
        {
            InitializeComponent();
            ShowiniChart();

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

        private void ShowSubChart(DataTable table, Chart chart,string title, bool[] checkList, double left, double right)
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
            int num = 0;
            for (int i = 0; i < Data.data5_check.Length; i++)
            {
                if (num >= 2) return;

                if (Data.data5_check[i] == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                ShowSubChart(Data.ResultDataTable, chart1, "电解电流", Data.data7_check, Data.data7_left, Data.data7_right);
                                num++;
                                break;
                            }
                        case 1:
                            {
                                ShowSubChart(Data.ResultDataTable, chart2, "温度", Data.data8_check, Data.data8_left, Data.data8_right);
                                num++;
                                break;
                            }
                        case 2:
                            {
                                ShowSubChart(Data.ResultDataTable, chart1, "氧中氢", Data.data9_check, Data.data9_left, Data.data9_right);
                                num++;
                                break;
                            }
                        case 3:
                            {
                                ShowSubChart(Data.ResultDataTable, chart2, "氢中氧", Data.data10_check, Data.data10_left, Data.data10_right);
                                num++;
                                break;
                            }
                        case 4:
                            {
                                ShowSubChart(Data.ResultDataTable, chart1, "阴极压力", Data.data1_check, Data.data1_left, Data.data1_right);
                                num++;
                                break;
                            }
                        case 5:
                            {
                                ShowSubChart(Data.ResultDataTable, chart2, "阴极分离器氢气含量", Data.data2_check, Data.data2_left, Data.data2_right);
                                num++;
                                break;
                            }
                        case 6:
                            {
                                ShowSubChart(Data.ResultDataTable, chart1, "阳极压力", Data.data3_check, Data.data3_left, Data.data3_right);
                                num++;
                                break;
                            }
                        case 7:
                            {
                                ShowSubChart(Data.ResultDataTable, chart2, "阳极分离器氧气含量", Data.data4_check, Data.data4_left, Data.data4_right);
                                num++;
                                break;
                            }
                    }
                }
            }
        }

        private void ShowiniChart()
        {

            ShowSubChart(Data.ResultDataTable, chart1, "", Data.data7_check, Data.data7_left, Data.data7_right);
            ShowSubChart(Data.ResultDataTable, chart2, "", Data.data7_check, Data.data7_left, Data.data7_right);
        }
        private void UserControl16_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }
    }
}
