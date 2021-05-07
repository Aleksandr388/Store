using Store.DataAcess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities
{
    public class Order : BaseEntity
    {
        public string Desription { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public long UserId { get; set; }
        public StoreUser StoreUser { get; set; }

        public long PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
