using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.UserControls
{
	public partial class UserControl8 : UserControl
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		public UserControl8()
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

			dataTable01.Columns.Add("sigma_e_1", typeof(double));
			dataTable01.Columns.Add("sigma_h2_r1", typeof(double));
			dataTable01.Columns.Add("sigma_h2o_r1", typeof(double));
			dataTable01.Columns.Add("sigma_e_2", typeof(double));
			dataTable01.Columns.Add("sigma_h2o_r2", typeof(double));
			dataTable01.Columns.Add("sigma_o2_r2", typeof(double));
			dataTable01.Columns.Add("eta_F", typeof(double));
			dataTable01.Columns.Add("F", typeof(double));
			dataTable01.Columns.Add("n_cell", typeof(double));
			dataTable01.Columns.Add("a_cell", typeof(double));
			dataTable01.Columns.Add("A_mem", typeof(double));
			dataTable01.Columns.Add("thickness_mem", typeof(double));
			dataTable01.Columns.Add("porosity_mem", typeof(double));
			dataTable01.Columns.Add("tortuosity_mem", typeof(double));
			dataTable01.Columns.Add("wt_KOHsln", typeof(double));
			dataTable01.Columns.Add("k", typeof(double));
			dataTable01.Columns.Add("D_h2", typeof(double));
			dataTable01.Columns.Add("D_o2", typeof(double));
			dataTable01.Columns.Add("k_x_h2", typeof(double));
			dataTable01.Columns.Add("k_x_o2", typeof(double));
			dataTable01.Columns.Add("eps_h2_Darcy", typeof(double));
			dataTable01.Columns.Add("eps_o2_Darcy", typeof(double));
			dataTable01.Columns.Add("tao_b", typeof(double));
			dataTable01.Columns.Add("FC_flash", typeof(double));
			dataTable01.Columns.Add("R", typeof(double));
			dataTable01.Columns.Add("T", typeof(double));
			dataTable01.Columns.Add("eta", typeof(double));
			dataTable01.Columns.Add("M_h2", typeof(double));
			dataTable01.Columns.Add("M_o2", typeof(double));
			dataTable01.Columns.Add("M_koh", typeof(double));
			dataTable01.Columns.Add("M_h2o", typeof(double));
			dataTable01.Columns.Add("rho_h2o", typeof(double));
			dataTable01.Columns.Add("rho_h2", typeof(double));
			dataTable01.Columns.Add("rho_o2", typeof(double));
			dataTable01.Columns.Add("rho_sln_koh", typeof(double));
			dataTable01.Columns.Add("g", typeof(double));
			dataTable01.Columns.Add("Re24_0", typeof(double));
			dataTable01.Columns.Add("mu", typeof(double));
			dataTable01.Columns.Add("Area_hx", typeof(double));
			dataTable01.Columns.Add("massFlowRate_cw", typeof(double));
			dataTable01.Columns.Add("cv1", typeof(double));
			dataTable01.Columns.Add("cv2", typeof(double));
			dataTable01.Columns.Add("P_cathode_sep_out", typeof(double));
			dataTable01.Columns.Add("P_anode_sep_out", typeof(double));
			dataTable01.Columns.Add("P_env", typeof(double));

			// 设置DataGridView的DataSource  
			dataGridView1.DataSource = dataTable01;
			dataGridView1.AllowUserToAddRows = false;

			// 设置列名  
			dataGridView1.Columns["sigma_e_1"].HeaderText = "sigma_e_1";
			dataGridView1.Columns["sigma_h2_r1"].HeaderText = "sigma_h2_r1";
			dataGridView1.Columns["sigma_h2o_r1"].HeaderText = "sigma_h2o_r1";
			dataGridView1.Columns["sigma_e_2"].HeaderText = "sigma_e_2";
			dataGridView1.Columns["sigma_h2o_r2"].HeaderText = "sigma_h2o_r2";
			dataGridView1.Columns["sigma_o2_r2"].HeaderText = "sigma_o2_r2";
			dataGridView1.Columns["eta_F"].HeaderText = "eta_F";
			dataGridView1.Columns["F"].HeaderText = "F";
			dataGridView1.Columns["n_cell"].HeaderText = "n_cell";
			dataGridView1.Columns["a_cell"].HeaderText = "a_cell";
			dataGridView1.Columns["A_mem"].HeaderText = "A_mem";
			dataGridView1.Columns["thickness_mem"].HeaderText = "thickness_mem";
			dataGridView1.Columns["porosity_mem"].HeaderText = "porosity_mem";
			dataGridView1.Columns["tortuosity_mem"].HeaderText = "tortuosity_mem";
			dataGridView1.Columns["wt_KOHsln"].HeaderText = "wt_KOHsln";
			dataGridView1.Columns["k"].HeaderText = "k";
			dataGridView1.Columns["D_h2"].HeaderText = "D_h2";
			dataGridView1.Columns["D_o2"].HeaderText = "D_o2";
			dataGridView1.Columns["k_x_h2"].HeaderText = "k_x_h2";
			dataGridView1.Columns["k_x_o2"].HeaderText = "k_x_o2";
			dataGridView1.Columns["eps_h2_Darcy"].HeaderText = "eps_h2_Darcy";
			dataGridView1.Columns["eps_o2_Darcy"].HeaderText = "eps_o2_Darcy";
			dataGridView1.Columns["tao_b"].HeaderText = "tao_b";
			dataGridView1.Columns["FC_flash"].HeaderText = "FC_flash";
			dataGridView1.Columns["R"].HeaderText = "R";
			dataGridView1.Columns["T"].HeaderText = "T";
			dataGridView1.Columns["eta"].HeaderText = "eta";
			dataGridView1.Columns["M_h2"].HeaderText = "M_h2";
			dataGridView1.Columns["M_o2"].HeaderText = "M_o2";
			dataGridView1.Columns["M_koh"].HeaderText = "M_koh";
			dataGridView1.Columns["rho_h2o"].HeaderText = "rho_h2o";
			dataGridView1.Columns["rho_h2o"].HeaderText = "rho_h2o";
			dataGridView1.Columns["rho_h2"].HeaderText = "rho_h2";
			dataGridView1.Columns["rho_o2"].HeaderText = "rho_o2";
			dataGridView1.Columns["rho_sln_koh"].HeaderText = "rho_sln_koh";
			dataGridView1.Columns["g"].HeaderText = "g";
			dataGridView1.Columns["Re24_0"].HeaderText = "Re24_0";
			dataGridView1.Columns["mu"].HeaderText = "mu";
			dataGridView1.Columns["Area_hx"].HeaderText = "Area_hx";
			dataGridView1.Columns["massFlowRate_cw"].HeaderText = "massFlowRate_cw";
			dataGridView1.Columns["cv1"].HeaderText = "cv1";
			dataGridView1.Columns["cv2"].HeaderText = "cv2";
			dataGridView1.Columns["P_cathode_sep_out"].HeaderText = "P_cathode_sep_out";
			dataGridView1.Columns["P_anode_sep_out"].HeaderText = "P_anode_sep_out";
			dataGridView1.Columns["P_env"].HeaderText = "P_env";

			//添加行数据
			DataRow row = dataTable01.NewRow();
			row["sigma_e_1"] = Data.sigma_e_1;
			row["sigma_h2_r1"] = Data.sigma_h2_r1;
			row["sigma_h2o_r1"] = Data.sigma_h2o_r1;
			row["sigma_e_2"] = Data.sigma_e_2;
			row["sigma_h2o_r2"] = Data.sigma_h2o_r2;
			row["sigma_o2_r2"] = Data.sigma_o2_r2;
			row["eta_F"] = Data.eta_F;
			row["F"] = Data.F;
			row["n_cell"] = Data.n_cell;
			row["a_cell"] = Data.a_cell;
			row["A_mem"] = Data.A_mem;
			row["thickness_mem"] = Data.thickness_mem;
			row["porosity_mem"] = Data.porosity_mem;
			row["tortuosity_mem"] = Data.tortuosity_mem;
			row["wt_KOHsln"] = Data.wt_KOHsln;
			row["k"] = Data.k;
			row["D_h2"] = Data.D_h2;
			row["D_o2"] = Data.D_o2;
			row["k_x_h2"] = Data.k_x_h2;
			row["k_x_o2"] = Data.k_x_o2;
			row["eps_h2_Darcy"] = Data.eps_h2_Darcy;
			row["eps_o2_Darcy"] = Data.eps_o2_Darcy;
			row["tao_b"] = Data.tao_b;
			row["FC_flash"] = Data.FC_flash;
			row["R"] = Data.R;
			row["T"] = Data.T;
			row["eta"] = Data.eta;
			row["M_h2"] = Data.M_h2;
			row["M_o2"] = Data.M_o2;
			row["M_koh"] = Data.M_koh;
			row["M_h2o"] = Data.M_h2o;
			row["rho_h2o"] = Data.rho_h2o;
			row["rho_h2"] = Data.rho_h2;
			row["rho_o2"] = Data.rho_o2;
			row["rho_sln_koh"] = Data.rho_sln_koh;
			row["g"] = Data.g;
			row["Re24_0"] = Data.Re24_0;
			row["mu"] = Data.mu;
			row["Area_hx"] = Data.Area_hx;
			row["massFlowRate_cw"] = Data.massFlowRate_cw;
			row["cv1"] = Data.cv1;
			row["cv2"] = Data.cv2;
			row["P_cathode_sep_out"] = Data.P_cathode_sep_out;
			row["P_anode_sep_out"] = Data.P_anode_sep_out;
			row["P_env"] = Data.P_env;

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
				dataGridView1.Rows.Add(i++, "sigma_e_1", Data.sigma_e_1, "-", "阴极电子反应系数");
				dataGridView1.Rows.Add(i++, "sigma_h2_r1", Data.sigma_h2_r1, "-", "阴极导电系数");
				dataGridView1.Rows.Add(i++, "sigma_h2o_r1", Data.sigma_h2o_r1, "-", "阴极导电系数");
				dataGridView1.Rows.Add(i++, "sigma_e_2", Data.sigma_e_2, "-", "阳极导电系数");
				dataGridView1.Rows.Add(i++, "sigma_h2o_r2", Data.sigma_h2o_r2, "-", "阳极导电系数");
				dataGridView1.Rows.Add(i++, "sigma_o2_r2", Data.sigma_o2_r2, "-", "阳极电子反应系数");
				dataGridView1.Rows.Add(i++, "eta_F", Data.eta_F, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "F", Data.F, "C·mol⁻¹", "电化学系数");
				dataGridView1.Rows.Add(i++, "n_cell", Data.n_cell, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "a_cell", Data.a_cell, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "A_mem", Data.A_mem, "m²", "膜面积");
				dataGridView1.Rows.Add(i++, "thickness_mem", Data.thickness_mem, "m", "膜厚度");
				dataGridView1.Rows.Add(i++, "porosity_mem", Data.porosity_mem, "-", "膜空隙率");
				dataGridView1.Rows.Add(i++, "tortuosity_mem", Data.tortuosity_mem, "-", "膜弯曲度");
				dataGridView1.Rows.Add(i++, "wt_KOHsln", Data.wt_KOHsln, "", "");
				dataGridView1.Rows.Add(i++, "k", Data.k, "", "");
				dataGridView1.Rows.Add(i++, "D_h2", Data.D_h2, "m²·s⁻¹", "氢气浓度计算参数");
				dataGridView1.Rows.Add(i++, "D_o2", Data.D_o2, "m²·s⁻¹", "氧气浓度计算参数");
				dataGridView1.Rows.Add(i++, "k_x_h2", Data.k_x_h2, "-", "氢气浓度计算参数");
				dataGridView1.Rows.Add(i++, "k_x_o2", Data.k_x_o2, "-", "氧气浓度计算参数");
				dataGridView1.Rows.Add(i++, "eps_h2_Darcy", Data.eps_h2_Darcy, "-", "氢气浓度计算参数");
				dataGridView1.Rows.Add(i++, "eps_o2_Darcy", Data.eps_o2_Darcy, "-", "氧气浓度计算参数");
				dataGridView1.Rows.Add(i++, "tao_b", Data.tao_b, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "FC_flash", Data.FC_flash, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "R", Data.R, "J·mol⁻¹·K⁻¹", "气体方程常数");
				dataGridView1.Rows.Add(i++, "T", Data.T, "K", "温度");
				dataGridView1.Rows.Add(i++, "eta", Data.eta, "-", "电化学系数");
				dataGridView1.Rows.Add(i++, "M_h2", Data.M_h2, "kg·mol⁻¹", "氢气摩尔量");
				dataGridView1.Rows.Add(i++, "M_o2", Data.M_o2, "kg·mol⁻¹", "氧气摩尔量");
				dataGridView1.Rows.Add(i++, "M_koh", Data.M_koh, "kg·mol⁻¹", "KOH溶液摩尔量");
				dataGridView1.Rows.Add(i++, "M_h2o", Data.M_h2o, "kg·mol⁻¹", "水摩尔量");
				dataGridView1.Rows.Add(i++, "rho_h2o", Data.rho_h2o, "kg·m³", "水密度系数");
				dataGridView1.Rows.Add(i++, "rho_h2", Data.rho_h2, "kg·m³", "氢气密度系数");
				dataGridView1.Rows.Add(i++, "rho_o2", Data.rho_o2, "kg·m³", "氧气密度系数");
				dataGridView1.Rows.Add(i++, "rho_sln_koh", Data.rho_sln_koh, "kg·m³", "KOH溶液密度系数");
				dataGridView1.Rows.Add(i++, "g", Data.g, "m·s⁻²", "重力系数");
				dataGridView1.Rows.Add(i++, "Re24_0", Data.Re24_0, "-", "雷诺数");
				dataGridView1.Rows.Add(i++, "mu", Data.mu, "Pa·s", "粘度系数");
				dataGridView1.Rows.Add(i++, "Area_hx", Data.Area_hx, "m²", "换热器换热面积");
				dataGridView1.Rows.Add(i++, "massFlowRate_cw", Data.massFlowRate_cw, "kg·s⁻¹", "冷却水给水流量");
				dataGridView1.Rows.Add(i++, "cv1", Data.cv1, "", "");
				dataGridView1.Rows.Add(i++, "cv2", Data.cv2, "", "");
				dataGridView1.Rows.Add(i++, "P_cathode_sep_out", Data.P_cathode_sep_out, "Pa", "阴极分离器出口压力");
				dataGridView1.Rows.Add(i++, "P_anode_sep_out", Data.P_anode_sep_out, "Pa", "阳极分离器出口压力");
				dataGridView1.Rows.Add(i++, "P_env", Data.P_env, "Pa", "环境压力");
			}
			else
			{
				int i = 0;
				dataGridView1[2, i++].Value = Data.sigma_e_1;
				dataGridView1[2, i++].Value = Data.sigma_h2_r1;
				dataGridView1[2, i++].Value = Data.sigma_h2o_r1;
				dataGridView1[2, i++].Value = Data.sigma_e_2;
				dataGridView1[2, i++].Value = Data.sigma_h2o_r2;
				dataGridView1[2, i++].Value = Data.sigma_o2_r2;
				dataGridView1[2, i++].Value = Data.eta_F;
				dataGridView1[2, i++].Value = Data.F;
				dataGridView1[2, i++].Value = Data.n_cell;
				dataGridView1[2, i++].Value = Data.a_cell;
				dataGridView1[2, i++].Value = Data.A_mem;
				dataGridView1[2, i++].Value = Data.thickness_mem;
				dataGridView1[2, i++].Value = Data.porosity_mem;
				dataGridView1[2, i++].Value = Data.tortuosity_mem;
				dataGridView1[2, i++].Value = Data.wt_KOHsln;
				dataGridView1[2, i++].Value = Data.k;
				dataGridView1[2, i++].Value = Data.D_h2;
				dataGridView1[2, i++].Value = Data.D_o2;
				dataGridView1[2, i++].Value = Data.k_x_h2;
				dataGridView1[2, i++].Value = Data.k_x_o2;
				dataGridView1[2, i++].Value = Data.eps_h2_Darcy;
				dataGridView1[2, i++].Value = Data.eps_o2_Darcy;
				dataGridView1[2, i++].Value = Data.tao_b;
				dataGridView1[2, i++].Value = Data.FC_flash;
				dataGridView1[2, i++].Value = Data.R;
				dataGridView1[2, i++].Value = Data.T;
				dataGridView1[2, i++].Value = Data.eta;
				dataGridView1[2, i++].Value = Data.M_h2;
				dataGridView1[2, i++].Value = Data.M_o2;
				dataGridView1[2, i++].Value = Data.M_koh;
				dataGridView1[2, i++].Value = Data.M_h2o;
				dataGridView1[2, i++].Value = Data.rho_h2o;
				dataGridView1[2, i++].Value = Data.rho_h2;
				dataGridView1[2, i++].Value = Data.rho_o2;
				dataGridView1[2, i++].Value = Data.rho_sln_koh;
				dataGridView1[2, i++].Value = Data.g;
				dataGridView1[2, i++].Value = Data.Re24_0;
				dataGridView1[2, i++].Value = Data.mu;
				dataGridView1[2, i++].Value = Data.Area_hx;
				dataGridView1[2, i++].Value = Data.massFlowRate_cw;
				dataGridView1[2, i++].Value = Data.cv1;
				dataGridView1[2, i++].Value = Data.cv2;
				dataGridView1[2, i++].Value = Data.P_cathode_sep_out;
				dataGridView1[2, i++].Value = Data.P_anode_sep_out;
				dataGridView1[2, i++].Value = Data.P_env;
			}
		}

		private void dataGridView1_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			dataGridView1.AllowUserToAddRows = false;
			int i = 0;
			Data.sigma_e_1 = (double)dataGridView1[2, i++].Value;
			Data.sigma_h2_r1 = (double)dataGridView1[2, i++].Value;
			Data.sigma_h2o_r1 = (double)dataGridView1[2, i++].Value;
			Data.sigma_e_2 = (double)dataGridView1[2, i++].Value;
			Data.sigma_h2o_r2 = (double)dataGridView1[2, i++].Value;
			Data.sigma_o2_r2 = (double)dataGridView1[2, i++].Value;
			Data.eta_F = (double)dataGridView1[2, i++].Value;
			Data.F = (double)dataGridView1[2, i++].Value;
			Data.n_cell = (double)dataGridView1[2, i++].Value;
			Data.a_cell = (double)dataGridView1[2, i++].Value;
			Data.A_mem = (double)dataGridView1[2, i++].Value;
			Data.thickness_mem = (double)dataGridView1[2, i++].Value;
			Data.porosity_mem = (double)dataGridView1[2, i++].Value;
			Data.tortuosity_mem = (double)dataGridView1[2, i++].Value;
			Data.wt_KOHsln = (double)dataGridView1[2, i++].Value;
			Data.k = (double)dataGridView1[2, i++].Value;
			Data.D_h2 = (double)dataGridView1[2, i++].Value;
			Data.D_o2 = (double)dataGridView1[2, i++].Value;
			Data.k_x_h2 = (double)dataGridView1[2, i++].Value;
			Data.k_x_o2 = (double)dataGridView1[2, i++].Value;
			Data.eps_h2_Darcy = (double)dataGridView1[2, i++].Value;
			Data.eps_o2_Darcy = (double)dataGridView1[2, i++].Value;
			Data.tao_b = (double)dataGridView1[2, i++].Value;
			Data.FC_flash = (double)dataGridView1[2, i++].Value;
			Data.R = (double)dataGridView1[2, i++].Value;
			Data.T = (double)dataGridView1[2, i++].Value;
			Data.eta = (double)dataGridView1[2, i++].Value;
			Data.M_h2 = (double)dataGridView1[2, i++].Value;
			Data.M_o2 = (double)dataGridView1[2, i++].Value;
			Data.M_koh = (double)dataGridView1[2, i++].Value;
			Data.M_h2o = (double)dataGridView1[2, i++].Value;
			Data.rho_h2o = (double)dataGridView1[2, i++].Value;
			Data.rho_h2 = (double)dataGridView1[2, i++].Value;
			Data.rho_o2 = (double)dataGridView1[2, i++].Value;
			Data.rho_sln_koh = (double)dataGridView1[2, i++].Value;
			Data.g = (double)dataGridView1[2, i++].Value;
			Data.Re24_0 = (double)dataGridView1[2, i++].Value;
			Data.mu = (double)dataGridView1[2, i++].Value;
			Data.Area_hx = (double)dataGridView1[2, i++].Value;
			Data.massFlowRate_cw = (double)dataGridView1[2, i++].Value;
			Data.cv1 = (double)dataGridView1[2, i++].Value;
			Data.cv2 = (double)dataGridView1[2, i++].Value;
			Data.P_cathode_sep_out = (double)dataGridView1[2, i++].Value;
			Data.P_anode_sep_out = (double)dataGridView1[2, i++].Value;
			Data.P_env = (double)dataGridView1[2, i++].Value;

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
