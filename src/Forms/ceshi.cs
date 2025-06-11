using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ElectCell_HMI.Forms
{
    public partial class ceshi : Form
    {
        public ceshi()
        {
            InitializeComponent();

            //初始化datagridview，为存放数据做准备
            DataTable dt2 = new DataTable();

            //表格，列标题
            dt2.Columns.Add("参数名", System.Type.GetType("System.String"));
            dt2.Columns.Add("起始值", System.Type.GetType("System.String"));
            dt2.Columns.Add("结束值", System.Type.GetType("System.String"));
            dt2.Columns.Add("计算点数", System.Type.GetType("System.String"));

            dt2.Rows.Add("PS(1)%n", (0.1).ToString(),(20).ToString(), (5).ToString());
            dt2.Rows.Add("PS(1)%n_h2", (0.1).ToString(), (0.8).ToString(), (5).ToString());
            dt2.Rows.Add("PS(2)%n", (0.1).ToString(), (20).ToString(), (5).ToString());
            dt2.Rows.Add("PS(1)%n_o2", (0.1).ToString(), (0.8).ToString(), (5).ToString());
            dt2.Rows.Add("PS(1)%p", (100000).ToString(), (200000).ToString(), (5).ToString());

            dataGridView2.DataSource = dt2;

            


        }
    }
}
