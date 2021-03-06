using System.Text;

namespace AdventCalendar2021.AdventOfCode;
public class Day4
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day4Input.txt");
        new Day4().Run(input.ToList());
    }

    int[] numbers;
    List<BingoBoard> Boards = new();

    public void Run(List<string> input)
    {
        numbers = input[0].Split(',').Select(int.Parse).ToArray();
        input.RemoveAt(0);
        BingoBoard? currentBoard = null;
        foreach (var line in input)
        {
            if (line.Trim() == "")
            {
                if (currentBoard != null)
                    Boards.Add(currentBoard);
                currentBoard = new();
            }
            else
            {
                currentBoard.AddRow(line.Split(' ').Where(l => l.Trim() != "").Select(int.Parse).ToList());
            }
        }
        Boards.Add(currentBoard);
        List<Tuple<int, BingoBoard>> winners = new();
        foreach (var number in numbers)
        {
            Boards.ForEach(b => b.MarkBoard(number));
            var newWinners = Boards.Where(b => b.HasBingo()).ToList();
            Boards.RemoveAll(b => newWinners.Contains(b));
            newWinners.ForEach(w => winners.Add(new(number, w)));
        }
        var (firstWinnerNumber, firstWinner) = winners.First();
        var (lastWinnerNumber, lastWinner) = winners.Last();
        Console.WriteLine($"First winner number: {firstWinnerNumber}\nFirst winner Board: \n{firstWinner.ToString()}\n");
        Console.WriteLine($"Last winner number: {lastWinnerNumber}\nLast winner Board: \n{lastWinner.ToString()}\n");
        Console.WriteLine($"AdventOfCode Day 4 Part 1 {firstWinner.GetScore() * firstWinnerNumber}");
        Console.WriteLine($"AdventOfCode Day 4 Part 2 {lastWinner.GetScore() * lastWinnerNumber}");
    }

    internal class BingoBoard
    {
        int BoardSize = 5;
        List<List<int>> Board = new List<List<int>>();
        List<List<bool>> Marked = new List<List<bool>>();

        public string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Marked[i][j])
                        builder.Append($"{Board[i][j]}* ");
                    else
                        builder.Append($"{Board[i][j]} ");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }

        public void AddRow(List<int> row)
        {
            if (row.Count != BoardSize)
            {
                throw new Exception($"Row of numbers is too short, need {BoardSize} numbers");
            }
            Board.Add(row);
            Marked.Add(Enumerable.Range(0, BoardSize).Select(i => false).ToList());
        }

        public void MarkBoard(int number)
        {
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                var columnIndex = Board[rowIndex].FindIndex(n => n == number);
                if (columnIndex >= 0)
                    Marked[rowIndex][columnIndex] = true;
            }
        }

        public bool HasBingo()
        {
            foreach (var row in Marked)
            {
                if (row.All(cell => cell)) return true;
            }
            for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
            {
                var column = Marked.Select(row => row[columnIndex]);
                if (column.All(cell => cell)) return true;
            }
            return false;
        }

        public int GetScore()
        {
            var score = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (!Marked[i][j]) score += Board[i][j];
                }
            }
            return score;
        }
    }
}
