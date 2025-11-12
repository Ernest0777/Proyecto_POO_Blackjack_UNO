namespace BlackJack_1.Juegos.Uno;

using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;


public class CartaUno : Carta
{
    public new TipoCartaUno Tipo { get; }

    // Constructor: define el color  valor y tipo
    public CartaUno(string color, string valor, TipoCartaUno tipo)
        : base(color, valor, tipo.ToString())
    {
        Tipo = tipo;
    }

    // Retorna el valor numérico de la carta solo para fines de comparación 
    public override int ObtenerValorNumerico()
    {
        return Tipo switch
        {
            TipoCartaUno.Normal => int.TryParse(Valor, out int n) ? n : 0,
            TipoCartaUno.MasDos => 20,
            TipoCartaUno.MasCuatro => 50,
            TipoCartaUno.Bloqueo => 20,
            TipoCartaUno.Reversa => 20,
            TipoCartaUno.CambioColor => 50,
            _ => 0
        };
    }

    public override string ToString()
    {
        return $"{Valor} ({Color})";
    }
}

// Enumeración con todos los tipos de cartas UNO
public enum TipoCartaUno
{
    Normal,
    MasDos,
    MasCuatro,
    Bloqueo,
    Reversa,
    CambioColor
}
