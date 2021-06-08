using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Models.Base
{
    public class BasePaginationFiltrationSortModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public BasePaginationFiltrationSortModel()
        {
            PageNumber = 1;
            PageSize = Shared.Constants.DefaultValues.DefaultPageSIzeValue;
        }
    }
}
