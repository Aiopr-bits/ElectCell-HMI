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
    public partial class UserControl14 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl14()
        {
            InitializeComponent();
			//GetDatabase();

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

        private void UserControl14_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.AllowUserToAddRows = true;
            //int id = dataGridView1.NewRowIndex;
            //Data.n_case = id;

            //for (int i = 0; i < id; i++)
            //{
            //    Data.case_data[i].icase = (int)dataGridView1.Rows[i].Cells[0].Value;
            //    Data.case_data[i].line_case = (int)dataGridView1.Rows[i].Cells[1].Value;
            //    Data.case_data[i].t_case = (double)dataGridView1.Rows[i].Cells[2].Value;
            //    Data.case_data[i].p_case = (double)dataGridView1.Rows[i].Cells[3].Value;
            //    Data.case_data[i].m_case = (double)dataGridView1.Rows[i].Cells[4].Value;
            //}
            //GetDatabase();
            //Task.Run(() =>
            //{
            //    MessageBox.Show("保存成功！");
            //});
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.AllowUserToDeleteRows = true;

            //DialogResult dr = MessageBox.Show("确定要删除吗？", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr == DialogResult.Yes)
            //{
            //    int a = dataGridView1.CurrentCell.RowIndex;
            //    DataGridViewRow row = dataGridView1.Rows[a];
            //    dataGridView1.Rows.Remove(row);
            //    Console.WriteLine("a = {0} ", a);

            //    CaseData[] case2 = new CaseData[Data.n_case_max - 1];
            //    int index = 0;
            //    for (int i = 0; i < Data.n_case; i++)
            //    {
            //        if (i != a)
            //        {
            //            case2[index] = Data.case_data[i];
            //            index++;
            //        }
            //    }
            //    Data.case_data = case2;
            //    Data.n_case = Data.n_case - 1;
            //    GetDatabase();
            //    Task.Run(() =>
            //    {
            //        MessageBox.Show("删除成功！");
            //    });
            //}
            //else
            //{
            //    MessageBox.Show("取消删除");
            //}
        }

        private void GetDatabase()
        {
            DataTable dataTable01 = new DataTable();

            dataTable01.Columns.Add("icase", typeof(int));
            dataTable01.Columns.Add("line_case", typeof(int));
            dataTable01.Columns.Add("t_case", typeof(double));
            dataTable01.Columns.Add("p_case", typeof(double));
            dataTable01.Columns.Add("m_case", typeof(double));

            // 设置DataGridView的DataSource  
            dataGridView1.DataSource = dataTable01;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置列名  
            dataGridView1.Columns["icase"].HeaderText = "工况";
            dataGridView1.Columns["line_case"].HeaderText = "流股";
            dataGridView1.Columns["t_case"].HeaderText = "温度";
            dataGridView1.Columns["p_case"].HeaderText = "压力";
            dataGridView1.Columns["m_case"].HeaderText = "流量";

            for (int i = 0; i < Data.n_case; i++)
            {
                //添加行数据
                DataRow row = dataTable01.NewRow();
                row["icase"] = Data.case_data[i].icase;
                row["line_case"] = Data.case_data[i].line_case;
                row["t_case"] = Data.case_data[i].t_case;
                row["p_case"] = Data.case_data[i].p_case;
                row["m_case"] = Data.case_data[i].m_case;

                dataTable01.Rows.Add(row);
            }
        }
    }
}
