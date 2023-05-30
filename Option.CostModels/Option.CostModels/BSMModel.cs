using System;
using MathNet.Numerics.Distributions;

namespace Option.CostModels
{
    public class BSMModel
    {
        public static double CallValue(double S, double K, double T, double r, double sigma)
        {
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double d2 = (Math.Log(S / K) + (r - 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double value = (S * Normal.CDF(0.0, 1.0, d1) - K * Math.Exp(-r * T) * Normal.CDF(0.0, 1.0, d2));
            return value;
        }

        public static double PutCallParity(double S, double K, double T, double r, double sigma)
        {
            double value = (K * Math.Exp(-r * T) - S + CallValue(S, K, T, r, sigma));
            return value;
        }

        public static double DeltaCall(double S, double K, double T, double r, double sigma)
        {
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double value = Normal.CDF(0.0, 1.0, d1);
            return value;
        }

        public static double GammaCall(double S, double K, double T, double r, double sigma)
        {
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double value = Normal.PDF(0.0, 1.0, d1) / (S * sigma * Math.Sqrt(T));
            return value;
        }

        public static double VegaCall(double S, double K, double T, double r, double sigma)
        {
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double value = S * Math.Sqrt(T) * Normal.PDF(0.0, 1.0, d1);
            return value;
        }

        public static double ThetaCall(double S, double K, double T, double r, double sigma)
        {
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double d2 = d1 - sigma * Math.Sqrt(T);
            double value = -(S * Normal.PDF(0.0, 1.0, d1) * sigma) / (2 * Math.Sqrt(T)) - r * K * Math.Exp(-r * T) * Normal.CDF(0.0, 1.0, d2);
            return value;
        }


        public static double RhoCall(double S, double K, double T, double r, double sigma)
        {
            double d2 = (Math.Log(S / K) + (r - 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double value = K * T * Math.Exp(-r * T) * MathNet.Numerics.Distributions.Normal.CDF(0.0, 1.0, d2);
            return value;
        }
    }
}
