namespace AdventCalendar2021.AdventOfCode;
public class Day12
{
    public static void Run()
    {
        var input = File.ReadAllLines("AdventOfCode\\Data\\Day12Input.txt").ToList();
        var exampleInput = new List<string>()
        {
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc"
        };
        var exampleCaves = exampleInput.SelectMany(i => i.Split("-")).ToHashSet().Select(c => new Cave(c)).ToHashSet();
        var exampleCavesMapped = exampleCaves.ToDictionary(c => c.Name);
        exampleInput.ForEach(connection =>
        {
            var parts = connection.Split("-");
            exampleCavesMapped[parts[0]].AddConnection(exampleCavesMapped[parts[1]]);
            exampleCavesMapped[parts[1]].AddConnection(exampleCavesMapped[parts[0]]);
        });
        Part1(exampleCavesMapped);
        var caves = input.SelectMany(i => i.Split("-")).ToHashSet().Select(c => new Cave(c)).ToHashSet();
        var cavesMapped = caves.ToDictionary(c => c.Name);
        input.ForEach(connection =>
        {
            var parts = connection.Split("-");
            cavesMapped[parts[0]].AddConnection(cavesMapped[parts[1]]);
            cavesMapped[parts[1]].AddConnection(cavesMapped[parts[0]]);
        });
        Part1(cavesMapped);
    }

    private static void Part1(Dictionary<string, Cave> cavesMapped)
    {
        var start = cavesMapped["start"];
        var end = cavesMapped["end"];
        var paths = FindPaths(start, end, new() { start });
        Console.WriteLine($"AdventOfCode Day 12 Part 1: {paths.Count}");
    }

    private static HashSet<List<Cave>> FindPaths(Cave start, Cave end, List<Cave> visitedSmallCaves)
    {
        var paths = new HashSet<List<Cave>>();
        foreach (var connectedCave in start.ConnectedCaves)
        {
            var path = new List<Cave>() { start };
            var subCavesVisited = visitedSmallCaves.ToList();
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
            var subPaths = FindPaths(connectedCave, end, subCavesVisited);
            subPaths.ToList().ForEach(subPath => paths.Add(path.Concat(subPath).ToList()));
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
