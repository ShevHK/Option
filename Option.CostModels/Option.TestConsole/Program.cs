
using CsvHelper;
using Option.CostModels;
using System.Globalization;
using YahooFinanceApi;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
// ...
//Extreme.License.Verify("38700-37500-64359-47856");



//BSMModel pricing = new BSMModel();

//double S = 100; // Ціна активу
//double K = 95; // Ціна страйку
//double t = 0.5; // Термін дії опціону (у днях)
//double r = 0.05; // Безризикова відсоткова ставка
//double sigma = 0.2; // Волатильність активу
//Console.WriteLine("S = " + S);
//Console.WriteLine("K = " + K);
//Console.WriteLine("t = " + t);
//Console.WriteLine("r = " + r);
//Console.WriteLine("sigma = " + sigma);
//Console.WriteLine("");

//double callValue = pricing.BsmCallValue(S, K, t, r, sigma);
//double putCallParity = pricing.PutCallParity(S, K, t, r, sigma);
//double deltaCall = pricing.DeltaCall(S, K, t, r, sigma);
//double gammaCall = pricing.GammaCall(S, K, t, r, sigma);
//double vegaCall = pricing.VegaCall(S, K, t, r, sigma);
//double thetaCall = pricing.ThetaCall(S, K, t, r, sigma);
//double rhoCall = pricing.RhoCall(S, K, t, r, sigma);

//Console.WriteLine("BSMModel");
//Console.WriteLine("Call Value: " + callValue);
//Console.WriteLine("Put-Call Parity: " + putCallParity);
//Console.WriteLine("Delta: " + deltaCall);
//Console.WriteLine("Gamma: " + gammaCall);
//Console.WriteLine("Vega: " + vegaCall);
//Console.WriteLine("Theta: " + thetaCall);
//Console.WriteLine("Rho: " + rhoCall);
//Console.WriteLine("");


//double callOptionPrice = CRRModel.CalculateOptionPrice(S, K, t, r, sigma);
//double putOptionPrice = CRRModel.CalculatePutOptionPrice(S, K, t, r, sigma);
//double delta1 = CRRModel.CalculateDelta(S, K, t, r, sigma);
//double gamma1 = CRRModel.CalculateGamma(S, K, t, r, sigma);
//double vega1 = CRRModel.CalculateVega(S, K, t, r, sigma);
//double theta1 = CRRModel.CalculateTheta(S, K, t, r, sigma);
//double rho1 = CRRModel.CalculateRho(S, K, t, r, sigma);

//Console.WriteLine("CRRModel");
//Console.WriteLine("Call Option Price: " + callOptionPrice);
//Console.WriteLine("Put Option Price: " + putOptionPrice);
//Console.WriteLine("Delta: " + delta1);
//Console.WriteLine("Gamma: " + gamma1);
//Console.WriteLine("Vega: " + vega1);
//Console.WriteLine("Theta: " + theta1);
//Console.WriteLine("Rho: " + rho1);
//Console.WriteLine("");




//Console.WriteLine("MonteCarloModel");

//MonteCarloModel model = new MonteCarloModel();

//double callOptionPrice2 = model.CallOptionPrice(S, K, t, r, sigma);
//double putOptionPrice2 = model.PutOptionPrice(S, K, t, r, sigma);

//Console.WriteLine("Call Option Price: " + callOptionPrice2);
//Console.WriteLine("Put Option Price: " + putOptionPrice2);
//Console.WriteLine("");

//Console.WriteLine("LeviModel");
//LevyModel levyModel = new LevyModel();
//double optionLeviPrice = levyModel.NormalInverseGaussian(S, K, t, r, sigma);
//double delta = levyModel.CalculateDelta(S, K, t, r, sigma);
//double gamma = levyModel.CalculateGamma(S, K, t, r, sigma);
//double vega = levyModel.CalculateVega(S, K, t, r, sigma);
//double theta = levyModel.CalculateTheta(S, K, t, r, sigma);
//double rho = levyModel.CalculateRho(S, K, t, r, sigma);

//Console.WriteLine("Ціна опціону: " + optionLeviPrice);
//Console.WriteLine("Delta: " + delta);
//Console.WriteLine("Gamma: " + gamma);
//Console.WriteLine("Vega: " + vega);
//Console.WriteLine("Theta: " + theta);
//Console.WriteLine("Rho: " + rho);
//Console.WriteLine("");

//double S = 100; // Ціна активу
//double K = 95; // Ціна страйку
//double t = 30; // Термін дії опціону (у днях)
//double r = 0.05; // Безризикова відсоткова ставка
//double sigma = 0.2; // Волатильність активу

string symbol = "DIS";

// Визначте період, для якого потрібно отримати історію (останній рік від сьогодні)
DateTime startTime = DateTime.Now.AddYears(-1);
DateTime endTime = DateTime.Now;

// Отримання історії цін акції з використанням Yahoo Finance API
var historicalData = await Yahoo.GetHistoricalAsync(symbol, startTime, endTime);

// Запис історії цін акції в текстовий файл
string filePath = $"../../../../symbols/{symbol}.txt";
using (StreamWriter writer = new StreamWriter(filePath))
{
    foreach (var candle in historicalData)
    {
        writer.WriteLine($"{candle.DateTime},{candle.Open},{candle.High},{candle.Low},{candle.Close},{candle.Volume}");
    }
}

Console.WriteLine("Historical data saved successfully.");

// Використання моделі CRR для обчислення опціонів
double S = (double)historicalData[historicalData.Count - 1].Close; // Ціна акції в останній доступній точці
double K = 200; // Ціна страйку
double r = 0.05; // Процентна ставка
double v = 0.3; // Волатильність
double T = 1; // Кількість днів до закінчення опціону
int PC = 1; // 1 для Call опціону, 0 для Put опціону

double callOptionPrice = CRRModel.CalculateOptionPrice(S, K, T, r, v);
double putOptionPrice = CRRModel.CalculatePutOptionPrice(S, K, T, r, v);

Console.WriteLine("CRR Model");
Console.WriteLine($"Call Option Price: {callOptionPrice}");
Console.WriteLine($"Put Option Price: {putOptionPrice}");

// Додаткові обчислення для отримання Delta, Gamma, Vega, Theta та Rho
