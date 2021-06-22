using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
        }
    }
}
