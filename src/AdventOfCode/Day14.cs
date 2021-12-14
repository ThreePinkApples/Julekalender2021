using System.Linq;

namespace AdventCalendar2021.AdventOfCode;
public class Day14
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day14Input.txt").ToList();
        var polymerTemplate = input[0];
        var pairInsertions = input.GetRange(2, input.Count - 2).Select(PairInsertion.FromLine);
        InsertPolymers(polymerTemplate, pairInsertions.ToList());
    }

    private static void InsertPolymers(string polymerTemplate, List<PairInsertion> pairInsertions)
    {
        for (int step = 1; step <= 40; step++)
        {
            var newTemplate = polymerTemplate;
            for (int index = 0; index < polymerTemplate.Length - 1; index++)
            {
                var newInsert = pairInsertions.Where(p => p.Pair == $"{polymerTemplate[index]}{polymerTemplate[index + 1]}").Single();
                newTemplate = newTemplate.Insert(index + 1, newInsert.Insert.ToString());
            }
            polymerTemplate = newTemplate;
            if (step == 10)
            {
                var letterFrequenciesPart1 = GetLetterFrequencies(polymerTemplate);
                var resultPart1 = letterFrequenciesPart1.First().Item2 - letterFrequenciesPart1.Last().Item2;
                Console.WriteLine($"AdventOfCode Day 14 Part 1: {resultPart1}");
            }
        }
        var letterFrequencies = GetLetterFrequencies(polymerTemplate);
        var result = letterFrequencies.First().Item2 - letterFrequencies.Last().Item2;
        Console.WriteLine($"AdventOfCode Day 14 Part 2: {result}");
    }

    private static IEnumerable<Tuple<char, int>> GetLetterFrequencies(string text)
    {
        return text
            .GroupBy(c => c)
            .Select(g => new Tuple<char, int>(g.Key, g.Count()))
            .OrderByDescending(t => t.Item2);
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
