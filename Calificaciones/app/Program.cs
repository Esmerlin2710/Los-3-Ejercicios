using cali.app.factory.factory;
using cali.app.models.clases;
using System.ComponentModel.Design;
using System.Threading;

int opcion;
int numTemp;
string textTemp = "";
bool verify;
int codigo = 0;
int codigoTemp;
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
                    break;
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
        System.Console.WriteLine("\nEstudiante agregado con exito\n");
        System.Console.WriteLine("1. Registrar otro estudiante");
        System.Console.WriteLine("2. Menu");
        System.Console.Write("\nElije: ");

        switch(Console.ReadLine())
        {
            case "1": Registrar(); break;
            case "2": Menu(); break;
            default: 
                System.Console.WriteLine("Opcion invalida");
                System.Console.WriteLine("Redireccionando al menu...");
                Thread.Sleep(1000);
                Menu();
                break;
        }
        
    } catch (Exception ex)
    {
        System.Console.WriteLine($"\nHa ocurrido un error: {ex.Message}\n");
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

    
}

void Consultar()
{

    if (lista_Estudiantes.Count() == 0)
        {
            System.Console.WriteLine("\nNo hay estudiantes registrados");
            Thread.Sleep(1000);
            Menu();
        }

    System.Console.Write("\nIngresa el codigo del estudiante: ");
    verify = VerificarNumeros(Console.ReadLine() ?? "");
    if (!verify)
    {
        System.Console.WriteLine("\nIngrese un codigo valido");
        Thread.Sleep(1000);
        Menu();
    }
    codigoTemp = numTemp;

    foreach (var i in lista_Estudiantes)
    {
        if (i.Codigo == codigoTemp)
        {
            System.Console.WriteLine($"\n============ ESTUDIANTE: {i.Nombre} ============\n");
            System.Console.WriteLine($"Pracica: {i.CaliPractica}");
            System.Console.WriteLine($"Examen: {i.CaliExamen}");
            System.Console.WriteLine($"Proyecto: {i.CaliProyecto}");
            System.Console.WriteLine($"Calificaciones final: {i.CalificacionFinal:F2}");
            System.Console.WriteLine($"Estado: {i.Clasificacion}\n");

            System.Console.WriteLine("1. Modificar notas");
            System.Console.WriteLine("2. Menu\n");
            System.Console.Write("Elije: ");

            switch (Console.ReadLine())
            {
                case "1": Modificar(); break;
                case "2": Menu(); break;
                default: 
                    System.Console.WriteLine("Opcion invalida");
                    System.Console.WriteLine("Redireccionando al menu...");
                    Thread.Sleep(1000);
                    Menu();
                    break;
            }
        }
    }

    System.Console.WriteLine("\nCodigo no registrado");
    Thread.Sleep(1000);
    Menu();
}

void MostrarEstudiantes()
{
    int numEstudiante = 1;
    if (lista_Estudiantes.Count() == 0)
        {
            System.Console.WriteLine("\nNo hay estudiantes registrados");
            Thread.Sleep(1000);
            Menu(); 
        }
    foreach (var i in lista_Estudiantes)
    {
        System.Console.WriteLine($"\n============ ESTUDIANTE #{numEstudiante} ============\n");
        System.Console.WriteLine($"Codigo: {i.Codigo}");
        System.Console.WriteLine($"Nombre: {i.Nombre}");
        System.Console.WriteLine($"Pracica: {i.CaliPractica}");
        System.Console.WriteLine($"Examen: {i.CaliExamen}");
        System.Console.WriteLine($"Proyecto: {i.CaliProyecto}");
        System.Console.WriteLine($"Calificaciones final: {i.CalificacionFinal:F2}");
        System.Console.WriteLine($"Estado: {i.Clasificacion}\n");
        numEstudiante++;
    }

    System.Console.WriteLine("Presione una tecla para volver al menu...");
    Console.ReadKey();
    Thread.Sleep(1000);
    Menu();

}

void MostrarCurso()
{
    double totalCali = 0;
    int aprobados = 0;
    int reprobados = 0;
    double caliAlta = 0;
    double caliBaja = 101;
    string? mejorCali = "";

    if (lista_Estudiantes.Count() == 0)
        {
            System.Console.WriteLine("\nNo hay estudiantes registrados");
            Thread.Sleep(1000);
            Menu(); 
        }

    int cantEstudiantes = lista_Estudiantes.Count();

    foreach (var i in lista_Estudiantes)
    {
        totalCali += i.CalificacionFinal;

        if (i.CalificacionFinal >= 70)
        {
            aprobados++;
        }
        else
        {
            reprobados++;
        }

        if (i.CalificacionFinal > caliAlta)
        {
            caliAlta = i.CalificacionFinal;
            mejorCali = i.Nombre;
        }

        if (i.CalificacionFinal < caliBaja)
        {
            caliBaja = i.CalificacionFinal;
        }

    }


    System.Console.WriteLine("\n========== RESUMEN CURSO ==========\n");
    System.Console.WriteLine($"Total de estudiantes: {cantEstudiantes}");
    System.Console.WriteLine($"Promedio General: {totalCali / cantEstudiantes:F2}");
    System.Console.WriteLine($"Cantidad de aprobados: {aprobados}");
    System.Console.WriteLine($"Cantidad de reprobados: {reprobados}");
    System.Console.WriteLine($"Calificacion mas alta: {caliAlta:F2}");
    System.Console.WriteLine($"Calificacion mas baja: {caliBaja:F2}");
    System.Console.WriteLine($"Estudiante con mejor calificacion: {mejorCali}");
    System.Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
    Console.ReadKey();
    Menu();
}

void Modificar()
{
    //Calificacion Practica
    do
    {
        System.Console.Write("\nCalificacion Practica: ");
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


    foreach (var i in lista_Estudiantes)
    {
        i.CaliPractica = cali1;
        i.CaliExamen = cali2;
        i.CaliProyecto = cali3;
        i.CalificacionFinal = (cali1 * 0.3) + (cali2 * 0.4) + (cali3 * 0.3);
    }

    System.Console.WriteLine("\nLas notas fueron modificadas con exito");
    System.Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
    Console.ReadKey();
    Menu();
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