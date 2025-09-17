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
        public MainWindow mainWindow;

        public SimulationResultPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainWindow.TimerTicked += MainWindow_TimerTicked;
        }

        public void MainWindow_TimerTicked(object sender, EventArgs e)
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
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            int rowIndex = 1;
            for (int i = 0; i < Data.result.result.Count; i++)
            {
                if (Data.result.result[i][0] == time)
                {
                    Dictionary<string, string> variableNames = new Dictionary<string, string>
                    {
                        { "ec 1-1n", "电解槽1阴极物质量" },
                        { "ec 1-1n-h2", "电解槽1阴极氢气物质量" },
                        { "ec 1-1n-o2", "电解槽1阴极氧气物质量" },
                        { "ec 1-2n", "电解槽1阳极物质量" },
                        { "ec 1-2n-h2", "电解槽1阳极氢气物质量" },
                        { "ec 1-2n-o2", "电解槽1阳极氧气物质量" },
                        { "ec 2-1n", "电解槽2阴极物质量" },
                        { "ec 2-1n-h2", "电解槽2阴极氢气物质量" },
                        { "ec 2-1n-o2", "电解槽2阴极氧气物质量" },
                        { "ec 2-2n", "电解槽2阳极物质量" },
                        { "ec 2-2n-h2", "电解槽2阳极氢气物质量" },
                        { "ec 2-2n-o2", "电解槽2阳极氧气物质量" },
                        { "ec 3-1n", "电解槽3阴极物质量" },
                        { "ec 3-1n-h2", "电解槽3阴极氢气物质量" },
                        { "ec 3-1n-o2", "电解槽3阴极氧气物质量" },
                        { "ec 3-2n", "电解槽3阳极物质量" },
                        { "ec 3-2n-h2", "电解槽3阳极氢气物质量" },
                        { "ec 3-2n-o2", "电解槽3阳极氧气物质量" },
                        { "ec 4-1n", "电解槽4阴极物质量" },
                        { "ec 4-1n-h2", "电解槽4阴极氢气物质量" },
                        { "ec 4-1n-o2", "电解槽4阴极氧气物质量" },
                        { "ec 4-2n", "电解槽4阳极物质量" },
                        { "ec 4-2n-h2", "电解槽4阳极氢气物质量" },
                        { "ec 4-2n-o2", "电解槽4阳极氧气物质量" },
                        { "cs 1n", "阴极分离器液空间物质量" },
                        { "cs 1n-h2", "阴极分离器氢气物质量" },
                        { "cs 1n-o2", "阴极分离器氧气物质量" },
                        { "cs 1-l_g", "阴极分离器液体高度" },
                        { "cs 2-p", "阴极分离器气空间压力" },
                        { "cs 2n", "阴极分离器气空间物质量" },
                        { "cs 2n-h2", "阴极分离器气空间氢气物质量" },
                        { "cs 2n-o2", "阴极分离器气空间氧气物质量" },
                        { "as 1n", "阳极分离器液空间物质量" },
                        { "as 1n-h2", "阳极分离器氢气物质量" },
                        { "as 1n-o2", "阳极分离器氧气物质量" },
                        { "as 1-l_g", "阳极分离器液体高度" },
                        { "as 2-p", "阳极分离器气空间压力" },
                        { "as 2n", "阳极分离器气空间物质量" },
                        { "as 2n-h2", "阳极分离器气空间氢气物质量" },
                        { "as 2n-o2", "阳极分离器气空间氧气物质量" },
                        { "bp 1n", "平衡管线物质量" },
                        { "bp 1n-h2", "平衡管线氢气物质量" },
                        { "bp 1n-o2", "平衡管线氧气物质量" }
                    };

                    for (int j = 1; j < Data.result.result[i].Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["序号"] = rowIndex++;
                        string variableName = Data.result.header[j];
                        variableName = variableName.Trim();
                        dr["变量名"] = variableName;
                        dr["变量值"] = Data.result.result[i][j];

                        if (variableNames.ContainsKey(variableName))
                        {
                            dr["含义"] = variableNames[variableName];
                            if (variableName.Contains("h2") || variableName.Contains("o2"))
                            {
                                dr["单位"] = "mol";
                            }
                            else if (variableName.Contains("p"))
                            {
                                dr["单位"] = "Pa";
                            }
                            else if (variableName.Contains("l_g"))
                            {
                                dr["单位"] = "m";
                            }
                            else
                            {
                                dr["单位"] = "kg";
                            }
                        }
                        else
                        {
                            dr["含义"] = "未知参数";
                            dr["单位"] = "未知单位";
                        }

                        dt.Rows.Add(dr);
                    }

                    this.label2.Text = "当前时间："+time.ToString()+"   ";
                    this.label2.TextAlign = ContentAlignment.MiddleLeft;

                    break;
                }
            }
            dataGridView1.DataSource = dt;
        }
    }

}
