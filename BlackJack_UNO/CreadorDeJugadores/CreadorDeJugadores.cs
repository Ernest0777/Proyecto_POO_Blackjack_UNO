using System;

namespace BlackJack_1.CreadorDeJugadores;

using BlackJack_1.Juegos.Uno;
using BlackJack_1.Juegos.Blackjack;
using BlackJack_1.Estrategias;
using BlackJack_1.Interfaces;

public class CreadorDeJugadores
{
    private readonly FabricaEstrategias fabrica = new();

    // Crea jugadores de UNO con una estrategia especifica
    public JugadorUno CrearJugadorUno(string nombre, string tipoEstrategia)
    {
        var estrategia = fabrica.CrearEstrategiaUNO(tipoEstrategia);
        return new JugadorUno(GenerarId(), nombre, estrategia);
    }

    // Crea jugadores de Blackjack con una estrategia especifica
    public JugadorBlackjack CrearJugadorBlackjack(string nombre, string tipoEstrategia)
    {
        var estrategia = fabrica.CrearEstrategiaBlackjack(tipoEstrategia);
        return new JugadorBlackjack(GenerarId(), nombre, estrategia);
    }

    // Metodo auxiliar para asignar IDs
    private static int idActual = 1;
    private static int GenerarId() => idActual++;
}
