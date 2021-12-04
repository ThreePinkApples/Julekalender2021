using System.Numerics;

namespace AdventCalendar2021.Knowit;
public class Day4
{
    public static void Run()
    {
        var X = BigInteger.Zero;
        var Y = BigInteger.Zero;
        var moves = BigInteger.Parse("100000000000000000079");
        var up = true;
        while (true)
        {
            if (up)
            {
                Y++;
                if (Y % 15 != 0)
                    up = !up;
            }
            else
            {
                X++;
                if (X % 15 != 0)
                    up = !up;
            }

            if (X + Y == moves)
                break;
        }
        Console.WriteLine($"Position {X},{Y}");
    }
}
