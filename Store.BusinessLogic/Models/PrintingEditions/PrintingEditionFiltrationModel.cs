using Shared.Enums;
using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionFiltrationModel : BasePageSortModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public List<Type> Type { get; set; }
        public string NameAuthor { get; set; }
    }
}
