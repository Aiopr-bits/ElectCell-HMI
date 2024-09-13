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

namespace SimulationDesignPlatform.UserControls
{
    public partial class UserControl2 : UserControl
    {
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度

		public UserControl2()
        {
            InitializeComponent();
            GetDatabase01();
            GetDatabase02();
            GetDatabase03();
            GetDatabase04();
            GetDatabase05();

            // 双缓冲
            DoubleBufferedControl(dataGridView1);
            DoubleBufferedControl(dataGridView2);
            DoubleBufferedControl(dataGridView3);
            DoubleBufferedControl(dataGridView4);
            DoubleBufferedControl(dataGridView5);

			#region  初始化控件缩放
			x = Width;
			y = Height;
			setTag(this);
			#endregion
		}
        private void DoubleBufferedControl(Control control)
        {
			Type conType = control.GetType();
			PropertyInfo pi = conType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(control, true, null);
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
		private void GetDatabase01()
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
            dataTable01.Columns.Add("use_ff_static", typeof(bool));
            dataTable01.Columns.Add("IsMixed_circleType", typeof(bool));
            dataTable01.Columns.Add("cal_superSat_fickTrans", typeof(bool));
            dataTable01.Columns.Add("ec_pump_independent", typeof(bool));

            // 设置DataGridView的DataSource  
            dataGridView1.DataSource = dataTable01;

            // 设置列名  
            dataGridView1.Columns["start_time"].HeaderText = "start_time";
            dataGridView1.Columns["end_time"].HeaderText = "end_time";
            dataGridView1.Columns["delta_t"].HeaderText = "delta_t";
            dataGridView1.Columns["cal_current"].HeaderText = "cal_current";
            dataGridView1.Columns["cal_valve"].HeaderText = "cal_valve";
            dataGridView1.Columns["cal_pump"].HeaderText = "cal_pump";
            dataGridView1.Columns["cal_balance_pipe"].HeaderText = "cal_balance_pipe";
            dataGridView1.Columns["use_ff_static"].HeaderText = "use_ff_static";
            dataGridView1.Columns["IsMixed_circleType"].HeaderText = "IsMixed_circleType";
            dataGridView1.Columns["cal_superSat_fickTrans"].HeaderText = "cal_superSat_fickTrans";
            dataGridView1.Columns["ec_pump_independent"].HeaderText = "ec_pump_independent";
           
            //添加行数据
            DataRow row = dataTable01.NewRow();
            row["start_time"] = Data.start_time; ;
            row["end_time"] = Data.end_time;
            row["delta_t"] = Data.delta_t;
            row["cal_current"] = Data.cal_current;
            row["cal_valve"] = Data.cal_valve;
            row["cal_pump"] = Data.cal_pump;
            row["cal_balance_pipe"] = Data.cal_balance_pipe;
            row["use_ff_static"] = Data.use_ff_static;
            row["IsMixed_circleType"] = Data.IsMixed_circleType;
            row["cal_superSat_fickTrans"] = Data.cal_superSat_fickTrans;

            dataTable01.Rows.Add(row);
            
        }

        private void GetDatabase02()
        {
            DataTable dataTable01 = new DataTable();

            dataTable01.Columns.Add("L_ca2se", typeof(double));
            dataTable01.Columns.Add("L_an2se", typeof(double));
            dataTable01.Columns.Add("D_sc", typeof(double));
            dataTable01.Columns.Add("l_sc", typeof(double));
            
            // 设置DataGridView的DataSource  
            dataGridView2.DataSource = dataTable01;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置列名  
            dataGridView2.Columns["L_ca2se"].HeaderText = "L_ca2se";
            dataGridView2.Columns["L_an2se"].HeaderText = "L_an2se";
            dataGridView2.Columns["D_sc"].HeaderText = "D_sc";
            dataGridView2.Columns["l_sc"].HeaderText = "l_sc";

            //添加行数据
            DataRow row = dataTable01.NewRow();
            row["L_ca2se"] = Data.L_ca2se; ;
            row["L_an2se"] = Data.L_an2se;
            row["D_sc"] = Data.D_sc;
            row["l_sc"] = Data.l_sc;

            dataTable01.Rows.Add(row);
        }

        private void GetDatabase03()
        {
            DataTable dataTable01 = new DataTable();

            dataTable01.Columns.Add("num", typeof(int));
            dataTable01.Columns.Add("x_h2", typeof(double));
            dataTable01.Columns.Add("x_o2", typeof(double));
            dataTable01.Columns.Add("x_h2o", typeof(double));
            dataTable01.Columns.Add("Di", typeof(double));
            dataTable01.Columns.Add("L", typeof(double));
            dataTable01.Columns.Add("v_t", typeof(double));

            // 设置DataGridView的DataSource  
            dataGridView3.DataSource = dataTable01;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置列名  
            dataGridView3.Columns["num"].HeaderText = "num";
            dataGridView3.Columns["x_h2"].HeaderText = "x_h2";
            dataGridView3.Columns["x_o2"].HeaderText = "x_o2";
            dataGridView3.Columns["x_h2o"].HeaderText = "x_h2o";
            dataGridView3.Columns["Di"].HeaderText = "Di";
            dataGridView3.Columns["L"].HeaderText = "L";
            dataGridView3.Columns["v_t"].HeaderText = "v_t";

            for (int i = 0; i < Data.n_flow; i++)
            {
                //添加行数据
                DataRow row = dataTable01.NewRow();
                row["num"] = Data.flow[i].num;
                row["x_h2"] = Data.flow[i].x_h2;
                row["x_o2"] = Data.flow[i].x_o2;
                row["x_h2o"] = Data.flow[i].x_h2o;
                row["Di"] = Data.flow[i].Di;
                row["L"] = Data.flow[i].L;

                dataTable01.Rows.Add(row);
            }
        }

