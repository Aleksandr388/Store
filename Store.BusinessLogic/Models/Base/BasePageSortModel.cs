namespace Store.BusinessLogic.Models.Base
{
    public class BasePageSortModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsAccesing { get; set; }
        public string SortIndex { get; set; }

        public BasePageSortModel()
        {
            PageNumber = 1;
            PageSize = Shared.Constants.DefaultValues.DefaultPageSIzeValue;
            SortIndex = "CreationDate";
            IsAccesing = true;
        }
    }
}
