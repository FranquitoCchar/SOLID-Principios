// ============================================================
// PRINCIPIO: Single Responsibility Principle (SRP)
// TEMÁTICA: Sistema de Pedidos
// AUTOR: Franco
// ============================================================
// QUÉ DICE EL PRINCIPIO:
// Cada clase debe tener UNA sola razón para cambiar.
// Es decir, cada clase debe encargarse de una sola cosa.
//
// PROBLEMA QUE RESUELVE:
// Sin SRP, una sola clase gestiona los datos del pedido,
// calcula el total Y envía el correo de confirmación Y
// guarda en base de datos. Si cambia la lógica del correo,
// hay que tocar la misma clase que maneja los datos del pedido,
// lo cual es riesgoso y genera código difícil de mantener.
//
// SOLUCIÓN:
// Separar en clases con una sola responsabilidad cada una.
// Así, si algo cambia, solo se toca la clase correspondiente
// sin afectar a las demás.
// ============================================================

// -------------------------------------------------------
// VERSIÓN CORRECTA (Con SRP)
// -------------------------------------------------------
// Cada clase tiene una única responsabilidad.
// Pedido solo guarda datos, CalculadorPedido solo calcula,
// NotificadorPedido solo envía la confirmación.
// -------------------------------------------------------
Console.WriteLine("--- VERSIÓN CORRECTA (Con SRP) ---");

// Se crea el pedido con los datos del cliente
var pedido = new Pedido { Id = 1, Cliente = "Juan Pérez" };

// Se agregan los productos al pedido
pedido.AgregarItem("Pizza", 8500);
pedido.AgregarItem("Refresco", 1200);
pedido.AgregarItem("Postre", 2000);

// CalculadorPedido tiene UNA sola responsabilidad: calcular el total.
// No sabe nada del correo ni de cómo se guardan los datos.
var calculador = new CalculadorPedido();
double total = calculador.CalcularTotal(pedido);

Console.WriteLine($"Pedido #{pedido.Id} - Cliente: {pedido.Cliente}");
Console.WriteLine($"Total: ${total:F2}");
Console.WriteLine();

// NotificadorPedido tiene UNA sola responsabilidad: enviar la confirmación.
// No sabe cómo se calcula el total ni cómo se guardan los datos.
var notificador = new NotificadorPedido();
notificador.EnviarConfirmacion(pedido, total);

// -------------------------------------------------------
// VERSIÓN MAL APLICADA (Sin SRP)
// -------------------------------------------------------
// Una sola clase hace TODO: gestiona datos, calcula el total,
// envía correos y guarda en base de datos.
// Si cambia cualquiera de esas cosas, hay que tocar esta clase.
// Tiene múltiples razones para cambiar = viola SRP.
// -------------------------------------------------------
Console.WriteLine("\n--- VERSIÓN MAL APLICADA (Sin SRP) ---");
var pedidoMal = new PedidoSinSRP { Id = 2, Cliente = "María López" };
pedidoMal.AgregarItem("Hamburguesa", 6000);
pedidoMal.AgregarItem("Papas", 2000);

// Esta clase llama a su propio método de correo internamente,
// lo que mezcla responsabilidades que no deberían estar juntas.
pedidoMal.EnviarConfirmacion();
pedidoMal.GuardarEnBaseDeDatos();

// ============================================================
// DEFINICIÓN DE CLASES - VERSIÓN CORRECTA
// ============================================================

// Clase 1: Solo representa un pedido y gestiona sus datos.
// No sabe calcular ni enviar nada. Una sola responsabilidad.
class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public List<(string Producto, double Precio)> Items { get; set; } = new();

    // Solo agrega items a la lista. Nada más.
    public void AgregarItem(string producto, double precio)
    {
        Items.Add((producto, precio));
    }
}

// Clase 2: Solo calcula el total del pedido.
// No sabe nada del correo ni de cómo se guardan los datos.
// Si cambia la lógica de cálculo (ej: agregar impuestos),
// solo se toca esta clase.
class CalculadorPedido
{
    public double CalcularTotal(Pedido pedido)
    {
        double total = 0;
        foreach (var item in pedido.Items)
            total += item.Precio; // Suma el precio de cada item
        return total;
    }
}

// Clase 3: Solo envía la notificación de confirmación.
// No sabe cómo se calculó el total ni cómo se guardan los datos.
// Si mañana se cambia el correo por WhatsApp, solo se toca aquí.
class NotificadorPedido
{
    public void EnviarConfirmacion(Pedido pedido, double total)
    {
        Console.WriteLine($"Correo enviado a: {pedido.Cliente}");
        Console.WriteLine($"Pedido #{pedido.Id} confirmado por ${total:F2}");
    }
}

// ============================================================
// CLASE MAL APLICADA - VIOLA EL SRP
// ============================================================
// Esta clase tiene CUATRO responsabilidades distintas:
// 1. Gestionar los datos del pedido
// 2. Calcular el total
// 3. Enviar el correo de confirmación
// 4. Guardar en la base de datos
//
// Tiene cuatro razones para cambiar = viola directamente SRP.
// Si el equipo de base de datos cambia el esquema, hay que
// tocar la misma clase que maneja los correos. Eso es el problema.
// ============================================================
class PedidoSinSRP
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public List<(string Producto, double Precio)> Items { get; set; } = new();

    public void AgregarItem(string producto, double precio)
    {
        Items.Add((producto, precio));
    }

    // PROBLEMA 1: Esta clase no debería calcular el total.
    // Eso le corresponde a otra clase especializada.
    public double CalcularTotal()
    {
        double total = 0;
        foreach (var item in Items)
            total += item.Precio;
        return total;
    }

    // PROBLEMA 2: Esta clase no debería enviar correos.
    // La lógica de notificación no tiene nada que ver con los datos del pedido.
    public void EnviarConfirmacion()
    {
        double total = CalcularTotal();
        Console.WriteLine($"Correo enviado a: {Cliente}");
        Console.WriteLine($"Pedido #{Id} confirmado por ${total:F2}");
    }

    // PROBLEMA 3: Esta clase no debería guardar en base de datos.
    // La persistencia de datos es una responsabilidad completamente distinta.
    public void GuardarEnBaseDeDatos()
    {
        Console.WriteLine($"Guardando pedido #{Id} en la base de datos...");
    }
}