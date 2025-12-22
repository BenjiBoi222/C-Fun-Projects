using Nebula_Banking;
using System;
using System.IO;

class Files
{
    // Where the EXE runs
    private static readonly string basePath = AppContext.BaseDirectory;

    // Runtime files (DATABASE)
    private static readonly string usersRuntimePath =
        Path.Combine(basePath, "Users.txt");

    private static readonly string stocksRuntimePath =
        Path.Combine(basePath, "Stocks.txt");

    // Project-root template files (SEED DATA)
    private static readonly string projectRoot =
        Directory.GetParent(basePath)!.Parent!.Parent!.FullName;

    private static readonly string usersTemplatePath =
        Path.Combine(projectRoot, "Users.txt");

    private static readonly string stocksTemplatePath =
        Path.Combine(projectRoot, "Stocks.txt");

    // =========================
    // INITIALIZATION (IMPORTANT)
    // =========================

    public static void EnsureFilesExist()
    {
        EnsureFile(usersRuntimePath, usersTemplatePath);
        EnsureFile(stocksRuntimePath, stocksTemplatePath);
    }

    private static void EnsureFile(string runtimePath, string templatePath)
    {
        if (File.Exists(runtimePath)) return;

        if (File.Exists(templatePath))
        {
            File.Copy(templatePath, runtimePath);
        }
        else
        {
            File.Create(runtimePath).Close();
        }
    }

    // =========================
    // USERS
    // =========================

    public static void ReadUserFile()
    {
        EnsureFilesExist();
        Universal.Users.Clear();

        using StreamReader sr = new StreamReader(usersRuntimePath);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(';');
            if (parts.Length != 5) continue;

            Users user = new Users(
                int.Parse(parts[1]),
                parts[2],
                parts[3],
                double.Parse(parts[4])
            )
            {
                Id = int.Parse(parts[0])
            };

            Universal.Users.Add(user);
        }

        Console.WriteLine($"Loaded {Universal.Users.Count} users.");
    }

    public static void WriteUserFile()
    {
        using StreamWriter sw = new StreamWriter(usersRuntimePath, false);
        foreach (var user in Universal.Users)
        {
            sw.WriteLine(
                $"{user.Id};{user.CardNumber};{user.Password};{user.UserName};{user.UserBalance}"
            );
        }
    }

    // =========================
    // STOCKS
    // =========================

    public static void ReadStocksFile()
    {
        EnsureFilesExist();
        Universal.Stocks.Clear();

        using StreamReader sr = new StreamReader(stocksRuntimePath);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(';');
            if (parts.Length != 3) continue;

            Universal.Stocks.Add(new Stocks
            {
                StockName = parts[0],
                AvailableStocks = int.Parse(parts[1]),
                StockPricePerPiece = double.Parse(parts[2])
            });
        }

        Console.WriteLine($"Loaded {Universal.Stocks.Count} stocks.");
    }

    public static void WriteStocksFile()
    {
        using StreamWriter sw = new StreamWriter(stocksRuntimePath, false);
        foreach (var stock in Universal.Stocks)
        {
            sw.WriteLine(
                $"{stock.StockName};{stock.AvailableStocks};{stock.StockPricePerPiece}"
            );
        }
    }
}
