namespace AdventCalendar2021.Knowit;
public class Day3
{
    public static void Run()
    {
        var data = File.ReadAllLines("Knowit\\Data\\Day3Input.txt")[0].ToList();
        //var data = "JJJJJNNJJNNJJJJJ";
        var currentNeighbourhoodIndex = 0;
        var currentNeighbourhoodSize = 0;
        var largestNeighbourhoodIndex = 0;
        var largestNeighbourhoodSize = 0;
        var numberOfJInARow = 0;
        var numberOfNInARow = 0;
        var balance = 0;
        for (int index = 0; index < data.Count(); index++)
        {
            if (data[index] == 'J')
            {
                if (index > 0 && data[index - 1] == 'N')
                {
                    // We've flipped from N to J
                    if (numberOfJInARow > 0 && numberOfNInARow != numberOfJInARow)
                    {
                        // The previous set of Js and Ns are not equal, neighbourhood is not neutral.
                        if (numberOfJInARow > numberOfNInARow)
                        {
                            // The number of Ns are fewer than the previous number of Js, we can
                            // move the index to where the number of Js will be equals to Ns.
                            currentNeighbourhoodIndex = index - (numberOfNInARow * 2);
                            currentNeighbourhoodSize = numberOfNInARow * 2;
                            balance = 0;
                        }
                        else
                        {
                            // The number of Ns are more than the number of Js, we must
                            // move the index to the start of the Ns
                            currentNeighbourhoodIndex = index - numberOfNInARow;
                            currentNeighbourhoodSize = numberOfNInARow;
                            balance = -numberOfNInARow;
                        }
                    }
                    numberOfJInARow = 0;
                }
                numberOfJInARow++;
                balance++;
            }
            else
            {
                if (index > 0 && data[index - 1] == 'J')
                {
                    // We've flipped from J to N
                    if (numberOfNInARow > 0 && numberOfJInARow != numberOfNInARow)
                    {
                        // The previous set of Ns and Js are not equal, neighbourhood is not neutral.
                        if (numberOfNInARow > numberOfJInARow)
                        {
                            // The number of Js are fewer than the previous number of Ns, we can
                            // move the index to where the number of Ns will be equals to Js.
                            currentNeighbourhoodIndex = index - (numberOfJInARow * 2);
                            currentNeighbourhoodSize = numberOfJInARow * 2;
                            balance = 0;
                        }
                        else
                        {
                            // The number of Js are more than the number of Ns, we must
                            // move the index to the start of the Ns
                            currentNeighbourhoodIndex = index - numberOfJInARow;
                            currentNeighbourhoodSize = numberOfJInARow;
                            balance = numberOfJInARow;
                        }
                    }
                    numberOfNInARow = 0;
                }
                numberOfNInARow++;
                balance--;
            }
            if (numberOfNInARow == numberOfJInARow)
            {
                var next = index + 1 < data.Count() ? data[index + 1] : '0';
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

    private void Attempt1(string data)
    {

        var currentNeighbourhoodIndex = 0;
        var currentNeighbourhoodSize = 0;
        var largestNeighbourhoodIndex = 0;
        var largestNeighbourhoodSize = 0;
        var numberOfJInARow = 0;
        var numberOfNInARow = 0;
        for (int index = 0; index < data.Length; index++)
        {
            if (data[index] == 'J')
            {
                if (index > 0 && data[index - 1] == 'N')
                {
                    if (numberOfJInARow > 0 && numberOfNInARow != numberOfJInARow)
                    {
                        if (currentNeighbourhoodSize > largestNeighbourhoodSize)
                        {
                            largestNeighbourhoodSize = currentNeighbourhoodSize;
                            largestNeighbourhoodIndex = currentNeighbourhoodIndex;
                        }
                        if (numberOfJInARow > numberOfNInARow)
                        {
                            // Where the number of Js will be in balance with the current set of Ns
                            currentNeighbourhoodIndex = index - (numberOfNInARow * 2);
                            currentNeighbourhoodSize = numberOfNInARow * 2;
                        }
                        else
                        {
                            // Start of current set of Ns
                            currentNeighbourhoodIndex = index - numberOfNInARow;
                            currentNeighbourhoodSize = 0;
                        }
                    }
                    else if (numberOfJInARow == numberOfNInARow)
                    {
                        // We've reached balance
                        if (currentNeighbourhoodSize == 0)
                        {
                            // Size was reset at previous imbalance
                            currentNeighbourhoodSize = numberOfJInARow * 2;
                        }
                        else
                        {
                            currentNeighbourhoodSize += numberOfJInARow;
                        }
                    }
                    numberOfJInARow = 1;
                }
                else
                {
                    numberOfJInARow++;
                }
            }
            else
            {
                if (index > 0 && data[index - 1] == 'J')
                {
                    if (numberOfNInARow > 0 && numberOfJInARow != numberOfNInARow)
                    {
                        if (currentNeighbourhoodSize > largestNeighbourhoodSize)
                        {
                            largestNeighbourhoodSize = currentNeighbourhoodSize;
                            largestNeighbourhoodIndex = currentNeighbourhoodIndex;
                        }
                        if (numberOfNInARow > numberOfJInARow)
                        {
                            // Where the number of Ns will be in balance with the current set of Js
                            currentNeighbourhoodIndex = index - (numberOfJInARow * 2);
                            currentNeighbourhoodSize = numberOfJInARow * 2;
                        }
                        else
                        {
                            // Start of current set of Js
                            currentNeighbourhoodIndex = index - numberOfJInARow;
                            currentNeighbourhoodSize = 0;
                        }
                    }
                    else if (numberOfNInARow == numberOfJInARow)
                    {
                        // We've reached balance
                        if (currentNeighbourhoodSize == 0)
                        {
                            // Size was reset at previous imbalance
                            currentNeighbourhoodSize = numberOfNInARow * 2;
                        }
                        else
                        {
                            currentNeighbourhoodSize += numberOfNInARow;
                        }
                    }
                    numberOfNInARow = 1;
                }
                else
                {
                    numberOfNInARow++;
                }
            }
        }
        if (currentNeighbourhoodSize > largestNeighbourhoodSize)
        {
            largestNeighbourhoodSize = currentNeighbourhoodSize;
            largestNeighbourhoodIndex = currentNeighbourhoodIndex;
        }
        Console.WriteLine($"Knowit Day 3 Result: {largestNeighbourhoodSize} {largestNeighbourhoodIndex}");
    }
}
