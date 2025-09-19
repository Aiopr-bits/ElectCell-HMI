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

namespace ElectCell_HMI.Forms
{
    public partial class guzhang : UserControl
    {
        public guzhang()
        {
            InitializeComponent();

            // 绑定事件
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox2.TextChanged += TextBox2_TextChanged;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            textBox1.ReadOnly = true;

            // 初始化 datagridview1
            InitializeDataGridView1();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            //DataGridView1_CellClick(Handle, new DataGridViewCellEventArgs(0, 0));

            // 初始化 dataGridView2
            InitializeDataGridView2();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = true; 
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
        }

        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "True")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }

        public void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Cells[0].Value = textBox1.Text;
            }
        }

        public void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Cells[1].Value = textBox2.Text;
            }
        }

        public void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Cells[2].Value = checkBox1.Checked ? "True" : "False";
            }
        }

        //初始化dataGridView1数据
        public void InitializeDataGridView1()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("信号名", System.Type.GetType("System.String"));
            dt1.Columns.Add("设置值", System.Type.GetType("System.String"));
            dt1.Columns.Add("使能开启", System.Type.GetType("System.String"));
            dataGridView1.DataSource = dt1;

            //解析injection.csv文件
            string filePath = "injection.csv";
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"文件 {filePath} 不存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (lineNumber == 0)
                        {
                            string[] firstLineValues = line.Split(',');
                            if (int.TryParse(firstLineValues[0].Trim(), out int firstValue))
                            {
                                if (firstValue == -1)
                                {
                                    checkBox2.Checked = true;
                                    tableLayoutPanel3.Visible = false;
                                }
                                else if (firstValue >= 0)
                                {
                                    checkBox2.Checked = false;
                                    tableLayoutPanel3.Visible = true;
                                    numericUpDown1.Value = Convert.ToDecimal(firstValue);
                                }
                            }
                        }

                        lineNumber++;
                        if (lineNumber < 3)
                        {
                            continue;
                        }

                        string[] values = line.Split(',');
                        if (values[2].Trim() != "")
                        {
                            DataRow newRow = dt1.NewRow();
                            if (values[0].Trim() == "flow")
                            {
                                newRow["信号名"] = "Flow(" + values[1].Trim() + ")%x_h2";
                            }
                            else if (values[0].Trim() == "ps")
                            {
                                newRow["信号名"] = "PS(" + values[1].Trim() + ")%n_h2";
                            }
                            else
                            {
                                continue;
                            }
                            newRow["设置值"] = values[2].Trim();
                            newRow["使能开启"] = "True";
                            dt1.Rows.Add(newRow);
                        }

                        if (values[3].Trim() != "")
                        {
                            DataRow newRow = dt1.NewRow();
                            if (values[0].Trim() == "flow")
                            {
                                newRow["信号名"] = "Flow(" + values[1].Trim() + ")%x_o2";
                            }
                            else if (values[0].Trim() == "ps")
                            {
                                newRow["信号名"] = "PS(" + values[1].Trim() + ")%n_o2";
                            }
                            else
                            {
                                continue;
                            }
                            newRow["设置值"] = values[3].Trim();
                            newRow["使能开启"] = "True";
                            dt1.Rows.Add(newRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"解析文件 {filePath} 时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InitializeDataGridView2()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("参数名", System.Type.GetType("System.String"));
            dt2.Columns.Add("描述", System.Type.GetType("System.String"));
            dataGridView2.DataSource = dt2;

            // 创建右键菜单
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem addAsFaultTestItem = new ToolStripMenuItem("添加为故障注入测试");
            addAsFaultTestItem.Click += AddAsFaultTestItem_Click;
            contextMenu.Items.Add(addAsFaultTestItem);
            dataGridView2.ContextMenuStrip = contextMenu;

            //电解槽
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.electrolyticCell[i].flow.Contains(Data.flowParameter.flow[j][0]))
                    {
                        dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                        dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                    }
                }
            }

            //泵
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.pump[i].flow.Contains(Data.flowParameter.flow[j][0]))
                    {
                        dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                        dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                    }
                }
            }

            //阴极分离器
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.cathodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阳极分离器
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.anodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阴极阀门
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.cathodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阳极阀门
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.anodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //平衡管线
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.balancePipe.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("Flow(" + Data.flowParameter.flow[j][0] + ")%x_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //电解槽
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.electrolyticCell[i].ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                        dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                    }
                }
            }

            //泵
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.pump[i].ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                        dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                    }
                }
            }

            //阴极分离器
            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.cathodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阳极分离器
            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.anodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阴极阀门
            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.cathodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //阳极阀门
            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.anodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                }
            }

            //平衡管线
            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.balancePipe.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_h2", "氢气在混合物中所占的物质的量分数");
                    dt2.Rows.Add("PS(" + Data.psParameter.ps[j][0] + ")%n_o2", "氧气在混合物中所占的物质的量分数");
                }
            }
        }

        public void AddAsFaultTestItem_Click(object sender, EventArgs e)
        {
            DataTable dt1 = dataGridView1.DataSource as DataTable;
            if (dt1 == null)
            {
                MessageBox.Show("dataGridView1 的数据源未正确初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 获取所有选中的行
            DataGridViewSelectedRowCollection selectedRows = dataGridView2.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要添加的行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> addedItems = new List<string>();
            List<string> existingItems = new List<string>();

            // 遍历所有选中的行
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                if (selectedRow.Cells[0].Value != null)
                {
                    string parameterName = selectedRow.Cells[0].Value.ToString();

                    // 检查是否已经存在
                    bool exists = dt1.AsEnumerable().Any(row => row.Field<string>("信号名") == parameterName);
                    if (!exists)
                    {
                        DataRow newRow = dt1.NewRow();
                        newRow["信号名"] = parameterName;
                        newRow["设置值"] = "0";
                        newRow["使能开启"] = "True";
                        dt1.Rows.Add(newRow);
                        addedItems.Add(parameterName);
                    }
                    else
                    {
                        existingItems.Add(parameterName);
                    }
                }
            }

            // 显示结果信息
            StringBuilder message = new StringBuilder();
            if (addedItems.Count > 0)
            {
                message.AppendLine($"成功添加了 {addedItems.Count} 个项目到故障测试：");
                foreach (string item in addedItems)
                {
                    message.AppendLine($"  - {item}");
                }
            }
            
            if (existingItems.Count > 0)
            {
                if (message.Length > 0) message.AppendLine();
                message.AppendLine($"以下 {existingItems.Count} 个项目已经存在：");
                foreach (string item in existingItems)
                {
                    message.AppendLine($"  - {item}");
                }
            }

            MessageBox.Show(message.ToString(), "添加结果", MessageBoxButtons.OK, 
                addedItems.Count > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            DataTable dt1 = dataGridView1.DataSource as DataTable;
            if (dt1 == null)
            {
                MessageBox.Show("dataGridView1 的数据源未正确初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = "injection.csv";
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    if (checkBox2.Checked)
                    {
                        sw.WriteLine("-1");
                    }
                    else
                    {
                        sw.WriteLine(numericUpDown1.Value.ToString());
                    }

                    sw.WriteLine("# flow/ps, num, h2, o2");

                    // 遍历 DataTable 的行，写入内容
                    foreach (DataRow row in dt1.Rows)
                    {
                        if (row["使能开启"].ToString() == "True")
                        {
                            string signalName = row["信号名"].ToString();
                            string value = row["设置值"].ToString();

                            // 解析信号名，提取类型和编号
                            string type = signalName.Contains("Flow") ? "flow" : "ps";
                            string num = signalName.Substring(signalName.IndexOf('(') + 1, signalName.IndexOf(')') - signalName.IndexOf('(') - 1);
                            string h2 = signalName.Contains("x_h2") || signalName.Contains("n_h2") ? value : "";
                            string o2 = signalName.Contains("x_o2") || signalName.Contains("n_o2") ? value : "";

                            // 写入一行
                            sw.WriteLine($"{type},{num},{h2},{o2},");
                        }
                    }
                }

                // 读取文件并进行同行合并
                List<string> mergedLines = new List<string>();
                Dictionary<string, (string h2, string o2)> mergeMap = new Dictionary<string, (string h2, string o2)>();

                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNumber++;

                        // 保留第一行和表头
                        if (lineNumber <= 2)
                        {
                            mergedLines.Add(line);
                            continue;
                        }

                        // 解析行内容
                        string[] values = line.Split(',');
                        if (values.Length >= 4)
                        {
                            string key = $"{values[0]},{values[1]}"; // 以 flow/ps 和 num 作为唯一标识
                            string h2 = values[2].Trim();
                            string o2 = values[3].Trim();

                            if (mergeMap.ContainsKey(key))
                            {
                                // 合并 h2 和 o2
                                var existing = mergeMap[key];
                                mergeMap[key] = (string.IsNullOrEmpty(h2) ? existing.h2 : h2,
                                                string.IsNullOrEmpty(o2) ? existing.o2 : o2);
                            }
                            else
                            {
                                mergeMap[key] = (h2, o2);
                            }
                        }
                    }
                }

                // 将合并后的内容写回文件
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (string line in mergedLines.Take(2)) // 写入前两行
                    {
                        sw.WriteLine(line);
                    }

                    foreach (var entry in mergeMap)
                    {
                        string[] keyParts = entry.Key.Split(',');
                        string type = keyParts[0];
                        string num = keyParts[1];
                        string h2 = entry.Value.h2;
                        string o2 = entry.Value.o2;

                        sw.WriteLine($"{type},{num},{h2},{o2},");
                    }
                }

                MessageBox.Show($"故障注入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"写入文件 {filePath} 时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            // 检查是否有选中行
            if (dataGridView1.CurrentRow != null)
            {
                // 获取 dataGridView1 的数据源
                DataTable dt1 = dataGridView1.DataSource as DataTable;
                if (dt1 == null)
                {
                    MessageBox.Show("dataGridView1 的数据源未正确初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 删除选中行
                int rowIndex = dataGridView1.CurrentRow.Index;
                dt1.Rows.RemoveAt(rowIndex);
            }
            else
            {
                MessageBox.Show("请先选择一行进行删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            //移除dataGridView1的所有行
            DataTable dt1 = dataGridView1.DataSource as DataTable;
            if (dt1 != null)
            {
                dt1.Clear();
            }
        }


        public void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox2选中的时候，tableLayoutPanel3隐藏，否则显示
            if (checkBox2.Checked)
            {
                tableLayoutPanel3.Visible = false;
            }
            else
            {
                tableLayoutPanel3.Visible = true;
            }
        }
    }
}

