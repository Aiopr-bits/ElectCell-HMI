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
    public partial class FlowParameterPage : UserControl
    {
        public FlowParameterPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
        }

        private void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("氢气", typeof(double));
            dt.Columns.Add("氧气", typeof(double));
            dt.Columns.Add("水", typeof(double));
            dt.Columns.Add("管道直径", typeof(double));
            dt.Columns.Add("管道长度", typeof(double));

            for (int i = 0; i < Data.flowParameter.flow.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = Data.flowParameter.flow[i][0];
                dr["氢气"] = Data.flowParameter.flow[i][1];
                dr["氧气"] = Data.flowParameter.flow[i][2];
                dr["水"] = Data.flowParameter.flow[i][3];
                dr["管道直径"] = Data.flowParameter.flow[i][4];
                dr["管道长度"] = Data.flowParameter.flow[i][5];
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
        }



    }
}
