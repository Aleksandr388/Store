using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorModel>().ReverseMap();
        }
    }
}
