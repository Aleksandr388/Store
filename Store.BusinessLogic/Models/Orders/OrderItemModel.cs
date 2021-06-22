using Shared.Enums;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModel
    {
        public decimal Price { get; set; }
        public int Count { get; set; }
        public CurencyType Curency { get; set; }

        public long PrintingEditionId { get; set; }
        public PrintingEditionModel PrintingEdition { get; set; }

        public long OrderId { get; set; }
        public OrderModel Order { get; set; }
    }
}
