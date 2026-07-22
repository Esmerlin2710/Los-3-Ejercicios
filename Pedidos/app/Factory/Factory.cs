using app.global.global;
using app.models.clases;
using app.models.enums;

namespace app.factory.factory;

public static class ProductoFactory
{
    private static int _nextId = Global.productos.Any() ? Global.productos.Max(p => p.ID) + 1 : 1;
    public static Producto Crear(
            string nombre,
            decimal precio,
            int stock
    )
    {
        return new Producto
        {
            ID = _nextId++,
            Nombre = nombre,
            Precio = precio,
            Stock = stock
        };
    }
    
}

public static class PedidoFatory
{
    private static int _nextId = Global.pedidos.Any() ? Global.pedidos.Max(p => p.ID) + 1 : 1;
    public static Pedido Crear(
        string cliente,
        List<DetallePedido> listaProductos
    )
    {
        return new Pedido
        {
            ID = _nextId++,
            Cliente = cliente,
            Estado = Estado.Pendiente,
            DetallePedido = listaProductos
        };
    }
}

public static class DetallesFactory
{
    public static DetallePedido Crear(
        Producto producto,
        int cantidad
    )
    {
        return new DetallePedido
        {
            _Producto = producto,
            Cantidad = cantidad
        };
    }
}