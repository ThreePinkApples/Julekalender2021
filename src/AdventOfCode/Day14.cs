using System.Linq;

namespace AdventCalendar2021.AdventOfCode;
public class Day14
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day14Input.txt").ToList();
        var polymerTemplate = input[0];
        var pairInsertions = input.GetRange(2, input.Count - 2).Select(PairInsertion.FromLine);
        Part1(polymerTemplate, pairInsertions.ToList());
    }

    private static void Part1(string polymerTemplate, List<PairInsertion> pairInsertions)
    {
        for (int step = 1; step <= 10; step++)
        {
            var newTemplate = polymerTemplate;
            for (int index = 0; index < polymerTemplate.Length - 1; index++)
            {
                var newInsert = pairInsertions.Where(p => p.Pair == $"{polymerTemplate[index]}{polymerTemplate[index + 1]}").Single();
                var numberOfInserts = newTemplate.Length - polymerTemplate.Length;
                newTemplate = newTemplate.Substring(0, index + numberOfInserts + 1) + newInsert.Insert + newTemplate.Substring(index + numberOfInserts + 1);
            }
            polymerTemplate = newTemplate;
        }
        var letterFrequencies = polymerTemplate
            .GroupBy(c => c)
            .Select(g => new { Letter = g.Key, Count = g.Count()})
            .OrderByDescending(n => n.Count);
        var result = letterFrequencies.First().Count - letterFrequencies.Last().Count;
        Console.WriteLine($"AdventOfCode Day 14 Part 1: {result}");
    }

    internal struct PairInsertion
    {
        public string Pair { get; set; }
        public char Insert { get; set; }
        public PairInsertion(string pair, char insert)
        {
            Pair = pair; Insert = insert;
        }

        public static PairInsertion FromLine(string line)
        {
            var parts = line.Split(" -> ");
            return new PairInsertion(parts[0], parts[1][0]);
        }
    }
}
