using System.Collections.Generic;

namespace ElectCell_HMI
{
    // 控制参数
    public class ControlParameter
    {
        public double start_time, end_time, delta_t;
        public bool cal_current, cal_valve, cal_pump, cal_balance_pipe, cal_mini_1, cal_mini_2, use_ff_static, IsMixed_circleType;
        public bool cal_superSat_fickTrans, cal_ShellTube_heatExchanger, cal_Ele_heater;
    }

    // 几何参数
    public class GeometryParameter
    {
        public double L_ca2p, L_an2p,L_ca2se, L_an2se, D_sc, l_sc, thickness_cat, thickness_ano, distance_am, distance_cm;
        public double Volume_hotside, Volume_codeside, di_stack, Area_sep, Area_stack, C_tsep, C_tk;
    }

    // flow参数
    public class FlowParameter
    {
        public List<List<double>> flow;
    }

    // ps参数
    public class PSParameter
    {
        public List<List<double>> ps;
    }

    // 工艺参数
    public class ProcessParameter
    {
        public double sigma_e_1, sigma_h2_r1, sigma_h2o_r1, sigma_e_2, sigma_h2o_r2, sigma_o2_r2, eta_F, F, n_cell, a_cell, A_mem;
        public double thickness_mem, porosity_mem, tortuosity_mem, wt_KOHsln, k, D_h2, D_o2, k_x_h2, k_x_o2, eps_h2_Darcy, eps_o2_Darcy;
        public double tao_b, FC_flash, R, eta, M_h2, M_o2, M_n2, M_koh, M_h2o, rho_h2o, rho_h2;
        public double rho_o2, rho_sln_koh, g, Re7_0, mu, cv1, cv2, P_cathode_sep_out, P_anode_sep_out, P_env;
        public double T_elin0, T_k0, T_K, T_btout, T_btout0;
        public double T_cw_in, T_cw_out0, T_ambi, T_pipeout0, T_btout_ano0, T_btout_cat0;
    }

    public struct ElectrolyticCell
    {
        public double current;
        public List<double> flow;
        public List<double> ps;
    }

    public struct Pump
    {
        public List<double> flow;
        public List<double> ps;
    }

    public struct CathodeSeparator
    {
        public List<double> flow;
        public List<double> ps;
    }

    public struct AnodeSeparator
    {
        public List<double> flow;
        public List<double> ps;
    }

    public struct CathodeValve
    {
        public List<double> flow;
        public List<double> ps;
    }

    public struct AnodeValve
    {
        public List<double> flow;
        public List<double> ps;
    }

    public struct BalancePipe
    {
        public List<double> flow;
        public List<double> ps;
    }

    // 部件参数
    public class ComponentParameter
    {
        public int nElectrolyticCell;
        public List<ElectrolyticCell> electrolyticCell;
        public List<Pump> pump;
        public CathodeSeparator cathodeSeparator;
        public AnodeSeparator anodeSeparator;
        public CathodeValve cathodeValve;
        public AnodeValve anodeValve;
        public BalancePipe balancePipe;
    }

    //计算结果
    public class Result
    {
        public List<string> header;
        public List<List<double>> result;
    }

    public static class Data
    {
        public static ControlParameter controlParameter = new ControlParameter();           // 控制参数
        public static GeometryParameter geometryParameter = new GeometryParameter();        // 几何参数
        public static FlowParameter flowParameter = new FlowParameter();                    // flow参数
        public static PSParameter psParameter = new PSParameter();                          // ps参数
        public static ProcessParameter processParameter = new ProcessParameter();           // 工艺参数
        public static ComponentParameter componentParameter = new ComponentParameter();     // 部件参数
        public static Result result = new Result();                                         // 计算结果
    }
}
