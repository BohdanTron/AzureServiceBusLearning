using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Processor
{
    public class ProcessTopic
    {
        [FunctionName(nameof(SendEmail))]
        public void SendEmail([ServiceBusTrigger("weather-forecast-added", "send-email", Connection = "WeatherDataConnection")] string mySbMsg, ILogger log)
        {
            log.LogInformation($"Sending email: {mySbMsg}");
        }

        [FunctionName(nameof(UpdateReport))]
        public void UpdateReport([ServiceBusTrigger("weather-forecast-added", "update-report", Connection = "WeatherDataConnection")] string mySbMsg, ILogger log)
        {
            log.LogInformation($"Updating report: {mySbMsg}");
        }
    }
}
