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
    public partial class UserControl12 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl12()
        {
            InitializeComponent();
            GetDatabase();

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

        private void GetDatabase()
        {
            if (Data.caseUsePath == "" || Data.caseUsePath == null)
            {
                MessageBox.Show("请先指定工作目录！");
                return;
            }
            DataTable dataTable01 = new DataTable();

            dataTable01.Columns.Add("num", typeof(int));
            dataTable01.Columns.Add("x_h2", typeof(double));
            dataTable01.Columns.Add("x_o2", typeof(double));
            dataTable01.Columns.Add("x_h2o", typeof(double));

            // 设置DataGridView的DataSource  
            dataGridView1.DataSource = dataTable01;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置列名  
            dataGridView1.Columns["num"].HeaderText = "num";
            dataGridView1.Columns["x_h2"].HeaderText = "x_h2";
            dataGridView1.Columns["x_o2"].HeaderText = "x_o2";
            dataGridView1.Columns["x_h2o"].HeaderText = "x_h2o";

            for (int i = 0; i < Data.n_flow; i++)
            {
                //添加行数据
                DataRow row = dataTable01.NewRow();
                row["num"] = Data.flow[i].num;
                row["x_h2"] = Data.flow[i].x_h2;
                row["x_o2"] = Data.flow[i].x_o2;
                row["x_h2o"] = Data.flow[i].x_h2o;

                dataTable01.Rows.Add(row);
            }

            DataTable dataTable02 = new DataTable();

            dataTable02.Columns.Add("num", typeof(int));
            dataTable02.Columns.Add("n", typeof(double));
            dataTable02.Columns.Add("v", typeof(double));
            dataTable02.Columns.Add("p", typeof(double));
            dataTable02.Columns.Add("l_l", typeof(double));
            dataTable02.Columns.Add("l_g", typeof(double));
            dataTable02.Columns.Add("x_h2", typeof(double));
            dataTable02.Columns.Add("x_o2", typeof(double));
            dataTable02.Columns.Add("x_h2o", typeof(double));

            // 设置DataGridView的DataSource  
            dataGridView2.DataSource = dataTable02;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置列名  
            dataGridView2.Columns["num"].HeaderText = "num";
            dataGridView2.Columns["n"].HeaderText = "n";
            dataGridView2.Columns["v"].HeaderText = "v";
            dataGridView2.Columns["p"].HeaderText = "p";
            dataGridView2.Columns["l_l"].HeaderText = "l_l";
            dataGridView2.Columns["l_g"].HeaderText = "l_g";
            dataGridView2.Columns["x_h2"].HeaderText = "x_h2";
            dataGridView2.Columns["x_o2"].HeaderText = "x_o2";
            dataGridView2.Columns["x_h2o"].HeaderText = "x_h2o";

            for (int i = 0; i < Data.n_ps; i++)
            {
                //添加行数据
                DataRow row = dataTable02.NewRow();
                row["num"] = Data.ps[i].num;
                row["n"] = Data.ps[i].n;
                row["v"] = Data.ps[i].v;
                row["p"] = Data.ps[i].p;
                row["l_l"] = Data.ps[i].l_l;
                row["l_g"] = Data.ps[i].l_g;
                row["n_h2"] = Data.ps[i].n_h2;
                row["n_o2"] = Data.ps[i].n_o2;
                row["v_t"] = Data.ps[i].v_t;

                dataTable02.Rows.Add(row);
            }
        }

        private void UserControl12_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }
    }
}
