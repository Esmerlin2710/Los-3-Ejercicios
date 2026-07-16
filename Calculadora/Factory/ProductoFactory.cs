using Calculadora.models;

public static class ProductosFactory
{
    

    public static Producto Crear(
        string nombre,
        decimal precio,
        decimal cantidad,
        decimal descuento,
        decimal impuesto)
    {
        return new Producto
        {
            Nombre = nombre,
            PrecioUnitario = precio,
            Cantidad = cantidad,
            Descuento = descuento,
            Impuesto = impuesto
        };
    }
}