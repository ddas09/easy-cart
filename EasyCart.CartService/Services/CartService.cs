using AutoMapper;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using EasyCart.CartService.DAL.Entities;
using EasyCart.CartService.DAL.Contracts;
using EasyCart.CartService.Services.Contracts;
using EasyCart.CartService.Models.Response;
using EasyCart.CartService.Models.Request;

namespace EasyCart.CartService.Services;

public class CartService : ICartService
{
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public CartService
    (
        IMapper mapper, 
        ICartRepository cartRepository,
        ICartItemRepository cartItemRepository)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task RemoveCartItem(CartItemRemoveRequest request)
    {
        var userCart = await this._cartRepository.Get(predicate: c => c.Id == request.CartId)
            ?? throw new ApiException(message: "Cart with this id doesn't exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);


        var cartItem = await _cartItemRepository.Get(item => item.Id == request.CartItemId) 
            ?? throw new ApiException(message: "Item not present in cart.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        await _cartItemRepository.Delete(cartItem);
    }

    public async Task<CartItemInformation> AddCartItem(CartItemAddRequest request)
    {
        var userCart = await this._cartRepository.Get(predicate: c => c.Id == request.CartId)
            ?? throw new ApiException(message: "Cart with this id doesn't exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        // Check if the item already exists in the cart
        var existingItem = _cartItemRepository.Get(item => item.CartId == request.CartId && item.ProductId == request.ProductId);
        if (existingItem != null)
        {
            throw new ApiException(message: "Item already present in cart.", errorCode: AppConstants.ErrorCodeEnum.BadRequest);
        }

        var cartItem = this._mapper.Map<CartItem>(request);
        // todo: created by updated by user microservice

        await this._cartItemRepository.Add(cartItem);

        return this._mapper.Map<CartItemInformation>(cartItem);
    }

    public async Task<CartItemInformation> UpdateCartItem(CartItemUpdateRequest request)
    {
        var userCart = await this._cartRepository.Get(predicate: c => c.Id == request.CartId)
            ?? throw new ApiException(message: "Cart with this id doesn't exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        var cartItem = await _cartItemRepository.Get(item => item.Id == request.CartItemId) 
            ?? throw new ApiException(message: "Item not present in cart.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        cartItem.Quantity = request.Quantity;
        cartItem.Price = request.Price;
        cartItem.UpdatedDate = DateTime.UtcNow;
        // todo: updated by user microservice

        await _cartItemRepository.Update(cartItem);

        return this._mapper.Map<CartItemInformation>(cartItem); 
    }

    public async Task<CartResponse> GetCart(int userId)
    {
        var userCart = await this._cartRepository.Get
        (
            predicate: c => c.UserId == userId,
            includes: [ nameof(Cart.Items) ]
        );

        if (userCart == null)
        {
            return await this.CreateEmptyCart(userId);
        }

        var cartItems = this._mapper.Map<List<CartItemInformation>>(userCart.Items);
        
        return new CartResponse
        {
            Id = userCart.Id,
            CartItems = cartItems
        };
    }

    private async Task<CartResponse> CreateEmptyCart(int userId)
    {
        var userEmail = string.Empty; // todo: get from user microservice

        var userCart = new Cart
        {
            UserId = userId,
            CreatedBy = userEmail,
            UpdatedBy = userEmail,
        };

        await this._cartRepository.Add(userCart);

        return new CartResponse
        {
            Id = userCart.Id,
            CartItems = []
        };
    }
}