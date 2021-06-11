using Store.BusinessLogic.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Payments
{
    public class PaymentModel : BaseModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public long OrderId { get; set; }
    }
}
