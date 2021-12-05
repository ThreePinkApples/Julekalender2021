namespace AdventCalendar2021.AdventOfCode;
public class Day5
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day5Input.txt");
        Part1(input);
        Part2(input);
    }

    public static void Part1(string[] input)
    {
        var matrix = new int[1000, 1000];
        foreach (var line in input)
        {
            var parts = line.Split(" -> ").SelectMany(p => p.Split(",")).Select(int.Parse).ToList();
            var start = new IntVector2(parts[0], parts[1]);
            var end = new IntVector2(parts[2], parts[3]);
            if (start.X != end.X && start.Y != end.Y) continue;

            var XDirection = end.X > start.X ? 1 : end.X < start.X ? -1 : 0;
            var YDirection = end.Y > start.Y ? 1 : end.Y < start.Y ? -1 : 0;
            if (XDirection != 0)
            {
                for (int X = start.X; X != end.X + XDirection; X += XDirection)
                    matrix[X, start.Y]++;
            }
            else if (YDirection != 0)
            {
                for (int Y = start.Y; Y != end.Y + YDirection; Y += YDirection)
                    matrix[start.X, Y]++;
            }
        }
        var result = matrix.Cast<int>().Count(n => n > 1);
        Console.WriteLine($"AdventOfCode Day 5 Part 1 {result}");
    }

    public static void Part2(string[] input)
    {
        var matrix = new int[1000, 1000];
        foreach (var line in input)
        {
            var parts = line.Split(" -> ").SelectMany(p => p.Split(",")).Select(int.Parse).ToList();
            var start = new IntVector2(parts[0], parts[1]);
            var end = new IntVector2(parts[2], parts[3]);
            var XDirection = end.X > start.X ? 1 : end.X < start.X ? -1 : 0;
            var YDirection = end.Y > start.Y ? 1 : end.Y < start.Y ? -1 : 0;
            if (XDirection != 0 && YDirection != 0)
            {
                // Line is diagonal, but we should only accept perfect 45 degrees
                var XDiff = Math.Abs(start.X - end.X);
                var YDiff = Math.Abs(start.Y - end.Y);
                if (XDiff != YDiff) continue;
                for (int diff = 0; diff <= XDiff; diff++)
                    matrix[start.X + (diff * XDirection), start.Y + (diff * YDirection)]++;
            }
            else if (XDirection != 0)
            {
                for (int X = start.X; X != end.X + XDirection; X += XDirection)
                    matrix[X, start.Y]++;
            }
            else if (YDirection != 0)
            {
                for (int Y = start.Y; Y != end.Y + YDirection; Y += YDirection)
                    matrix[start.X, Y]++;
            }
        }
        var result = matrix.Cast<int>().Count(n => n > 1);
        Console.WriteLine($"AdventOfCode Day 5 Part 2 {result}");
    }

    internal struct IntVector2
    {
        public int X; public int Y;
        public IntVector2(int x, int y) { X = x; Y = y; }
    }
}
