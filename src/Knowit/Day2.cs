using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventCalendar2021.Knowit;
public class Day2
{

    public static void Run()
    {
        var regex = new Regex("^(.+),Point\\((.+) (.+)\\)$");
        var cities = File.ReadAllLines("Knowit\\Data\\cities.csv")
            .Select(line => LineToCity(line, regex))
            .ToList();
        var radius = 6371;
        // Assuming the north pole vector ¯\_(ツ)_/¯
        var start = new Vector2(90, 90);
        var currentPosition = start;
        var totalDistance = 0f;
        while (cities.Count > 0)
        {
            var nextCity = cities.OrderBy(c => Vector2.Distance(currentPosition, c.Position)).First();
            cities.Remove(nextCity);
            var distance = Vector2.Distance(currentPosition, nextCity.Position);
            totalDistance += (distance / 180) * radius;
            currentPosition = nextCity.Position;
        }
        // Back to the north pole
        totalDistance += (Vector2.Distance(currentPosition, start) / 180) * radius;
        currentPosition = start;
        Console.WriteLine($"Total distance {totalDistance}");
    }

    private static City LineToCity(string line, Regex regex)
    {
        var regexMatch = regex.Match(line);
        return new City(
            regexMatch.Groups[1].Value,
            float.Parse(regexMatch.Groups[2].Value.Replace(".", ",")),
            float.Parse(regexMatch.Groups[3].Value.Replace(".", ","))
        );
    }

    internal class City
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }

        public City(string name, float x, float y)
        {
            Name = name;
            Position = new Vector2(x, y);
        }
    }
}
