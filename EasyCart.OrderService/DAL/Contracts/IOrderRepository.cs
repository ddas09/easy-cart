using EasyCart.Shared.DAL.Contracts;
using EasyCart.OrderService.DAL.Entities;

namespace EasyCart.OrderService.DAL.Contracts;

public interface IOrderRepository : ICrudBaseRepository<Order>
{
}