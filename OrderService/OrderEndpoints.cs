using OrderService.Entities;
using OrderService.Services;


namespace OrderService;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/orders",
        async (IOrderService service) =>
        {
            var orders = await service.GetAllOrdersAsync();

            return Results.Ok(orders);
        });

        app.MapPost("/orders",
        async (Order order, IOrderService service) =>
        {
            try
            {
                await service.CreateOrderAsync(order);

                return Results.Ok(new
                {
                    Message = "Pedido creado correctamente"
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}