using Shared.Enums;
using Store.BusinessLogic.Models.Users;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel
    {
        public string Description { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Unpaid;
        public long PaymentId { get; set; }

        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();

        public long UserId { get; set; }
        public UserModel User { get; set; }
    }
}
