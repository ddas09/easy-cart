using EasyCart.Shared.DAL.Repositories;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.OrderService.DAL.Contracts;

namespace EasyCart.OrderService.DAL.Repositories;

public class OrderItemRepository : CrudBaseRepository<OrderItem, OrderDBContext>, IOrderItemRepository
{
    public OrderItemRepository(OrderDBContext dbContext) : base(dbContext)
    {
    }
}