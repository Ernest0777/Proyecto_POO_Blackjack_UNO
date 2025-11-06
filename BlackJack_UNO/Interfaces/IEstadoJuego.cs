using System.Collections.Generic;

namespace BlackJack_1.Interfaces;

public interface IEstadoJuego
{
    string NombreJuego { get; }
    IReadOnlyList<IJugador> Jugadores { get; }
    IReadOnlyList<ICarta> Mazo { get; }
    IReadOnlyList<ICarta> Descarte { get; }
    int TurnoActual { get; }
    string ObtenerResumen();

}
