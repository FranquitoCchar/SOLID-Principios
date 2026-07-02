// ============================================================
// PRINCIPIO: Open/Closed Principle (OCP)
// TEMÁTICA: Sistema de Pedidos
// AUTOR: Franco
// ============================================================
// QUÉ DICE EL PRINCIPIO:
// Las clases deben estar ABIERTAS para extenderse (agregar
// funcionalidad nueva) pero CERRADAS para modificarse (no
// tocar el código que ya funciona y fue probado).
//
// PROBLEMA QUE RESUELVE:
// Sin OCP, cada vez que el negocio pide un nuevo tipo de
// descuento, hay que entrar a modificar la clase existente.
// Esto es peligroso porque podés romper descuentos que ya
// funcionaban correctamente.
//
// SOLUCIÓN:
// Definir una interfaz IDescuento que todos los descuentos
// deben implementar. Cada nuevo descuento es una clase nueva
// que implementa esa interfaz. Nunca se toca el código viejo.
// ============================================================

// -------------------------------------------------------
// VERSIÓN CORRECTA (Con OCP)
// -------------------------------------------------------
// Creamos un pedido con un total base de 10,000
// A este total le vamos a aplicar diferentes descuentos
// -------------------------------------------------------
Console.WriteLine("--- VERSIÓN CORRECTA (Con OCP) ---");

var pedido = new Pedido { Id = 1, Cliente = "Juan Pérez", Total = 10000 };

// Lista de descuentos disponibles.
// Si mañana el negocio pide un nuevo descuento, solo se agrega
// una nueva clase aquí, sin tocar nada de lo que ya existe.
List<IDescuento> descuentos = new()
{
    new DescuentoVIP(),           // 20% de descuento para clientes VIP
    new DescuentoTemporada(),     // 15% de descuento por temporada
    new DescuentoNuevoCliente()   // 10% de descuento para clientes nuevos
};

// Aplicamos cada descuento al total del pedido
// Este foreach no cambia nunca, aunque agreguemos 10 descuentos más
foreach (var descuento in descuentos)
{
    double totalConDescuento = descuento.Aplicar(pedido.Total);
    Console.WriteLine($"{descuento.Nombre}: ${totalConDescuento:F2}");
}

// -------------------------------------------------------
// VERSIÓN MAL APLICADA (Sin OCP)
// -------------------------------------------------------
// Aquí una sola clase maneja todos los tipos de descuento
// con if/else. Cada nuevo descuento obliga a modificar
// esta clase, lo cual viola el principio OCP.
// -------------------------------------------------------
Console.WriteLine("\n--- VERSIÓN MAL APLICADA (Sin OCP) ---");
var calculadorMal = new CalculadorDescuentoSinOCP();
Console.WriteLine($"VIP: ${calculadorMal.Calcular(10000, "VIP"):F2}");
Console.WriteLine($"Temporada: ${calculadorMal.Calcular(10000, "Temporada"):F2}");
Console.WriteLine($"NuevoCliente: ${calculadorMal.Calcular(10000, "NuevoCliente"):F2}");

// ============================================================
// DEFINICIÓN DE CLASES - VERSIÓN CORRECTA
// ============================================================

// Clase que representa un pedido en el sistema
class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public double Total { get; set; } // Total base antes del descuento
}

// Interfaz que define el contrato que todo descuento debe cumplir.
// Cualquier descuento nuevo DEBE tener un Nombre y un método Aplicar.
// Esta interfaz NUNCA se modifica, solo se extiende con nuevas clases.
interface IDescuento
{
    string Nombre { get; }           // Nombre descriptivo del descuento
    double Aplicar(double total);    // Retorna el total con el descuento aplicado
}

// Descuento para clientes VIP: 20% de rebaja
// Clase CERRADA para modificación, ABIERTA para extensión
class DescuentoVIP : IDescuento
{
    public string Nombre => "Descuento VIP (20%)";

    public double Aplicar(double total)
    {
        // Se aplica un 20% de descuento al total
        return total - (total * 0.20);
    }
}

// Descuento por temporada: 15% de rebaja
// Nueva funcionalidad agregada SIN tocar las clases anteriores
class DescuentoTemporada : IDescuento
{
    public string Nombre => "Descuento Temporada (15%)";

    public double Aplicar(double total)
    {
        // Se aplica un 15% de descuento al total
        return total - (total * 0.15);
    }
}

// Descuento para clientes nuevos: 10% de rebaja
// Otra extensión sin modificar nada existente
class DescuentoNuevoCliente : IDescuento
{
    public string Nombre => "Descuento Nuevo Cliente (10%)";

    public double Aplicar(double total)
    {
        // Se aplica un 10% de descuento al total
        return total - (total * 0.10);
    }
}

// ============================================================
// CLASE MAL APLICADA - VIOLA EL OCP
// ============================================================
// Esta clase tiene un método con if/else para cada tipo de
// descuento. Si el negocio pide un nuevo descuento "Estudiante"
// hay que entrar aquí y agregar otro else if, arriesgando
// romper los descuentos que ya funcionaban.
// Esto viola OCP porque la clase no está CERRADA a modificaciones.
// ============================================================
class CalculadorDescuentoSinOCP
{
    public double Calcular(double total, string tipo)
    {
        if (tipo == "VIP")
            return total - (total * 0.20);
        else if (tipo == "Temporada")
            return total - (total * 0.15);
        else if (tipo == "NuevoCliente")
            return total - (total * 0.10);
        // PROBLEMA: si mañana agregan "Estudiante", "Empleado",
        // "Mayorista", etc., hay que modificar este método cada vez.
        // Eso es exactamente lo que OCP quiere evitar.
        return total;
    }
}