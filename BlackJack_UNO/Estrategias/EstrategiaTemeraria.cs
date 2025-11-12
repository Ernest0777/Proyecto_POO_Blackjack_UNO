namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Blackjack;

public class EstrategiaTemeraria : IEstrategiaJugadorBlackjack
{
    public string NombreEstrategia => "Temeraria";
    public string UltimaAccion { get; private set; } = "Esperando turno";

    // pide hasta tener 19 o más puntos
    public string DecidirAccion(JugadorBlackjack jugador, IJuego juego)
    {
        return jugador.ObtenerPuntos() < 19 ? "Pedir" : "Plantarse";
    }

    // Ejecuta la acción elegida
    public void EjecutarDecision(JugadorBlackjack jugador, IJuego juego)
    {
        if (juego is not Blackjack blackjack)
        {
            UltimaAccion = "Error: contexto no válido.";
            return;
        }

        var decision = DecidirAccion(jugador, juego);

        if (decision == "Pedir")
        {
            var carta = blackjack.MazoBlackjack.SacarCarta();
            jugador.RecibirCarta(carta);
            int puntos = jugador.ObtenerPuntos();

            if (puntos > 21)
                UltimaAccion = $"Pidió carta ({carta}) → Total: {puntos} ❌ SE PASÓ";
            else
                UltimaAccion = $"Pidió carta ({carta}) → Total: {puntos}";
        }
        else
        {
            UltimaAccion = $"Se plantó con {jugador.ObtenerPuntos()} puntos.";
        }

        jugador.NotificarAccion(UltimaAccion);
    }
}

