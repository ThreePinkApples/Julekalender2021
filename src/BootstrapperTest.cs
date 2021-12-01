using System.Reflection;

namespace Julekalender2021;
public class BootstrapperTest
{
    public static void Start()
    {
        Console.WriteLine("Pick a calendar and day");

        string line;
        while ((line = Console.ReadLine()) != null)
        {
            try
            {
                ProcessLine(line);
            }
            catch (Exception ex)
            {
                LogError("Failure", ex);
            }
        }
    }

    private static void ProcessLine(string line)
    {
        var parts = line.Split(' ');
        var calendar = parts[0];
        int day;
        try
        {
            day = Int32.Parse(parts[1]);
        }
        catch (FormatException)
        {
            LogError($"{parts[1]} is not a number. How naughty of you.");
            return;
        }
        switch (calendar)
        {
            case "knowit":
                Knowit(day);
                break;
            case "advent":
                break;
            default:
                LogError("Unknown calendar");
                break;
        }
    }

    private static void Knowit(int day)
    {
        var t = Type.GetType($"Julekalender2021.Knowit.Day{day}");
        var method = t.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
        method.Invoke(null, null);
    }

    private static void LogError(string error)
    {
        LogError(error, null);
    }

    private static void LogError(string error, Exception? ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error, ex);
        Console.ResetColor();
    }
}
