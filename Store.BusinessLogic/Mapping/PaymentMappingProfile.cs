using AutoMapper;
using Store.BusinessLogic.Models.Payments;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<Payment, PaymentModel>().ReverseMap();
        }
    }
}
