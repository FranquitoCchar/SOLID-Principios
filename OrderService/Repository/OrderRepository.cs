using Microsoft.Data.SqlClient;
using OrderService.Entities;

namespace OrderService.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _factory;

    public OrderRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task CreateAsync(Order order)
    {
        using var connection = _factory.CreateConnection();

        await connection.OpenAsync();

        string sql = @"
            INSERT INTO Orders
            (
                CustomerName,
                Total
            )
            VALUES
            (
                @CustomerName,
                @Total
            )";

        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@CustomerName",
            order.CustomerName);

        command.Parameters.AddWithValue(
            "@Total",
            order.Total);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Order>> GetAllAsync()
    {
        var orders = new List<Order>();

        using var connection = _factory.CreateConnection();

        await connection.OpenAsync();

        string sql = "SELECT * FROM Orders";

        using var command = new SqlCommand(sql, connection);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            orders.Add(new Order
            {
                Id = Convert.ToInt32(reader["Id"]),
                CustomerName = reader["CustomerName"].ToString()!,
                Total = Convert.ToDecimal(reader["Total"])
            });
        }

        return orders;
    }
}