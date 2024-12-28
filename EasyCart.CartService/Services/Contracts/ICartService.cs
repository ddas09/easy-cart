using EasyCart.CartService.Models.Request;
using EasyCart.CartService.Models.Response;

namespace EasyCart.CartService.Services.Contracts;

public interface ICartService
{
    Task<CartResponse> GetCart(int userId);

    Task RemoveCartItem(CartItemRemoveRequest request);

    Task<CartItemInformation> AddCartItem(CartItemAddRequest request);

    Task<CartItemInformation> UpdateCartItem(CartItemUpdateRequest request);
}