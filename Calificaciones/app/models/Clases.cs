using cali.app.models.enums;

namespace cali.app.models.clases;


public class Estudiante
{
    //Propiedades de los estudiantes
    public int _codigo;
    private string? _nombre;
    public int _caliPractica;
    public int _caliExamen;
    public int _caliProyecto;
    public double _calificacionFinal;
    public Clasificacion _clasificacion;



    //Validaciones en la clase
    public int Codigo
    {
        get => _codigo;
        set
        {
            _codigo = value;
        }
    }
    public string? Nombre
    {
        get => _nombre;
        set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Error: Ingrese un nombre");
            _nombre = value;
                
        }
    }

    public int CaliPractica
    {
        get => _caliPractica;
        set
        {
            if (value < 0)
                throw new ArgumentException("Error: La calificacion no puede ser menor o igual que 0");
            _caliPractica = value;
        }
    }
    public int CaliExamen
    {
        get => _caliExamen;
        set
        {
            if (value < 0)
                throw new ArgumentException("Error: La calificacion no puede ser menor o igual que 0");
            _caliExamen = value;
        }
    }
    public int CaliProyecto
    {
        get => _caliProyecto;
        set
        {
            if (value < 0)
                throw new ArgumentException("Error: La calificacion no puede ser menor o igual que 0");
            _caliProyecto = value;
        }
    }
    public double CalificacionFinal
    {
        get => _calificacionFinal;
        set
        {
            if (value < 0)
                throw new ArgumentException("Error: La calificacion no puede ser menor o igual que 0");
            _calificacionFinal = value;
        }
    }

    public Clasificacion Clasificacion
    {
        get => _clasificacion;
        set
        {
            _clasificacion = value;
        }
    }

    


}