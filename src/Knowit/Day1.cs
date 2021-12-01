using System.Linq;
namespace AdventCalendar2021.Knowit;
public class Day1
{
    public static void Run()
    {
        var text = File.ReadAllLines("Knowit\\Data\\tall.txt")[0];
        var sum = 0;
        var previousNumber = 0;
        for (int index = 0; index < text.Length;)
        {
            var (wordLength, number) = ScanForNumber(index, text);
            index += wordLength;
            if (NumbersFollowedBySingle.Contains(previousNumber) && number < 10)
            {
                number += previousNumber;
            }
            else
            {
                // Previous number was not followed by a relevant number
                sum += previousNumber;
                previousNumber = 0;
            }
            if (!NumbersFollowedBySingle.Contains(number))
            {
                Console.WriteLine($"Adding number {number}");
                sum += number;
                previousNumber = 0;
            }
            else
            {
                previousNumber = number;
            }
        }
        Console.WriteLine($"Knowit Day1 Result: {sum}");
    }

    private static Tuple<int, int> ScanForNumber(int startIndex, string text)
    {
        for (int wordLength = 7; wordLength > 0; wordLength--)
        {
            if (startIndex + wordLength > text.Length) continue;
            var word = text.Substring(startIndex, wordLength);
            var number = WordToNumber(word);
            if (number != null)
            {
                return new(wordLength, number.Value);
            }
        }
        throw new Exception($"Failed to find number at startIndex {startIndex}");
    }

    private static readonly int[] NumbersFollowedBySingle = new int[] { 20, 30, 40 };

    private static int? WordToNumber(string word)
    {
        switch (word)
        {
            case "en":
                return 1;
            case "to":
                return 2;
            case "tre":
                return 3;
            case "fire":
                return 4;
            case "fem":
                return 5;
            case "seks":
                return 6;
            case "sju":
                return 7;
            case "åtte":
                return 8;
            case "ni":
                return 9;
            case "ti":
                return 10;
            case "elleve":
                return 11;
            case "tolv":
                return 12;
            case "tretten":
                return 13;
            case "fjorten":
                return 14;
            case "femten":
                return 15;
            case "seksten":
                return 16;
            case "sytten":
                return 17;
            case "atten":
                return 18;
            case "nitten":
                return 19;
            case "tjue":
                return 20;
            case "tretti":
                return 30;
            case "førti":
                return 40;
            case "femti":
                return 50;
            default:
                return null;
        }
    }
}
