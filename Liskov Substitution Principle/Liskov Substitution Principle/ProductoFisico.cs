using System;
using System.Collections.Generic;
using System.Text;

namespace Liskov_Substitution_Principle
{
    public class ProductoFisico :Producto, Ivendible
    {
            public void Vender(int cantidad)
    {
        if (cantidad > Stock)
        {
            Console.WriteLine("No hay suficiente inventario.");
            return;
        }

        Stock -= cantidad;

        Console.WriteLine($"Producto: {Nombre}");
        Console.WriteLine($"Se vendieron {cantidad} unidades.");
        Console.WriteLine($"Stock restante: {Stock}");
    }
    }
}
