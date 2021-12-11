namespace AdventCalendar2021.AdventOfCode;
public class Day9
{

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day9Input.txt").Select(l => l.Select(c => (int)char.GetNumericValue(c)).ToList()).ToList();
        Part1(input);
        Part2(input);
    }

    private static void Part1(List<List<int>> HeightMap)
    {
        var totalLowPointRiskLevel = 0;
        for (int rowIndex = 0; rowIndex < HeightMap.Count; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < HeightMap[rowIndex].Count; columnIndex++)
            {
                var currentHeight = HeightMap[rowIndex][columnIndex];
                var up = rowIndex != 0 ? HeightMap[rowIndex - 1][columnIndex] : 10;
                var down = rowIndex != HeightMap.Count - 1 ? HeightMap[rowIndex + 1][columnIndex] : 10;
                var left = columnIndex != 0 ? HeightMap[rowIndex][columnIndex - 1] : 10;
                var right = columnIndex != HeightMap[rowIndex].Count - 1 ? HeightMap[rowIndex][columnIndex + 1] : 10;
                if (currentHeight < up && currentHeight < down && currentHeight < left && currentHeight < right)
                {
                    totalLowPointRiskLevel += 1 + currentHeight;
                }
            }
        }
        Console.WriteLine($"AdventOfCode Day 9 Part 1 result: {totalLowPointRiskLevel}");
    }

    private static void Part2(List<List<int>> HeightMap)
    {
        Console.WriteLine($"AdventOfCode Day 9 Part 2 result: ");
    }
}
