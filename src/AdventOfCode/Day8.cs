namespace AdventCalendar2021.AdventOfCode;
public class Day8
{
    private static readonly Dictionary<int, List<char>> InitialWireMapping = new()
    {
        { 0, new() { 'a', 'b', 'c', 'e', 'f', 'g' } },
        { 1, new() { 'c', 'f' } },
        { 2, new() { 'a', 'c', 'd', 'e', 'g' } },
        { 3, new() { 'a', 'c', 'd', 'f', 'g' } },
        { 4, new() { 'b', 'c', 'd', 'f' } },
        { 5, new() { 'a', 'b', 'd', 'f', 'g' } },
        { 6, new() { 'a', 'b', 'd', 'e', 'f', 'g' } },
        { 7, new() { 'a', 'c', 'f' } },
        { 8, new() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
        { 9, new() { 'a', 'b', 'c', 'd', 'f', 'g' } },
    };
    // 1, 4, 7, and 8 all have a unique number of wires
    private static readonly List<int> UniqueLengths = new() { 2, 3, 4, 7 };

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day8Input.txt").Select(DisplaySignal.FromInputLine).ToList();
        Part1(input);
        Part2(input);
    }

    private static void Part1(List<DisplaySignal> DisplaySignals)
    {
        var numberOf1478 = 0;
        foreach (var displaySignal in DisplaySignals)
        {
            foreach (var outputSignal in displaySignal.Outputs)
            {
                if (UniqueLengths.Contains(outputSignal.Length)) numberOf1478++;
            }
        }
        Console.WriteLine($"AdventOfCode Day 8 Part 1 result: {numberOf1478}");
    }

    private static void Part2(List<DisplaySignal> DisplaySignals)
    {
        var result = DisplaySignals.Sum(i => i.GetOutput());
        Console.WriteLine($"AdventOfCode Day 8 Part 2 result: {result}");
    }

    internal class DisplaySignal
    {
        private string[] Patterns { get; set; }
        public string[] Outputs { get; set; }
        private int Output { get; set; }

        private Dictionary<int, List<char>> WireMapping = new()
        {
            { 0, new() },
            { 1, new() },
            { 2, new() },
            { 3, new() },
            { 4, new() },
            { 5, new() },
            { 6, new() },
            { 7, new() },
            { 8, new() },
            { 9, new() },
        };

        private DisplaySignal(string patternString, string outputStrings)
        {
            Patterns = patternString.Trim().Split(" ").ToArray();
            Outputs = outputStrings.Trim().Split(" ").ToArray();
        }

        public static DisplaySignal FromInputLine(string line)
        {
            var parts = line.Split(" | ");
            return new DisplaySignal(parts[0], parts[1]);
        }

        public int GetOutput()
        {
            if (WireMapping[0].Count == 0) MapWires();
            var outputStr = "";
            foreach (var outputPattern in Outputs)
            {
                outputStr += WireMapping
                    .Where(wire => wire.Value.Count == outputPattern.Length)
                    .Where(wire => wire.Value.All(w => outputPattern.Contains(w)))
                    .Single().Key;
            }
            Output = int.Parse(outputStr);
            return Output;
        }

        public void MapWires()
        {
            foreach (var patternWithUniqueLength in Patterns.Where(p => UniqueLengths.Contains(p.Length)))
            {
                // 1, 4, 7, or 8
                var number = InitialWireMapping.Where(w => w.Value.Count == patternWithUniqueLength.Length).Select(w => w.Key).Single();
                AddWireMapping(number, patternWithUniqueLength);
            }
            foreach (var patternWithoutUniqueLength in Patterns.Where(p => p.Length == 5))
            {
                // 2, 3, or 5
                if (WireMapping[1].All(wire => patternWithoutUniqueLength.Contains(wire)))
                {
                    // All wires for the number 1, is also in 3, but not 2 or 5
                    AddWireMapping(3, patternWithoutUniqueLength);
                }
                else if (patternWithoutUniqueLength.Count(wire => WireMapping[4].Contains(wire)) == 3)
                {
                    // 4 and 5 have 3 wires in common, while 4 and 2 have only 2 in common
                    AddWireMapping(5, patternWithoutUniqueLength);
                }
                else
                {
                    AddWireMapping(2, patternWithoutUniqueLength);
                }
            }
            foreach (var patternWithoutUniqueLength in Patterns.Where(p => p.Length == 6))
            {
                // 0, 6, or 9
                if (!WireMapping[1].All(wire => patternWithoutUniqueLength.Contains(wire)))
                {
                    // 6 does not contain all wires that are in 1, but 0 and 9 do
                    AddWireMapping(6, patternWithoutUniqueLength);
                }
                else if (WireMapping[3].All(wire => patternWithoutUniqueLength.Contains(wire)))
                {
                    // All wires for the number 3, is also in 9, but not in 0
                    AddWireMapping(9, patternWithoutUniqueLength);
                }
                else
                {
                    AddWireMapping(0, patternWithoutUniqueLength);
                }
            }
        }

        private void AddWireMapping(int number, string pattern)
        {
            WireMapping[number] = pattern.ToCharArray().ToList();
        }
    }
}
