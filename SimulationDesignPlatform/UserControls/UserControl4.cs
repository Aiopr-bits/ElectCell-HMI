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

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl4 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度

		// 可能用到的单元格样式。20240322，由M添加
		private readonly DataGridViewCellStyle style = new DataGridViewCellStyle
		{
			Alignment = DataGridViewContentAlignment.MiddleCenter,
		};

		public UserControl4()
		{
			InitializeComponent();
			GetDatabase();
			DoubleBufferedControl(dataGridView1);

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

		private void GetDatabaseOld()
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}
			DataTable dataTable01 = new DataTable();

			dataTable01.Columns.Add("start_time", typeof(double));
			dataTable01.Columns.Add("end_time", typeof(double));
			dataTable01.Columns.Add("delta_t", typeof(double));
			dataTable01.Columns.Add("cal_current", typeof(bool));
			dataTable01.Columns.Add("cal_valve", typeof(bool));
			dataTable01.Columns.Add("cal_pump", typeof(bool));
			dataTable01.Columns.Add("cal_balance_pipe", typeof(bool));
			dataTable01.Columns.Add("cal_mini_1", typeof(bool));
			dataTable01.Columns.Add("cal_mini_2", typeof(bool));
			dataTable01.Columns.Add("use_ff_static", typeof(bool));
			dataTable01.Columns.Add("IsMixed_circleType", typeof(bool));
			dataTable01.Columns.Add("cal_superSat_fickTrans", typeof(bool));
			dataTable01.Columns.Add("ec_pump_independent", typeof(bool));

			// 设置DataGridView的DataSource  
			dataGridView1.DataSource = dataTable01;
			dataGridView1.AllowUserToAddRows = false;

			// 设置列名  
			dataGridView1.Columns["start_time"].HeaderText = "start_time";
			dataGridView1.Columns["end_time"].HeaderText = "end_time";
			dataGridView1.Columns["delta_t"].HeaderText = "delta_t";
			dataGridView1.Columns["cal_current"].HeaderText = "cal_current";
			dataGridView1.Columns["cal_valve"].HeaderText = "cal_valve";
			dataGridView1.Columns["cal_pump"].HeaderText = "cal_pump";
			dataGridView1.Columns["cal_balance_pipe"].HeaderText = "cal_balance_pipe";
			dataGridView1.Columns["cal_mini_1"].HeaderText = "cal_mini_1";
			dataGridView1.Columns["cal_mini_2"].HeaderText = "cal_mini_2";
			dataGridView1.Columns["use_ff_static"].HeaderText = "use_ff_static";
			dataGridView1.Columns["IsMixed_circleType"].HeaderText = "IsMixed_circleType";
			dataGridView1.Columns["cal_superSat_fickTrans"].HeaderText = "cal_superSat_fickTrans";
			dataGridView1.Columns["ec_pump_independent"].HeaderText = "ec_pump_independent";

			//添加行数据
			DataRow row = dataTable01.NewRow();
			row["start_time"] = Data.start_time;
			row["end_time"] = Data.end_time;
			row["delta_t"] = Data.delta_t;
			row["cal_current"] = Data.cal_current;
			row["cal_valve"] = Data.cal_valve;
			row["cal_pump"] = Data.cal_pump;
			row["cal_balance_pipe"] = Data.cal_balance_pipe;
			row["cal_mini_1"] = Data.cal_mini_1;
			row["cal_mini_2"] = Data.cal_mini_2;
			row["use_ff_static"] = Data.use_ff_static;
			row["IsMixed_circleType"] = Data.IsMixed_circleType;
			row["cal_superSat_fickTrans"] = Data.cal_superSat_fickTrans;
			row["ec_pump_independent"] = Data.ec_pump_independent;

			dataTable01.Rows.Add(row);
		}

		// 双缓冲
		private void DoubleBufferedControl(Control control)
		{
			Type conType = control.GetType();
			PropertyInfo pi = conType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(control, true, null);
		}

		// 用户要求增加显示变量单位及含义。20240322，由M添加
		private void GetDatabase()
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			if (dataGridView1.Columns.Count == 0)
			{
				dataGridView1.Columns.Add(new DataGridViewColumn()
				{
					Name = "num",
					HeaderText = "序号",
					ReadOnly = true,
				});
				dataGridView1.Columns.Add(new DataGridViewColumn()
				{
					Name = "name",
					HeaderText = "变量名",
					ReadOnly = true,
				});
				dataGridView1.Columns.Add(new DataGridViewColumn()
				{
					Name = "value",
					HeaderText = "变量值",
				});
				dataGridView1.Columns.Add(new DataGridViewColumn()
				{
					Name = "unit",
					HeaderText = "单位",
					ReadOnly = true,
				});
				dataGridView1.Columns.Add(new DataGridViewColumn()
				{
					Name = "note",
					HeaderText = "含义",
					ReadOnly = true,
				});

				int i = 1;
				dataGridView1.Rows.Add(GenerateRow(i++, "start_time", Data.start_time, "s", "开始计算时间", false));
				dataGridView1.Rows.Add(GenerateRow(i++, "end_time", Data.end_time, "s", "结束计算时间", false));
				dataGridView1.Rows.Add(GenerateRow(i++, "delta_t", Data.delta_t, "s", "时间步长", false));
				dataGridView1.Rows.Add(GenerateRow(i++, "cal_current", Data.cal_current, "-", "打开电流计算", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "cal_valve", Data.cal_valve, "-", "打开阀门计算", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "cal_pump", Data.cal_pump, "-", "打开泵计算", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "cal_balance_pipe", Data.cal_balance_pipe, "-", "打开平衡管路计算", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "use_ff_static", Data.use_ff_static, "-", "管路摩擦系数定值", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "IsMixed_circleType", Data.IsMixed_circleType, "-", "循环泵混合模式", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "cal_superSat_fickTrans", Data.cal_superSat_fickTrans, "-", "是否计算饱和分压", true));
				dataGridView1.Rows.Add(GenerateRow(i++, "ec_pump_independent", Data.ec_pump_independent, "-", "电解槽泵独立计算", true));
			}
			else
			{
				int i = 0;
				dataGridView1[2, i++].Value = Data.start_time;
				dataGridView1[2, i++].Value = Data.end_time;
				dataGridView1[2, i++].Value = Data.delta_t;
				dataGridView1[2, i++].Value = Data.cal_current;
				dataGridView1[2, i++].Value = Data.cal_valve;
				dataGridView1[2, i++].Value = Data.cal_pump;
				dataGridView1[2, i++].Value = Data.cal_balance_pipe;
				dataGridView1[2, i++].Value = Data.use_ff_static;
				dataGridView1[2, i++].Value = Data.IsMixed_circleType;
				dataGridView1[2, i++].Value = Data.cal_superSat_fickTrans;
				dataGridView1[2, i++].Value = Data.ec_pump_independent;
			}
		}

		private DataGridViewRow GenerateRow(int num, string name, object value,
			string unit, string note, bool isCheckBox)
		{
			DataGridViewRow row = new DataGridViewRow();
			row.Cells.Add(new DataGridViewTextBoxCell()
			{
				Value = num,
			});
			row.Cells.Add(new DataGridViewTextBoxCell()
			{
				Value = name,
			});
			if (isCheckBox)
			{
				row.Cells.Add(new DataGridViewCheckBoxCell()
				{
					Value = value,
					Style = style,
				});
			}
			else
			{
				row.Cells.Add(new DataGridViewTextBoxCell()
				{
					Value = value,
				});
			}
			row.Cells.Add(new DataGridViewTextBoxCell()
			{
				Value = unit,
			});
			row.Cells.Add(new DataGridViewTextBoxCell()
			{
				Value = note,
			});
			return row;
		}

		private void UserControl4_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			dataGridView1.AllowUserToAddRows = false;

			// 由于修改了变量显示，对应修改存储逻辑。20240322，由M添加
			int i = 0;
			Data.start_time = Convert.ToDouble(dataGridView1[2, i++].Value);
			Data.end_time = Convert.ToDouble(dataGridView1[2, i++].Value);
			Data.delta_t = Convert.ToDouble(dataGridView1[2, i++].Value);
			Data.cal_current = (bool)dataGridView1[2, i++].Value;
			Data.cal_valve = (bool)dataGridView1[2, i++].Value;
			Data.cal_pump = (bool)dataGridView1[2, i++].Value;
			Data.cal_balance_pipe = (bool)dataGridView1[2, i++].Value;
			Data.use_ff_static = (bool)dataGridView1[2, i++].Value;
			Data.IsMixed_circleType = (bool)dataGridView1[2, i++].Value;
			Data.cal_superSat_fickTrans = (bool)dataGridView1[2, i++].Value;
			Data.ec_pump_independent = (bool)dataGridView1[2, i++].Value;

			//GetDatabase();

			//点了保存按钮进⼊
			Data.saveFile = Path.Combine(Data.exePath, Data.case_name, "data_input.csv"); 
			Data.GUI2CSV(@Data.saveFile);

			Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}

	}
}
