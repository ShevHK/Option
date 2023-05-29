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

        public double NormalInverseGaussian(double S, double K, double t, double r, double sigma)
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

        public double CalculateDelta(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = NormalInverseGaussian(S, K, t, r, sigma);
            double priceWithIncrement = NormalInverseGaussian(S + epsilon, K, t, r, sigma);
            double delta = (priceWithIncrement - price) / epsilon;
            return delta;
        }

        public double CalculateGamma(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double delta = CalculateDelta(S, K, t, r, sigma);
            double deltaWithIncrement = CalculateDelta(S + epsilon, K, t, r, sigma);
            double gamma = (deltaWithIncrement - delta) / epsilon;
            return gamma;
        }

        public double CalculateVega(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = NormalInverseGaussian(S, K, t, r, sigma);
            double priceWithIncrement = NormalInverseGaussian(S, K, t, r, sigma + epsilon);
            double vega = (priceWithIncrement - price) / epsilon;
            return vega;
        }

        public double CalculateTheta(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = NormalInverseGaussian(S, K, t, r, sigma);
            double priceWithDecrement = NormalInverseGaussian(S, K, t - epsilon, r, sigma);
            double theta = (priceWithDecrement - price) / epsilon;
            return theta;
        }

        public double CalculateRho(double S, double K, double t, double r, double sigma)
        {
            double epsilon = 0.001; // small increment
            double price = NormalInverseGaussian(S, K, t, r, sigma);
            double priceWithIncrement = NormalInverseGaussian(S, K, t, r + epsilon, sigma);
            double rho = (priceWithIncrement - price) / epsilon;
            return rho;
        }
    }
}
