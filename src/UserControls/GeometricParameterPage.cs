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
    public partial class GeometricParameterPage : UserControl
    {
        public GeometricParameterPage()
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

            // 添加几何参数数据
            AddGeometryParameter(dt, 1, "L_ca2se", Data.geometryParameter.L_ca2se, "m", "阳极到隔膜的距离");
            AddGeometryParameter(dt, 2, "L_an2se", Data.geometryParameter.L_an2se, "m", "阴极到隔膜的距离");
            AddGeometryParameter(dt, 3, "D_sc", Data.geometryParameter.D_sc, "m", "短路距离");
            AddGeometryParameter(dt, 4, "l_sc", Data.geometryParameter.l_sc, "m", "短路长度");
            AddGeometryParameter(dt, 5, "thickness_cat", Data.geometryParameter.thickness_cat, "m", "阳极厚度");
            AddGeometryParameter(dt, 6, "thickness_ano", Data.geometryParameter.thickness_ano, "m", "阴极厚度");
            AddGeometryParameter(dt, 7, "distance_am", Data.geometryParameter.distance_am, "m", "阳极到膜的距离");
            AddGeometryParameter(dt, 8, "distance_cm", Data.geometryParameter.distance_cm, "m", "阴极到膜的距离");
            AddGeometryParameter(dt, 9, "Volume_hotside", Data.geometryParameter.Volume_hotside, "m³", "热侧体积");
            AddGeometryParameter(dt, 10, "Volume_codeside", Data.geometryParameter.Volume_codeside, "m³", "冷侧体积");
            AddGeometryParameter(dt, 11, "di_stack", Data.geometryParameter.di_stack, "m", "堆叠直径");
            AddGeometryParameter(dt, 12, "Area_sep", Data.geometryParameter.Area_sep, "m²", "隔膜面积");
            AddGeometryParameter(dt, 13, "Area_stack", Data.geometryParameter.Area_stack, "m²", "堆叠面积");
            AddGeometryParameter(dt, 14, "C_tsep", Data.geometryParameter.C_tsep, "F", "隔膜电容");
            AddGeometryParameter(dt, 15, "C_tk", Data.geometryParameter.C_tk, "F", "堆叠电容");

            // 添加空行
            for (int i = 0; i < 24; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
        }

        public void AddGeometryParameter(DataTable dt, int index, string name, double value, string unit, string description)
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
                    case "L_ca2se":
                        Data.geometryParameter.L_ca2se = variableValue;
                        break;
                    case "L_an2se":
                        Data.geometryParameter.L_an2se = variableValue;
                        break;
                    case "D_sc":
                        Data.geometryParameter.D_sc = variableValue;
                        break;
                    case "l_sc":
                        Data.geometryParameter.l_sc = variableValue;
                        break;
                    case "thickness_cat":
                        Data.geometryParameter.thickness_cat = variableValue;
                        break;
                    case "thickness_ano":
                        Data.geometryParameter.thickness_ano = variableValue;
                        break;
                    case "distance_am":
                        Data.geometryParameter.distance_am = variableValue;
                        break;
                    case "distance_cm":
                        Data.geometryParameter.distance_cm = variableValue;
                        break;
                    case "Volume_hotside":
                        Data.geometryParameter.Volume_hotside = variableValue;
                        break;
                    case "Volume_codeside":
                        Data.geometryParameter.Volume_codeside = variableValue;
                        break;
                    case "di_stack":
                        Data.geometryParameter.di_stack = variableValue;
                        break;
                    case "Area_sep":
                        Data.geometryParameter.Area_sep = variableValue;
                        break;
                    case "Area_stack":
                        Data.geometryParameter.Area_stack = variableValue;
                        break;
                    case "C_tsep":
                        Data.geometryParameter.C_tsep = variableValue;
                        break;
                    case "C_tk":
                        Data.geometryParameter.C_tk = variableValue;
                        break;
                }
            }
        }


    }
}
