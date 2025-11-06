namespace BlackJack_1.Interfaces;

public interface IEstrategiaJugador
{
    string DecidirAccion(IJugador jugador, IJuego juego);
    string EjecutarDecision(IJugador jugador, IJuego juego);

    string NombreEstrategia{ get;  }

    string UltimaAccion{ get;  }
}
