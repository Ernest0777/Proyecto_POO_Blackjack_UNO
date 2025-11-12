namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Blackjack;

public class EstrategiaCautelosa : IEstrategiaJugadorBlackjack
{
    public string NombreEstrategia => "Cautelosa";
    public string UltimaAccion { get; private set; } = "Esperando turno";
    private readonly int limitePuntos;

    public EstrategiaCautelosa(int limite = 16)
    {
        limitePuntos = limite;
    }

    public string DecidirAccion(JugadorBlackjack jugador, IJuego juego)
    {
        return jugador.ObtenerPuntos() < limitePuntos ? "Pedir" : "Plantarse";
    }

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
