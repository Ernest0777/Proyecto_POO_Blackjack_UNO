namespace BlackJack_1.Interfaces;

using BlackJack_1.Juegos.Uno;



public interface IEstrategiaJugadorUno 
{
    string NombreEstrategia { get; }

    string UltimaAccion { get; }

    // Metodo principal que decide que carta jugar en el turno.
    // Si devuelve null significa que el jugador debe comer
    CartaUno? DecidirCarta(
        JugadorUno jugadorActual,
        CartaUno cartaSuperior,
        JugadorUno? siguienteJugador = null,
        MazoUno? mazo = null);
}
