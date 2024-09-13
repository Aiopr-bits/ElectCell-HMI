using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl5 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		public UserControl5()
		{
			InitializeComponent();
			GetDatabase();

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

			dataTable01.Columns.Add("L_ca2se", typeof(double));
			dataTable01.Columns.Add("L_an2se", typeof(double));
			dataTable01.Columns.Add("D_sc", typeof(double));
			dataTable01.Columns.Add("l_sc", typeof(double));

			// 设置DataGridView的DataSource  
			dataGridView1.DataSource = dataTable01;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridView1.AllowUserToAddRows = false;

			// 设置列名  
			dataGridView1.Columns["L_ca2se"].HeaderText = "L_ca2se";
			dataGridView1.Columns["L_an2se"].HeaderText = "L_an2se";
			dataGridView1.Columns["D_sc"].HeaderText = "D_sc";
			dataGridView1.Columns["l_sc"].HeaderText = "l_sc";

			//添加行数据
			DataRow row = dataTable01.NewRow();
			row["L_ca2se"] = Data.L_ca2se;
			row["L_an2se"] = Data.L_an2se;
			row["D_sc"] = Data.D_sc;
			row["l_sc"] = Data.l_sc;

			dataTable01.Rows.Add(row);
		}

		// 用户要求增加显示变量单位及含义。20240322，由M添加
		private void GetDatabase()
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Columns.Count == 0)
			{
				dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
				{
					Name = "num",
					HeaderText = "序号",
					ReadOnly = true,
					ValueType = typeof(int),
				});
				dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
				{
					Name = "name",
					HeaderText = "变量名",
					ReadOnly = true,
					ValueType = typeof(string),
				});
				dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
				{
					Name = "value",
					HeaderText = "变量值",
					ValueType = typeof(double),
				});
				dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
				{
					Name = "unit",
					HeaderText = "单位",
					ReadOnly = true,
					ValueType = typeof(string),
				});
				dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
				{
					Name = "note",
					HeaderText = "含义",
					ReadOnly = true,
					ValueType = typeof(string),
				});
				int i = 1;
				dataGridView1.Rows.Add(i++, "L_ca2se", Data.L_ca2se, "m", "氢气分离器长度");
				dataGridView1.Rows.Add(i++, "L_an2se", Data.L_an2se, "m", "氧气分离器长度");
				dataGridView1.Rows.Add(i++, "D_sc", Data.D_sc, "m", "电解槽直径");
				dataGridView1.Rows.Add(i++, "l_sc", Data.l_sc, "m", "电解槽长度");
			}
			else
			{
				int i = 0;
				dataGridView1[2, i++].Value = Data.L_ca2se;
				dataGridView1[2, i++].Value = Data.L_an2se;
				dataGridView1[2, i++].Value = Data.D_sc;
				dataGridView1[2, i++].Value = Data.l_sc;
			}
		}

		private void button1_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			dataGridView1.AllowUserToAddRows = false;

			Data.L_ca2se = (double)dataGridView1[2, 0].Value;
			Data.L_an2se = (double)dataGridView1[2, 1].Value;
			Data.D_sc = (double)dataGridView1[2, 2].Value;
			Data.l_sc = (double)dataGridView1[2, 3].Value;

            //点了保存按钮进⼊
            Data.saveFile = Path.Combine(Data.exePath, Data.case_name, "data_input.csv");
            Data.GUI2CSV(@Data.saveFile);

            GetDatabase();

            Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}

	}
}
