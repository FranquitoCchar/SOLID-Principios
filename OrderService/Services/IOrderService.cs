using OrderService.Entities;

namespace OrderService.Services;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync();
}