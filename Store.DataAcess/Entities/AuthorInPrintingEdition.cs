using Store.DataAcess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities
{
    public class AuthorInPrintingEdition : BaseEntity
    {
        public long AuthorId { get; set; } 
        public Author Author { get; set; }
        public long PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
    }
}
