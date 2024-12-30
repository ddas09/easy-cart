using AutoMapper;
using EasyCart.OrderService.DAL.Contracts;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.OrderService.Models.Request;
using EasyCart.OrderService.Models.Response;
using EasyCart.OrderService.Services.Contracts;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;

namespace EasyCart.OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;

    private readonly IOrderRepository _orderRepository;

    public OrderService(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse> CreateOrder(OrderCreateRequest request)
    {
        var order = _mapper.Map<Order>(request);

        // Calculate total amount
        order.TotalAmount = request.Items.Sum(item => item.Price * item.Quantity);

        // Add current date
        order.CreatedDate = DateTime.UtcNow;
        // order.CreatedBy = currentUser;

        await _orderRepository.Add(order);

        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<OrderResponse> GetOrder(int orderId)
    {
        var order = await this._orderRepository.Get(predicate: o => o.Id == orderId, includes: [ nameof(Order.Items) ])
            ?? throw new ApiException("Order with this id not found", AppConstants.ErrorCodeEnum.NotFound);

        return this._mapper.Map<OrderResponse>(order);
    }

    public async Task<List<OrderResponse>> GetOrdersByUserId(int userId)
    {
        var orders = await _orderRepository.GetList(predicate: o => o.UserId == userId, includes: [ nameof(Order.Items) ]);
        return _mapper.Map<List<OrderResponse>>(orders);
    }

    public async Task UpdateOrderStatus(int orderId, string status)
    {
        var order = await _orderRepository.Get(predicate: o => o.Id == orderId)
            ?? throw new ApiException("Order with this id not found", AppConstants.ErrorCodeEnum.NotFound);

        order.Status = status;
        order.UpdatedDate = DateTime.UtcNow;

        await _orderRepository.Update(order);
    }
}
