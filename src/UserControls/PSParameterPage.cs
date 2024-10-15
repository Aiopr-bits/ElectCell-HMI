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
    public partial class PSParameterPage : UserControl
    {
        public PSParameterPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
        }

        public void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("物质的量", typeof(string));
            dt.Columns.Add("摩尔体积", typeof(string));
            dt.Columns.Add("压强", typeof(string));
            dt.Columns.Add("液体高度", typeof(string));
            dt.Columns.Add("气体高度", typeof(string));
            dt.Columns.Add("氢气", typeof(string));
            dt.Columns.Add("氧气", typeof(string));
            //dt.Columns.Add("水", typeof(string));

            for (int i = 0; i < Data.psParameter.ps.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = Data.psParameter.ps[i][0];
                dr["物质的量"] = Data.psParameter.ps[i][1];
                dr["摩尔体积"] = Data.psParameter.ps[i][2];
                dr["压强"] = Data.psParameter.ps[i][3];
                dr["液体高度"] = Data.psParameter.ps[i][4];
                dr["气体高度"] = Data.psParameter.ps[i][5];
                dr["氢气"] = Data.psParameter.ps[i][6];
                dr["氧气"] = Data.psParameter.ps[i][7];
                //dr["水"] = Data.psParameter.ps[i][8];
                dt.Rows.Add(dr);
            }

            for (int i = 0; i < 20; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }


            dataGridView1.DataSource = dt;
        }
    }
}
