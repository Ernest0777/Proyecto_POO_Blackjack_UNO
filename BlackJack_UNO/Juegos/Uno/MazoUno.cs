namespace BlackJack_1.Juegos.Uno;

using System;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

public class MazoUno : Mazo<CartaUno>, IMazo
{
    public MazoUno()
    {
        GenerarCartas();
        Barajar();
    }

    // Genera todas las cartas de UNO 
    private void GenerarCartas()
    {
        // Colores principales (no incluye los comodines negros)
        string[] colores = { "Rojo", "Azul", "Verde", "Amarillo" };

        foreach (var color in colores)
        {
            // Cartas numericas dos copias del 1 al 9 una sola del 0
            for (int numero = 0; numero <= 9; numero++)
            {
                var cartaNormal = new CartaUno(color, numero.ToString(), TipoCartaUno.Normal);
                AgregarCarta(cartaNormal);

                if (numero != 0)
                    AgregarCarta(new CartaUno(color, numero.ToString(), TipoCartaUno.Normal));
            }

            // Cartas especiales (+2, Bloqueo, Reversa) dos por color
            for (int i = 0; i < 2; i++)
            {
                AgregarCarta(new CartaUno(color, "+2", TipoCartaUno.MasDos));
                AgregarCarta(new CartaUno(color, "Bloqueo", TipoCartaUno.Bloqueo));
                AgregarCarta(new CartaUno(color, "Reversa", TipoCartaUno.Reversa));
            }
        }
            // Comodines negros +4 y Cambio de color 4 de cada uno
        for (int i = 0; i < 4; i++)
        {
            AgregarCarta(new CartaUno("Negro", "+4", TipoCartaUno.MasCuatro));
            AgregarCarta(new CartaUno("Negro", "Cambio Color", TipoCartaUno.CambioColor));
        }
    }
}
