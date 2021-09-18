using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;

namespace AzureServiceQueue
{
    class Program
    {
        // Add 2 properties => connection string and queue name
        private static string connection_string = "Endpoint=sb://shanbus.servicebus.windows.net/;SharedAccessKeyName=SendMessage;SharedAccessKey=L67apJ4lkRqU6tHojTTqgpgsYP+D60ecMOX4X/3iTG4=;EntityPath=appqueue2";
        private static string queue_name = "appqueue2";

        static void Main(string[] args)
        {
            // Create a list of new orders => add orders as messages on the queue
            List<Order> orders = new List<Order>()
            {
                new Order() { OrderID = "11", Quantity = 4, UnitPrice = 9.99m},
                new Order() { OrderID = "12", Quantity = 5, UnitPrice = 19.99m},
                new Order() { OrderID = "13", Quantity = 6, UnitPrice = 29.99m},
                new Order() { OrderID = "14", Quantity = 7, UnitPrice = 39.99m},
                new Order() { OrderID = "15", Quantity = 8, UnitPrice = 49.99m}
            };

            // Create a ServiceBusClient => allows to interact with the ServiceBus
            ServiceBusClient client = new ServiceBusClient(connection_string);

            // Create a ServiceBusSender => send messages on the specified queue
            ServiceBusSender sender = client.CreateSender(queue_name);

            // Loop through each of the order in the Orders List
            foreach(Order order in orders)
            {
                // Create a message => Entire Json string sent as a message
                ServiceBusMessage message = new ServiceBusMessage(order.ToString());

                // Set the content type of each of the messages
                message.ContentType = "application/json";

                // Set the Time to Live Property for each message in the queue
                message.TimeToLive = TimeSpan.FromSeconds(30);

                // Add Key-Value Message property for the message being sent
                message.ApplicationProperties.Add("Department", "Finance");

                sender.SendMessageAsync(message).GetAwaiter().GetResult();
            };

            Console.WriteLine("All Messages have been Sent");

        }
    }
}
