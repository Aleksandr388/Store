using Store.DataAcess.Entities;
using System.Collections.Generic;

namespace Store.DataAcess.Models
{
    public class Pay
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public long TransactionId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
