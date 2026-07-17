using System.Security.Cryptography.X509Certificates;
using cali.app.models.clases;
using cali.app.models.enums;

namespace cali.app.factory.factory;

public static class EstudiantesFactory
{
    public static Estudiante Crear(
        int codigo,
        string nombre,
        int calipractica,
        int caliexamen,
        int caliproyecto
    )
    {
        Clasificacion clasificacion;
        if(string.IsNullOrWhiteSpace(nombre)) 
            throw new ArgumentException("\nDebes ingresar un nombre valido.", nameof(nombre));
        if(calipractica < 0)
            throw new ArgumentException("\nLa calificacion no puede ser menor o igual a 0.", nameof(calipractica));
        if(caliexamen < 0)
            throw new ArgumentException("\nLa calificacion no puede ser menor o igual a 0.", nameof(caliexamen));
        if(caliproyecto < 0)
            throw new ArgumentException("\nLa calificacion no puede ser menor o igual a 0.", nameof(caliproyecto));
        
        double calificacionFinal = (calipractica * 0.3) + (caliexamen * 0.4) + (caliproyecto * 0.3);

        if (calificacionFinal >= 90 && calificacionFinal <= 100)
        {
            Enum.TryParse<Clasificacion>("Excelente", out clasificacion);
        } 
        else if (calificacionFinal >= 80 && calificacionFinal <= 89)
        {
            Enum.TryParse<Clasificacion>("MuyBueno", out clasificacion);
        }
        else if (calificacionFinal >= 70 && calificacionFinal <= 79)
        {
            Enum.TryParse<Clasificacion>("Aprobado", out clasificacion);
        } else if (calificacionFinal >= 0 && calificacionFinal <= 69)
        {
            Enum.TryParse<Clasificacion>("Reprobado", out clasificacion);
        } else
        {
            throw new ArgumentException("Error: Debes poner una calificacion entre 0 y 100");
        }

        return new Estudiante
        {
            Codigo = codigo,
            Nombre = nombre,
            CaliPractica = calipractica,
            CaliExamen = caliexamen,
            CaliProyecto = caliproyecto,
            CalificacionFinal = calificacionFinal,
            Clasificacion = clasificacion
        };
    }
}