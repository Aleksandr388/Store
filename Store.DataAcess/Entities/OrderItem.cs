using Store.DataAcess.Entities.Base;
using Store.DataAcess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace Store.DataAcess.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Amounnt { get; set; }
        public int Count { get; set; }
        public Curency Curency { get; set; }

        public long PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
