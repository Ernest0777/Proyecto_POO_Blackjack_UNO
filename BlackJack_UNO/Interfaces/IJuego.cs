using System;

namespace BlackJack_1.Interfaces;

public interface IJuego
{
    void IniciarJuego();
    void RepartirCartas();
    void JugarTurno();
    void MostrarEstado();
    void DeterminarGanador();

    string NombreJuego { get; set; }
    List<IJugador> Jugadores { get; set; }
    List<ICarta> Mazo { get; set; }
    List<ICarta> Descarte { get; set; }
    int TurnoActual { get; set; }
}
