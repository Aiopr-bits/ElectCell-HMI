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
            originalColumnWidth = tableLayoutPanel1.ColumnStyles[3].Width;
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
            dt1.Columns.Add("flow", typeof(double));
            dt2.Columns.Add("ps", typeof(double));
            dt3.Columns.Add("I_current", typeof(double));
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                if (selectedNode == $"电解槽{i + 1}")
                {
                    dataGridView3.Visible = true;
                    tableLayoutPanel1.ColumnStyles[3].Width = originalColumnWidth;

                    for (int j=0;j< Data.componentParameter.electrolyticCell[i].flow.Count; j++)
                    {
                        dt1.Rows.Add(Data.componentParameter.electrolyticCell[i].flow[j]);
                    }
                    dataGridView1.DataSource = dt1;
                
                    for (int j = 0; j < Data.componentParameter.electrolyticCell[i].ps.Count; j++)
                    {
                        dt2.Rows.Add(Data.componentParameter.electrolyticCell[i].ps[j]);
                    }
                    dataGridView2.DataSource = dt2;
                    
                    dt3.Rows.Add(Data.componentParameter.electrolyticCell[i].current);
                    dataGridView3.DataSource = dt3;

                    for (int j = 0; j < 50; j++)
                    {
                        DataRow dr1 = dt1.NewRow();
                        DataRow dr2 = dt2.NewRow();
                        DataRow dr3 = dt3.NewRow();
                        dt1.Rows.Add(dr1);
                        dt2.Rows.Add(dr2);
                        dt3.Rows.Add(dr3);
                    }
                    return;
                }
                else if (selectedNode == $"泵{i + 1}")
                {
                    dataGridView3.Visible = false;
                    tableLayoutPanel1.ColumnStyles[3].Width = 0;

                    for (int j = 0; j < Data.componentParameter.pump[i].flow.Count; j++)
                    {
                        dt1.Rows.Add(Data.componentParameter.pump[i].flow[j]);
                    }
                    dataGridView1.DataSource = dt1;

                    for (int j = 0; j < Data.componentParameter.pump[i].ps.Count; j++)
                    {
                        dt2.Rows.Add(Data.componentParameter.pump[i].ps[j]);
                    }
                    dataGridView2.DataSource = dt2;

                    for (int j = 0; j < 50; j++)
                    {
                        DataRow dr1 = dt1.NewRow();
                        DataRow dr2 = dt2.NewRow();
                        dt1.Rows.Add(dr1);
                        dt2.Rows.Add(dr2);
                    }
                    return;
                }
            }

            if (selectedNode == "阴极分离器")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;

                for (int j = 0; j < Data.componentParameter.cathodeSeparator.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.cathodeSeparator.flow[j]);
                }
                dataGridView1.DataSource = dt1;

                for (int j = 0; j < Data.componentParameter.cathodeSeparator.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.cathodeSeparator.ps[j]);
                }
                dataGridView2.DataSource = dt2;

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                }

                return;
            }
            else if (selectedNode == "阳极分离器")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;

                for (int j = 0; j < Data.componentParameter.anodeSeparator.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.anodeSeparator.flow[j]);
                }
                dataGridView1.DataSource = dt1;

                for (int j = 0; j < Data.componentParameter.anodeSeparator.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.anodeSeparator.ps[j]);
                }
                dataGridView2.DataSource = dt2;

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                }
                return;
            }
            else if (selectedNode == "阴极阀门")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;

                for (int j = 0; j < Data.componentParameter.cathodeValve.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.cathodeValve.flow[j]);
                }
                dataGridView1.DataSource = dt1;

                for (int j = 0; j < Data.componentParameter.cathodeValve.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.cathodeValve.ps[j]);
                }
                dataGridView2.DataSource = dt2;

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                }
                return;
            }
            else if (selectedNode == "阳极阀门")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;

                for (int j = 0; j < Data.componentParameter.anodeValve.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.anodeValve.flow[j]);
                }
                dataGridView1.DataSource = dt1;

                for (int j = 0; j < Data.componentParameter.anodeValve.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.anodeValve.ps[j]);
                }
                dataGridView2.DataSource = dt2;

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                }
                return;
            }
            else if (selectedNode == "平衡管线")
            {
                dataGridView3.Visible = false;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;

                for (int j = 0; j < Data.componentParameter.balancePipe.flow.Count; j++)
                {
                    dt1.Rows.Add(Data.componentParameter.balancePipe.flow[j]);
                }
                dataGridView1.DataSource = dt1;

                for (int j = 0; j < Data.componentParameter.balancePipe.ps.Count; j++)
                {
                    dt2.Rows.Add(Data.componentParameter.balancePipe.ps[j]);
                }
                dataGridView2.DataSource = dt2;

                for (int j = 0; j < 50; j++)
                {
                    DataRow dr1 = dt1.NewRow();
                    DataRow dr2 = dt2.NewRow();
                    dt1.Rows.Add(dr1);
                    dt2.Rows.Add(dr2);
                }
                return;
            }
        }

    }
}

