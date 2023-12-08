
using CsvHelper;
using Option.CostModels;
using System.Globalization;
using YahooFinanceApi;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;



BSMModel pricing = new BSMModel();
LevyModel levyModel = new LevyModel();


string filePath = @"D:\University\magistry1\diploma\Option.CostModels\symbols\TEST DATA.txt";

try
{
    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
        string[] values = line.Split(',');

        string date1 = values[0].Trim();
        string date2 = values[1].Trim();
        double stockPrice = Convert.ToDouble(values[2].Trim());
        double strikePrice = Convert.ToDouble(values[3].Trim());
        double callPrice = Convert.ToDouble(values[4].Trim());
        double putPrice = Convert.ToDouble(values[5].Trim());

        Console.WriteLine($"Дата укладання опціону: {date1}");
        Console.WriteLine($"Дата виконання: {date2}");
        Console.WriteLine($"Ціна акції на час укладання: {stockPrice}");
        Console.WriteLine($"Ціна страйку: {strikePrice}");
        Console.WriteLine($"Ціна опціону колл: {callPrice}");
        Console.WriteLine($"Ціна опціону пут: {putPrice}");

        Console.WriteLine(); // Перехід на новий рядок

    }
}
catch (Exception ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}