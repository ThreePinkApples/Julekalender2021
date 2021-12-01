namespace AdventCalendar2021.AdventOfCode;
public class Day1
{
    public static void Run()
    {
        var increases = 0;
        var previousDepth = int.MaxValue;
        var depths = File.ReadAllLines("AdventOfCode\\Data\\Day1Input.txt").Select(d => int.Parse(d)).ToList();
        var numberOfDepths = depths.Count();
        foreach (var depth in depths)
        {
            if (depth > previousDepth) increases++;
            previousDepth = depth;
        }
        Console.WriteLine($"AdventOfCode Day 1 Part 1 result: {increases}");

        increases = 0;
        previousDepth = int.MaxValue;
        for (int index = 0; index + 2 < numberOfDepths; index++)
        {
            var depth = depths[index] + depths[index + 1] + depths[index + 2];
            if (depth > previousDepth) increases++;
            previousDepth = depth;
        }
        Console.WriteLine($"AdventOfCode Day 1 Part 2 result: {increases}");
    }
}
