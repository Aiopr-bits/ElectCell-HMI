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
    public partial class ControlParameterPage : UserControl
    {
        public ControlParameterPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
        }

        private void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(double));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            DataRow dr = dt.NewRow();
            dr["序号"] = 1;
            dr["变量名"] = "start_time";
            dr["变量值"] = Data.controlParameter.start_time;
            dr["单位"] = "s";
            dr["含义"] = "开始时间";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 2;
            dr["变量名"] = "end_time";
            dr["变量值"] = Data.controlParameter.end_time;
            dr["单位"] = "s";
            dr["含义"] = "结束时间";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 3;
            dr["变量名"] = "delta_t";
            dr["变量值"] = Data.controlParameter.delta_t;
            dr["单位"] = "s";
            dr["含义"] = "时间步长";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 4;
            dr["变量名"] = "cal_current";
            dr["变量值"] = Data.controlParameter.cal_current;
            dr["单位"] = "-";
            dr["含义"] = "是否打开电流";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 5;
            dr["变量名"] = "cal_valve";
            dr["变量值"] = Data.controlParameter.cal_valve;
            dr["单位"] = "-";
            dr["含义"] = "是否打开阀门";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 6;
            dr["变量名"] = "cal_pump";
            dr["变量值"] = Data.controlParameter.cal_pump;
            dr["单位"] = "-";
            dr["含义"] = "是否打开泵";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 7;
            dr["变量名"] = "cal_balance_pipe";
            dr["变量值"] = Data.controlParameter.cal_balance_pipe;
            dr["单位"] = "-";
            dr["含义"] = "是否打开平衡管路";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 8;
            dr["变量名"] = "cal_mini_1";
            dr["变量值"] = Data.controlParameter.cal_mini_1;
            dr["单位"] = "-";
            dr["含义"] = "是否打开mini_1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 9;
            dr["变量名"] = "cal_mini_2";
            dr["变量值"] = Data.controlParameter.cal_mini_2;
            dr["单位"] = "-";
            dr["含义"] = "是否打开mini_2";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 10;
            dr["变量名"] = "use_ff_static";
            dr["变量值"] = Data.controlParameter.use_ff_static;
            dr["单位"] = "-";
            dr["含义"] = "是否使用静态ff";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 11;
            dr["变量名"] = "IsMixed_circleType";
            dr["变量值"] = Data.controlParameter.IsMixed_circleType;
            dr["单位"] = "-";
            dr["含义"] = "是否混合循环类型";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 12;
            dr["变量名"] = "cal_superSat_fickTrans";
            dr["变量值"] = Data.controlParameter.cal_superSat_fickTrans;
            dr["单位"] = "-";
            dr["含义"] = "是否打开超饱和fickTrans";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 13;
            dr["变量名"] = "cal_ShellTube_heatExchanger";
            dr["变量值"] = Data.controlParameter.cal_ShellTube_heatExchanger;
            dr["单位"] = "-";
            dr["含义"] = "是否打开壳管换热器";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["序号"] = 14;
            dr["变量名"] = "cal_Ele_heater";
            dr["变量值"] = Data.controlParameter.cal_Ele_heater;
            dr["单位"] = "-";
            dr["含义"] = "是否打开电加热器";
            dt.Rows.Add(dr);

            for (int i = 0; i < 25; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
        }
    }
}
