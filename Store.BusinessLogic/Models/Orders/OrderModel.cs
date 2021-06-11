using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel
    {
        public string Description { get; set; }

        public ICollection<OrderItemModel> OrderItemsModel { get; set; }

        public long UserId { get; set; }
        public UserModel StoreUser { get; set; }

        public long PaymentId { get; set; }
        public PaymentModel Payment { get; set; }
    }
}
