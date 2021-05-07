using Store.DataAcess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities
{
    public class Payment : BaseEntity
    {
        public long TransactionId { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
