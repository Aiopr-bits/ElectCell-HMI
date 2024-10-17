using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI
{
    public partial class ProcessParameterPage : UserControl
    {
        public ProcessParameterPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
        }

        public void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(double));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            // 添加过程参数数据
            AddProcessParameter(dt, 1, "sigma_e_1", Data.processParameter.sigma_e_1, "-", "阴极电子反应系数");
            AddProcessParameter(dt, 2, "sigma_h2_r1", Data.processParameter.sigma_h2_r1, "-", "阴极导电系数");
            AddProcessParameter(dt, 3, "sigma_h2o_r1", Data.processParameter.sigma_h2o_r1, "-", "阴极导电系数");
            AddProcessParameter(dt, 4, "sigma_e_2", Data.processParameter.sigma_e_2, "-", "阳极导电系数");
            AddProcessParameter(dt, 5, "sigma_h2o_r2", Data.processParameter.sigma_h2o_r2, "-", "阳极导电系数");
            AddProcessParameter(dt, 6, "sigma_o2_r2", Data.processParameter.sigma_o2_r2, "-", "阳极电子反应系数");
            AddProcessParameter(dt, 7, "eta_F", Data.processParameter.eta_F, "-", "电化学系数");
            AddProcessParameter(dt, 8, "F", Data.processParameter.F, "C·mol⁻¹", "电化学系数");
            AddProcessParameter(dt, 9, "n_cell", Data.processParameter.n_cell, "-", "电化学系数");
            AddProcessParameter(dt, 10, "a_cell", Data.processParameter.a_cell, "-", "电化学系数");
            AddProcessParameter(dt, 11, "A_mem", Data.processParameter.A_mem, "m²", "膜面积");
            AddProcessParameter(dt, 12, "thickness_mem", Data.processParameter.thickness_mem, "m", "膜厚度");
            AddProcessParameter(dt, 13, "porosity_mem", Data.processParameter.porosity_mem, "-", "膜空隙率");
            AddProcessParameter(dt, 14, "tortuosity_mem", Data.processParameter.tortuosity_mem, "-", "膜弯曲度");
            AddProcessParameter(dt, 15, "wt_KOHsln", Data.processParameter.wt_KOHsln, "", "");
            AddProcessParameter(dt, 16, "k", Data.processParameter.k, "", "");
            AddProcessParameter(dt, 17, "D_h2", Data.processParameter.D_h2, "m²·s⁻¹", "氢气浓度计算参数");
            AddProcessParameter(dt, 18, "D_o2", Data.processParameter.D_o2, "m²·s⁻¹", "氧气浓度计算参数");
            AddProcessParameter(dt, 19, "k_x_h2", Data.processParameter.k_x_h2, "-", "氢气浓度计算参数");
            AddProcessParameter(dt, 20, "k_x_o2", Data.processParameter.k_x_o2, "-", "氧气浓度计算参数");
            AddProcessParameter(dt, 21, "eps_h2_Darcy", Data.processParameter.eps_h2_Darcy, "-", "氢气浓度计算参数");
            AddProcessParameter(dt, 22, "eps_o2_Darcy", Data.processParameter.eps_o2_Darcy, "-", "氧气浓度计算参数");
            AddProcessParameter(dt, 23, "tao_b", Data.processParameter.tao_b, "-", "电化学系数");
            AddProcessParameter(dt, 24, "FC_flash", Data.processParameter.FC_flash, "-", "电化学系数");
            AddProcessParameter(dt, 25, "R", Data.processParameter.R, "J·mol⁻¹·K⁻¹", "气体方程常数");
            AddProcessParameter(dt, 26, "eta", Data.processParameter.eta, "-", "电化学系数");
            AddProcessParameter(dt, 27, "M_h2", Data.processParameter.M_h2, "kg·mol⁻¹", "氢气摩尔量");
            AddProcessParameter(dt, 28, "M_o2", Data.processParameter.M_o2, "kg·mol⁻¹", "氧气摩尔量");
            AddProcessParameter(dt, 29, "M_n2", Data.processParameter.M_n2, "kg·mol⁻¹", "氮气摩尔量");
            AddProcessParameter(dt, 30, "M_koh", Data.processParameter.M_koh, "kg·mol⁻¹", "KOH溶液摩尔量");
            AddProcessParameter(dt, 31, "M_h2o", Data.processParameter.M_h2o, "kg·mol⁻¹", "水摩尔量");
            AddProcessParameter(dt, 32, "rho_h2o", Data.processParameter.rho_h2o, "kg·m³", "水密度系数");
            AddProcessParameter(dt, 33, "rho_h2", Data.processParameter.rho_h2, "kg·m³", "氢气密度系数");
            AddProcessParameter(dt, 34, "rho_o2", Data.processParameter.rho_o2, "kg·m³", "氧气密度系数");
            AddProcessParameter(dt, 35, "rho_sln_koh", Data.processParameter.rho_sln_koh, "kg·m³", "KOH溶液密度系数");
            AddProcessParameter(dt, 36, "g", Data.processParameter.g, "m·s⁻²", "重力系数");
            AddProcessParameter(dt, 37, "Re7_0", Data.processParameter.Re7_0, "-", "雷诺数");
            AddProcessParameter(dt, 38, "mu", Data.processParameter.mu, "Pa·s", "粘度系数");
            AddProcessParameter(dt, 39, "cv1", Data.processParameter.cv1, "", "");
            AddProcessParameter(dt, 40, "cv2", Data.processParameter.cv2, "", "");
            AddProcessParameter(dt, 41, "P_cathode_sep_out", Data.processParameter.P_cathode_sep_out, "Pa", "阴极分离器出口压力");
            AddProcessParameter(dt, 42, "P_anode_sep_out", Data.processParameter.P_anode_sep_out, "Pa", "阳极分离器出口压力");
            AddProcessParameter(dt, 43, "P_env", Data.processParameter.P_env, "Pa", "环境压力");
            AddProcessParameter(dt, 44, "T_elin0", Data.processParameter.T_elin0, "K", "参数描述44");
            AddProcessParameter(dt, 45, "T_k0", Data.processParameter.T_k0, "K", "参数描述45");
            AddProcessParameter(dt, 46, "T_K", Data.processParameter.T_K, "K", "参数描述46");
            AddProcessParameter(dt, 47, "T_btout", Data.processParameter.T_btout, "K", "参数描述47");
            AddProcessParameter(dt, 48, "T_btout0", Data.processParameter.T_btout0, "K", "参数描述48");
            AddProcessParameter(dt, 49, "T_cw_in", Data.processParameter.T_cw_in, "K", "参数描述49");
            AddProcessParameter(dt, 50, "T_cw_out0", Data.processParameter.T_cw_out0, "K", "参数描述50");
            AddProcessParameter(dt, 51, "T_ambi", Data.processParameter.T_ambi, "K", "参数描述51");
            AddProcessParameter(dt, 52, "T_pipeout0", Data.processParameter.T_pipeout0, "K", "参数描述52");
            AddProcessParameter(dt, 53, "T_btout_ano0", Data.processParameter.T_btout_ano0, "K", "参数描述53");
            AddProcessParameter(dt, 54, "T_btout_cat0", Data.processParameter.T_btout_cat0, "K", "参数描述54");

            dataGridView1.DataSource = dt;
        }

        public void AddProcessParameter(DataTable dt, int index, string name, double value, string unit, string description)
        {
            DataRow dr = dt.NewRow();
            dr["序号"] = index;
            dr["变量名"] = name;
            dr["变量值"] = value;
            dr["单位"] = unit;
            dr["含义"] = description;
            dt.Rows.Add(dr);
        }

        public void SaveData()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["变量名"].Value == null || string.IsNullOrWhiteSpace(row.Cells["变量名"].Value.ToString()) ||
                    row.Cells["变量值"].Value == null || string.IsNullOrWhiteSpace(row.Cells["变量值"].Value.ToString()))
                {
                    return;
                }

                string variableName = row.Cells["变量名"].Value.ToString();
                double variableValue = Convert.ToDouble(row.Cells["变量值"].Value);

                switch (variableName)
                {
                    case "sigma_e_1":
                        Data.processParameter.sigma_e_1 = variableValue;
                        break;
                    case "sigma_h2_r1":
                        Data.processParameter.sigma_h2_r1 = variableValue;
                        break;
                    case "sigma_h2o_r1":
                        Data.processParameter.sigma_h2o_r1 = variableValue;
                        break;
                    case "sigma_e_2":
                        Data.processParameter.sigma_e_2 = variableValue;
                        break;
                    case "sigma_h2o_r2":
                        Data.processParameter.sigma_h2o_r2 = variableValue;
                        break;
                    case "sigma_o2_r2":
                        Data.processParameter.sigma_o2_r2 = variableValue;
                        break;
                    case "eta_F":
                        Data.processParameter.eta_F = variableValue;
                        break;
                    case "F":
                        Data.processParameter.F = variableValue;
                        break;
                    case "n_cell":
                        Data.processParameter.n_cell = variableValue;
                        break;
                    case "a_cell":
                        Data.processParameter.a_cell = variableValue;
                        break;
                    case "A_mem":
                        Data.processParameter.A_mem = variableValue;
                        break;
                    case "thickness_mem":
                        Data.processParameter.thickness_mem = variableValue;
                        break;
                    case "porosity_mem":
                        Data.processParameter.porosity_mem = variableValue;
                        break;
                    case "tortuosity_mem":
                        Data.processParameter.tortuosity_mem = variableValue;
                        break;
                    case "wt_KOHsln":
                        Data.processParameter.wt_KOHsln = variableValue;
                        break;
                    case "k":
                        Data.processParameter.k = variableValue;
                        break;
                    case "D_h2":
                        Data.processParameter.D_h2 = variableValue;
                        break;
                    case "D_o2":
                        Data.processParameter.D_o2 = variableValue;
                        break;
                    case "k_x_h2":
                        Data.processParameter.k_x_h2 = variableValue;
                        break;
                    case "k_x_o2":
                        Data.processParameter.k_x_o2 = variableValue;
                        break;
                    case "eps_h2_Darcy":
                        Data.processParameter.eps_h2_Darcy = variableValue;
                        break;
                    case "eps_o2_Darcy":
                        Data.processParameter.eps_o2_Darcy = variableValue;
                        break;
                    case "tao_b":
                        Data.processParameter.tao_b = variableValue;
                        break;
                    case "FC_flash":
                        Data.processParameter.FC_flash = variableValue;
                        break;
                    case "R":
                        Data.processParameter.R = variableValue;
                        break;
                    case "eta":
                        Data.processParameter.eta = variableValue;
                        break;
                    case "M_h2":
                        Data.processParameter.M_h2 = variableValue;
                        break;
                    case "M_o2":
                        Data.processParameter.M_o2 = variableValue;
                        break;
                    case "M_n2":
                        Data.processParameter.M_n2 = variableValue;
                        break;
                    case "M_koh":
                        Data.processParameter.M_koh = variableValue;
                        break;
                    case "M_h2o":
                        Data.processParameter.M_h2o = variableValue;
                        break;
                    case "rho_h2o":
                        Data.processParameter.rho_h2o = variableValue;
                        break;
                    case "rho_h2":
                        Data.processParameter.rho_h2 = variableValue;
                        break;
                    case "rho_o2":
                        Data.processParameter.rho_o2 = variableValue;
                        break;
                    case "rho_sln_koh":
                        Data.processParameter.rho_sln_koh = variableValue;
                        break;
                    case "g":
                        Data.processParameter.g = variableValue;
                        break;
                    case "Re7_0":
                        Data.processParameter.Re7_0 = variableValue;
                        break;
                    case "mu":
                        Data.processParameter.mu = variableValue;
                        break;
                    case "cv1":
                        Data.processParameter.cv1 = variableValue;
                        break;
                    case "cv2":
                        Data.processParameter.cv2 = variableValue;
                        break;
                    case "P_cathode_sep_out":
                        Data.processParameter.P_cathode_sep_out = variableValue;
                        break;
                    case "P_anode_sep_out":
                        Data.processParameter.P_anode_sep_out = variableValue;
                        break;
                    case "P_env":
                        Data.processParameter.P_env = variableValue;
                        break;
                    case "T_elin0":
                        Data.processParameter.T_elin0 = variableValue;
                        break;
                    case "T_k0":
                        Data.processParameter.T_k0 = variableValue;
                        break;
                    case "T_K":
                        Data.processParameter.T_K = variableValue;
                        break;
                    case "T_btout":
                        Data.processParameter.T_btout = variableValue;
                        break;
                    case "T_btout0":
                        Data.processParameter.T_btout0 = variableValue;
                        break;
                    case "T_cw_in":
                        Data.processParameter.T_cw_in = variableValue;
                        break;
                    case "T_cw_out0":
                        Data.processParameter.T_cw_out0 = variableValue;
                        break;
                    case "T_ambi":
                        Data.processParameter.T_ambi = variableValue;
                        break;
                    case "T_pipeout0":
                        Data.processParameter.T_pipeout0 = variableValue;
                        break;
                    case "T_btout_ano0":
                        Data.processParameter.T_btout_ano0 = variableValue;
                        break;
                    case "T_btout_cat0":
                        Data.processParameter.T_btout_cat0 = variableValue;
                        break;
                }
            }
        }


    }
}
