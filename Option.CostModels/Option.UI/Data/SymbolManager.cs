using Option.UI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace SymbolManagerNamespace
{
    public class SymbolManager
    {
        private const string SymbolsDirectory = "../symbols/";

        public async Task FetchHistoricalDataAsync(SymbolsEnum symbol)
        {
            string symbolFileName = GetSymbolFileName(symbol);

            DateTime lastDate = await GetLastDateAsync(symbolFileName);

            DateTime endDate = DateTime.Today;
            if (lastDate.Date >= endDate.Date)
            {
                Console.WriteLine($"Historical data for symbol {symbol} is up to date.");
                return;
            }

            List<Candle> historicalData;
            if (lastDate == DateTime.MinValue)
            {
                DateTime startDate = endDate.AddYears(-10); // Вибрати період за останні 10 років
                historicalData = (await Yahoo.GetHistoricalAsync(symbol.ToString(), startDate, endDate)).ToList();
            }
            else
            {
                historicalData = (await Yahoo.GetHistoricalAsync(symbol.ToString(), lastDate.AddDays(1), endDate)).ToList();
            }

            if (historicalData.Count > 0)
            {
                using (StreamWriter writer = File.AppendText(symbolFileName))
                {
                    foreach (Candle candle in historicalData)
                    {
                        writer.WriteLine($"{candle.DateTime},{candle.Open},{candle.High},{candle.Low},{candle.Close},{candle.Volume}");
                    }
                }

                Console.WriteLine($"Fetched {historicalData.Count} new records for symbol {symbol}.");
            }
        }


        public async Task<List<Candle>> ReadHistoricalDataAsync(SymbolsEnum symbol)
        {
            string symbolFileName = GetSymbolFileName(symbol);

            List<Candle> historicalData = new List<Candle>();

            using (StreamReader reader = File.OpenText(symbolFileName))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] fields = line.Split(',');
                    DateTime dateTime = DateTime.Parse(fields[0]);
                    double open = double.Parse(fields[1]);
                    double high = double.Parse(fields[2]);
                    double low = double.Parse(fields[3]);
                    double close = double.Parse(fields[4]);
                    long volume = long.Parse(fields[5]);

                    Candle candle = new Candle()
                    {
                        DateTime = dateTime,
                        Open = (decimal)open,
                        High = (decimal)high,
                        Low = (decimal)low,
                        Close = (decimal)close,
                        Volume = volume
                    };
                    historicalData.Add(candle);
                }
            }

            return historicalData;
        }

        public async Task FetchAllSymbolsHistoricalDataAsync()
        {
            foreach (SymbolsEnum symbol in Enum.GetValues(typeof(SymbolsEnum)))
            {
                await FetchHistoricalDataAsync(symbol);
            }
        }

        private string GetSymbolFileName(SymbolsEnum symbol)
        {
            return $"{SymbolsDirectory}{symbol}.txt";
        }

        private async Task<DateTime> GetLastDateAsync(string symbolFileName)
        {
            DateTime lastDate = DateTime.MinValue;

            if (File.Exists(symbolFileName))
            {
                using (StreamReader reader = File.OpenText(symbolFileName))
                {
                    string lastLine = await reader.ReadToEndAsync();
                    string[] fields = lastLine.Split(',');
                    lastDate = DateTime.Parse(fields[0]);
                }
            }

            return lastDate;
        }
    }
}
