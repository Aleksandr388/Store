using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Base;
using Store.DataAcess.Entities;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Shared.Enums.Status Status { get; set; }
        public Shared.Enums.Curency Curency { get; set; }
        public Shared.Enums.Type Type { get; set; }
        public ICollection<AuthorModel> AuthorModels { get; set; }
    }
}
