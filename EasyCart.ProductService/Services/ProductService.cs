using LinqKit;
using AutoMapper;
using System.Linq.Expressions;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using EasyCart.ProductService.DAL.Contracts;
using EasyCart.ProductService.DAL.Entities;
using EasyCart.ProductService.Models.Request;
using EasyCart.ProductService.Models.Response;
using EasyCart.ProductService.Services.Contracts;

namespace EasyCart.ProductService.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService
    (
        IMapper mapper, 
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductInformation> AddProduct(ProductAddRequest request)
    {
        var product = this._mapper.Map<Product>(request);
        product.UpdatedBy = request.CreatedBy;

        await this._productRepository.Add(product);

        return this._mapper.Map<ProductInformation>(product);
    }

    public async Task<ProductInformation> UpdateProduct(ProductUpdateRequest request)
    {
        var product = await this._productRepository.Get(p => p.Id == request.Id)
            ?? throw new ApiException(message: "Product with this id doesn't exists.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UpdatedDate = DateTime.UtcNow;
        product.UpdatedBy = request.UpdatedBy;

        await this._productRepository.Update(product);

        return this._mapper.Map<ProductInformation>(product);
    }

    public async Task DeleteProduct(ProductDeleteRequest request)
    {
        var product = await this._productRepository.Get(p => p.Id == request.Id)
            ?? throw new ApiException(message: "Product with this id doesn't exists.", errorCode: AppConstants.ErrorCodeEnum.NotFound);
        
        // just deactivate the product
        product.IsActive = false;
        product.UpdatedDate = DateTime.UtcNow;
        product.UpdatedBy = request.DeletedBy;

        await this._productRepository.Update(product);
    }

    public async Task<ProductResponse> GetAllProducts(string searchText)
    {
        Expression<Func<Product, bool>> predicate = p => p.IsActive;

        // If searchText is provided, add conditions to match product name or description
        if (!string.IsNullOrEmpty(searchText))
        {
            string searchFilter = searchText.Trim().ToLower();
            predicate = predicate.And(p => p.Name.ToLower().Contains(searchFilter) || p.Description.ToLower().Contains(searchFilter));
        }

        var activeProducts = await this._productRepository.GetList(predicate: predicate);

        return new ProductResponse
        {
            Products = this._mapper.Map<List<ProductInformation>>(activeProducts)
        };
    }

    public async Task<ProductInformation> GetProduct(int productId)
    {
        var product = await this._productRepository.Get(p => p.Id == productId && p.IsActive)
            ?? throw new ApiException(message: "Product with this id doesn't exists.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        return this._mapper.Map<ProductInformation>(product);
    }
}