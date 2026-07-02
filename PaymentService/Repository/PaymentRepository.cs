using Microsoft.Data.SqlClient;
using PaymentService.Entities;

namespace PaymentService.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDbConnectionFactory _factory;

    public PaymentRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task CreateAsync(Payment payment)
    {
        using var connection = _factory.CreateConnection();

        await connection.OpenAsync();

        string sql = @"
            INSERT INTO Payments
            (
                OrderId,
                Amount,
                Status
            )
            VALUES
            (
                @OrderId,
                @Amount,
                @Status
            )";

        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@OrderId", payment.OrderId);
        command.Parameters.AddWithValue("@Amount", payment.Amount);
        command.Parameters.AddWithValue("@Status", payment.Status);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        var payments = new List<Payment>();

        using var connection = _factory.CreateConnection();

        await connection.OpenAsync();

        string sql = "SELECT * FROM Payments";

        using var command = new SqlCommand(sql, connection);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            payments.Add(new Payment
            {
                Id = Convert.ToInt32(reader["Id"]),
                OrderId = Convert.ToInt32(reader["OrderId"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                Status = reader["Status"].ToString()!
            });
        }

        return payments;
    }
}