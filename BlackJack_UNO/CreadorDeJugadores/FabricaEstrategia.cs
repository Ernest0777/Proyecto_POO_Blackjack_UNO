namespace BlackJack_1.CreadorDeJugadores;

using BlackJack_1.Interfaces;
using BlackJack_1.Estrategias;

public class FabricaEstrategias
{
    // Devuelve una estrategia adecuada para jugadores de UNO
    public IEstrategiaJugadorUno CrearEstrategiaUNO(string tipo)
    {
        tipo = tipo.ToLower().Trim();

        return tipo switch
        {
            "aleatoria" => new EstrategiaAleatoria(),
            "calculadora" => new EstrategiaCalculadora(),
            _ => new EstrategiaAleatoria() // Estrategia por defecto
        };
    }

    // Devuelve una estrategia adecuada para jugadores de Blackjack
    public IEstrategiaJugadorBlackjack CrearEstrategiaBlackjack(string tipo)
    {
        tipo = tipo.ToLower().Trim();

        return tipo switch
        {
            "cautelosa" => new EstrategiaCautelosa(),
            "temeraria" => new EstrategiaTemeraria(),
            _ => new EstrategiaCautelosa() // Estrategia por defecto
        };
    }
}
