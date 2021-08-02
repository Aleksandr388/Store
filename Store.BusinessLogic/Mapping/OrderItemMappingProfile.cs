using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;

namespace Store.BusinessLogic.Mapping
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItemModel, OrderItem>().ForMember(x => x.CreationDate, opt => opt.Ignore());
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<Order, OrderItem>();
            CreateMap<OrderFiltration, OrderFiltrationModel>().ReverseMap();
        }
    }
}
