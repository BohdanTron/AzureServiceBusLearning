using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Processor
{
    public class ProcessQueue
    {
        [FunctionName("ProcessQueue")]
        public void Run([ServiceBusTrigger("add-weather-data", Connection = "WeatherDataConnection")] string myQueueItem, ILogger log)
        {
            if (myQueueItem.Contains("exception"))
                throw new Exception();

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
