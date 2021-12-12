namespace AdventCalendar2021.AdventOfCode;
public class Day12
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day12Input.txt").ToList();
        var caves = input.SelectMany(i => i.Split("-")).ToHashSet().Select(c => new Cave(c)).ToHashSet();
        var cavesMapped = caves.ToDictionary(c => c.Name);
        input.ForEach(connection =>
        {
            var parts = connection.Split("-");
            cavesMapped[parts[0]].AddConnection(cavesMapped[parts[1]]);
            cavesMapped[parts[1]].AddConnection(cavesMapped[parts[0]]);
        });
        Part1(cavesMapped);
        Part2(cavesMapped);
    }

    private static void Part1(Dictionary<string, Cave> cavesMapped)
    {
        var start = cavesMapped["start"];
        var end = cavesMapped["end"];
        var paths = FindPaths(start, end, new() { start }, false);
        Console.WriteLine($"AdventOfCode Day 12 Part 1: {paths.Count}");
    }

    private static void Part2(Dictionary<string, Cave> cavesMapped)
    {
        var start = cavesMapped["start"];
        var end = cavesMapped["end"];
        var paths = FindPaths(start, end, new() { start }, true);
        // Didn't bother to find out how to eliminate duplicates in the FindPaths
        // method, so just using the magic of HashSet on strings to take care of it
        var uniquePaths = paths.Select(path => string.Join(',', path)).ToHashSet();
        Console.WriteLine($"AdventOfCode Day 12 Part 2: {uniquePaths.Count}");
    }

    private static HashSet<List<Cave>> FindPaths(Cave start, Cave end, HashSet<Cave> visitedSmallCaves, bool allowSmallDouble = false)
    {
        var paths = new HashSet<List<Cave>>();
        var runs = 1;
        if (allowSmallDouble && start.Name != "start" && start.Size == CaveSize.SMALL)
            // Run through subpaths twice. Once were we do not allow revisiting 'start',
            // and once were revisting is allowed.
            runs = 2;
        for (int run = 1; run <= runs; run++)
        {
            if (run == 2) visitedSmallCaves.Remove(start);
            foreach (var connectedCave in start.ConnectedCaves)
            {
                var path = new List<Cave>() { start };
                var subCavesVisited = visitedSmallCaves.ToHashSet();
                if (connectedCave == end)
                {
                    path.Add(end);
                    paths.Add(path);
                    continue;
                }
                else if (visitedSmallCaves.Contains(connectedCave))
                    continue;
                if (connectedCave.Size == CaveSize.SMALL)
                    subCavesVisited.Add(connectedCave);
                var subPaths = FindPaths(connectedCave, end, subCavesVisited, allowSmallDouble && run == 1);
                subPaths.ToList().ForEach(subPath => paths.Add(path.Concat(subPath).ToList()));
            }
        }
        return paths;
    }

    internal class Cave
    {
        public CaveSize Size;
        public string Name;
        public HashSet<Cave> ConnectedCaves = new();

        public Cave(string name)
        {
            Name = name;
            Size = name == name.ToUpper() ? CaveSize.BIG : CaveSize.SMALL;
        }

        public void AddConnection(Cave cave)
        {
            ConnectedCaves.Add(cave);
        }

        public override string ToString() => Name;
    }

    internal enum CaveSize
    {
        BIG, SMALL
    }
}
