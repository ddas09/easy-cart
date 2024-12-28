using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.ActionFilters;
using EasyCart.CartService.Models.Request;
using EasyCart.CartService.Services.Contracts;

namespace EasyCart.CartService.Controllers
{
    /// <summary>
    /// Controller responsible for managing user cart related operations.
    /// </summary>
    [ApiController]
    [ValidateModelState]
    [Route("api/carts")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly CustomResponse _customResponse = new();
        private readonly ICartService _cartService = cartService;

        /// <summary>
        /// Get the cart for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The cart details for the user.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart([FromRoute] int userId)
        {
            var cart = await _cartService.GetCart(userId);
            return _customResponse.Success(data: cart);
        }

        /// <summary>
        /// Add an item to the cart.
        /// </summary>
        /// <param name="request">The request containing cart item details.</param>
        /// <returns>The added cart item details.</returns>
        [HttpPost("items")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemAddRequest request)
        {
            var cartItem = await _cartService.AddCartItem(request);
            return _customResponse.Success(message: "Item added to cart successfully.", data: cartItem);
        }

         /// <summary>
        /// Update an item in the cart.
        /// </summary>
        /// <param name="request">The request containing updated cart item details.</param>
        /// <returns>The updated cart item details.</returns>
        [HttpPut("items")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateRequest request)
        {
            var cartItem = await _cartService.UpdateCartItem(request);
            return _customResponse.Success(message: "Cart item updated successfully.", data: cartItem);
        }

        /// <summary>
        /// Remove an item from the cart.
        /// </summary>
        /// <param name="request">The request containing the cart item to remove.</param>
        [HttpDelete("items")]
        public async Task<IActionResult> RemoveCartItem([FromBody] CartItemRemoveRequest request)
        {
            await _cartService.RemoveCartItem(request);
            return _customResponse.Success(message: "Cart item removed successfully.");
        }
    }
}