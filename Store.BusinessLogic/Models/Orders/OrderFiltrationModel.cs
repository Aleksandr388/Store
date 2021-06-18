using Shared.Enums;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderFiltrationModel : BasePageSortModel
    {
        public string Description { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string User { get; set; }
    }
}
