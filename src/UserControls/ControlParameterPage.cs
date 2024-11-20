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

        public void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(double));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            // 添加控制参数数据
            AddControlParameter(dt, 1, "start_time", Data.controlParameter.start_time, "s", "开始时间");
            AddControlParameter(dt, 2, "end_time", Data.controlParameter.end_time, "s", "结束时间");
            AddControlParameter(dt, 3, "delta_t", Data.controlParameter.delta_t, "s", "时间步长");
            AddControlParameter(dt, 4, "cal_current", Convert.ToDouble(Data.controlParameter.cal_current), "-", "是否打开电流");
            AddControlParameter(dt, 5, "cal_valve", Convert.ToDouble(Data.controlParameter.cal_valve), "-", "是否打开阀门");
            AddControlParameter(dt, 6, "cal_pump", Convert.ToDouble(Data.controlParameter.cal_pump), "-", "是否打开泵");
            AddControlParameter(dt, 7, "cal_balance_pipe", Convert.ToDouble(Data.controlParameter.cal_balance_pipe), "-", "是否打开平衡管路");
            AddControlParameter(dt, 8, "cal_mini_1", Convert.ToDouble(Data.controlParameter.cal_mini_1), "-", "是否进行电解槽阴阳极渗透计算");
            AddControlParameter(dt, 9, "cal_mini_2", Convert.ToDouble(Data.controlParameter.cal_mini_2), "-", "是否进行气体沸腾率计算");
            AddControlParameter(dt, 10, "use_ff_static", Convert.ToDouble(Data.controlParameter.use_ff_static), "-", "是否指定管道摩擦阻力系数");
            AddControlParameter(dt, 11, "IsMixed_circleType", Convert.ToDouble(Data.controlParameter.IsMixed_circleType), "-", "是否采用混合模式计算泵模型");
            AddControlParameter(dt, 12, "cal_superSat_fickTrans", Convert.ToDouble(Data.controlParameter.cal_superSat_fickTrans), "-", "是否计算过饱和氢气");
            AddControlParameter(dt, 13, "cal_ShellTube_heatExchanger", Convert.ToDouble(Data.controlParameter.cal_ShellTube_heatExchanger), "-", "是否计算膜换热");
            AddControlParameter(dt, 14, "cal_Ele_heater", Convert.ToDouble(Data.controlParameter.cal_Ele_heater), "-", "是否计算换热器模型");

            // 添加空行
            for (int i = 0; i < 25; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
        }

        public void AddControlParameter(DataTable dt, int index, string name, double value, string unit, string description)
        {
            DataRow dr = dt.NewRow();
            dr["序号"] = index;
            dr["变量名"] = name;
            dr["变量值"] = value;
            dr["单位"] = unit;
            dr["含义"] = description;
            dt.Rows.Add(dr);
        }

        public void SaveData()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["变量名"].Value == null || string.IsNullOrWhiteSpace(row.Cells["变量名"].Value.ToString()) ||
                    row.Cells["变量值"].Value == null || string.IsNullOrWhiteSpace(row.Cells["变量值"].Value.ToString()))
                {
                    return;
                }

                string variableName = row.Cells["变量名"].Value.ToString();
                double variableValue = Convert.ToDouble(row.Cells["变量值"].Value);

                switch (variableName)
                {
                    case "start_time":
                        Data.controlParameter.start_time = variableValue;
                        break;
                    case "end_time":
                        Data.controlParameter.end_time = variableValue;
                        break;
                    case "delta_t":
                        Data.controlParameter.delta_t = variableValue;
                        break;
                    case "cal_current":
                        Data.controlParameter.cal_current = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_valve":
                        Data.controlParameter.cal_valve = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_pump":
                        Data.controlParameter.cal_pump = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_balance_pipe":
                        Data.controlParameter.cal_balance_pipe = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_mini_1":
                        Data.controlParameter.cal_mini_1 = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_mini_2":
                        Data.controlParameter.cal_mini_2 = Convert.ToBoolean(variableValue);
                        break;
                    case "use_ff_static":
                        Data.controlParameter.use_ff_static = Convert.ToBoolean(variableValue);
                        break;
                    case "IsMixed_circleType":
                        Data.controlParameter.IsMixed_circleType = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_superSat_fickTrans":
                        Data.controlParameter.cal_superSat_fickTrans = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_ShellTube_heatExchanger":
                        Data.controlParameter.cal_ShellTube_heatExchanger = Convert.ToBoolean(variableValue);
                        break;
                    case "cal_Ele_heater":
                        Data.controlParameter.cal_Ele_heater = Convert.ToBoolean(variableValue);
                        break;
                }
            }
        }



    }
}
