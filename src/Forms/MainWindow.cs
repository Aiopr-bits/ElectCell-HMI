using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ElectCell_HMI
{
    public partial class MainWindow : Form
    {
        private ControlParameterPage controlParameter;
        private GeometricParameterPage geometricParameter;
        private FlowParameterPage flowParameter;
        private PSParameterPage psParameter;
        private ProcessParameterPage processParameter;

        public MainWindow()
        {
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            InitializeComponent();
            InitializeTreeView();            
            string path = @"C:/Users/Aiopr/Desktop/ElectCell-HMI/case1";
            readFile(path);
            InitializeControlPanel();
            AdjustDataGridViewStyles(this);
        }

        private void InitializeTreeView()
        {
            treeView1.Font = new System.Drawing.Font(treeView1.Font.FontFamily, 12);
            treeView1.ItemHeight = 24;

            // 创建根节点
            TreeNode rootNode = new TreeNode("电解水制氢仿真测试平台");

            // 创建子节点
            TreeNode simulationParamsNode = new TreeNode("仿真参数配置");
            simulationParamsNode.Nodes.Add(new TreeNode("控制参数配置"));
            simulationParamsNode.Nodes.Add(new TreeNode("几何参数配置"));
            simulationParamsNode.Nodes.Add(new TreeNode("flow参数配置"));
            simulationParamsNode.Nodes.Add(new TreeNode("ps参数配置"));
            simulationParamsNode.Nodes.Add(new TreeNode("工艺参数配置"));
            simulationParamsNode.Nodes.Add(new TreeNode("部件参数配置"));

            TreeNode simulationResultsNode = new TreeNode("仿真结果");
            TreeNode variableListNode = new TreeNode("变量清单");
            TreeNode faultInjectionNode = new TreeNode("故障注入");
            TreeNode autoTestNode = new TreeNode("自动测试");

            TreeNode dataMonitoringNode = new TreeNode("数据监控");
            dataMonitoringNode.Nodes.Add(new TreeNode("趋势监控"));
            dataMonitoringNode.Nodes.Add(new TreeNode("数据列表"));

            // 将子节点添加到根节点
            rootNode.Nodes.Add(simulationParamsNode);
            rootNode.Nodes.Add(simulationResultsNode);
            rootNode.Nodes.Add(variableListNode);
            rootNode.Nodes.Add(faultInjectionNode);
            rootNode.Nodes.Add(autoTestNode);
            rootNode.Nodes.Add(dataMonitoringNode);

            // 将根节点添加到TreeView
            treeView1.Nodes.Add(rootNode);

            // 展开所有节点
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeText = e.Node.Text;
            switch (nodeText)
            {
                case "控制参数配置":
                    geometricParameter.Hide();
                    flowParameter.Hide();
                    psParameter.Hide();
                    processParameter.Hide();
                    controlParameter.Show();
                    break;
                case "几何参数配置":
                    controlParameter.Hide();
                    flowParameter.Hide();
                    psParameter.Hide();
                    processParameter.Hide();
                    geometricParameter.Show();
                    break;
                case "flow参数配置":
                    controlParameter.Hide();
                    geometricParameter.Hide();
                    psParameter.Hide();
                    processParameter.Hide();
                    flowParameter.Show();
                    break;
                case "ps参数配置":
                    controlParameter.Hide();
                    geometricParameter.Hide();
                    flowParameter.Hide();
                    processParameter.Hide();
                    psParameter.Show();
                    break;
                case "工艺参数配置":
                    controlParameter.Hide();
                    geometricParameter.Hide();
                    flowParameter.Hide();
                    psParameter.Hide();
                    processParameter.Show();
                    break;
                case "部件参数配置":
                    
                    break;
                default:
                    controlParameter.Hide();
                    break;
            }
        }

        private void InitializeControlPanel()
        {
            controlParameter = new ControlParameterPage();
            controlParameter.Dock = DockStyle.Fill; 
            tableLayoutPanel1.Controls.Add(controlParameter, 1, 0);
            controlParameter.Hide();

            geometricParameter = new GeometricParameterPage();
            geometricParameter.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(geometricParameter, 1, 0);
            geometricParameter.Hide();

            flowParameter = new FlowParameterPage();
            flowParameter.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(flowParameter, 1, 0);
            flowParameter.Hide();

            psParameter = new PSParameterPage();
            psParameter.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(psParameter, 1, 0);
            psParameter.Hide();

            processParameter = new ProcessParameterPage();
            processParameter.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(processParameter, 1, 0);
            processParameter.Hide();
        }

        private void readFile(string path)
        {
            path = path + "/data_input.csv";
            string nextLine;
            string[] values;

            using (StreamReader sr = new StreamReader(path))
            {
                /*###########################控制参数###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.controlParameter.start_time = Convert.ToDouble(values[0]);
                    Data.controlParameter.end_time = Convert.ToDouble(values[1]);
                    Data.controlParameter.delta_t = Convert.ToDouble(values[2]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.controlParameter.cal_current = Convert.ToBoolean(values[0]);
                    Data.controlParameter.cal_valve = Convert.ToBoolean(values[1]);
                    Data.controlParameter.cal_pump = Convert.ToBoolean(values[2]);
                    Data.controlParameter.cal_balance_pipe = Convert.ToBoolean(values[3]);
                    Data.controlParameter.cal_mini_1 = Convert.ToBoolean(values[4]);
                    Data.controlParameter.cal_mini_2 = Convert.ToBoolean(values[5]);
                    Data.controlParameter.use_ff_static = Convert.ToBoolean(values[6]);
                    Data.controlParameter.IsMixed_circleType = Convert.ToBoolean(values[7]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.controlParameter.cal_superSat_fickTrans = Convert.ToBoolean(values[0]);
                    Data.controlParameter.cal_ShellTube_heatExchanger = Convert.ToBoolean(values[1]);
                    Data.controlParameter.cal_Ele_heater = Convert.ToBoolean(values[2]);
                }

                /*###########################几何参数###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.geometryParameter.L_ca2se = Convert.ToDouble(values[0]);
                    Data.geometryParameter.L_an2se = Convert.ToDouble(values[1]);
                    Data.geometryParameter.D_sc = Convert.ToDouble(values[2]);
                    Data.geometryParameter.l_sc = Convert.ToDouble(values[3]);
                    Data.geometryParameter.thickness_cat = Convert.ToDouble(values[4]);
                    Data.geometryParameter.thickness_ano = Convert.ToDouble(values[5]);
                    Data.geometryParameter.distance_am = Convert.ToDouble(values[6]);
                    Data.geometryParameter.distance_cm = Convert.ToDouble(values[7]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.geometryParameter.Volume_hotside = Convert.ToDouble(values[0]);
                    Data.geometryParameter.Volume_codeside = Convert.ToDouble(values[1]);
                    Data.geometryParameter.di_stack = Convert.ToDouble(values[2]);
                    Data.geometryParameter.Area_sep = Convert.ToDouble(values[3]);
                    Data.geometryParameter.Area_stack = Convert.ToDouble(values[4]);
                    Data.geometryParameter.C_tsep = Convert.ToDouble(values[5]);
                    Data.geometryParameter.C_tk = Convert.ToDouble(values[6]);
                }

                /*###########################部件参数flow和ps###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                Data.flowParameter.flow = new List<List<double>>();
                for (int i = 0; ; i++)
                {
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        if (!double.TryParse(values[0], out double temp)) break;
                        Data.flowParameter.flow.Add(new List<double>());
                        for (int j = 0; j < 6; j++)
                        {
                            Data.flowParameter.flow[i].Add(Convert.ToDouble(values[j]));
                        }
                    }
                }

                /*###########################部件参数ps###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                Data.psParameter.ps = new List<List<double>>();
                for (int i = 0; ; i++)
                {
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        if (!double.TryParse(values[0], out double temp)) break;
                        Data.psParameter.ps.Add(new List<double>());
                        for (int j = 0; j < 8; j++)
                        {
                            Data.psParameter.ps[i].Add(Convert.ToDouble(values[j]));
                        }
                    }
                }

                /*###########################部件参数###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.nElectrolyticCell = Convert.ToInt32(values[0]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.electrolyticCell = new List<ElectrolyticCell>();
                    for (int i = 0;i< Data.componentParameter.nElectrolyticCell; i++)
                    {
                        var cell = new ElectrolyticCell();
                        cell.current = Convert.ToDouble(values[i]);
                        Data.componentParameter.electrolyticCell.Add(cell);
                    }
                }
                for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++) 
                {
                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        var cell = Data.componentParameter.electrolyticCell[i];
                        cell.flow = new List<double>();
                        for (int j = 0; j < values.Length; j++)
                        {
                            if (double.TryParse(values[j], out double result))
                            {
                                cell.flow.Add(result);
                            }
                        }
                        Data.componentParameter.electrolyticCell[i] = cell;
                    }
                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        var cell = Data.componentParameter.electrolyticCell[i];
                        cell.ps = new List<double>();
                        for (int j = 0; j < values.Length; j++)
                        {
                            if (double.TryParse(values[j], out double result))
                            {
                                cell.ps.Add(result);
                            }
                        }
                        Data.componentParameter.electrolyticCell[i] = cell;
                    }
                }
                Data.componentParameter.pump = new List<Pump>();
                for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
                {
                    Data.componentParameter.pump.Add(new Pump());

                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        var cell = Data.componentParameter.pump[i];
                        cell.flow = new List<double>();
                        for (int j = 0; j < values.Length; j++)
                        {
                            if (double.TryParse(values[j], out double result))
                            {
                                cell.flow.Add(result);
                            }
                        }
                        Data.componentParameter.pump[i] = cell;
                    }
                    nextLine = sr.ReadLine();
                    nextLine = sr.ReadLine();
                    {
                        values = nextLine.Split(',');
                        var cell = Data.componentParameter.pump[i];
                        cell.ps = new List<double>();
                        for (int j = 0; j < values.Length; j++)
                        {
                            if (double.TryParse(values[j], out double result))
                            {
                                cell.ps.Add(result);
                            }
                        }
                        Data.componentParameter.pump[i] = cell;
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.cathodeSeparator = new CathodeSeparator();
                    Data.componentParameter.cathodeSeparator.flow = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.cathodeSeparator.flow.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.cathodeSeparator.ps = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.cathodeSeparator.ps.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.anodeSeparator = new AnodeSeparator();
                    Data.componentParameter.anodeSeparator.flow = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.anodeSeparator.flow.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.anodeSeparator.ps = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.anodeSeparator.ps.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.cathodeValve = new CathodeValve();
                    Data.componentParameter.cathodeValve.flow = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.cathodeValve.flow.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.cathodeValve.ps = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.cathodeValve.ps.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.anodeValve = new AnodeValve();
                    Data.componentParameter.anodeValve.flow = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.anodeValve.flow.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.anodeValve.ps = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.anodeValve.ps.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.balancePipe = new BalancePipe();
                    Data.componentParameter.balancePipe.flow = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.balancePipe.flow.Add(result);
                        }
                    }
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.componentParameter.balancePipe.ps = new List<double>();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (double.TryParse(values[j], out double result))
                        {
                            Data.componentParameter.balancePipe.ps.Add(result);
                        }
                    }
                }

                /*###########################工艺参数###########################*/
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.sigma_e_1 = Convert.ToDouble(values[0]);
                    Data.processParameter.sigma_h2_r1 = Convert.ToDouble(values[1]);
                    Data.processParameter.sigma_h2o_r1 = Convert.ToDouble(values[2]);
                    Data.processParameter.sigma_e_2 = Convert.ToDouble(values[3]);
                    Data.processParameter.sigma_h2o_r2 = Convert.ToDouble(values[4]);
                    Data.processParameter.sigma_o2_r2 = Convert.ToDouble(values[5]);
                    Data.processParameter.eta_F = Convert.ToDouble(values[6]);
                    Data.processParameter.F = Convert.ToDouble(values[7]);
                    Data.processParameter.n_cell = Convert.ToDouble(values[8]);
                    Data.processParameter.a_cell = Convert.ToDouble(values[9]);
                    Data.processParameter.A_mem = Convert.ToDouble(values[10]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.thickness_mem = Convert.ToDouble(values[0]);
                    Data.processParameter.porosity_mem = Convert.ToDouble(values[1]);
                    Data.processParameter.tortuosity_mem = Convert.ToDouble(values[2]);
                    Data.processParameter.wt_KOHsln = Convert.ToDouble(values[3]);
                    Data.processParameter.k = Convert.ToDouble(values[4]);
                    Data.processParameter.D_h2 = Convert.ToDouble(values[5]);
                    Data.processParameter.D_o2 = Convert.ToDouble(values[6]);
                    Data.processParameter.k_x_h2 = Convert.ToDouble(values[7]);
                    Data.processParameter.k_x_o2 = Convert.ToDouble(values[8]);
                    Data.processParameter.eps_h2_Darcy = Convert.ToDouble(values[9]);
                    Data.processParameter.eps_o2_Darcy = Convert.ToDouble(values[10]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.tao_b = Convert.ToDouble(values[0]);
                    Data.processParameter.FC_flash = Convert.ToDouble(values[1]);
                    Data.processParameter.R = Convert.ToDouble(values[2]);
                    Data.processParameter.eta = Convert.ToDouble(values[3]);
                    Data.processParameter.M_h2 = Convert.ToDouble(values[4]);
                    Data.processParameter.M_o2 = Convert.ToDouble(values[5]);
                    Data.processParameter.M_n2 = Convert.ToDouble(values[6]);
                    Data.processParameter.M_koh = Convert.ToDouble(values[7]);
                    Data.processParameter.M_h2o = Convert.ToDouble(values[8]);
                    Data.processParameter.rho_h2o = Convert.ToDouble(values[9]);
                    Data.processParameter.rho_h2 = Convert.ToDouble(values[10]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.rho_o2 = Convert.ToDouble(values[0]);
                    Data.processParameter.rho_sln_koh = Convert.ToDouble(values[1]);
                    Data.processParameter.g = Convert.ToDouble(values[2]);
                    Data.processParameter.Re7_0 = Convert.ToDouble(values[3]);
                    Data.processParameter.mu = Convert.ToDouble(values[4]);
                    Data.processParameter.cv1 = Convert.ToDouble(values[5]);
                    Data.processParameter.cv2 = Convert.ToDouble(values[6]);
                    Data.processParameter.P_cathode_sep_out = Convert.ToDouble(values[7]);
                    Data.processParameter.P_anode_sep_out = Convert.ToDouble(values[8]);
                    Data.processParameter.P_env = Convert.ToDouble(values[9]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.T_elin0 = Convert.ToDouble(values[0]);
                    Data.processParameter.T_k0 = Convert.ToDouble(values[1]);
                    Data.processParameter.T_K = Convert.ToDouble(values[2]);
                    Data.processParameter.T_btout = Convert.ToDouble(values[3]);
                    Data.processParameter.T_btout0 = Convert.ToDouble(values[4]);
                }
                nextLine = sr.ReadLine();
                nextLine = sr.ReadLine();
                {
                    values = nextLine.Split(',');
                    Data.processParameter.T_cw_in = Convert.ToDouble(values[0]);
                    Data.processParameter.T_cw_out0 = Convert.ToDouble(values[1]);
                    Data.processParameter.T_ambi = Convert.ToDouble(values[2]);
                    Data.processParameter.T_pipeout0 = Convert.ToDouble(values[3]);
                    Data.processParameter.T_btout_ano0 = Convert.ToDouble(values[4]);
                    Data.processParameter.T_btout_cat0 = Convert.ToDouble(values[5]);
                }
            }

            using (StreamWriter sw = new StreamWriter("history.csv", false))
            {
                sw.WriteLine(Path.GetDirectoryName(path));
            }
        }

        private void saveFile(string path) 
        {
            path = path + "/data_input.csv";
            using (StreamWriter sw = new StreamWriter(path)) 
            {
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("# 控制参数,,,,,,,,,,");
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("start_time,end_time,delta_t,,,,,,,,");
                sw.WriteLine(Data.controlParameter.start_time + "," + Data.controlParameter.end_time + "," + Data.controlParameter.delta_t + ",,,,,,,,");
                sw.WriteLine("cal_current,cal_valve,cal_pump,cal_balance_pipe,cal_mini_1,cal_mini_2,use_ff_static,IsMixed_circleType,,,");
                sw.WriteLine(Data.controlParameter.cal_current + "," + Data.controlParameter.cal_valve + "," + Data.controlParameter.cal_pump + "," + Data.controlParameter.cal_balance_pipe + "," + Data.controlParameter.cal_mini_1 + "," + Data.controlParameter.cal_mini_2 + "," + Data.controlParameter.use_ff_static + "," + Data.controlParameter.IsMixed_circleType + ",,,");
                sw.WriteLine("cal_superSat_fickTrans,cal_ShellTube_heatExchanger,cal_Ele_heater,,,,,,,,");
                sw.WriteLine(Data.controlParameter.cal_superSat_fickTrans + "," + Data.controlParameter.cal_ShellTube_heatExchanger + "," + Data.controlParameter.cal_Ele_heater + ",,,,,,,,");

                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("# 几何参数,,,,,,,,,,");
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("L_ca2se,L_an2se,D_sc,l_sc,thickness_cat,thickness_ano,distance_am,distance_cm,,,");
                sw.WriteLine(Data.geometryParameter.L_ca2se + "," + Data.geometryParameter.L_an2se + "," + Data.geometryParameter.D_sc + "," + Data.geometryParameter.l_sc + "," + Data.geometryParameter.thickness_cat + "," + Data.geometryParameter.thickness_ano + "," + Data.geometryParameter.distance_am + "," + Data.geometryParameter.distance_cm + ",,,");
                sw.WriteLine("Volume_hotside,Volume_codeside,di_stack,Area_sep,Area_stack,C_tsep,C_tk,,,,");
                sw.WriteLine(Data.geometryParameter.Volume_hotside + "," + Data.geometryParameter.Volume_codeside + "," + Data.geometryParameter.di_stack + "," + Data.geometryParameter.Area_sep + "," + Data.geometryParameter.Area_stack + "," + Data.geometryParameter.C_tsep + "," + Data.geometryParameter.C_tk + ",,,,");

                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("# 部件参数,,,,,,,,,,");
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                sw.WriteLine(Data.flowParameter.flow.Count + ",,,,,,,,,,");
                sw.WriteLine("num,x_h2,x_o2,x_h2o,Di,L,,,,,");
                for (int i = 0; i < Data.flowParameter.flow.Count; i++)
                {
                    sw.WriteLine(Data.flowParameter.flow[i][0] + "," + Data.flowParameter.flow[i][1] + "," + Data.flowParameter.flow[i][2] + "," + Data.flowParameter.flow[i][3] + "," + Data.flowParameter.flow[i][4] + "," + Data.flowParameter.flow[i][5] + ",,,,,");
                }

                sw.WriteLine("ps,,,,,,,,,,");
                sw.WriteLine(Data.psParameter.ps.Count + ",,,,,,,,,,");
                sw.WriteLine("num,n,v,p,l_l,l_g,n_h2,n_o2,v_t,,");
                for (int i = 0; i < Data.psParameter.ps.Count; i++)
                {
                    sw.WriteLine(Data.psParameter.ps[i][0] + "," + Data.psParameter.ps[i][1] + "," + Data.psParameter.ps[i][2] + "," + Data.psParameter.ps[i][3] + "," + Data.psParameter.ps[i][4] + "," + Data.psParameter.ps[i][5] + "," + Data.psParameter.ps[i][6] + "," + Data.psParameter.ps[i][7] +  ",,,");
                }

                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("# 部件参数,,,,,,,,,,");
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("电解槽个数,,,,,,,,,,");
                sw.WriteLine(Data.componentParameter.nElectrolyticCell + ",,,,,,,,,,");
                sw.WriteLine("电流,,,,,,,,,,");
                string line = "";
                for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
                {
                    line += Data.componentParameter.electrolyticCell[i].current + ",";
                }
                sw.WriteLine(line + ",,,,,,");
                for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
                {
                    sw.WriteLine("电解槽" + (i + 1) + ",,,,,,,,,,");
                    sw.WriteLine("flow,,,,,,,,,,");
                    line = "";
                    for (int j = 0; j < Data.componentParameter.electrolyticCell[i].flow.Count; j++)
                    {
                        line += Data.componentParameter.electrolyticCell[i].flow[j] + ",";
                    }
                    sw.WriteLine(line + ",,,,");
                    sw.WriteLine("ps,,,,,,,,,,");
                    line = "";
                    for (int j = 0; j < Data.componentParameter.electrolyticCell[i].ps.Count; j++)
                    {
                        line += Data.componentParameter.electrolyticCell[i].ps[j] + ",";
                    }
                    sw.WriteLine(line + ",,,,,,,,");
                }
                for (int i = 0; i < Data.componentParameter.nElectrolyticCell; i++)
                {
                    sw.WriteLine("泵" + (i + 1) + ",,,,,,,,,,");
                    sw.WriteLine("flow,,,,,,,,,,");
                    line = "";
                    for (int j = 0; j < Data.componentParameter.pump[i].flow.Count; j++)
                    {
                        line += Data.componentParameter.pump[i].flow[j] + ",";
                    }
                    sw.WriteLine(line + ",,,,,,,,");
                    sw.WriteLine("ps,,,,,,,,,,");
                    line = "";
                    for (int j = 0; j < Data.componentParameter.pump[i].ps.Count; j++)
                    {
                        line += Data.componentParameter.pump[i].ps[j] + ",";
                    }
                    sw.WriteLine(line + ",,,,,,,,,");
                }
                sw.WriteLine("阴极分离器,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.cathodeSeparator.flow.Count; j++)
                {
                    line += Data.componentParameter.cathodeSeparator.flow[j] + ",";
                }
                sw.WriteLine(line + ",,,,,");
                sw.WriteLine("ps,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.cathodeSeparator.ps.Count; j++)
                {
                    line += Data.componentParameter.cathodeSeparator.ps[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,");
                sw.WriteLine("阳极分离器,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.anodeSeparator.flow.Count; j++)
                {
                    line += Data.componentParameter.anodeSeparator.flow[j] + ",";
                }
                sw.WriteLine(line + ",,,,,");
                sw.WriteLine("ps,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.anodeSeparator.ps.Count; j++)
                {
                    line += Data.componentParameter.anodeSeparator.ps[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,");
                sw.WriteLine("阴极阀门,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.cathodeValve.flow.Count; j++)
                {
                    line += Data.componentParameter.cathodeValve.flow[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,");
                sw.WriteLine("ps,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.cathodeValve.ps.Count; j++)
                {
                    line += Data.componentParameter.cathodeValve.ps[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,,");
                sw.WriteLine("阳极阀门,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.anodeValve.flow.Count; j++)
                {
                    line += Data.componentParameter.anodeValve.flow[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,");
                sw.WriteLine("ps,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.anodeValve.ps.Count; j++)
                {
                    line += Data.componentParameter.anodeValve.ps[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,,");
                sw.WriteLine("平衡管线,,,,,,,,,,");
                sw.WriteLine("flow,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.balancePipe.flow.Count; j++)
                {
                    line += Data.componentParameter.balancePipe.flow[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,");
                sw.WriteLine("ps,,,,,,,,,,");
                line = "";
                for (int j = 0; j < Data.componentParameter.balancePipe.ps.Count; j++)
                {
                    line += Data.componentParameter.balancePipe.ps[j] + ",";
                }
                sw.WriteLine(line + ",,,,,,,,,");

                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("# 工艺参数,,,,,,,,,,");
                sw.WriteLine("###########################,,,,,,,,,,");
                sw.WriteLine("sigma_e_1,sigma_h2_r1,sigma_h2o_r1,sigma_e_2,sigma_h2o_r2,sigma_o2_r2,eta_F,F,n_cell,a_cell,A_mem");
                sw.WriteLine(Data.processParameter.sigma_e_1 + "," + Data.processParameter.sigma_h2_r1 + "," + Data.processParameter.sigma_h2o_r1 + "," + Data.processParameter.sigma_e_2 + "," + Data.processParameter.sigma_h2o_r2 + "," + Data.processParameter.sigma_o2_r2 + "," + Data.processParameter.eta_F + "," + Data.processParameter.F + "," + Data.processParameter.n_cell + "," + Data.processParameter.a_cell + "," + Data.processParameter.A_mem);
                sw.WriteLine("thickness_mem,porosity_mem,tortuosity_mem,wt_KOHsln,k,D_h2,D_o2,k_x_h2,k_x_o2,eps_h2_Darcy,eps_o2_Darcy");
                sw.WriteLine(Data.processParameter.thickness_mem + "," + Data.processParameter.porosity_mem + "," + Data.processParameter.tortuosity_mem + "," + Data.processParameter.wt_KOHsln + "," + Data.processParameter.k + "," + Data.processParameter.D_h2 + "," + Data.processParameter.D_o2 + "," + Data.processParameter.k_x_h2 + "," + Data.processParameter.k_x_o2 + "," + Data.processParameter.eps_h2_Darcy + "," + Data.processParameter.eps_o2_Darcy);
                sw.WriteLine("tao_b,FC_flash,R,eta,M_h2,M_o2,M_n2,M_koh,M_h2o,rho_h2o,rho_h2");
                sw.WriteLine(Data.processParameter.tao_b + "," + Data.processParameter.FC_flash + "," + Data.processParameter.R + "," + Data.processParameter.eta + "," + Data.processParameter.M_h2 + "," + Data.processParameter.M_o2 + "," + Data.processParameter.M_n2 + "," + Data.processParameter.M_koh + "," + Data.processParameter.M_h2o + "," + Data.processParameter.rho_h2o + "," + Data.processParameter.rho_h2);
                sw.WriteLine("rho_o2,rho_sln_koh,g,Re7_0,mu,cv1,cv2,P_cathode_sep_out,P_anode_sep_out,P_env,");
                sw.WriteLine(Data.processParameter.rho_o2 + "," + Data.processParameter.rho_sln_koh + "," + Data.processParameter.g + "," + Data.processParameter.Re7_0 + "," + Data.processParameter.mu + "," + Data.processParameter.cv1 + "," + Data.processParameter.cv2 + "," + Data.processParameter.P_cathode_sep_out + "," + Data.processParameter.P_anode_sep_out + "," + Data.processParameter.P_env + ",");
                sw.WriteLine("T_elin0,T_k0,T_K,T_btout,T_btout0,,,,,,");
                sw.WriteLine(Data.processParameter.T_elin0 + "," + Data.processParameter.T_k0 + "," + Data.processParameter.T_K + "," + Data.processParameter.T_btout + "," + Data.processParameter.T_btout0 + ",,,,,,");
                sw.WriteLine("T_cw_in,T_cw_out0,T_ambi,T_pipeout0,T_btout_ano0,T_btout_cat0,,,,,");
                sw.WriteLine(Data.processParameter.T_cw_in + "," + Data.processParameter.T_cw_out0 + "," + Data.processParameter.T_ambi + "," + Data.processParameter.T_pipeout0 + "," + Data.processParameter.T_btout_ano0 + "," + Data.processParameter.T_btout_cat0 + ",,,,,");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void AdjustDataGridViewStyles(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is DataGridView dataGridView)
                {

                    dataGridView.BackgroundColor = Color.FromArgb(255, 255, 255);
                    dataGridView.BorderStyle = BorderStyle.FixedSingle;
                    dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
                    dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 10); 
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 12, FontStyle.Bold);
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.EnableHeadersVisualStyles = false;
                    dataGridView.GridColor = Color.FromArgb(200, 200, 200);
                    dataGridView.RowHeadersVisible = false;
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.Dock = DockStyle.Fill;
                    dataGridView.AllowUserToAddRows = false;
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    dataGridView.AllowUserToDeleteRows = false;
                    dataGridView.AllowUserToResizeColumns = false;
                    dataGridView.AllowUserToResizeRows = false;
                    dataGridView.ScrollBars = ScrollBars.None;

                    // 添加 MouseWheel 事件处理程序
                    dataGridView.MouseWheel += (s, e) =>
                    {
                        if (e.Delta > 0)
                        {
                            if (dataGridView.FirstDisplayedScrollingRowIndex > 0)
                            {
                                dataGridView.FirstDisplayedScrollingRowIndex--;
                            }
                        }
                        else if (e.Delta < 0)
                        {
                            if (dataGridView.FirstDisplayedScrollingRowIndex < dataGridView.RowCount - 1)
                            {
                                dataGridView.FirstDisplayedScrollingRowIndex++;
                            }
                        }
                    };
                }
                else if (control.HasChildren)
                {
                    AdjustDataGridViewStyles(control);
                }
            }
        }
    }
}
