using Store.DataAcess.Entities.Base;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<PrintingEdition> PrintingEditions { get; set; } = new List<PrintingEdition>();
    }
}
