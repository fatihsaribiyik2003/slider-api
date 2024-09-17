using SubuProtokol.Models;

namespace SubuProtokol.Services.NoContext
{
    public interface ILocationService
    {
        bool IsExists(string cityName);
        IEnumerable<WeatherForecast> List();
    }

    public class LocationService : ILocationService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public bool IsExists(string cityName)
        {
            return false;
        }

        public IEnumerable<WeatherForecast> List()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
