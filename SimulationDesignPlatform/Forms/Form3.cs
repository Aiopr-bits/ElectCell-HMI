using Microsoft.VisualBasic.FileIO;
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

namespace SimulationDesignPlatform.Forms
{
    public partial class Form3 : Form
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public Form3()
        {
            InitializeComponent();

			#region  初始化控件缩放
			x = Width;
			y = Height;
			setTag(this);
			#endregion
		}

		#region 初始化控件缩放

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

		#endregion

        private void Form3_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取选择的选项
            string selectedOption = comboBox1.SelectedItem.ToString();

            // 更新标签文本
            comboBox1.Text = selectedOption;

            dataGridView1.AutoGenerateColumns = false;

            if (selectedOption == "图表-1")
            {
                //清空列勾选框的所有选项
                checkedListBox1.Items.Clear();
                // 添加列勾选项
                if (Data.data1.Count > 0)
                {
                    //int columnCount = data1[0].Count;
                    //for (int i = 0; i < columnCount; i++)
                    //{
                    //    dataGridView1.Columns.Add("Column" + i, "Column" + i);
                    //}
                    dataGridView1.ColumnCount = Data.data1[0].Count;
                    for (int k = 0; k < Data.data1[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data1[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data1_check.Length; i++)
                    {
                        if (Data.data1_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
                // 将数据绑定到DataGridView中
                //foreach (List<string> row in data1)
                //{
                //    dataGridView1.Rows.Add(row.ToArray());
                //}
                for (int i = 1; i < Data.data1.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data1[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[0] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[0];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[0].ToString();

            }
            else if (selectedOption == "图表-2")
            {
                checkedListBox1.Items.Clear();

                // 添加列勾选项
                if (Data.data2.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data2[0].Count;
                    for (int k = 0; k < Data.data2[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data2[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data2_check.Length; i++)
                    {
                        if (Data.data2_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 1; i < Data.data2.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data2[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[1] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[1];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[1].ToString();
            }
            else if (selectedOption == "图表-3")
            {
                checkedListBox1.Items.Clear();

                // 添加列勾选项
                if (Data.data3.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data3[0].Count;
                    for (int k = 0; k < Data.data3[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data3[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data3_check.Length; i++)
                    {
                        if (Data.data3_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 1; i < Data.data3.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data3[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[2] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[2];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[2].ToString();
            }
            else if (selectedOption == "图表-4")
            {
                checkedListBox1.Items.Clear();

                // 添加列勾选项
                if (Data.data4.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data4[0].Count;
                    for (int k = 0; k < Data.data4[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data4[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data4_check.Length; i++)
                    {
                        if (Data.data4_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 1; i < Data.data4.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data4[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[3] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[3];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[3].ToString();
            }
            else if (selectedOption == "图表-5")
            {
                checkedListBox1.Items.Clear();

                // 添加列勾选项
                if (Data.data5.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data5[0].Count;
                    for (int k = 0; k < Data.data5[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data5[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data5_check.Length; i++)
                    {
                        if (Data.data5_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 1; i < Data.data5.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data5[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[4] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[4];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[4].ToString();
            }
            else if (selectedOption == "图表-6")
            {
                checkedListBox1.Items.Clear();

                // 添加列勾选项
                if (Data.data6.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data6[0].Count;
                    for (int k = 0; k < Data.data6[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data6[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                    for (int i = 0; i < Data.data6_check.Length; i++)
                    {
                        if (Data.data6_check[i])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 1; i < Data.data6.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data6[i].ToArray());
                }
                //显示设置的时间
                if (Data.check_time[5] > DateTime.MinValue)
                {
                    dateTimePicker1.Value = Data.check_time[5];
                }
                //显示设置的阈值
                textBox1.Text = Data.yz_num[5].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBox1.SelectedItem.ToString();
            if (selectedOption == "图表-1")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[0] = 0;
                }
                else
                {
                    Data.yz_num[0] = int.Parse(textBox1.Text);
                }

                Data.check_time[0] = dateTimePicker1.Value;

                Data.data1_check = new bool[Data.data1[0].Count];
                for (int i = 0; i < Data.data1_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data1_check[i] = true;
                    }
                    else
                    {
                        Data.data1_check[i] = false;
                    }
                }
            }
            else if (selectedOption == "图表-2")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[1] = 0;
                }
                else
                {
                    Data.yz_num[1] = int.Parse(textBox1.Text);
                }

                Data.check_time[1] = dateTimePicker1.Value;

                Data.data2_check = new bool[Data.data2[0].Count];
                for (int i = 0; i < Data.data2_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data2_check[i] = true;
                    }
                    else
                    {
                        Data.data2_check[i] = false;
                    }
                }
            }
            else if (selectedOption == "图表-3")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[2] = 0;
                }
                else
                {
                    Data.yz_num[2] = int.Parse(textBox1.Text);
                }

                Data.check_time[2] = dateTimePicker1.Value;

                Data.data3_check = new bool[Data.data3[0].Count];
                for (int i = 0; i < Data.data3_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data3_check[i] = true;
                    }
                    else
                    {
                        Data.data3_check[i] = false;
                    }
                }
            }
            else if (selectedOption == "图表-4")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[3] = 0;
                }
                else
                {
                    Data.yz_num[3] = int.Parse(textBox1.Text);
                }

                Data.check_time[3] = dateTimePicker1.Value;

                Data.data4_check = new bool[Data.data4[0].Count];
                for (int i = 0; i < Data.data4_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data4_check[i] = true;
                    }
                    else
                    {
                        Data.data4_check[i] = false;
                    }
                }
            }
            else if (selectedOption == "图表-5")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[4] = 0;
                }
                else
                {
                    Data.yz_num[4] = int.Parse(textBox1.Text);
                }

                Data.check_time[4] = dateTimePicker1.Value;

                Data.data5_check = new bool[Data.data5[0].Count];
                for (int i = 0; i < Data.data5_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data5_check[i] = true;
                    }
                    else
                    {
                        Data.data5_check[i] = false;
                    }
                }
            }
            else if (selectedOption == "图表-6")
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    Data.yz_num[5] = 0;
                }
                else
                {
                    Data.yz_num[5] = int.Parse(textBox1.Text);
                }

                Data.check_time[5] = dateTimePicker1.Value;

                Data.data6_check = new bool[Data.data6[0].Count];
                for (int i = 0; i < Data.data6_check.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        Data.data6_check[i] = true;
                    }
                    else
                    {
                        Data.data6_check[i] = false;
                    }
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object selectedOption = comboBox1.SelectedItem;
            if (selectedOption == null)
            {
                MessageBox.Show("请先选择图表！");
                return;
            }
            if (selectedOption.ToString() == "图表-1")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data1.Add(row);
                    }
                }

                // 输出读取的数据
                //foreach (List<string> row in Data.data1)
                //{
                //    foreach (string cell in row)
                //    {
                //        Console.Write(cell + "\t");
                //    }
                //    Console.WriteLine();
                //}

                MessageBox.Show("图表-1数据导入成功！");

                if (Data.data1.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data1[0].Count;
                    for (int k = 0; k < Data.data1[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data1[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data1.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data1[i].ToArray());
                }

            }
            else if (selectedOption.ToString() == "图表-2")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data2.Add(row);
                    }
                }
                MessageBox.Show("图表-2数据导入成功！");

                if (Data.data2.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data2[0].Count;
                    for (int k = 0; k < Data.data2[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data2[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data2.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data2[i].ToArray());
                }
            }
            else if (selectedOption.ToString() == "图表-3")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data3.Add(row);
                    }
                }
                MessageBox.Show("图表-3数据导入成功！");

                if (Data.data3.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data3[0].Count;
                    for (int k = 0; k < Data.data3[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data3[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data3.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data3[i].ToArray());
                }
            }
            else if (selectedOption.ToString() == "图表-4")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data4.Add(row);
                    }
                }
                MessageBox.Show("图表-4数据导入成功！");

                if (Data.data4.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data4[0].Count;
                    for (int k = 0; k < Data.data4[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data4[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data4.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data4[i].ToArray());
                }
            }
            else if (selectedOption.ToString() == "图表-5")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data5.Add(row);
                    }
                }
                MessageBox.Show("图表-5数据导入成功！");

                if (Data.data5.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data5[0].Count;
                    for (int k = 0; k < Data.data5[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data5[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data5.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data5[i].ToArray());
                }
            }
            else if (selectedOption.ToString() == "图表-6")
            {
                string filepath = Path.Combine(Data.exePath, Data.case_name, "output.data", "case_1.csv");
                using (TextFieldParser parser = new TextFieldParser(filepath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        List<string> row = new List<string>(fields);
                        Data.data6.Add(row);
                    }
                }
                MessageBox.Show("图表-6数据导入成功！");

                if (Data.data6.Count > 0)
                {
                    dataGridView1.ColumnCount = Data.data6[0].Count;
                    for (int k = 0; k < Data.data6[0].Count; k++)
                    {
                        dataGridView1.Columns[k].Name = Data.data6[0][k];
                        checkedListBox1.Items.Add(dataGridView1.Columns[k].Name);
                    }
                }

                for (int i = 1; i < Data.data6.Count; i++)
                {
                    dataGridView1.Rows.Add(Data.data6[i].ToArray());
                }
            }
        }
    }
}
