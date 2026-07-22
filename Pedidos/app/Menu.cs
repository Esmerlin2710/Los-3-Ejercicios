namespace app.main;

using System.Threading;
using app.services.servicios;
using app.serivice.json;
using app.global.global;

public static class Main
{
    public static void Menu()
    {
        System.Console.WriteLine("\n----------------MENU----------------\n");
        System.Console.WriteLine("1. Crear Pedido");
        System.Console.WriteLine("2. Agregar productos a un pedido");
        System.Console.WriteLine("3. Mostrar todos los pedidos");
        System.Console.WriteLine("4. Buscar Pedido");
        System.Console.WriteLine("5. Cambiar estado de un pedido");
        System.Console.WriteLine("6. Cancelar pedido");
        System.Console.WriteLine("7. Mostrar total de un pedido");
        System.Console.WriteLine("8. Mostrar indicadores");
        System.Console.WriteLine("9. Crear Productos");
        System.Console.WriteLine("10. Guardar");
        System.Console.WriteLine("11. Salir");
        System.Console.Write("\nELija una opcion: ");

        switch (Console.ReadLine())
        {
            case "1": Servicio.CrearPedido(); break;
            case "2": Servicio.AgregarProductos(); break;
            case "3": Servicio.MostrarTodosLosPedidos(); break;
            case "4": Servicio.BuscarPedido(); break;
            case "5": Servicio.CambiarEstado(); break;
            case "6": break;
            case "7": break;
            case "8": break;
            case "9": Servicio.CrearProducto(); break;
            case "10": 
                Thread.Sleep(1000); 
                Json.GuardarProductos(Global.productos); 
                Json.GuardarPedidos(Global.pedidos); 
                System.Console.WriteLine("\nGuardado con exito");
                Thread.Sleep(1000); 
                Main.Menu();
                break;
            case "11": 
                System.Console.WriteLine("\nSaliendo del programa...\n"); 
                Thread.Sleep(1000); 
                Json.GuardarProductos(Global.productos); 
                Json.GuardarPedidos(Global.pedidos);
                Environment.Exit(0); 
                break;
            default: System.Console.WriteLine("\nOpcion invalida"); Menu(); break;
        }
    }
}

