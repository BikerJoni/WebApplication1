using Microsoft.AspNetCore.Mvc;
using WebApplication1.Brokers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private IStorageBroker istorageBroker; 
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IStorageBroker storageBroker)
        {
            this.istorageBroker = storageBroker;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task< IEnumerable<WeatherForecast>> Get()
        {

            Subject subject = new Subject();
            subject.Name = "Jahongir";
            subject.ID = Guid.NewGuid();
            await istorageBroker.InsertSubjectAsync(subject);
            

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
