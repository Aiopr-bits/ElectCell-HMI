﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.UserControls
{
    public partial class UserControl6 : UserControl
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public UserControl6()
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
            dataTable01.Columns.Add("Di", typeof(double));
            dataTable01.Columns.Add("L", typeof(double));
            dataTable01.Columns.Add("v_t", typeof(double));

            // 设置DataGridView的DataSource  
            dataGridView1.DataSource = dataTable01;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = true;

            // 设置列名  
            dataGridView1.Columns["num"].HeaderText = "序号";
            dataGridView1.Columns["x_h2"].HeaderText = "氢气";
            dataGridView1.Columns["x_o2"].HeaderText = "氧气";
            dataGridView1.Columns["x_h2o"].HeaderText = "水";
            dataGridView1.Columns["Di"].HeaderText = "管道直径";
            dataGridView1.Columns["L"].HeaderText = "管道长度";
            dataGridView1.Columns["v_t"].HeaderText = "体积流速";

            for (int i = 0; i < Data.n_flow; i++)
            {
                //添加行数据
                DataRow row = dataTable01.NewRow();
                row["num"] = Data.flow[i].num;
                row["x_h2"] = Data.flow[i].x_h2;
                row["x_o2"] = Data.flow[i].x_o2;
                row["x_h2o"] = Data.flow[i].x_h2o;
                row["Di"] = Data.flow[i].Di;
                row["L"] = Data.flow[i].L;
                row["v_t"] = Data.flow[i].v_t;

                dataTable01.Rows.Add(row);
            }
        }

        private void UserControl6_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = dataGridView1.NewRowIndex;
            if(id > 42)
            {
                MessageBox.Show("已超出允许新增数据的最大行数，最多只允许包含42行数据！");
                GetDatabase();
                return;
            }
            Data.n_flow = id;
            for (int i = 0; i < id; i++)
            {
                Data.flow[i] = new FlowData();
            }

            for (int i = 0; i < id; i++)
            {
                Data.flow[i].num = (int)dataGridView1.Rows[i].Cells[0].Value;
                Data.flow[i].x_h2 = (double)dataGridView1.Rows[i].Cells[1].Value;
                Data.flow[i].x_o2 = (double)dataGridView1.Rows[i].Cells[2].Value;
                Data.flow[i].x_h2o = (double)dataGridView1.Rows[i].Cells[3].Value;
                Data.flow[i].Di = (double)dataGridView1.Rows[i].Cells[4].Value;
                Data.flow[i].L = (double)dataGridView1.Rows[i].Cells[5].Value;
                Data.flow[i].v_t = (double)dataGridView1.Rows[i].Cells[6].Value;
            }

            //点了保存按钮进⼊
            Data.saveFile = Path.Combine(Data.exePath, Data.case_name, "data_input.csv");
            Data.GUI2CSV(@Data.saveFile);

            GetDatabase();

            Task.Run(() =>
            {
                MessageBox.Show("保存成功！");
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = true;

            DialogResult dr = MessageBox.Show("确定要删除吗？", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int a = dataGridView1.CurrentCell.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[a];
                dataGridView1.Rows.Remove(row);

                FlowData[] flow2 = new FlowData[Data.n_flow_max - 1];
                int index = 0;
                for (int i = 0; i < Data.n_flow; i++)
                {
                    if (i != a)
                    {
                        flow2[index] = Data.flow[i];
                        index++;
                    }
                }
                Data.flow = flow2;
                Data.n_flow = Data.n_flow - 1;
                GetDatabase();
                Task.Run(() =>
                {
                    MessageBox.Show("删除成功！");
                });
            }
            else
            {
                MessageBox.Show("取消删除");
            }
        }
    }
}
