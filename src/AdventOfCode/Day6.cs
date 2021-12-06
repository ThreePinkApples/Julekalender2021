namespace AdventCalendar2021.AdventOfCode;
public class Day6
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day6Input.txt")[0].Split(",").Select(int.Parse);
        SimulateFishes(input.ToList(), 80);
        SimulateFishes(input.ToList(), 256);
    }

    public static void SimulateFishes(List<int> input, int days)
    {
        List<long> dayCounters = new()
        {
            input.Count(i => i == 0),
            input.Count(i => i == 1),
            input.Count(i => i == 2),
            input.Count(i => i == 3),
            input.Count(i => i == 4),
            input.Count(i => i == 5),
            input.Count(i => i == 6),
            input.Count(i => i == 7),
            input.Count(i => i == 8),
        };
        for (int day = 0; day < days; day++)
        {
            long day0Temp = dayCounters[0];
            dayCounters[0] = dayCounters[1];
            dayCounters[1] = dayCounters[2];
            dayCounters[2] = dayCounters[3];
            dayCounters[3] = dayCounters[4];
            dayCounters[4] = dayCounters[5];
            dayCounters[5] = dayCounters[6];
            dayCounters[6] = dayCounters[7] + day0Temp;
            dayCounters[7] = dayCounters[8];
            dayCounters[8] = day0Temp;
        }
        var result = dayCounters.Sum();
        Console.WriteLine($"AdventOfCode Day 6 After {days} Result {result}");
    }
}
