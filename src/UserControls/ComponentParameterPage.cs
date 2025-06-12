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

namespace ElectCell_HMI
{
    public partial class ComponentParameterPage : UserControl
    {
        public float originalColumnWidth;

        public ComponentParameterPage()
        {
            InitializeComponent();
            initTreeview();
            originalColumnWidth = tableLayoutPanel3.ColumnStyles[2].Width;
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];

            //隐藏tablelayoutPanel3以及其中的所有控件
            tableLayoutPanel3.Visible = false;          
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

        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedNode = e.Node.Text;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            dt1.Columns.Add("流股", typeof(double));
            dt2.Columns.Add("过程系统", typeof(double));
            dt3.Columns.Add("电流（A）", typeof(double));

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
                    //参数表
                    dataGridView3.Visible = true;
                    tableLayoutPanel3.ColumnStyles[2].Width = originalColumnWidth;

                    for (int j = 0; j < Data.componentParameter.electrolyticCell[i].flow.Count; j++)
                    {
                        dt1.Rows.Add(Data.componentParameter.electrolyticCell[i].flow[j]);
                    }

                    for (int j = 0; j < Data.componentParameter.electrolyticCell[i].ps.Count; j++)
                    {
                        dt2.Rows.Add(Data.componentParameter.electrolyticCell[i].ps[j]);
                    }

                    dt3.Rows.Add(Data.componentParameter.electrolyticCell[i].current);

                    //数据表flow
                    for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                    {
                        if (Data.componentParameter.electrolyticCell[i].flow.Contains(Data.flowParameter.flow[j][0]))
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
                    }

                    //数据表ps
                    for (int j = 0; j < Data.psParameter.ps.Count; j++)
                    {
                        if (Data.componentParameter.electrolyticCell[i].ps.Contains(Data.psParameter.ps[j][0]))
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

                    for (int j = 0; j < 50; j++)
                    {
                        DataRow dr1 = dt1.NewRow();
                        DataRow dr2 = dt2.NewRow();
                        DataRow dr3 = dt3.NewRow();
                        DataRow dr4 = dt4.NewRow();
                        DataRow dr5 = dt5.NewRow();
                        dt1.Rows.Add(dr1);
                        dt2.Rows.Add(dr2);
                        dt3.Rows.Add(dr3);
                        dt4.Rows.Add(dr4);
                        dt5.Rows.Add(dr5);
                    }

                    dataGridView1.DataSource = dt1;
                    dataGridView2.DataSource = dt2;
                    dataGridView3.DataSource = dt3;
                    dataGridView4.DataSource = dt4;
                    dataGridView5.DataSource = dt5;

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
                    return;
                }
                else if (selectedNode == $"泵{i + 1}")
                {
                    dataGridView3.Visible = false;
                    tableLayoutPanel3.ColumnStyles[2].Width = 0;

                    for (int j = 0; j < Data.componentParameter.pump[i].flow.Count; j++)
                    {
                        dt1.Rows.Add(Data.componentParameter.pump[i].flow[j]);
                    }

                    for (int j = 0; j < Data.componentParameter.pump[i].ps.Count; j++)
                    {
                        dt2.Rows.Add(Data.componentParameter.pump[i].ps[j]);
                    }

                    //数据表flow
                    for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                    {
                        if (Data.componentParameter.pump[i].flow.Contains(Data.flowParameter.flow[j][0]))
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
                    }

                    //数据表ps
                    for (int j = 0; j < Data.psParameter.ps.Count; j++)
                    {
                        if (Data.componentParameter.pump[i].ps.Contains(Data.psParameter.ps[j][0]))
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

                    for (int j = 0; j < 50; j++)
                    {
                        DataRow dr1 = dt1.NewRow();
                        DataRow dr2 = dt2.NewRow();
                        DataRow dr4 = dt4.NewRow();
                        DataRow dr5 = dt5.NewRow();
                        dt1.Rows.Add(dr1);
                        dt2.Rows.Add(dr2);
                        dt4.Rows.Add(dr4);
                        dt5.Rows.Add(dr5);
                    }

                    dataGridView1.DataSource = dt1;
                    dataGridView2.DataSource = dt2;
                    dataGridView3.DataSource = dt3;
                    dataGridView4.DataSource = dt4;
                    dataGridView5.DataSource = dt5;

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

                    return;
                }
            }
            if (selectedNode == "阴极分离器")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel3.ColumnStyles[2].Width = 0;

                for (int j = 0; j < Data.componentParameter.cathodeSeparator.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.cathodeSeparator.flow[j]);
                }

