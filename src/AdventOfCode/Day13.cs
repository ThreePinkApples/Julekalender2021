using System.Linq;

namespace AdventCalendar2021.AdventOfCode;
public class Day13
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day13Input.txt").ToList();
        var splitIndex = input.FindIndex(i => i.Trim() == "");
        var dots = input.GetRange(0, splitIndex)
            .Select(l => l.Split(",").Select(int.Parse))
            .Select(x => new Dot(x.First(), x.Last()));
        var folds = input.GetRange(splitIndex + 1, input.Count - dots.Count() - 1)
            .Select(l => l.Split(" ").Last())
            .Select(l => l.Split("="))
            .Select(l => new FoldInstruction(l[0][0], int.Parse(l[1])));

        var largestX = dots.OrderByDescending(d => d.X).First().X;
        var largestY = dots.OrderByDescending(d => d.Y).First().Y;
        var paper = new bool[largestX + 1, largestY + 1];
        foreach (var dot in dots)
        {
            paper[dot.X, dot.Y] = true;
        }
        Part1(folds.ToList(), paper);
        Part2(folds.ToList(), paper);
    }

    private static void Part1(List<FoldInstruction> folds, bool[,] paper)
    {
        var foldedPaper = FoldPaper(folds.First(), paper);
        var result = foldedPaper.Cast<bool>().Count(x => x);
        Console.WriteLine($"AdventOfCode Day 13 Part 1: {result}");
    }

    private static void Part2(List<FoldInstruction> folds, bool[,] paper)
    {
        var foldedPaper = paper;
        foreach (var fold in folds)
        {
            foldedPaper = FoldPaper(fold, foldedPaper);
        }
        Console.WriteLine($"AdventOfCode Day 13 Part 2:");
        PrintPaper(foldedPaper);
    }

    private static void PrintPaper(bool[,] paper)
    {
        for (int Y = 0; Y < paper.GetLength(1); Y++)
        {
            for (int X = 0; X < paper.GetLength(0); X++)
            {
                Console.Write(paper[X, Y] ? " # " : " . ");
            }
            Console.Write("\n");
        }
    }

    private static bool[,] FoldPaper(FoldInstruction foldInstruction, bool[,] paper)
    {
        if (foldInstruction.Axis == 'x')
            return FoldOnX(foldInstruction.Index, paper);
        else
            return FoldOnY(foldInstruction.Index, paper);
    }

    private static bool[,] FoldOnX(int index, bool[,] paper)
    {
        var newXLength = paper.GetLength(0) - index - 1;
        var foldedPaper = new bool[newXLength, paper.GetLength(1)];
        for (int X = 0; X < index; X++)
        {
            for (int Y = 0; Y < paper.GetLength(1); Y++)
            {
                foldedPaper[X, Y] = paper[X, Y];
            }
        }
        for (int X = index + 1; X < paper.GetLength(0); X++)
        {
            for (int Y = 0; Y < paper.GetLength(1); Y++)
            {
                var newX = index - (X - index);
                foldedPaper[newX, Y] = paper[X, Y] || foldedPaper[newX, Y];
            }
        }
        return foldedPaper;
    }

    private static bool[,] FoldOnY(int index, bool[,] paper)
    {
        var newYLength = paper.GetLength(1) - index - 1;
        var foldedPaper = new bool[paper.GetLength(0), newYLength];
        for (int X = 0; X < paper.GetLength(0); X++)
        {
            for (int Y = 0; Y < index; Y++)
            {
                foldedPaper[X, Y] = paper[X, Y];
            }
        }
        for (int X = 0; X < paper.GetLength(0); X++)
        {
            for (int Y = index + 1; Y < paper.GetLength(1); Y++)
            {
                var newY = index - (Y - index);
                foldedPaper[X, newY] = paper[X, Y] || foldedPaper[X, newY];
            }
        }
        return foldedPaper;
    }

    internal struct Dot
    {
        public int X; public int Y;
        public Dot(int x, int y) { X = x; Y = y; }
    }

    internal struct FoldInstruction
    {
        public char Axis; public int Index;
        public FoldInstruction(char axis, int index) { Axis = axis; Index = index; }
    }
}
