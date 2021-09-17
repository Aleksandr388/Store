using AutoMapper;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class PrintingEditionMappingProfile : Profile
    {
        public PrintingEditionMappingProfile()
        {
            CreateMap<PrintingEdition, PrintingEditionModel>()
                .ForMember(x => x.AuthorModels, opt => opt.MapFrom(x => x.Authors))
                .ReverseMap();
        }
    }
}
