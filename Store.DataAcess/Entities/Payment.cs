using Store.DataAcess.Entities.Base;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    public class Payment : BaseEntity
    {
        public long OrderId { get; set; }
    }
}
