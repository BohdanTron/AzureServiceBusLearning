namespace ServiceBusQueues
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class WeatherForecastAdded
    {
        public Guid Id { get; set; }
        public DateOnly ForDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
