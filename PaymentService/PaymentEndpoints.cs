using PaymentService.Entities;
using PaymentService.Services;

namespace PaymentService;

public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this WebApplication app)
    {
        app.MapGet("/payments",
        async (IPaymentService service) =>
        {
            var payments = await service.GetAllPaymentsAsync();

            return Results.Ok(payments);
        });

        app.MapPost("/payments",
        async (Payment payment,
               IPaymentService service) =>
        {
            try
            {
                await service.CreatePaymentAsync(payment);

                return Results.Ok(new
                {
                    Message = "Pago registrado correctamente"
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}