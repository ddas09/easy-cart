using AutoMapper;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.OrderService.Models.Request;
using EasyCart.OrderService.Models.Response;

namespace EasyCart.OrderService.Models.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<OrderCreateRequest, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore());

        CreateMap<OrderItem, OrderItemInformation>();

        CreateMap<OrderItemRequest, OrderItem>();
    }
}