using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventCalendar2021.Knowit;
public class Day2
{
    private const int RadiusEarth = 6371;

    public static void Run()
    {
        var regex = new Regex("^(.+),Point\\((.+) (.+)\\)$");
        var cities = File.ReadAllLines("Knowit\\Data\\cities.csv")
            .Select(line => LineToCity(line, regex));
        Vector2Attempt(cities.ToList());
        Vector3Attempt(cities.ToList());
    }

    private static void Vector2Attempt(List<City> cities)
    {
        // Assuming the north pole vector ¯\_(ツ)_/¯
        var start = new Vector2(90, 90);
        var currentPosition = start;
        var totalDistance = 0f;
        while (cities.Count > 0)
        {
            var nextCity = cities.OrderBy(c => Vector2.Distance(currentPosition, c.Position2)).First();
            cities.Remove(nextCity);
            var distance = Vector2.Distance(currentPosition, nextCity.Position2);
            // 180 is diameter I guess, so Raidus * 2?
            totalDistance += (distance / 180) * RadiusEarth * 2;
            currentPosition = nextCity.Position2;
        }
        // Back to the north pole
        totalDistance += Vector2.Distance(currentPosition, start);
        currentPosition = start;
        Console.WriteLine($"Total Vector2 distance {totalDistance}");
    }

    private static void Vector3Attempt(List<City> cities)
    {
        // Assuming the north pole vector ¯\_(ツ)_/¯
        var start = CoordinatesToVector3(90, 90);
        var currentPosition = start;
        var totalDistance = 0f;
        while (cities.Count > 0)
        {
            var nextCity = cities.OrderBy(c => Vector3.Distance(currentPosition, c.Position3)).First();
            cities.Remove(nextCity);
            var distance = Vector3.Distance(currentPosition, nextCity.Position3);
            totalDistance += distance;
            currentPosition = nextCity.Position3;
        }
        // Back to the north pole
        totalDistance += Vector3.Distance(currentPosition, start);
        currentPosition = start;
        Console.WriteLine($"Total Vector3 distance {totalDistance}");
    }

    private static City LineToCity(string line, Regex regex)
    {
        var regexMatch = regex.Match(line);
        var x = float.Parse(regexMatch.Groups[2].Value.Replace(".", ","));
        var y = float.Parse(regexMatch.Groups[3].Value.Replace(".", ","));
        return new City(
            regexMatch.Groups[1].Value,
            CoordinatesToVector3(x, y),
            new(x, y)
        );
    }

    public static Vector3 CoordinatesToVector3(float latitude, float longitude)
    {
        double latitude_rad = latitude * Math.PI / 180;
        double longitude_rad = longitude * Math.PI / 180;

        double zPos = RadiusEarth * Math.Cos(latitude_rad) * Math.Cos(longitude_rad);
        double xPos = -RadiusEarth * Math.Cos(latitude_rad) * Math.Sin(longitude_rad);
        double yPos = RadiusEarth * Math.Sin(latitude_rad);

        return new Vector3((float)xPos, (float)yPos, (float)zPos);
    }

    internal class City
    {
        public string Name { get; set; }
        public Vector3 Position3 { get; set; }
        public Vector2 Position2 { get; set; }

        public City(string name, Vector3 position3, Vector2 position2)
        {
            Name = name;
            Position3 = position3;
            Position2 = position2;
        }
    }
}
