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
    public partial class ComponentParameterPage : UserControl
    {
        public ComponentParameterPage()
        {
            InitializeComponent();
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            dataGridView1LoadData();
        }

        public void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewStyle();
        }

        public void dataGridView1LoadData()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                dt.Columns.Add($"电解槽{i + 1}-flow", typeof(double));
                dt.Columns.Add($"电解槽{i + 1}-ps", typeof(double));
                dt.Columns.Add($"电解槽{i + 1}-current", typeof(double));
            }
            for (int i = 0; i < Data.componentParameter.pump.Count; i++)
            {
                dt.Columns.Add($"泵{i + 1}-flow", typeof(double));
                dt.Columns.Add($"泵{i + 1}-ps", typeof(double));
            }
            dt.Columns.Add("阴极分离器-flow", typeof(double));
            dt.Columns.Add("阴极分离器-ps", typeof(double));
            dt.Columns.Add("阳极分离器-flow", typeof(double));
            dt.Columns.Add("阳极分离器-ps", typeof(double));
            dt.Columns.Add("阴极阀门-flow", typeof(double));
            dt.Columns.Add("阴极阀门-ps", typeof(double));
            dt.Columns.Add("阳极阀门-flow", typeof(double));
            dt.Columns.Add("阳极阀门-ps", typeof(double));
            dt.Columns.Add("平衡管线-flow", typeof(double));
            dt.Columns.Add("平衡管线-ps", typeof(double));

            for (int i = 0; i < Data.componentParameter.electrolyticCell[0].flow.Count; i++)
            {
                AddControlParameter(dt, i);
            }

            DataTable rotatedDt = RotateDataTable(dt);
            dataGridView1.DataSource = rotatedDt;
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        public void AddControlParameter(DataTable dt, int nrow)
        {
            DataRow dr = dt.NewRow();

            for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
            {
                dr[$"电解槽{i + 1}-flow"] = nrow < Data.componentParameter.electrolyticCell[i].flow.Count ?
                    Data.componentParameter.electrolyticCell[i].flow[nrow] : (object)DBNull.Value;
                dr[$"电解槽{i + 1}-ps"] = nrow < Data.componentParameter.electrolyticCell[i].ps.Count ?
                    Data.componentParameter.electrolyticCell[i].ps[nrow] : (object)DBNull.Value;
                dr[$"电解槽{i + 1}-current"] = nrow < 1 ?
                    Data.componentParameter.electrolyticCell[i].current : (object)DBNull.Value;
            }

            for (int i = 0; i < Data.componentParameter.pump.Count; i++)
            {
                dr[$"泵{i + 1}-flow"] = nrow < Data.componentParameter.pump[i].flow.Count ?
                    Data.componentParameter.pump[i].flow[nrow] : (object)DBNull.Value;
                dr[$"泵{i + 1}-ps"] = nrow < Data.componentParameter.pump[i].ps.Count ?
                    Data.componentParameter.pump[i].ps[nrow] : (object)DBNull.Value;
            }

            dr["阴极分离器-flow"] = nrow < Data.componentParameter.cathodeSeparator.flow.Count ?
                Data.componentParameter.cathodeSeparator.flow[nrow] : (object)DBNull.Value;
            dr["阴极分离器-ps"] = nrow < Data.componentParameter.cathodeSeparator.ps.Count ?
                Data.componentParameter.cathodeSeparator.ps[nrow] : (object)DBNull.Value;
            dr["阳极分离器-flow"] = nrow < Data.componentParameter.anodeSeparator.flow.Count ?
                Data.componentParameter.anodeSeparator.flow[nrow] : (object)DBNull.Value;
            dr["阳极分离器-ps"] = nrow < Data.componentParameter.anodeSeparator.ps.Count ?
                Data.componentParameter.anodeSeparator.ps[nrow] : (object)DBNull.Value;
            dr["阴极阀门-flow"] = nrow < Data.componentParameter.cathodeValve.flow.Count ?
                Data.componentParameter.cathodeValve.flow[nrow] : (object)DBNull.Value;
            dr["阴极阀门-ps"] = nrow < Data.componentParameter.cathodeValve.ps.Count ?
                Data.componentParameter.cathodeValve.ps[nrow] : (object)DBNull.Value;
            dr["阳极阀门-flow"] = nrow < Data.componentParameter.anodeValve.flow.Count ?
                Data.componentParameter.anodeValve.flow[nrow] : (object)DBNull.Value;
            dr["阳极阀门-ps"] = nrow < Data.componentParameter.anodeValve.ps.Count ?
                Data.componentParameter.anodeValve.ps[nrow] : (object)DBNull.Value;
            dr["平衡管线-flow"] = nrow < Data.componentParameter.balancePipe.flow.Count ?
                Data.componentParameter.balancePipe.flow[nrow] : (object)DBNull.Value;
            dr["平衡管线-ps"] = nrow < Data.componentParameter.balancePipe.ps.Count ?
                Data.componentParameter.balancePipe.ps[nrow] : (object)DBNull.Value;

            dt.Rows.Add(dr);
        }

        public DataTable RotateDataTable(DataTable dt)
        {
            DataTable rotatedDt = new DataTable();

            // Add columns
            rotatedDt.Columns.Add("Parameter", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rotatedDt.Columns.Add($"Row {i + 1}", typeof(double));
            }

            // Add rows
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataRow dr = rotatedDt.NewRow();
                dr[0] = dt.Columns[i].ColumnName;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dr[j + 1] = dt.Rows[j][i];
                }
                rotatedDt.Rows.Add(dr);
            }

            return rotatedDt;
        }

        public void dataGridViewStyle()
        {
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ColumnHeadersVisible = false;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
            }

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
            }
        }

        public void SaveDataGridViewChanges()
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parameterName = dt.Rows[i]["Parameter"].ToString();

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    double value = dt.Rows[i][j] != DBNull.Value ? Convert.ToDouble(dt.Rows[i][j]) : double.NaN;

                    if (parameterName.StartsWith("电解槽"))
                    {
                        int cellIndex = int.Parse(parameterName.Substring(3, 1)) - 1;
                        var cell = Data.componentParameter.electrolyticCell[cellIndex];
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < cell.flow.Count)
                            {
                                cell.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < cell.ps.Count)
                            {
                                cell.ps[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("current"))
                        {
                            if (j - 1 < 1)
                            {
                                cell.current = value;
                            }
                        }
                    }
                    else if (parameterName.StartsWith("泵"))
                    {
                        int pumpIndex = int.Parse(parameterName.Substring(1, 1)) - 1;
                        var pump = Data.componentParameter.pump[pumpIndex];
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < pump.flow.Count)
                            {
                                pump.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < pump.ps.Count)
                            {
                                pump.ps[j - 1] = value;
                            }
                        }
                    }
                    else if (parameterName.Contains("阴极分离器"))
                    {
                        var cathodeSeparator = Data.componentParameter.cathodeSeparator;
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < cathodeSeparator.flow.Count)
                            {
                                cathodeSeparator.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < cathodeSeparator.ps.Count)
                            {
                                cathodeSeparator.ps[j - 1] = value;
                            }
                        }
                    }
                    else if (parameterName.Contains("阳极分离器"))
                    {
                        var anodeSeparator = Data.componentParameter.anodeSeparator;
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < anodeSeparator.flow.Count)
                            {
                                anodeSeparator.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < anodeSeparator.ps.Count)
                            {
                                anodeSeparator.ps[j - 1] = value;
                            }
                        }
                    }
                    else if (parameterName.Contains("阴极阀门"))
                    {
                        var cathodeValve = Data.componentParameter.cathodeValve;
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < cathodeValve.flow.Count)
                            {
                                cathodeValve.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < cathodeValve.ps.Count)
                            {
                                cathodeValve.ps[j - 1] = value;
                            }
                        }
                    }
                    else if (parameterName.Contains("阳极阀门"))
                    {
                        var anodeValve = Data.componentParameter.anodeValve;
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < anodeValve.flow.Count)
                            {
                                anodeValve.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < anodeValve.ps.Count)
                            {
                                anodeValve.ps[j - 1] = value;
                            }
                        }
                    }
                    else if (parameterName.Contains("平衡管线"))
                    {
                        var balancePipe = Data.componentParameter.balancePipe;
                        if (parameterName.Contains("flow"))
                        {
                            if (j - 1 < balancePipe.flow.Count)
                            {
                                balancePipe.flow[j - 1] = value;
                            }
                        }
                        else if (parameterName.Contains("ps"))
                        {
                            if (j - 1 < balancePipe.ps.Count)
                            {
                                balancePipe.ps[j - 1] = value;
                            }
                        }
                    }
                }
            }
        }


    }

}
