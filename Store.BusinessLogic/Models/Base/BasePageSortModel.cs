using Shared.Constants;

namespace Store.BusinessLogic.Models.Base
{
    public class BasePageSortModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsAccesing { get; set; }
        public string SortOrder { get; set; }

        public BasePageSortModel()
        {
            PageNumber = 1;
            PageSize = DefaultValues.DefaultPageSIzeValue;
            SortOrder = DefaultValues.CreationDate;
            IsAccesing = true;
        }
    }
}
