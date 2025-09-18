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
    public partial class GeometricParameterPage : UserControl
    {
        // 分类参数名
        private Dictionary<string, List<string>> parameterCategories = new Dictionary<string, List<string>>
        {
            { "阴极参数", new List<string> { "L_ca2p", "L_an2se", "thickness_ano", "distance_cm" } },
            { "阳极参数", new List<string> { "L_an2p", "L_ca2se", "thickness_cat", "distance_am" } },
            { "隔膜参数", new List<string> { "Area_sep", "C_tsep" } },
            { "泵管道参数", new List<string> { "D_sc", "l_sc" } },
            { "体积参数", new List<string> { "Volume_hotside", "Volume_codeside" } },
            { "堆叠参数", new List<string> { "di_stack", "Area_stack", "C_tk" } }
        };

        // 分类参数显示表格
        private Dictionary<string, DataGridView> categoryDataGrids = new Dictionary<string, DataGridView>();

        public GeometricParameterPage()
        {
            InitializeComponent();
            InitTreeView();
            dataGridView1LoadData();
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick; // 新增
        }

        private void InitTreeView()
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("几何参数");
            foreach (var category in parameterCategories.Keys)
            {
                root.Nodes.Add(new TreeNode(category));
            }
            treeView1.Nodes.Add(root);
            treeView1.SelectedNode = root;
            root.Expand(); // 默认展开
        }

        public void dataGridView1LoadData()
        {
            DataTable dt = CreateParameterDataTable();
            // 添加几何参数数据
            AddGeometryParameter(dt, 1, "L_ca2p", Data.geometryParameter.L_ca2p, "m", "阴极到泵管道距离");
            AddGeometryParameter(dt, 2, "L_an2p", Data.geometryParameter.L_an2p, "m", "阳极到泵管道距离");
            AddGeometryParameter(dt, 3, "L_ca2se", Data.geometryParameter.L_ca2se, "m", "阳极到隔膜的距离");
            AddGeometryParameter(dt, 4, "L_an2se", Data.geometryParameter.L_an2se, "m", "阴极到隔膜的距离");
            AddGeometryParameter(dt, 5, "D_sc", Data.geometryParameter.D_sc, "m", "短路距离");
            AddGeometryParameter(dt, 6, "l_sc", Data.geometryParameter.l_sc, "m", "短路长度");
            AddGeometryParameter(dt, 7, "thickness_cat", Data.geometryParameter.thickness_cat, "m", "阳极厚度");
            AddGeometryParameter(dt, 8, "thickness_ano", Data.geometryParameter.thickness_ano, "m", "阴极厚度");
            AddGeometryParameter(dt, 9, "distance_am", Data.geometryParameter.distance_am, "m", "阳极到膜的距离");
            AddGeometryParameter(dt, 10, "distance_cm", Data.geometryParameter.distance_cm, "m", "阴极到膜的距离");
            AddGeometryParameter(dt, 11, "Volume_hotside", Data.geometryParameter.Volume_hotside, "m³", "热侧体积");
            AddGeometryParameter(dt, 12, "Volume_codeside", Data.geometryParameter.Volume_codeside, "m³", "冷侧体积");
            AddGeometryParameter(dt, 13, "di_stack", Data.geometryParameter.di_stack, "m", "堆叠直径");
            AddGeometryParameter(dt, 14, "Area_sep", Data.geometryParameter.Area_sep, "m²", "隔膜面积");
            AddGeometryParameter(dt, 15, "Area_stack", Data.geometryParameter.Area_stack, "m²", "堆叠面积");
            AddGeometryParameter(dt, 16, "C_tsep", Data.geometryParameter.C_tsep, "F", "隔膜电容");
            AddGeometryParameter(dt, 17, "C_tk", Data.geometryParameter.C_tk, "F", "堆叠电容");

            // 添加空行
            for (int i = 0; i < 24; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Visible = true;
            // 初始化各分类表格
            InitCategoryDataGrids();
        }

        private DataTable CreateParameterDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(double));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("含义", typeof(string));
            return dt;
        }

        private void InitCategoryDataGrids()
        {
            categoryDataGrids.Clear();
            // 获取dataGridView1在tableLayoutPanel1中的行和列
            int dgvRow = tableLayoutPanel1.GetRow(dataGridView1);
            int dgvCol = tableLayoutPanel1.GetColumn(dataGridView1);
            foreach (var category in parameterCategories)
            {
                DataTable dt = CreateParameterDataTable();
                int index = 1;
                foreach (var param in category.Value)
                {
                    switch (param)
                    {
                        case "L_ca2p":
                            AddGeometryParameter(dt, index++, "L_ca2p", Data.geometryParameter.L_ca2p, "m", "阴极到泵管道距离");
                            break;
                        case "L_ca2se":
                            AddGeometryParameter(dt, index++, "L_ca2se", Data.geometryParameter.L_ca2se, "m", "阳极到隔膜的距离");
                            break;
                        case "thickness_cat":
                            AddGeometryParameter(dt, index++, "thickness_cat", Data.geometryParameter.thickness_cat, "m", "阳极厚度");
                            break;
                        case "distance_cm":
                            AddGeometryParameter(dt, index++, "distance_cm", Data.geometryParameter.distance_cm, "m", "阴极到膜的距离");
                            break;
                        case "Area_stack":
                            AddGeometryParameter(dt, index++, "Area_stack", Data.geometryParameter.Area_stack, "m²", "堆叠面积");
                            break;
                        case "L_an2p":
                            AddGeometryParameter(dt, index++, "L_an2p", Data.geometryParameter.L_an2p, "m", "阳极到泵管道距离");
                            break;
                        case "L_an2se":
                            AddGeometryParameter(dt, index++, "L_an2se", Data.geometryParameter.L_an2se, "m", "阴极到隔膜的距离");
                            break;
                        case "thickness_ano":
                            AddGeometryParameter(dt, index++, "thickness_ano", Data.geometryParameter.thickness_ano, "m", "阴极厚度");
                            break;
                        case "distance_am":
                            AddGeometryParameter(dt, index++, "distance_am", Data.geometryParameter.distance_am, "m", "阳极到膜的距离");
                            break;
                        case "Area_sep":
                            AddGeometryParameter(dt, index++, "Area_sep", Data.geometryParameter.Area_sep, "m²", "隔膜面积");
                            break;
                        case "C_tsep":
                            AddGeometryParameter(dt, index++, "C_tsep", Data.geometryParameter.C_tsep, "F", "隔膜电容");
                            break;
                        case "D_sc":
                            AddGeometryParameter(dt, index++, "D_sc", Data.geometryParameter.D_sc, "m", "短路距离");
                            break;
                        case "l_sc":
                            AddGeometryParameter(dt, index++, "l_sc", Data.geometryParameter.l_sc, "m", "短路长度");
                            break;
                        case "Volume_hotside":
                            AddGeometryParameter(dt, index++, "Volume_hotside", Data.geometryParameter.Volume_hotside, "m³", "热侧体积");
                            break;
                        case "Volume_codeside":
                            AddGeometryParameter(dt, index++, "Volume_codeside", Data.geometryParameter.Volume_codeside, "m³", "冷侧体积");
                            break;
                        case "di_stack":
                            AddGeometryParameter(dt, index++, "di_stack", Data.geometryParameter.di_stack, "m", "堆叠直径");
                            break;
                        case "C_tk":
                            AddGeometryParameter(dt, index++, "C_tk", Data.geometryParameter.C_tk, "F", "堆叠电容");
                            break;
                    }
                }
                DataGridView dgv = new DataGridView();
                dgv.DataSource = dt;
                dgv.Dock = dataGridView1.Dock;
                dgv.Size = dataGridView1.Size;
                dgv.Location = dataGridView1.Location;
                dgv.Visible = false;
                categoryDataGrids[category.Key] = dgv;
                tableLayoutPanel1.Controls.Add(dgv, dgvCol, dgvRow); // 关键修改
            }
        }

        public void AddGeometryParameter(DataTable dt, int index, string name, double value, string unit, string description)
        {
            DataRow dr = dt.NewRow();
            dr["序号"] = index;
            dr["变量名"] = name;
            dr["变量值"] = value;
            dr["单位"] = unit;
            dr["含义"] = description;
            dt.Rows.Add(dr);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 隐藏所有分类表格
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
            // 只要点击根节点文本就显示主表格
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
                    case "L_ca2se":
                        Data.geometryParameter.L_ca2se = variableValue;
                        break;
                    case "L_an2se":
                        Data.geometryParameter.L_an2se = variableValue;
                        break;
                    case "D_sc":
                        Data.geometryParameter.D_sc = variableValue;
                        break;
                    case "l_sc":
                        Data.geometryParameter.l_sc = variableValue;
                        break;
                    case "thickness_cat":
                        Data.geometryParameter.thickness_cat = variableValue;
                        break;
                    case "thickness_ano":
                        Data.geometryParameter.thickness_ano = variableValue;
                        break;
                    case "distance_am":
                        Data.geometryParameter.distance_am = variableValue;
                        break;
                    case "distance_cm":
                        Data.geometryParameter.distance_cm = variableValue;
                        break;
                    case "Volume_hotside":
                        Data.geometryParameter.Volume_hotside = variableValue;
                        break;
                    case "Volume_codeside":
                        Data.geometryParameter.Volume_codeside = variableValue;
                        break;
                    case "di_stack":
                        Data.geometryParameter.di_stack = variableValue;
                        break;
                    case "Area_sep":
                        Data.geometryParameter.Area_sep = variableValue;
                        break;
                    case "Area_stack":
                        Data.geometryParameter.Area_stack = variableValue;
                        break;
                    case "C_tsep":
                        Data.geometryParameter.C_tsep = variableValue;
                        break;
                    case "C_tk":
                        Data.geometryParameter.C_tk = variableValue;
                        break;
                }
            }
        }
    }
}
