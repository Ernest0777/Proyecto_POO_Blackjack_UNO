using System;
using System.Collections.Generic;

namespace BlackJack_1.Interfaces;

public interface IMazo
{
    // Mezcla las cartas actuales del mazo
    void Barajar();

    // Devuelve la cantidad de cartas restantes
    int CartasRestantes();

    // Saca la carta superior del mazo
    ICarta SacarCarta();

    // Devuelve una lista con las cartas actuales solo para pruebas 
    IEnumerable<ICarta> MostrarCartas();
}
