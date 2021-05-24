using AutoMapper;
using Store.BusinessLogic.Models.Users;
using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<StoreUser, UserModel>().ReverseMap();
        }
    }
}
