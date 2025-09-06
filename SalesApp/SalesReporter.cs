using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SalesApp;

public static class SalesReporter
{
    public static void WriteSalesSummary(string salesDirectory, string summaryFilePath)
    {
        if (!Directory.Exists(salesDirectory))
            throw new DirectoryNotFoundException($"Directory not found: {salesDirectory}");

        var sb = new StringBuilder();
        sb.AppendLine("Sales Summary");
        sb.AppendLine("----------------------------");

        decimal grandTotal = 0m;
        var details = new List<(string FileName, decimal Total)>();

        foreach (var file in Directory.EnumerateFiles(salesDirectory))
        {
            decimal fileTotal = 0m;

            foreach (var line in File.ReadLines(file))
            {
                foreach (Match m in Regex.Matches(line, @"[+-]?(?:\p{Sc}\s*)?(\d{1,3}(?:[.,]\d{3})*|\d+)(?:[.,]\d{2})?"))
                {
                    var raw = m.Value.Trim();
                    if (decimal.TryParse(raw, NumberStyles.Number | NumberStyles.AllowCurrencySymbol,
                                         CultureInfo.CurrentCulture, out var amount))
                    {
                        fileTotal += amount;
                    }
                }
            }

            details.Add((Path.GetFileName(file), fileTotal));
            grandTotal += fileTotal;
        }

        sb.AppendLine($" Total Sales: {grandTotal.ToString("C")}");
        sb.AppendLine();
        sb.AppendLine(" Details:");
        foreach (var (fileName, total) in details)
        {
            sb.AppendLine($"  {fileName}: {total.ToString("C")}");
        }

        File.WriteAllText(summaryFilePath, sb.ToString());
    }
}
