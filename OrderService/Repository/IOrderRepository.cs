using OrderService.Entities;

namespace OrderService.Repository;

public interface IOrderRepository
{
    Task CreateAsync(Order order);

    Task<List<Order>> GetAllAsync();
}