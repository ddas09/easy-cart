using AutoMapper;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Models.Response;

namespace EasyCart.AuthService.Models.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterRequest, User>();

        CreateMap<User, UserInformation>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Role == "Admin"));
    }
}