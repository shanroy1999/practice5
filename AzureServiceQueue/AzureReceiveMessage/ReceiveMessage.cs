using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo1
{
    public static class ReceiveMessage
    {
        [FunctionName("ReceiveMessage")]
        public static void Run([ServiceBusTrigger("appqueue", Connection = "servicebus-connection")]Message myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            log.LogInformation($"Message Body : {myQueueItem.Body.ToString()}");
            log.LogInformation($"Content Type : {myQueueItem.ContentType}");
        }
    }
}