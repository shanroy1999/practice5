using Azure.Messaging.ServiceBus;
using System;

namespace ReceiveDealLetter
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://shanbus.servicebus.windows.net/;SharedAccessKeyName=ReceiveMessage;SharedAccessKey=lVJ9LJ+filvPmOudXBIpWsV128MPpwqSwfbjUjY8El8=;EntityPath=appqueue2/$DeadLetterQueue";
        private static string queue_name = "appqueue2/$DeadLetterQueue";

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
            var message_received = receiver.ReceiveMessagesAsync(3).GetAwaiter().GetResult();

            Console.WriteLine("Dead Letter Received : ");

            foreach(var message in message_received)
            {
                Console.WriteLine($"Dead Letter Reason : {message.DeadLetterReason}");
                Console.WriteLine($"Dead Letter Description : {message.DeadLetterErrorDescription}");
                Console.WriteLine(message.Body);
            }
        }
    }
}
