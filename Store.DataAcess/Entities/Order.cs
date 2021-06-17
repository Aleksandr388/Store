using Shared.Enums;
using Store.DataAcess.Entities.Base;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long PaymentId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public long UserId { get; set; }
        public StoreUser User { get; set; }
    }
}
