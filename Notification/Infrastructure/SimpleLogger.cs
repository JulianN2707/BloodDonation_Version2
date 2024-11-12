using Spectre.Console;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure;

internal static class SimpleLogger
{
    private static readonly List<string> Logs = new List<string>();

    public static void StartLog()
    {
        var log = $"Execution started at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
        var rule = new Rule();

        AnsiConsole.WriteLine(log);
        Logs.Add(log);
    }

    public static void Log(string message)
    {
        var log = $"# {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}: {message}";
        var rule = new Rule();
        AnsiConsole.Write(rule);

        AnsiConsole.WriteLine(log);
        Logs.Add(log);
    }

    public static void FinishLog()
    {
        var log = $"Execution finished {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
        var rule = new Rule();
        AnsiConsole.Write(rule);

        AnsiConsole.WriteLine(log);
        Logs.Add(log);

        SaveLogs();
    }

    private static void SaveLogs()
    {
        var logFile = Path.Combine(Environment.CurrentDirectory, $"{DateTime.Now:ddMMyyyyHHmmss}.log");
        File.WriteAllLines(logFile, Logs.ToArray());
    }
}
