using ElectCell_HMI.Forms;
using ElectCell_HMI.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ElectCell_HMI
{
    public partial class ComponentParameterPage : UserControl
    {
        public bool _defaultSelectionApplied;
        private string _lastSelectedNodeText = null;

        public ComponentParameterPage()
        {
            InitializeComponent();
            initTreeview();

            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            numericUpDown5.ValueChanged += numericUpDown5_ValueChanged;
            numericUpDown6.ValueChanged += numericUpDown6_ValueChanged;
            numericUpDown7.ValueChanged += numericUpDown7_ValueChanged;
            numericUpDown8.ValueChanged += numericUpDown8_ValueChanged;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)numericUpDown1.Value;
            // flow: 每项6个参数，编号递增
            while (Data.flowParameter.flow.Count < count)
            {
                int index = Data.flowParameter.flow.Count + 1;
                Data.flowParameter.flow.Add(new List<double> { index, 0, 0, 0, 0, 0 });
            }
            while (Data.flowParameter.flow.Count > count)
            {
                Data.flowParameter.flow.RemoveAt(Data.flowParameter.flow.Count - 1);
            }
            if (_lastSelectedNodeText != null)
                UpdateUIFromData(_lastSelectedNodeText);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)numericUpDown2.Value;
            // ps: 每项8个参数，编号递增
            while (Data.psParameter.ps.Count < count)
            {
                int index = Data.psParameter.ps.Count + 1;
                Data.psParameter.ps.Add(new List<double> { index, 0, 0, 0, 0, 0, 0, 0 });
            }
            while (Data.psParameter.ps.Count > count)
            {
                Data.psParameter.ps.RemoveAt(Data.psParameter.ps.Count - 1);
            }
            if (_lastSelectedNodeText != null)
                UpdateUIFromData(_lastSelectedNodeText);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)numericUpDown3.Value;

            // 电解槽
            while (Data.componentParameter.electrolyticCell.Count < count)
            {
                var cell = new ElectrolyticCell
                {
                    current = 0,
                    flow = new List<double> { 1, 2, 3, 4, 5, 6 }, 
                    ps = new List<double> { 1, 2 } 
                };
                Data.componentParameter.electrolyticCell.Add(cell);
            }
            while (Data.componentParameter.electrolyticCell.Count > count)
            {
                Data.componentParameter.electrolyticCell.RemoveAt(Data.componentParameter.electrolyticCell.Count - 1);
            }

            // 泵
            while (Data.componentParameter.pump.Count < count)
            {
                var pump = new Pump
                {
                    flow = new List<double> { 1, 2 },
                    ps = new List<double> { 1 } 
                };
                Data.componentParameter.pump.Add(pump);
            }
            while (Data.componentParameter.pump.Count > count)
            {
                Data.componentParameter.pump.RemoveAt(Data.componentParameter.pump.Count - 1);
            }

            // 更新数量
            Data.componentParameter.nElectrolyticCell = count;

            // 重新初始化树
            initTreeview();

            // 刷新界面显示
            if (_lastSelectedNodeText != null)
                UpdateUIFromData(_lastSelectedNodeText);
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown5.Value == 0)
            {
                MessageBox.Show("阴极分离器数量不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown5.Value = 1;
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown6.Value == 0)
            {
                MessageBox.Show("阳极分离器数量不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown6.Value = 1;
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown7.Value == 0)
            {
                MessageBox.Show("阴极阀门数量不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown7.Value = 1;
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown8.Value == 0)
            {
                MessageBox.Show("阳极阀门数量不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown8.Value = 1;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            if (_defaultSelectionApplied) return;

            // 确保树已初始化
            if (treeView1.Nodes.Count == 0)
            {
                initTreeview();
            }

            // 优先选择第一个电解槽节点（电解槽1）
            TreeNode target = FindNodeByText(treeView1.Nodes, "电解槽1");
            if (target == null)
            {
                // 尝试选择“电解槽”的第一个子节点
                TreeNode dz = FindNodeByText(treeView1.Nodes, "电解槽");
                if (dz != null && dz.Nodes.Count > 0)
                {
                    target = dz.Nodes[0];
                }
            }
            // 如果仍未找到，则选择第一个叶子节点兜底
            if (target == null)
            {
                target = FindFirstLeafNode(treeView1.Nodes);
            }

            if (target != null)
            {
                treeView1.SelectedNode = target;
            }

            _defaultSelectionApplied = true;
        }

        public TreeNode FindFirstLeafNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes == null || node.Nodes.Count == 0)
                {
                    return node;
                }
                var inner = FindFirstLeafNode(node.Nodes);
                if (inner != null) return inner;
            }
            return null;
        }

        public TreeNode FindNodeByText(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (string.Equals(node.Text, text, StringComparison.Ordinal))
                {
                    return node;
                }
                var inner = FindNodeByText(node.Nodes, text);
                if (inner != null) return inner;
            }
            return null;
        }

        public void initTreeview()
        {
            treeView1.Nodes.Clear();

            // 创建根节点
            TreeNode rootNode = new TreeNode("部件");

            // 添加电解槽节点
            TreeNode electrolyticCellsNode = new TreeNode("电解槽");
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                electrolyticCellsNode.Nodes.Add(new TreeNode($"电解槽{i + 1}"));
            }
            rootNode.Nodes.Add(electrolyticCellsNode);

            // 添加泵节点
            TreeNode pumpsNode = new TreeNode("泵");
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                pumpsNode.Nodes.Add(new TreeNode($"泵{i + 1}"));
            }
            rootNode.Nodes.Add(pumpsNode);

            // 添加分离器节点
            TreeNode separatorsNode = new TreeNode("分离器");
            separatorsNode.Nodes.Add(new TreeNode("阴极分离器"));
            separatorsNode.Nodes.Add(new TreeNode("阳极分离器"));
            rootNode.Nodes.Add(separatorsNode);

            // 添加阀门节点
            TreeNode valvesNode = new TreeNode("阀门");
            valvesNode.Nodes.Add(new TreeNode("阴极阀门"));
            valvesNode.Nodes.Add(new TreeNode("阳极阀门"));
            rootNode.Nodes.Add(valvesNode);

            // 添加平衡管线节点
            rootNode.Nodes.Add(new TreeNode("平衡管线"));

            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();
        }

        public void PopulateAllFlowAndPs(DataTable dt4, DataTable dt5)
        {
            // 填充所有flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                DataRow dr = dt4.NewRow();
                dr["流股编号"] = Data.flowParameter.flow[j][0];
                dr["氢气占比"] = Data.flowParameter.flow[j][1];
                dr["氧气占比"] = Data.flowParameter.flow[j][2];
                dr["水占比"] = Data.flowParameter.flow[j][3];
                dr["直径（m）"] = Data.flowParameter.flow[j][4];
                dr["长度（m）"] = Data.flowParameter.flow[j][5];
                dt4.Rows.Add(dr);
            }

            // 填充所有ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                DataRow dr = dt5.NewRow();
                dr["过程系统编号"] = Data.psParameter.ps[j][0];
                dr["总物质量"] = Data.psParameter.ps[j][1];
                dr["摩尔体积（m³/mol）"] = Data.psParameter.ps[j][2];
                dr["压力"] = Data.psParameter.ps[j][3];
                dr["液体高度（m）"] = Data.psParameter.ps[j][4];
                dr["气体高度（m）"] = Data.psParameter.ps[j][5];
                dr["氢气占比"] = Data.psParameter.ps[j][6];
                dr["氧气占比"] = Data.psParameter.ps[j][7];
                dt5.Rows.Add(dr);
            }
        }

        public void HighlightFlowAndPsRows(ICollection<double> selectedFlows, ICollection<double> selectedPs)
        {
            if (selectedFlows == null) selectedFlows = new List<double>();
            if (selectedPs == null) selectedPs = new List<double>();

            Color flowDefault = dataGridView4.DefaultCellStyle.BackColor;
            Color psDefault = dataGridView5.DefaultCellStyle.BackColor;
            Color highlight = Color.LightGoldenrodYellow;

            // 高亮flow
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                row.DefaultCellStyle.BackColor = flowDefault;
                var val = row.Cells["流股编号"].Value;
                if (val == null || val == DBNull.Value) continue;
                double id;
                try { id = Convert.ToDouble(val); }
                catch { continue; }
                if (selectedFlows.Contains(id))
                {
                    row.DefaultCellStyle.BackColor = highlight;
                }
            }

            // 高亮ps
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                row.DefaultCellStyle.BackColor = psDefault;
                var val = row.Cells["过程系统编号"].Value;
                if (val == null || val == DBNull.Value) continue;
                double id;
                try { id = Convert.ToDouble(val); }
                catch { continue; }
                if (selectedPs.Contains(id))
                {
                    row.DefaultCellStyle.BackColor = highlight;
                }
            }
        }

        public void UpdateDataFromUI(string selectedNode)
        {
            // 回写 dataGridView4 到 Data.flowParameter.flow
            Data.flowParameter.flow.Clear();
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["流股编号"].Value == null ||
                    row.Cells["氢气占比"].Value == null ||
                    row.Cells["氧气占比"].Value == null ||
                    row.Cells["水占比"].Value == null ||
                    row.Cells["直径（m）"].Value == null ||
                    row.Cells["长度（m）"].Value == null)
                    continue;
                var list = new List<double>
                {
                    Convert.ToDouble(row.Cells["流股编号"].Value),
                    Convert.ToDouble(row.Cells["氢气占比"].Value),
                    Convert.ToDouble(row.Cells["氧气占比"].Value),
                    Convert.ToDouble(row.Cells["水占比"].Value),
                    Convert.ToDouble(row.Cells["直径（m）"].Value),
                    Convert.ToDouble(row.Cells["长度（m）"].Value)
                };
                Data.flowParameter.flow.Add(list);
            }

            // 回写 dataGridView5 到 Data.psParameter.ps
            Data.psParameter.ps.Clear();
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["过程系统编号"].Value == null ||
                    row.Cells["总物质量"].Value == null ||
                    row.Cells["摩尔体积（m³/mol）"].Value == null ||
                    row.Cells["压力"].Value == null ||
                    row.Cells["液体高度（m）"].Value == null ||
                    row.Cells["气体高度（m）"].Value == null ||
                    row.Cells["氢气占比"].Value == null ||
                    row.Cells["氧气占比"].Value == null)
                    continue;
                var list = new List<double>
                {
                    Convert.ToDouble(row.Cells["过程系统编号"].Value),
                    Convert.ToDouble(row.Cells["总物质量"].Value),
                    Convert.ToDouble(row.Cells["摩尔体积（m³/mol）"].Value),
                    Convert.ToDouble(row.Cells["压力"].Value),
                    Convert.ToDouble(row.Cells["液体高度（m）"].Value),
                    Convert.ToDouble(row.Cells["气体高度（m）"].Value),
                    Convert.ToDouble(row.Cells["氢气占比"].Value),
                    Convert.ToDouble(row.Cells["氧气占比"].Value)
                };
                Data.psParameter.ps.Add(list);
            }

            // 回写界面上的combox到Data.componentParameter
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                if (selectedNode == $"电解槽{i + 1}")
                {
                    // 找到dianjiecao控件
                    var ctrl = tableLayoutPanel2.Controls.OfType<dianjiecao>().FirstOrDefault();
                    if (ctrl != null)
                    {
                        var cell = Data.componentParameter.electrolyticCell[i];
                        cell.flow.Clear();
                        cell.ps.Clear();
                        //6个流股编号
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox1.SelectedItem));
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox2.SelectedItem));
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox3.SelectedItem));
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox4.SelectedItem));
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox5.SelectedItem));
                        cell.flow.Add(Convert.ToInt32(ctrl.comboBox6.SelectedItem));
                        //2个ps编号
                        cell.ps.Add(Convert.ToInt32(ctrl.comboBox7.SelectedItem));
                        cell.ps.Add(Convert.ToInt32(ctrl.comboBox8.SelectedItem));
                        // 电流
                        float current = 0;
                        float.TryParse(ctrl.comboBox9.SelectedItem?.ToString(), out current);
                        cell.current = current;
                        Data.componentParameter.electrolyticCell[i] = cell;
                    }
                }
                else if (selectedNode == $"泵{i + 1}")
                {
                    var ctrl = tableLayoutPanel2.Controls.OfType<beng>().FirstOrDefault();
                    if (ctrl != null)
                    {
                        var pump = Data.componentParameter.pump[i];
                        pump.flow.Clear();
                        pump.ps.Clear();
                        pump.flow.Add(Convert.ToInt32(ctrl.comboBox1.SelectedItem));
                        pump.flow.Add(Convert.ToInt32(ctrl.comboBox3.SelectedItem));
                        pump.ps.Add(Convert.ToInt32(ctrl.comboBox7.SelectedItem));
                        Data.componentParameter.pump[i] = pump;
                    }
                }
            }
            if (selectedNode == "阴极分离器")
            {
                var ctrl = tableLayoutPanel2.Controls.OfType<fenliqi1>().FirstOrDefault();
                if (ctrl != null)
                {
                    var sep = Data.componentParameter.cathodeSeparator;
                    sep.flow.Clear();
                    sep.ps.Clear();
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox10.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox16.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox13.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox11.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox12.SelectedItem));
                    sep.ps.Add(Convert.ToInt32(ctrl.comboBox15.SelectedItem));
                    sep.ps.Add(Convert.ToInt32(ctrl.comboBox14.SelectedItem));
                    Data.componentParameter.cathodeSeparator = sep;
                }
            }
            else if (selectedNode == "阳极分离器")
            {
                var ctrl = tableLayoutPanel2.Controls.OfType<fenliqi1>().FirstOrDefault();
                if (ctrl != null)
                {
                    var sep = Data.componentParameter.anodeSeparator;
                    sep.flow.Clear();
                    sep.ps.Clear();
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox10.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox16.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox13.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox11.SelectedItem));
                    sep.flow.Add(Convert.ToInt32(ctrl.comboBox12.SelectedItem));
                    sep.ps.Add(Convert.ToInt32(ctrl.comboBox15.SelectedItem));
                    sep.ps.Add(Convert.ToInt32(ctrl.comboBox14.SelectedItem));
                    Data.componentParameter.anodeSeparator = sep;
                }
            }
            else if (selectedNode == "阴极阀门")
            {
                var ctrl = tableLayoutPanel2.Controls.OfType<famen>().FirstOrDefault();
                if (ctrl != null)
                {
                    var valve = Data.componentParameter.cathodeValve;
                    valve.flow.Clear();
                    valve.ps.Clear();
                    valve.flow.Add(Convert.ToInt32(ctrl.comboBox10.SelectedItem));
                    valve.flow.Add(Convert.ToInt32(ctrl.comboBox11.SelectedItem));
                    valve.ps.Add(Convert.ToInt32(ctrl.comboBox15.SelectedItem));
                    Data.componentParameter.cathodeValve = valve;
                }
            }
            else if (selectedNode == "阳极阀门")
            {
                var ctrl = tableLayoutPanel2.Controls.OfType<famen>().FirstOrDefault();
                if (ctrl != null)
                {
                    var valve = Data.componentParameter.anodeValve;
                    valve.flow.Clear();
                    valve.ps.Clear();
                    valve.flow.Add(Convert.ToInt32(ctrl.comboBox10.SelectedItem));
                    valve.flow.Add(Convert.ToInt32(ctrl.comboBox11.SelectedItem));
                    valve.ps.Add(Convert.ToInt32(ctrl.comboBox15.SelectedItem));
                    Data.componentParameter.anodeValve = valve;
                }
            }
            else if (selectedNode == "平衡管线")
            {
                var ctrl = tableLayoutPanel2.Controls.OfType<pipe>().FirstOrDefault();
                if (ctrl != null)
                {
                    var pipeObj = Data.componentParameter.balancePipe;
                    pipeObj.flow.Clear();
                    pipeObj.ps.Clear();
                    pipeObj.flow.Add(Convert.ToInt32(ctrl.comboBox1.SelectedItem));
                    pipeObj.flow.Add(Convert.ToInt32(ctrl.comboBox3.SelectedItem));
                    pipeObj.ps.Add(Convert.ToInt32(ctrl.comboBox7.SelectedItem));
                    Data.componentParameter.balancePipe = pipeObj;
                }
            }
        }

        public void UpdateUIFromData(string selectedNode)
        {
            numericUpDown1.Value = Data.flowParameter.flow.Count;
            numericUpDown2.Value = Data.psParameter.ps.Count;
            numericUpDown3.Value = Data.componentParameter.nElectrolyticCell;
            numericUpDown5.Value = 1;
            numericUpDown6.Value = 1;
            numericUpDown7.Value = 1;
            numericUpDown8.Value = 1;

            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();

            dt4.Columns.Add("流股编号", typeof(int));
            dt4.Columns.Add("氢气占比", typeof(double));
            dt4.Columns.Add("氧气占比", typeof(double));
            dt4.Columns.Add("水占比", typeof(double));
            dt4.Columns.Add("直径（m）", typeof(double));
            dt4.Columns.Add("长度（m）", typeof(double));

            dt5.Columns.Add("过程系统编号", typeof(int));
            dt5.Columns.Add("总物质量", typeof(string));
            dt5.Columns.Add("摩尔体积（m³/mol）", typeof(string));
            dt5.Columns.Add("压力", typeof(string));
            dt5.Columns.Add("液体高度（m）", typeof(string));
            dt5.Columns.Add("气体高度（m）", typeof(string));
            dt5.Columns.Add("氢气占比", typeof(string));
            dt5.Columns.Add("氧气占比", typeof(string));

            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                if (selectedNode == $"电解槽{i + 1}")
                {
                    // 数据表flow/ps 显示所有
                    PopulateAllFlowAndPs(dt4, dt5);
                    dataGridView4.DataSource = dt4;
                    dataGridView5.DataSource = dt5;

                    // 高亮所选部件对应的flow/ps
                    var selectedFlowsSet = new HashSet<double>(Data.componentParameter.electrolyticCell[i].flow.Select(x => Convert.ToDouble(x)));
                    var selectedPsSet = new HashSet<double>(Data.componentParameter.electrolyticCell[i].ps.Select(x => Convert.ToDouble(x)));
                    HighlightFlowAndPsRows(selectedFlowsSet, selectedPsSet);
                    BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet, selectedPsSet)));

                    // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                    foreach (Control control in tableLayoutPanel2.Controls)
                    {
                        if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                        {
                            tableLayoutPanel2.Controls.Remove(control);
                            control.Dispose();
                        }
                    }

                    dianjiecao dianjiecao = new dianjiecao();
                    tableLayoutPanel2.Controls.Add(dianjiecao, 0, 0);
                    dianjiecao.Dock = DockStyle.Fill;

                    // 初始化界面上的combox
                    int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                    int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                    int[] flowDefaultNum = Data.componentParameter.electrolyticCell[i].flow.Select(f => (int)f).ToArray();
                    int[] psDefaultNum = Data.componentParameter.electrolyticCell[i].ps.Select(p => (int)p).ToArray();
                    float current = (float)Data.componentParameter.electrolyticCell[i].current;

                    dianjiecao.comboBox1.Items.Clear();
                    dianjiecao.comboBox2.Items.Clear();
                    dianjiecao.comboBox3.Items.Clear();
                    dianjiecao.comboBox4.Items.Clear();
                    dianjiecao.comboBox5.Items.Clear();
                    dianjiecao.comboBox6.Items.Clear();
                    dianjiecao.comboBox7.Items.Clear();
                    dianjiecao.comboBox8.Items.Clear();
                    dianjiecao.comboBox9.Items.Clear();

                    foreach (var item in flowNumPool)
                    {
                        dianjiecao.comboBox1.Items.Add(item);
                        dianjiecao.comboBox2.Items.Add(item);
                        dianjiecao.comboBox3.Items.Add(item);
                        dianjiecao.comboBox4.Items.Add(item);
                        dianjiecao.comboBox5.Items.Add(item);
                        dianjiecao.comboBox6.Items.Add(item);
                    }
                    dianjiecao.comboBox1.SelectedItem = flowDefaultNum[0];
                    dianjiecao.comboBox2.SelectedItem = flowDefaultNum[1];
                    dianjiecao.comboBox3.SelectedItem = flowDefaultNum[2];
                    dianjiecao.comboBox4.SelectedItem = flowDefaultNum[3];
                    dianjiecao.comboBox5.SelectedItem = flowDefaultNum[4];
                    dianjiecao.comboBox6.SelectedItem = flowDefaultNum[5];

                    foreach (var item in psNumPool)
                    {
                        dianjiecao.comboBox7.Items.Add(item);
                        dianjiecao.comboBox8.Items.Add(item);
                    }
                    dianjiecao.comboBox7.SelectedItem = psDefaultNum[0];
                    dianjiecao.comboBox8.SelectedItem = psDefaultNum[1];

                    dianjiecao.comboBox9.Items.Add(current.ToString());
                    dianjiecao.comboBox9.SelectedItem = current.ToString();

                    return;
                }
                else if (selectedNode == $"泵{i + 1}")
                {
                    PopulateAllFlowAndPs(dt4, dt5);
                    dataGridView4.DataSource = dt4;
                    dataGridView5.DataSource = dt5;

                    // 高亮所选部件对应的flow/ps
                    var selectedFlowsSet = new HashSet<double>(Data.componentParameter.pump[i].flow.Select(x => Convert.ToDouble(x)));
                    var selectedPsSet = new HashSet<double>(Data.componentParameter.pump[i].ps.Select(x => Convert.ToDouble(x)));
                    HighlightFlowAndPsRows(selectedFlowsSet, selectedPsSet);
                    BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet, selectedPsSet)));

                    // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                    foreach (Control control in tableLayoutPanel2.Controls)
                    {
                        if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                        {
                            tableLayoutPanel2.Controls.Remove(control);
                            control.Dispose();
                        }
                    }

                    beng beng = new beng();
                    tableLayoutPanel2.Controls.Add(beng, 0, 0);
                    beng.Dock = DockStyle.Fill;

                    // 初始化界面上的combox
                    int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                    int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                    int[] flowDefaultNum = Data.componentParameter.pump[i].flow.Select(f => (int)f).ToArray();
                    int[] psDefaultNum = Data.componentParameter.pump[i].ps.Select(p => (int)p).ToArray();

                    beng.comboBox1.Items.Clear();
                    beng.comboBox7.Items.Clear();
                    beng.comboBox3.Items.Clear();

                    foreach (var item in flowNumPool)
                    {
                        beng.comboBox1.Items.Add(item);
                        beng.comboBox3.Items.Add(item);
                    }
                    beng.comboBox1.SelectedItem = flowDefaultNum[0];
                    beng.comboBox3.SelectedItem = flowDefaultNum[1];

                    foreach (var item in psNumPool)
                    {
                        beng.comboBox7.Items.Add(item);
                    }
                    beng.comboBox7.SelectedItem = psDefaultNum[0];

                    return;
                }
            }
            if (selectedNode == "阴极分离器")
            {
                // 数据表flow/ps 显示所有
                PopulateAllFlowAndPs(dt4, dt5);

                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

                // 高亮所选部件对应的flow/ps
                var selectedFlowsSet_cs = new HashSet<double>(Data.componentParameter.cathodeSeparator.flow.Select(x => Convert.ToDouble(x)));
                var selectedPsSet_cs = new HashSet<double>(Data.componentParameter.cathodeSeparator.ps.Select(x => Convert.ToDouble(x)));
                HighlightFlowAndPsRows(selectedFlowsSet_cs, selectedPsSet_cs);
                BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet_cs, selectedPsSet_cs)));

                // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                    {
                        tableLayoutPanel2.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                fenliqi1 fenliqi1 = new fenliqi1();
                tableLayoutPanel2.Controls.Add(fenliqi1, 0, 0);
                fenliqi1.Dock = DockStyle.Fill;

                // 初始化界面上的combox
                int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                int[] flowDefaultNum = Data.componentParameter.cathodeSeparator.flow.Select(f => (int)f).ToArray();
                int[] psDefaultNum = Data.componentParameter.cathodeSeparator.ps.Select(p => (int)p).ToArray();

                fenliqi1.comboBox10.Items.Clear();
                fenliqi1.comboBox16.Items.Clear();
                fenliqi1.comboBox13.Items.Clear();
                fenliqi1.comboBox11.Items.Clear();
                fenliqi1.comboBox12.Items.Clear();
                fenliqi1.comboBox15.Items.Clear();
                fenliqi1.comboBox16.Items.Clear();

                foreach (var item in flowNumPool)
                {
                    fenliqi1.comboBox10.Items.Add(item);
                    fenliqi1.comboBox16.Items.Add(item);
                    fenliqi1.comboBox13.Items.Add(item);
                    fenliqi1.comboBox11.Items.Add(item);
                    fenliqi1.comboBox12.Items.Add(item);
                }
                fenliqi1.comboBox10.SelectedItem = flowDefaultNum[0];
                fenliqi1.comboBox16.SelectedItem = flowDefaultNum[1];
                fenliqi1.comboBox13.SelectedItem = flowDefaultNum[2];
                fenliqi1.comboBox11.SelectedItem = flowDefaultNum[3];
                fenliqi1.comboBox12.SelectedItem = flowDefaultNum[4];

                foreach (var item in psNumPool)
                {
                    fenliqi1.comboBox15.Items.Add(item);
                    fenliqi1.comboBox14.Items.Add(item);
                }
                fenliqi1.comboBox15.SelectedItem = psDefaultNum[0];
                fenliqi1.comboBox14.SelectedItem = psDefaultNum[1];

                return;
            }
            else if (selectedNode == "阳极分离器")
            {
                // 数据表flow/ps 显示所有
                PopulateAllFlowAndPs(dt4, dt5);

                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

                // 高亮所选部件对应的flow/ps
                var selectedFlowsSet_as = new HashSet<double>(Data.componentParameter.anodeSeparator.flow.Select(x => Convert.ToDouble(x)));
                var selectedPsSet_as = new HashSet<double>(Data.componentParameter.anodeSeparator.ps.Select(x => Convert.ToDouble(x)));
                HighlightFlowAndPsRows(selectedFlowsSet_as, selectedPsSet_as);
                BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet_as, selectedPsSet_as)));

                // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                    {
                        tableLayoutPanel2.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                fenliqi1 fenliqi1 = new fenliqi1();
                tableLayoutPanel2.Controls.Add(fenliqi1, 0, 0);
                fenliqi1.Dock = DockStyle.Fill;

                // 初始化界面上的combox
                int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                int[] flowDefaultNum = Data.componentParameter.anodeSeparator.flow.Select(f => (int)f).ToArray();
                int[] psDefaultNum = Data.componentParameter.anodeSeparator.ps.Select(p => (int)p).ToArray();

                fenliqi1.comboBox10.Items.Clear();
                fenliqi1.comboBox16.Items.Clear();
                fenliqi1.comboBox13.Items.Clear();
                fenliqi1.comboBox11.Items.Clear();
                fenliqi1.comboBox12.Items.Clear();
                fenliqi1.comboBox15.Items.Clear();
                fenliqi1.comboBox16.Items.Clear();

                foreach (var item in flowNumPool)
                {
                    fenliqi1.comboBox10.Items.Add(item);
                    fenliqi1.comboBox16.Items.Add(item);
                    fenliqi1.comboBox13.Items.Add(item);
                    fenliqi1.comboBox11.Items.Add(item);
                    fenliqi1.comboBox12.Items.Add(item);
                }
                fenliqi1.comboBox10.SelectedItem = flowDefaultNum[0];
                fenliqi1.comboBox16.SelectedItem = flowDefaultNum[1];
                fenliqi1.comboBox13.SelectedItem = flowDefaultNum[2];
                fenliqi1.comboBox11.SelectedItem = flowDefaultNum[3];
                fenliqi1.comboBox12.SelectedItem = flowDefaultNum[4];

                foreach (var item in psNumPool)
                {
                    fenliqi1.comboBox15.Items.Add(item);
                    fenliqi1.comboBox14.Items.Add(item);
                }
                fenliqi1.comboBox15.SelectedItem = psDefaultNum[0];
                fenliqi1.comboBox14.SelectedItem = psDefaultNum[1];

                return;
            }
            else if (selectedNode == "阴极阀门")
            {
                // 数据表flow/ps 显示所有
                PopulateAllFlowAndPs(dt4, dt5);

                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

                // 高亮所选部件对应的flow/ps
                var selectedFlowsSet_cv = new HashSet<double>(Data.componentParameter.cathodeValve.flow.Select(x => Convert.ToDouble(x)));
                var selectedPsSet_cv = new HashSet<double>(Data.componentParameter.cathodeValve.ps.Select(x => Convert.ToDouble(x)));
                HighlightFlowAndPsRows(selectedFlowsSet_cv, selectedPsSet_cv);
                BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet_cv, selectedPsSet_cv)));

                // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                    {
                        tableLayoutPanel2.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                famen famen = new famen();
                tableLayoutPanel2.Controls.Add(famen, 0, 0);
                famen.Dock = DockStyle.Fill;

                // 初始化界面上的combox
                int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                int[] flowDefaultNum = Data.componentParameter.cathodeValve.flow.Select(f => (int)f).ToArray();
                int[] psDefaultNum = Data.componentParameter.cathodeValve.ps.Select(p => (int)p).ToArray();

                famen.comboBox10.Items.Clear();
                famen.comboBox11.Items.Clear();
                famen.comboBox15.Items.Clear();

                foreach (var item in flowNumPool)
                {
                    famen.comboBox10.Items.Add(item.ToString());
                    famen.comboBox11.Items.Add(item.ToString());
                }
                famen.comboBox10.SelectedItem = flowDefaultNum[0].ToString();
                famen.comboBox11.SelectedItem = flowDefaultNum[1].ToString();

                foreach (var item in psNumPool)
                {
                    famen.comboBox15.Items.Add(item.ToString());
                }
                famen.comboBox15.SelectedItem = psDefaultNum[0].ToString();

                return;
            }
            else if (selectedNode == "阳极阀门")
            {
                // 数据表flow/ps 显示所有
                PopulateAllFlowAndPs(dt4, dt5);

                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

                // 高亮所选部件对应的flow/ps
                var selectedFlowsSet_av = new HashSet<double>(Data.componentParameter.anodeValve.flow.Select(x => Convert.ToDouble(x)));
                var selectedPsSet_av = new HashSet<double>(Data.componentParameter.anodeValve.ps.Select(x => Convert.ToDouble(x)));
                HighlightFlowAndPsRows(selectedFlowsSet_av, selectedPsSet_av);
                BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet_av, selectedPsSet_av)));

                // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                    {
                        tableLayoutPanel2.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                famen famen = new famen();
                tableLayoutPanel2.Controls.Add(famen, 0, 0);
                famen.Dock = DockStyle.Fill;

                // 初始化界面上的combox
                int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                int[] flowDefaultNum = Data.componentParameter.anodeValve.flow.Select(f => (int)f).ToArray();
                int[] psDefaultNum = Data.componentParameter.anodeValve.ps.Select(p => (int)p).ToArray();

                famen.comboBox10.Items.Clear();
                famen.comboBox11.Items.Clear();
                famen.comboBox15.Items.Clear();

                foreach (var item in flowNumPool)
                {
                    famen.comboBox10.Items.Add(item.ToString());
                    famen.comboBox11.Items.Add(item.ToString());
                }
                famen.comboBox10.SelectedItem = flowDefaultNum[0].ToString();
                famen.comboBox11.SelectedItem = flowDefaultNum[1].ToString();

                foreach (var item in psNumPool)
                {
                    famen.comboBox15.Items.Add(item.ToString());
                }
                famen.comboBox15.SelectedItem = psDefaultNum[0].ToString();

                return;
            }
            else if (selectedNode == "平衡管线")
            {
                // 数据表flow/ps 显示所有
                PopulateAllFlowAndPs(dt4, dt5);

                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

                // 高亮所选部件对应的flow/ps
                var selectedFlowsSet_bp = new HashSet<double>(Data.componentParameter.balancePipe.flow.Select(x => Convert.ToDouble(x)));
                var selectedPsSet_bp = new HashSet<double>(Data.componentParameter.balancePipe.ps.Select(x => Convert.ToDouble(x)));
                HighlightFlowAndPsRows(selectedFlowsSet_bp, selectedPsSet_bp);
                BeginInvoke(new Action(() => HighlightFlowAndPsRows(selectedFlowsSet_bp, selectedPsSet_bp)));

                // 清除原来的电解槽控件，泵，分离器，阀门控件，平衡管线控件
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control is beng || control is fenliqi1 || control is dianjiecao || control is famen || control is pipe)
                    {
                        tableLayoutPanel2.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                pipe pipe = new pipe();
                tableLayoutPanel2.Controls.Add(pipe, 0, 0);
                pipe.Dock = DockStyle.Fill;

                // 初始化界面上的combox
                int[] flowNumPool = Data.flowParameter.flow.Select(f => (int)f[0]).ToArray();
                int[] psNumPool = Data.psParameter.ps.Select(p => (int)p[0]).ToArray();
                int[] flowDefaultNum = Data.componentParameter.balancePipe.flow.Select(f => (int)f).ToArray();
                int[] psDefaultNum = Data.componentParameter.balancePipe.ps.Select(p => (int)p).ToArray();

                pipe.comboBox1.Items.Clear();
                pipe.comboBox3.Items.Clear();
                pipe.comboBox7.Items.Clear();

                foreach (var item in flowNumPool)
                {
                    pipe.comboBox1.Items.Add(item.ToString());
                    pipe.comboBox3.Items.Add(item.ToString());
                }
                pipe.comboBox1.SelectedItem = flowDefaultNum[0].ToString();
                pipe.comboBox3.SelectedItem = flowDefaultNum[1].ToString();

                foreach (var item in psNumPool)
                {
                    pipe.comboBox7.Items.Add(item.ToString());
                }
                pipe.comboBox7.SelectedItem = psDefaultNum[0].ToString();
                return;
            }
        }

        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "部件" || e.Node.Text == "电解槽" || e.Node.Text == "泵" || e.Node.Text == "分离器" || e.Node.Text == "阀门")
                return;

            if (_lastSelectedNodeText != null)
            {
                UpdateDataFromUI(_lastSelectedNodeText);
            }
            UpdateUIFromData(e.Node.Text);
            _lastSelectedNodeText = e.Node.Text;
        }

        public void SaveData()
        {
            UpdateDataFromUI(_lastSelectedNodeText);
        }
    }
}

