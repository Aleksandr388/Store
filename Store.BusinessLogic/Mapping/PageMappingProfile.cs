using AutoMapper;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Models;

namespace Store.BusinessLogic.Mapping
{
    public class PageMappingProfile : Profile
    {
        public PageMappingProfile()
        {
            CreateMap<PrintingEditionPaginationFiltrationSort, PrintingEditionPaginationFiltrationSortModel>().ReverseMap();
        }
    }
}
