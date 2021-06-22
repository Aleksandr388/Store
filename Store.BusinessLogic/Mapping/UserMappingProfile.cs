using AutoMapper;
using Store.BusinessLogic.Models.Users;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserSignUpModel, StoreUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
