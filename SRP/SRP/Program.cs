// ============================================================
// PRINCIPIO: Single Responsibility Principle (SRP)
// TEMÁTICA: Sistema de Pedidos
// AUTOR: Franco
// ============================================================

// --- VERSIÓN CORRECTA (Con SRP) ---
Console.WriteLine("--- VERSIÓN CORRECTA (Con SRP) ---");

var pedido = new Pedido { Id = 1, Cliente = "Juan Pérez" };
pedido.AgregarItem("Pizza", 8500);
pedido.AgregarItem("Refresco", 1200);
pedido.AgregarItem("Postre", 2000);

var calculador = new CalculadorPedido();
double total = calculador.CalcularTotal(pedido);

Console.WriteLine($"Pedido #{pedido.Id} - Cliente: {pedido.Cliente}");
Console.WriteLine($"Total: ${total:F2}");
Console.WriteLine();

var notificador = new NotificadorPedido();
notificador.EnviarConfirmacion(pedido, total);

Console.WriteLine("\n--- VERSIÓN MAL APLICADA (Sin SRP) ---");
var pedidoMal = new PedidoSinSRP { Id = 2, Cliente = "María López" };
pedidoMal.AgregarItem("Hamburguesa", 6000);
pedidoMal.AgregarItem("Papas", 2000);
pedidoMal.EnviarConfirmacion();
pedidoMal.GuardarEnBaseDeDatos();

class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public List<(string Producto, double Precio)> Items { get; set; } = new();

    public void AgregarItem(string producto, double precio)
    {
        Items.Add((producto, precio));
    }
}

class CalculadorPedido
{
    public double CalcularTotal(Pedido pedido)
    {
        double total = 0;
        foreach (var item in pedido.Items)
            total += item.Precio;
        return total;
    }
}

class NotificadorPedido
{
    public void EnviarConfirmacion(Pedido pedido, double total)
    {
        Console.WriteLine($"Correo enviado a: {pedido.Cliente}");
        Console.WriteLine($"Pedido #{pedido.Id} confirmado por ${total:F2}");
    }
}

class PedidoSinSRP
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public List<(string Producto, double Precio)> Items { get; set; } = new();

    public void AgregarItem(string producto, double precio)
    {
        Items.Add((producto, precio));
    }

    // PROBLEMA 1: Esta clase no debería calcular el total
    public double CalcularTotal()
    {
        double total = 0;
        foreach (var item in Items)
            total += item.Precio;
        return total;
    }

    // PROBLEMA 2: Esta clase no debería enviar correos
    public void EnviarConfirmacion()
    {
        double total = CalcularTotal();
        Console.WriteLine($"Correo enviado a: {Cliente}");
        Console.WriteLine($"Pedido #{Id} confirmado por ${total:F2}");
    }

    // PROBLEMA 3: Esta clase no debería guardar en base de datos
    public void GuardarEnBaseDeDatos()
    {
        Console.WriteLine($"Guardando pedido #{Id} en la base de datos...");
    }
}