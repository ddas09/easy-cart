using EasyCart.Shared.DAL.Repositories;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.OrderService.DAL.Contracts;

namespace EasyCart.OrderService.DAL.Repositories;

public class OrderRepository : CrudBaseRepository<Order, OrderDBContext>, IOrderRepository
{
    public OrderRepository(OrderDBContext dbContext) : base(dbContext)
    {
    }
}