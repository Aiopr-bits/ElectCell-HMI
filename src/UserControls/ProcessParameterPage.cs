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
        // 分类参数名
        private Dictionary<string, List<string>> parameterCategories = new Dictionary<string, List<string>>
        {
            { "电解槽参数", new List<string> { "n_cell", "a_cell", "A_mem", "thickness_mem", "porosity_mem", "tortuosity_mem", "T_K", "T_k0", "T_elin0" } },
            { "分离器参数", new List<string> { "P_cathode_sep_out", "P_anode_sep_out", "T_btout", "T_btout0", "T_btout_ano0", "T_btout_cat0" } },
            { "阀门参数", new List<string> { "cv1", "cv2" } },
            { "电化学参数", new List<string> { "eta_F", "eta", "F", "k_x_h2", "k_x_o2", "tao_b", "k", "FC_flash" } },
            { "物性参数", new List<string> { "M_h2", "M_o2", "M_n2", "M_koh", "M_h2o", "rho_h2o", "rho_h2", "rho_o2", "rho_sln_koh", "mu", "g", "R" } },
            { "导电参数", new List<string> { "sigma_e_1", "sigma_h2_r1", "sigma_h2o_r1", "sigma_e_2", "sigma_h2o_r2", "sigma_o2_r2" } },
            { "扩散参数", new List<string> { "D_h2", "D_o2" } },
            { "其他参数", new List<string> { "wt_KOHsln", "eps_h2_Darcy", "eps_o2_Darcy", "Re7_0", "P_env", "T_cw_in", "T_cw_out0", "T_ambi", "T_pipeout0" } }
        };

        private Dictionary<string, DataGridView> categoryDataGrids = new Dictionary<string, DataGridView>();

        public ProcessParameterPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
            InitTreeView();
            InitCategoryDataGrids();
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
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
            AddProcessParameter(dt, 1, "sigma_e_1", Data.processParameter.sigma_e_1, "S/m", "阴极电子导电系数");
            AddProcessParameter(dt, 2, "sigma_h2_r1", Data.processParameter.sigma_h2_r1, "S/m", "氢气导电系数");
            AddProcessParameter(dt, 3, "sigma_h2o_r1", Data.processParameter.sigma_h2o_r1, "S/m", "水导电系数");
            AddProcessParameter(dt, 4, "sigma_e_2", Data.processParameter.sigma_e_2, "S/m", "阳极电子导电系数");
            AddProcessParameter(dt, 5, "sigma_h2o_r2", Data.processParameter.sigma_h2o_r2, "S/m", "水导电系数");
            AddProcessParameter(dt, 6, "sigma_o2_r2", Data.processParameter.sigma_o2_r2, "S/m", "氧气导电系数");
            AddProcessParameter(dt, 7, "eta_F", Data.processParameter.eta_F, "-", "电化学效率");
            AddProcessParameter(dt, 8, "F", Data.processParameter.F, "C/mol", "法拉第常数");
            AddProcessParameter(dt, 9, "n_cell", Data.processParameter.n_cell, "-", "电池数量");
            AddProcessParameter(dt, 10, "a_cell", Data.processParameter.a_cell, "m²", "电池面积");
            AddProcessParameter(dt, 11, "A_mem", Data.processParameter.A_mem, "m²", "膜面积");
            AddProcessParameter(dt, 12, "thickness_mem", Data.processParameter.thickness_mem, "m", "膜厚度");
            AddProcessParameter(dt, 13, "porosity_mem", Data.processParameter.porosity_mem, "-", "膜孔隙率");
            AddProcessParameter(dt, 14, "tortuosity_mem", Data.processParameter.tortuosity_mem, "-", "膜弯曲度");
            AddProcessParameter(dt, 15, "wt_KOHsln", Data.processParameter.wt_KOHsln, "-", "KOH溶液的质量分数");
            AddProcessParameter(dt, 16, "k", Data.processParameter.k, "-", "压差渗透计算系数");
            AddProcessParameter(dt, 17, "D_h2", Data.processParameter.D_h2, "m²/s", "氢气扩散系数");
            AddProcessParameter(dt, 18, "D_o2", Data.processParameter.D_o2, "m²/s", "氧气扩散系数");
            AddProcessParameter(dt, 19, "k_x_h2", Data.processParameter.k_x_h2, "-", "氢气电解反应速率常数");
            AddProcessParameter(dt, 20, "k_x_o2", Data.processParameter.k_x_o2, "-", "氧气电解反应速率常数");
            AddProcessParameter(dt, 21, "eps_h2_Darcy", Data.processParameter.eps_h2_Darcy, "-", "氢气达西渗透率");
            AddProcessParameter(dt, 22, "eps_o2_Darcy", Data.processParameter.eps_o2_Darcy, "-", "氧气达西渗透率");
            AddProcessParameter(dt, 23, "tao_b", Data.processParameter.tao_b, "-", "电化学时间常数");
            AddProcessParameter(dt, 24, "FC_flash", Data.processParameter.FC_flash, "-", "燃料电池闪点");
            AddProcessParameter(dt, 25, "R", Data.processParameter.R, "J/(mol·K)", "气体常数");
            AddProcessParameter(dt, 26, "eta", Data.processParameter.eta, "-", "电化学效率");
            AddProcessParameter(dt, 27, "M_h2", Data.processParameter.M_h2, "kg/mol", "氢气摩尔质量");
            AddProcessParameter(dt, 28, "M_o2", Data.processParameter.M_o2, "kg/mol", "氧气摩尔质量");
            AddProcessParameter(dt, 29, "M_n2", Data.processParameter.M_n2, "kg/mol", "氮气摩尔质量");
            AddProcessParameter(dt, 30, "M_koh", Data.processParameter.M_koh, "kg/mol", "KOH摩尔质量");
            AddProcessParameter(dt, 31, "M_h2o", Data.processParameter.M_h2o, "kg/mol", "水摩尔质量");
            AddProcessParameter(dt, 32, "rho_h2o", Data.processParameter.rho_h2o, "kg/m³", "水密度");
            AddProcessParameter(dt, 33, "rho_h2", Data.processParameter.rho_h2, "kg/m³", "氢气密度");
            AddProcessParameter(dt, 34, "rho_o2", Data.processParameter.rho_o2, "kg/m³", "氧气密度");
            AddProcessParameter(dt, 35, "rho_sln_koh", Data.processParameter.rho_sln_koh, "kg/m³", "KOH溶液密度");
            AddProcessParameter(dt, 36, "g", Data.processParameter.g, "m/s²", "重力加速度");
            AddProcessParameter(dt, 37, "Re7_0", Data.processParameter.Re7_0, "-", "雷诺数");
            AddProcessParameter(dt, 38, "mu", Data.processParameter.mu, "Pa·s", "动力粘度");
            AddProcessParameter(dt, 39, "cv1", Data.processParameter.cv1, "-", "阴极阀门开度");
            AddProcessParameter(dt, 40, "cv2", Data.processParameter.cv2, "-", "阳极阀门开度");
            AddProcessParameter(dt, 41, "P_cathode_sep_out", Data.processParameter.P_cathode_sep_out, "Pa", "阴极分离器出口压力");
            AddProcessParameter(dt, 42, "P_anode_sep_out", Data.processParameter.P_anode_sep_out, "Pa", "阳极分离器出口压力");
            AddProcessParameter(dt, 43, "P_env", Data.processParameter.P_env, "Pa", "环境压力");
            AddProcessParameter(dt, 44, "T_elin0", Data.processParameter.T_elin0, "K", "上一时刻电解槽入口碱液温度");
            AddProcessParameter(dt, 45, "T_k0", Data.processParameter.T_k0, "K", "上一时刻电解槽温度（液固）");
            AddProcessParameter(dt, 46, "T_K", Data.processParameter.T_K, "K", "电解槽温度（液固）");
            AddProcessParameter(dt, 47, "T_btout", Data.processParameter.T_btout, "K", "气液分离器出口温度");
            AddProcessParameter(dt, 48, "T_btout0", Data.processParameter.T_btout0, "K", "上一时刻气液分离器出口温度");
            AddProcessParameter(dt, 49, "T_cw_in", Data.processParameter.T_cw_in, "K", "换热器入口温度");
            AddProcessParameter(dt, 50, "T_cw_out0", Data.processParameter.T_cw_out0, "K", "上一时刻换热器出口温度");
            AddProcessParameter(dt, 51, "T_ambi", Data.processParameter.T_ambi, "K", "环境温度");
            AddProcessParameter(dt, 52, "T_pipeout0", Data.processParameter.T_pipeout0, "K", "上一时刻管道出口温度");
            AddProcessParameter(dt, 53, "T_btout_ano0", Data.processParameter.T_btout_ano0, "K", "上一时刻阳极分离器出口温度");
            AddProcessParameter(dt, 54, "T_btout_cat0", Data.processParameter.T_btout_cat0, "K", "上一时刻阴极分离器出口温度");

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

        private void InitTreeView()
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("工艺参数");
            foreach (var category in parameterCategories.Keys)
            {
                root.Nodes.Add(new TreeNode(category));
            }
            treeView1.Nodes.Add(root);
            treeView1.SelectedNode = root;
            root.Expand();
        }

        private void InitCategoryDataGrids()
        {
            categoryDataGrids.Clear();
            int dgvRow = tableLayoutPanel1.GetRow(dataGridView1);
            int dgvCol = tableLayoutPanel1.GetColumn(dataGridView1);
            foreach (var category in parameterCategories)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("序号", typeof(int));
                dt.Columns.Add("变量名", typeof(string));
                dt.Columns.Add("变量值", typeof(double));
                dt.Columns.Add("单位", typeof(string));
                dt.Columns.Add("含义", typeof(string));
                int index = 1;
                foreach (var param in category.Value)
                {
                    var row = FindParameterRow(param);
                    if (row != null)
                    {
                        // 用自己的编号
                        dt.Rows.Add(new object[] {
                            index++, // 序号
                            row[1],  // 变量名
                            row[2],  // 变量值
                            row[3],  // 单位
                            row[4]   // 含义
                        });
                    }
                }
                DataGridView dgv = new DataGridView();
                dgv.DataSource = dt;
                dgv.Dock = dataGridView1.Dock;
                dgv.Size = dataGridView1.Size;
                dgv.Location = dataGridView1.Location;
                dgv.Visible = false;
                categoryDataGrids[category.Key] = dgv;
                tableLayoutPanel1.Controls.Add(dgv, dgvCol, dgvRow);
            }
        }

        private object[] FindParameterRow(string paramName)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["变量名"].Value != null && row.Cells["变量名"].Value.ToString() == paramName)
                {
                    return new object[] {
                        row.Cells["序号"].Value,
                        row.Cells["变量名"].Value,
                        row.Cells["变量值"].Value,
                        row.Cells["单位"].Value,
                        row.Cells["含义"].Value
                    };
                }
            }
            return null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (var dgv in categoryDataGrids.Values)
            {
                dgv.Visible = false;
            }
            if (e.Node.Parent == null) // 根节点
            {
                dataGridView1.Visible = true;
                dataGridView1.BringToFront();
            }
            else if (categoryDataGrids.ContainsKey(e.Node.Text))
            {
                dataGridView1.Visible = false;
                var dgv = categoryDataGrids[e.Node.Text];
                dgv.Visible = true;
                dgv.BringToFront();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                dataGridView1.Visible = true;
                dataGridView1.BringToFront();
                foreach (var dgv in categoryDataGrids.Values)
                {
                    dgv.Visible = false;
                }
            }
        }
    }
}
