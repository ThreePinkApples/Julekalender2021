namespace AdventCalendar2021.AdventOfCode;
public class Day1
{
    public static void Run()
    {
        var increases = 0;
        var previousDepth = int.MaxValue;
        var depths = File.ReadAllLines("AdventOfCode\\Data\\Day1Input.txt");
        foreach (var depthStr in depths)
        {
            var depth = int.Parse(depthStr);
            if (depth > previousDepth) increases++;
            previousDepth = depth;
        }
        Console.WriteLine($"AdventOfCode Day 1 result: {increases}");
    }
}