        private void GetDatabase04()
        {
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
                row["num"] = Data.ps[i].num;
                row["n"] = Data.ps[i].n;
                row["v"] = Data.ps[i].v;
                row["p"] = Data.ps[i].p;
                row["l_l"] = Data.ps[i].l_l;
                row["l_g"] = Data.ps[i].l_g;
                row["x_h2"] = Data.ps[i].x_h2;
                row["x_o2"] = Data.ps[i].x_o2;
                row["x_h2o"] = Data.ps[i].x_h2o;

                dataTable01.Rows.Add(row);
            }
        }

        private void GetDatabase05()
        {
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
            dataGridView5.DataSource = dataTable01;

            // 设置列名  
            dataGridView5.Columns["sigma_e_1"].HeaderText = "sigma_e_1";
            dataGridView5.Columns["sigma_h2_r1"].HeaderText = "sigma_h2_r1";
            dataGridView5.Columns["sigma_h2o_r1"].HeaderText = "sigma_h2o_r1";
            dataGridView5.Columns["sigma_e_2"].HeaderText = "sigma_e_2";
            dataGridView5.Columns["sigma_h2o_r2"].HeaderText = "sigma_h2o_r2";
            dataGridView5.Columns["sigma_o2_r2"].HeaderText = "sigma_o2_r2";
            dataGridView5.Columns["eta_F"].HeaderText = "eta_F";
            dataGridView5.Columns["F"].HeaderText = "F";
            dataGridView5.Columns["n_cell"].HeaderText = "n_cell";
            dataGridView5.Columns["a_cell"].HeaderText = "a_cell";
            dataGridView5.Columns["A_mem"].HeaderText = "A_mem";
            dataGridView5.Columns["thickness_mem"].HeaderText = "thickness_mem";
            dataGridView5.Columns["porosity_mem"].HeaderText = "porosity_mem";
            dataGridView5.Columns["tortuosity_mem"].HeaderText = "tortuosity_mem";
            dataGridView5.Columns["wt_KOHsln"].HeaderText = "wt_KOHsln";
            dataGridView5.Columns["k"].HeaderText = "k";
            dataGridView5.Columns["D_h2"].HeaderText = "D_h2";
            dataGridView5.Columns["D_o2"].HeaderText = "D_o2";
            dataGridView5.Columns["k_x_h2"].HeaderText = "k_x_h2";
            dataGridView5.Columns["k_x_o2"].HeaderText = "k_x_o2";
            dataGridView5.Columns["eps_h2_Darcy"].HeaderText = "eps_h2_Darcy";
            dataGridView5.Columns["eps_o2_Darcy"].HeaderText = "eps_o2_Darcy";
            dataGridView5.Columns["tao_b"].HeaderText = "tao_b";
            dataGridView5.Columns["FC_flash"].HeaderText = "FC_flash";
            dataGridView5.Columns["R"].HeaderText = "R";
            dataGridView5.Columns["T"].HeaderText = "T";
            dataGridView5.Columns["eta"].HeaderText = "eta";
            dataGridView5.Columns["M_h2"].HeaderText = "M_h2";
            dataGridView5.Columns["M_o2"].HeaderText = "M_o2";
            dataGridView5.Columns["M_koh"].HeaderText = "M_koh";
            dataGridView5.Columns["rho_h2o"].HeaderText = "rho_h2o";
            dataGridView5.Columns["rho_h2o"].HeaderText = "rho_h2o";
            dataGridView5.Columns["rho_h2"].HeaderText = "rho_h2";
            dataGridView5.Columns["rho_o2"].HeaderText = "rho_o2";
            dataGridView5.Columns["rho_sln_koh"].HeaderText = "rho_sln_koh";
            dataGridView5.Columns["g"].HeaderText = "g";
            dataGridView5.Columns["Re24_0"].HeaderText = "Re24_0";
            dataGridView5.Columns["mu"].HeaderText = "mu";
            dataGridView5.Columns["Area_hx"].HeaderText = "Area_hx";
            dataGridView5.Columns["massFlowRate_cw"].HeaderText = "massFlowRate_cw";
            dataGridView5.Columns["cv1"].HeaderText = "cv1";
            dataGridView5.Columns["cv2"].HeaderText = "cv2";
            dataGridView5.Columns["P_cathode_sep_out"].HeaderText = "P_cathode_sep_out";
            dataGridView5.Columns["P_anode_sep_out"].HeaderText = "P_anode_sep_out";
            dataGridView5.Columns["P_env"].HeaderText = "P_env";

            //添加行数据
            DataRow row = dataTable01.NewRow();
            row["sigma_e_1"] = Data.sigma_e_1; ;
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

		private void UserControl2_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}
	}
}
