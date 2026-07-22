using System.Data.Common;
using app.models.enums;

namespace app.models.clases;

public class Producto
{
    private int _id;
    private string? _nombre;
    private decimal _precio;
    private int _stock;


    public int ID
    {
        get => _id;
        set
        {
            _id = value;
        }
    }

    public string Nombre
    {
        get => _nombre ?? "";
        set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Error: El nombre no puede ser nulo");
            _nombre = value;
        }
    }

    public decimal Precio
    {
        get => _precio;
        set
        {
            if(value < 0)
                throw new ArgumentException("Error: El precio debe ser mayor a 0");
            _precio = value;
        }
    }

    public int Stock
    {
        get => _stock;
        set
        {
            if(value < 0)
                throw new ArgumentException("Error: La cantidad debe ser mayor a 0");
            _stock = value;
        }
    }

}

public class Pedido
{
    private int _id;
    private string? _cliente;
    private DateTime _FechaCreacion {get; set;} = DateTime.Now;
   
    private List<DetallePedido>? _detallePedido;



    public int ID
    {
        get => _id;
        set
        {
            _id = value;
        }
    }

    public string Cliente
    {
        get => _cliente ?? "";
        set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Error: El nombre no puede ser nulo");
            _cliente = value;
        }
    }

    public DateTime FechaCreacion
    {
        get => _FechaCreacion;
    }

    public Estado Estado {get; set;}

    public List<DetallePedido>? DetallePedido
    {
        get => _detallePedido;
        set
        {
            _detallePedido = value;
        }
    }

    

}

public class DetallePedido
{
    public Producto? _Producto { get; set; }
    public int Cantidad { get; set; }
}