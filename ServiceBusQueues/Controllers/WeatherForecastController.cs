using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ServiceBusQueues.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ServiceBusClient serviceBusClient,
            ILogger<WeatherForecastController> logger)
        {
            _serviceBusClient = serviceBusClient;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast data)
        {
            var sender = _serviceBusClient.CreateSender("add-weather-data");

            var message = new ServiceBusMessage(JsonSerializer.Serialize(data));

            if (data.Summary?.Contains("scheduled") == true)
                message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(10);

            await sender.SendMessageAsync(message);
        }
    }
}
