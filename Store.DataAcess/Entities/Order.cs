using Shared.Enums;
using Store.DataAcess.Entities.Base;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public long PaymentId { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public long UserId { get; set; }
        public virtual StoreUser User { get; set; }
    }
}
