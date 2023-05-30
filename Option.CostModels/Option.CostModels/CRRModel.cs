using System;

namespace Option.CostModels
{
    public class CRRModel
    {
        public static double CallValue(
            double S, double K, double T,
            double r, double sigma)
        {
            int numberOfSteps = 100; // Кількість кроків в біноміальному дереві

            double deltaT = T / numberOfSteps;
            double upFactor = Math.Exp(sigma * Math.Sqrt(deltaT));
            double downFactor = 1 / upFactor;
            double discountFactor = Math.Exp(-r * deltaT);

            double[,] prices = new double[numberOfSteps + 1, numberOfSteps + 1];

            for (int i = 0; i <= numberOfSteps; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    prices[i, j] = S * Math.Pow(upFactor, j) * Math.Pow(downFactor, i - j);
                }
            }

            double[,] optionValues = new double[numberOfSteps + 1, numberOfSteps + 1];

            for (int j = 0; j <= numberOfSteps; j++)
            {
                optionValues[numberOfSteps, j] = Math.Max(0, prices[numberOfSteps, j] - K);
            }

            for (int i = numberOfSteps - 1; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    double expectedOptionValue = discountFactor * (
                        0.5 * optionValues[i + 1, j] + 0.5 * optionValues[i + 1, j + 1]);

                    optionValues[i, j] = Math.Max(expectedOptionValue, prices[i, j] - K);
                }
            }

            return optionValues[0, 0];
        }

        public static double PutCallParity(
            double S, double K, double T,
            double r, double sigma)
        {
            int numberOfSteps = 100; // Кількість кроків в біноміальному дереві

            double deltaT = T / numberOfSteps;
            double upFactor = Math.Exp(sigma * Math.Sqrt(deltaT));
            double downFactor = 1 / upFactor;
            double discountFactor = Math.Exp(-r * deltaT);

            double[,] prices = new double[numberOfSteps + 1, numberOfSteps + 1];

            for (int i = 0; i <= numberOfSteps; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    prices[i, j] = S * Math.Pow(upFactor, j) * Math.Pow(downFactor, i - j);
                }
            }

            double[,] optionValues = new double[numberOfSteps + 1, numberOfSteps + 1];

            for (int j = 0; j <= numberOfSteps; j++)
            {
                optionValues[numberOfSteps, j] = Math.Max(0, K - prices[numberOfSteps, j]);
            }

            for (int i = numberOfSteps - 1; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    double expectedOptionValue = discountFactor * (
                        0.5 * optionValues[i + 1, j] + 0.5 * optionValues[i + 1, j + 1]);

                    optionValues[i, j] = Math.Max(expectedOptionValue, K - prices[i, j]);
                }
            }

            return optionValues[0, 0];
        }

        public static double DeltaCall(
            double S, double K, double T,
            double r, double sigma)
        {
            double epsilon = 0.01;
            double priceUp = CallValue(
                S + epsilon, K, T, r, sigma);
            double priceDown = CallValue(
                S - epsilon, K, T, r, sigma);

            double delta = (priceUp - priceDown) / (2 * epsilon);
            return delta;
        }

        public static double GammaCall(
            double S, double K, double T,
            double r, double sigma)
        {
            double epsilon = 0.01;
            double price = CallValue(
                S, K, T, r, sigma);
            double priceUp = CallValue(
                S + epsilon, K, T, r, sigma);
            double priceDown = CallValue(
                S - epsilon, K, T, r, sigma);

            double gamma = (priceUp - 2 * price + priceDown) / (epsilon * epsilon);
            return gamma;
        }

        public static double VegaCall(
            double S, double K, double T,
            double r, double sigma)
        {
            double epsilon = 0.0001;
            double priceUp = CallValue(
                S, K, T, r, sigma + epsilon);
            double priceDown = CallValue(
                S, K, T, r, sigma - epsilon);

            double vega = (priceUp - priceDown) / (2 * epsilon);
            return vega;
        }

        public static double ThetaCall(
            double S, double K, double T,
            double r, double sigma)
        {
            double epsilon = 0.01;
            double price = CallValue(
                S, K, T, r, sigma);
            double priceDown = CallValue(
                S, K, T - epsilon, r, sigma);

            double theta = (price - priceDown) / epsilon;
            return theta;
        }

        public static double RhoCall(
            double S, double K, double T,
            double r, double sigma)
        {
            double epsilon = 0.01;
            double price = CallValue(
                S, K, T, r, sigma);
            double priceUp = CallValue(
                S, K, T, r + epsilon, sigma);

            double rho = (priceUp - price) / epsilon;
            return rho;
        }
    }
}
