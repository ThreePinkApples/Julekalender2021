namespace AdventCalendar2021.AdventOfCode;
public class Day9
{

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day9Input.txt").Select(l => l.Select(c => (int)char.GetNumericValue(c)));
        Part1(input);
        Part2(input);
    }

    private static void Part1(IEnumerable<IEnumerable<int>> HeightMap)
    {
        Console.WriteLine($"AdventOfCode Day 9 Part 1 result: ");
    }

    private static void Part2(IEnumerable<IEnumerable<int>> HeightMap)
    {
        Console.WriteLine($"AdventOfCode Day 9 Part 2 result: ");
    }
}
