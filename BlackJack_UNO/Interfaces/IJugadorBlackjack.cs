using System;

namespace BlackJack_1.Interfaces;

public interface IJugadorBlackjack: IJugador
{
    IEstrategiaJugadorBlackjack Estrategia { get; set; }

}
