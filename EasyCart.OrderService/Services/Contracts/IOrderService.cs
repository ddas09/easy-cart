using EasyCart.OrderService.Models.Request;
using EasyCart.OrderService.Models.Response;

namespace EasyCart.OrderService.Services.Contracts;

public interface IOrderService
{
    Task<OrderResponse> CreateOrder(OrderCreateRequest request);

    Task<OrderResponse> GetOrder(int orderId);

    Task<List<OrderResponse>> GetOrdersByUserId(int userId);

    Task UpdateOrderStatus(int orderId, string status);
}
