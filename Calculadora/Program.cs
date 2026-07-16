using System.ComponentModel.Design;
using Calculadora.models;
using System.Threading;


//Variables
bool verify;
string producto;
decimal precio;
decimal num2;
decimal cantidad;
decimal descuento;
decimal impuesto;
var productoList = new List<Producto>();
int d = 1;
decimal totalCompra = 1;


System.Console.WriteLine("\n------------Bienvenida/o a la Calculadora de Compras-----------");

Menu();

void Menu() 
{
    
    //Producto
    do
    {
        System.Console.Write("\nIngrese el nombre del producto: ");
        producto = Console.ReadLine() ?? "";
        verify = VerificarTexto(producto);
    } while (!verify);


    //Precio
    do
    {
        System.Console.Write("\nIngrese el precio: ");
        verify = VerificarNum(Console.ReadLine() ?? "");
        precio = num2;
    } while (!verify);


    //Cantidad
    do
    {
        System.Console.Write("\nIngrese la cantidad comprada: ");
        verify = VerificarNum(Console.ReadLine() ?? "");
        cantidad = num2;
    } while (!verify);


    //Descuento
    do
    {
        System.Console.Write("\nIngrese el descuento (solo el numero): ");
        verify = VerificarNum(Console.ReadLine() ?? "");
        descuento = num2;

        if (num2 > 100)
        {
            System.Console.WriteLine("\nError: El descuento no puede superar el 100%");
            verify = false;
        } 
    } while (!verify);
        

    //Impuesto
    do
    {
        System.Console.Write("\nIngrese el impuesto del producto: ");
        verify = VerificarNum(Console.ReadLine() ?? "");
        impuesto = num2;

        if (num2 > 100)
        {
            System.Console.WriteLine("\nError: El impuesto no puede superar el 100%");
            verify = false;
        }
    } while (!verify);





    //Crear objeto
    try
    {
        var productoTemp = ProductosFactory.Crear(producto, precio, cantidad, descuento, impuesto);
        productoList.Add(productoTemp);
        System.Console.WriteLine("Producto creado exitosamente");
        System.Console.WriteLine("\nQuieres agregar otro producto?: \n");
        System.Console.WriteLine("1. Si");
        System.Console.WriteLine("2. No");
        System.Console.Write("\nElije: ");
        var opcion = Console.ReadLine() ?? "";

        if (opcion.ToLower() == "1")
        {
            Menu();
        }
        else if (opcion.ToLower() == "2")
        {
            Resultado();
        }
        else
        {
            System.Console.WriteLine("\nOpcion invalida...");
            System.Console.WriteLine("Mostrando resultados...");
            Thread.Sleep(1000);
            Resultado();
        }
    } 
    catch (ArgumentException ex)
    {
        System.Console.WriteLine($"\nError: {ex.Message}\n");
        Thread.Sleep(1000);
        System.Console.WriteLine("Volviendo al menu en");
        Thread.Sleep(1000);
        System.Console.WriteLine("3");
        Thread.Sleep(1000);
        System.Console.WriteLine("2");
        Thread.Sleep(1000);
        System.Console.WriteLine("1");
        Thread.Sleep(1000);
    }
}



    
    


//Funciones

 
//Verificar Texto
bool VerificarTexto(string text)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        System.Console.WriteLine("\nError: El valor no puede ser nulo");
        return false;
    }
    else if (decimal.TryParse(text, out num2))
    {
        System.Console.WriteLine("\nError: Ingresa un texto.");
        return false;
    }
    producto = text;
    return true;
}

//Verificar Numeros
bool VerificarNum(string num)
{

    if (!decimal.TryParse(num, out num2))
    {
        System.Console.WriteLine("\nError: Digite un numero");
        return false;
    } 
    else if (num2 <= 0)
    {
        System.Console.WriteLine("\nError: El numero no puede ser 0 o negativo");
        return false;
    }

    return true;
}

void Resultado()
{
    System.Console.WriteLine("\n=========RESULTADO========\n");
        foreach (var i in productoList)
        {
            System.Console.WriteLine($"Producto #{d}\n");
            Thread.Sleep(1000);
            System.Console.WriteLine($"Producto: {i.Nombre}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Cantidad: {i.Cantidad:F0}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Precio: {i.PrecioUnitario:F2}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Subtotal: {i.SubTotal:F2}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Descuentos: {i.DescuentoTotal:F2}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Impuestos: {i.ImpuestoTotal:F2}");
            Thread.Sleep(500);
            System.Console.WriteLine($"Total: {i.Total:F2}\n");
            Thread.Sleep(1000);

            totalCompra += i.Total;
            d++;


        }
    System.Console.WriteLine($"El total de la compra fue: {totalCompra:F2}");
}