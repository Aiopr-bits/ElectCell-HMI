using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimulationDesignPlatform.UserControls
{
    public partial class UserControl21 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl21()
        {
            InitializeComponent();
            Show_Chart();

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

        private void Show_Chart()
        {
            if (Data.caseUsePath == "" || Data.caseUsePath == null)
            {
                MessageBox.Show("请先指定工作目录！");
                return;
            }
            if (Data.data11.Count == 0)
            {
                return;
            }
            chart1.Series.Clear();
            chart1.Titles.Add("图表-1");
            int check_num = 0;
            if (Data.data11_check != null)
            {
                for (int a = 0; a < Data.data11_check.Length; a++)
                {
                    if (Data.data11_check[a] == true && a > 0)
                    {
                        check_num++;
                    }
                }
                int[] nums = new int[check_num];
                int m = 0;
                for (int b = 0; b < Data.data11_check.Length; b++)
                {
                    if (Data.data11_check[b] == true && b > 0)
                    {
                        nums[m] = b;
                        m++;
                    }
                }

                for (int i = 0; i < check_num; i++)
                {
                    Series series1 = chart1.Series.Add(Data.data11[0][nums[i]]);
                    series1.ChartType = SeriesChartType.Spline;
                    if (Data.check_time[10].Ticks < Data.start_time2.Ticks)
                    {
                        return;
                    }
                    TimeSpan ts = new TimeSpan(Data.check_time[10].Ticks - Data.start_time2.Ticks);
                    int seconds = (int)ts.TotalSeconds;
                    int aa = seconds * 10 - Data.yz_num[10];
                    if (seconds * 10 <= Data.yz_num[10])
                    {
                        for (int k = 1; k < seconds * 10; k++)
                        {
                            series1.Points.AddXY(Data.data11[k][0], Data.data11[k][nums[i]]);
                        }
                    }
                    else if (seconds * 10 > Data.data11.Count && aa < Data.data11.Count)
                    {
                        for (int k = 1; k < seconds * 10 - Data.data11.Count; k++)
                        {
                            series1.Points.AddXY(Data.data11[aa][0], Data.data11[aa][nums[i]]);
                            aa++;
                        }
                    }
                    else if (aa >= 0 && aa <= Data.data11.Count)
                    {

                        for (int k = 1; k < Data.yz_num[11]; k++)
                        {
                            series1.Points.AddXY(Data.data11[aa][0], Data.data11[aa][nums[i]]);
                            aa++;
                        }
                    }
                }
            }

            chart2.Series.Clear();
            chart2.Titles.Add("图表-2");
            int check_num2 = 0;
            if (Data.data12_check != null)
            {
                for (int a = 0; a < Data.data12_check.Length; a++)
                {
                    if (Data.data12_check[a] == true && a > 0)
                    {
                        check_num2++;
                    }
                }
                int[] nums = new int[check_num2];
                int m = 0;
                for (int b = 0; b < Data.data12_check.Length; b++)
                {
                    if (Data.data12_check[b] == true && b > 0)
                    {
                        nums[m] = b;
                        m++;
                    }
                }

                for (int i = 0; i < check_num2; i++)
                {
                    Series series2 = chart2.Series.Add(Data.data12[0][nums[i]]);
                    series2.ChartType = SeriesChartType.Spline;
                    if (Data.check_time[11].Ticks < Data.start_time2.Ticks)
                    {
                        return;
                    }
                    TimeSpan ts = new TimeSpan(Data.check_time[11].Ticks - Data.start_time2.Ticks);
                    int seconds = (int)ts.TotalSeconds;
                    int aa = seconds * 10 - Data.yz_num[11];
                    if (seconds * 10 <= Data.yz_num[11])
                    {
                        for (int k = 1; k < seconds * 10; k++)
                        {
                            series2.Points.AddXY(Data.data12[k][0], Data.data12[k][nums[i]]);
                        }
                    }
                    else if (seconds * 10 > Data.data12.Count && aa < Data.data12.Count)
                    {
                        for (int k = 1; k < seconds * 10 - Data.data12.Count; k++)
                        {
                            series2.Points.AddXY(Data.data12[aa][0], Data.data12[aa][nums[i]]);
                            aa++;
                        }
                    }
                    else if (aa >= 0 && aa <= Data.data12.Count)
                    {

                        for (int k = 1; k < Data.yz_num[11]; k++)
                        {
                            series2.Points.AddXY(Data.data12[aa][0], Data.data12[aa][nums[i]]);
                            aa++;
                        }
                    }
                }

            }

            chart3.Series.Clear();
            chart3.Titles.Add("图表-3");
            int check_num3 = 0;
            if (Data.data13_check != null)
            {
                for (int a = 0; a < Data.data13_check.Length; a++)
                {
                    if (Data.data13_check[a] == true && a > 0)
                    {
                        check_num3++;
                    }
                }
                int[] nums = new int[check_num3];
                int m = 0;
                for (int b = 0; b < Data.data13_check.Length; b++)
                {
                    if (Data.data13_check[b] == true && b > 0)
                    {
                        nums[m] = b;
                        m++;
                    }
                }

                for (int i = 0; i < check_num3; i++)
                {
                    Series series3 = chart3.Series.Add(Data.data13[0][nums[i]]);
                    series3.ChartType = SeriesChartType.Spline;
                    if (Data.check_time[12].Ticks < Data.start_time2.Ticks)
                    {
                        return;
                    }
                    TimeSpan ts = new TimeSpan(Data.check_time[12].Ticks - Data.start_time2.Ticks);
                    int seconds = (int)ts.TotalSeconds;
                    int aa = seconds * 10 - Data.yz_num[12];
                    if (seconds * 10 <= Data.yz_num[12])
                    {
                        for (int k = 1; k < seconds * 10; k++)
                        {
                            series3.Points.AddXY(Data.data13[k][0], Data.data13[k][nums[i]]);
                        }
                    }
                    else if (seconds * 10 > Data.data13.Count && aa < Data.data13.Count)
                    {
                        for (int k = 1; k < seconds * 10 - Data.data13.Count; k++)
                        {
                            series3.Points.AddXY(Data.data13[aa][0], Data.data13[aa][nums[i]]);
                            aa++;
                        }
                    }
                    else if (aa >= 0 && aa <= Data.data13.Count)
                    {

                        for (int k = 1; k < Data.yz_num[12]; k++)
                        {
                            series3.Points.AddXY(Data.data13[aa][0], Data.data13[aa][nums[i]]);
                            aa++;
                        }
                    }
                }

            }

            using (TextFieldParser parser = new TextFieldParser("line_result.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    List<string> row = new List<string>(fields);
                    Data.data14.Add(row);
                }
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (Data.data14.Count > 0)
            {
                dataGridView1.ColumnCount = Data.data14[0].Count;
                for (int k = 0; k < Data.data14[0].Count; k++)
                {
                    dataGridView1.Columns[k].Name = Data.data14[0][k];
                }
            }

            for (int i = 1; i < Data.data14.Count; i++)
            {
                dataGridView1.Rows.Add(Data.data14[i].ToArray());
            }
        }

        private void UserControl21_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }
    }
}
