namespace AdventCalendar2021.AdventOfCode;
public class Day10
{
    private static Dictionary<char, char> OpenToCloseTokenMap = new()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' }
    };

    private static Dictionary<char, char> CloseToOpenTokenMap = new()
    {
        { ')', '(' },
        { ']', '[' },
        { '}', '{' },
        { '>', '<' }
    };

    private static Dictionary<char, int> SyntaxErrorPointMap = new()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 },
    };

    private static Dictionary<char, int> IncompletePointMap = new()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
    };

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day10Input.txt").ToList();
        var syntaxErrorScore = 0;
        var incompleteScores = new List<double>();
        foreach (var line in input)
        {
            var tokenStack = new Stack<char>();
            var syntaxError = false;
            foreach (var token in line)
            {
                if (OpenToCloseTokenMap.Keys.Contains(token))
                {
                    tokenStack.Push(token);
                }
                else if (tokenStack.Peek() != CloseToOpenTokenMap[token])
                {
                    syntaxErrorScore += SyntaxErrorPointMap[token];
                    syntaxError = true;
                    break;
                }
                else
                {
                    tokenStack.Pop();
                }
            }
            if (!syntaxError && tokenStack.Count > 0)
            {
                // Line is incomplete
                var lineScore = 0.0;
                foreach (var remainingToken in tokenStack)
                {
                    lineScore *= 5;
                    lineScore += IncompletePointMap[OpenToCloseTokenMap[remainingToken]];
                }
                incompleteScores.Add(lineScore);
            }
        }
        Console.WriteLine($"AdventOfCode Day 10 Part 1: {syntaxErrorScore}");
        var incompleteFinalScore = incompleteScores.OrderBy(s => s).ToList()[incompleteScores.Count / 2];
        Console.WriteLine($"AdventOfCode Day 10 Part 2: {incompleteFinalScore}");
    }
}
