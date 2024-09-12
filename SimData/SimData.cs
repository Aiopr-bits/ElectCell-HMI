using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimData
{
    //路由表
    public class SimPara
    {
        public string name;
        public int type, loc, nd, up, down;
        public double[] des;
        //public double[] sim;

        public SimPara(bool flag)
        {
            name = "";
            type = 0;
            loc = 0;
            nd = 0;
            up = 0;
            down = 0;
            des = new double[12];
            //sim = new double[16];
            for (int i = 0; i < 12; i++)
            {
                des[i] = 0D;
                //sim[i] = 0D;
            }
        }
    }

    public class ProcPara
    {
        public int n;
        public int[] part;
        
        public ProcPara(bool flag)
        {
            n = 0;
            part = new int[20];
            for (int i = 0; i < 20; i++)
            {
                part[i] = 0;
                //sim[i] = 0D;
            }
        }
    }

    //部件：长管拖车
    public class TrailerPara
    {
        //输入参数
        public string name;
        public double T1;    //初始温度
        public double p1;    //初始压力
        public double V;     //体积
        public double Z;     //压缩机频率

        //其他单元传入量
        public double ma;     //每个循环吸入膜腔的气体质量

        //输出参数
        public double rho;    //密度
        public double Qm;     //质量流量

        public TrailerPara(bool flag)
        {
            name = "";
            T1 = 0D;
            p1 = 0D;
            V = 0D;
            Z = 0D;
            ma = 0D;
            rho = 0D;
            Qm = 0D;            
        }
    }

    //部件：管路
    public class PipePara
    {
        //输入参数
        public string name;
        public double l;    //长度
        public double d;    //直径
        public double nu;     //动力粘度
        public double epsilon;     //绝对粗糙度

        //其他单元传入量
        public double rho;     //每个循环吸入膜腔的气体质量
        public double Qm;     //质量流量
        
        //输出参数
        public double u;      //流速
        public double dp;     //压差
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度
        

        public PipePara(bool flag)
        {
            name = "";
            l = 0D;
            d = 0D;
            nu = 0D;
            epsilon = 0D;
            rho = 0D;
            Qm = 0D;
            u = 0D;
            dp = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：开关阀
    public class SwitchValvePara
    {
        //输入参数
        public string name;
        public double xi;    //局部阻力系数
        public double c;    //流量系数
        
        //其他单元传入量
        public double rho;     //每个循环吸入膜腔的气体质量
        
        //输出参数
        public double u;      //流速
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度


        public SwitchValvePara(bool flag)
        {
            name = "";
            xi = 0D;
            c = 0D;
            rho = 0D;
            u = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：缓冲罐
    public class TankPara
    {
        //输入参数
        public string name;
        public double d0;    //局部阻力系数
        public double d1;    //流量系数
        public double d2;    //流量系数

        //其他单元传入量
        public double rho;     //每个循环吸入膜腔的气体质量

        //输出参数
        public double A0;      //流速
        public double A1;      //流速
        public double A2;      //流速
        public double u;      //流速
        public double xi1;      //流速
        public double xi2;      //流速
        public double dp1;      //流速
        public double dp2;      //流速
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度


        public TankPara(bool flag)
        {
            name = "";
            d0 = 0D;
            d1 = 0D;
            d2 = 0D;
            rho = 0D;
            A0 = 0D;
            A1 = 0D;
            A2 = 0D;
            u = 0D;
            xi1 = 0D;
            xi2 = 0D;
            dp1 = 0D;
            dp2 = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：过滤器
    public class FilterPara
    {
        //输入参数
        public string name;
        public double xi;    //局部阻力系数
        
        //其他单元传入量
        public double rho;     //每个循环吸入膜腔的气体质量
        public double u;      //流速

        //输出参数
        public double dp;      //流速
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度


        public FilterPara(bool flag)
        {
            name = "";
            xi = 0D;
            rho = 0D;
            u = 0D;
            dp = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：入口阀
    public class InletValvePara
    {
        //输入参数
        public string name;
        public double xi;    //局部阻力系数
        public double din;    //流量系数

        //其他单元传入量
        public double rho;     //每个循环吸入膜腔的气体质量

        //输出参数
        public double u;      //流速
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度


        public InletValvePara(bool flag)
        {
            name = "";
            xi = 0D;
            din = 0D;
            rho = 0D;
            u = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：压缩机
    public class CompressorPara
    {
        //输入参数
        public string name;
        public double Vh;    //局部阻力系数
        public double m;    //流量系数
        public double alpha;    //流量系数
        public double lambda_p;    //流量系数
        public double lambda_T;    //流量系数
        public double ps;    //流量系数
        public double pd;    //流量系数
        public double mT;    //流量系数
        public double Zs;    //流量系数
        public double Zd;    //流量系数

        //其他单元传入量
        public double p;     //每个循环吸入膜腔的气体质量
        public double T;     //每个循环吸入膜腔的气体质量

        //输出参数
        public double epsilon;      //流速
        public double lambda_V;      //流速
        public double lambda_1;      //流速
        public double W;      //流速
        public double N;      //流速
        public double T1;     //温度
        public double p1;     //压力
        public double rho1;   //密度


        public CompressorPara(bool flag)
        {
            name = "";
            Vh = 0D;
            m = 0D;
            alpha = 0D;
            lambda_p = 0D;
            lambda_T = 0D;
            ps = 0D;
            pd = 0D;
            mT = 0D;
            Zs = 0D;
            Zd = 0D;
            p = 0D;
            T = 0D;
            lambda_V = 0D;
            lambda_1 = 0D;
            W = 0D;
            N = 0D;
            T1 = 0D;
            p1 = 0D;
            rho1 = 0D;
        }
    }

    //部件：储气罐
    public class GasholderPara
    {
        //输入参数
        public string name;
        public double V;    //局部阻力系数
        

        //其他单元传入量
        public double T;    //流量系数
        public double P;    //流量系数

        //输出参数
        public double rho1;   //密度


        public GasholderPara(bool flag)
        {
            name = "";
            V = 0D;
            T = 0D;
            P = 0D;
            rho1 = 0D;
        }
    }

    //腔膜
    public class MembranePara
    {
        public double pg;
        public double pl;
        public double dpm;
        public double Z;
        public double W;
        public double W0;
        public double mu;
        public double r_m;
        public double E;
        public double h;
        public double pt;
        public double ps;
        public double pd;
        public double dpv;
        public double dpe;
        public double Fp;
        public double FIS;
        public double Ff;
        public double mc;
        public double mp;
        public double ml;
        public double X;
        public double v;
        public double a;
        public double r_q;
        public double l;
        public double lambda;
        public double omega;
        public double delta_s;
        public double delta_d;
        public double alpha;
        public double S;
        public double S0;
        public double k;
        public double m;
        public double P_i;
        public double eta_m;
        public double n;
        public double area;
        public double A1;
        public double A3;
        public double V;
        public double theta;

        public MembranePara(bool flag)
        {
            pg = 0D;
            pl = 0D;
            dpm = 0D;
            Z = 0D;
            W = 0D;
            W0 = 0D;
            mu = 0D;
            r_m= 0D;
            E= 0D;
            h = 0D;
            pt = 0D;
            ps = 0D;
            pd = 0D;
            dpv= 0D;
            dpe = 0D;
            Fp = 0D;
            FIS = 0D;
            Ff= 0D;
            mc= 0D;
            mp= 0D;
            ml = 0D;
            X= 0D;
            v= 0D;
            a= 0D;
            r_q= 0D;
            l = 0D;
            lambda = 0D;
            omega = 0D;
            delta_s = 0D;
            delta_d = 0D;
            alpha = 0D;
            S= 0D;
            S0= 0D;
            k= 0D;
            m = 0D;
            P_i = 0D;
            eta_m = 0D;
            n = 0D;
            area = 0D;
            A1 = 0D;
            A3 = 0D;
            V = 0D;
            theta = 0D;




        }
    }



}
