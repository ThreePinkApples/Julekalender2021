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
