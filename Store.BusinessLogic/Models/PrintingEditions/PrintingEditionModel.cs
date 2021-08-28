using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Base;
using Store.DataAcess.Entities;
using System;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Shared.Enums.StatusType Status { get; set; }
        public Shared.Enums.CurencyType Curency { get; set; }
        public Shared.Enums.EditionType Type { get; set; }
        public ICollection<AuthorModel> AuthorModels { get; set; }
        public List<Author> AuthorsModel { get; set; }
    }
}
