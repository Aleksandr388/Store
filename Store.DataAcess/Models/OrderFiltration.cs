using Shared.Enums;

namespace Store.DataAcess.Models
{
    public class OrderFiltration : BasePageSort
    {
        public string Description { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string User { get; set; }
    }
}
