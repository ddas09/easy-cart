using AutoMapper;
using EasyCart.CartService.DAL.Entities;
using EasyCart.CartService.Models.Request;
using EasyCart.CartService.Models.Response;

namespace EasyCart.CartService.Models.Mappings;

public class CartItemMappingProfile : Profile
{
    public CartItemMappingProfile()
    {
        CreateMap<CartItem, CartItemInformation>();

        CreateMap<CartItemAddRequest, CartItem>();
    }
}