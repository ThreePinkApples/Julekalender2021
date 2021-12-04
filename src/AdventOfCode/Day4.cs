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
        input.Remove(input[0]);
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
                currentBoard.AddRow(line.Split(' ').Where(l => l.Trim() != "").Select(l => int.Parse(l.Trim())).ToList());
            }
        }
        Boards.Add(currentBoard);
        int finalScore = 0;
        foreach (var number in numbers)
        {
            Boards.ForEach(b => b.MarkBoard(number));
            var winner = Boards.FirstOrDefault(b => b.HasBingo());
            if (winner != null)
            {
                finalScore = winner.GetScore() * number;
                break;
            }
        }
        Console.WriteLine($"AdventOfCode Day 4 Part 1 {finalScore}");
    }

    internal class BingoBoard
    {
        int BoardSize = 5;
        List<List<int>> Board = new List<List<int>>();
        List<List<bool>> Marked = new List<List<bool>>();

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
                if (columnIndex > 0)
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
