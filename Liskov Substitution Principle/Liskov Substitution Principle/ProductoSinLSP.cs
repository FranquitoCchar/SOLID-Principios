using System;
using System.Collections.Generic;
using System.Text;

namespace Liskov_Substitution_Principle
{
    public class ProductoSinLSP
    {
        public string Nombre { get; set; } = string.Empty;
        public int Stock { get; set; }

        public virtual void Vender(int cantidad)
        {
            Stock -= cantidad;

            Console.WriteLine($"Se vendieron {cantidad} unidades.");
        }
    }
}
