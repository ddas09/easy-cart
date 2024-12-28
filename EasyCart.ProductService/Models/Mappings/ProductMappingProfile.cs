using AutoMapper;
using EasyCart.ProductService.DAL.Entities;
using EasyCart.ProductService.Models.Request;
using EasyCart.ProductService.Models.Response;

namespace EasyCart.ProductService.Models.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductAddRequest, Product>();

        CreateMap<ProductUpdateRequest, Product>();

        CreateMap<Product, ProductInformation>();
    }
}