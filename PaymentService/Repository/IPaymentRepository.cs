using PaymentService.Entities;

namespace PaymentService.Repository;

public interface IPaymentRepository
{
    Task CreateAsync(Payment payment);

    Task<List<Payment>> GetAllAsync();
}