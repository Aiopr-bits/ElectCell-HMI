using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            //初始化datagridview，为存放数据做准备
            DataTable dt1 = new DataTable();

            //表格，列标题
            dt1.Columns.Add("信号名", System.Type.GetType("System.String"));
            dt1.Columns.Add("设置值", System.Type.GetType("System.String"));
            dt1.Columns.Add("使能开启", System.Type.GetType("System.String"));
            dt1.Columns.Add("描述", System.Type.GetType("System.String"));

            dt1.Rows.Add("PS(1)%n", (0.1).ToString(), "True", "阴极摩尔量");
            dt1.Rows.Add("PS(1)%n_h2", (0.1).ToString(), "True", "阴极氢气摩尔量");
            dt1.Rows.Add("PS(2)%n", (0.1).ToString(), "True", "阳极摩尔量");
            dt1.Rows.Add("PS(1)%n_o2", (0.1).ToString(), "True", "阳极氢气摩尔量");
            //dt1.Rows.Add("PS(1)%p", (100000).ToString(), (200000).ToString(), (5).ToString());

            dataGridView1.DataSource = dt1;

            //初始化datagridview，为存放数据做准备
            DataTable dt2 = new DataTable();

            //表格，列标题
            dt2.Columns.Add("参数名", System.Type.GetType("System.String"));
            //dt2.Columns.Add("起始值", System.Type.GetType("System.String"));
            //dt2.Columns.Add("结束值", System.Type.GetType("System.String"));
            //dt2.Columns.Add("计算点数", System.Type.GetType("System.String"));

            dt2.Rows.Add("PS(1)%n");
            dt2.Rows.Add("PS(1)%n_h2");
            dt2.Rows.Add("PS(2)%n");
            dt2.Rows.Add("PS(1)%n_o2");
            dt2.Rows.Add("PS(1)%p");
            dt2.Rows.Add("PS(2)%p");
            dt2.Rows.Add("PS(3)%n");
            dt2.Rows.Add("PS(3)%n_h2");
            dt2.Rows.Add("PS(4)%n");
            dt2.Rows.Add("PS(4)%n_o2");
            dt2.Rows.Add("PS(3)%p");
            dt2.Rows.Add("PS(4)%p");

            dataGridView2.DataSource = dt2;

            textBox1.Text = "PS(1)%n";
            textBox2.Text = "1.5";
        }
    }
}
