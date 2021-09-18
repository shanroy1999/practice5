using Azure.Messaging.ServiceBus;
using System;

namespace AzureReceiveMessage
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://shanbus.servicebus.windows.net/;SharedAccessKeyName=ReceiveMessage;SharedAccessKey=lVJ9LJ+filvPmOudXBIpWsV128MPpwqSwfbjUjY8El8=;EntityPath=appqueue2";
        private static string queue_name = "appqueue2";

        static void Main(string[] args)
        {

            // For receiving Message
            ServiceBusClient client = new ServiceBusClient(connection_string);

            // Create a receiver for the service bus
            // Ensure that we only peek on the messages in the queue => ServiceBusReceiverOptions object
            ServiceBusReceiver receiver = client.CreateReceiver(queue_name,
                new ServiceBusReceiverOptions()
                {
                    // ReceiveMode = ServiceBusReceiveMode.PeekLock
                    ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                });

            // Create a received message from the receiver object
            var messages_received = receiver.ReceiveMessagesAsync(3).GetAwaiter().GetResult();

            Console.WriteLine("Messages Received : ");

            // Write the received message body on the console
            foreach (var message in messages_received)
            {
                Console.WriteLine(message.SequenceNumber);
                Console.WriteLine(message.Body.ToString());
            }

            foreach (var message in messages_received)
            {
                foreach (var key in message.ApplicationProperties.Keys)
                {
                    Console.WriteLine($"Key : " + key.ToString());
                    Console.WriteLine($"Value : " + message.ApplicationProperties[key].ToString());
                }
            }
        }
    }
}
