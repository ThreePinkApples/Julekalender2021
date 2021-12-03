using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar2021.AdventOfCode;
public class Day3
{
    public static void Run()
    {
        var lines = File.ReadAllLines("AdventOfCode\\Data\\Day3Input.txt");
        List<int[]> bitCounters = new();
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Count(); j++)
            {
                if (i == 0)
                    bitCounters.Add(new int[] { 0, 0 });
                var index = (int)char.GetNumericValue(lines[i][j]);
                bitCounters[j][index]++;
            }
        }
        Part1(bitCounters);
        Part2(lines, bitCounters);
    }

    private static void Part1(List<int[]> bitCounters)
    {
        var gamma = "";
        var epsilon = "";
        foreach (var bitCounter in bitCounters)
        {
            if (bitCounter[0] > bitCounter[1])
            {
                gamma += '0';
                epsilon += '1';
            }
            else
            {
                gamma += '1';
                epsilon += '0';
            }
        }
        var gammaRate = Convert.ToInt32(gamma, 2);
        var epsilonRate = Convert.ToInt32(epsilon, 2);
        var powerConsumption = gammaRate * epsilonRate;
        Console.WriteLine($"AdventOfCode Day 3 Part 1 Result: {powerConsumption}");
    }

    public static void Part2(string[] lines, List<int[]> bitCounters)
    {

    }
}
