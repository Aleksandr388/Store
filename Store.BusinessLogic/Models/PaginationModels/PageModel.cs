using System;

namespace Store.BusinessLogic.Models.PaginationModels
{
    public class PageModel
    {
        public int MaxPageSize { get; private set; } = 50;

        private int _pageSize = 10;
        public int PageNumber { get; private set; } = 1;

        public PageModel(int pageNumber)
        {
            PageNumber = pageNumber;
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
