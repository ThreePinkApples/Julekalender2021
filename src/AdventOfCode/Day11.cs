namespace AdventCalendar2021.AdventOfCode;
public class Day11
{
    private List<List<int>> Octopi { get; set; }
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day11Input.txt").Select(l => l.Select(c => (int)char.GetNumericValue(c)).ToList()).ToList();
        new Day11().Run(input);
    }

    public void Run(List<List<int>> octopi)
    {
        Octopi = octopi;
        Part1();
    }

    public void Part1()
    {
        var numberOfFlashes = 0;
        var allFlashedStep = 0;
        for (int step = 1; step <= 500; step++)
        {
            IncreaseEnergy();
            var stepFlashes = Flash();
            numberOfFlashes += stepFlashes;
            if (stepFlashes == Octopi.Count * Octopi[0].Count)
            {
                allFlashedStep = step;
                break;
            }
        }
        Console.WriteLine($"AdventOfCode Day 11 Part 1: {numberOfFlashes}");
        Console.WriteLine($"AdventOfCode Day 11 Part 2 {allFlashedStep}");
    }

    private void IncreaseEnergy()
    {
        for (int rowIndex = 0; rowIndex < Octopi.Count; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < Octopi[rowIndex].Count; columnIndex++)
            {
                Octopi[rowIndex][columnIndex]++;
            }
        }
    }

    private int Flash()
    {
        var flashes = 0;
        for (int rowIndex = 0; rowIndex < Octopi.Count; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < Octopi[rowIndex].Count; columnIndex++)
            {
                if (Octopi[rowIndex][columnIndex] > 9 && Octopi[rowIndex][columnIndex] > 0)
                {
                    flashes++;
                    Octopi[rowIndex][columnIndex] = 0;
                    PropegateFlash(rowIndex, columnIndex);
                    // Must check for new flashes
                    flashes += Flash();
                }
            }
        }
        return flashes;
    }

    private void PropegateFlash(int rowIndex, int columnIndex)
    {
        if (rowIndex != 0)
        {
            AddIfNot0(rowIndex - 1, columnIndex);
            if (columnIndex < Octopi[rowIndex - 1].Count - 1)
                AddIfNot0(rowIndex - 1, columnIndex + 1);
            if (columnIndex > 0)
                AddIfNot0(rowIndex - 1, columnIndex - 1);
        }
        if (rowIndex < Octopi.Count - 1)
        {
            AddIfNot0(rowIndex + 1, columnIndex);
            if (columnIndex < Octopi[rowIndex + 1].Count - 1)
                AddIfNot0(rowIndex + 1, columnIndex + 1);
            if (columnIndex > 0)
                AddIfNot0(rowIndex + 1, columnIndex - 1);
        }
        if (columnIndex > 0)
            AddIfNot0(rowIndex, columnIndex - 1);
        if (columnIndex < Octopi[rowIndex].Count - 1)
            AddIfNot0(rowIndex, columnIndex + 1);
    }

    private void AddIfNot0(int rowIndex, int columnIndex)
    {
        if (Octopi[rowIndex][columnIndex] != 0)
            Octopi[rowIndex][columnIndex]++;
    }

    internal struct Position
    {
        public int Row; public int Column;
        public Position(int row, int column) { Row = row; Column = column; }
    }
}
