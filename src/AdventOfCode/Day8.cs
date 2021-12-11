﻿namespace AdventCalendar2021.AdventOfCode;
public class Day8
{
    private static readonly Dictionary<int, List<char>> WireMapping = new()
    {
        { 0, new() { 'a', 'b', 'c', 'e', 'f', 'g' } },
        { 1, new() { 'c', 'f' } },
        { 2, new() { 'a', 'c', 'd', 'e', 'g' } },
        { 3, new() { 'a', 'c', 'd', 'f', 'g' } },
        { 4, new() { 'b', 'c', 'd', 'f' } },
        { 5, new() { 'a', 'b', 'd', 'f', 'g' } },
        { 6, new() { 'a', 'b', 'd', 'e', 'f', 'g' } },
        { 7, new() { 'a', 'c', 'f' } },
        { 8, new() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
        { 9, new() { 'a', 'b', 'c', 'd', 'f', 'g' } },
    };

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day8Input.txt").Select(DisplaySignals.FromInputLine).ToList();
        Part1(input);
        Part2(input);
    }

    private static void Part1(List<DisplaySignals> input)
    {
        var lookingForLength = new List<int>
        {
            WireMapping[1].Count, WireMapping[4].Count, WireMapping[7].Count, WireMapping[8].Count
        };
        var numberOf1478 = 0;
        foreach (var displaySignal in input)
        {
            foreach (var outputSignal in displaySignal.Outputs)
            {
                if (lookingForLength.Contains(outputSignal.Count())) numberOf1478++;
            }
        }
        Console.WriteLine($"AdventOfCode Day 8 Part 1 result: {numberOf1478}");
    }

    private static void Part2(List<DisplaySignals> input)
    {
        Console.WriteLine($"AdventOfCode Day 8 Part 2 result:");
    }

    internal class DisplaySignals
    {
        private string[] Patterns { get; set; }
        public string[] Outputs { get; set; }

        private DisplaySignals(string patternString, string outputStrings)
        {
            Patterns = patternString.Trim().Split(" ").ToArray();
            Outputs = outputStrings.Trim().Split(" ").ToArray();
        }

        public static DisplaySignals FromInputLine(string line)
        {
            var parts = line.Split(" | ");
            return new DisplaySignals(parts[0], parts[1]);
        }
    }
}
