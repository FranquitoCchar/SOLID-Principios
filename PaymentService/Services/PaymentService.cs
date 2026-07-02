using PaymentService.Entities;
using PaymentService.Repository;

namespace PaymentService.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task CreatePaymentAsync(Payment payment)
    {
        if (payment.Amount <= 0)
            throw new Exception("Monto inválido");

        await _repository.CreateAsync(payment);
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _repository.GetAllAsync();
    }
}