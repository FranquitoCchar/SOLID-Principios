using Liskov_Substitution_Principle;

Console.WriteLine("--- VERSION CORRECTA Con LSP ---");

Ivendible producto1 = new ProductoFisico
{
    Nombre = "Laptop",
    Stock = 10
};

producto1.Vender(2);

Console.WriteLine();
//////////////////////////////////////////////////////////////////////

Console.WriteLine("--- VERSION MAL APLICADA Sin LSP ---");

ProductoSinLSP producto2 = new ProductoDeMuestra
{
    Nombre = "Televisor de Exhibición",
    Stock = 1
};

try
{
    producto2.Vender(1);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
