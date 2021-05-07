using Store.DataAcess.Entities.Base;
using Store.DataAcess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public Curency Curency { get; set; }
        public Enums.Type Type { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }

    }
}
