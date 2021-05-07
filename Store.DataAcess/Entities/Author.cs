using Store.DataAcess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<PrintingEdition> PrintingEditions { get; set; }
        public ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}
