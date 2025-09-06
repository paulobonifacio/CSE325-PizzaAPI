using SalesApp;

class Program
{
    static void Main(string[] args)
    {
        var salesDir = args.Length > 0 ? args[0] : Path.Combine(Environment.CurrentDirectory, "SalesData");
        var outPath  = args.Length > 1 ? args[1] : Path.Combine(Environment.CurrentDirectory, "Sales Summary.txt");

        Directory.CreateDirectory(salesDir);

        // Seed rápido se a pasta estiver vazia (só pra testar)
        if (!Directory.EnumerateFiles(salesDir).Any())
        {
            File.WriteAllLines(Path.Combine(salesDir, "jan.txt"), new []{ "Product A: 123.45", "Product B: 67.89" });
            File.WriteAllLines(Path.Combine(salesDir, "fev.txt"), new []{ "$10.00", "$20.00", "$30.00" });
        }

        SalesReporter.WriteSalesSummary(salesDir, outPath);

        Console.WriteLine($"Sales summary written to: {outPath}");
        Console.WriteLine();
        Console.WriteLine(File.ReadAllText(outPath));
    }
}
