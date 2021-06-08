using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PaginationModels
{
    public class ResponseModel<T>
    {
        public PageModel PageModel { get; set; }
        public IEnumerable<T> NavigationModels { get; set; }

    }
}
