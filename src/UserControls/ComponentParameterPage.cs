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
        private float originalColumnWidth;

        public ComponentParameterPage()
        {
            InitializeComponent();
            initTreeview();
            originalColumnWidth = tableLayoutPanel3.ColumnStyles[2].Width;

            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];
        }

        private void initTreeview()
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedNode = e.Node.Text;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            dt1.Columns.Add("Flow", typeof(double));
            dt2.Columns.Add("Ps", typeof(double));
            dt3.Columns.Add("I_current", typeof(double));

            dt4.Columns.Add("Flow号", typeof(int));
            dt4.Columns.Add("氢气", typeof(double));
            dt4.Columns.Add("氧气", typeof(double));
            dt4.Columns.Add("水", typeof(double));
            dt4.Columns.Add("管道直径", typeof(double));
            dt4.Columns.Add("管道长度", typeof(double));

            dt5.Columns.Add("PS号", typeof(int));
            dt5.Columns.Add("物质的量", typeof(string));
            dt5.Columns.Add("摩尔体积", typeof(string));
            dt5.Columns.Add("压强", typeof(string));
            dt5.Columns.Add("液体高度", typeof(string));
            dt5.Columns.Add("气体高度", typeof(string));
            dt5.Columns.Add("氢气", typeof(string));
            dt5.Columns.Add("氧气", typeof(string));

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
                            dr["Flow号"] = Data.flowParameter.flow[j][0];
                            dr["氢气"] = Data.flowParameter.flow[j][1];
                            dr["氧气"] = Data.flowParameter.flow[j][2];
                            dr["水"] = Data.flowParameter.flow[j][3];
                            dr["管道直径"] = Data.flowParameter.flow[j][4];
                            dr["管道长度"] = Data.flowParameter.flow[j][5];
                            dt4.Rows.Add(dr);
                        }
                    }

                    //数据表ps
                    for (int j = 0; j < Data.psParameter.ps.Count; j++)
                    {
                        if (Data.componentParameter.electrolyticCell[i].ps.Contains(Data.psParameter.ps[j][0]))
                        {
                            DataRow dr = dt5.NewRow();
                            dr["PS号"] = Data.psParameter.ps[j][0];
                            dr["物质的量"] = Data.psParameter.ps[j][1];
                            dr["摩尔体积"] = Data.psParameter.ps[j][2];
                            dr["压强"] = Data.psParameter.ps[j][3];
                            dr["液体高度"] = Data.psParameter.ps[j][4];
                            dr["气体高度"] = Data.psParameter.ps[j][5];
                            dr["氢气"] = Data.psParameter.ps[j][6];
                            dr["氧气"] = Data.psParameter.ps[j][7];
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
                            dr["Flow号"] = Data.flowParameter.flow[j][0];
                            dr["氢气"] = Data.flowParameter.flow[j][1];
                            dr["氧气"] = Data.flowParameter.flow[j][2];
                            dr["水"] = Data.flowParameter.flow[j][3];
                            dr["管道直径"] = Data.flowParameter.flow[j][4];
                            dr["管道长度"] = Data.flowParameter.flow[j][5];
                            dt4.Rows.Add(dr);
                        }
                    }

                    //数据表ps
                    for (int j = 0; j < Data.psParameter.ps.Count; j++)
                    {
                        if (Data.componentParameter.pump[i].ps.Contains(Data.psParameter.ps[j][0]))
                        {
                            DataRow dr = dt5.NewRow();
                            dr["PS号"] = Data.psParameter.ps[j][0];
                            dr["物质的量"] = Data.psParameter.ps[j][1];
                            dr["摩尔体积"] = Data.psParameter.ps[j][2];
                            dr["压强"] = Data.psParameter.ps[j][3];
                            dr["液体高度"] = Data.psParameter.ps[j][4];
                            dr["气体高度"] = Data.psParameter.ps[j][5];
                            dr["氢气"] = Data.psParameter.ps[j][6];
                            dr["氧气"] = Data.psParameter.ps[j][7];
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
                        dr["Flow号"] = Data.flowParameter.flow[j][0];
                        dr["氢气"] = Data.flowParameter.flow[j][1];
                        dr["氧气"] = Data.flowParameter.flow[j][2];
                        dr["水"] = Data.flowParameter.flow[j][3];
                        dr["管道直径"] = Data.flowParameter.flow[j][4];
                        dr["管道长度"] = Data.flowParameter.flow[j][5];
                        dt4.Rows.Add(dr);
                    }
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.cathodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        DataRow dr = dt5.NewRow();
                        dr["PS号"] = Data.psParameter.ps[j][0];
                        dr["物质的量"] = Data.psParameter.ps[j][1];
                        dr["摩尔体积"] = Data.psParameter.ps[j][2];
                        dr["压强"] = Data.psParameter.ps[j][3];
                        dr["液体高度"] = Data.psParameter.ps[j][4];
                        dr["气体高度"] = Data.psParameter.ps[j][5];
                        dr["氢气"] = Data.psParameter.ps[j][6];
                        dr["氧气"] = Data.psParameter.ps[j][7];
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
                        dr["Flow号"] = Data.flowParameter.flow[j][0];
                        dr["氢气"] = Data.flowParameter.flow[j][1];
                        dr["氧气"] = Data.flowParameter.flow[j][2];
                        dr["水"] = Data.flowParameter.flow[j][3];
                        dr["管道直径"] = Data.flowParameter.flow[j][4];
                        dr["管道长度"] = Data.flowParameter.flow[j][5];
                        dt4.Rows.Add(dr);
                    }
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.anodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        DataRow dr = dt5.NewRow();
                        dr["PS号"] = Data.psParameter.ps[j][0];
                        dr["物质的量"] = Data.psParameter.ps[j][1];
                        dr["摩尔体积"] = Data.psParameter.ps[j][2];
                        dr["压强"] = Data.psParameter.ps[j][3];
                        dr["液体高度"] = Data.psParameter.ps[j][4];
                        dr["气体高度"] = Data.psParameter.ps[j][5];
                        dr["氢气"] = Data.psParameter.ps[j][6];
                        dr["氧气"] = Data.psParameter.ps[j][7];
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
                        dr["Flow号"] = Data.flowParameter.flow[j][0];
                        dr["氢气"] = Data.flowParameter.flow[j][1];
                        dr["氧气"] = Data.flowParameter.flow[j][2];
                        dr["水"] = Data.flowParameter.flow[j][3];
                        dr["管道直径"] = Data.flowParameter.flow[j][4];
                        dr["管道长度"] = Data.flowParameter.flow[j][5];
                        dt4.Rows.Add(dr);
                    }
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.cathodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        DataRow dr = dt5.NewRow();
                        dr["PS号"] = Data.psParameter.ps[j][0];
                        dr["物质的量"] = Data.psParameter.ps[j][1];
                        dr["摩尔体积"] = Data.psParameter.ps[j][2];
                        dr["压强"] = Data.psParameter.ps[j][3];
                        dr["液体高度"] = Data.psParameter.ps[j][4];
                        dr["气体高度"] = Data.psParameter.ps[j][5];
                        dr["氢气"] = Data.psParameter.ps[j][6];
                        dr["氧气"] = Data.psParameter.ps[j][7];
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
                        dr["Flow号"] = Data.flowParameter.flow[j][0];
                        dr["氢气"] = Data.flowParameter.flow[j][1];
                        dr["氧气"] = Data.flowParameter.flow[j][2];
                        dr["水"] = Data.flowParameter.flow[j][3];
                        dr["管道直径"] = Data.flowParameter.flow[j][4];
                        dr["管道长度"] = Data.flowParameter.flow[j][5];
                        dt4.Rows.Add(dr);
                    }
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.anodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        DataRow dr = dt5.NewRow();
                        dr["PS号"] = Data.psParameter.ps[j][0];
                        dr["物质的量"] = Data.psParameter.ps[j][1];
                        dr["摩尔体积"] = Data.psParameter.ps[j][2];
                        dr["压强"] = Data.psParameter.ps[j][3];
                        dr["液体高度"] = Data.psParameter.ps[j][4];
                        dr["气体高度"] = Data.psParameter.ps[j][5];
                        dr["氢气"] = Data.psParameter.ps[j][6];
                        dr["氧气"] = Data.psParameter.ps[j][7];
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
                        dr["Flow号"] = Data.flowParameter.flow[j][0];
                        dr["氢气"] = Data.flowParameter.flow[j][1];
                        dr["氧气"] = Data.flowParameter.flow[j][2];
                        dr["水"] = Data.flowParameter.flow[j][3];
                        dr["管道直径"] = Data.flowParameter.flow[j][4];
                        dr["管道长度"] = Data.flowParameter.flow[j][5];
                        dt4.Rows.Add(dr);
                    }
                }

                //数据表ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.balancePipe.ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        DataRow dr = dt5.NewRow();
                        dr["PS号"] = Data.psParameter.ps[j][0];
                        dr["物质的量"] = Data.psParameter.ps[j][1];
                        dr["摩尔体积"] = Data.psParameter.ps[j][2];
                        dr["压强"] = Data.psParameter.ps[j][3];
                        dr["液体高度"] = Data.psParameter.ps[j][4];
                        dr["气体高度"] = Data.psParameter.ps[j][5];
                        dr["氢气"] = Data.psParameter.ps[j][6];
                        dr["氧气"] = Data.psParameter.ps[j][7];
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
                return;
            }
        }

    }
}

