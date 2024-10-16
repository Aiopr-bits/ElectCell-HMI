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
    public partial class DataPlaybackPage : UserControl
    {
        public DataPlaybackPage()
        {
            InitializeComponent();
            comboBox1LoadData();
        }

        public void comboBox1LoadData()
        {
            comboBox1.Items.Clear();
            if (Data.result.result.Count > 0)
            {
                for (int i = 0; i < Data.result.result.Count; i++)
                {
                    comboBox1.Items.Add(Data.result.result[i][0]);
                }
                comboBox1.SelectedIndex = 0;
            }
        }


        void dataGridView1LoadData(int time)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("变量名",typeof(string));
            dt.Columns.Add("变量值",typeof (string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("读/写", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            for(int i = 0; i < Data.result.result.Count; i++)
            {
                if (Data.result.result[i][0] == time)
                {
                    for (int j = 1; j < Data.result.result[i].Count; j++)
                    {
                        DataRow dr = dt.NewRow();
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

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox1.SelectedItem.ToString(), out int selectedTime))
            {
                dataGridView1LoadData(selectedTime);
            }
        }
    }

}
