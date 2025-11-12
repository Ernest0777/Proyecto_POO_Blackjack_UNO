namespace BlackJack_1.Interfaces;

using BlackJack_1.Juegos.Blackjack;

public interface IEstrategiaJugadorBlackjack
{
    
    string NombreEstrategia { get; }

    // Última acción ejecutada por el jugador para logs 
    string UltimaAccion { get; }

    // Decide la acción del jugador segun su mano y el estado actual del juego
    
    string DecidirAccion(JugadorBlackjack jugador, IJuego juego);

    // Ejecuta la decisión anterior 
    void EjecutarDecision(JugadorBlackjack jugador, IJuego juego);
}

