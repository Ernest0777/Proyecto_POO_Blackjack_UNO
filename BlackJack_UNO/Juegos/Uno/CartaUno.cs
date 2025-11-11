using System;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;
namespace BlackJack_1.Juegos.Uno;

public enum ColorCartaUno
{
Rojo,
    Azul,
    Verde,
    Amarillo,
    Negro  // usado para comodines
}
public enum TipoCartaUno
{
    Normal,
    MasDos,
    MasCuatro,
    Reversa,
    Bloqueo,
    CambioColor
}



public class CartaUno : Carta
{
    public ColorCartaUno Color { get; }
    public TipoCartaUno Tipo { get; }
    public int? Numero { get; } 
    public CartaUno(ColorCartaUno color, TipoCartaUno tipo, int? numero = null)
    {
        Color = color;
        Tipo = tipo;
        Numero = numero;
    }

    public override string ToString()
    {
        return Tipo == TipoCartaUno.Normal
            ? $"{Numero} {Color}"
            : $"{Tipo} {Color}";
    }
}