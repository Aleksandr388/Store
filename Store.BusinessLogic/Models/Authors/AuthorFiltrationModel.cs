using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorFiltrationModel : BasePageSortModel
    {
        public string Name { get; set; }
        public string PrintingEditionTitle { get; set; }
    }
}
