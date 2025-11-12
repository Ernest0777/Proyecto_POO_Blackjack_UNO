using System;

namespace BlackJack_1.Interfaces;

public interface IJugadorUno: IJugador
{
    IEstrategiaJugadorUno Estrategia { get; set; }

}
