using Shared.Enums;
using Store.DataAcess.Entities.Base;
using System;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
        public CurencyType Curency { get; set; }
        public EditionType Type { get; set; }

        public virtual List<Author> Authors { get; set; } = new List<Author>();
    }
}
