using cali.app.factory.factory;
using cali.app.models.clases;
using System.ComponentModel.Design;
using System.Threading;

int opcion;
int numTemp;
string textTemp = "";
bool verify;
int codigo;
string nombre;
int cali1;
int cali2;
int cali3;


var lista_Estudiantes = new List<Estudiante>();



System.Console.WriteLine("\n===============SISTEMA DE CALIFICACIONES==================\n");

Menu();

//Menu
void Menu()
{
    
    System.Console.WriteLine("\n--------MENU-------\n");
    System.Console.WriteLine("1. Registrar estudiante");
    System.Console.WriteLine("2. Consultar estudiante");
    System.Console.WriteLine("3. Mostrar todos los estudiantes");
    System.Console.WriteLine("4. Mostrar resumen del curso");
    System.Console.WriteLine("5. Salir\n");
    System.Console.Write("Elija una opcion: ");

    if(!int.TryParse(Console.ReadLine(), out opcion))
    {
        System.Console.WriteLine("Error, ingrese un numero");
        return;
    }

    switch (opcion)
    {
        case 1: Registrar(); break;
        case 2: Consultar(); break;
        case 3: MostrarEstudiantes(); break;
        case 4: MostrarCurso(); break;
        case 5: System.Console.WriteLine("\nSaliendo del programa...\n"); Environment.Exit(0); break;
        default: System.Console.WriteLine("Error: Ingrese una opcion valida"); break;
    }
}


//Funciones
void Registrar()
{
    //Codigo
    do
    {
        System.Console.Write("\nCodigo (Solo numeros): ");
        verify = VerificarNumeros(Console.ReadLine() ?? "");
        if (lista_Estudiantes.Count != 0)
        {
            foreach (var i in lista_Estudiantes)
            {
                if (i.Codigo == numTemp)
                {
                    System.Console.WriteLine("\nEse codigo ya esta registrado, ingrese otro\n");
                    Registrar();
                }
            }
        }
        codigo = numTemp;

    } while (!verify);
    
    //Nombre
    do
    {
        System.Console.Write("Nombre: ");
        verify = VerificarTexto(Console.ReadLine() ?? "");
        nombre = textTemp;

    } while (!verify);

    //Calificacion Practica
    do
    {
        System.Console.Write("Calificacion Practica: ");
        verify = VerificarNumeros(Console.ReadLine() ?? "");
        cali1 = numTemp;

    } while (!verify);

    //Calificacion Examen
    do
    {
        System.Console.Write("Calificacion Examen: ");
        verify = VerificarNumeros(Console.ReadLine() ?? "");
        cali2 = numTemp;

    } while (!verify);

    //Calificacion Proyecto
    do
    {
        System.Console.Write("Calificacion Proyecto: ");
        verify = VerificarNumeros(Console.ReadLine() ?? "");
        cali3 = numTemp;
        
    } while (!verify);


    //Crear estudiante
    try
    {
        var estudiante = EstudiantesFactory.Crear(codigo, nombre, cali1, cali2, cali3);
        lista_Estudiantes.Add(estudiante);
        System.Console.WriteLine("\nEstudiante agregado con exito");
        System.Console.WriteLine("Presione una tecla para volver al menu...\n");
        Console.ReadKey();
        
    } catch (Exception ex)
    {
        System.Console.WriteLine($"Ha ocurrido un error: {ex.Message}\n");
    }

    System.Console.WriteLine("Volviendo al menu en");
    Thread.Sleep(1000);
    System.Console.WriteLine("3");
    Thread.Sleep(1000);
    System.Console.WriteLine("2");
    Thread.Sleep(1000);
    System.Console.WriteLine("1");
    Thread.Sleep(1000);
    Menu();
    
}

void Consultar()
{
    
}

void MostrarEstudiantes()
{
    
}

void MostrarCurso()
{
    
}


bool VerificarNumeros(string num)
{
    if(!int.TryParse(num, out numTemp))
    {
        System.Console.WriteLine("\nError: Ingrese un numero\n");
        return false;
    }
    else if (numTemp <= 0)
    {
        System.Console.WriteLine("\nError: Ingrese un numero mayor a 0\n");
        return false;
    }
    return true;
}

bool VerificarTexto(string text)
{
    if(string.IsNullOrWhiteSpace(text))
    {
        System.Console.WriteLine("\nError: Ingrese su nombre\n");
        return false;
    } 
    if(int.TryParse(text, out int temp))
    {
        System.Console.WriteLine("\nError: Ingrese un nombre valido\n");
        return false;
    }
    textTemp = text;
    return true;
}