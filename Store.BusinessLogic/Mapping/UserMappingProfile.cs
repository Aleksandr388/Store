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
            //CreateMap<StoreUser, UserModel>()
            //    .ForMember(destination => destination.FullName, options => options.MapFrom(sourse => sourse.FirstName + " " + sourse.LastName))
            //    .ReverseMap();
            CreateMap<StoreUser, UserModel>()
                .ReverseMap();
        }
    }
}
