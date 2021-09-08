using Dapper.Contrib.Extensions;
using Store.DataAcess.Entities.Base;
using System.Collections.Generic;

namespace Store.DataAcess.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        [Write(false)]
        [Computed]
        public virtual List<PrintingEdition> PrintingEditions { get; set; } = new List<PrintingEdition>();
    }
}
