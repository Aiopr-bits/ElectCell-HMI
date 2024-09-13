using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform
{

	//ps参数
	public class PsData
	{
		public double n, v, p, l_l, l_g, n_h2, n_o2, v_t;
		public int num;
		public PsData()
		{

		}
	}

	//ps参数
	public class FaultPsData
	{
		public double n, v, p, l_l, l_g, n_h2, n_o2, v_t;
		public int num;
		public bool is_fault, is_result;
	}

	//flow参数
	public class FlowData
	{
		public int num;
		public double Di, L, x_h2, x_o2, x_h2o, h2_type;
		public FlowData()
		{
		}
	}

	//fault-flow参数
	public class FaultFlowData
	{
		public int num;
		public double Di, L, v_t, x_h2, x_o2, x_h2o, h2_type;
		public bool is_fault, is_result;
	}

	//电解槽参数
	//public class ElectricTankData
	//{

	//    public int m, n, num, count;
	//    public double[] I_current;
	//    public List<List<int>> flow, ps;
	//    public string[] Part_name;
	//    public string node_name;
	//    public ElectricTankData()
	//    {
	//        I_current = new double[10];
	//        flow = new List<List<int>>(); 
	//        ps = new List<List<int>>();
	//        Part_name = new string[10];
	//    }
	//}

	public class NodeData
	{
		public int i_flow, i_ps;
		public double I_current;
		public int[] flow, ps;
		public string node_name;
		public NodeData()
		{

		}
	}

	//工况参数
	public class CaseData
	{
		public int icase, line_case;
		public double t_case, p_case, m_case;

	}

	//一般参数，用于总调度
	public class Data
	{
		public static int n_flow, n_ps, n_node, n_case, n_ele;//
		public static double delta_t, start_time, end_time;
        public static double L_ca2se, L_an2se, D_sc, l_sc, thickness_cat, thickness_ano, distance_am, distance_cm;
        public static double Volume_hotside, Volume_codeside, di_stack, Area_sep, Area_stack, C_tsep, C_tk;
        public static double sigma_e_1, sigma_h2_r1, sigma_h2o_r1, sigma_e_2, sigma_h2o_r2, sigma_o2_r2, eta_F, F, n_cell,
			a_cell, A_mem, thickness_mem, porosity_mem, tortuosity_mem, wt_KOHsln, k, D_h2, D_o2, k_x_h2, k_x_o2, eps_h2_Darcy,
			eps_o2_Darcy, tao_b, FC_flash, R, eta, M_h2, M_o2, M_n2, M_koh, M_h2o, rho_h2o, rho_h2, rho_o2, rho_sln_koh, g, Re7_0,
			mu, cv1, cv2, P_cathode_sep_out, P_anode_sep_out, P_env;
        public static double T_elin0, T_k0, T_K, T_btout, T_btout0;
        public static double T_cw_in, T_cw_out0, T_ambi, T_pipeout0, T_btout_ano0, T_btout_cat0;
        public static bool cal_current, cal_valve, cal_pump, cal_balance_pipe, cal_mini_1, cal_mini_2, use_ff_static, IsMixed_circleType;
        public static bool cal_superSat_fickTrans, cal_ShellTube_heatExchanger, cal_Ele_heater;
        public static bool multi_case;//多工况计算
		public static string fzxt_name, user_name, user_password, case_name;//用户登录用户名，密码
		public static string imagePath;

		public static DateTime start_time2 = new DateTime(2024, 1, 17, 10, 00, 00);//-------------后期改成页面配置时间
		public static DateTime[] check_time = new DateTime[16];
		public static int[] yz_num = new int[16];//图表阈值设定

		#region 一些读取仿真结果文件并加载到内存中的字段及方法
		private static readonly List<string> data_name = new List<string>();//仿真结果数据  变量名
		private static readonly List<List<double>> data = new List<List<double>>();//仿真结果数据  变量值
		private static DataTable resultDataTable;//仿真结果数据  数据表
		public static DataTable ResultDataTable
		{
			get
			{
				if (resultDataTable == null)
				{
					string filepath = Path.Combine(exePath, case_name, "output.data", "case_1.csv");
					{
						// 修改数据结构及缓存方式。20240320，由M修改
						resultDataTable = new DataTable();
						bool fileExistFlag = File.Exists(filepath);

						if (fileExistFlag == false)
							return resultDataTable;
						try
						{
							using (StreamReader sr = new StreamReader(new FileStream(filepath, FileMode.Open)))
							{
								string line = sr.ReadLine();
								string[] lineSplit = line.Split(',');
								foreach (string s in lineSplit)
								{
									if (s != string.Empty)
									{
										data_name.Add(s);
										resultDataTable.Columns.Add(new DataColumn(s, typeof(double)));
									}
								}
								line = sr.ReadLine();
								while (line != null && line != string.Empty)
								{
									lineSplit = line.Split(',');
									List<double> data = new List<double>();
									foreach (string s in lineSplit)
									{
										if (s != null && s != string.Empty)
										{
											data.Add(Convert.ToDouble(s));
										}
									}
									Data.data.Add(data);
									DataRow row = resultDataTable.NewRow();
									for (int i = 0; i < data.Count; i++)
									{
										row[i] = data[i];
									}
									resultDataTable.Rows.Add(row);
									line = sr.ReadLine();
								}
							}
						}
						catch (Exception myException)
						{
							MessageBox.Show("文件是打开状态，请检查后重试！", "提示");
						}
					}
				}
					return resultDataTable;				
			}
		}

        public static void GUI2CSV(string fn)
        {
            // output gui.input
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, false, Encoding.Default))
            {
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("# 控制参数,,,,,,,,,,,,,,,");
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("start_time,end_time,delta_t,,,,,,,,,");
                file.WriteLine(Data.start_time.ToString() + ',' + Data.end_time.ToString() + ',' + Data.delta_t.ToString() + ',');
                file.WriteLine("cal_current,cal_valve,cal_pump,cal_balance_pipe,cal_mini_1,cal_mini_2,use_ff_static,IsMixed_circleType,cal_superSat_fickTrans,ec_pump_independent,,");
                file.WriteLine(Data.cal_current.ToString() + ',' + Data.cal_valve.ToString() + ',' + Data.cal_pump.ToString() + ',' + Data.cal_balance_pipe.ToString() + ',' + Data.cal_mini_1.ToString() + ','
                    + Data.cal_mini_2.ToString() + ',' + Data.use_ff_static.ToString() + ',' + Data.IsMixed_circleType.ToString() + ',' + Data.cal_superSat_fickTrans.ToString() + ',' );
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("# 几何参数,,,,,,,,,,,,,,,");
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("L_ca2se,L_an2se,D_sc,l_sc,,,,,,,,,,,,,,,");
                file.WriteLine(Data.L_ca2se.ToString() + ',' + Data.L_an2se.ToString() + ',' + Data.D_sc.ToString() + ',' + Data.l_sc.ToString() + ',');
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("# flow和ps参数,,,,,,,,,,,,,,,");
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("flow,,,,,,,,,,,,,,,");
                file.WriteLine(Data.n_flow.ToString() + ',');
                file.WriteLine("num,x_h2,x_o2,x_h2o,Di,L,v_t,,,,,,,,,,,,");
                for (int i = 0; i < Data.n_flow; i++)
                {
                    file.WriteLine(Data.flow[i].num.ToString() + ',' + Data.flow[i].x_h2.ToString() + ',' + Data.flow[i].x_o2.ToString() + ',' + Data.flow[i].x_h2o.ToString() + ','
                        + Data.flow[i].Di.ToString() + ',' + Data.flow[i].L.ToString() + ',' );
                }
                file.WriteLine("ps,,,,,,,,,,,,,,,");
                file.WriteLine(Data.n_ps.ToString() + ',');
                file.WriteLine("num,n,v,p,l_l,l_g,x_h2,x_o2,x_h2o,,,,,,,,,,");
                for (int i = 0; i < Data.n_ps; i++)
                {
                    file.WriteLine(Data.ps[i].num.ToString() + ',' + Data.ps[i].n.ToString() + ',' + Data.ps[i].v.ToString() + ',' + Data.ps[i].p.ToString() + ',' + Data.ps[i].l_l.ToString() + ','
                         + Data.ps[i].l_g.ToString() + ',' + Data.ps[i].n_h2.ToString() + ',' + Data.ps[i].n_o2.ToString() + ',' + Data.ps[i].v_t.ToString() + ',');
                }
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("# 工艺参数,,,,,,,,,,,,,,,");
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("sigma_e_1,sigma_h2_r1,sigma_h2o_r1,sigma_e_2,sigma_h2o_r2,sigma_o2_r2,eta_F,F,n_cell,a_cell,A_mem,,,,,,,,,");
                file.WriteLine(Data.sigma_e_1.ToString() + ',' + Data.sigma_h2_r1.ToString() + ',' + Data.sigma_h2o_r1.ToString() + ',' + Data.sigma_e_2.ToString() + ',' + Data.sigma_h2o_r2.ToString() + ',' +
                    Data.sigma_o2_r2.ToString() + ',' + Data.eta_F.ToString() + ',' + Data.F.ToString() + ',' + Data.n_cell.ToString() + ',' + Data.a_cell.ToString() + ',' + Data.A_mem.ToString() + ',');
                file.WriteLine("thickness_mem,porosity_mem,tortuosity_mem,wt_KOHsln,k,D_h2,D_o2,k_x_h2,k_x_o2,eps_h2_Darcy,eps_o2_Darcy,,,,,,,,,");
                file.WriteLine(Data.thickness_mem.ToString() + ',' + Data.porosity_mem.ToString() + ',' + Data.tortuosity_mem.ToString() + ',' + Data.wt_KOHsln.ToString() + ',' + Data.k.ToString() + ',' +
                    Data.D_h2.ToString() + ',' + Data.D_o2.ToString() + ',' + Data.k_x_h2.ToString() + ',' + Data.k_x_o2.ToString() + ',' + Data.eps_h2_Darcy.ToString() + ',' + Data.eps_o2_Darcy.ToString() + ',');
                file.WriteLine("tao_b,FC_flash,R,T,eta,M_h2,M_o2,M_koh,M_h2o,rho_h2o,rho_h2,,,,,,,,,");
                file.WriteLine(Data.tao_b.ToString() + ',' + Data.FC_flash.ToString() + ',' + Data.R.ToString()  + ',' + Data.eta.ToString() + ',' +
                    Data.M_h2.ToString() + ',' + Data.M_o2.ToString() + ',' + Data.M_n2.ToString() + ',' + Data.M_koh.ToString() + ',' + Data.M_h2o.ToString() + ',' + Data.rho_h2o.ToString() + ',' + Data.rho_h2.ToString() + ',');
                file.WriteLine("rho_o2,rho_sln_koh,g,Re24_0,mu,Area_hx,massFlowRate_cw,cv1,cv2,P_cathode_sep_out,P_anode_sep_out,,,,,,,,,");
                file.WriteLine(Data.rho_o2.ToString() + ',' + Data.rho_sln_koh.ToString() + ',' + Data.g.ToString() + ',' + Data.Re7_0.ToString() + ',' + Data.mu.ToString() + ','  + Data.cv1.ToString() + ',' + Data.cv2.ToString() + ',' + Data.P_cathode_sep_out.ToString() + ',' + Data.P_anode_sep_out.ToString() + ',' + Data.P_env.ToString() + ',');
                file.WriteLine("T_elin0,T_k0,T_K,T_btout,T_btout0,,,,,,,,,");
                file.WriteLine(Data.T_elin0.ToString() + ',' + Data.T_k0.ToString() + ',' + Data.T_K.ToString() + ',' + Data.T_btout.ToString() + ',' + Data.T_btout0.ToString() + ',');
                file.WriteLine("T_cw_in,T_cw_out0,T_ambi,T_pipeout0,T_btout_ano0,T_btout_cat0,,,,,,,,,");
                file.WriteLine(Data.T_cw_in.ToString() + ',' + Data.T_cw_out0.ToString() + ',' + Data.T_ambi.ToString() + ',' + Data.T_pipeout0.ToString() + ',' + Data.T_btout_ano0.ToString() + ',' + Data.T_btout_cat0.ToString() + ',');
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("# 部件参数,,,,,,,,,,,,,,,");
                file.WriteLine("###########################,,,,,,,,,,,,,,,");
                file.WriteLine("Part_one,,,,,,,,,,,,,,,");
                file.WriteLine("electrolyzer,,,,,,,,,,,,,,,");
                file.WriteLine("个数,,,,,,,,,,,,,,,");
                file.WriteLine(Data.n_ele.ToString() + ',');
                for (int i = 0; i < Data.n_ele; i++)
                {
                    file.WriteLine("I_current,i_flow,i_ps,,,,,,,,,,,,,");
                    file.WriteLine(Data.ele[i].I_current.ToString() + ',' + Data.ele[i].i_flow.ToString() + ',' + Data.ele[i].i_ps.ToString() + ',');
                    file.WriteLine("flow号,,,,,,,,,,,,,,,");
                    string item_m = "";
                    for (int j = 0; j < Data.ele[i].i_flow; j++)
                    {
                        item_m = item_m + Data.ele[i].flow[j].ToString() + ",";
                    }
                    file.WriteLine(item_m);
                    file.WriteLine("ps号,,,,,,,,,,,,,,,");
                    string item_n = "";
                    for (int l = 0; l < Data.ele[i].i_ps; l++)
                    {
                        item_n = item_n + Data.ele[i].ps[l].ToString() + ",";
                    }
                    file.WriteLine(item_n);
                }

                file.WriteLine("############,,,,,,,,,,,,,,,");
                file.WriteLine("Part_two,,,,,,,,,,,,,,,");
                file.WriteLine("Cathode_separator,,,,,,,,,,,,,,,");
                file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                file.WriteLine(Data.Cathode_separator.i_flow.ToString() + ',' + Data.Cathode_separator.i_ps.ToString() + ',');
                file.WriteLine("flow号,,,,,,,,,,,,,,,");
                string item_f = "";
                for (int j = 0; j < Data.Cathode_separator.i_flow; j++)
                {
                    item_f = item_f + Data.Cathode_separator.flow[j].ToString() + ",";
                }
                file.WriteLine(item_f);
                file.WriteLine("ps号,,,,,,,,,,,,,,,");
                string item_p = "";
                for (int l = 0; l < Data.Cathode_separator.i_ps; l++)
                {
                    item_p = item_p + Data.Cathode_separator.ps[l].ToString() + ",";
                }
                file.WriteLine(item_p);

                //file.WriteLine("Cathode_separator,,,,,,,,,,,,,,,");
                //file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                //file.WriteLine(Data.Cathode_separator.i_flow.ToString() + ',' + Data.Cathode_separator.i_ps.ToString() + ',');
                //file.WriteLine("flow号,,,,,,,,,,,,,,,");
                //item_f = "";
                //for (int j = 0; j < Data.Cathode_separator.i_flow; j++)
                //{
                //    item_f = item_f + Data.Cathode_separator.flow[j].ToString() + ",";
                //}
                //file.WriteLine(item_f);
                //file.WriteLine("ps号,,,,,,,,,,,,,,,");
                //item_p = "";
                //for (int l = 0; l < Data.Cathode_separator.i_ps; l++)
                //{
                //    item_p = item_p + Data.Cathode_separator.ps[l].ToString() + ",";
                //}
                //file.WriteLine(item_p);

                file.WriteLine("Anode_separator,,,,,,,,,,,,,,,");
                file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                file.WriteLine(Data.Anode_separator.i_flow.ToString() + ',' + Data.Anode_separator.i_ps.ToString() + ',');
                file.WriteLine("flow号,,,,,,,,,,,,,,,");
                item_f = "";
                for (int j = 0; j < Data.Anode_separator.i_flow; j++)
                {
                    item_f = item_f + Data.Anode_separator.flow[j].ToString() + ",";
                }
                file.WriteLine(item_f);
                file.WriteLine("ps号,,,,,,,,,,,,,,,");
                item_p = "";
                for (int l = 0; l < Data.Anode_separator.i_ps; l++)
                {
                    item_p = item_p + Data.Anode_separator.ps[l].ToString() + ",";
                }
                file.WriteLine(item_p);

                file.WriteLine("############,,,,,,,,,,,,,,,");
                file.WriteLine("Part_three,,,,,,,,,,,,,,,");
                file.WriteLine("Balance_line,,,,,,,,,,,,,,,");
                file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                file.WriteLine(Data.Balance_line.i_flow.ToString() + ',' + Data.Balance_line.i_ps.ToString() + ',');
                file.WriteLine("flow号,,,,,,,,,,,,,,,");
                item_f = "";
                for (int j = 0; j < Data.Balance_line.i_flow; j++)
                {
                    item_f = item_f + Data.Balance_line.flow[j].ToString() + ",";
                }
                file.WriteLine(item_f);
                file.WriteLine("ps号,,,,,,,,,,,,,,,");
                item_p = "";
                for (int l = 0; l < Data.Balance_line.i_ps; l++)
                {
                    item_p = item_p + Data.Balance_line.ps[l].ToString() + ",";
                }
                file.WriteLine(item_p);

                file.WriteLine("Cathode_valve,,,,,,,,,,,,,,,");
                file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                file.WriteLine(Data.Cathode_valve.i_flow.ToString() + ',' + Data.Cathode_valve.i_ps.ToString() + ',');
                file.WriteLine("flow号,,,,,,,,,,,,,,,");
                item_f = "";
                for (int j = 0; j < Data.Cathode_valve.i_flow; j++)
                {
                    item_f = item_f + Data.Cathode_valve.flow[j].ToString() + ",";
                }
                file.WriteLine(item_f);
                file.WriteLine("ps号,,,,,,,,,,,,,,,");
                item_p = "";
                for (int l = 0; l < Data.Cathode_valve.i_ps; l++)
                {
                    item_p = item_p + Data.Cathode_valve.ps[l].ToString() + ",";
                }
                file.WriteLine(item_p);

                file.WriteLine("Anode_valve,,,,,,,,,,,,,,,");
                file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
                file.WriteLine(Data.Anode_valve.i_flow.ToString() + ',' + Data.Anode_valve.i_ps.ToString() + ',');
                file.WriteLine("flow号,,,,,,,,,,,,,,,");
                item_f = "";
                for (int j = 0; j < Data.Anode_valve.i_flow; j++)
                {
                    item_f = item_f + Data.Anode_valve.flow[j].ToString() + ",";
                }
                file.WriteLine(item_f);
                file.WriteLine("ps号,,,,,,,,,,,,,,,");
                item_p = "";
                for (int l = 0; l < Data.Anode_valve.i_ps; l++)
                {
                    item_p = item_p + Data.Anode_valve.ps[l].ToString() + ",";
                }
                file.WriteLine(item_p);
            }
        }
        #endregion

        public static List<List<string>> data1;
		public static List<List<string>> data2;
		public static List<List<string>> data3;
		public static List<List<string>> data4;
		public static List<List<string>> data5;
		public static List<List<string>> data6;
		public static List<List<string>> data11;
		public static List<List<string>> data12;
		public static List<List<string>> data13;
		public static List<List<string>> data14;



		public static bool[] data1_check;//工艺流程-阴极压力  列勾选情况
        public static bool[] data2_check;//工艺流程-阴极分离器氢气含量  列勾选情况
        public static bool[] data3_check;//工艺流程-阳极压力  列勾选情况
        public static bool[] data4_check;//工艺流程-阳极分离器氧气含量  列勾选情况
        public static bool[] data5_check;//曲线显示设置  列勾选情况
		public static bool[] data6_check;
		public static bool[] data7_check;//工艺流程-电解电流  列勾选情况
		public static bool[] data8_check;//工艺流程-温度  列勾选情况
		public static bool[] data9_check;//工艺流程-氧中氢  列勾选情况
		public static bool[] data10_check;//工艺流程-氢中氧  列勾选情况
		public static bool[] data11_check;//数据回放-图表-1  列勾选情况
		public static bool[] data12_check;//数据回放-图表-2  列勾选情况
		public static bool[] data13_check;//数据回放-图表-3  列勾选情况
		public static bool[] data17_check;//仿真结果  列勾选情况

        public static double data1_left, data1_right;   //工艺流程-电解电流  时间阈值
        public static double data2_left, data2_right;   //工艺流程-温度  时间阈值
        public static double data3_left, data3_right;   //工艺流程-氧中氢  时间阈值
        public static double data4_left, data4_right; //工艺流程-氢中氧  时间阈值

        public static double data7_left, data7_right;	//工艺流程-电解电流  时间阈值
		public static double data8_left, data8_right;	//工艺流程-温度  时间阈值
		public static double data9_left, data9_right;	//工艺流程-氧中氢  时间阈值
		public static double data10_left, data10_right;	//工艺流程-氢中氧  时间阈值

		public const int n_flow_max = 1000;//flow参数
		public static FlowData[] flow = new FlowData[n_flow_max]; //全局变量，存储
		public static FlowData[] flow_f = new FlowData[n_flow_max]; //全局变量，存储

		public const int n_faultflow_max = 1000;//faultflow参数
		public static FaultFlowData[] faultflow = new FaultFlowData[n_faultflow_max]; //全局变量，存储

		public const int n_ps_max = 1000;//ps参数
		public static PsData[] ps = new PsData[n_ps_max]; //全局变量，存储
		public static PsData[] ps_f = new PsData[n_ps_max]; //全局变量，存储

		public const int n_faultps_max = 1000;//faultps参数
		public static FaultPsData[] faultps = new FaultPsData[n_faultps_max]; //全局变量，存储

		public const int n_node_max = 1000;//部件参数
		public static NodeData[] ele = new NodeData[n_node_max]; //电解槽  数组---多个
		public static NodeData Cathode_separator = new NodeData();//阴极分离器
		public static NodeData Anode_separator = new NodeData();//阳极分离器
		public static NodeData Balance_line = new NodeData();//平衡管线
		public static NodeData Cathode_valve = new NodeData();//阴极阀门
		public static NodeData Anode_valve = new NodeData();//阳极阀门

		public const int n_case_max = 1000;//工况参数
		public static CaseData[] case_data = new CaseData[n_case_max]; //全局变量，存储

		public static string openFile, saveFile, fileName, filePath, exePath, casePath, caseUsePath, newFolderPath, newFolderName;  // 文件名，包含路径，用于存储文件

	}

}
