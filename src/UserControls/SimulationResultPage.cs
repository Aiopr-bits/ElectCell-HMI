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
    public partial class SimulationResultPage : UserControl
    {
        private MainWindow mainWindow;

        public SimulationResultPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainWindow.TimerTicked += MainWindow_TimerTicked;
        }

        private void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            DataGridView1LoadData();
        }

        public void DataGridView1LoadData()
        {
            if (Data.result.result == null || Data.result.result.Count == 0)
            {
                return;
            }

            int time = (int)Data.result.result[Data.result.result.Count - 1][0];

            DataTable dt = new DataTable();
            dt.Columns.Add("当前时间步", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("读/写", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            for (int i = 0; i < Data.result.result.Count; i++)
            {
                if (Data.result.result[i][0] == time)
                {
                    for (int j = 1; j < Data.result.result[i].Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["当前时间步"] = time;
                        dr["变量名"] = Data.result.header[j];
                        dr["变量值"] = Data.result.result[i][j];
                        dr["单位"] = "m/s";
                        dr["读/写"] = "RW";
                        dr["含义"] = "参数含义";
                        dt.Rows.Add(dr);
                    }
                    break;
                }
            }
            dataGridView1.DataSource = dt;
        }
    }

}
