using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Models;

namespace Store.BusinessLogic.Mapping
{
    public class PageMappingProfile : Profile
    {
        public PageMappingProfile()
        {
            CreateMap<PrintingEditionFiltration, PrintingEditionFiltrationModel>().ReverseMap();
            CreateMap<AuthorFiltration, AuthorFiltrationModel>().ReverseMap();
            CreateMap<OrderFiltration, OrderFiltrationModel>().ReverseMap();
        }
    }
}
