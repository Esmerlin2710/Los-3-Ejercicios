using System.Text.Json;
using app.models.clases;

namespace app.serivice.json;

public static class Json
{
    public static void GuardarProductos(List<Producto>productos)
    {
        string json = JsonSerializer.Serialize(productos);
        File.WriteAllText("Productos.json", json);
    }

    public static List<Producto> CargarProductos() 
    {
        if(!File.Exists("Productos.json"))
            return new List<Producto>();
        
        string json = File.ReadAllText("Productos.json");
        return JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();
    }

    public static void GuardarPedidos(List<Pedido>pedidos)
    {
        string json = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("Pedidos.json", json);
    }

    public static List<Pedido> CargarPedidos() 
    {
        if(!File.Exists("Pedidos.json"))
            return new List<Pedido>();
        
        string json = File.ReadAllText("Pedidos.json");
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }
}