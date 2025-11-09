using System;
using BlackJack_1.Interfaces;
namespace BlackJack_1.ModelosBase;

    public abstract class Carta: ICarta
    {
 public string Color { get;protected init; }
        public string Valor { get; protected init; }
        public string Tipo { get; protected init; }

        protected Carta(string color, string valor, string tipo)
        {
            Color = color;
            Valor = valor;
            Tipo = tipo;
        }

    public abstract int ObtenerValorNumerico();

     public virtual string NombreCorto => $"{Valor}{Color[0]}";

    protected static bool SoportaUnicode()
{
    try
    {
        
        return Console.OutputEncoding.Equals(System.Text.Encoding.UTF8);
    }
    catch
    {
        
        return false;
    }
}


    public override string ToString()
    {
        // Si la consola no soporta Unicode mostramos solo el texto
        if (!SoportaUnicode())
            return $"{Valor} de {Color} ({Tipo})";

        
        return $"{Valor} de {Color} ({Tipo})";
    }
}