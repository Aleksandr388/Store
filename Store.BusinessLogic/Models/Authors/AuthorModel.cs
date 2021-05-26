using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        public string Name { get; set; }
        public ICollection<PrintingEditionModel> PrintingEditionModels { get; set; }
    }
}
