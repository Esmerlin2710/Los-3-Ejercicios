namespace app.global.global;

using app.models.clases;

public static class Global
{
    public static decimal Num;
    public static string? Error = "";
    public static List<Producto> productos = new List<Producto>();
    public static List<Pedido> pedidos = new List<Pedido>();
}