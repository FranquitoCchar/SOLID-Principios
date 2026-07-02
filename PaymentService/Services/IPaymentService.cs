using PaymentService.Entities;

namespace PaymentService.Services;

public interface IPaymentService
{
    Task CreatePaymentAsync(Payment payment);

    Task<List<Payment>> GetAllPaymentsAsync();
}