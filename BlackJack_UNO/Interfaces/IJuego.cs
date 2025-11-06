using System;

namespace BlackJack_1.Interfaces;

public interface IJuego: IEstadoJuego
{
    void IniciarJuego();
    void RepartirCartas();
    void JugarTurno();
    void MostrarEstado();
    void DeterminarGanador();
    void FinalizarJuego();
}
