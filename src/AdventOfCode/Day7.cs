namespace AdventCalendar2021.AdventOfCode;
public class Day7
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day7Input.txt")[0]
            .Split(",")
            .Select(int.Parse)
            .OrderBy(i => i)
            .ToList();
        Part1(input);
        Part2(input);
    }

    private static void Part1(List<int> input)
    {
        var median = input[input.Count / 2];
        var totalMovement = 0;
        foreach (var crab in input)
        {
            totalMovement += Math.Abs(crab - median);
        }
        Console.WriteLine($"AdventOfCode Day 7 Part 1 result: {totalMovement}");
    }

    private static void Part2(List<int> input)
    {
        var average = input.Sum() / input.Count;
        var totalMovement = 0;
        foreach (var crab in input)
        {
            var diff = Math.Abs(crab - average);
            totalMovement += Factorial(diff);
        }
        Console.WriteLine($"AdventOfCode Day 7 Part 2 result: {totalMovement}");
    }

    private static int Factorial(int n)
    {
        var factorial = 0;
        for (int i = n; i > 0; i--)
        {
            factorial += i;
        }
        return factorial;
    }
}
