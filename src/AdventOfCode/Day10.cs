namespace AdventCalendar2021.AdventOfCode;
public class Day10
{
    private static char[] OpenTokens = new char[] { '[', '{', '<', '(' };
    private static Dictionary<char, char> CloseToOpenTokenMap = new()
    {
        { ']', '[' },
        { '}', '{' },
        { '>', '<' },
        { ')', '(' }
    };

    private static Dictionary<char, int> PointMap = new()
    {
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 },
        { ')', 3 }
    };

    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day10Input.txt").ToList();
        Part1(input);
    }

    public static void Part1(List<string> input)
    {
        var totalPoints = 0;
        foreach (var line in input)
        {
            var tokenStack = new Stack<char>();
            foreach (var token in line)
            {
                if (OpenTokens.Contains(token))
                {
                    tokenStack.Push(token);
                }
                else if (tokenStack.Peek() != CloseToOpenTokenMap[token])
                {
                    totalPoints += PointMap[token];
                    break;
                }
                else
                {
                    tokenStack.Pop();
                }
            }
        }
        Console.Write($"AdventOfCode Day 10 Part 1: {totalPoints}");
    }
}
