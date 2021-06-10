using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderModel>().ReverseMap();
        }
    }
}
