namespace AdventCalendar2021.AdventOfCode;
public class Day9
{
    private List<List<int>> HeightMap { get; set; }
    private List<Area> LowPoints = new();

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day9Input.txt").Select(l => l.Select(c => (int)char.GetNumericValue(c)).ToList()).ToList();
        new Day9().Run(input);
    }

    public void Run(List<List<int>> input)
    {
        HeightMap = input;
        Part1();
        Part2();
    }

    private void Part1()
    {
        var totalRiskLevel = 0;
        for (int rowIndex = 0; rowIndex < HeightMap.Count; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < HeightMap[rowIndex].Count; columnIndex++)
            {
                var position = new Position(rowIndex, columnIndex);
                if (IsLowPoint(position))
                {
                    var height = HeightMap[rowIndex][columnIndex];
                    LowPoints.Add(new(height, position));
                    totalRiskLevel += 1 + height;
                }
            }
        }
        Console.WriteLine($"AdventOfCode Day 9 Part 1 result: {totalRiskLevel}");
    }

    private void Part2()
    {
        var basinSizes = new List<int>();
        foreach (var lowPoint in LowPoints)
        {
            basinSizes.Add(1 + GetBasinFromArea(lowPoint).Count);
        }
        var result = basinSizes.OrderByDescending(bs => bs).Take(3).Aggregate(1, (total, next) => next * total);
        Console.WriteLine($"AdventOfCode Day 9 Part 2 result: {result}");
    }

    private bool IsLowPoint(Position position)
    {
        var neighbours = GetNeighbouringAreas(position);
        return neighbours.All(n => n.Height > HeightMap[position.Row][position.Column]);
    }

    private HashSet<Area> GetBasinFromArea(Area startArea)
    {
        var startPosition = startArea.Position;
        var startHeight = HeightMap[startPosition.Row][startPosition.Column];
        var basin = GetNeighbouringAreas(startPosition)
            .Where(neighbour => neighbour.Height < 9 && neighbour.Height > startHeight)
            .ToHashSet();
        basin = basin.Union(basin.SelectMany(n => GetBasinFromArea(n)).ToHashSet()).ToHashSet();
        return basin;
    }

    private List<Area> GetNeighbouringAreas(Position position)
    {
        var rowIndex = position.Row;
        var columnIndex = position.Column;
        var up = rowIndex != 0 ? HeightMap[rowIndex - 1][columnIndex] : 10;
        var down = rowIndex != HeightMap.Count - 1 ? HeightMap[rowIndex + 1][columnIndex] : 10;
        var left = columnIndex != 0 ? HeightMap[rowIndex][columnIndex - 1] : 10;
        var right = columnIndex != HeightMap[rowIndex].Count - 1 ? HeightMap[rowIndex][columnIndex + 1] : 10;
        return new()
        {
            new(up, new(rowIndex - 1, columnIndex)),
            new(down, new(rowIndex + 1, columnIndex)),
            new(left, new(rowIndex, columnIndex - 1)),
            new(right, new(rowIndex, columnIndex + 1))
        };
    }

    internal struct Area
    {
        public int Height; public Position Position;
        public Area(int height, Position position) { Height = height; Position = position; }
    }

    internal struct Position
    {
        public int Row; public int Column;
        public Position(int row, int column) { Row = row; Column = column; }
    }
}
