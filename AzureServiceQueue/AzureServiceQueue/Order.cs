using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AzureServiceQueue
{
    class Order
    {
        // Adding Properties of the Order Class
        public string OrderID { get; set; }

        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }

        // Override ToString method
        public override string ToString()
        {
            // Get Json String representation of properties of object
            return JsonSerializer.Serialize(this);
        }
    }
}
