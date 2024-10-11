using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ElectCell_HMI
{
    public partial class MainWindow : Form
    {
        private ControlParameterPage controlParameter;

        public MainWindow()
        {
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            InitializeComponent();
            InitializeTreeView();
            InitializeControlPanel();
            string path = @"C:/Users/Aiopr/Desktop/ElectCell-HMI/case1";
            readFile(path);
            saveFile(path);
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
        }

        private void InitializeControlPanel()
        {
            controlParameter = new ControlParameterPage();
            controlParameter.Dock = DockStyle.Fill; 
            tableLayoutPanel1.Controls.Add(controlParameter, 1, 0);
            controlParameter.Show();
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

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
