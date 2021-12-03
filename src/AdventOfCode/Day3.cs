namespace AdventCalendar2021.AdventOfCode;
public class Day3
{
    public static void Run()
    {
        var lines = File.ReadAllLines("AdventOfCode\\Data\\Day3Input.txt");
        Part1(lines);
        Part2(lines);
    }

    private static void Part1(string[] lines)
    {
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

    public static void Part2(string[] lines)
    {
        var oxygen = "";
        var co2 = "";

        for (int index = 0; index < lines[0].Count(); index++)
        {
            var oxygenBitCounter = new int[] { 0, 0 };
            var co2BitCounter = new int[] { 0, 0 };
            var filteredOxygenLines = lines.Where(l => l.StartsWith(oxygen));
            if (filteredOxygenLines.Count() > 1)
            {
                foreach (var line in filteredOxygenLines)
                {
                    oxygenBitCounter[(int)char.GetNumericValue(line[index])]++;
                }
                if (oxygenBitCounter[0] > oxygenBitCounter[1])
                    oxygen += '0';
                else
                    oxygen += '1';
            }
            else
            {
                oxygen = filteredOxygenLines.First();
            }
            var filteredCO2Lines = lines.Where(l => l.StartsWith(co2));
            if (filteredCO2Lines.Count() > 1)
            {
                foreach (var line in filteredCO2Lines)
                {
                    co2BitCounter[(int)char.GetNumericValue(line[index])]++;
                }
                if (co2BitCounter[1] < co2BitCounter[0])
                    co2 += '1';
                else
                    co2 += '0';
            }
            else
            {
                co2 = filteredCO2Lines.First();
            }
        }
        var oxygenRating = Convert.ToInt32(oxygen, 2);
        var co2Rating = Convert.ToInt32(co2, 2);
        var lifeSupportRating = oxygenRating * co2Rating;
        Console.WriteLine($"AdventOfCode Day 3 Part 2 Result: {lifeSupportRating}");
    }
}
