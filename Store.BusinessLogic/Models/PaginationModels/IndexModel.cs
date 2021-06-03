using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PaginationModels
{
    public class IndexModel<T>
    {
        public IEnumerable<T> NavigationModels { get; set; }
        public PageModel PageModel { get; set; }
    }
}
