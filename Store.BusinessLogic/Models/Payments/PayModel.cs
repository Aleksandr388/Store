using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Payments
{
    public class PayModel
    {
        public long OrderId { get; set; } 
        public string Description { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public long TransactionId { get; set; }
        public IEnumerable<PriceModel> PriceModels { get; set; }
    }
}
