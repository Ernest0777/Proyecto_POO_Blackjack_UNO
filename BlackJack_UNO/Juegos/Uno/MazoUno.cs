using System;
using BlackJack_1.ModelosBase;

namespace BlackJack_1.Juegos.Uno;

public class MazoUno: Mazo<CartaUno>
{
 public MazoUno()
    {
        GenerarCartas();
        Barajar();
    }

    private void GenerarCartas()
    {
        // Cartas normales del 0 al 9 Dos copias excepto 0
        foreach (var color in Enum.GetValues<ColorCartaUno>())
        {
            if (color == ColorCartaUno.Negro)
                continue; // Los comodines no tienen color

            for (int numeroActual = 0; numeroActual <= 9; numeroActual++)
            {
                AgregarCarta(new CartaUno(color, TipoCartaUno.Normal, numeroActual));
                if (numeroActual != 0)
                    AgregarCarta(new CartaUno(color, TipoCartaUno.Normal, numeroActual));
            }

            // Cartas especiales por color 
            for (int cantidad = 0; cantidad < 2; cantidad++)
            {
                AgregarCarta(new CartaUno(color, TipoCartaUno.MasDos));
                AgregarCarta(new CartaUno(color, TipoCartaUno.Bloqueo));
                AgregarCarta(new CartaUno(color, TipoCartaUno.Reversa));
            }
        }

        //  Comodines 
        for (int cantidad = 0; cantidad < 4; cantidad++)
        {
            AgregarCarta(new CartaUno(ColorCartaUno.Negro, TipoCartaUno.MasCuatro));
            AgregarCarta(new CartaUno(ColorCartaUno.Negro, TipoCartaUno.CambioColor));
        }
    }
}
