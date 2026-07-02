using System;
using System.Collections.Generic;
using System.Text;

namespace Liskov_Substitution_Principle
{
    internal class ProductoDeMuestra:ProductoSinLSP
    {
        public override void Vender(int cantidad)
        {
            throw new NotImplementedException(
                "Los productos de muestra no pueden venderse."
            );
        }
    }
}
