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
    public partial class huifang : Form
    {
        public huifang()
        {
            InitializeComponent();

            //初始化datagridview，为存放数据做准备
            DataTable dt1 = new DataTable();

            //表格，列标题
            dt1.Columns.Add("信号名", System.Type.GetType("System.String"));
            dt1.Columns.Add("描述", System.Type.GetType("System.String"));
            dt1.Columns.Add("图表", System.Type.GetType("System.String"));
            //dt1.Columns.Add("计算点数", System.Type.GetType("System.String"));

            dt1.Rows.Add("PS(1)%n","1","图表1");
            dt1.Rows.Add("PS(1)%n_h2", "2", "");
            dt1.Rows.Add("PS(2)%n", "3", "");
            dt1.Rows.Add("PS(1)%n_o2", "4", "");
            dt1.Rows.Add("PS(1)%p","5","图表2");
            dt1.Rows.Add("PS(2)%p","6","");
            dt1.Rows.Add("PS(3)%n", "7", "");
            dt1.Rows.Add("PS(3)%n_h2", "8", "");
            dt1.Rows.Add("PS(4)%n", "9", "图表3");
            dt1.Rows.Add("PS(4)%n_o2", "10", "");
            dt1.Rows.Add("PS(3)%p", "11", "");
            dt1.Rows.Add("PS(4)%p", "12", "");

            dataGridView1.DataSource = dt1;

            textBox1.Text = "D:\\work_202302\\C#\\ElectCell-HMI\\case2\\output.data\\result.csv";
            
        }
    }
}