                for (int j = 0; j < Data.componentParameter.cathodeSeparator.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.cathodeSeparator.ps[j]);
                }

                //数据表flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.cathodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
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
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.cathodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
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

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    DataRow dr4 = dt4.NewRow();
                    DataRow dr5 = dt5.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                    dt4.Rows.Add(dr4);
                    dt5.Rows.Add(dr5);
                }

                dataGridView1.DataSource = dt1;
                dataGridView2.DataSource = dt2;
                dataGridView3.DataSource = dt3;
                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

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
                return;
            }
            else if (selectedNode == "阳极分离器")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel3.ColumnStyles[2].Width = 0;

                for (int j = 0; j < Data.componentParameter.anodeSeparator.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.anodeSeparator.flow[j]);
                }

                for (int j = 0; j < Data.componentParameter.anodeSeparator.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.anodeSeparator.ps[j]);
                }

                //数据表flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.anodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
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
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.anodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
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

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    DataRow dr4 = dt4.NewRow();
                    DataRow dr5 = dt5.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                    dt4.Rows.Add(dr4);
                    dt5.Rows.Add(dr5);
                }

                dataGridView1.DataSource = dt1;
                dataGridView2.DataSource = dt2;
                dataGridView3.DataSource = dt3;
                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

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

                return;
            }
            else if (selectedNode == "阴极阀门")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel3.ColumnStyles[2].Width = 0;

                for (int j = 0; j < Data.componentParameter.cathodeValve.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.cathodeValve.flow[j]);
                }

                for (int j = 0; j < Data.componentParameter.cathodeValve.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.cathodeValve.ps[j]);
                }

                //数据表flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.cathodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
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
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.cathodeValve.ps.Contains(Data.psParameter.ps[j][0]))
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

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    DataRow dr4 = dt4.NewRow();
                    DataRow dr5 = dt5.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                    dt4.Rows.Add(dr4);
                    dt5.Rows.Add(dr5);
                }

                dataGridView1.DataSource = dt1;
                dataGridView2.DataSource = dt2;
                dataGridView3.DataSource = dt3;
                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

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

                return;
            }
            else if (selectedNode == "阳极阀门")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel3.ColumnStyles[2].Width = 0;

                for (int j = 0; j < Data.componentParameter.anodeValve.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.anodeValve.flow[j]);
                }

                for (int j = 0; j < Data.componentParameter.anodeValve.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.anodeValve.ps[j]);
                }

                //数据表flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.anodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
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
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.anodeValve.ps.Contains(Data.psParameter.ps[j][0]))
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

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    DataRow dr4 = dt4.NewRow();
                    DataRow dr5 = dt5.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                    dt4.Rows.Add(dr4);
                    dt5.Rows.Add(dr5);
                }

                dataGridView1.DataSource = dt1;
                dataGridView2.DataSource = dt2;
                dataGridView3.DataSource = dt3;
                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

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
                return;
            }
            else if (selectedNode == "平衡管线")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel3.ColumnStyles[2].Width = 0;

                for (int j = 0; j < Data.componentParameter.balancePipe.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.balancePipe.flow[j]);
                }

                for (int j = 0; j < Data.componentParameter.balancePipe.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.balancePipe.ps[j]);
                }

                //数据表flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.balancePipe.flow.Contains(Data.flowParameter.flow[j][0]))
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
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.balancePipe.ps.Contains(Data.psParameter.ps[j][0]))
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

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    DataRow dr4 = dt4.NewRow();
                    DataRow dr5 = dt5.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                    dt4.Rows.Add(dr4);
                    dt5.Rows.Add(dr5);
                }

                dataGridView1.DataSource = dt1;
                dataGridView2.DataSource = dt2;
                dataGridView3.DataSource = dt3;
                dataGridView4.DataSource = dt4;
                dataGridView5.DataSource = dt5;

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

                return;
            }
        }

        public void SaveData()
        {
            //// 保存 dataGridView1 的数据
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.IsNewRow || row.Cells["Flow"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Flow"].Value.ToString()))
            //    {
            //        continue;
            //    }

            //    double flowValue = Convert.ToDouble(row.Cells["Flow"].Value);
            //    // 假设 Data.componentParameter.electrolyticCell[0] 是当前选中的电解槽
            //    var cell = Data.componentParameter.electrolyticCell[0];
            //    cell.flow.Add(flowValue);
            //    Data.componentParameter.electrolyticCell[0] = cell;
            //}

            //// 保存 dataGridView2 的数据
            //foreach (DataGridViewRow row in dataGridView2.Rows)
            //{
            //    if (row.IsNewRow || row.Cells["Ps"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Ps"].Value.ToString()))
            //    {
            //        continue;
            //    }

            //    double psValue = Convert.ToDouble(row.Cells["Ps"].Value);
            //    // 假设 Data.componentParameter.electrolyticCell[0] 是当前选中的电解槽
            //    var cell = Data.componentParameter.electrolyticCell[0];
            //    cell.ps.Add(psValue);
            //    Data.componentParameter.electrolyticCell[0] = cell;
            //}

            //// 保存 dataGridView3 的数据
            //foreach (DataGridViewRow row in dataGridView3.Rows)
            //{
            //    if (row.IsNewRow || row.Cells["I_current"].Value == null || string.IsNullOrWhiteSpace(row.Cells["I_current"].Value.ToString()))
            //    {
            //        continue;
            //    }

            //    double currentValue = Convert.ToDouble(row.Cells["I_current"].Value);
            //    // 假设 Data.componentParameter.electrolyticCell[0] 是当前选中的电解槽
            //    var cell = Data.componentParameter.electrolyticCell[0];
            //    cell.current = currentValue;
            //    Data.componentParameter.electrolyticCell[0] = cell;
            //}

            //// 保存 dataGridView4 的数据
            //foreach (DataGridViewRow row in dataGridView4.Rows)
            //{
            //    if (row.IsNewRow || row.Cells["Flow号"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Flow号"].Value.ToString()) ||
            //        row.Cells["氢气"].Value == null || string.IsNullOrWhiteSpace(row.Cells["氢气"].Value.ToString()) ||
            //        row.Cells["氧气"].Value == null || string.IsNullOrWhiteSpace(row.Cells["氧气"].Value.ToString()) ||
            //        row.Cells["水"].Value == null || string.IsNullOrWhiteSpace(row.Cells["水"].Value.ToString()) ||
            //        row.Cells["管道直径（m）"].Value == null || string.IsNullOrWhiteSpace(row.Cells["管道直径（m）"].Value.ToString()) ||
            //        row.Cells["管道长度（m）"].Value == null || string.IsNullOrWhiteSpace(row.Cells["管道长度（m）"].Value.ToString()))
            //    {
            //        continue;
            //    }

            //    int flowNumber = Convert.ToInt32(row.Cells["Flow号"].Value);
            //    double hydrogen = Convert.ToDouble(row.Cells["氢气"].Value);
            //    double oxygen = Convert.ToDouble(row.Cells["氧气"].Value);
            //    double water = Convert.ToDouble(row.Cells["水"].Value);
            //    double pipeDiameter = Convert.ToDouble(row.Cells["管道直径（m）"].Value);
            //    double pipeLength = Convert.ToDouble(row.Cells["管道长度（m）"].Value);

            //    // 假设 Data.flowParameter.flow 是一个 List<List<double>>
            //    Data.flowParameter.flow.Add(new List<double> { flowNumber, hydrogen, oxygen, water, pipeDiameter, pipeLength });
            //}

            //// 保存 dataGridView5 的数据
            //foreach (DataGridViewRow row in dataGridView5.Rows)
            //{
            //    if (row.IsNewRow || row.Cells["PS号"].Value == null || string.IsNullOrWhiteSpace(row.Cells["PS号"].Value.ToString()) ||
            //        row.Cells["物质的量"].Value == null || string.IsNullOrWhiteSpace(row.Cells["物质的量"].Value.ToString()) ||
            //        row.Cells["摩尔体积（m³/mol）"].Value == null || string.IsNullOrWhiteSpace(row.Cells["摩尔体积（m³/mol）"].Value.ToString()) ||
            //        row.Cells["压力"].Value == null || string.IsNullOrWhiteSpace(row.Cells["压力"].Value.ToString()) ||
            //        row.Cells["液体高度（m）"].Value == null || string.IsNullOrWhiteSpace(row.Cells["液体高度（m）"].Value.ToString()) ||
            //        row.Cells["气体高度（m）"].Value == null || string.IsNullOrWhiteSpace(row.Cells["气体高度（m）"].Value.ToString()) ||
            //        row.Cells["氢气"].Value == null || string.IsNullOrWhiteSpace(row.Cells["氢气"].Value.ToString()) ||
            //        row.Cells["氧气"].Value == null || string.IsNullOrWhiteSpace(row.Cells["氧气"].Value.ToString()))
            //    {
            //        continue;
            //    }

            //    int psNumber = Convert.ToInt32(row.Cells["PS号"].Value);
            //    double substanceAmount = Convert.ToDouble(row.Cells["物质的量"].Value);
            //    double molarVolume = Convert.ToDouble(row.Cells["摩尔体积（m³/mol）"].Value);
            //    double pressure = Convert.ToDouble(row.Cells["压力"].Value);
            //    double liquidHeight = Convert.ToDouble(row.Cells["液体高度（m）"].Value);
            //    double gasHeight = Convert.ToDouble(row.Cells["气体高度（m）"].Value);
            //    double hydrogen = Convert.ToDouble(row.Cells["氢气"].Value);
            //    double oxygen = Convert.ToDouble(row.Cells["氧气"].Value);

            //    // 假设 Data.psParameter.ps 是一个 List<List<double>>
            //    Data.psParameter.ps.Add(new List<double> { psNumber, substanceAmount, molarVolume, pressure, liquidHeight, gasHeight, hydrogen, oxygen });
            //}
        }


    }
}

