namespace AdventCalendar2021.Knowit;
public class Day3
{
    private int balance = 0;
    private int currentNeighbourhoodIndex = 0;
    private int currentNeighbourhoodSize = 0;
    private int largestNeighbourhoodIndex = 0;
    private int largestNeighbourhoodSize = 0;

    public static void Run()
    {
        var data = File.ReadAllLines("Knowit\\Data\\Day3Input.txt")[0].ToList();
        //var data = "JJJJJNNJJNNJJJJJ".ToList();
        new Day3().Run(data);
    }

    public void Run(List<char> data)
    {
        var numberOfJInARow = 0;
        var numberOfNInARow = 0;
        for (int index = 0; index < data.Count; index++)
        {
            if (data[index] == 'J')
            {
                numberOfJInARow = ProcessHouse('J', index > 0 ? data[index - 1] : '0', numberOfJInARow, numberOfNInARow, 1, index);
            }
            else
            {
                numberOfNInARow = ProcessHouse('N', index > 0 ? data[index - 1] : '0', numberOfNInARow, numberOfJInARow, -1, index);
            }

            if (numberOfNInARow == numberOfJInARow)
            {
                var next = index + 1 < data.Count ? data[index + 1] : '0';
                if (data[index] == next && next == data[currentNeighbourhoodIndex] && balance != 0)
                {
                    // The next house will break the pattern, move the index
                    currentNeighbourhoodIndex += numberOfNInARow;
                    if (next == 'J') balance -= numberOfJInARow;
                    else balance += numberOfNInARow;
                }
                else
                {
                    // The current pattern will continue
                    currentNeighbourhoodSize += numberOfNInARow;
                }
            }
            if (balance == 0 && currentNeighbourhoodSize > largestNeighbourhoodSize)
            {
                largestNeighbourhoodSize = currentNeighbourhoodSize;
                largestNeighbourhoodIndex = currentNeighbourhoodIndex;
            }
        }
        Console.WriteLine($"Knowit Day 3 Result: {largestNeighbourhoodSize} {largestNeighbourhoodIndex}");
    }

    private int ProcessHouse(char house, char previousHouse, int counter, int otherCounter, int sign, int index)
    {
        if (house != previousHouse)
        {
            // We've flipped from N to J
            if (counter > 0 && counter != otherCounter)
            {
                // The previous set of Js and Ns are not equal, neighbourhood is not neutral.
                if (counter > otherCounter)
                {
                    // The number of Ns are fewer than the previous number of Js, we can
                    // move the index to where the number of Js will be equals to Ns.
                    currentNeighbourhoodIndex = index - (otherCounter * 2);
                    currentNeighbourhoodSize = otherCounter * 2;
                    balance = 0;
                }
                else
                {
                    // The number of Ns are more than the number of Js, we must
                    // move the index to the start of the Ns
                    currentNeighbourhoodIndex = index - otherCounter;
                    currentNeighbourhoodSize = otherCounter;
                    balance = otherCounter * sign;
                }
            }
            counter = 0;
        }
        balance += sign;
        return counter + 1;
    }
}
