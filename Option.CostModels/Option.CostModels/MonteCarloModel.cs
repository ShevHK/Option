using System;

public class MonteCarloModel
{
    private Random random;

    public MonteCarloModel()
    {
        this.random = new Random();
    }

    private double StandardNormalRandom()
    {
        return Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) * Math.Sin(2.0 * Math.PI * random.NextDouble());
    }

    public double CallOptionPrice(double S, double K, double T, double r, double sigma)
    {
        double sumPayoff = 0.0;
        int M = 10000; // число симуляцій

        for (int i = 0; i < M; i++)
        {
            double ST = S * Math.Exp((r - 0.5 * sigma * sigma) * T + sigma * Math.Sqrt(T) * StandardNormalRandom());
            double payoff = Math.Max(ST - K, 0);
            sumPayoff += payoff;
        }

        return Math.Exp(-r * T) * (sumPayoff / (double)M);
    }

    public double PutOptionPrice(double S, double K, double T, double r, double sigma)
    {
        double sumPayoff = 0.0;
        int M = 10000; // число симуляцій

        for (int i = 0; i < M; i++)
        {
            double ST = S * Math.Exp((r - 0.5 * sigma * sigma) * T + sigma * Math.Sqrt(T) * StandardNormalRandom());
            double payoff = Math.Max(K - ST, 0);
            sumPayoff += payoff;
        }

        return Math.Exp(-r * T) * (sumPayoff / (double)M);
    }
}
