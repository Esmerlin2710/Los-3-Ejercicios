namespace app.services.servicios;

using app.models.clases;
using app.models.enums;
using app.services.validacion;
using app.global.global;
using app.main;
using System.Threading;
using app.factory.factory;
using System.Security.Cryptography;

public static class Servicio
{
   
    public static bool verify;
    public static Producto? producto;
    
    //Crear pedidos
    public static void CrearPedido()
    {
        string? nombre;
        string idTemp;

        if(!Global.productos.Any())
        {
            System.Console.WriteLine("\nAun no hay productos registrados");
            System.Console.WriteLine("Crea al menos un producto para crear el pedido");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        System.Console.Write("\nIngrese su nombre: ");
        nombre = Console.ReadLine() ?? "";
        verify = Validacion.VerificarTexto(nombre);

        if(!verify)
        {
            System.Console.WriteLine($"\nError: {Global.Error}");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }


        do
        {
            System.Console.Write("Ingresa el ID del producto a agregar: ");
            idTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(idTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                continue;
            }

            int.TryParse(idTemp, out int id);


            var productoSeleccionado = Global.productos.FirstOrDefault(p => p.ID == id);

            if(productoSeleccionado == default)
            {
                System.Console.WriteLine("\nID no encontrada\n");
                continue;
            }

            System.Console.Write("Que cantidad va a agregar?: ");
            var cantTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(cantTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                continue;
            }

            int.TryParse(cantTemp, out int cant);

            if(productoSeleccionado.Stock < cant)
            {
                System.Console.WriteLine("\nError: Ingrese una cantidad igual o menor al stock\n");
                continue;
            }

            productoSeleccionado.Stock -= cant;

            try
            {
                var detalle = DetallesFactory.Crear(productoSeleccionado, cant);
                var pedido = PedidoFatory.Crear(nombre, new List<DetallePedido> { detalle });
                Global.pedidos.Add(pedido);
                System.Console.WriteLine("\nPedido creado exitosamente");
                Thread.Sleep(1000);
                Main.Menu();
            } catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex}");
                Thread.Sleep(1000);
                Main.Menu();
            }

            if(verify) 
                break;

        } while(true);

    }
    //Crear Productos
    public static void CrearProducto()
    {
        string? nombre;
        string cantidadTemp;
        string precioTemp;
        decimal precio = 0;
        int cantidad = 0;

        do
        {
            Console.Write("\nProducto: ");
            nombre = Console.ReadLine() ?? "";
            verify = Validacion.VerificarTexto(nombre);

            if (!verify)
                Console.WriteLine($"\nError: {Global.Error}");

        } while(!verify);
        
        do
        {
            Console.Write("Precio: ");
            precioTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarDecimal(precioTemp);

            if (verify)
            {
                decimal.TryParse(precioTemp, out precio);
                break;
            }
            Console.WriteLine($"\nError: {Global.Error}\n");

        } while(!verify);

        do 
        {
            Console.Write("Cantidad: ");
            cantidadTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(cantidadTemp);

            if (verify)
            {
                int.TryParse(cantidadTemp, out cantidad);
                break;
            }
            Console.WriteLine($"\nError: {Global.Error}\n");

        } while (!verify);

        try
        {
            producto = ProductoFactory.Crear(nombre, precio, cantidad);
            Global.productos.Add(producto);
            System.Console.WriteLine("\nProducto Guardado con exito");
            System.Console.WriteLine("\n1.Crear otro producto");
            System.Console.WriteLine("2. Menu");
            System.Console.Write("\nElije: ");

            switch(Console.ReadLine())
            {
                case "1": CrearProducto(); break;
                case "2": Main.Menu(); break;
                default: System.Console.WriteLine("\nOpcion invalida"); Thread.Sleep(1000); Main.Menu(); break;
            }
        } catch (Exception ex)
        {
            System.Console.WriteLine($"\nError: {ex}");
            Thread.Sleep(1000);
            Main.Menu();
        }
  
    }
    //Agregar productos
    public static void AgregarProductos() 
    {
        int IdPedido = 0;
        int IdProducto = 0;

        do
        {
            if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido para poder agregar productos");
                Thread.Sleep(1000);
                Main.Menu();
            }

            if(!Global.productos.Any())
            {
                System.Console.WriteLine("\nAun no hay productos registrados");
                System.Console.WriteLine("Crea al menos un producto para agregar al pedido");
                Thread.Sleep(1000);
                Main.Menu();
            }

            System.Console.Write("\nIngrese la ID del pedido: ");
            var IdPedidoTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(IdPedidoTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}");
                continue;
            }

            int.TryParse(IdPedidoTemp, out IdPedido);

            verify = Global.pedidos.Exists(p => p.ID == IdPedido);

            if(!verify)
            {
                System.Console.WriteLine("\nPedido no encontrado");
                continue;
            }

            if(Global.pedidos.Where(p => p.ID == IdPedido).Any(g => g.Estado == Estado.Cancelado || g.Estado == Estado.Entregado || g.Estado == Estado.Enviado))
            {
                System.Console.WriteLine("\nNo puedes agregar productos a un pedido Entregado, Enviado o cancelado");
                Thread.Sleep(1000);
                Main.Menu();
                break;
            }

            if(verify) 
                break;

        } while(true);

        

        do
        {
            System.Console.Write("Ingrese la ID del producto a agregar: ");
            var IdProductoTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(IdProductoTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                continue;
            }

            int.TryParse(IdProductoTemp, out IdProducto);

            verify = Global.productos.Exists(p => p.ID == IdProducto);

            if(!verify)
            {
                System.Console.WriteLine("\nProducto no encontrado");
                continue;
            }

            if (verify)
                break;

        } while(true);

        do
        {
            var pedidoActual = Global.pedidos.FirstOrDefault(p => p.ID == IdPedido);
            var productoActual = Global.productos.FirstOrDefault(p => p.ID == IdProducto);


            System.Console.Write("Que cantidad va a agregar?: ");
            var cantTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(cantTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                continue;
            }

            int.TryParse(cantTemp, out int cant);

            if(productoActual.Stock < cant)
            {
                System.Console.WriteLine("\nError: Ingrese una cantidad igual o menor al stock\n");
                continue;
            }
            
            foreach (var item in Global.pedidos)
            {
                if(item.DetallePedido.Exists(p => p._Producto.ID == IdProducto))
                {
                    var productoExistente = item.DetallePedido.FirstOrDefault(p => p._Producto.ID == IdProducto);
                    productoExistente.Cantidad += cant;
                    productoExistente._Producto.Stock -= cant;

                    System.Console.WriteLine("\nProducto existente en el pedido");
                    System.Console.WriteLine("Agregando cantidad al pedido");
                    Thread.Sleep(2000);
                    System.Console.WriteLine("\nAgregado con exito");
                    System.Console.WriteLine("\nPresione una tecla para volver al menu...");
                    Console.ReadKey();
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                }
            }

            productoActual.Stock -= cant;

            try
            {
                var detalle = DetallesFactory.Crear(productoActual, cant);
                pedidoActual.DetallePedido.Add(detalle);
                System.Console.WriteLine("\nPedido agregado exitosamente");
                Thread.Sleep(1000);
                Main.Menu();
            } catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex}");
                Thread.Sleep(1000);
                Main.Menu();
            }

            if(verify)
                break;

        } while(true);

    }
    //Mostrar los pedidos
    public static void MostrarTodosLosPedidos()
    {
        //Validacion de lista pedidos vacia
        if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido");
                Thread.Sleep(1000);
                Main.Menu();
            }
        
        foreach (var pedidos in Global.pedidos)
        {
            //Bucle inicial y general
            System.Console.WriteLine($"\n========== Pedido #{pedidos.ID} ==========\n");
            Thread.Sleep(100);
            System.Console.WriteLine($"Cliente: {pedidos.Cliente}");
            Thread.Sleep(100);
            System.Console.WriteLine($"Fecha de Creacion: {pedidos.FechaCreacion}");
            Thread.Sleep(100);
            System.Console.WriteLine($"Estado: {pedidos.Estado}");
            Thread.Sleep(100);
            System.Console.WriteLine($"\n----------Productos----------\n");

            //Bucle para los productos
            foreach (var detalle in pedidos.DetallePedido)
            {
                Thread.Sleep(100);
                System.Console.WriteLine($"Producto: {detalle._Producto.Nombre}");
                Thread.Sleep(100);
                System.Console.WriteLine($"Precio: {detalle._Producto.Precio}");
                Thread.Sleep(100);
                System.Console.WriteLine($"Cantidad: {detalle.Cantidad}\n");
                Thread.Sleep(100);
            }
            
        }

        System.Console.WriteLine("Presione una tecla para volver al menu...");
        Console.ReadKey();
        Thread.Sleep(1000);
        Main.Menu();
    }
    //Buscar pedidos diferentes campos
    public static void BuscarPedido()
    {
        string idTemp;
        string nombre;
        string estado;

        if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido");
                Thread.Sleep(1000);
                Main.Menu();
            }

        System.Console.WriteLine("\n1. Por ID");
        System.Console.WriteLine("2. Por Nombre");
        System.Console.WriteLine("3. Por Estado");
        System.Console.Write("\nELije: ");

        switch(Console.ReadLine())
        {
            case "1": BuscarPorID(); break;
            case "2": BuscarPorNombre(); break;
            case "3": BuscarPorEstado(); break;
            default: Console.WriteLine("\nOpcion invalida..."); Thread.Sleep(1000); Main.Menu(); break;
        }

        void BuscarPorID()
        {
            int bucle = 1;

            System.Console.WriteLine("\n------------Busqueda avanzada por ID------------\n");
            System.Console.Write("\nIngrese el ID: ");
            idTemp = Console.ReadLine() ?? "";
            verify = Validacion.VerificarInt(idTemp);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                Thread.Sleep(1000);
                Main.Menu();
            }

            int.TryParse(idTemp, out int Id);

            var pedidoElegido = Global.pedidos.FirstOrDefault(p => p.ID == Id);

            if (pedidoElegido == default)
            {
                System.Console.WriteLine("ID no encontrada");
                Thread.Sleep(1000);
                Main.Menu();
            }

            System.Console.WriteLine("\n---------------Pedido Encontrado---------------\n");
            System.Console.WriteLine($"Cliente: {pedidoElegido.Cliente}");
            System.Console.WriteLine($"Fecha de Creacion: {pedidoElegido.FechaCreacion}");
            System.Console.WriteLine($"Estado: {pedidoElegido.Estado}");
            System.Console.WriteLine("\n----------------Productos----------------\n");

            foreach (var i in pedidoElegido.DetallePedido)
            {
                System.Console.WriteLine($"Producto #{bucle}");
                System.Console.WriteLine($"Nombre: {i._Producto.Nombre}");
                System.Console.WriteLine($"Precio: {i._Producto.Precio}");
                System.Console.WriteLine($"Cantidad: {i.Cantidad}\n");
                bucle++;
            }

            System.Console.WriteLine("Presione una tecla para volver al menu...");
            Console.ReadKey();
            Thread.Sleep(1000);
            Main.Menu();

        }

        void BuscarPorNombre()
        {
            int pedidosEncontrados = 0;
            int contador = 1;

            System.Console.WriteLine("\n--------Busqueda avanzada por coincidencias--------\n");
            System.Console.Write("Ingrese el nombre: ");
            nombre = Console.ReadLine() ?? "";
            verify = Validacion.VerificarTexto(nombre);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                Thread.Sleep(1000);
                Main.Menu();
            }

            var colecciones = Global.pedidos.Where(p => p.Cliente.Contains(nombre));

            if(!colecciones.Any())
            {
                System.Console.WriteLine("\nNo se encontraron coincidencias");
                Thread.Sleep(1000);
                Main.Menu();
            }

            

            foreach (var grupos in colecciones)
            {
                int bucle = 1;

                System.Console.WriteLine($"\n---------------Informacion Pedido #{contador}---------------\n");
                System.Console.WriteLine($"Cliente: {grupos.Cliente}");
                System.Console.WriteLine($"Fecha de Creacion: {grupos.FechaCreacion}");
                System.Console.WriteLine($"Estado: {grupos.Estado}");
                System.Console.WriteLine("\n----------------Productos----------------\n");

                foreach (var i in grupos.DetallePedido)
                {
                    System.Console.WriteLine($"Producto #{bucle}");
                    System.Console.WriteLine($"Nombre: {i._Producto.Nombre}");
                    System.Console.WriteLine($"Precio: {i._Producto.Precio}");
                    System.Console.WriteLine($"Cantidad: {i.Cantidad}\n");
                    bucle++;
                }
                contador++;
                pedidosEncontrados++;
            }

            System.Console.WriteLine($"Pedidos encontrados: {pedidosEncontrados}");
            System.Console.WriteLine("\nPresione una tecla para volver al menu...");
            Console.ReadKey();
            Thread.Sleep(1000);
            Main.Menu();

        }

        void BuscarPorEstado()
        {
            int pedidosEncontrados = 0;
            int contador = 1;

            System.Console.WriteLine("\n--------Busqueda avanzada por estado--------\n");
            System.Console.Write("Ingrese el estado: ");
            estado = Console.ReadLine() ?? "";
            verify = Validacion.VerificarTexto(estado);

            if(!verify)
            {
                System.Console.WriteLine($"\nError: {Global.Error}\n");
                Thread.Sleep(1000);
                Main.Menu();
            }

            if(!Enum.TryParse(estado, out Estado result))
            {
                System.Console.WriteLine("\nError: Estado no valido\n");
                Thread.Sleep(1000);
                Main.Menu();
            }

            var seleccionados = Global.pedidos.Where(p => p.Estado == result);

            if(!seleccionados.Any())
            {
                System.Console.WriteLine("\nNo se encontraron coincidencias");
                Thread.Sleep(1000);
                Main.Menu();
            }


            foreach (var p in seleccionados)
            {
                int bucle = 1;

                System.Console.WriteLine($"\n---------------Informacion Pedido #{contador}---------------\n");
                System.Console.WriteLine($"Cliente: {p.Cliente}");
                System.Console.WriteLine($"Fecha de Creacion: {p.FechaCreacion}");
                System.Console.WriteLine($"Estado: {p.Estado}");
                System.Console.WriteLine("\n----------------Productos----------------\n");

                foreach (var i in p.DetallePedido)
                {
                    System.Console.WriteLine($"Producto #{bucle}");
                    System.Console.WriteLine($"Nombre: {i._Producto.Nombre}");
                    System.Console.WriteLine($"Precio: {i._Producto.Precio}");
                    System.Console.WriteLine($"Cantidad: {i.Cantidad}\n");
                    bucle++;
                }
                contador++;
                pedidosEncontrados++;
            }

            System.Console.WriteLine($"Pedidos encontrados: {pedidosEncontrados}");
            System.Console.WriteLine("\nPresione una tecla para volver al menu...");
            Console.ReadKey();
            Thread.Sleep(1000);
            Main.Menu();
        }
    }
    //Cambiar estado de un producto
    public static void CambiarEstado()
    {
        string id;

        if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido");
                Thread.Sleep(1000);
                Main.Menu();
                return;
            }

        System.Console.WriteLine("\n----------- Cambiar de estado automaticamente-----------\n");
        System.Console.Write("Ingrese el ID del pedido: ");
        id = Console.ReadLine() ?? "";
        verify = Validacion.VerificarInt(id);

        if(!verify)
        {
            System.Console.WriteLine($"\nError: {Global.Error}\n");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        int.TryParse(id, out int Id);

        if (!Global.pedidos.Any(p => p.ID == Id))
        {
            System.Console.WriteLine("ID no encontrada");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        var seleccion = Global.pedidos.FirstOrDefault(p => p.ID == Id);

        if(seleccion.Estado == Estado.Entregado)
        {
            System.Console.WriteLine("\nNo se puede modificar un pedido entregado\n");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        } 
        else if (seleccion.Estado == Estado.Cancelado)
        {
            System.Console.WriteLine("\nNo se puede modificar un pedido cancelado\n");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }
        else if (seleccion.Estado == Estado.Pendiente)
        {
            System.Console.WriteLine("\nSe cambiara el estado de 'Pendiente' a 'En preparacion'.");
            System.Console.WriteLine("Quieres hacer este cambio?\n");
            System.Console.WriteLine("1. Si");
            System.Console.WriteLine("2. No");
            System.Console.Write("\nElije: ");

            switch(Console.ReadLine() ?? "")
            {
                case "1":
                    seleccion.Estado = Estado.Preparacion;
                    System.Console.WriteLine("\nEstado cambiado con exito");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                case "2":
                    System.Console.WriteLine("\nVolviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                default:
                    System.Console.WriteLine("\nOpcion invalida");
                    System.Console.WriteLine("Volviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
            }
        }
        else if (seleccion.Estado == Estado.Preparacion)
        {
            System.Console.WriteLine("\nSe cambiara el estado de 'En Preparacion' a 'Enviado'.");
            System.Console.WriteLine("Quieres hacer este cambio?\n");
            System.Console.WriteLine("1. Si");
            System.Console.WriteLine("2. No");
            System.Console.Write("\nElije: ");

            switch(Console.ReadLine() ?? "")
            {
                case "1":
                    seleccion.Estado = Estado.Enviado;
                    System.Console.WriteLine("\nEstado cambiado con exito");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                case "2":
                    System.Console.WriteLine("\nVolviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                default:
                    System.Console.WriteLine("\nOpcion invalida");
                    System.Console.WriteLine("Volviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
            }
        }
        else if (seleccion.Estado == Estado.Enviado)
        {
            System.Console.WriteLine("\nSe cambiara el estado de 'Enviado' a 'Entregado'.");
            System.Console.WriteLine("Quieres hacer este cambio?\n");
            System.Console.WriteLine("1. Si");
            System.Console.WriteLine("2. No");
            System.Console.Write("\nElije: ");

            switch(Console.ReadLine() ?? "")
            {
                case "1":
                    seleccion.Estado = Estado.Entregado;
                    System.Console.WriteLine("\nEstado cambiado con exito");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                case "2":
                    System.Console.WriteLine("\nVolviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                default:
                    System.Console.WriteLine("\nOpcion invalida");
                    System.Console.WriteLine("Volviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
            }
        }

    }
    //Cancelar pedido
    public static void CancelarPedido()
    {
        string pedido;

        if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido");
                Thread.Sleep(1000);
                Main.Menu();
                return;
            }

        System.Console.Write("\nIngrese el ID del pedido: ");
        pedido = Console.ReadLine() ?? "";
        verify = Validacion.VerificarInt(pedido);

        if(!verify)
        {
            System.Console.WriteLine($"\nError: {Global.Error}\n");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        int.TryParse(pedido, out int Id);

        if (!Global.pedidos.Any(p => p.ID == Id))
        {
            System.Console.WriteLine("ID no encontrada");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        var seleccionado = Global.pedidos.FirstOrDefault(p => p.ID == Id);

        if(seleccionado.Estado == Estado.Pendiente)
        {
            System.Console.WriteLine("\nEstas seguro de cancelar este pedido en pendiente?\n");
        }
        else if(seleccionado.Estado == Estado.Preparacion)
        {
            System.Console.WriteLine("\nEstas seguro de cancelar este pedido en preparacion?\n");
        }

        if(seleccionado.Estado != Estado.Pendiente && seleccionado.Estado != Estado.Preparacion)
        {
            System.Console.WriteLine("\nSolo se pueden cancelar pedidos 'pendientes' o en 'preparacion'");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }
        
        System.Console.WriteLine("1. Si");
        System.Console.WriteLine("2. No");
        System.Console.Write("\nELije: ");

        switch(Console.ReadLine() ?? "")
            {
                case "1":
                    seleccionado.Estado = Estado.Cancelado;
                    System.Console.WriteLine("\nEstado cambiado con exito");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                case "2":
                    System.Console.WriteLine("\nVolviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
                default:
                    System.Console.WriteLine("\nOpcion invalida");
                    System.Console.WriteLine("Volviendo al menu...");
                    Thread.Sleep(1000);
                    Main.Menu();
                    break;
            }
    }
    //Total de los pedidos
    public static void TotalPedido()
    {
        string pedido;
        decimal total = 0;
        decimal subTotalProducto = 0;
        decimal subTotal = 0;
        decimal descuento;
        int contador = 1;

        if(!Global.pedidos.Any())
            {
                System.Console.WriteLine("\nAun no hay pedidos registrados");
                System.Console.WriteLine("Crea al menos un pedido");
                Thread.Sleep(1000);
                Main.Menu();
                return;
            }

        System.Console.Write("\nIngrese el ID del pedido: ");
        pedido = Console.ReadLine() ?? "";
        verify = Validacion.VerificarInt(pedido);

        if(!verify)
        {
            System.Console.WriteLine($"\nError: {Global.Error}\n");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        int.TryParse(pedido, out int Id);

        if (!Global.pedidos.Any(p => p.ID == Id))
        {
            System.Console.WriteLine("}nID no encontrada");
            Thread.Sleep(1000);
            Main.Menu();
            return;
        }

        var seleccionado = Global.pedidos.FirstOrDefault(p => p.ID == Id);

        System.Console.WriteLine($"\n------------- Pedido de {seleccionado.Cliente} -------------");

        foreach (var i in seleccionado.DetallePedido)
        {
            subTotalProducto = i.Cantidad * i._Producto.Precio;
            System.Console.WriteLine($"\n-------Producto #{contador}-------\n");
            System.Console.WriteLine($"Producto: {i._Producto.Nombre}");
            System.Console.WriteLine($"Cantidad {i.Cantidad}");
            System.Console.WriteLine($"Precio: {i._Producto.Precio}");
            System.Console.WriteLine($"Subtotal Producto: {subTotalProducto}");
            subTotal += subTotalProducto;
            contador++;
        }

        if(subTotal >= 10000)
        {
            descuento = 5;
            total = subTotal - (subTotal * (descuento / 100));
        }
        else
        {
            descuento = 0;
            total = subTotal;
        }

        System.Console.WriteLine("\n-------Total del pedido-------\n");
        System.Console.WriteLine($"Subtotal: {subTotal}");
        System.Console.WriteLine($"Descuento: {descuento}%");
        System.Console.WriteLine($"Total: {total}");
        System.Console.WriteLine("\n--------------------------------\n");

        System.Console.WriteLine("Presione una tecla para volver al menu...");
        Console.ReadKey();
        Thread.Sleep(1000);
        Main.Menu();
    }
    //Indicadores
    public static void Indicadores()
    {
        int cantidadPedidos = Global.pedidos.Count();
        var estadoPreparacion = Global.pedidos.Count(p => p.Estado == Estado.Preparacion);
        var estadoPendiente = Global.pedidos.Count(p => p.Estado == Estado.Pendiente);
        var estadoEnviado = Global.pedidos.Count(p => p.Estado == Estado.Enviado);
        var estadoEntregado = Global.pedidos.Count(p => p.Estado == Estado.Entregado);
        var estadoCancelado = Global.pedidos.Count(p => p.Estado == Estado.Cancelado);
        decimal montoTotal = Global.pedidos.Where(i => i.Estado == Estado.Entregado).Sum(p => p.DetallePedido.Sum(d => d.Cantidad * d._Producto.Precio));
        var pedidoMayor = Global.pedidos.MaxBy(p => p.DetallePedido.Sum(d => d.Cantidad * d._Producto.Precio));
        var monto = pedidoMayor.DetallePedido.Sum(p => p.Cantidad * p._Producto.Precio);
        var mayorCantidadPedidos = Global.pedidos.GroupBy(p => p.Cliente).OrderByDescending(g => g.Count()).First();
        var productoMasSolicitado = Global.pedidos.SelectMany(d => d.DetallePedido).GroupBy(p => p._Producto).OrderByDescending(g => g.Count()).First();

        System.Console.WriteLine("\n------------------- Indicadores -------------------\n");
        System.Console.WriteLine($"Total de pedidos registrados: {cantidadPedidos}\n");
        System.Console.WriteLine($"Cantidad de pedidos por estado (Pendiente): {estadoPendiente}");
        System.Console.WriteLine($"Preparacion: {estadoPreparacion}");
        System.Console.WriteLine($"Enviado: {estadoEnviado}");
        System.Console.WriteLine($"Entregado: {estadoEntregado}");
        System.Console.WriteLine($"Cancelado: {estadoCancelado}\n");
        System.Console.WriteLine($"Monto total de pedidos entregados: {montoTotal}\n");
        System.Console.WriteLine($"Pedido con monto mas alto: {pedidoMayor.Cliente}");
        System.Console.WriteLine($"Monto: {monto}\n");
        System.Console.WriteLine($"Cliente con mas pedidos: {mayorCantidadPedidos.Key}");
        System.Console.WriteLine($"Producto mas solicitado: {productoMasSolicitado.Key.Nombre}");

        System.Console.WriteLine("\nPresione una tecla para volver al menu...");
        Console.ReadKey();
        Thread.Sleep(1000);
        Main.Menu();
    }
}