using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl13 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度

		private int fault_id = 0;
		private int fault_id2 = 0;
		public UserControl13()
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

		private void GetDatabase()
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}
			dataGridView1.AllowUserToAddRows = false;
			DataTable dataTable01 = new DataTable();

			dataTable01.Columns.Add("num", typeof(int));
			dataTable01.Columns.Add("x_h2", typeof(double));
			dataTable01.Columns.Add("x_o2", typeof(double));
			dataTable01.Columns.Add("x_h2o", typeof(double));
			dataTable01.Columns.Add("Di", typeof(double));
			dataTable01.Columns.Add("L", typeof(double));
			dataTable01.Columns.Add("v_t", typeof(double));
			dataTable01.Columns.Add("is_zhuru", typeof(bool));
			dataTable01.Columns.Add("is_result", typeof(bool));

			// 设置DataGridView的DataSource  
			dataGridView1.DataSource = dataTable01;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// 设置列名  
			dataGridView1.Columns["num"].HeaderText = "num";
			dataGridView1.Columns["num"].ReadOnly = true;
			dataGridView1.Columns["x_h2"].HeaderText = "x_h2";
			dataGridView1.Columns["x_h2"].ReadOnly = true;
			dataGridView1.Columns["x_o2"].HeaderText = "x_o2";
			dataGridView1.Columns["x_o2"].ReadOnly = true;
			dataGridView1.Columns["x_h2o"].HeaderText = "x_h2o";
			dataGridView1.Columns["x_h2o"].ReadOnly = true;
			dataGridView1.Columns["Di"].HeaderText = "Di";
			dataGridView1.Columns["Di"].ReadOnly = true;
			dataGridView1.Columns["L"].HeaderText = "L";
			dataGridView1.Columns["L"].ReadOnly = true;
			dataGridView1.Columns["v_t"].HeaderText = "v_t";
			dataGridView1.Columns["v_t"].ReadOnly = true;
			dataGridView1.Columns["is_zhuru"].HeaderText = "是否故障注入";
			dataGridView1.Columns["is_result"].HeaderText = "是否查看结果";

			for (int i = 0; i < Data.faultflow.Length; i++)
			{
				if (Data.faultflow[0] == null)
				{
					return;
				}
				//添加行数据
				DataRow row = dataTable01.NewRow();
				row["num"] = Data.faultflow[i].num;
				row["x_h2"] = Data.faultflow[i].x_h2;
				row["x_o2"] = Data.faultflow[i].x_o2;
				row["x_h2o"] = Data.faultflow[i].x_h2o;
				row["Di"] = Data.faultflow[i].Di;
				row["L"] = Data.faultflow[i].L;
				row["v_t"] = Data.faultflow[i].v_t;
				row["is_zhuru"] = Data.faultflow[i].is_fault;
				row["is_result"] = Data.faultflow[i].is_result;
				if (Data.faultflow[i].num > 0)
				{
					dataTable01.Rows.Add(row.ItemArray);
					fault_id = Data.faultflow[i].num;
				}
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 1)
			{
				GetDatabase2();
			}
			if (tabControl1.SelectedIndex == 2)
			{
				GetDatabase3();
			}
			if (tabControl1.SelectedIndex == 3)
			{
				GetDatabase4();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			dataGridView2.AllowUserToAddRows = false;

			for (int i = 0; i < Data.n_flow; i++)
			{
				if (Data.flow_f[i].num != 0)
				{
					Data.flow_f[i].num = (int)dataGridView2.Rows[i].Cells[0].Value;
					Data.flow_f[i].x_h2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
					Data.flow_f[i].x_o2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
					Data.flow_f[i].x_h2o = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
					Data.flow_f[i].Di = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
					Data.flow_f[i].L = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
					Data.flow_f[i].v_t = Convert.ToInt32(dataGridView2.Rows[i].Cells[6].Value);
				}

			}
			GetDatabase2();
			Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}

		private void button2_Click(object sender, EventArgs e)
		{
			dataGridView1.AllowUserToAddRows = false;

			Array.Clear(Data.flow_f, 0, Data.flow_f.Length); // 清空数组中的所有元素
			for (int i = 0; i < Data.n_flow_max; i++)
			{
				Data.flow_f[i] = new FlowData();    //故障注入流股
			}

			int a = 0;
			for (int i = 0; i < Data.n_flow; i++)
			{
				Data.faultflow[i].num = (int)dataGridView1.Rows[i].Cells[0].Value;
				Data.faultflow[i].x_h2 = (double)dataGridView1.Rows[i].Cells[1].Value;
				Data.faultflow[i].x_o2 = (double)dataGridView1.Rows[i].Cells[2].Value;
				Data.faultflow[i].x_h2o = (double)dataGridView1.Rows[i].Cells[3].Value;
				Data.faultflow[i].Di = (double)dataGridView1.Rows[i].Cells[4].Value;
				Data.faultflow[i].L = (double)dataGridView1.Rows[i].Cells[5].Value;
				Data.faultflow[i].v_t = (double)dataGridView1.Rows[i].Cells[6].Value;
				Data.faultflow[i].is_fault = (bool)dataGridView1.Rows[i].Cells[7].Value;
				Data.faultflow[i].is_result = (bool)dataGridView1.Rows[i].Cells[8].Value;
				if (Data.faultflow[i].is_fault || Data.faultflow[i].is_result)
				{
					Data.flow_f[a].num = Data.faultflow[i].num;
					Data.flow_f[a].x_h2 = Data.faultflow[i].x_h2;
					Data.flow_f[a].x_o2 = Data.faultflow[i].x_o2;
					Data.flow_f[a].x_h2o = Data.faultflow[i].x_h2o;
					Data.flow_f[a].Di = Data.faultflow[i].Di;
					Data.flow_f[a].L = Data.faultflow[i].L;
					Data.flow_f[a].v_t = Data.faultflow[i].v_t;
					a++;
				}
			}

			GetDatabase();
			Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}

		private void GetDatabase2()
		{
			dataGridView2.AllowUserToAddRows = false;
			DataTable dataTable01 = new DataTable();

			dataTable01.Columns.Add("num", typeof(int));
			dataTable01.Columns.Add("x_h2", typeof(double));
			dataTable01.Columns.Add("x_o2", typeof(double));
			dataTable01.Columns.Add("x_h2o", typeof(double));
			dataTable01.Columns.Add("Di", typeof(double));
			dataTable01.Columns.Add("L", typeof(double));
			dataTable01.Columns.Add("v_t", typeof(double));

			// 设置DataGridView的DataSource  
			dataGridView2.DataSource = dataTable01;
			dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// 设置列名  
			dataGridView2.Columns["num"].HeaderText = "num";
			dataGridView2.Columns["x_h2"].HeaderText = "x_h2";
			dataGridView2.Columns["x_o2"].HeaderText = "x_o2";
			dataGridView2.Columns["x_h2o"].HeaderText = "x_h2o";
			dataGridView2.Columns["Di"].HeaderText = "Di";
			dataGridView2.Columns["L"].HeaderText = "L";
			dataGridView2.Columns["v_t"].HeaderText = "v_t";

			for (int i = 0; i < Data.n_flow; i++)
			{
				//添加行数据
				DataRow row = dataTable01.NewRow();
				row["num"] = Data.flow_f[i].num;
				row["x_h2"] = Data.flow_f[i].x_h2;
				row["x_o2"] = Data.flow_f[i].x_o2;
				row["x_h2o"] = Data.flow_f[i].x_h2o;
				row["Di"] = Data.flow_f[i].Di;
				row["L"] = Data.flow_f[i].L;
				row["v_t"] = Data.flow_f[i].v_t;

				if (Data.flow_f[i].num != 0)
				{
					dataTable01.Rows.Add(row);
				}

			}
		}

		private void GetDatabase3()
		{
			dataGridView3.AllowUserToAddRows = false;
			DataTable dataTable01 = new DataTable();

			dataTable01.Columns.Add("num", typeof(int));
			dataTable01.Columns.Add("n", typeof(double));
			dataTable01.Columns.Add("v", typeof(double));
			dataTable01.Columns.Add("p", typeof(double));
			dataTable01.Columns.Add("l_l", typeof(double));
			dataTable01.Columns.Add("l_g", typeof(double));
			dataTable01.Columns.Add("x_h2", typeof(double));
			dataTable01.Columns.Add("x_o2", typeof(double));
			dataTable01.Columns.Add("x_h2o", typeof(double));
			dataTable01.Columns.Add("is_zhuru", typeof(bool));
			dataTable01.Columns.Add("is_result", typeof(bool));

			// 设置DataGridView的DataSource  
			dataGridView3.DataSource = dataTable01;
			dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// 设置列名  
			dataGridView3.Columns["num"].HeaderText = "num";
			dataGridView3.Columns["num"].ReadOnly = true;
			dataGridView3.Columns["n"].HeaderText = "n";
			dataGridView3.Columns["n"].ReadOnly = true;
			dataGridView3.Columns["v"].HeaderText = "v";
			dataGridView3.Columns["v"].ReadOnly = true;
			dataGridView3.Columns["p"].HeaderText = "p";
			dataGridView3.Columns["p"].ReadOnly = true;
			dataGridView3.Columns["l_l"].HeaderText = "l_l";
			dataGridView3.Columns["l_l"].ReadOnly = true;
			dataGridView3.Columns["l_g"].HeaderText = "l_g";
			dataGridView3.Columns["l_g"].ReadOnly = true;
			dataGridView3.Columns["x_h2"].HeaderText = "x_h2";
			dataGridView3.Columns["x_h2"].ReadOnly = true;
			dataGridView3.Columns["x_o2"].HeaderText = "x_o2";
			dataGridView3.Columns["x_o2"].ReadOnly = true;
			dataGridView3.Columns["x_h2o"].HeaderText = "x_h2o";
			dataGridView3.Columns["x_h2o"].ReadOnly = true;
			dataGridView3.Columns["is_zhuru"].HeaderText = "是否故障注入";
			dataGridView3.Columns["is_result"].HeaderText = "是否查看结果";

			for (int i = 0; i < Data.faultps.Length; i++)
			{
				if (Data.faultps[0] == null)
				{
					return;
				}
				//添加行数据
				DataRow row = dataTable01.NewRow();
				row["num"] = Data.faultps[i].num;
				row["n"] = Data.faultps[i].n;
				row["v"] = Data.faultps[i].v;
				row["p"] = Data.faultps[i].p;
				row["l_l"] = Data.faultps[i].l_l;
				row["l_g"] = Data.faultps[i].l_g;
				row["x_h2"] = Data.faultps[i].x_h2;
				row["x_o2"] = Data.faultps[i].x_o2;
				row["x_h2o"] = Data.faultps[i].x_h2o;
				row["is_zhuru"] = Data.faultps[i].is_fault;
				row["is_result"] = Data.faultps[i].is_result;
				if (Data.faultps[i].num > 0)
				{
					dataTable01.Rows.Add(row.ItemArray);
					fault_id2 = Data.faultps[i].num;
				}
			}
		}

		private void GetDatabase4()
		{
			dataGridView4.AllowUserToAddRows = false;
			DataTable dataTable01 = new DataTable();

			dataTable01.Columns.Add("num", typeof(int));
			dataTable01.Columns.Add("n", typeof(double));
			dataTable01.Columns.Add("v", typeof(double));
			dataTable01.Columns.Add("p", typeof(double));
			dataTable01.Columns.Add("l_l", typeof(double));
			dataTable01.Columns.Add("l_g", typeof(double));
			dataTable01.Columns.Add("x_h2", typeof(double));
			dataTable01.Columns.Add("x_o2", typeof(double));
			dataTable01.Columns.Add("x_h2o", typeof(double));

			// 设置DataGridView的DataSource  
			dataGridView4.DataSource = dataTable01;
			dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// 设置列名  
			dataGridView4.Columns["num"].HeaderText = "num";
			dataGridView4.Columns["n"].HeaderText = "n";
			dataGridView4.Columns["v"].HeaderText = "v";
			dataGridView4.Columns["p"].HeaderText = "p";
			dataGridView4.Columns["l_l"].HeaderText = "l_l";
			dataGridView4.Columns["l_g"].HeaderText = "l_g";
			dataGridView4.Columns["x_h2"].HeaderText = "x_h2";
			dataGridView4.Columns["x_o2"].HeaderText = "x_o2";
			dataGridView4.Columns["x_h2o"].HeaderText = "x_h2o";

			for (int i = 0; i < Data.n_ps; i++)
			{
				//添加行数据
				DataRow row = dataTable01.NewRow();
				row["num"] = Data.ps_f[i].num;
				row["n"] = Data.ps_f[i].n;
				row["v"] = Data.ps_f[i].v;
				row["p"] = Data.ps_f[i].p;
				row["l_l"] = Data.ps_f[i].l_l;
				row["l_g"] = Data.ps_f[i].l_g;
				row["x_h2"] = Data.ps_f[i].x_h2;
				row["x_o2"] = Data.ps_f[i].x_o2;
				row["x_h2o"] = Data.ps_f[i].x_h2o;

				if (Data.ps_f[i].num != 0)
				{
					dataTable01.Rows.Add(row);
				}

			}
		}

		private void userControl13_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			dataGridView3.AllowUserToAddRows = false;

			Array.Clear(Data.ps_f, 0, Data.ps_f.Length); // 清空数组中的所有元素
			for (int i = 0; i < Data.n_ps_max; i++)
			{
				Data.ps_f[i] = new PsData();    //故障注入流股
			}

			int a = 0;
			for (int i = 0; i < fault_id2; i++)
			{
				Data.faultps[i].num = (int)dataGridView3.Rows[i].Cells[0].Value;
				Data.faultps[i].n = (double)dataGridView3.Rows[i].Cells[1].Value;
				Data.faultps[i].v = (double)dataGridView3.Rows[i].Cells[2].Value;
				Data.faultps[i].p = (double)dataGridView3.Rows[i].Cells[3].Value;
				Data.faultps[i].l_l = (double)dataGridView3.Rows[i].Cells[4].Value;
				Data.faultps[i].l_g = (double)dataGridView3.Rows[i].Cells[5].Value;
				Data.faultps[i].x_h2 = (double)dataGridView3.Rows[i].Cells[6].Value;
				Data.faultps[i].x_o2 = (double)dataGridView3.Rows[i].Cells[7].Value;
				Data.faultps[i].x_h2o = (double)dataGridView3.Rows[i].Cells[8].Value;
				Data.faultps[i].is_fault = (bool)dataGridView3.Rows[i].Cells[9].Value;
				Data.faultps[i].is_result = (bool)dataGridView3.Rows[i].Cells[10].Value;
				if (Data.faultps[i].is_fault || Data.faultps[i].is_result)
				{
					Data.ps_f[a].num = Data.faultps[i].num;
					Data.ps_f[a].n = Data.faultps[i].n;
					Data.ps_f[a].v = Data.faultps[i].v;
					Data.ps_f[a].p = Data.faultps[i].p;
					Data.ps_f[a].l_l = Data.faultps[i].l_l;
					Data.ps_f[a].l_g = Data.faultps[i].l_g;
					Data.ps_f[a].x_h2 = Data.faultps[i].x_h2;
					Data.ps_f[a].x_o2 = Data.faultps[i].x_o2;
					Data.ps_f[a].x_h2o = Data.faultps[i].x_h2o;
					a++;
				}
			}

			GetDatabase3();
			Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}

		private void button4_Click(object sender, EventArgs e)
		{
			dataGridView4.AllowUserToAddRows = false;

			for (int i = 0; i < Data.n_ps; i++)
			{
				if (Data.ps_f[i].num != 0)
				{
					Data.ps_f[i].num = (int)dataGridView2.Rows[i].Cells[0].Value;
					Data.ps_f[i].n = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
					Data.ps_f[i].v = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
					Data.ps_f[i].p = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
					Data.ps_f[i].l_l = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
					Data.ps_f[i].l_g = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
					Data.ps_f[i].x_h2 = Convert.ToInt32(dataGridView2.Rows[i].Cells[6].Value);
					Data.ps_f[i].x_o2 = Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
					Data.ps_f[i].x_h2o = Convert.ToInt32(dataGridView2.Rows[i].Cells[8].Value);
				}

			}
			GetDatabase4();
			Task.Run(() =>
			{
				MessageBox.Show("保存成功！");
			});
		}
	}
}
