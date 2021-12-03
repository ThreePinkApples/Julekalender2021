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
    }

    private static void Vector2Attempt(List<City> cities)
    {
        // Assuming the north pole vector ¯\_(ツ)_/¯
        var northPole = new City("North Pole", 90.0, 90.0);
        var currentCity = northPole;
        var totalDistance = 0.0;
        while (cities.Count > 0)
        {
            var nextCity = cities.OrderBy(c => Haversine(currentCity, c)).First();
            cities.Remove(nextCity);
            var distance = Haversine(currentCity, nextCity);
            totalDistance += distance;
            currentCity = nextCity;
        }
        // Back to the north pole
        totalDistance += Haversine(currentCity, northPole);
        currentCity = northPole;
        Console.WriteLine($"Total distance {totalDistance}");
    }

    private static double Haversine(City from, City to)
    {
        var dLat = to.LatitudeRadians - from.LatitudeRadians;
        var dLong = to.LongitudeRadians - from.LongitudeRadians;
        var sdLat = Math.Sin(dLat / 2);
        var sdLong = Math.Sin(dLong / 2);
        var q = (sdLat * sdLat) + Math.Cos(from.LatitudeRadians) * Math.Cos(to.LatitudeRadians) * (sdLong * sdLong);
        var distance = 2 * RadiusEarth * Math.Asin(Math.Min(1, Math.Sqrt(q)));

        return distance;
    }

    private static double ToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    private static City LineToCity(string line, Regex regex)
    {
        var regexMatch = regex.Match(line);
        var x = double.Parse(regexMatch.Groups[2].Value.Replace(".", ","));
        var y = double.Parse(regexMatch.Groups[3].Value.Replace(".", ","));
        return new City(regexMatch.Groups[1].Value, x, y);
    }

    internal class City
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double LongitudeRadians { get; set; }
        public double Latitude { get; set; }
        public double LatitudeRadians { get; set; }

        public City(string name, double longitude, double latitude)
        {
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            LongitudeRadians = ToRadians(longitude);
            LatitudeRadians = ToRadians(latitude);
        }
    }
}
