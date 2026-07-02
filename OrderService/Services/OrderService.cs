using OrderService.Entities;
using OrderService.Repository;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateOrderAsync(Order order)
    {
        if (string.IsNullOrWhiteSpace(order.CustomerName))
            throw new Exception("El nombre del cliente es obligatorio");

        if (order.Total <= 0)
            throw new Exception("El total debe ser mayor a cero");

        await _repository.CreateAsync(order);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _repository.GetAllAsync();
    }
}