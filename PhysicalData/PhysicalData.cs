using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalData
{
    public class PhysicalPara
    {
        public int    matId = 1;
        public double p     = 0D;
        public double Tc    = 0D;
        public double T     = 0D;
        public double R     = 0D;
        public double M     = 0D;
        public double Rm    = 0D;
        public double rho   = 0D;
        public double h     = 0D;
        public double u     = 0D;
        public double cv    = 0D;
        public double z     = 0D;

        public PhysicalPara()
        {
            
        }

        //public double calRho(double tt,double pp)
        //{
        //    return (-1.1671d - 16 * Math.Pow(pp,4) + 0.000000000000035429 * Math.Pow(pp, 3) - 0.00000000000380467 * Math.Pow(pp, 3) + 0.000000000151947 * pp
        //            - 0.00000000000376254) * Math.Pow(tt, 4) + (0.000000000000159364 * Math.Pow(pp, 4) - 0.0000000000491286 * Math.Pow(pp, 3) + 0.00000000538378 * Math.Pow(pp, 2) - 0.000000222007 * pp
        //            + 0.00000000512189) * Math.Pow(tt, 3) + (-0.0000000000826768 * Math.Pow(pp, 4) + 0.000000026014 * Math.Pow(pp, 3) - 0.00000293356 * Math.Pow(pp, 2) + 0.00012714 * pp
        //            - 0.00000263185) * Math.Pow(tt, 2) + (0.0000000195877 * Math.Pow(pp, 4) - 0.00000634261 * Math.Pow(pp, 3) + 0.0007478 * Math.Pow(pp, 2) - 0.0354828 * pp + 0.000608078) * tt
        //            + (-0.0000018437 * Math.Pow(pp, 4) + 0.000623884 * Math.Pow(pp, 3) - 0.0798237 * Math.Pow(pp, 2) + 4.77618 * pp - 0.0536549);
        //}

        //public double calH(double tt, double pp)
        //{
        //    return (4.92522d - 15 * Math.Pow(pp, 5) - 0.0000000000016076 * Math.Pow(pp, 4) + 0.000000000214858 * Math.Pow(pp, 3) - 0.0000000146189 * Math.Pow(pp, 2) + 0.000000454324 * pp - 0.00000987705) * Math.Pow(tt, 3)
        //            + (0.00000000055184 * Math.Pow(pp, 4) - 0.000000141472 * Math.Pow(pp, 3) + 0.0000132881 * Math.Pow(pp, 2) - 0.000497343 * pp + 0.0108438) * Math.Pow(tt, 2)
        //            + (0.00000000202796 * Math.Pow(pp, 5) - 0.000000664561 * Math.Pow(pp, 4) + 0.0000901478 * Math.Pow(pp, 3) - 0.00639724 * Math.Pow(pp, 2) + 0.225139 * pp + 10.4372) * tt
        //            + (-0.000000255167 * Math.Pow(pp, 5) + 0.0000852825 * Math.Pow(pp, 4) - 0.0119306 * Math.Pow(pp, 3) + 0.894471 * Math.Pow(pp, 2) - 27.6592 * pp + 112.034);
        //}

        //public double calU(double tt, double pp)
        //{
        //    return 496.1 * ((-0.000000251102 * Math.Pow(pp, 2) + 0.00003270544 * pp + 0.020635744157) * tt 
        //            +(0.000110237178 * Math.Pow(pp, 2) - 0.014948338423 * pp - 0.706955972653));
        //}

        //public double calCv(double tt, double pp)
        //{
        //    return 0.49608 * ((-2.6305218d - 13 * Math.Pow(tt, 3) + 2.9504059885d - 10 * Math.Pow(tt, 2) - 0.000000114316 * tt + 0.000015724461) * Math.Pow(pp, 3)
        //            + (2.239406732d - 11 * Math.Pow(tt, 3) - 0.000000026242 * Math.Pow(tt, 2) + 0.000010846506 * tt - 0.001653192666) * Math.Pow(pp, 2)
        //            + (-0.000000000909584 * Math.Pow(tt, 3) + 0.000001163147 * Math.Pow(tt, 2) - 0.000549326926 * tt + 0.106819055) * pp
        //            + (0.000000183407 * Math.Pow(tt, 3) - 0.000222580465 * Math.Pow(tt, 2) + 0.091271823485 * tt + 8.231398432926));
        //}

        //public double calZ(double tt, double pp, double rm, double rm)
        //{
        //    return pp * 1e6 / tt / rho / rm;
        //}

        //--------------------------------------------------------------------------------------------
        public void setPara()
        {
            
            Rm = R * 1000 / M;
            //T = Tc + 273;
        }

        public void calRho()
        {
            rho= (-1.1671e-16 * Math.Pow(p, 4) + 0.000000000000035429 * Math.Pow(p, 3) - 0.00000000000380467 * Math.Pow(p, 3) + 0.000000000151947 * p
                    - 0.00000000000376254) * Math.Pow(T, 4) + (0.000000000000159364 * Math.Pow(p, 4) - 0.0000000000491286 * Math.Pow(p, 3) + 0.00000000538378 * Math.Pow(p, 2) - 0.000000222007 * p
                    + 0.00000000512189) * Math.Pow(T, 3) + (-0.0000000000826768 * Math.Pow(p, 4) + 0.000000026014 * Math.Pow(p, 3) - 0.00000293356 * Math.Pow(p, 2) + 0.00012714 * p
                    - 0.00000263185) * Math.Pow(T, 2) + (0.0000000195877 * Math.Pow(p, 4) - 0.00000634261 * Math.Pow(p, 3) + 0.0007478 * Math.Pow(p, 2) - 0.0354828 * p + 0.000608078) * T
                    + (-0.0000018437 * Math.Pow(p, 4) + 0.000623884 * Math.Pow(p, 3) - 0.0798237 * Math.Pow(p, 2) + 4.77618 * p - 0.0536549);
        }

        public void calH()
        {
            h= (4.92522e-15 * Math.Pow(p, 5) - 0.0000000000016076 * Math.Pow(p, 4) + 0.000000000214858 * Math.Pow(p, 3) - 0.0000000146189 * Math.Pow(p, 2) + 0.000000454324 * p - 0.00000987705) * Math.Pow(T, 3)
                    + (0.00000000055184 * Math.Pow(p, 4) - 0.000000141472 * Math.Pow(p, 3) + 0.0000132881 * Math.Pow(p, 2) - 0.000497343 * p + 0.0108438) * Math.Pow(T, 2)
                    + (0.00000000202796 * Math.Pow(p, 5) - 0.000000664561 * Math.Pow(p, 4) + 0.0000901478 * Math.Pow(p, 3) - 0.00639724 * Math.Pow(p, 2) + 0.225139 * p + 10.4372) * T
                    + (-0.000000255167 * Math.Pow(p, 5) + 0.0000852825 * Math.Pow(p, 4) - 0.0119306 * Math.Pow(p, 3) + 0.894471 * Math.Pow(p, 2) - 27.6592 * p + 112.034);
        }

        public void calU()
        {
            u= 496.1 * ((-0.000000251102 * Math.Pow(p, 2) + 0.00003270544 * p + 0.020635744157) * T
                    + (0.000110237178 * Math.Pow(p, 2) - 0.014948338423 * p - 0.706955972653));
        }

        public void calCv()
        {
            cv= 0.49608 * ((-2.6305218e-13 * Math.Pow(T, 3) + 2.9504059885e-10 * Math.Pow(T, 2) - 0.000000114316 * T + 0.000015724461) * Math.Pow(p, 3)
                    + (2.239406732e-11 * Math.Pow(T, 3) - 0.000000026242 * Math.Pow(T, 2) + 0.000010846506 * T - 0.001653192666) * Math.Pow(p, 2)
                    + (-0.000000000909584 * Math.Pow(T, 3) + 0.000001163147 * Math.Pow(T, 2) - 0.000549326926 * T + 0.106819055) * p
                    + (0.000000183407 * Math.Pow(T, 3) - 0.000222580465 * Math.Pow(T, 2) + 0.091271823485 * T + 8.231398432926));
        }

        public void calZ()
        {
            z= p * 1e6 / T / rho / Rm;
        }

        

    }
}
