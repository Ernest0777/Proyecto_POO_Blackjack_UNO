using System;

namespace BlackJack_1.Estrategias;

public class EstrategiaCautelosa
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
            UltimaAccion = "Error: contexto no valido.";
            return;
        }

        var decision = DecidirAccion(jugador, juego);

        if (decision == "Pedir")
        {
            var carta = blackjack.MazoBlackjack.SacarCarta();
            jugador.RecibirCarta(carta);
            UltimaAccion = $"Pidio carta ({carta}) → Total: {jugador.ObtenerPuntos()}";
        }
        else
        {
            UltimaAccion = $"Se planto con {jugador.ObtenerPuntos()} puntos.";
        }

        jugador.NotificarAccion(UltimaAccion);
    }
}
