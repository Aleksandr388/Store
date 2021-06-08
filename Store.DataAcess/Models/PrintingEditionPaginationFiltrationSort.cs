using Shared.Enums;
using System.Collections.Generic;

namespace Store.DataAcess.Models
{
    public class PrintingEditionPaginationFiltrationSort : BasePaginationFiltrarionSortDAL
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public List<Type> Type { get; set; }
        public string NameAuthor { get; set; }

    }
}
