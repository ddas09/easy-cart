using Microsoft.AspNetCore.Mvc;
using EasyCart.OrderService.Models.Request;
using EasyCart.OrderService.Services.Contracts;
using EasyCart.Shared.Models;
using EasyCart.Shared.ActionFilters;

namespace EasyCart.OrderService.Controllers;

/// <summary>
/// Controller responsible for managing user orders.
/// </summary>
[ApiController]
[ValidateModelState]
[Route("api/orders")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly CustomResponse _customResponse = new();
    private readonly IOrderService _orderService = orderService;

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="request">The order creation request.</param>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
    {
        var order = await _orderService.CreateOrder(request);
        return _customResponse.Success(message: "Order created successfully.", data: order);
    }

    /// <summary>
    /// Get an order by its ID.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder([FromRoute] int orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        return _customResponse.Success(data: order);
    }

    /// <summary>
    /// Get all orders for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    [HttpGet]
    public async Task<IActionResult> GetOrdersByUserId([FromQuery] int userId)
    {
        var orders = await _orderService.GetOrdersByUserId(userId);
        return _customResponse.Success("Orders fetched successfully.", orders);
    }

    /// <summary>
    /// Update the status of an order.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <param name="status">The new status.</param>
    [HttpPatch("{orderId}/status")]
    public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, [FromBody] string status)
    {
        await _orderService.UpdateOrderStatus(orderId, status);
        return _customResponse.Success("Order status updated successfully.");
    }
}
