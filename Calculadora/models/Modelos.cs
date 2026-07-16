namespace Calculadora.models;

public class Producto
{
    public string? Nombre {get; set;}
    public decimal PrecioUnitario {get; set;}
    public decimal Cantidad {get; set;}
    public decimal Descuento {get; set;}
    public decimal Impuesto {get; set;}
    


    public decimal SubTotal => PrecioUnitario * Cantidad;
    public decimal DescuentoTotal => SubTotal * (Descuento / 100);
    public decimal ImpuestoTotal => SubTotal * (Impuesto / 100);
    public decimal Total => SubTotal + Impuesto - Descuento;
}