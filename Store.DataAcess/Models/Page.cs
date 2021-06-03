using System;

namespace Store.DataAcess.Models
{
    public class Page
    {
        public int MaxPageSize { get; private set; }

        private int _pageSize;
        public int PageNumber { get; private set; } 

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
