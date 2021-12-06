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
        long day0 = input.Count(i => i == 0);
        long day1 = input.Count(i => i == 1);
        long day2 = input.Count(i => i == 2);
        long day3 = input.Count(i => i == 3);
        long day4 = input.Count(i => i == 4);
        long day5 = input.Count(i => i == 5);
        long day6 = input.Count(i => i == 6);
        long day7 = input.Count(i => i == 7);
        long day8 = input.Count(i => i == 8);
        for (int day = 0; day < days; day++)
        {
            long day0Temp = day0;
            day0 = day1;
            day1 = day2;
            day2 = day3;
            day3 = day4;
            day4 = day5;
            day5 = day6;
            day6 = day7 + day0Temp;
            day7 = day8;
            day8 = day0Temp;
        }
        var result = day0 + day1 + day2 + day3 + day4 + day5 + day6 + day7 + day8;
        Console.WriteLine($"AdventOfCode Day 6 After {days} Result {result}");
    }
}
