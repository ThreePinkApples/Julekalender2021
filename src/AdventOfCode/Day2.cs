namespace AdventCalendar2021.AdventOfCode;
public class Day2
{
    public static void Run()
    {
        var commands = File.ReadAllLines("AdventOfCode\\Data\\Day2Input.txt")
            .Select(cd => cd.Split(" "))
            .Select(cd => new Tuple<string, int>(cd[0], int.Parse(cd[1])))
            .ToList();
        Part1(commands);
        Part2(commands);
    }

    private static void Part1(List<Tuple<string, int>> commands)
    {
        var horizontalPosition = 0;
        var depth = 0;
        foreach (var (direction, distance) in commands)
        {
            switch (direction)
            {
                case "forward":
                    horizontalPosition += distance;
                    break;
                case "up":
                    depth -= distance;
                    break;
                case "down":
                    depth += distance;
                    break;
                default:
                    throw new Exception($"Unknown direction {direction}");
            }
        }
        Console.WriteLine($"AdventOfCode Day 2 Part 1 Horizontal: {horizontalPosition} Depth: {depth} Result: {horizontalPosition * depth}");
    }

    private static void Part2(List<Tuple<string, int>> commands)
    {
        var horizontalPosition = 0;
        var depth = 0;
        var aim = 0;
        foreach (var (direction, distance) in commands)
        {
            switch (direction)
            {
                case "forward":
                    horizontalPosition += distance;
                    depth += aim * distance;
                    break;
                case "up":
                    aim -= distance;
                    break;
                case "down":
                    aim += distance;
                    break;
                default:
                    throw new Exception($"Unknown direction {direction}");
            }
        }
        Console.WriteLine($"AdventOfCode Day 2 Part 2 Horizontal: {horizontalPosition} Depth: {depth} Aim: {aim} Result: {horizontalPosition * depth}");
    }
}
