using Shared.Constants;

namespace Store.BusinessLogic.Models.PaginationModels
{
    public class PageModel
    {
        public int MaxPageSize { get; private set; } = DefaultValues.MaxPageValue;

        private int _pageSize = DefaultValues.DefaultPageSIzeValue;
        public int PageNumber { get; private set; }
        public int Сount { get; set; }

        public PageModel( int pageNumber, int pageSize, int count)
        {
            Сount = count;
            PageNumber = pageNumber;
            _pageSize = pageSize;
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}
