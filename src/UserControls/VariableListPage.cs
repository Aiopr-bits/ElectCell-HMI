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
    public partial class VariableListPage : UserControl
    {
        public VariableListPage()
        {
            InitializeComponent();
            dataGridView1LoadData();
        }

        public void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("部件名称", typeof(string));
            dt.Columns.Add("流股/子部件", typeof(string));
            dt.Columns.Add("变量名", typeof(string));
            dt.Columns.Add("变量值", typeof(double));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("属性", typeof(string));
            dt.Columns.Add("含义", typeof(string));

            //电解槽
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.electrolyticCell[i].flow.Contains(Data.flowParameter.flow[j][0]))
                    {
                        AddProcessParameter(dt, "电解槽"+(i+1), "流股-"+ Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");

                    }
                }

                //ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.electrolyticCell[i].ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "氢气", Data.psParameter.ps[j][6].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "电解槽" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "氧气", Data.psParameter.ps[j][7].ToString(), "m/s", "RW", "变量含义");


                    }
                }

                //i_current
                AddProcessParameter(dt, "电解槽" + (i + 1), "i_current", "电流", Data.componentParameter.electrolyticCell[i].current.ToString(), "A", "RW", "变量含义");
            }

            //泵
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                //flow
                for (int j = 0; j < Data.flowParameter.flow.Count; j++)
                {
                    if (Data.componentParameter.pump[i].flow.Contains(Data.flowParameter.flow[j][0]))
                    {
                        AddProcessParameter(dt, "泵" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");

                    }
                }

                //ps
                for (int j = 0; j < Data.psParameter.ps.Count; j++)
                {
                    if (Data.componentParameter.pump[i].ps.Contains(Data.psParameter.ps[j][0]))
                    {
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                        AddProcessParameter(dt, "泵" + (i + 1), "子部件-" + Data.psParameter.ps[j][0], "氢气", Data.psParameter.ps[j][6].ToString(), "m/s", "RW", "变量含义");
                    }
                }
            }

            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.cathodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    AddProcessParameter(dt, "阴极分离器", "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.cathodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "氢气", Data.psParameter.ps[j][6].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极分离器", "子部件-" + Data.psParameter.ps[j][0], "氧气", Data.psParameter.ps[j][7].ToString(), "m/s", "RW", "变量含义");
                }
            }

            //阳极分离器
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.anodeSeparator.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    AddProcessParameter(dt, "阳极分离器", "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.anodeSeparator.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    AddProcessParameter(dt, "阳极分离器", "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分��器", "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极分离器", "子部件-" + Data.psParameter.ps[j][0], "氢气", Data.psParameter.ps[j][6].ToString(), "m/s", "RW", "变量含义");
                }
            }

            //阴极阀门
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.cathodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    AddProcessParameter(dt, "阴极阀门", "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.cathodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    AddProcessParameter(dt, "阴极阀门", "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阴极阀门", "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //阳极阀门
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.anodeValve.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    AddProcessParameter(dt, "阳极阀门", "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.anodeValve.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    AddProcessParameter(dt, "阳极阀门", "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "阳极阀门", "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //平衡管线
            //flow
            for (int j = 0; j < Data.flowParameter.flow.Count; j++)
            {
                if (Data.componentParameter.balancePipe.flow.Contains(Data.flowParameter.flow[j][0]))
                {
                    AddProcessParameter(dt, "平衡管线", "流股-" + Data.flowParameter.flow[j][0], "氢气", Data.flowParameter.flow[j][1].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "流股-" + Data.flowParameter.flow[j][0], "氧气", Data.flowParameter.flow[j][2].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "流股-" + Data.flowParameter.flow[j][0], "水", Data.flowParameter.flow[j][3].ToString(), "m/s", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "流股-" + Data.flowParameter.flow[j][0], "管道直径", Data.flowParameter.flow[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "流股-" + Data.flowParameter.flow[j][0], "管道长度", Data.flowParameter.flow[j][5].ToString(), "m", "RW", "变量含义");
                }
            }

            //ps
            for (int j = 0; j < Data.psParameter.ps.Count; j++)
            {
                if (Data.componentParameter.balancePipe.ps.Contains(Data.psParameter.ps[j][0]))
                {
                    AddProcessParameter(dt, "平衡管线", "子部件-" + Data.psParameter.ps[j][0], "物质的量", Data.psParameter.ps[j][1].ToString(), "mol", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "子部件-" + Data.psParameter.ps[j][0], "摩尔体积", Data.psParameter.ps[j][2].ToString(), "m^3", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "子部件-" + Data.psParameter.ps[j][0], "压强", Data.psParameter.ps[j][3].ToString(), "Pa", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "子部件-" + Data.psParameter.ps[j][0], "液体高度", Data.psParameter.ps[j][4].ToString(), "m", "RW", "变量含义");
                    AddProcessParameter(dt, "平衡管线", "子部件-" + Data.psParameter.ps[j][0], "气体高度", Data.psParameter.ps[j][5].ToString(), "m", "RW", "变量含义");
                }
            }



            dataGridView1.DataSource = dt;
        }

        public void AddProcessParameter(DataTable dt, string partName, string subPart, string name, string value, string unit, string attribute, string description)
        {
            DataRow dr = dt.NewRow();
            dr["部件名称"] = partName;
            dr["流股/子部件"] = subPart;
            dr["变量名"] = name;
            dr["变量值"] = value;
            dr["单位"] = unit;
            dr["属性"] = attribute;
            dr["含义"] = description;
            dt.Rows.Add(dr);
        }
    }
}
