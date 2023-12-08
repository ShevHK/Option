using System;

namespace Option.CostModels
{
    public class LevyModel
    {
        private Random random;

        public LevyModel()
        {
            this.random = new Random();
        }

        private double StandardNormalRandom()
        {
            return Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) * Math.Sin(2.0 * Math.PI * random.NextDouble());
        }
        private double LevyRandom()
        {
            double u = random.NextDouble();
            double v = random.NextDouble();

            double levyRandom = Math.Tan(Math.PI * (v - 0.5)) * (1.0 / (u * Math.Cos(Math.PI * (v - 0.5))));

            return levyRandom;
        }

        public double CallValue(double S, double K, double t, double r, double sigma)
        {
            double mu = r - 0.5 * sigma * sigma;

            int numSamples = 1000000;
            double sum = 0;

            for (int i = 0; i < numSamples; i++)
            {
                double normalRandom = StandardNormalRandom();
                double ST = S * Math.Exp(mu * t + sigma * Math.Sqrt(t) * normalRandom);
                sum += Math.Max(ST - K, 0);
            }

            double optionPrice = Math.Exp(-r * t) * (sum / numSamples);

            return optionPrice;
        }
        public double PutValue(double S, double K, double t, double r, double sigma)
        {
            double mu = r - 0.5 * sigma * sigma;

            int numSamples = 1000000;
            double sum = 0;

            for (int i = 0; i < numSamples; i++)
            {
                double levyRandom = StandardNormalRandom();
                double ST = S * Math.Exp(mu * t + sigma * Math.Sqrt(t) * levyRandom);
                sum += Math.Max(K - ST, 0);
            }

            double optionPrice = Math.Exp(-r * t) * (sum / numSamples);

            return optionPrice;
        }


        public double DeltaCall(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = CallValue(S, K, t, r, sigma);
            double priceWithIncrement = CallValue(S + epsilon, K, t, r, sigma);
            double delta = (priceWithIncrement - price) / epsilon;
            return delta;
        }

        public double GammaCall(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double delta = DeltaCall(S, K, t, r, sigma);
            double deltaWithIncrement = DeltaCall(S + epsilon, K, t, r, sigma);
            double gamma = (deltaWithIncrement - delta) / epsilon;
            return gamma;
        }

        public double VegaCall(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = CallValue(S, K, t, r, sigma);
            double priceWithIncrement = CallValue(S, K, t, r, sigma + epsilon);
            double vega = (priceWithIncrement - price) / epsilon;
            return vega;
        }

        public double ThetaCall(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = CallValue(S, K, t, r, sigma);
            double priceWithDecrement = CallValue(S, K, t - epsilon, r, sigma);
            double theta = (priceWithDecrement - price) / epsilon;
            return theta;
        }

        public double RhoCall(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = CallValue(S, K, t, r, sigma);
            double priceWithIncrement = CallValue(S, K, t, r + epsilon, sigma);
            double rho = (priceWithIncrement - price) / epsilon;
            return rho;
        }
    }
}
