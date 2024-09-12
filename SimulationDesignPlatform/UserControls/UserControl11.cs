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
    public partial class UserControl11 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl11()
        {
            InitializeComponent();
            Show_TreeView();

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

        private void Show_TreeView()
        {
            if (Data.caseUsePath == "" || Data.caseUsePath == null)
            {
                MessageBox.Show("请先指定工作目录！");
                return;
            }
            TreeNode tn1 = new TreeNode("部件");
            treeView1.Nodes.Add(tn1);

            TreeNode tn2 = new TreeNode("Part_one");
            tn1.Nodes.Add(tn2);
            TreeNode tn3 = new TreeNode("electrolyzer");
            tn2.Nodes.Add(tn3);
            for (int i = 0; i < Data.n_ele; i++)
            {
                TreeNode tn31 = new TreeNode("electrolyzer" + (i + 1));
                tn3.Nodes.Add(tn31);
            }

            TreeNode tn4 = new TreeNode("Part_two");
            tn1.Nodes.Add(tn4);
            TreeNode tn5 = new TreeNode("Cathode_separator");
            tn4.Nodes.Add(tn5);
            TreeNode tn6 = new TreeNode("Anode_separator");
            tn4.Nodes.Add(tn6);

            TreeNode tn7 = new TreeNode("Part_three");
            tn1.Nodes.Add(tn7);
            TreeNode tn8 = new TreeNode("Balance_line");
            tn7.Nodes.Add(tn8);
            TreeNode tn9 = new TreeNode("Cathode_valve");
            tn7.Nodes.Add(tn9);
            TreeNode tn10 = new TreeNode("Anode_valve");
            tn7.Nodes.Add(tn10);

			// 树默认展开。20240321，由M添加
			Expand(treeView1.Nodes);
			void Expand(TreeNodeCollection nodes)
			{
				foreach (TreeNode node in nodes)
				{
					node.Expand();
					if (node.Nodes != null || node.Nodes.Count > 0)
					{
						Expand(node.Nodes);
					}
				}
			}
		}

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode sn1 = treeView1.SelectedNode; // 获取选中的节点
            if (sn1 != null)
            {
                // 判断节点是否有子节点
                if (sn1.Nodes.Count != 0)
                {
                    return;
                }
            }
            string node_text = sn1.Text;
            if (node_text == "")
            {
                return;
            }

            if (node_text.Contains("electrolyzer"))
            {
                int node_id = int.Parse(node_text.Substring(node_text.Length - 1, 1));
                Show_NodeData(node_id);
            }
            else
            {
                Show_NodeData2(node_text);
            }
        }

        private void Show_NodeData(int id)
        {
            dataGridView1.AllowUserToAddRows = false;
            DataTable dataTable01 = new DataTable();
            dataTable01.Columns.Add("flow", typeof(int));
            dataGridView1.DataSource = dataTable01;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["flow"].HeaderText = "flow号";

            dataGridView2.AllowUserToAddRows = false;
            DataTable dataTable02 = new DataTable();
            dataTable02.Columns.Add("ps", typeof(int));
            dataGridView2.DataSource = dataTable02;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Columns["ps"].HeaderText = "ps";

            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.Visible = true;
            DataTable dataTable03 = new DataTable();
            dataTable03.Columns.Add("I_current", typeof(double));
            dataGridView3.DataSource = dataTable03;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < Data.ele[id - 1].i_flow; i++)
            {
                //添加行数据
                DataRow row = dataTable01.NewRow();
                row["flow"] = Data.ele[id - 1].flow[i];
                dataTable01.Rows.Add(row);
            }
            for (int i = 0; i < Data.ele[id - 1].i_ps; i++)
            {
                //添加行数据
                DataRow row2 = dataTable02.NewRow();
                row2["ps"] = Data.ele[id - 1].ps[i];
                dataTable02.Rows.Add(row2);
            }
            //添加行数据
            DataRow row3 = dataTable03.NewRow();
            row3["I_current"] = Data.ele[id - 1].I_current;
            dataTable03.Rows.Add(row3);
        }

        private void Show_NodeData2(string str)
        {
            dataGridView1.AllowUserToAddRows = false;
            DataTable dataTable01 = new DataTable();
            dataTable01.Columns.Add("flow", typeof(int));
            dataGridView1.DataSource = dataTable01;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["flow"].HeaderText = "flow号";

            dataGridView2.AllowUserToAddRows = false;
            DataTable dataTable02 = new DataTable();
            dataTable02.Columns.Add("ps", typeof(int));
            dataGridView2.DataSource = dataTable02;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Columns["ps"].HeaderText = "ps";

            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.Visible = false;
            DataTable dataTable03 = new DataTable();
            dataTable03.Columns.Add("I_current", typeof(double));
            dataGridView3.DataSource = dataTable03;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (str == "Cathode_separator")
            {
                for (int i = 0; i < Data.Cathode_separator.i_flow; i++)
                {
                    //添加行数据
                    DataRow row = dataTable01.NewRow();
                    row["flow"] = Data.Cathode_separator.flow[i];
                    dataTable01.Rows.Add(row);
                }
                for (int i = 0; i < Data.Cathode_separator.i_ps; i++)
                {
                    //添加行数据
                    DataRow row2 = dataTable02.NewRow();
                    row2["ps"] = Data.Cathode_separator.ps[i];
                    dataTable02.Rows.Add(row2);
                }
            }
            else if (str == "Anode_separator")
            {
                for (int i = 0; i < Data.Anode_separator.i_flow; i++)
                {
                    //添加行数据
                    DataRow row = dataTable01.NewRow();
                    row["flow"] = Data.Anode_separator.flow[i];
                    dataTable01.Rows.Add(row);
                }
                for (int i = 0; i < Data.Cathode_separator.i_ps; i++)
                {
                    //添加行数据
                    DataRow row2 = dataTable02.NewRow();
                    row2["ps"] = Data.Anode_separator.ps[i];
                    dataTable02.Rows.Add(row2);
                }
            }
            else if (str == "Balance_line")
            {
                for (int i = 0; i < Data.Balance_line.i_flow; i++)
                {
                    //添加行数据
                    DataRow row = dataTable01.NewRow();
                    row["flow"] = Data.Balance_line.flow[i];
                    dataTable01.Rows.Add(row);
                }
                for (int i = 0; i < Data.Balance_line.i_ps; i++)
                {
                    //添加行数据
                    DataRow row2 = dataTable02.NewRow();
                    row2["ps"] = Data.Balance_line.ps[i];
                    dataTable02.Rows.Add(row2);
                }
            }
            else if (str == "Cathode_valve")
            {
                for (int i = 0; i < Data.Cathode_valve.i_flow; i++)
                {
                    //添加行数据
                    DataRow row = dataTable01.NewRow();
                    row["flow"] = Data.Cathode_valve.flow[i];
                    dataTable01.Rows.Add(row);
                }
                for (int i = 0; i < Data.Cathode_valve.i_ps; i++)
                {
                    //添加行数据
                    DataRow row2 = dataTable02.NewRow();
                    row2["ps"] = Data.Cathode_valve.ps[i];
                    dataTable02.Rows.Add(row2);
                }
            }
            else if (str == "Anode_valve")
            {
                for (int i = 0; i < Data.Anode_valve.i_flow; i++)
                {
                    //添加行数据
                    DataRow row = dataTable01.NewRow();
                    row["flow"] = Data.Anode_valve.flow[i];
                    dataTable01.Rows.Add(row);
                }
                for (int i = 0; i < Data.Anode_valve.i_ps; i++)
                {
                    //添加行数据
                    DataRow row2 = dataTable02.NewRow();
                    row2["ps"] = Data.Anode_valve.ps[i];
                    dataTable02.Rows.Add(row2);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode sn1 = treeView1.SelectedNode; // 获取选中的节点
            string node_text = sn1.Text;

            if (node_text.Contains("electrolyzer"))
            {
                int node_id = int.Parse(node_text.Substring(node_text.Length - 1, 1));
                save_NodeData(node_id);
            }
            else
            {
                save_NodeData2(node_text);
            }

            Task.Run(() =>
            {
                MessageBox.Show("保存成功！");
            });
        }

        private void save_NodeData(int id)
        {
            for (int i = 0; i < Data.ele[id - 1].i_flow; i++)
            {
                Data.ele[id - 1].flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
            }

            for (int i = 0; i < Data.ele[id - 1].i_ps; i++)
            {
                Data.ele[id - 1].ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
            }

            Data.ele[id - 1].I_current = (double)dataGridView3.Rows[0].Cells[0].Value;
        }

        private void save_NodeData2(string str)
        {
            if (str == "Cathode_separator")
            {
                for (int i = 0; i < Data.Cathode_separator.i_flow; i++)
                {
                    Data.Cathode_separator.flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                }

                for (int i = 0; i < Data.Cathode_separator.i_ps; i++)
                {
                    Data.Cathode_separator.ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
                }
            }
            else if (str == "Anode_separator")
            {
                for (int i = 0; i < Data.Anode_separator.i_flow; i++)
                {
                    Data.Anode_separator.flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                }

                for (int i = 0; i < Data.Anode_separator.i_ps; i++)
                {
                    Data.Anode_separator.ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
                }
            }
            else if (str == "Balance_line")
            {
                for (int i = 0; i < Data.Balance_line.i_flow; i++)
                {
                    Data.Balance_line.flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                }

                for (int i = 0; i < Data.Balance_line.i_ps; i++)
                {
                    Data.Balance_line.ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
                }
            }
            else if (str == "Cathode_valve")
            {
                for (int i = 0; i < Data.Cathode_valve.i_flow; i++)
                {
                    Data.Cathode_valve.flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                }
                
               
                for (int i = 0; i < Data.Cathode_valve.i_ps; i++)
                {
                    Data.Cathode_valve.ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
                }
            }
            else if (str == "Anode_valve")
            {
                for (int i = 0; i < Data.Anode_valve.i_flow; i++)
                {
                    Data.Anode_valve.flow[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                }

                for (int i = 0; i < Data.Anode_valve.i_ps; i++)
                {
                    Data.Anode_valve.ps[i] = (int)dataGridView2.Rows[i].Cells[0].Value;
                }
            }
        }

        private void UserControl11_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }
    }
}
