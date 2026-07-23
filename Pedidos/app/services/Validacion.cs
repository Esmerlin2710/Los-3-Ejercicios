using app.global.global;

namespace app.services.validacion;

public static class Validacion
{
    public static bool VerificarTexto(string text)
    {
        if(string.IsNullOrWhiteSpace(text))
        {
            Global.Error = "Texto vacio o nulo";
            return false;
        }
        else if (double.TryParse(text, out var temp))
        {
            Global.Error = "El texto no puede ser un numero";
            return false;
        }
        return true;
    }

    public static bool VerificarInt(string num)
    {
        if(!int.TryParse(num, out var num2))
        {
            Global.Error = "Ingrese un numero";
            return false;
        }  
        else if (num2 <= 0)
        {
            Global.Error = "El numero ingresado no puede ser negativo o 0";
            return false;
        }  
        else
        {
            return true;
        }      
    }

    public static bool VerificarDecimal(string num)
    {
        if(!decimal.TryParse(num, out var num2))
        {
            Global.Error = "Ingrese un numero";
            return false;
        }  
        else if (num2 <= 0)
        {
            Global.Error = "El numero ingresado no puede ser negativo o 0";
            return false;
        }  
        else
        {
            return true;
        }      
    }

}