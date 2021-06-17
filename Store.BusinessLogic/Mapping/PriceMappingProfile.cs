using AutoMapper;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Models;

namespace Store.BusinessLogic.Mapping
{
    public class PriceMappingProfile : Profile
    {
        public PriceMappingProfile()
        {
            CreateMap<PriceDal, PriceModel>().ReverseMap();
        }
    }
}
