using AutoMapper;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.Models.Request;

namespace EasyCart.AuthService.Models.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterRequest, User>();
    }
}